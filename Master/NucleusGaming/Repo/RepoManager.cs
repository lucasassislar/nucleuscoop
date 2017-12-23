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
//using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Nucleus.Gaming.Repo
{
    public class RepoManager
    {
        public string InfoFileName = "info";
        public string HandlerFileName = "handler";
        public readonly string JsonFormat = ".json";
        public readonly string JsFormat = ".js";

        private CoopConfigInfo config;
        private GameManager gameManager;
        //private Dictionary<string, RepoHeader> repos;

        public RepoManager(CoopConfigInfo config)
        {
            this.config = config;

            gameManager = GameManager.Instance;
            //Initialize();
        }

        //private void Initialize()
        //{
        //    repos = new Dictionary<string, RepoHeader>();

        //    int totalHeaders = config.RepoHeaders.Count;
        //    for (int i = 0; i < totalHeaders; i++)
        //    {
        //        DownloadHeader(config.RepoHeaders[i]);
        //    }
        //}

        //public void UpdateHeader(string headerUrl, RAction<RepoHeader> callback)
        //{
        //    WebClient client = new WebClient();
        //    client.DownloadStringCompleted += new DownloadStringCompletedEventHandler((object sender, DownloadStringCompletedEventArgs e) =>
        //    {
        //        var result = new RequestResult<RepoHeader>();

        //        if (e.Error != null)
        //        {
        //            string log = "Downloading header for repository (" + headerUrl + ") failed with error " + e.Error;
        //            Log.WriteLine(log, Palette.Warning);
        //            result.LogData = log;
        //            result.Failed = true;
        //            result.AdditionalData = e.Error;
        //            callback(result);
        //            return;
        //        }

        //        RepoHeader header = JsonConvert.DeserializeObject<RepoHeader>(e.Result);
        //        header.Url = headerUrl;
        //        if (string.IsNullOrEmpty(header.RepositoryId))
        //        {
        //            string log = "Parsed repository (" + headerUrl + ") header, but it has no RepositoryId.";
        //            Log.WriteLine(log, Palette.Warning);
        //            result.LogData = log;
        //            result.Failed = true;
        //            callback(result);
        //            return;
        //        }

        //        if (repos.ContainsKey(header.RepositoryId))
        //        {
        //            repos.Remove(header.RepositoryId);
        //        }
        //        repos.Add(header.RepositoryId, header);

        //        result.Success = true;
        //        result.Data = header;
        //        result.AdditionalData = e.Result;

        //        callback(result);
        //    });
        //    client.DownloadStringAsync(new Uri(headerUrl));
        //}

        //private void DownloadHeader(string url)
        //{
        //    WebClient client = new WebClient();
        //    client.DownloadStringCompleted += Client_DownloadStringCompleted;
        //    client.DownloadStringAsync(new Uri(url));
        //}

        //private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        Log.WriteLine("Downloading header for repository failed with error " + e.Error, Palette.Warning);
        //        return;
        //    }

        //    RepoHeader header = JsonConvert.DeserializeObject<RepoHeader>(e.Result);
        //    if (string.IsNullOrEmpty(header.RepositoryId))
        //    {
        //        Log.WriteLine("Parsed repository header, but it has no RepositoryId.", Palette.Warning);
        //        return;
        //    }
        //    repos.Add(header.RepositoryId, header);
        //}

        //public void RequestPackageFullInfo(RepoHeader header, GameHandlerPackageInfo info, int version, RAction<GameHandlerMetadata> callback)
        //{
        //    Uri uri = new Uri(new Uri(header.Url),
        //        header.PackagesInfoRoot + "/" + info.HandlerID.ToString(CultureInfo.InvariantCulture) + @"/info-" + version.ToString(CultureInfo.InvariantCulture) + ".json");
        //    WebClient client = new WebClient();
        //    client.DownloadStringCompleted += new DownloadStringCompletedEventHandler((object sender, DownloadStringCompletedEventArgs e) =>
        //    {
        //        var result = new RequestResult<GameHandlerMetadata>();
        //        Exception ex = e.Error;

        //        if (e.Error == null)
        //        {
        //            try
        //            {
        //                var gameFullInfo = JsonConvert.DeserializeObject<GameHandlerMetadata>(e.Result);
        //                result.Success = true;
        //                result.Data = gameFullInfo;
        //                result.AdditionalData = e.Result;
        //            }
        //            catch (Exception nEx)
        //            {
        //                ex = nEx;
        //            }
        //        }

        //        if (ex != null)
        //        {
        //            string log = "Request for full Package info failed with error " + ex;
        //            Log.WriteLine(log, Palette.Error);
        //            result.LogData = log;
        //            result.Failed = true;
        //            result.AdditionalData = ex;
        //        }

        //        callback(result);
        //    });

        //    client.DownloadStringAsync(uri);
        //}

        //public void RequestPackageDownload(RepoHeader header, GameHandlerPackageInfo info, RAction<string> callback)
        //{
        //    Uri uri = new Uri(new Uri(header.Url), header.PackagesRoot + "/" + info.HandlerID.ToString(CultureInfo.InvariantCulture) + "-" + info.V + ".nc");
        //    WebClient client = new WebClient();
        //    string tmp = GameManager.Instance.GetPackageTmpPath();
        //    Directory.CreateDirectory(tmp);

        //    string tmpName = info.HandlerID + "-" + info.ExeName + "-" + Guid.NewGuid().ToString() + ".nc";
        //    string tmpFile = Path.Combine(tmp, tmpName);

        //    client.DownloadFileCompleted += new AsyncCompletedEventHandler((object sender, AsyncCompletedEventArgs e) =>
        //    {
        //        var result = new RequestResult<string>();
        //        Exception ex = e.Error;

        //        if (e.Error == null)
        //        {
        //            result.Data = tmpFile;
        //            result.Success = true;
        //        }

        //        if (ex != null)
        //        {
        //            string log = "Request for package download failed with error " + ex;
        //            Log.WriteLine(log, Palette.Error);
        //            result.LogData = log;
        //            result.Failed = true;
        //            result.AdditionalData = ex;
        //        }

        //        callback(result);
        //    });

        //    client.DownloadFileAsync(uri, Path.Combine(tmp, tmpName));
        //}

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

        public GameHandlerMetadata ReadInfoFromInstalledHandler(UserInstalledHandler handler)
        {
            return ReadInfoFromPackageFile(handler.PackagePath);
        }

        public GameHandlerMetadata ReadMetadataFromFile(string metadataPath)
        {
            using (Stream str = File.OpenRead(metadataPath))
            {
                using (StreamReader reader = new StreamReader(str))
                {
                    string fullGameInfo = reader.ReadToEnd();
                    GameHandlerMetadata metadata = JsonConvert.DeserializeObject<GameHandlerMetadata>(fullGameInfo);
                    metadata.RootDirectory = Path.GetDirectoryName(metadataPath);
                    gameManager.NameManager.UpdateNaming(metadata);
                    return metadata;
                }
            }
        }

        public GameHandlerMetadata ReadInfoFromPackageFile(string pkgPath)
        {
            ZipFile zip = new ZipFile(pkgPath);
            ZipEntry entry = zip[InfoFileName + JsonFormat];
            using (Stream str = entry.OpenReader())
            {
                using (StreamReader reader = new StreamReader(str))
                {
                    string fullGameInfo = reader.ReadToEnd();
                    GameHandlerMetadata metadata = JsonConvert.DeserializeObject<GameHandlerMetadata>(fullGameInfo);
                    gameManager.NameManager.UpdateNaming(metadata);
                    return metadata;
                }
            }
        }

        public GenericHandlerData ReadHandlerDataFromInstalledPackage(GameHandlerMetadata handler)
        {
            string installPath = GetInstallPath(handler);
            string handlerPath = Path.Combine(installPath, HandlerFileName + JsFormat);

            if (!File.Exists(handlerPath))
            {
                return null;
            }

            using (Stream str = File.OpenRead(handlerPath))
            {
                return new GenericHandlerData(str);
            }
        }

        public GameHandlerMetadata GetFirstInstalledHandler(string gameId)
        {
            int totalHeaders = config.RepoHeaders.Count;
            UserProfile user = GameManager.Instance.User;

            return user.InstalledHandlers.FirstOrDefault(c => c.GameID == gameId);
        }

        public GameHandlerMetadata[] GetInstalledHandlers(string gameId)
        {
            int totalHeaders = config.RepoHeaders.Count;
            UserProfile user = GameManager.Instance.User;

            return user.InstalledHandlers.Where(c => c.GameID == gameId).ToArray();
        }

        public GameHandlerMetadata GetInstalledHandlerMetadata(string repoId, GameHandlerBaseMetadata info)
        {
            return GetInstalledHandlerMetadata(repoId, info.HandlerID, info.GameID);
        }

        public GameHandlerMetadata GetInstalledHandlerMetadata(string repoId, string handlerId, string gameId)
        {
            int totalHeaders = config.RepoHeaders.Count;
            UserProfile user = GameManager.Instance.User;

            return user.InstalledHandlers.FirstOrDefault(c => c.HandlerID == handlerId &&
                                                                c.GameID == gameId);
        }

        public static string GetInstallPath(GameHandlerMetadata metadata)
        {
            if (!string.IsNullOrEmpty(metadata.RootDirectory))
            {
                return metadata.RootDirectory;
            }

            string installed = GameManager.Instance.GetInstalledPackagePath();
            string installedName = metadata.GameID + "-H" + metadata.HandlerID + "-V" + metadata.V + "-N" + metadata.PlatV + "-" + metadata.Dev;
            return Path.Combine(installed, installedName);
        }

        public RequestResult<GameHandlerMetadata> InstallPackage(string path)
        {
            var result = new RequestResult<GameHandlerMetadata>();
            // MD5 check and copy the package to the downloaded packages folder
            GameHandlerMetadata metadata;
            ZipFile zip = new ZipFile(path);
            ZipEntry entry = zip["info.json"];
            using (Stream str = entry.OpenReader())
            {
                using (StreamReader reader = new StreamReader(str))
                {
                    string fullGameInfo = reader.ReadToEnd();
                    metadata = JsonConvert.DeserializeObject<GameHandlerMetadata>(fullGameInfo);
                    gameManager.NameManager.UpdateNaming(metadata);
                }
            }

            if (metadata.PlatV != Globals.Version)
            {
                result.LogLine("Package platform version mismatch (Package: {0}, Nucleus: {1})", metadata.PlatV, Globals.Version);
            }

            if (string.IsNullOrEmpty(metadata.Dev))
            {
                result.Failed = true;
                result.LogLine("Package has JSON data but no Dev information. Cannot install");
            }
            else
            {
                result.Success = true;

                string installPath = GetInstallPath(metadata);

                if (Directory.Exists(installPath))
                {
                    // game already installed?
                    result.LogLine("Package was already installed, removing old version.");
                    try
                    {
                        Directory.Delete(installPath, true);
                    }
                    catch (Exception ex)
                    {
                        result.LogLine("Old package failed to uninstall. Please remove manually or restart the app and try again");
                        result.AdditionalData = ex;
                        return result;
                    }
                }

                Directory.CreateDirectory(installPath);

                zip.ExtractAll(installPath, ExtractExistingFileAction.OverwriteSilently);
            }

            GameManager.Instance.RebuildGameDb();
            return result;
        }
    }
}