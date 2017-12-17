using Ionic.Zip;
using Newtonsoft.Json;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Nucleus.Gaming.Repo
{
    public class RepoManager
    {
        private CoopConfigInfo config;
        private Dictionary<string, RepoHeader> repos;

        public RepoManager(CoopConfigInfo config)
        {
            this.config = config;

            Initialize();
        }

        private void Initialize()
        {
            repos = new Dictionary<string, RepoHeader>();

            int totalHeaders = config.RepoHeaders.Count;
            for (int i = 0; i < totalHeaders; i++)
            {
                DownloadHeader(config.RepoHeaders[i]);
            }
        }

        public void UpdateHeader(string headerUrl, RAction<RepoHeader> callback)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler((object sender, DownloadStringCompletedEventArgs e) =>
            {
                var result = new RequestResult<RepoHeader>();

                if (e.Error != null)
                {
                    string log = "Downloading header for repository (" + headerUrl + ") failed with error " + e.Error;
                    Log.WriteLine(log, Palette.Warning);
                    result.LogData = log;
                    result.Failed = true;
                    result.AdditionalData = e.Error;
                    callback(result);
                    return;
                }

                RepoHeader header = JsonConvert.DeserializeObject<RepoHeader>(e.Result);
                header.Url = headerUrl;
                if (string.IsNullOrEmpty(header.RepositoryId))
                {
                    string log = "Parsed repository (" + headerUrl + ") header, but it has no RepositoryId.";
                    Log.WriteLine(log, Palette.Warning);
                    result.LogData = log;
                    result.Failed = true;
                    callback(result);
                    return;
                }

                if (repos.ContainsKey(header.RepositoryId))
                {
                    repos.Remove(header.RepositoryId);
                }
                repos.Add(header.RepositoryId, header);

                result.Success = true;
                result.Data = header;
                result.AdditionalData = e.Result;

                callback(result);
            });
            client.DownloadStringAsync(new Uri(headerUrl));
        }

        private void DownloadHeader(string url)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted;
            client.DownloadStringAsync(new Uri(url));
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Log.WriteLine("Downloading header for repository failed with error " + e.Error, Palette.Warning);
                return;
            }

            RepoHeader header = JsonConvert.DeserializeObject<RepoHeader>(e.Result);
            if (string.IsNullOrEmpty(header.RepositoryId))
            {
                Log.WriteLine("Parsed repository header, but it has no RepositoryId.", Palette.Warning);
                return;
            }
            repos.Add(header.RepositoryId, header);
        }

        public void RequestPackageFullInfo(RepoHeader header, RepoGameHandlerInfo info, int version, RAction<RepoGameHandlerFullInfo> callback)
        {
            Uri uri = new Uri(new Uri(header.Url), 
                header.PackagesInfoRoot + "/" + info.HandlerID.ToString(CultureInfo.InvariantCulture) + @"/info-" + version.ToString(CultureInfo.InvariantCulture) + ".json");
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler((object sender, DownloadStringCompletedEventArgs e) =>
            {
                var result = new RequestResult<RepoGameHandlerFullInfo>();
                Exception ex = e.Error;

                if (e.Error == null)
                {
                    try
                    {
                        var gameFullInfo = JsonConvert.DeserializeObject<RepoGameHandlerFullInfo>(e.Result);
                        result.Success = true;
                        result.Data = gameFullInfo;
                        result.AdditionalData = e.Result;
                    }
                    catch (Exception nEx)
                    {
                        ex = nEx;
                    }
                }

                if (ex != null)
                {
                    string log = "Request for full Package info failed with error " + ex;
                    Log.WriteLine(log, Palette.Error);
                    result.LogData = log;
                    result.Failed = true;
                    result.AdditionalData = ex;
                }

                callback(result);
            });

            client.DownloadStringAsync(uri);
        }

        public void RequestPackageDownload(RepoHeader header, RepoGameHandlerInfo info, RAction<string> callback)
        {
            Uri uri = new Uri(new Uri(header.Url), header.PackagesRoot + "/" + info.HandlerID.ToString(CultureInfo.InvariantCulture) + "-" + info.V + ".nc");
            WebClient client = new WebClient();
            string tmp = GameManager.Instance.GetPackageTmpPath();
            Directory.CreateDirectory(tmp);

            string tmpName = info.HandlerID + "-" + info.ExeName + "-" + Guid.NewGuid().ToString() + ".nc";
            string tmpFile = Path.Combine(tmp, tmpName);

            client.DownloadFileCompleted += new AsyncCompletedEventHandler((object sender, AsyncCompletedEventArgs e) =>
            {
                var result = new RequestResult<string>();
                Exception ex = e.Error;

                if (e.Error == null)
                {
                    result.Data = tmpFile;
                    result.Success = true;
                }

                if (ex != null)
                {
                    string log = "Request for package download failed with error " + ex;
                    Log.WriteLine(log, Palette.Error);
                    result.LogData = log;
                    result.Failed = true;
                    result.AdditionalData = ex;
                }

                callback(result);
            });

            client.DownloadFileAsync(uri, Path.Combine(tmp, tmpName));
        }

        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public RepoGameHandlerFullInfo ReadInfoFromInstalledHandler(UserInstalledHandler handler)
        {
            return ReadInfoFromPackageFile(handler.PackagePath);
        }

        public RepoGameHandlerFullInfo ReadInfoFromPackageFile(string pkgPath)
        {
            ZipFile zip = new ZipFile(pkgPath);
            ZipEntry entry = zip["info.json"];
            using (Stream str = entry.OpenReader())
            {
                using (StreamReader reader = new StreamReader(str))
                {
                    string fullGameInfo = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<RepoGameHandlerFullInfo>(fullGameInfo);
                }
            }
        }

        public GenericHandlerData ReadHandlerDataFromPackageFile(UserInstalledHandler handler)
        {
            return ReadHandlerDataFromPackageFile(handler.PackagePath);
        }

        public GenericHandlerData ReadHandlerDataFromPackageFile(string pkgPath)
        {
            ZipFile zip = new ZipFile(pkgPath);
            ZipEntry entry = zip["game.js"];
            using (Stream str = entry.OpenReader())
            {
                return new GenericHandlerData(str);
            }
        }

        public RepoGameHandlerInfo GetGameInfo(string repositoryId, string handlerId)
        {
            int totalHeaders = config.RepoHeaders.Count;

            RepoHeader h = repos.FirstOrDefault(c => c.Value.RepositoryId == repositoryId).Value;
            if (h == null)
            {
                return null;
            }

            RepoGameHandlerInfo gameInfo = h.Games.FirstOrDefault(c => c.HandlerID == handlerId);
            return gameInfo;
        }

        public List<RepoGameHandlerInfo> GetGameInfos(string repositoryId, string gameId)
        {
            int totalHeaders = config.RepoHeaders.Count;

            RepoHeader h = repos.FirstOrDefault(c => c.Value.RepositoryId == repositoryId).Value;
            if (h == null)
            {
                return null;
            }

            return h.Games.Where(c => c.GameID == gameId).ToList();
        }

        public UserInstalledHandler[] GetInstalledHandlers(string gameId)
        {
            int totalHeaders = config.RepoHeaders.Count;
            UserProfile user = GameManager.Instance.User;

            return user.InstalledHandlers.Where(c => c.HandlerInfo.GameID == gameId).ToArray();
        }

        public UserInstalledHandler GetInstalledHandler(string repoId, RepoGameHandlerInfo info)
        {
            return GetInstalledHandler(repoId, info.HandlerID, info.GameID);
        }

        public UserInstalledHandler GetInstalledHandler(string repoId, string handlerId, string gameId)
        {
            int totalHeaders = config.RepoHeaders.Count;
            UserProfile user = GameManager.Instance.User;

            return user.InstalledHandlers.FirstOrDefault(c => c.HandlerInfo.HandlerID == handlerId && 
                                                                c.HandlerInfo.RepositoryID == repoId && 
                                                                c.HandlerInfo.GameID == gameId);
        }

        public RepoGameHandlerInfo GetFirstInfo(string repositoryId, string gameId)
        {
            int totalHeaders = config.RepoHeaders.Count;

            RepoHeader h = repos.FirstOrDefault(c => c.Value.RepositoryId == repositoryId).Value;
            if (h == null)
            {
                return null;
            }

            return h.Games.FirstOrDefault(c => c.GameID == gameId);
        }

        public void InstallPackage(string path)
        {
            // MD5 check and copy the package to the downloaded packages folder
            RepoGameHandlerFullInfo fullInfo;
            ZipFile zip = new ZipFile(path);
            ZipEntry entry = zip["info.json"];
            using (Stream str = entry.OpenReader())
            {
                using (StreamReader reader = new StreamReader(str))
                {
                    string fullGameInfo = reader.ReadToEnd();
                    fullInfo = JsonConvert.DeserializeObject<RepoGameHandlerFullInfo>(fullGameInfo);
                }
            }

            string installed = GameManager.Instance.GetInstalledPackagePath();
            Directory.CreateDirectory(installed);

            string installedName = fullInfo.HandlerID + "-" + fullInfo.Version + "-" + fullInfo.PlatformVersion + "-" + fullInfo.Developer;
            string finalInstalled = Path.Combine(installed, installedName);
            File.Copy(path, finalInstalled);
        }
    }
}