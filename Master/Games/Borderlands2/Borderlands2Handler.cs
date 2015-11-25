using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Diagnostics;
using WindowScrape.Types;
using System.Threading;
using System.Runtime.InteropServices;
using WindowScrape.Static;
using WindowScrape.Constants;
using Nucleus.Gaming.Interop;

namespace Games
{
    public class Borderlands2Handler : IGameHandler
    {
        protected string executablePlace;
        protected string saveFile;
        protected int delayTime;
        protected int titleHeight;
        private UserGameInfo userGame;

        public int TimerInterval
        {
            get { return 1000; }
        }

        public void End()
        {
        }

        private GameProfile profile;
        private bool end;

        public bool Initialize(UserGameInfo game, GameProfile profile)
        {
            this.executablePlace = game.ExePath;
            this.profile = profile;
            this.userGame = game;

            delayTime = (int)((double)profile.Options["delay"] * 1000);

            // Let's search for the save file
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string myGames = Path.Combine(documents, @"My Games\Borderlands 2\WillowGame\Config");
            string willowEngine = Path.Combine(myGames, "WillowEngine.ini");

            if (File.Exists(willowEngine))
            {
                saveFile = willowEngine;
            }
            else
            {
                MessageBox.Show("Could not find WillowEngine.ini file!");

                using (OpenFileDialog open = new OpenFileDialog())
                {
                    open.Filter = "WillowEngine.ini file|WillowEngine.ini";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        saveFile = open.FileName;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            // backup the WillowEngine ini
            GameManager.Instance.StartBackup(game.Game);
            GameManager.Instance.BackupFile(game.Game, willowEngine);

            return true;
        }

        public string Play()
        {
            if (!SteamUtil.IsSteamRunning())
            {
                MessageBox.Show("If you own the Steam Version, please open Steam, then click OK");
            }

            var options = profile.Options;
            bool playerKeyboard = (bool)profile.Options["keyboardPlayer"];

            IniFile file = new IniFile(saveFile);
            file.IniWriteValue("SystemSettings", "WindowedFullscreen", "False");
            file.IniWriteValue("SystemSettings", "Fullscreen", "False");
            file.IniWriteValue("Engine.Engine", "bMuteAudioWhenNotInFocus", "False");
            file.IniWriteValue("Engine.Engine", "bPauseOnLossOfFocus", "False");
            file.IniWriteValue("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", "False");
            file.IniWriteValue("WillowGame.WillowGameEngine", "bMuteAudioWhenNotInFocus", "False");

            Screen[] all = Screen.AllScreens;

            // minimize everything
            //User32.MinimizeEverything();
            List<PlayerInfo> players = profile.PlayerData;

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];
                // Set Borderlands 2 Resolution and stuff to run

                Rectangle playerBounds = player.monitorBounds;

                // find the monitor that has this screen
                Screen owner = null;
                for (int j = 0; j < all.Length; j++)
                {
                    Screen s = all[j];
                    if (s.Bounds.Contains(playerBounds))
                    {
                        owner = s;
                        break;
                    }
                }

                int width = playerBounds.Width;
                int height = playerBounds.Height;

                if (owner == null)
                {
                    // log
                    // screen doesn't exist
                    //continue;
                }
                else
                {
                    Rectangle ob = owner.Bounds;
                    if (playerBounds.X == ob.X &&
                        playerBounds.Y == ob.Y &&
                        playerBounds.Width == ob.Width &&
                        playerBounds.Height == ob.Height)
                    {
                        // borderlands 2 has a limitation for max-screen size, we can't go up to the monitor's bounds
                        // in windowed mode
                        file.IniWriteValue("SystemSettings", "WindowedFullscreen", "True");
                    }
                    else
                    {
                        file.IniWriteValue("SystemSettings", "WindowedFullscreen", "False");
                    }
                }

                file.IniWriteValue("SystemSettings", "ResX", width.ToString(CultureInfo.InvariantCulture));
                file.IniWriteValue("SystemSettings", "ResY", height.ToString(CultureInfo.InvariantCulture));

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = executablePlace;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                // NEW
                object option = options["saveid" + i];
                //object option = 11;
                int id = (int)option;

                if (playerKeyboard)
                {
                    startInfo.Arguments = "-AlwaysFocus -NoController -ControllerOffset=" + (i - 1).ToString(CultureInfo.InvariantCulture) + " -SaveDataId=" + id.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    startInfo.Arguments = "-AlwaysFocus -ControllerOffset=" + i.ToString(CultureInfo.InvariantCulture) + " -SaveDataId=" + id.ToString(CultureInfo.InvariantCulture);
                }

                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Path.GetDirectoryName(executablePlace);

                Process proc = Process.Start(startInfo);

                ScreenData data = new ScreenData();
                data.Position = new Point(playerBounds.X, playerBounds.Y);
                data.Size = new Size(playerBounds.Width, playerBounds.Height);
                player.Process = proc;
                player.Tag = data;
            }

            return string.Empty;
        }

        private int timer;
        public void Update(int delayMS)
        {
            if (profile == null)
            {
                return;
            }

            int exited = 0;
            List<PlayerInfo> players = profile.PlayerData;
            timer += delayMS;

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo p = players[i];
                if (p.Tag == null)
                {
                    continue;
                }
                if (p.Process.HasExited)
                {
                    exited++;
                    continue;
                }

                ScreenData data = (ScreenData)p.Tag;
                if (!data.Set)
                {
                    p.Process.Refresh();

                    if (data.HWND == null || data.HWND.Hwnd != p.Process.MainWindowHandle)
                    {
                        data.HWND = new HwndObject(p.Process.MainWindowHandle);
                        Point pos = data.HWND.Location;

                        if (String.IsNullOrEmpty(data.HWND.Title) || data.HWND.Title.ToLower() == "splashscreen" || pos.X == -32000)
                        {
                            data.HWND = null;
                        }
                        else
                        {
                            Thread.Sleep(delayTime);
                            Size s = data.Size;

                            data.Set = true;
                            data.HWND.Size = data.Size;
                            User32.HideBorder(p.Process.MainWindowHandle);
                            data.HWND.Location = data.Position;
                        }
                    }
                }

            }

            if (exited == players.Count)
            {
                if (!end)
                {
                    end = true;
                    GameManager.Instance.ExecuteBackup(this.userGame.Game);

                    if (Ended != null)
                    {
                        Ended();
                    }
                }
            }
        }
        
        public bool HasEnded
        {
            get { return end; }
        }

        public event Action Ended;
    }
}
