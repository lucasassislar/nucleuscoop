/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;
using Gibbed.IO;
using System.IO;
using System;
using System.Windows.Forms;
using Caliburn.Micro.Contrib.Results;
using X360;
using X360.IO;
using X360.Other;
using X360.Profile;
using X360.STFS;
using System.Xml;
namespace Gibbed.Borderlands2.SaveEdit
{
    public partial class GeneralView
    {
        public GeneralView()
        {
            this.InitializeComponent();
            loadprofiles();
            this.myprofiles.SelectionChanged += myprofiles_SelectedIndexChanged;
        }
        public void loadprofiles()
        {
            try
            {
                string name = "";

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlTextReader reader = new XmlTextReader(path + "/profiles.xml");
                //doc.Load("C:/XKey Desktop/data.xml");
                // Read until end of file


                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "profile")
                    {
                        while (reader.NodeType != XmlNodeType.EndElement)
                        {
                            reader.Read();

                            if (reader.Name == "name")
                            {
                                while (reader.NodeType != XmlNodeType.EndElement)
                                {
                                    reader.Read();
                                    if (reader.NodeType == XmlNodeType.Text)
                                    {
                                        name = reader.Value;
                                        myprofiles.Items.Add(reader.Value);
                                        //MessageBox.Show(reader.Value);
                                    }
                                }
                                reader.Read();
                            }
                            //end if

                        }

                        //this.myprofiles = new ObservableCollection<AssetDisplay>(name);

                        //comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["ZoneName"].ToString();
                        //comboBoxName.SelectedValuePath = ds.Tables[0].Columns["ZoneId"].ToString(); 
                    }

                }
            }
            catch { }
        }
        private void myprofiles_SelectedIndexChanged(object sender,
        System.EventArgs e)
        {
            try
            {


                // Save the selected employee's name, because we will remove 
                // the employee's name from the list. 
                string selectedprofile = (string)myprofiles.SelectedValue;
                string name = "";

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlTextReader reader = new XmlTextReader(path + "/profiles.xml");
                //doc.Load("C:/XKey Desktop/data.xml");
                // Read until end of file


                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "profile")
                    {
                        while (reader.NodeType != XmlNodeType.EndElement)
                        {
                            reader.Read();

                            if (reader.Name == "name")
                            {
                                while (reader.NodeType != XmlNodeType.EndElement)
                                {
                                    reader.Read();
                                    if (reader.NodeType == XmlNodeType.Text)
                                    {
                                        name = reader.Value;
                                        //myprofiles.Items.Add(reader.Value);
                                        //MessageBox.Show(reader.Value);
                                    }
                                }
                                reader.Read();
                            }
                            if (name == selectedprofile)
                            {
                                if (reader.Name == "id")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            myprofileid.Text = reader.Value;
                                            myprofilename.Text = selectedprofile;
                                            //MessageBox.Show(reader.Value);
                                        }
                                    }
                                    reader.Read();
                                }
                            }
                            //end if

                        }

                        //this.myprofiles = new ObservableCollection<AssetDisplay>(name);

                        //comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["ZoneName"].ToString();
                        //comboBoxName.SelectedValuePath = ds.Tables[0].Columns["ZoneId"].ToString(); 
                    }

                }
                //MessageBox.Show(selectedEmployee);
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
    }

}
