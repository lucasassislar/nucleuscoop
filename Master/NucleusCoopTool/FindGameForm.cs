using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SplitTool
{
    public partial class FindGameForm : Form
    {
        protected GameManager gameManager;
        public FindGameForm(GameManager manager)
        {
            this.gameManager = manager;
            InitializeComponent();
        }

        private void btn_Manually_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Game Executable|*.exe";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string path = open.FileName;
                    Bitmap icon;
                    string key = Path.GetDirectoryName(path);
                    if (!imageList1.Images.ContainsKey(key))
                    {
                        using (System.Drawing.Icon sysicon = Icon.ExtractAssociatedIcon(path))
                        {
                            using (MemoryStream str = new MemoryStream())
                            {
                                sysicon.Save(str);
                                str.Position = 0;
                                icon = (Bitmap)Image.FromStream(str);
                            }
                        }
                        this.imageList1.Images.Add(key, icon);
                    }

                    ListViewItem item = new ListViewItem(open.FileName, key);
                    this.list_Games.Items.Add(item);
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this.list_Games.CheckedItems != null &&
                this.list_Games.CheckedItems.Count > 0)
            {
                var selected = this.list_Games.CheckedItems;
                UserProfile user = gameManager.User;

                bool added = false;
                for (int i = 0; i < selected.Count; i++)
                {
                    var sel = selected[i];
                    string path = sel.Text;

                    // Search for that specific EXE on every of the users games
                    path = path.ToLower();
                    string fileName = Path.GetFileName(path);
                    bool repeats = false;
                    for (int j = 0; j < user.Games.Count; j++)
                    {
                        var game = user.Games[j];
                        if (game.ExecutablePath == path)
                        {
                            repeats = true;
                            MessageBox.Show("The game '" + fileName + "' is already on your library");
                            break;
                        }
                    }

                    // Now look if that game is split-screen supported
                    bool foundInfo = false;
                    string gameName = null;
                    string guid = null;
                    foreach (var gameInfo in gameManager.Games.Values)
                    {
                        string[] exec = gameInfo.ExecutableName.Split('|');

                        bool found = false;
                        for (int j = 0; j < exec.Length; j++)
                        {
                            string name = exec[j];
                            if (name == fileName)
                            {
                                gameName = gameInfo.GameName;
                                foundInfo = true;
                                guid = gameInfo.GUID;
                                found = true;
                                break;
                            }
                        }

                        if (found)
                        {
                            break;
                        }
                    }

                    if (foundInfo)
                    {
                        if (!repeats)
                        {
                            // add to library
                            UserGameInfo info = new UserGameInfo();
                            info.ExecutablePath = path;
                            info.GameName = gameName;
                            info.GameGuid = guid;
                            user.Games.Add(info);
                            added = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The game '" + fileName + "' is not support for SplitScreen");
                    }
                }

                if (added)
                {
                    gameManager.UpdateUserProfile();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
