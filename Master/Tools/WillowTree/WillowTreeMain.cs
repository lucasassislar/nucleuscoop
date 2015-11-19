using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using X360.IO;
using X360.STFS;
//Yeah, I don't need most of these. So sue me.
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using Microsoft.VisualBasic;
using System.Net;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Diagnostics;
//using System.Collections.Specialized;


namespace WillowTree
{
    public partial class WillowTreeMain : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public bool Clicked = false; //Goes with the quest stuff. Really...ineffective.
        public static bool UseTitlePrefix = false;
        string VersionFromServer;
        string DownloadURLFromServer;

        //private Ini.IniFile TitlesIni = new Ini.IniFile("");
        private XmlFile TitlesXml = new XmlFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data\\Titles.ini");

        //Grabs the name of a weapon/item from an INI.
        /*public string GetName(Ini.IniFile INI, string[,] PartArray, int DesiredPart, int NumberOfSubParts, string INIValueToRetrieve)
        {
            string Name = "";
            for (int build = 0; build < NumberOfSubParts; build++)
            {

                string readValue = INI.IniReadValue(PartArray[DesiredPart, build], INIValueToRetrieve);



                if (Name == "" && readValue != null)
                    Name = readValue;
                else if (readValue != null && readValue != "")
                    Name = (Name + " " + readValue);
            }

            return Name;
        }*/
        // 0 references - Build composite string from xmlvalues in string array.  Looks up xmlvalue of every part not just 12+13 or 7+8.  Don't need to specify item type. output is name only, no prefix
        public string GetName(XmlFile xml, string[,] PartArray, int DesiredPart, int NumberOfSubParts, string INIValueToRetrieve)
        {
            string Name = "";
            for (int build = 0; build < NumberOfSubParts; build++)
            {

                string readValue = xml.XmlReadValue(PartArray[DesiredPart, build], INIValueToRetrieve);

                if (Name == "" && readValue != null)
                    Name = readValue;
                else if (readValue != null && readValue != "")
                    Name = (Name + " " + readValue);
            }

            return Name;
        }
        /*public string GetName(Ini.IniFile INI, string[] PartArray, int DesiredPart, string INIValueToRetrieve)
        {
            string Name = "";

            Name = (INI.IniReadValue(PartArray[DesiredPart], INIValueToRetrieve));

            return Name;
        }*/
        // 29 references - Fetch single xml value from name in part array
        public string GetName(XmlFile xml, string[] PartArray, int DesiredPart, string INIValueToRetrieve)
        {
            string Name = "";

            Name = (xml.XmlReadValue(PartArray[DesiredPart], INIValueToRetrieve));

            return Name;
        }
        /*public string GetName(Ini.IniFile INI, List<List<string>> PartArray, int DesiredPart, int NumberOfSubParts, string INIValueToRetrieve, string Itemtype)
        {
            string Name = "";
            string itemtypeprefix = ""; 
            for (int build = 0; build < NumberOfSubParts; build++)
            {
                
                // Get the Weapontype from part0 (Itemgrade)
                //gd_itemgrades.Weapons_Eridan.ItemGrade_Eridan_SMG_Blaster
                if ((Itemtype == "Item") && (build==1))
                {
                    //Type in Part2 -> strParts[2]
                    itemtypeprefix = PartArray[DesiredPart][build].Substring(PartArray[DesiredPart][build].LastIndexOf(".") + 1);
                    itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1) + " ";
                }

                if ((Itemtype == "Weapon") && (build==0))
                {
                    //Type in Part1 -> strParts[1]

                    itemtypeprefix = PartArray[DesiredPart][build].Substring(PartArray[DesiredPart][build].LastIndexOf(".") + 1);
                    itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1) + " ";
                    if (itemtypeprefix.StartsWith("Weapon_"))
                        itemtypeprefix = itemtypeprefix.Substring(7);

                }

                // Names in Weapons only in Build 12,13 Items only in 7,8
                if (((Itemtype == "Weapon") && (build == 12 || build == 13)) || ((Itemtype == "Item") && (build == 7 || build == 8)))
                {
                    string iniReadvalue = INI.IniReadValue(PartArray[DesiredPart][build], INIValueToRetrieve);

                    if (Name == "" && iniReadvalue != "")
                        Name = iniReadvalue;
                    else if (iniReadvalue != null && iniReadvalue != "")
                        Name = (Name + " " + iniReadvalue);
                }
            }

            return itemtypeprefix + Name;
        }*/
        // 15 references - read name from xml of 12+13 weapon or 7+8 item.  no prefix. - output is name only 
        public string GetName(XmlFile xml, List<List<string>> PartArray, int DesiredPart, int NumberOfSubParts, string INIValueToRetrieve, string Itemtype)
        {
            string Name = "";
            string itemtypeprefix = "";
            for (int build = 0; build < NumberOfSubParts; build++)
            {

                // Get the Weapontype from part0 (Itemgrade)
                //gd_itemgrades.Weapons_Eridan.ItemGrade_Eridan_SMG_Blaster
                if ((Itemtype == "Item") && (build == 1))
                {
                    //Type in Part2 -> strParts[2]
                    // itemtypeprefix = PartArray[DesiredPart][build].Substring(PartArray[DesiredPart][build].LastIndexOf(".") + 1);
                    // itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1) + " ";
                }

                if ((Itemtype == "Weapon") && (build == 0))
                {
                    //Type in Part1 -> strParts[1]

                    // itemtypeprefix = PartArray[DesiredPart][build].Substring(PartArray[DesiredPart][build].LastIndexOf(".") + 1);
                    // itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1) + " ";
                    // if (itemtypeprefix.StartsWith("Weapon_"))
                    //    itemtypeprefix = itemtypeprefix.Substring(7);

                }

                // Names in Weapons only in Build 12,13 Items only in 7,8
                if (((Itemtype == "Weapon") && (build == 12 || build == 13)) || ((Itemtype == "Item") && (build == 7 || build == 8)))
                {
                    string iniReadvalue = xml.XmlReadValue(PartArray[DesiredPart][build], INIValueToRetrieve);

                    if (Name == "" && iniReadvalue != "")
                        Name = iniReadvalue;
                    else if (iniReadvalue != null && iniReadvalue != "")
                        Name = (Name + " " + iniReadvalue);
                }
            }

            return itemtypeprefix + Name;
        }
        // 3 references - read name from xml of 12+13 weapon or 7+8 item, analyze 1 weapon or 2 item for class - output is class + name
        public string GetName(XmlFile xml, List<string> PartArray, int NumberOfSubParts, string INIValueToRetrieve, string Itemtype)
        {
            string Name = "";
            string itemtypeprefix = "";
            for (int build = 0; build < NumberOfSubParts; build++)
            {

                // Get the Weapontype from part0 (Itemgrade)
                //gd_itemgrades.Weapons_Eridan.ItemGrade_Eridan_SMG_Blaster
                if ((Itemtype == "Item") && (build == 1))
                {
                    //Type in Part2 -> strParts[2]
                    itemtypeprefix = PartArray[build].Substring(PartArray[build].LastIndexOf(".") + 1);
                    itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1) + " ";
                }

                if ((Itemtype == "Weapon") && (build == 0))
                {
                    //Type in Part1 -> strParts[1]

                    itemtypeprefix = PartArray[build].Substring(PartArray[build].LastIndexOf(".") + 1);
                    itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1) + " ";
                    if (itemtypeprefix.StartsWith("Weapon_"))
                        itemtypeprefix = itemtypeprefix.Substring(7);
                }

                // Names in Weapons only in Build 12,13 Items only in 7,8
                if (((Itemtype == "Weapon") && (build == 12 || build == 13)) || ((Itemtype == "Item") && (build == 7 || build == 8)))
                {
                    string iniReadvalue = xml.XmlReadValue(PartArray[build], INIValueToRetrieve);

                    if (Name == "" && iniReadvalue != "")
                        Name = iniReadvalue;
                    else if (iniReadvalue != null && iniReadvalue != "")
                        Name = (Name + " " + iniReadvalue);
                }
            }

            return itemtypeprefix + Name;
        }

        public string GetLongName(XmlFile xml, string CategoryPart, string PrefixPart, string NamePart)
        {
            string category = xml.XmlReadValue(CategoryPart, "Prefix");
            string prefix = xml.XmlReadValue(PrefixPart, "PartName");
            string name = xml.XmlReadValue(NamePart, "PartName");

            if (prefix != "")
                name = prefix + " " + name;
            if (category != "")
                name = category + " " + name;
            return name;
        }

        string OpenedLocker;

        //Generate each advTree
        public void DoQuestTree()
        {
            QuestTree.Nodes.Clear();
            //Ini.IniFile Quests = new Ini.IniFile(AppDir + "\\Data\\Quests.ini");
            XmlFile Quests = new XmlFile(AppDir + "\\Data\\Quests.ini");
            Node PT1 = new Node();
            PT1.Text = "Playthrough 1 Quests";
            PT1.Name = "PT1";
            Node PT2 = new Node();
            PT2.Text = "Playthrough 2 Quests";
            PT2.Name = "PT2";
            QuestTree.Nodes.Add(PT1);
            QuestTree.Nodes.Add(PT2);
            for (int build = 0; build < CurrentWSG.TotalPT1Quests; build++)
            {
                Node TempNode = new Node();
                TempNode.Name = "PT1";
                TempNode.Text = GetName(Quests, CurrentWSG.PT1Strings, build, "MissionName");
                if (TempNode.Text == null || TempNode.Text == "") TempNode.Text = "Unknown Quest";
                PT1.Nodes.Add(TempNode);


                //for(int sub_build = 0; sub_build < (CurrentWSG.PT1Values[build,3] + 4); sub_build++)
                //{
                //    Node Sub = new Node();
                //    Sub.Text = "Value: " + CurrentWSG.PT1Values[build, sub_build];
                //    TempNode.Nodes.Add(Sub);
                //     + CurrentWSG.TotalPT1Quests
                //}

            }

            for (int build2 = 0; build2 < CurrentWSG.TotalPT2Quests; build2++)
            {
                Node TempNode = new Node();
                TempNode.Name = "PT2";
                TempNode.Text = GetName(Quests, CurrentWSG.PT2Strings, build2, "MissionName");
                if (TempNode.Text == null || TempNode.Text == "") TempNode.Text = "Unknown Quest";

                PT2.Nodes.Add(TempNode);


                //for(int sub_build = 0; sub_build < (CurrentWSG.PT1Values[build,3] + 4); sub_build++)
                //{
                //    Node Sub = new Node();
                //    Sub.Text = "Value: " + CurrentWSG.PT1Values[build, sub_build];
                //    TempNode.Nodes.Add(Sub);
                //    
                //}

            }

        }
        public void DoLocationTree()
        {
            //Ini.IniFile PartList = new Ini.IniFile(AppDir + "\\Data\\Locations.ini");
            XmlFile PartList = new XmlFile(AppDir + "\\Data\\Locations.ini");
            //Ini.IniFile PartList = new Ini.IniFile(AppDir + "\\Data\\Locations.ini");
            LocationTree.Nodes.Clear();

            for (int build = 0; build < CurrentWSG.TotalLocations; build++)
            {
                //string name = PartList.IniReadValue(CurrentWSG.LocationStrings[build], "OutpostDisplayName");
                string name = PartList.XmlReadValue(CurrentWSG.LocationStrings[build], "OutpostDisplayName");
                Node TempNode = new Node();
                if (name != "")
                    TempNode.Text = name;
                else TempNode.Text = CurrentWSG.LocationStrings[build];


                LocationTree.Nodes.Add(TempNode);

            }





        }
        public void DoSkillTree()
        {
            List<string> filestoconvert = new List<string>();
            filestoconvert.Add(AppDir + "\\Data\\gd_skills_common.txt");
            filestoconvert.Add(AppDir + "\\Data\\gd_Skills2_Roland.txt");
            filestoconvert.Add(AppDir + "\\Data\\gd_Skills2_Lilith.txt");
            filestoconvert.Add(AppDir + "\\Data\\gd_skills2_Mordecai.txt");
            filestoconvert.Add(AppDir + "\\Data\\gd_Skills2_Brick.txt");

            XmlFile AllSkills = new XmlFile(filestoconvert, AppDir + "\\Data\\xml\\gd_skills.xml");

            /*
                        XmlFile Common = new XmlFile();
                        Common.XmlFilename(AppDir + "\\Data\\gd_skills_common.txt");
                        XmlFile Roland = new XmlFile();
                        Roland.XmlFilename(AppDir + "\\Data\\gd_Skills2_Roland.txt");
                        XmlFile Lilith = new XmlFile();
                        Lilith.XmlFilename(AppDir + "\\Data\\gd_Skills2_Lilith.txt");
                        XmlFile Mordecai = new XmlFile();
                        Mordecai.XmlFilename(AppDir + "\\Data\\gd_skills2_Mordecai.txt");
                        XmlFile Brick = new XmlFile();
                        Brick.XmlFilename(AppDir + "\\Data\\gd_Skills2_Brick.txt");
            */
            //Ini.IniFile Common = new Ini.IniFile(AppDir + "\\Data\\gd_skills_common.txt");
            //Ini.IniFile Roland = new Ini.IniFile(AppDir + "\\Data\\gd_Skills2_Roland.txt");
            //Ini.IniFile Lilith = new Ini.IniFile(AppDir + "\\Data\\gd_Skills2_Lilith.txt");
            //Ini.IniFile Mordecai = new Ini.IniFile(AppDir + "\\Data\\gd_skills2_Mordecai.txt");
            //Ini.IniFile Brick = new Ini.IniFile(AppDir + "\\Data\\gd_Skills2_Brick.txt");
            SkillTree.Nodes.Clear();
            SkillLevel.Value = 0;
            SkillExp.Value = 0;
            SkillActive.SelectedItem = "No";
            for (int build = 0; build < CurrentWSG.NumberOfSkills; build++)
            {
                Node TempNode = new Node();
                if (AllSkills.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName") != "")
                    TempNode.Text = AllSkills.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName");
                else
                    TempNode.Text = CurrentWSG.SkillNames[build];


                /*                if (Common.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName") != "")
                                    TempNode.Text = Common.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName");
                                else if (Roland.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName") != "")
                                    TempNode.Text = Roland.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName");
                                else if (Lilith.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName") != "")
                                    TempNode.Text = Lilith.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName");
                                else if (Mordecai.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName") != "")
                                    TempNode.Text = Mordecai.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName");
                                else if (Brick.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName") != "")
                                    TempNode.Text = Brick.XmlReadValue(CurrentWSG.SkillNames[build], "SkillName");
                                else
                                    TempNode.Text = CurrentWSG.SkillNames[build];
                */
                SkillTree.Nodes.Add(TempNode);


            }



        }
        public void DoEchoTree()
        {
            EchoTree.Nodes.Clear();
            //Ini.IniFile Echos = new Ini.IniFile(AppDir + "\\Data\\Echos.ini");
            XmlFile Echos = new XmlFile(AppDir + "\\Data\\Echos.ini");
            Node PT1 = new Node();
            PT1.Text = "Playthrough 1 Echo Logs";
            Node PT2 = new Node();
            PT2.Text = "Playthrough 2 Echo Logs";
            EchoTree.Nodes.Add(PT1);
            EchoTree.Nodes.Add(PT2);
            for (int build = 0; build < CurrentWSG.NumberOfEchos; build++)
            {
                Node TempNode = new Node();
                TempNode.Name = "PT1";
                TempNode.Text = GetName(Echos, CurrentWSG.EchoStrings, build, "Subject");
                if (TempNode.Text == null || TempNode.Text == "") TempNode.Text = "Unknown Echo";
                PT1.Nodes.Add(TempNode);
            }
            if (CurrentWSG.NumberOfEchosPT2 > 0 && CurrentWSG.TotalPT2Quests > 1 && CurrentWSG.NumberOfEchosPT2 < 300)
                for (int build2 = 0; build2 < CurrentWSG.NumberOfEchosPT2; build2++)
                {
                    Node TempNode = new Node();
                    TempNode.Name = "PT2";
                    TempNode.Text = GetName(Echos, CurrentWSG.EchoStringsPT2, build2, "Subject");
                    if (TempNode.Text == null || TempNode.Text == "") TempNode.Text = "Unknown Echo";

                    PT2.Nodes.Add(TempNode);
                }
        }
        public void DoAmmoTree()
        {
            AmmoTree.Nodes.Clear();

            for (int build = 0; build < CurrentWSG.NumberOfPools; build++)
            {
                Node TempNode = new Node();

                TempNode.Text = GetAmmoName(CurrentWSG.ResourcePools[build]);

                AmmoTree.Nodes.Add(TempNode);

            }



        }
        public void DoWeaponTree()
        {
            WeaponTree.Nodes.Clear();
            //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
            //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini");

            for (int build = 0; build < CurrentWSG.WeaponStrings.Count; build++)
            {
                Node TempNode = new Node();
                TempNode.Text = GetName(TitlesXml, CurrentWSG.WeaponStrings, build, 14, "PartName", "Weapon");
                //TempNode.Text = GetName(CurrentWSG.WeaponStrings, build, 14, "PartName");
                //TempNode.Name = "" + build;
                //for (int search_name = 0; search_name < 14; search_name++)
                //{

                //Node Sub = new Node();
                //Sub.Text = "Part " + (search_name + 1) + ": " + CurrentWSG.WeaponStrings[build][search_name];
                //TempNode.Nodes.Add(Sub);

                //if(TempNode.Text == "" && Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName") != null)
                //TempNode.Text = (Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName"));
                //else if(Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName") != null) 
                //TempNode.Text = (TempNode.Text + " " + Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName"));
                //}
                if (TempNode.Text == "") TempNode.Text = "Unknown Weapon";
                WeaponTree.Nodes.Add(TempNode);
            }

        }
        public void DoItemTree()
        {
            ItemTree.Nodes.Clear();
            //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
            //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini");

            for (int build = 0; build < CurrentWSG.NumberOfItems; build++)
            {
                Node TempNode = new Node();
                TempNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, build, 9, "PartName", "Item");
                //TempNode.Name = "" + build;
                //for (int search_name = 0; search_name < 9; search_name++)
                //{

                //Node Sub = new Node();
                //Sub.Text = "Part " + (search_name + 1) + ": " + CurrentWSG.ItemStrings[build][search_name];
                //TempNode.Nodes.Add(Sub);

                // if (TempNode.Text == "" && Titles.IniReadValue(CurrentWSG.ItemStrings[build][search_name], "PartName") != null)
                //     TempNode.Text = (Titles.IniReadValue(CurrentWSG.ItemStrings[build][search_name], "PartName"));
                // else if (Titles.IniReadValue(CurrentWSG.ItemStrings[build][search_name], "PartName") != null)
                //     TempNode.Text = (TempNode.Text + " " + Titles.IniReadValue(CurrentWSG.ItemStrings[build][search_name], "PartName"));

                // }
                if (TempNode.Text == "") TempNode.Text = "Unknown Item";
                //Node Sub_Ammo = new Node();
                //Node Sub_Quality = new Node();
                //Node Sub_Slot = new Node();
                //Sub_Ammo.Text = "Quantity: " + CurrentWSG.ItemValues[build][0];
                //Sub_Quality.Text = "Quality Level: " + CurrentWSG.ItemValues[build][1];
                //Sub_Slot.Text = "Equipped: " + CurrentWSG.ItemValues[build][2];
                //TempNode.Nodes.Add(Sub_Ammo);
                //TempNode.Nodes.Add(Sub_Quality);
                //TempNode.Nodes.Add(Sub_Slot);
                ItemTree.Nodes.Add(TempNode);
            }

        }
        public void DoLockerTree(string InputFile)
        {
            LockerTree.Nodes.Clear();
            //XmlFile Locker = new XmlFile(InputFile);
            //Ini.IniFile Locker = new Ini.IniFile(InputFile);
            //XmlLocker.XmlFilename(InputFile);
            OpenedLocker = InputFile;
            XmlLocker.path = InputFile;

            // Assumed that the List contains no dupes

            foreach (string iniListSectionName in XmlLocker.stListSectionNames())
            {
                /*
                bool bFound = false;
                int ndcnt = 0;
                for (ndcnt = 0; ndcnt < LockerTree.Nodes.Count; ndcnt++)
                {
                    if (LockerTree.Nodes[ndcnt].Text == iniListSectionName.Remove(iniListSectionName.IndexOf(" ")))
                    {
                        bFound = true;
                        break;
                    }
                }
                if (bFound)
                {
                    Node tempchild = new Node();
                    tempchild.Text = iniListSectionName.Substring(iniListSectionName.IndexOf(" ") + 1);
                    tempchild.Name = iniListSectionName;
                    LockerTree.Nodes[ndcnt].Nodes.Add(tempchild);
                }
                else
                {
                    Node temp = new Node();
                    temp.Text = iniListSectionName.Remove(iniListSectionName.IndexOf(" "));
                    LockerTree.Nodes.Add(temp);
                    Node tempchild = new Node();
                    tempchild.Text = iniListSectionName.Substring(iniListSectionName.IndexOf(" ")+1);
                    tempchild.Name = iniListSectionName;
                    temp.Nodes.Add(tempchild);
                }
                */



                Node temp = new Node();
                temp.Text = iniListSectionName;
                temp.Name = iniListSectionName;
                LockerTree.Nodes.Add(temp);

            }


            /*
            List<string> iniListSectionNames = new List<string>();

            foreach (string item in XmlLocker.stListSectionNames())
            {
                int Count = 0;
                // Check if element is there
                if (iniListSectionNames.Contains(item))
                {
                    // rename and try again until in can be added
                    do
                    {
                        Count++;
                    } while (iniListSectionNames.Contains(item + " (Copy " + Count + ")"));
                    iniListSectionNames.Add(item + " (Copy " + Count + ")");
                }
                else
                    iniListSectionNames.Add(item);
            
            }
 
            iniListSectionNames.TrimExcess();
            bool bFound;
            int Listsectioncounter = 0;

            //iterate over the now dupefree and renamed list and add the nodes to the lockertree
            foreach (string iniListSectionName in iniListSectionNames)
            {
                
                bFound = false;
                int ndcnt = 0;
                for (ndcnt = 0; ndcnt < LockerTree.Nodes.Count; ndcnt++)
                {
                    if (LockerTree.Nodes[ndcnt].Text == iniListSectionName.Remove(iniListSectionName.IndexOf(" ")))
                    {
                        bFound = true;
                        break;
                    }
                }
                if (bFound)
                {
                    Node tempchild = new Node();
                    tempchild.Text = iniListSectionName.Substring(iniListSectionName.IndexOf(" ") + 1);
                    tempchild.TagString = Listsectioncounter.ToString();
                    LockerTree.Nodes[ndcnt].Nodes.Add(tempchild);
                }
                else
                {
                    Node temp = new Node();
                    temp.Text = iniListSectionName.Remove(iniListSectionName.IndexOf(" "));
                    LockerTree.Nodes.Add(temp);
                    Node tempchild = new Node();
                    tempchild.Text = iniListSectionName.Substring(iniListSectionName.IndexOf(" ")+1);
                    tempchild.TagString = Listsectioncounter.ToString();
                    temp.Nodes.Add(tempchild);
                }
                Listsectioncounter++;
                



                Node temp = new Node();
                temp.Text = iniListSectionName;
                LockerTree.Nodes.Add(temp);
            }
            */


        }
        // Used to convert wtl -> xml locker
        public void DoLockerTreetry(string InputFile)
        {
            LockerTreetry2.Nodes.Clear();
            //Ini.IniFile Locker = new Ini.IniFile(InputFile);
            //XmlFile Locker = new XmlFile(InputFile);

            //OpenedLocker = InputFile;

            // rootnode
            Node ndroot = new Node();
            ndroot.Text = "INI";
            ndroot.Expand();
            LockerTreetry2.Nodes.Add(ndroot);

            Node[] ndparts = new Node[19];
            string[] strParts = new string[19];
            int[] ndcntparts = new int[19];


            bool bFound;

            //Build tree

            // read file (default.wtl) to stringarray
            string[] strRead = new string[19];
            string line;
            int count = 0;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(InputFile);
            while ((line = file.ReadLine()) != null)
            {
                // Section
                if (line.Length > 0) // no empty lines
                {
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        strRead[15] = line.Substring(1, line.Length - 2);
                        if (strRead[15].Contains(" (Copy"))
                            strRead[15] = strRead[15].Remove(strRead[15].IndexOf(" (Copy"));

                        count = 0;
                    }
                    if (line.Contains("Type="))
                    {
                        strRead[0] = line.Substring(line.IndexOf("=") + 1);
                        count++;
                    }
                    if (line.Contains("Rating="))
                    {
                        strRead[16] = line.Substring(line.IndexOf("=") + 1);
                        count++;
                    }
                    if (line.Contains("Description="))
                    {
                        strRead[17] = line.Substring(line.IndexOf("=") + 1);
                        count++;
                    }
                    for (int partindex = 1; partindex < 15; partindex++)
                    {
                        if (line.Contains("Part" + partindex + "="))
                        {
                            strRead[partindex] = line.Substring(line.IndexOf("=") + 1);
                            count++;
                        }
                    }

                    if (count == 17)
                    {
                        count = 0;
                        for (int partindex = 0; partindex < 1; partindex++)
                        {
                            // All sections
                            // read the ini values
                            bFound = false;
                            ndparts[partindex] = null;
                            strParts[partindex] = null;

                            //read type
                            strParts[partindex] = strRead[partindex];

                            for (int ndcnt = 0; ndcnt < ndroot.Nodes.Count; ndcnt++)
                            {
                                if (ndroot.Nodes[ndcnt].Text == strParts[partindex])
                                {
                                    bFound = true;
                                    ndparts[partindex] = ndroot.Nodes[ndcnt];
                                    break;
                                }
                            }
                            if (!bFound)
                            {
                                ndparts[partindex] = new Node();
                                ndparts[partindex].Text = strParts[partindex];
                                ndparts[partindex].Expand();
                                ndroot.Nodes.Add(ndparts[partindex]);
                            }

                        }

                        for (int partindex = 1; partindex < 18; partindex++)
                        {
                            // All sections
                            // read the ini values
                            bFound = false;
                            ndparts[partindex] = null;
                            strParts[partindex] = null;

                            //read
                            strParts[partindex] = strRead[partindex];

                            for (int ndcnt = 0; ndcnt < ndparts[partindex - 1].Nodes.Count; ndcnt++)
                            {
                                if (ndparts[partindex - 1].Nodes[ndcnt].Text == strParts[partindex])
                                {
                                    bFound = true;
                                    ndparts[partindex] = ndparts[partindex - 1].Nodes[ndcnt];
                                    break;
                                }
                            }
                            if (!bFound)
                            {
                                ndparts[partindex] = new Node();
                                ndparts[partindex].Text = strParts[partindex];
                                ndparts[partindex].Expand();
                                ndparts[partindex - 1].Nodes.Add(ndparts[partindex]);
                            }
                        }
                    }
                }
            }
            file.Close();

            List<string> listNames = new List<string>();

            XmlTextWriter writer = new XmlTextWriter(InputFile.Replace(".wtl", ".xml"), new System.Text.ASCIIEncoding());
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("Comment");
            writer.WriteStartElement("INI");

            for (int ndcntrt = 0; ndcntrt < ndroot.Nodes.Count; ndcntrt++)
            {
                Node ndtype = ndroot.Nodes[ndcntrt];
                for (int ndcnttype = 0; ndcnttype < ndtype.Nodes.Count; ndcnttype++)
                {
                    Node ndpart1 = ndtype.Nodes[ndcnttype];

                    for (int ndcntpart1 = 0; ndcntpart1 < ndpart1.Nodes.Count; ndcntpart1++)
                    {
                        Node ndpart2 = ndpart1.Nodes[ndcntpart1];

                        for (int ndcntpart2 = 0; ndcntpart2 < ndpart2.Nodes.Count; ndcntpart2++)
                        {
                            Node ndpart3 = ndpart2.Nodes[ndcntpart2];
                            for (int ndcntpart3 = 0; ndcntpart3 < ndpart3.Nodes.Count; ndcntpart3++)
                            {
                                Node ndpart4 = ndpart3.Nodes[ndcntpart3];

                                for (int ndcntpart4 = 0; ndcntpart4 < ndpart4.Nodes.Count; ndcntpart4++)
                                {
                                    Node ndpart5 = ndpart4.Nodes[ndcntpart4];

                                    for (int ndcntpart5 = 0; ndcntpart5 < ndpart5.Nodes.Count; ndcntpart5++)
                                    {
                                        Node ndpart6 = ndpart5.Nodes[ndcntpart5];

                                        for (int ndcntpart6 = 0; ndcntpart6 < ndpart6.Nodes.Count; ndcntpart6++)
                                        {
                                            Node ndpart7 = ndpart6.Nodes[ndcntpart6];

                                            for (int ndcntpart7 = 0; ndcntpart7 < ndpart7.Nodes.Count; ndcntpart7++)
                                            {
                                                Node ndpart8 = ndpart7.Nodes[ndcntpart7];

                                                for (int ndcntpart8 = 0; ndcntpart8 < ndpart8.Nodes.Count; ndcntpart8++)
                                                {
                                                    Node ndpart9 = ndpart8.Nodes[ndcntpart8];

                                                    for (int ndcntpart9 = 0; ndcntpart9 < ndpart9.Nodes.Count; ndcntpart9++)
                                                    {
                                                        Node ndpart10 = ndpart9.Nodes[ndcntpart9];

                                                        for (int ndcntpart10 = 0; ndcntpart10 < ndpart10.Nodes.Count; ndcntpart10++)
                                                        {
                                                            Node ndpart11 = ndpart10.Nodes[ndcntpart10];

                                                            for (int ndcntpart11 = 0; ndcntpart11 < ndpart11.Nodes.Count; ndcntpart11++)
                                                            {
                                                                Node ndpart12 = ndpart11.Nodes[ndcntpart11];

                                                                for (int ndcntpart12 = 0; ndcntpart12 < ndpart12.Nodes.Count; ndcntpart12++)
                                                                {
                                                                    Node ndpart13 = ndpart12.Nodes[ndcntpart12];

                                                                    for (int ndcntpart13 = 0; ndcntpart13 < ndpart13.Nodes.Count; ndcntpart13++)
                                                                    {
                                                                        Node ndpart14 = ndpart13.Nodes[ndcntpart13];
                                                                        Node ndpart15 = ndpart14.Nodes[0];
                                                                        Node ndpart16 = ndpart15.Nodes[0];
                                                                        Node ndpart17 = ndpart16.Nodes[0];

                                                                        string NewName = "";

                                                                        if (listNames.Contains(ndpart15.Text))
                                                                        {
                                                                            int nCount = 1;
                                                                            while (listNames.Contains(ndpart15.Text + " (Copy " + nCount + ")"))
                                                                                nCount++;

                                                                            NewName = ndpart15.Text + " (Copy " + nCount + ")";
                                                                        }
                                                                        else
                                                                        {
                                                                            NewName = ndpart15.Text;
                                                                        }
                                                                        listNames.Add(NewName);

                                                                        //writer.WriteStartAttribute("prefix", "attrName");
                                                                        //writer.WriteEndAttribute();
                                                                        writer.WriteStartElement("Section");
                                                                        //writer.WriteAttributeString("Name", NewName);
                                                                        writer.WriteElementString("Name", NewName);
                                                                        writer.WriteElementString("Type", ndtype.Text);
                                                                        writer.WriteElementString("Rating", ndpart16.Text);
                                                                        writer.WriteElementString("Description", ndpart17.Text);

                                                                        writer.WriteElementString("Part1", ndpart1.Text);
                                                                        writer.WriteElementString("Part2", ndpart2.Text);
                                                                        writer.WriteElementString("Part3", ndpart3.Text);
                                                                        writer.WriteElementString("Part4", ndpart4.Text);
                                                                        writer.WriteElementString("Part5", ndpart5.Text);
                                                                        writer.WriteElementString("Part6", ndpart6.Text);
                                                                        writer.WriteElementString("Part7", ndpart7.Text);
                                                                        writer.WriteElementString("Part8", ndpart8.Text);
                                                                        writer.WriteElementString("Part9", ndpart9.Text);
                                                                        writer.WriteElementString("Part10", ndpart10.Text);
                                                                        writer.WriteElementString("Part11", ndpart11.Text);
                                                                        writer.WriteElementString("Part12", ndpart12.Text);
                                                                        writer.WriteElementString("Part13", ndpart13.Text);
                                                                        writer.WriteElementString("Part14", ndpart14.Text);
                                                                        // Hardcoded values, the wtl has no values for that
                                                                        writer.WriteElementString("RemAmmo_Quantity", "0");
                                                                        writer.WriteElementString("Quality", "5");
                                                                        writer.WriteElementString("Level", "63");
                                                                        writer.WriteEndElement();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //System.IO.File.WriteAllText(AppDir + "\\Data\\default2.wtl", tempINI);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();


        }
        // Clear out weapon/itemdupes by splitting them to their parts and add all to a tree
        // Clear out namedupes too (renaming to (Copy x)
        public void DoLockerTreeNodupe(string InputFile)
        {
            LockerTreetry2.Nodes.Clear();
            //Ini.IniFile Locker = new Ini.IniFile(InputFile);
            //XmlFile Locker = new XmlFile(InputFile);

            //OpenedLocker = InputFile;

            // rootnode
            Node ndroot = new Node();
            ndroot.Text = "INI";
            ndroot.Expand();
            LockerTreetry2.Nodes.Add(ndroot);

            XmlDocument xmlrdrdoc = new XmlDocument();
            
            xmlrdrdoc.Load(InputFile);

            // get a list of all items
            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("/INI/Section");
            foreach (XmlNode xn in xnrdrList)
            {
                //xn.Value
                //temp = xn.InnerText.ToString();
                //Console.WriteLine(xn.InnerText);
                Node ndparent = ndroot;
                Node ndchild = null;
                bool bFound;

                string[] strParts = new string[]
                {
                    xn["Type"].InnerText,
                    xn["Part1"].InnerText,
                    xn["Part2"].InnerText,
                    xn["Part3"].InnerText,
                    xn["Part4"].InnerText,
                    xn["Part5"].InnerText,
                    xn["Part6"].InnerText,
                    xn["Part7"].InnerText,
                    xn["Part8"].InnerText,
                    xn["Part9"].InnerText,
                    xn["Part10"].InnerText,
                    xn["Part11"].InnerText,
                    xn["Part12"].InnerText,
                    xn["Part13"].InnerText,
                    xn["Part14"].InnerText,
                    xn["Name"].InnerText,
                    xn["Rating"].InnerText,
                    xn["Description"].InnerText,
                    //"0", "5", "63" 
                    xn["RemAmmo_Quantity"].InnerText,
                    xn["Quality"].InnerText,
                    xn["Level"].InnerText
                };
                //xn.Attributes["Name"].Value };

                if (strParts[15].Contains(" (Copy"))
                    strParts[15] = strParts[15].Remove(strParts[15].IndexOf(" (Copy"));

                string itemtypeprefixget = "";
                string itemtypeprefixgetsc = "";
                if (strParts[0] == "Item")
                {
                    //Type in Part2 -> strParts[2]
                    itemtypeprefixget = GetName(TitlesXml, strParts, 2, "Prefix");
                    if (itemtypeprefixget.Length == 0)
                    {
                        // Create a Section with the default prefix
                        string itemtypeprefix = strParts[2].Substring(strParts[2].LastIndexOf(".") + 1);
                        itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1);
                        TitlesXml.XmlWriteValue(strParts[2], "Prefix", itemtypeprefix);
                        itemtypeprefixget = itemtypeprefix;
                    }
                    itemtypeprefixget += " "; // Add trailing space

                    if (!strParts[15].StartsWith(itemtypeprefixget + itemtypeprefixgetsc) && itemtypeprefixget.Length > 1)
                    {
                        strParts[15] = itemtypeprefixget + itemtypeprefixgetsc + GetName(TitlesXml, strParts, 8, "PartName");
                        if (strParts[15].EndsWith(" "))
                            strParts[15] = strParts[15] + GetName(TitlesXml, strParts, 9, "PartName");
                        else
                            strParts[15] = strParts[15] + ' ' + GetName(TitlesXml, strParts, 9, "PartName");
                    }
                }
                else if (strParts[0] == "Weapon")
                {
                    //Type in Part1 -> strParts[1]
                    itemtypeprefixget = GetName(TitlesXml, strParts, 1, "Prefix");
                    if (itemtypeprefixget.Length == 0)
                    {
                        string itemtypeprefix = strParts[1].Substring(strParts[1].LastIndexOf(".") + 1);
                        itemtypeprefix = itemtypeprefix.Substring(itemtypeprefix.IndexOf("_") + 1);
                        if (itemtypeprefix.StartsWith("Weapon_"))
                            itemtypeprefix = itemtypeprefix.Substring(7);

                        TitlesXml.XmlWriteValue(strParts[1], "Prefix", itemtypeprefix);
                        itemtypeprefixget = itemtypeprefix;
                    }
                    itemtypeprefixget += " "; // Add trailing space

                    if (strParts[1].EndsWith("Scorpio"))
                    {
                        // if its a scorpio better take the type in Part3 -> strParts[3]
                        //gd_weap_assault_shotgun.A_Weapon.WeaponType_assault_shotgun
                        itemtypeprefixgetsc = GetName(TitlesXml, strParts, 3, "ScorpioPrefix");
                        if (itemtypeprefixgetsc.Length == 0)
                        {
                            string itemtypeprefixsc = strParts[3].Substring(strParts[3].LastIndexOf(".") + 1);
                            itemtypeprefixsc = itemtypeprefixsc.Substring(itemtypeprefixsc.IndexOf("_") + 1);
                            if (itemtypeprefixsc.StartsWith("Weapon_"))
                                itemtypeprefixsc = itemtypeprefixsc.Substring(7);

                            TitlesXml.XmlWriteValue(strParts[3], "ScorpioPrefix", itemtypeprefixsc);
                            itemtypeprefixgetsc = itemtypeprefixsc;


                        }
                        itemtypeprefixgetsc += " "; // Add trailing space
                    }

                    if (!strParts[15].StartsWith(itemtypeprefixget + itemtypeprefixgetsc) && itemtypeprefixget.Length > 1)
                    {
                        strParts[15] = itemtypeprefixget + itemtypeprefixgetsc + GetName(TitlesXml, strParts, 13, "PartName");
                        if (strParts[15].EndsWith(" "))
                            strParts[15] = strParts[15] + GetName(TitlesXml, strParts, 14, "PartName");
                        else
                            strParts[15] = strParts[15] + ' ' + GetName(TitlesXml, strParts, 14, "PartName");
                    }
                }

                for (int partindex = 0; partindex < 21; partindex++)
                {
                    // All sections
                    // read the xml values
                    bFound = false;

                    for (int ndcnt = 0; ndcnt < ndparent.Nodes.Count; ndcnt++)
                    {
                        if (ndparent.Nodes[ndcnt].Text == strParts[partindex])
                        {
                            bFound = true;
                            ndparent = ndparent.Nodes[ndcnt];
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        ndchild = new Node();
                        ndchild.Text = strParts[partindex];
                        ndchild.Expand();
                        ndparent.Nodes.Add(ndchild);
                        ndparent = ndchild;
                    }
                }
            }



            //string tempINI = "";
            List<string> listNames = new List<string>();

            XmlTextWriter writer = new XmlTextWriter(AppDir + "\\Data\\default.xml", new System.Text.ASCIIEncoding());
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("Comment");
            writer.WriteStartElement("INI");

            for (int ndcntrt = 0; ndcntrt < ndroot.Nodes.Count; ndcntrt++)
            {
                Node ndtype = ndroot.Nodes[ndcntrt];
                for (int ndcnttype = 0; ndcnttype < ndtype.Nodes.Count; ndcnttype++)
                {
                    Node ndpart1 = ndtype.Nodes[ndcnttype];

                    for (int ndcntpart1 = 0; ndcntpart1 < ndpart1.Nodes.Count; ndcntpart1++)
                    {
                        Node ndpart2 = ndpart1.Nodes[ndcntpart1];

                        for (int ndcntpart2 = 0; ndcntpart2 < ndpart2.Nodes.Count; ndcntpart2++)
                        {
                            Node ndpart3 = ndpart2.Nodes[ndcntpart2];
                            for (int ndcntpart3 = 0; ndcntpart3 < ndpart3.Nodes.Count; ndcntpart3++)
                            {
                                Node ndpart4 = ndpart3.Nodes[ndcntpart3];

                                for (int ndcntpart4 = 0; ndcntpart4 < ndpart4.Nodes.Count; ndcntpart4++)
                                {
                                    Node ndpart5 = ndpart4.Nodes[ndcntpart4];

                                    for (int ndcntpart5 = 0; ndcntpart5 < ndpart5.Nodes.Count; ndcntpart5++)
                                    {
                                        Node ndpart6 = ndpart5.Nodes[ndcntpart5];

                                        for (int ndcntpart6 = 0; ndcntpart6 < ndpart6.Nodes.Count; ndcntpart6++)
                                        {
                                            Node ndpart7 = ndpart6.Nodes[ndcntpart6];

                                            for (int ndcntpart7 = 0; ndcntpart7 < ndpart7.Nodes.Count; ndcntpart7++)
                                            {
                                                Node ndpart8 = ndpart7.Nodes[ndcntpart7];

                                                for (int ndcntpart8 = 0; ndcntpart8 < ndpart8.Nodes.Count; ndcntpart8++)
                                                {
                                                    Node ndpart9 = ndpart8.Nodes[ndcntpart8];

                                                    for (int ndcntpart9 = 0; ndcntpart9 < ndpart9.Nodes.Count; ndcntpart9++)
                                                    {
                                                        Node ndpart10 = ndpart9.Nodes[ndcntpart9];

                                                        for (int ndcntpart10 = 0; ndcntpart10 < ndpart10.Nodes.Count; ndcntpart10++)
                                                        {
                                                            Node ndpart11 = ndpart10.Nodes[ndcntpart10];

                                                            for (int ndcntpart11 = 0; ndcntpart11 < ndpart11.Nodes.Count; ndcntpart11++)
                                                            {
                                                                Node ndpart12 = ndpart11.Nodes[ndcntpart11];

                                                                for (int ndcntpart12 = 0; ndcntpart12 < ndpart12.Nodes.Count; ndcntpart12++)
                                                                {
                                                                    Node ndpart13 = ndpart12.Nodes[ndcntpart12];

                                                                    for (int ndcntpart13 = 0; ndcntpart13 < ndpart13.Nodes.Count; ndcntpart13++)
                                                                    {
                                                                        Node ndpart14 = ndpart13.Nodes[ndcntpart13];

                                                                        for (int ndcntpart14 = 0; ndcntpart14 < ndpart14.Nodes.Count; ndcntpart14++)
                                                                        {

                                                                            Node ndpart15 = ndpart14.Nodes[ndcntpart14];
                                                                            for (int ndcntpart15 = 0; ndcntpart15 < ndpart15.Nodes.Count; ndcntpart15++)
                                                                            {
                                                                                Node ndpart16 = ndpart15.Nodes[ndcntpart15];
                                                                                for (int ndcntpart16 = 0; ndcntpart16 < ndpart16.Nodes.Count; ndcntpart16++)
                                                                                {
                                                                                    Node ndpart17 = ndpart16.Nodes[ndcntpart16];
                                                                                    for (int ndcntpart17 = 0; ndcntpart17 < ndpart17.Nodes.Count; ndcntpart17++)
                                                                                    {
                                                                                        Node ndpart18 = ndpart17.Nodes[ndcntpart17];
                                                                                        for (int ndcntpart18 = 0; ndcntpart18 < ndpart18.Nodes.Count; ndcntpart18++)
                                                                                        {
                                                                                            Node ndpart19 = ndpart18.Nodes[ndcntpart18];
                                                                                            for (int ndcntpart19 = 0; ndcntpart19 < ndpart19.Nodes.Count; ndcntpart19++)
                                                                                            {
                                                                                                Node ndpart20 = ndpart19.Nodes[ndcntpart19];

                                                                                                string NewName = "";

                                                                                                if (listNames.Contains(ndpart15.Text))
                                                                                                {
                                                                                                    int nCount = 1;
                                                                                                    while (listNames.Contains(ndpart15.Text + " (Copy " + nCount + ")"))
                                                                                                        nCount++;

                                                                                                    NewName = ndpart15.Text + " (Copy " + nCount + ")";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    NewName = ndpart15.Text;
                                                                                                }

                                                                                                listNames.Add(NewName);

                                                                                                writer.WriteStartElement("Section");
                                                                                                //writer.WriteAttributeString("Name", NewName);
                                                                                                writer.WriteElementString("Name", NewName);
                                                                                                writer.WriteElementString("Type", ndtype.Text);
                                                                                                writer.WriteElementString("Rating", ndpart16.Text);
                                                                                                writer.WriteElementString("Description", ndpart17.Text.Replace('\"', ' ').Trim());

                                                                                                writer.WriteElementString("Part1", ndpart1.Text);
                                                                                                writer.WriteElementString("Part2", ndpart2.Text);
                                                                                                writer.WriteElementString("Part3", ndpart3.Text);
                                                                                                writer.WriteElementString("Part4", ndpart4.Text);
                                                                                                writer.WriteElementString("Part5", ndpart5.Text);
                                                                                                writer.WriteElementString("Part6", ndpart6.Text);
                                                                                                writer.WriteElementString("Part7", ndpart7.Text);
                                                                                                writer.WriteElementString("Part8", ndpart8.Text);
                                                                                                writer.WriteElementString("Part9", ndpart9.Text);
                                                                                                writer.WriteElementString("Part10", ndpart10.Text);
                                                                                                writer.WriteElementString("Part11", ndpart11.Text);
                                                                                                writer.WriteElementString("Part12", ndpart12.Text);
                                                                                                writer.WriteElementString("Part13", ndpart13.Text);
                                                                                                writer.WriteElementString("Part14", ndpart14.Text);
                                                                                                writer.WriteElementString("RemAmmo_Quantity", ndpart18.Text);
                                                                                                writer.WriteElementString("Quality", ndpart19.Text);
                                                                                                writer.WriteElementString("Level", ndpart20.Text);
                                                                                                writer.WriteEndElement();
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //System.IO.File.WriteAllText(AppDir + "\\Data\\default2.wtl", tempINI);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();


        }
        public void DoPartsCategory(string Category, AdvTree Tree)
        {
            //Ini.IniFile PartList = new Ini.IniFile(AppDir + "\\Data\\" + Category + ".txt");
            XmlFile PartList = new XmlFile(AppDir + "\\Data\\" + Category + ".txt");

            Node TempNode = new Node();
            TempNode.Name = Category;
            TempNode.Text = Category;
            //TempNode.Style.ApplyStyle(elementStyle16);
            foreach (string section in PartList.stListSectionNames())
            {
                Node Part = new Node();
                Part.Name = section;
                Part.Text = section;
                TempNode.Nodes.Add(Part);

            }
            Tree.Nodes.Add(TempNode);

        }
        public void DoTabs()
        {
            string TabsLine = System.IO.File.ReadAllText(AppDir + "\\Data\\weapon_tabs.txt");
            string[] TabsList = TabsLine.Split(new char[] { (char)';' });
            for (int Progress = 0; Progress < TabsList.Length; Progress++) DoPartsCategory(TabsList[Progress], PartCategories);
            string TabsLine2 = System.IO.File.ReadAllText(AppDir + "\\Data\\item_tabs.txt");
            string[] TabsList2 = TabsLine2.Split(new char[] { (char)';' });
            for (int Progress = 0; Progress < TabsList2.Length; Progress++) DoPartsCategory(TabsList2[Progress], ItemPartsSelector);

        }
        public void DoQuestList()
        {
            XmlFile PartList = new XmlFile(AppDir + "\\Data\\Quests.ini");

            foreach (string section in PartList.stListSectionNames())
                QuestList.Items.Add(PartList.XmlReadValue(section, "MissionName"));

            //Ini.IniFile PartList = new Ini.IniFile(AppDir + "\\Data\\Quests.ini");
            //for (int Progress = 0; Progress < PartList.ListSectionNames().Length; Progress++)
            //    QuestList.Items.Add(PartList.IniReadValue(PartList.ListSectionNames()[Progress], "MissionName"));

        }
        public void DoEchoList()
        {
            //Ini.IniFile PartList = new Ini.IniFile(AppDir + "\\Data\\Echos.ini");
            XmlFile PartList = new XmlFile(AppDir + "\\Data\\Echos.ini");

            foreach (string section in PartList.stListSectionNames())
            {
                string name = PartList.XmlReadValue(section, "Subject");
                if (name != "")
                    EchoList.Items.Add(name);
                else
                    EchoList.Items.Add(section);
            }

            //for (int Progress = 0; Progress < PartList.stListSectionNames().Count; Progress++)
            //{
            //    string name = PartList.XmlReadValue(PartList.stListSectionNames()[Progress], "Subject");
            //    if (name != "")
            //        EchoList.Items.Add(name);
            //    else EchoList.Items.Add(PartList.stListSectionNames()[Progress]);
            //}

        }

        public void SaveChangesToLocker()
        {
            // no dupes in locker

            // Locate Node

            try
            {
                /*              string tempINI = System.IO.File.ReadAllText(OpenedLocker);
                              tempINI = tempINI.Replace("\r\n\r\n\r\n", "\r\n"); // Clean up extra lines.
                              System.IO.File.WriteAllText(OpenedLocker, tempINI);
                              string search = "[" + LockerTree.SelectedNode.Text + "]";

                              string[] tempINI2 = System.IO.File.ReadAllLines(OpenedLocker);
                              int NumberOfFoundLines = 0;
                              int DiscoveredLine = -1;
                              bool FoundOriginal = false;
                              for (int Progress = 0; Progress < tempINI2.Length; Progress++)
                              {
                                  //string tewst = tempINI2[Progress].Substring(0, tempINI2[Progress].Length - 1);

                                  if (tempINI2[Progress] == search)
                                      DiscoveredLine = Progress;


                                  else if (tempINI2[Progress] == "[" + NameLocker.Text + "]")
                                  {
                                      if (LockerTree.SelectedNode.Text != NameLocker.Text)
                                      {
                                          NumberOfFoundLines = NumberOfFoundLines + 1;
                                          FoundOriginal = true;
                                      }
                                  }
                                  else if (tempINI2[Progress].Contains("[" + NameLocker.Text + " (Copy ") == true)
                                      if (LockerTree.SelectedNode.Text != NameLocker.Text)
                                          NumberOfFoundLines = NumberOfFoundLines + 1;
                              }
                              if (NumberOfFoundLines == 0)
                                  tempINI2[DiscoveredLine] = "[" + NameLocker.Text + "]";
                              else if (NumberOfFoundLines > 0 && FoundOriginal == false)
                                  tempINI2[DiscoveredLine] = "[" + NameLocker.Text + "]";
                              else if (NumberOfFoundLines > 0 && FoundOriginal == true)
                                  tempINI2[DiscoveredLine] = "[" + NameLocker.Text + " (Copy " + (NumberOfFoundLines) + ")" + "]";


                              //tempINI.Replace("[" + LockerTree.SelectedNode.Text + "]", "[" + NameLocker.Text + "]");
                              System.IO.File.WriteAllLines(OpenedLocker, tempINI2);

              */
                string Description = "";
                //Ini.IniFile Locker = new Ini.IniFile(OpenedLocker);
                string LockerSelectedNode = LockerTree.SelectedNode.Name;

                //XmlLocker.ListSectionNames()[LockerTree.SelectedNode.Index] = NameLocker.Text;
                XmlLocker.XmlWriteValue(LockerSelectedNode, "Rating", "" + RatingLocker.Rating);
                for (int Progress = 0; Progress < DescriptionLocker.Lines.Length; Progress++)
                    Description = Description + DescriptionLocker.Lines[Progress] + "$LINE$";
                for (int Progress = 0; Progress < 14; Progress++)
                    XmlLocker.XmlWriteValue(LockerSelectedNode, "Part" + (Progress + 1), (string)PartsLocker.Items[Progress]);

                XmlLocker.XmlWriteValue(LockerSelectedNode, "Description", Description);
                XmlLocker.XmlWriteValue(LockerSelectedNode, "Type", "" + ItemTypeLocker.SelectedItem);

                XmlLocker.XmlWriteValue(LockerSelectedNode, "RemAmmo_Quantity", LockerRemAmmo.Value.ToString());
                XmlLocker.XmlWriteValue(LockerSelectedNode, "Quality", LockerQuality.Value.ToString());
                XmlLocker.XmlWriteValue(LockerSelectedNode, "Level", LockerLevel.Value.ToString());

                //Locker.IniReadValue(Locker.ListSectionNames()[LockerTree.SelectedNode.Index] = "";

                //DoLockerTree(OpenedLocker);
                //LockerTree.SelectedNode.Text = XmlLocker.ListSectionNames()[LockerTree.SelectedNode.Index];

                if (LockerSelectedNode != NameLocker.Text)
                {
                    string UniqueName = XmlLocker.GetUniqueName(NameLocker.Text);
                    XmlLocker.RenameItem(LockerSelectedNode, UniqueName);
                    LockerTree.SelectedNode.Name = UniqueName;
                    LockerTree.SelectedNode.Text = UniqueName;
                    LockerTree.Update();
                }
              
//              NameLocker.Text = LockerSelectedNode;

                XmlLocker.Reload(); //reload next time
            }
            catch { MessageBox.Show("Couldn't save changes."); }
        }
        public void DoLocationsList()
        {
            LocationsList.Items.Clear();
            CurrentLocation.Items.Clear();
            //Ini.IniFile Locations = new Ini.IniFile(AppDir + "\\Data\\Locations.ini");
            XmlFile Locations = new XmlFile(AppDir + "\\Data\\Locations.ini");
            foreach (string section in Locations.stListSectionNames())
            {
                string outpostname = Locations.XmlReadValue(section, "OutpostDisplayName");
                LocationsList.Items.Add(outpostname);
                CurrentLocation.Items.Add(outpostname);
            }
        }
        public void DoSkillList()
        {
            SkillList.Items.Clear();
            if ((string)Class.SelectedItem == "Soldier")
            {
                //Ini.IniFile SkillINI = new Ini.IniFile(AppDir + "\\Data\\gd_skills2_Roland.txt");
                XmlFile SkillXML = new XmlFile(AppDir + "\\Data\\gd_skills2_Roland.txt");
                foreach (string section in SkillXML.stListSectionNames())
                    SkillList.Items.Add((string)SkillXML.XmlReadValue(section, "SkillName"));
            }
            else if ((string)Class.SelectedItem == "Siren")
            {
                //Ini.IniFile SkillINI = new Ini.IniFile(AppDir + "\\Data\\gd_Skills2_Lilith.txt");
                XmlFile SkillXML = new XmlFile(AppDir + "\\Data\\gd_Skills2_Lilith.txt");
                foreach (string section in SkillXML.stListSectionNames())
                    SkillList.Items.Add((string)SkillXML.XmlReadValue(section, "SkillName"));
            }
            else if ((string)Class.SelectedItem == "Hunter")
            {
                //Ini.IniFile SkillINI = new Ini.IniFile(AppDir + "\\Data\\gd_skills2_Mordecai.txt");
                XmlFile SkillXML = new XmlFile(AppDir + "\\Data\\gd_skills2_Mordecai.txt");
                foreach (string section in SkillXML.stListSectionNames())
                    SkillList.Items.Add((string)SkillXML.XmlReadValue(section, "SkillName"));
            }
            else if ((string)Class.SelectedItem == "Berserker")
            {
                //Ini.IniFile SkillINI = new Ini.IniFile(AppDir + "\\Data\\gd_Skills2_Brick.txt");
                XmlFile SkillXML = new XmlFile(AppDir + "\\Data\\gd_Skills2_Brick.txt");
                foreach (string section in SkillXML.stListSectionNames())
                    SkillList.Items.Add((string)SkillXML.XmlReadValue(section, "SkillName"));
            }
            else
            {
                //Ini.IniFile SkillINI = new Ini.IniFile(AppDir + "\\Data\\gd_skills_common.txt");
                XmlFile SkillXML = new XmlFile(AppDir + "\\Data\\gd_skills_common.txt");
                foreach (string section in SkillXML.stListSectionNames())
                    SkillList.Items.Add((string)SkillXML.XmlReadValue(section, "SkillName"));
            }
        }
        public string GetAmmoName(string d_resources)
        {
            if (d_resources == "d_resources.AmmoResources.Ammo_Sniper_Rifle")
                return "Sniper Rifle";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Repeater_Pistol")
                return "Repeater Pistol";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Grenade_Protean")
                return "Protean Grenades";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Patrol_SMG")
                return "Patrol SMG";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Combat_Shotgun")
                return "Combat Shotgun";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Combat_Rifle")
                return "Combat Rifle";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Revolver_Pistol")
                return "Revolver Pistol";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Rocket_Launcher")
                return "Rocket Launcher";
            else
                return d_resources;
        }
        public void StartUpFunctions()
        {
            DoTabs();
//            if ((System.IO.File.Exists(AppDir + "\\Data\\default.xml") == false) && (System.IO.File.Exists(AppDir + "\\Data\\default.wtl") == true))
//                DoLockerTreetry(AppDir + "\\Data\\default.wtl");
            if (System.IO.File.Exists(AppDir + "\\Data\\default.xml") == false)
                System.IO.File.WriteAllText(AppDir + "\\Data\\default.xml", "<?xml version=\"1.0\" encoding=\"us-ascii\"?>\r\n<INI></INI>\r\n");

            try
            {
                DoLockerTreeNodupe(AppDir + "\\Data\\default.xml");
                DoLockerTree(AppDir + "\\Data\\default.xml");
            }
            catch
            {
                MessageBox.Show("The locker file \"" + AppDir + "\\Data\\default.xml\" could not be loaded.  It may be corrupt.  If you delete it the program will make a new one and you may be able to start the program successfully.  Shutting down now.");
                Application.Exit();
                return;
            }


            DoLocationsList();
            DoQuestList();
            DoEchoList();
            setXPchart();
        }
        public bool CheckIfNull(string[] Array)
        {
            try
            {
                if (Array.Length > 0)
                    return false;
                else
                    return true;
            }
            catch { return true; }
        }
        public int[] XPChart = new int[71];
        public void setXPchart()
        {
            XPChart[0] = 0;
            XPChart[1] = 0;
            XPChart[2] = 358;
            XPChart[3] = 1241;
            XPChart[4] = 2850;
            XPChart[5] = 5376;
            XPChart[6] = 8997;
            XPChart[7] = 13886;
            XPChart[8] = 20208;
            XPChart[9] = 28126;
            XPChart[10] = 37798;
            XPChart[11] = 49377;
            XPChart[12] = 63016;
            XPChart[13] = 78861;
            XPChart[14] = 97061;
            XPChart[15] = 117757;
            XPChart[16] = 141092;
            XPChart[17] = 167207;
            XPChart[18] = 196238;
            XPChart[19] = 228322;
            XPChart[20] = 263595;
            XPChart[21] = 302190;
            XPChart[22] = 344238;
            XPChart[23] = 389873;
            XPChart[24] = 439222;
            XPChart[25] = 492414;
            XPChart[26] = 549578;
            XPChart[27] = 610840;
            XPChart[28] = 676325;
            XPChart[29] = 746158;
            XPChart[30] = 820463;
            XPChart[31] = 899363;
            XPChart[32] = 982980;
            XPChart[33] = 1071436;
            XPChart[34] = 1164850;
            XPChart[35] = 1263343;
            XPChart[36] = 1367034;
            XPChart[37] = 1476041;
            XPChart[38] = 1590483;
            XPChart[39] = 1710476;
            XPChart[40] = 1836137;
            XPChart[41] = 1967582;
            XPChart[42] = 2104926;
            XPChart[43] = 2248285;
            XPChart[44] = 2397772;
            XPChart[45] = 2553561;
            XPChart[46] = 2715586;
            XPChart[47] = 2884139;
            XPChart[48] = 3059273;
            XPChart[49] = 3241098;
            XPChart[50] = 3429728;
            // lvl 50 says 3625271
            XPChart[51] = 3628272;
            XPChart[52] = 3827841;
            XPChart[53] = 4037544;
            XPChart[54] = 4254492;
            XPChart[55] = 4478793;
            XPChart[56] = 4710557;
            XPChart[57] = 4949891;
            XPChart[58] = 5196904;
            XPChart[59] = 5451702;
            XPChart[60] = 5714395;
            XPChart[61] = 5985086;
            //Knoxx-only
            XPChart[62] = 6263885;
            XPChart[63] = 6550897;
            XPChart[64] = 6846227;
            XPChart[65] = 7149982;
            XPChart[66] = 7462266;
            XPChart[67] = 7783184;
            XPChart[68] = 8112840;
            XPChart[69] = 8451340;

            XPChart[70] = 2147483647;
        }
        public bool MultipleIntroStateSaver(int Playthrough)
        {
            int TotalFound = 0;
            string[] PT;
            if (Playthrough == 1)
                PT = CurrentWSG.PT1Strings;
            else
                PT = CurrentWSG.PT2Strings;
            for (int Progress = 0; Progress < PT.Length; Progress++)
                if (PT[Progress] == "Z0_Missions.Missions.M_IntroStateSaver")
                    TotalFound = TotalFound + 1;
            if (TotalFound > 1)
                return true;
            else
                return false;
        }
        public int GetExtraStats(string[] WeaponParts, string StatName)
        {
            try
            {



                double ExtraDamage = 0;
                for (int i = 3; i < 14; i++)
                    if (WeaponParts[i].Contains("."))
                        ExtraDamage = ExtraDamage + Conversion.Val(new XmlFile(AppDir + "\\Data\\" + WeaponParts[i].Substring(0, WeaponParts[i].IndexOf(".")) + ".txt").XmlReadValue(WeaponParts[i].Substring(WeaponParts[i].IndexOf(".") + 1), StatName));

                if (StatName == "TechLevelIncrease")
                    return (int)ExtraDamage;
                else
                    return (int)((ExtraDamage) * 100);
            }
            catch
            {
                return -1;
            }
        }
        public int GetWeaponDamage(string[] WeaponParts)
        {
            try
            {
                string ItemGradeFile = WeaponParts[0].Substring(0, WeaponParts[0].IndexOf(".")) + ".txt";
                string ItemGradePart = WeaponParts[0].Substring(WeaponParts[0].IndexOf(".") + 1);
                string Manufacturer = WeaponParts[1].Substring(WeaponParts[1].LastIndexOf(".") + 1);
                XmlFile ItemGrade = new XmlFile(AppDir + "\\Data\\" + ItemGradeFile);

                double ExtraDamage = 0;
                double Multiplier = Conversion.Val(new XmlFile(AppDir + "\\Data\\" + WeaponParts[2].Substring(0, WeaponParts[2].IndexOf(".")) + ".txt").XmlReadValue(WeaponParts[2].Substring(WeaponParts[2].IndexOf(".") + 1), "WeaponDamageFormulaMultiplier")); ;
                double Level = Conversion.Val(ItemGrade.XmlReadValue(ItemGradePart, Manufacturer + "(" + WeaponQuality.Value + ")"));
                double Power = 1.3;
                double Offset = 9;
                for (int i = 3; i < 14; i++)
                    if (WeaponParts[i].Contains("."))
                        ExtraDamage = ExtraDamage + Conversion.Val(new XmlFile(AppDir + "\\Data\\" + WeaponParts[i].Substring(0, WeaponParts[i].IndexOf(".")) + ".txt").XmlReadValue(WeaponParts[i].Substring(WeaponParts[i].IndexOf(".") + 1), "WeaponDamage"));


                return (int)(ExtraDamage * (Multiplier * (Math.Pow(Level, Power) + Offset))) + (int)(Multiplier * (Math.Pow(Level, Power) + Offset));
            }
            catch
            {
                return 1337;
            }
        }
        public int GetWeaponDamage(string[] WeaponParts, double Itemgrade)
        {
            try
            {
                string ItemGradeFile = WeaponParts[0].Substring(0, WeaponParts[0].IndexOf(".")) + ".txt";
                string ItemGradePart = WeaponParts[0].Substring(WeaponParts[0].IndexOf(".") + 1);
                string Manufacturer = WeaponParts[1].Substring(WeaponParts[1].LastIndexOf(".") + 1);
                XmlFile ItemGrade = new XmlFile(AppDir + "\\Data\\" + ItemGradeFile);
                double ExtraDamage = 0;
                double Multiplier = Conversion.Val(new XmlFile(AppDir + "\\Data\\" + WeaponParts[2].Substring(0, WeaponParts[2].IndexOf(".")) + ".txt").XmlReadValue(WeaponParts[2].Substring(WeaponParts[2].IndexOf(".") + 1), "WeaponDamageFormulaMultiplier")); ;
                double Level = Itemgrade;
                double Power = 1.3;
                double Offset = 9;
                for (int i = 3; i < 14; i++)
                    if (WeaponParts[i].Contains("."))
                        ExtraDamage = ExtraDamage + Conversion.Val(new XmlFile(AppDir + "\\Data\\" + WeaponParts[i].Substring(0, WeaponParts[i].IndexOf(".")) + ".txt").XmlReadValue(WeaponParts[i].Substring(WeaponParts[i].IndexOf(".") + 1), "WeaponDamage"));


                return (int)(ExtraDamage * (Multiplier * (Math.Pow(Level, Power) + Offset))) + (int)(Multiplier * (Math.Pow(Level, Power) + Offset));
            }
            catch
            {
                return 1337;
            }
        }
        public bool IsDLCWeaponMode = false;
        public bool IsDLCItemMode = false;

        //No longer used. Just kept in case the need arises to have the DLC/Normal backpacks seperated.
        public void DoDLCWeaponTree()
        {
            WeaponTree.Nodes.Clear();
            //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
            //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini"); ;

            for (int build = 0; build < CurrentWSG.DLC.WeaponParts.Count; build++)
            {
                Node TempNode = new Node();
                TempNode.Text = GetName(TitlesXml, CurrentWSG.DLC.WeaponParts, build, 14, "PartName", "Weapon");
                TempNode.Name = "" + build;
                for (int search_name = 0; search_name < 14; search_name++)
                {

                    //Node Sub = new Node();
                    //Sub.Text = "Part " + (search_name + 1) + ": " + CurrentWSG.WeaponStrings[build][search_name];
                    //TempNode.Nodes.Add(Sub);

                    //if(TempNode.Text == "" && Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName") != null)
                    //TempNode.Text = (Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName"));
                    //else if(Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName") != null) 
                    //TempNode.Text = (TempNode.Text + " " + Titles.IniReadValue(CurrentWSG.WeaponStrings[build, search_name], "PartName"));
                }
                if (TempNode.Text == "") TempNode.Text = "Unknown Weapon";
                WeaponTree.Nodes.Add(TempNode);
            }

        }
        public void DoDLCItemTree()
        {
            ItemTree.Nodes.Clear();
            //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
            //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini");

            for (int build = 0; build < CurrentWSG.DLC.ItemParts.Count; build++)
            {
                Node TempNode = new Node();
                TempNode.Text = "";
                TempNode.Name = "" + build;
                for (int search_name = 0; search_name < 9; search_name++)
                {

                    //Node Sub = new Node();
                    //Sub.Text = "Part " + (search_name + 1) + ": " + CurrentWSG.ItemStrings[build][search_name];
                    //TempNode.Nodes.Add(Sub);

                    if (TempNode.Text == "" && TitlesXml.XmlReadValue(CurrentWSG.DLC.ItemParts[build][search_name], "PartName") != null)
                        TempNode.Text = (TitlesXml.XmlReadValue(CurrentWSG.DLC.ItemParts[build][search_name], "PartName"));
                    else if (TitlesXml.XmlReadValue(CurrentWSG.DLC.ItemParts[build][search_name], "PartName") != null)
                        TempNode.Text = (TempNode.Text + " " + TitlesXml.XmlReadValue(CurrentWSG.DLC.ItemParts[build][search_name], "PartName"));

                }
                if (TempNode.Text == "") TempNode.Text = "Unknown Item";
                //Node Sub_Ammo = new Node();
                //Node Sub_Quality = new Node();
                //Node Sub_Slot = new Node();
                //Sub_Ammo.Text = "Quantity: " + CurrentWSG.ItemValues[build][0];
                //Sub_Quality.Text = "Quality Level: " + CurrentWSG.ItemValues[build][1];
                //Sub_Slot.Text = "Equipped: " + CurrentWSG.ItemValues[build][2];
                //TempNode.Nodes.Add(Sub_Ammo);
                //TempNode.Nodes.Add(Sub_Quality);
                //TempNode.Nodes.Add(Sub_Slot);
                ItemTree.Nodes.Add(TempNode);
            }

        }

        //Don't ask.
        public string AmIACookie(bool passedCookieClass)
        {
            if (passedCookieClass)
                return "Good job, Mr. Cookie!";
            else
                return "Bad Mr. Cookie! You're actually a Brownie!";
        }

        //Recovers the latest version from the sourceforge server.
        public void CheckVersion(object state)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string VersionTextFromServer = webClient.DownloadString("http://willowtree.sourceforge.net/version.txt");
                    string[] RemoteVersionInfo = VersionTextFromServer.Replace("\r\n", "\n").Split('\n');
                    if ((RemoteVersionInfo.Count() > 1) || (RemoteVersionInfo.Count() <= 3))
                    {
                        VersionFromServer = RemoteVersionInfo[0];
                        DownloadURLFromServer = RemoteVersionInfo[1];
                    }
                }
            }
            catch (WebException ex)
            {
                Trace.TraceError("Update check failed:" + Environment.NewLine + ex.ToString());
            }
        }


        private void TrySelectedNode(AdvTree DesiredTree, int SelectedIndex)
        {

            try
            {
                DesiredTree.SelectedNode = DesiredTree.Nodes[SelectedIndex];
            }
            catch
            {
                DesiredTree.SelectedNode = DesiredTree.Nodes[SelectedIndex - 1];
            }
            DesiredTree.SelectedNode.EnsureVisible();
        }
        private void TrySelectedNode(AdvTree DesiredTree, int RootNodeIndex, int SelectedIndex)
        {
            try { DesiredTree.SelectedNode = DesiredTree.Nodes[RootNodeIndex].Nodes[SelectedIndex]; }
            catch { DesiredTree.SelectedNode = DesiredTree.Nodes[RootNodeIndex].Nodes[SelectedIndex - 1]; }
            DesiredTree.SelectedNode.EnsureVisible();
        }

        //Resizes arrays. I really should just convert everything to lists...
        private void ResizeArrayLarger(ref string[,] Input, int rows, int cols)
        {
            string[,] newArray = new string[rows, cols];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        private void ResizeArraySmaller(ref string[,] Input, int rows, int cols)
        {
            string[,] newArray = new string[rows, cols];
            Array.Copy(Input, 0, newArray, 0, (long)(rows * cols));
            Input = newArray;
        }
        private void ResizeArrayLarger(ref string[] Input, int rows)
        {
            string[] newArray = new string[rows];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        private void ResizeArraySmaller(ref string[] Input, int rows)
        {
            string[] newArray = new string[rows];
            Array.Copy(Input, 0, newArray, 0, (long)rows);
            Input = newArray;
        }
        private void ResizeArrayLarger(ref int[,] Input, int rows, int cols)
        {
            int[,] newArray = new int[rows, cols];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        private void ResizeArraySmaller(ref int[,] Input, int rows, int cols)
        {
            int[,] newArray = new int[rows, cols];
            Array.Copy(Input, 0, newArray, 0, (long)((rows) * cols));
            Input = newArray;
        }
        private void ResizeArrayLarger(ref int[] Input, int rows)
        {
            int[] newArray = new int[rows];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        private void ResizeArraySmaller(ref int[] Input, int rows)
        {
            int[] newArray = new int[rows];
            Array.Copy(Input, 0, newArray, 0, (long)rows);
            Input = newArray;
        }
        private void ResizeArrayLarger(ref float[] Input, int rows)
        {
            float[] newArray = new float[rows];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        private void ResizeArraySmaller(ref float[] Input, int rows)
        {
            float[] newArray = new float[rows];
            Array.Copy(Input, 0, newArray, 0, (long)rows);
            Input = newArray;
        }

        WillowSaveGame CurrentWSG;

        private string AppDir = Path.GetDirectoryName(Application.ExecutablePath);

        public WillowTreeMain()
        {
            InitializeComponent();

            UpdateBar.Hide();

            MainTab.Select();
            if (System.IO.Directory.Exists(AppDir + "\\Data") == false)
            {
                MessageBox.Show("Couldn't find the 'Data' folder! Please make sure that WillowTree# and the 'Data' folder are in the same directory.");
                Application.Exit();
            }
            //DoTabs();
            //if (System.IO.File.Exists(AppDir + "\\Data\\default.wtl") == false)
            //System.IO.File.WriteAllText(AppDir + "\\Data\\default.wtl", "\r\n[New Weapon]\r\nType=Weapon\r\nRating=0\r\nDescription=\"Type in a description for the weapon here.\"\r\nPart1=None\r\nPart2=None\r\nPart3=None\r\nPart4=None\r\nPart5=None\r\nPart6=None\r\nPart7=None\r\nPart8=None\r\nPart9=None\r\nPart10=None\r\nPart11=None\r\nPart12=None\r\nPart13=None\r\nPart14=None");

            //DoLockerTree(AppDir + "\\Data\\default.wtl");
            GeneralTab.Enabled = false;
            SkillsTab.Enabled = false;
            AmmoTab.Enabled = false;
            ItemsTab.Enabled = false;
            WeaponsTab.Enabled = false;
            QuestsTab.Enabled = false;
            EchosTab.Enabled = false;
            //WTLTab.Enabled = false;
            ExportToBackpack.Enabled = false;
            ImportAllFromItems.Enabled = false;
            ImportAllFromWeapons.Enabled = false;
            ribbonControl2.SelectedRibbonTabItem = MainTab;
            if (System.Diagnostics.Debugger.IsAttached == true) DebugTab.Enabled = true;
            else
            {
                DebugTab.Enabled = false;
                DebugTab.Visible = false;
            }
            SelectFormat.Enabled = false;
            //DoLocationsList();
            //DoQuestList();
            //DoEchoList();


        }

        private void Open_Click(object sender, EventArgs e)
        {
            textBox1.Clear();


            OpenFileDialog tempOpen = new OpenFileDialog();
            tempOpen.DefaultExt = "*.sav";
            tempOpen.Filter = "WillowSaveGame(*.sav)|*.sav";



            if (tempOpen.ShowDialog() == DialogResult.OK)
                try
                {
                    BankSpace.Enabled = false;
                    BankSpace.Value = 0;
                    CurrentWSG = new WillowSaveGame();
                    CurrentWSG.OpenWSG(tempOpen.FileName);
                    //Ini.IniFile Locations = new Ini.IniFile(AppDir + "\\Data\\Locations.ini");
                    XmlFile Locations = new XmlFile(AppDir + "\\Data\\Locations.ini");
                    setXPchart();
                    textBox1.AppendText(CurrentWSG.Platform);
                    CharacterName.Text = CurrentWSG.CharacterName;
                    try
                    {
                        //Experience.Maximum = XPChart[(int)Level.Value];
                        if (CurrentWSG.Level <= Level.Maximum)
                            Level.Value = CurrentWSG.Level;
                        else
                            Level.Value = Level.Maximum;

                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("The level value was outside the acceptable range. Adjust your level and experience to their correct values.\r\nLevel: " + CurrentWSG.Level + "\r\nExperience: " + CurrentWSG.Experience);
                        Level.Value = 1;
                        Experience.Value = 0;
                    }
                    try
                    {
                        //Experience.Maximum = XPChart[(int)Level.Value];

                        if (CurrentWSG.Experience >= Experience.Minimum)
                            Experience.Value = CurrentWSG.Experience;
                        else
                            Experience.Value = Experience.Minimum;

                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("The experience value was outside the acceptable range. Adjust your level and experience to their correct values.\r\nLevel: " + CurrentWSG.Level + "\r\nExperience: " + CurrentWSG.Experience);

                        Experience.Value = Experience.Minimum;
                    }
                    if (CurrentWSG.SkillPoints <= SkillPoints.Maximum)
                        SkillPoints.Value = CurrentWSG.SkillPoints;
                    else
                        SkillPoints.Value = SkillPoints.Maximum;
                    PT2Unlocked.SelectedIndex = CurrentWSG.FinishedPlaythrough1;
                    try
                    {
                        Cash.Value = CurrentWSG.Cash;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Cash.Value = Cash.Maximum;
                        MessageBox.Show("This save has $" + CurrentWSG.Cash + ", which is outside of WillowTree#'s limits and can cause in-game issues. Because of this, your cash has been lowered. It is suggested that you further reduce your cash to prevent future problems.");
                    }
                    BackpackSpace.Value = CurrentWSG.BackpackSize;
                    EquipSlots.Value = CurrentWSG.EquipSlots;
                    SaveNumber.Value = CurrentWSG.SaveNumber;
                    CurrentLocation.SelectedItem = Locations.XmlReadValue(CurrentWSG.CurrentLocation, "OutpostDisplayName");
                    if (CurrentWSG.Class == "gd_Roland.Character.CharacterClass_Roland") Class.SelectedIndex = 0;
                    else if (CurrentWSG.Class == "gd_lilith.Character.CharacterClass_Lilith") Class.SelectedIndex = 1;
                    else if (CurrentWSG.Class == "gd_mordecai.Character.CharacterClass_Mordecai") Class.SelectedIndex = 2;
                    else if (CurrentWSG.Class == "gd_Brick.Character.CharacterClass_Brick") Class.SelectedIndex = 3;
                    textBox1.AppendText("\r\nHeader: " + CurrentWSG.MagicHeader + "\r\nVersion: " + CurrentWSG.VersionNumber + "\r\nPlyr String: " + CurrentWSG.PLYR + "\r\nRevision Number: " + CurrentWSG.RevisionNumber + "\r\nClass: " + CurrentWSG.Class + "\r\nLevel: " + CurrentWSG.Level + "\r\nExp: " + CurrentWSG.Experience + "\r\nSkill Points: " + CurrentWSG.SkillPoints + "\r\nunknown1: " + CurrentWSG.Unknown1 + "\r\nMoney: " + CurrentWSG.Cash + "\r\nFinished Game: " + CurrentWSG.FinishedPlaythrough1 + "\r\nNumber of skills: " + CurrentWSG.NumberOfSkills + "\r\nCurrent Quest: " + CurrentWSG.CurrentQuest + "\r\nTotal PT1 Quests: " + CurrentWSG.TotalPT1Quests + "\r\nSecondary Quest: " + CurrentWSG.SecondaryQuest + "\r\nTotal PT2 Quests: " + CurrentWSG.TotalPT2Quests + "\r\nUnknown PT1 Quest Value: " + CurrentWSG.UnknownPT1QuestValue);
                    ActivePT1Quest.Text = CurrentWSG.CurrentQuest;
                    ActivePT2Quest.Text = CurrentWSG.SecondaryQuest;
                    DoQuestTree();
                    DoLocationTree();
                    DoSkillTree();
                    DoEchoTree();
                    DoAmmoTree();
                    DoWeaponTree();
                    DoItemTree();
                    DoSkillList();

                    GeneralTab.Enabled = true;
                    AmmoTab.Enabled = true;
                    SkillsTab.Enabled = true;
                    EchosTab.Enabled = true;
                    WTLTab.Enabled = true;
                    ExportToBackpack.Enabled = true;
                    ImportAllFromItems.Enabled = true;
                    ImportAllFromWeapons.Enabled = true;
                    WeaponsTab.Enabled = true;
                    ItemsTab.Enabled = true;
                    QuestsTab.Enabled = true;
                    Save.Enabled = true;
                    SaveAs.Enabled = true;
                    SelectFormat.Enabled = true;

                    if (CurrentWSG.DLC.BankSize > 0)
                    {
                        BankSpace.Value = CurrentWSG.DLC.BankSize;
                        BankSpace.Enabled = true;
                    }
                    DoWindowTitle();
                }
                //if (VersionFromServer == "f")    
                //try {
                //  DoAmmoTree();
                //}
                catch
                {

                    if (CurrentWSG.EchoStringsPT2 == null && CurrentWSG.NumberOfEchosPT2 > 0 && CurrentWSG.TotalPT2Quests > 1 && CurrentWSG.NumberOfEchosPT2 < 300)
                        MessageBox.Show("Error reading PT2 echo logs.");
                    else
                        MessageBox.Show("Could not open save.");
                    BankSpace.Enabled = false;
                    BankSpace.Value = 0;
                    GeneralTab.Enabled = false;
                    AmmoTab.Enabled = false;
                    SkillsTab.Enabled = false;
                    EchosTab.Enabled = false;
                    //WTLTab.Enabled = false;
                    ExportToBackpack.Enabled = false;
                    ImportAllFromItems.Enabled = false;
                    ImportAllFromWeapons.Enabled = false;
                    WeaponsTab.Enabled = false;
                    ItemsTab.Enabled = false;
                    QuestsTab.Enabled = false;
                    Save.Enabled = false;
                    SaveAs.Enabled = false;
                    MainTab.Select();
                    textBox1.AppendText("\r\nHeader: " + CurrentWSG.MagicHeader + "\r\nVersion: " + CurrentWSG.VersionNumber + "\r\nPlyr String: " + CurrentWSG.PLYR + "\r\nRevision Number: " + CurrentWSG.RevisionNumber + "\r\nClass: " + CurrentWSG.Class + "\r\nLevel: " + CurrentWSG.Level + "\r\nExp: " + CurrentWSG.Experience + "\r\nSkill Points: " + CurrentWSG.SkillPoints + "\r\nunknown1: " + CurrentWSG.Unknown1 + "\r\nMoney: " + CurrentWSG.Cash + "\r\nFinished Game: " + CurrentWSG.FinishedPlaythrough1 + "\r\nNumber of skills: " + CurrentWSG.NumberOfSkills + "\r\nCurrent Quest: " + CurrentWSG.CurrentQuest + "\r\nTotal PT1 Quests: " + CurrentWSG.TotalPT1Quests + "\r\nSecondary Quest: " + CurrentWSG.SecondaryQuest + "\r\nTotal PT2 Quests: " + CurrentWSG.TotalPT2Quests + "\r\nUnknown PT1 Quest Value: " + CurrentWSG.UnknownPT1QuestValue);
                }
        }


        private void SkillTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            try
            {
                SkillName.Text = CurrentWSG.SkillNames[SkillTree.SelectedNode.Index];
                SkillLevel.Value = CurrentWSG.LevelOfSkills[SkillTree.SelectedNode.Index];
                SkillExp.Value = CurrentWSG.ExpOfSkills[SkillTree.SelectedNode.Index];
                if (CurrentWSG.InUse[SkillTree.SelectedNode.Index] == -1) SkillActive.SelectedItem = "No";
                else SkillActive.SelectedItem = "Yes";
            }
            catch { }
        }

        private void BuildXboxPackage(string packageFileName, string saveFileName)
        {
            CreateSTFS Package = new CreateSTFS();

            Package.STFSType = STFSType.Type1;
            Package.HeaderData.ProfileID = CurrentWSG.ProfileID;
            Package.HeaderData.DeviceID = CurrentWSG.DeviceID;

            Assembly newAssembly = Assembly.GetExecutingAssembly();
            Stream WT_Icon = newAssembly.GetManifestResourceStream("WillowTree.Resources.WT_CON.png");

            Package.HeaderData.ContentImage = System.Drawing.Image.FromStream(WT_Icon);
            Package.HeaderData.Title_Display = CurrentWSG.CharacterName + " - Level " + CurrentWSG.Level + " - " + CurrentLocation.Text;
            Package.HeaderData.Title_Package = "Borderlands";
            Package.HeaderData.TitleID = 1414793191;
            Package.AddFile(saveFileName, "SaveGame.sav");

            STFSPackage CON = new STFSPackage(Package, new RSAParams(AppDir + "\\Data\\KV.bin"), packageFileName, new X360.Other.LogRecord());

            CON.FlushPackage(new RSAParams(AppDir + "\\Data\\KV.bin"));
            CON.CloseIO();
            WT_Icon.Close();
        }
        private void SaveToFile(string filename)
        {
            //Ini.IniFile Locations = new Ini.IniFile(AppDir + "\\Data\\Locations.ini");
            XmlFile Locations = new XmlFile(AppDir + "\\Data\\Locations.ini");

            if (BankSpace.Enabled)
                CurrentWSG.DLC.BankSize = (int)BankSpace.Value;

            if (Class.SelectedIndex == 0) CurrentWSG.Class = "gd_Roland.Character.CharacterClass_Roland";
            else if (Class.SelectedIndex == 1) CurrentWSG.Class = "gd_lilith.Character.CharacterClass_Lilith";
            else if (Class.SelectedIndex == 2) CurrentWSG.Class = "gd_mordecai.Character.CharacterClass_Mordecai";
            else if (Class.SelectedIndex == 3) CurrentWSG.Class = "gd_Brick.Character.CharacterClass_Brick";
            CurrentWSG.CharacterName = CharacterName.Text;
            CurrentWSG.Level = (int)Level.Value;
            CurrentWSG.Experience = (int)Experience.Value;
            CurrentWSG.SkillPoints = (int)SkillPoints.Value;
            CurrentWSG.FinishedPlaythrough1 = PT2Unlocked.SelectedIndex;
            CurrentWSG.Cash = (int)Cash.Value;
            CurrentWSG.BackpackSize = (int)BackpackSpace.Value;
            CurrentWSG.EquipSlots = (int)EquipSlots.Value;
            CurrentWSG.SaveNumber = (int)SaveNumber.Value;
            if (CurrentLocation.SelectedText != "" && CurrentLocation.SelectedText != null)
                CurrentWSG.CurrentLocation = Locations.stListSectionNames()[CurrentLocation.SelectedIndex];

            if (CurrentWSG.Platform == "PS3" || CurrentWSG.Platform == "PC")
            {
                using (BinaryWriter Save = new BinaryWriter(new FileStream(filename, FileMode.Create)))
                {
                    Save.Write(CurrentWSG.SaveWSG());
                }
            }

            else if (CurrentWSG.Platform == "X360")
            {
                string tempSaveName = filename + ".temp";
                using (BinaryWriter Save = new BinaryWriter(new FileStream(tempSaveName, FileMode.Create)))
                {
                    Save.Write(CurrentWSG.SaveWSG());
                }

                BuildXboxPackage(filename, tempSaveName);
                File.Delete(tempSaveName);
            }
            CurrentWSG.OpenedWSG = filename;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog tempSave = new SaveFileDialog();
            tempSave.DefaultExt = "*.sav";
            tempSave.Filter = "WillowSaveGame(*.sav)|*.sav";

            tempSave.FileName = CurrentWSG.OpenedWSG;

            if (tempSave.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveToFile(tempSave.FileName);
                    MessageBox.Show("Saved WSG to: " + CurrentWSG.OpenedWSG);
                    Save.Enabled = true;
                }
                catch { MessageBox.Show("Couldn't save WSG"); }
            }
        }
        private void Save_Click_1(object sender, EventArgs e)
        {
            try
            {

                File.Copy(CurrentWSG.OpenedWSG, CurrentWSG.OpenedWSG + ".bak0", true);
                if (File.Exists(CurrentWSG.OpenedWSG + ".bak10") == true)
                    File.Delete(CurrentWSG.OpenedWSG + ".bak10");
                for (int i = 9; i >= 0; i--)
                {
                    if (File.Exists(CurrentWSG.OpenedWSG + ".bak" + i) == true)
                        File.Move(CurrentWSG.OpenedWSG + ".bak" + i, CurrentWSG.OpenedWSG + ".bak" + (i + 1));
                }

            }
            catch { }

            try
            {
                SaveToFile(CurrentWSG.OpenedWSG);
                MessageBox.Show("Saved WSG to: " + CurrentWSG.OpenedWSG);
            }
            catch { MessageBox.Show("Couldn't save WSG"); }
        }
        private void ExitWT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            //AliasToMyClass somevar = new AliasToMyClass();

            //MessageBox.Show( somevar);
        }

        private void WillowTree_Load(object sender, EventArgs e)
        {
            StartUpFunctions();

#if !DEBUG
            // Only check for new version if it's not a debug build.
            ThreadPool.QueueUserWorkItem(CheckVersion);
#endif

            //t1.Join();
            //if (VersionFromServer != Version.Text && VersionFromServer != "" && VersionFromServer != null)
            //{
            //    UpdateButton.Text = "Version " + VersionFromServer + " is now available! Click here to download.";
            //    UpdateBar.Show();
            //}

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //DoLockerTreetry(AppDir + "\\Data\\default.wtl");
        }
        private void Breakpoint_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debugger.Break();
        }

        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< WEAPONS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\

        private void WeaponTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {

            try
            {

                if (IsDLCWeaponMode)
                {
                    CurrentWeaponParts.Items.Clear();
                    CurrentPartsGroup.Text = WeaponTree.SelectedNode.Text;

                    for (int build_list = 0; build_list < 14; build_list++)
                    {
                        CurrentWeaponParts.Items.Add(CurrentWSG.DLC.WeaponParts[WeaponTree.SelectedNode.Index][build_list]);
                    }

                    RemainingAmmo.Value = CurrentWSG.DLC.WeaponAmmo[WeaponTree.SelectedNode.Index];
                    WeaponQuality.Value = CurrentWSG.DLC.WeaponQuality[WeaponTree.SelectedNode.Index];

                    WeaponItemGradeSlider.Value = CurrentWSG.DLC.WeaponLevel[WeaponTree.SelectedNode.Index];
                    if (CurrentWSG.DLC.WeaponEquippedSlot[WeaponTree.SelectedNode.Index] == 0) EquippedSlot.SelectedItem = "Unequipped";
                    else if (CurrentWSG.DLC.WeaponEquippedSlot[WeaponTree.SelectedNode.Index] == 1) EquippedSlot.SelectedItem = "Slot 1 (Up)";
                    else if (CurrentWSG.DLC.WeaponEquippedSlot[WeaponTree.SelectedNode.Index] == 2) EquippedSlot.SelectedItem = "Slot 2 (Down)";
                    else if (CurrentWSG.DLC.WeaponEquippedSlot[WeaponTree.SelectedNode.Index] == 3) EquippedSlot.SelectedItem = "Slot 3 (Left)";
                    else if (CurrentWSG.DLC.WeaponEquippedSlot[WeaponTree.SelectedNode.Index] > 3) EquippedSlot.SelectedItem = "Slot 4 (Right)";
                }
                else
                {
                    CurrentWeaponParts.Items.Clear();
                    CurrentPartsGroup.Text = WeaponTree.SelectedNode.Text;

                    for (int ndcnt = 0; ndcnt < PartCategories.Nodes.Count; ndcnt++)
                        PartCategories.Nodes[ndcnt].Style = elementStyle15;

                    for (int build_list = 0; build_list < 14; build_list++)
                    {
                        string curWeaponpart = CurrentWSG.WeaponStrings[WeaponTree.SelectedNode.Index][build_list];
                        CurrentWeaponParts.Items.Add(curWeaponpart);
                        // highlight the used partfamilies in the partscategories tree
                        if (curWeaponpart.Contains('.'))
                        {
                            string curWeaponpartclass = curWeaponpart.Substring(0, curWeaponpart.IndexOf('.'));
                            for (int ndcnt = 0; ndcnt < PartCategories.Nodes.Count; ndcnt++)
                            {
                                if (PartCategories.Nodes[ndcnt].Name == curWeaponpartclass)
                                {
                                    PartCategories.Nodes[ndcnt].Style = elementStyle16;
                                    break;
                                }
                            }
                        }

                    }

                    RemainingAmmo.Value = CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][0];
                    //Set Itemgrade before Quality -> Level will display correct
                    WeaponItemGradeSlider.Value = CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][3];

                    WeaponQuality.Value = CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][1];

                    if (CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][2] == 0) EquippedSlot.SelectedItem = "Unequipped";
                    else if (CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][2] == 1) EquippedSlot.SelectedItem = "Slot 1 (Up)";
                    else if (CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][2] == 2) EquippedSlot.SelectedItem = "Slot 2 (Down)";
                    else if (CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][2] == 3) EquippedSlot.SelectedItem = "Slot 3 (Left)";
                    else if (CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][2] > 3) EquippedSlot.SelectedItem = "Slot 4 (Right)";
                }

            }
            catch { }

        }

        private void PartCategories_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (CurrentWeaponParts.SelectedItem != null && PartCategories.SelectedNode.HasChildNodes == false)
            {
                CurrentWeaponParts.Items[CurrentWeaponParts.SelectedIndex] = PartCategories.SelectedNode.Parent.Text + "." + PartCategories.SelectedNode.Text;
            }

        }

        private void NewWeapons_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDLCWeaponMode)
                {

                    CurrentWSG.DLC.TotalWeapons++;
                    CurrentWSG.DLC.WeaponParts.Add(new List<string>());
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Item Grade");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Manufacturer");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Weapon Type");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Body");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Grip");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Mag");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Barrel");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Sight");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Stock");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Action");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Accessory");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Material");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Prefix");
                    CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add("Title");
                    CurrentWSG.DLC.WeaponLevel.Add(0);
                    CurrentWSG.DLC.WeaponQuality.Add(0);
                    CurrentWSG.DLC.WeaponAmmo.Add(0);
                    CurrentWSG.DLC.WeaponEquippedSlot.Add(0);


                    DoDLCWeaponTree();
                }
                else
                {
                    CurrentWSG.NumberOfWeapons++;
                    CurrentWSG.WeaponStrings.Add(new List<string>());
                    CurrentWSG.WeaponValues.Add(new List<int>());
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Item Grade");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Manufacturer");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Weapon Type");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Body");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Grip");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Mag");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Barrel");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Sight");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Stock");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Action");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Accessory");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Material");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Prefix");
                    CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add("Title");
                    CurrentWSG.WeaponValues[(CurrentWSG.NumberOfWeapons - 1)].Add(0);
                    CurrentWSG.WeaponValues[(CurrentWSG.NumberOfWeapons - 1)].Add(0);
                    CurrentWSG.WeaponValues[(CurrentWSG.NumberOfWeapons - 1)].Add(0);
                    CurrentWSG.WeaponValues[(CurrentWSG.NumberOfWeapons - 1)].Add(0);

                    Node TempNode = new Node();
                    TempNode.Text = "New Weapon";
                    WeaponTree.Nodes.Add(TempNode);
                    //DoWeaponTree();
                }
            }
            catch { }
        }

        private void SaveChangesWeapon_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = WeaponTree.SelectedNode.Index; ;

                if (IsDLCWeaponMode)
                {
                    for (int Progress = 0; Progress < 14; Progress++)
                        CurrentWSG.DLC.WeaponParts[WeaponTree.SelectedNode.Index][Progress] = (string)CurrentWeaponParts.Items[Progress];
                    CurrentWSG.DLC.WeaponAmmo[WeaponTree.SelectedNode.Index] = (int)RemainingAmmo.Value;
                    CurrentWSG.DLC.WeaponEquippedSlot[WeaponTree.SelectedNode.Index] = EquippedSlot.SelectedIndex;

                    CurrentWSG.DLC.WeaponQuality[WeaponTree.SelectedNode.Index] = (int)WeaponQuality.Value;
                    CurrentWSG.DLC.WeaponLevel[WeaponTree.SelectedNode.Index] = WeaponItemGradeSlider.Value;
                    //DoDLCWeaponTree();
                }
                else
                {
                    for (int Progress = 0; Progress < 14; Progress++)
                        CurrentWSG.WeaponStrings[WeaponTree.SelectedNode.Index][Progress] = (string)CurrentWeaponParts.Items[Progress];
                    CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][0] = (int)RemainingAmmo.Value;

                    CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][1] = (int)WeaponQuality.Value;
                    CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][3] = (int)WeaponItemGradeSlider.Value;

                    CurrentWSG.WeaponValues[WeaponTree.SelectedNode.Index][2] = EquippedSlot.SelectedIndex;
                    //DoWeaponTree();
                }
                WeaponTree.SelectedNode.Text = GetName(TitlesXml, CurrentWSG.WeaponStrings, WeaponTree.SelectedIndex, 14, "PartName", "Weapon");
            }
            catch { }
        } // Save Changes

        private void ExportWeaponToClipboard_Click(object sender, EventArgs e)
        {
            InOutPartsBox.Clear();
            for (int Progress = 0; Progress < 14; Progress++)
            {
                if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                InOutPartsBox.AppendText((string)CurrentWeaponParts.Items[Progress]);
            }
            InOutPartsBox.AppendText("\r\n" + RemainingAmmo.Value);
            InOutPartsBox.AppendText("\r\n" + WeaponQuality.Value);
            InOutPartsBox.AppendText("\r\n" + EquippedSlot.SelectedIndex);
            InOutPartsBox.AppendText("\r\n" + WeaponItemGradeSlider.Value);
            Clipboard.SetText(InOutPartsBox.Text + "\r\n");
        } // Export -> to Clipboard

        private void ImportWeaponFromClipboard_Click(object sender, EventArgs e)
        {
            InOutPartsBox.Clear();
            InOutPartsBox.Text = Clipboard.GetText();
            InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");

            try
            {
                for (int Progress = 0; Progress < 14; Progress++)
                {
                    ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                    CurrentWeaponParts.Items[Progress] = InOutPartsBox.Lines[Progress];
                }
                //CurrentWeaponParts.Items[0] = Cookie(true);

                // Exception if these are not int values
                RemainingAmmo.Value = Convert.ToInt32(InOutPartsBox.Lines[14]);
                WeaponQuality.Value = Convert.ToInt32(InOutPartsBox.Lines[15]);
                Convert.ToInt32(InOutPartsBox.Lines[16]); // Check for valid int, but don't use it
                EquippedSlot.SelectedIndex = 0; // override the equipped slot to 0
                WeaponItemGradeSlider.Value = Convert.ToInt32(InOutPartsBox.Lines[17]);
            }
            catch
            {
                MessageBox.Show("Invalid clipboard data.  Reverting to saved values.");
                WeaponTree_AfterNodeSelect(null, null);  // reloads the saved values from the node
            }
        
        }// Import -> from Clipboard

        private void DuplicateWeapons_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = WeaponTree.SelectedNode.Index;

                if (IsDLCWeaponMode)
                {
                    CurrentWSG.DLC.WeaponParts.Add(new List<string>());
                    foreach (string i in CurrentWSG.DLC.WeaponParts[Selected])
                        CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add(i);
                    CurrentWSG.DLC.WeaponAmmo.Add(CurrentWSG.DLC.WeaponAmmo[Selected]);
                    CurrentWSG.DLC.WeaponQuality.Add(CurrentWSG.DLC.WeaponQuality[Selected]);
                    CurrentWSG.DLC.WeaponLevel.Add(CurrentWSG.DLC.WeaponLevel[Selected]);
                    CurrentWSG.DLC.WeaponEquippedSlot.Add(0);
                    CurrentWSG.DLC.TotalWeapons++;

                    DoDLCWeaponTree();
                }
                else
                {
                    CurrentWSG.WeaponStrings.Add(new List<string>());
                    CurrentWSG.WeaponValues.Add(new List<int>());
                    foreach (string i in CurrentWSG.WeaponStrings[Selected])
                        CurrentWSG.WeaponStrings[CurrentWSG.WeaponStrings.Count - 1].Add(i);
                    foreach (int i in CurrentWSG.WeaponValues[Selected])
                        CurrentWSG.WeaponValues[CurrentWSG.WeaponStrings.Count - 1].Add(i);
                    //CurrentWSG.WeaponValues.Add(CurrentWSG.WeaponValues[Selected]);
                    CurrentWSG.WeaponValues[CurrentWSG.NumberOfWeapons][2] = 0;
                    CurrentWSG.NumberOfWeapons++;
                    WeaponTree.Nodes.Add(WeaponTree.SelectedNode.Copy());
                    //DoWeaponTree();
                }
            }
            catch { MessageBox.Show("Select a weapon to duplicate first."); }
        } // Duplicate Weapon

        private void DeleteWeapons_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = WeaponTree.SelectedIndex;
                WeaponTree.DeselectNode(WeaponTree.SelectedNode, new eTreeAction());
                WeaponTree.Nodes.RemoveAt(Selected);
                CurrentWSG.WeaponStrings.RemoveAt(Selected);
                CurrentWSG.WeaponValues.RemoveAt(Selected);
                CurrentWSG.NumberOfWeapons--;
                TrySelectedNode(WeaponTree, Selected);
                //DoWeaponTree();

            }
            catch { MessageBox.Show("Select a weapon to delete first."); }
        } // Delete Weapon

        private void ExportWeaponsToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog ToFile = new SaveFileDialog();
            ToFile.DefaultExt = "*.txt";
            ToFile.Filter = "Weapon Files(*.txt)|*.txt";
            ToFile.FileName = CurrentPartsGroup.Text + ".txt";
            if (ToFile.ShowDialog() == DialogResult.OK)
            {

                InOutPartsBox.Clear();
                for (int Progress = 0; Progress < 14; Progress++)
                {
                    if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                    InOutPartsBox.AppendText((string)CurrentWeaponParts.Items[Progress]);
                }
                InOutPartsBox.AppendText("\r\n" + RemainingAmmo.Value);
                InOutPartsBox.AppendText("\r\n" + WeaponQuality.Value);
                InOutPartsBox.AppendText("\r\n" + EquippedSlot.SelectedIndex);
                InOutPartsBox.AppendText("\r\n" + WeaponItemGradeSlider.Value);
                InOutPartsBox.AppendText("\r\n");

                System.IO.File.WriteAllLines(ToFile.FileName, InOutPartsBox.Lines);
            }
        } // Export -> to File

        private void ImportFromTextBlockWeapon(string Text)
        {
            InOutPartsBox.Clear();
            InOutPartsBox.Text = Text;

            for (int Progress = 0; Progress < 14; Progress++)
            {
                ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                CurrentWeaponParts.Items[Progress] = InOutPartsBox.Lines[Progress];
            }

            RemainingAmmo.Value = Convert.ToInt32(InOutPartsBox.Lines[14]);
            WeaponQuality.Value = Convert.ToInt32(InOutPartsBox.Lines[15]);
            EquippedSlot.SelectedIndex = Convert.ToInt32(InOutPartsBox.Lines[16]);
            WeaponItemGradeSlider.Value = Convert.ToInt32(InOutPartsBox.Lines[17]);
        }

        private void ImportWeaponFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.txt";
            FromFile.Filter = "Weapon Files(*.txt)|*.txt";

            FromFile.FileName = CurrentPartsGroup.Text + ".txt";

            if (FromFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportFromTextBlockWeapon(System.IO.File.ReadAllText(FromFile.FileName));
                }
                catch
                {
                    MessageBox.Show("Invalid file data.  Reverting to saved values.");
                    WeaponTree_AfterNodeSelect(null, null);  // reloads the saved values from the node
                }
            }
        } // Import -> from File

        private void DeletePartWeapon_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentWeaponParts.Items[CurrentWeaponParts.SelectedIndex] = "None";
            }
            catch { }
        } // Delete Part

        private void InsertWeaponFromClipboard_Click(object sender, EventArgs e) // Insert -> from Clipboard
        {
            try
            {
                InOutPartsBox.Clear();
                InOutPartsBox.Text = Clipboard.GetText();
                InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");
                if (IsDLCWeaponMode)
                {
                    CurrentWSG.DLC.WeaponParts.Add(new List<string>());
                    CurrentWSG.DLC.TotalWeapons++;
                    for (int Progress = 0; Progress < 14; Progress++)
                    {
                        CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add(InOutPartsBox.Lines[Progress]);

                    }
                    CurrentWSG.DLC.WeaponAmmo.Add(Convert.ToInt32(InOutPartsBox.Lines[14]));
                    CurrentWSG.DLC.WeaponQuality.Add(Convert.ToInt32(InOutPartsBox.Lines[15]));
                    CurrentWSG.DLC.WeaponEquippedSlot.Add(Convert.ToInt32(InOutPartsBox.Lines[16]));
                    DoDLCWeaponTree();
                }
                else
                {
                    // Create new lists
                    List<string> WS = new List<string>();
                    List<int> WV = new List<int>();

                    try
                    {

                        for (int Progress = 0; Progress < 14; Progress++)
                        {
                            ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                            WS.Add(InOutPartsBox.Lines[Progress]);
                        }

                        for (int Progress = 0; Progress < 4; Progress++)
                            WV.Add(Convert.ToInt32(InOutPartsBox.Lines[Progress + 14]));
                        WV[2] = 0; // set equipped slot to 0
                    }
                    catch
                    {
                        MessageBox.Show("Invalid file data or unable to read file.");
                        return;
                    }
                    CurrentWSG.WeaponStrings.Add(WS);
                    CurrentWSG.WeaponValues.Add(WV);
                    CurrentWSG.NumberOfWeapons++;

                    Node TempNode = new Node();
                    TempNode.Text = GetName(TitlesXml, CurrentWSG.WeaponStrings, CurrentWSG.NumberOfWeapons - 1, 14, "PartName", "Weapon");
                    WeaponTree.Nodes.Add(TempNode);
                    //DoWeaponTree();
                }
            }
            catch { }
        }

        private void InsertWeaponFromFile_Click(object sender, EventArgs e) // Insert -> from File
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.txt";
            FromFile.Filter = "Weapon Files(*.txt)|*.txt";

            FromFile.FileName = CurrentPartsGroup.Text + ".txt";

            if (FromFile.ShowDialog() == DialogResult.OK)
            {

                InOutPartsBox.Clear();
                InOutPartsBox.Text = System.IO.File.ReadAllText(FromFile.FileName);
                if (IsDLCWeaponMode)
                {
                    CurrentWSG.DLC.WeaponParts.Add(new List<string>());
                    CurrentWSG.DLC.TotalWeapons++;
                    for (int Progress = 0; Progress < 14; Progress++)
                    {
                        CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add(InOutPartsBox.Lines[Progress]);

                    }
                    CurrentWSG.DLC.WeaponAmmo.Add(Convert.ToInt32(InOutPartsBox.Lines[14]));
                    CurrentWSG.DLC.WeaponQuality.Add(Convert.ToInt32(InOutPartsBox.Lines[15]));
                    CurrentWSG.DLC.WeaponEquippedSlot.Add(Convert.ToInt32(InOutPartsBox.Lines[16]));
                    DoDLCWeaponTree();
                }
                else
                {
                    List<string> wpnstrings = new List<string>();
                    List<int> wpnvalues = new List<int>();

                    try
                    {
                        for (int Progress = 0; Progress < 14; Progress++)
                        {
                            ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                            wpnstrings.Add(InOutPartsBox.Lines[Progress]);
                        }
                        for (int Progress = 0; Progress < 4; Progress++)
                            wpnvalues.Add(Convert.ToInt32(InOutPartsBox.Lines[Progress+14]));
                        wpnvalues[2] = 0; // set equipped slot to none
                    }
                    catch
                    {
                        MessageBox.Show("Invalid file data or unable to read file.");
                        return;
                    }

                    CurrentWSG.WeaponStrings.Add(wpnstrings);
                    CurrentWSG.WeaponValues.Add(wpnvalues);
                    CurrentWSG.NumberOfWeapons++;

                    Node TempNode = new Node();
                    TempNode.Text = GetName(TitlesXml, CurrentWSG.WeaponStrings, CurrentWSG.NumberOfWeapons - 1, 14, "PartName", "Weapon");
                    WeaponTree.Nodes.Add(TempNode);
                    //DoWeaponTree();
                }
            }

        }

        private void ExportToLockerWeapon_Click(object sender, EventArgs e)
        {
            List<string> itemparts = new List<string>();
            List<int> itemvalues = new List<int>() { 0, 5, 0, 63 };

            itemvalues[0] = (int)RemainingAmmo.Value;
            itemvalues[1] = WeaponQuality.Value;
            itemvalues[2] = 0;  // Drop the equipped slot
            itemvalues[3] = WeaponItemGradeSlider.Value;

            InOutPartsBox.Clear();
            for (int Progress = 0; Progress < 14; Progress++)
            {
                //if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                //InOutPartsBox.AppendText((string)CurrentWeaponParts.Items[Progress]);
                itemparts.Add((string)CurrentWeaponParts.Items[Progress]);
            }

            string weaponname = GetName(TitlesXml, itemparts, itemparts.Count, "PartName", "Weapon");
            //New Weapon

            List<string> iniListSectionNames = new List<string>();
            iniListSectionNames = XmlLocker.stListSectionNames();
            iniListSectionNames.TrimExcess();

            // Ok all Lockeritems now in a list

            int Occurances = 0;

            // Search the WeaponTree.Nodes[Progress].Text in the locker
            if (iniListSectionNames.Contains(weaponname))
            {
                // rename and try again until in can be added
                do
                {
                    Occurances++;
                } while (iniListSectionNames.Contains(weaponname + " (Copy " + Occurances + ")"));
                //More than one found

                //Add new Item to the xml locker
                XmlLocker.AddItem(weaponname + " (Copy " + Occurances + ")", "Weapon", itemparts, itemvalues);

                Node temp = new Node();
                temp.Text = weaponname + " (Copy " + Occurances + ")";
                temp.Name = weaponname + " (Copy " + Occurances + ")";
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text + " (Copy " + Occurances + ")");
            }
            else
            {
                //only one found
                XmlLocker.AddItem(weaponname, "Weapon", itemparts, itemvalues);

                XmlLocker.Reload(); // Reload the locker next time

                Node temp = new Node();
                temp.Text = weaponname;
                temp.Name = weaponname;
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text);
            }








            /*string tempINI = System.IO.File.ReadAllText(OpenedLocker);
            int Occurances = 0;
            XmlFile Locker = new XmlFile(OpenedLocker);
            for (int Progress = 0; Progress < Locker.ListSectionNames().Length; Progress++)
                if (Locker.ListSectionNames()[Progress] == "New Weapon" || Locker.ListSectionNames()[Progress].Contains("New Weapon (Copy "))
                    Occurances = Occurances + 1;


            if (Occurances > 0)
            {
                //MessageBox.Show("A new weapon already exists.");
                string NewWeaponEntry = "\r\n\r\n[New Weapon (Copy " + Occurances + ")]\r\nType=Weapon\r\nRating=0\r\nDescription=\"Type in a description for the weapon here.\"\r\nPart1=None\r\nPart2=None\r\nPart3=None\r\nPart4=None\r\nPart5=None\r\nPart6=None\r\nPart7=None\r\nPart8=None\r\nPart9=None\r\nPart10=None\r\nPart11=None\r\nPart12=None\r\nPart13=None\r\nPart14=None";
                tempINI = tempINI + NewWeaponEntry;
                System.IO.File.WriteAllText(OpenedLocker, tempINI);
                Node temp = new Node();
                temp.Text = "New Weapon (Copy " + Occurances + ")";
                LockerTree.Nodes.Add(temp);
            }
            else
            {

                string NewWeaponEntry = "\r\n\r\n[New Weapon]\r\nType=Weapon\r\nRating=0\r\nDescription=\"Type in a description for the weapon here.\"\r\nPart1=None\r\nPart2=None\r\nPart3=None\r\nPart4=None\r\nPart5=None\r\nPart6=None\r\nPart7=None\r\nPart8=None\r\nPart9=None\r\nPart10=None\r\nPart11=None\r\nPart12=None\r\nPart13=None\r\nPart14=None";
                tempINI = tempINI + NewWeaponEntry;
                System.IO.File.WriteAllText(OpenedLocker, tempINI);
                Node temp = new Node();
                temp.Text = "New Weapon";
                LockerTree.Nodes.Add(temp);
            }
            LockerTree.SelectedNode = LockerTree.Nodes[LockerTree.Nodes.Count - 1];

            //Insert From Clipboard
            InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");

            NameLocker.Text = "";
            //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
            //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini"); ;

            for (int Progress = 0; Progress < 14; Progress++)
            {
                PartsLocker.Items[Progress] = InOutPartsBox.Lines[Progress];
                if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text == "")
                    NameLocker.Text = GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                else if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text != "")
                    NameLocker.Text = NameLocker.Text + " " + GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                DescriptionLocker.Text = "Type in a description for the item here.";
                RatingLocker.Rating = 0;

            }
            SaveChangesToLocker(); */
        }

        private void CurrentWeaponParts_DoubleClick(object sender, EventArgs e)
        {
            //Interaction.InputBox TempBox = new Interaction.InputBox("Enter a new part", "Manual Edit", (string)CurrentWeaponParts.SelectedItem, 0, 0);
            //string test = Interaction.InputBox("Enter a new part", "Manual Edit", (string)CurrentWeaponParts.SelectedItem, 10, 10);
            string tempManualPart = Interaction.InputBox("Enter a new part", "Manual Edit", (string)CurrentWeaponParts.SelectedItem, 10, 10);
            if (tempManualPart != "")
                CurrentWeaponParts.Items[CurrentWeaponParts.SelectedIndex] = tempManualPart;
        }

        private void ImportWeaponsFromXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog tempImport = new OpenFileDialog();
            tempImport.DefaultExt = "*.xml";
            tempImport.Filter = "WillowTree Locker(*.xml)|*.xml";

            tempImport.FileName = CurrentWSG.CharacterName + "'s Weapons.xml";
            if (tempImport.ShowDialog() == DialogResult.OK)
            {
                //Ini.IniFile ImportWTL = new Ini.IniFile(tempImport.FileName);
                XmlFile ImportWTL = new XmlFile(tempImport.FileName);
                if (IsDLCWeaponMode)
                {

                    for (int Progress = 0; Progress < ImportWTL.stListSectionNames("Weapon").Count; Progress++)
                    {
                        CurrentWSG.DLC.WeaponParts.Add(new List<string>());
                        for (int ProgressStrings = 0; ProgressStrings < 14; ProgressStrings++)
                            CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Weapon")[Progress], "Part" + (ProgressStrings + 1)));

                        CurrentWSG.DLC.WeaponAmmo.Add(0);
                        CurrentWSG.DLC.WeaponQuality.Add(0);
                        CurrentWSG.DLC.WeaponLevel.Add(0);
                        CurrentWSG.DLC.WeaponEquippedSlot.Add(0);
                        CurrentWSG.DLC.TotalWeapons++;
                    }
                    DoDLCWeaponTree();
                }

                else
                {
                    for (int Progress = 0; Progress < ImportWTL.stListSectionNames("Weapon").Count; Progress++)
                    {
                        List<String> wpnstrings = new List<string>();
                        List<int> wpnvalues = new List<int>();
                        try 
                        {
                            for (int ProgressStrings = 0; ProgressStrings < 14; ProgressStrings++)
                            {
                                string part = ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Weapon")[Progress], "Part" + (ProgressStrings + 1));
                                ThrowExceptionIfIntString(part);
                                wpnstrings.Add(part);
                            }

                            wpnvalues.Add(Int32FromString(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Weapon")[Progress], "RemAmmo_Quantity"),0));
                            wpnvalues.Add(Int32FromString(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Weapon")[Progress], "Quality"),0));
                            wpnvalues.Add(0); // set equipped slot to 0
                            wpnvalues.Add(Int32FromString(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Weapon")[Progress], "Level"),0));
                        }
                        catch
                        {
                            MessageBox.Show("Error reading XML file.  Skipping item.");
                            continue;
                        }

                        CurrentWSG.WeaponStrings.Add(wpnstrings);
                        CurrentWSG.WeaponValues.Add(wpnvalues);
                        CurrentWSG.NumberOfWeapons++;
                    }

                    DoWeaponTree();
                }
            }
        }

        private void ExportWeapons_Click(object sender, EventArgs e)
        {
            SaveFileDialog tempExport = new SaveFileDialog();
            tempExport.DefaultExt = "*.xml";
            tempExport.Filter = "WillowTree Locker(*.xml)|*.xml";

            tempExport.FileName = CurrentWSG.CharacterName + "'s Weapons.xml";
            //string ExportText = "";
            if (tempExport.ShowDialog() == DialogResult.OK)
            {
                // Create empty xml file
                XmlTextWriter writer = new XmlTextWriter(tempExport.FileName, new System.Text.ASCIIEncoding());
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartDocument();
                writer.WriteStartElement("INI");
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

                XmlFile LockerSave = new XmlFile(tempExport.FileName);
                List<string> itemparts = new List<string>();
                List<int> itemvalues = new List<int>();

                if (IsDLCWeaponMode)
                    for (int Progress = 0; Progress < CurrentWSG.DLC.WeaponParts.Count; Progress++)
                    {
                        itemparts.Clear();
                        //ExportText = ExportText + "[" + WeaponTree.Nodes[Progress].Text + "]\r\nType=Weapon\r\nRating=0\r\nDescription=Type in a description for the weapon here.\r\n";
                        for (int PartProgress = 0; PartProgress < 14; PartProgress++)
                            itemparts.Add(CurrentWSG.DLC.WeaponParts[Progress][PartProgress]);
                        //ExportText = ExportText + "Part" + (PartProgress + 1) + "=" + CurrentWSG.DLC.WeaponParts[Progress][PartProgress] + "\r\n";

                        itemvalues.Add(CurrentWSG.DLC.WeaponAmmo[Progress]);
                        itemvalues.Add(CurrentWSG.DLC.WeaponQuality[Progress]);
                        itemvalues.Add(CurrentWSG.DLC.WeaponEquippedSlot[Progress]);
                        itemvalues.Add(CurrentWSG.DLC.WeaponLevel[Progress]);

                        LockerSave.AddItem(WeaponTree.Nodes[Progress].Text, "Weapon", itemparts, itemvalues);
                    }
                else
                    for (int Progress = 0; Progress < CurrentWSG.NumberOfWeapons; Progress++)
                    {
                        itemparts.Clear();
                        itemvalues.Clear();
                        for (int PartProgress = 0; PartProgress < 14; PartProgress++)
                            itemparts.Add(CurrentWSG.WeaponStrings[Progress][PartProgress]);

                        for (int PartProgress = 0; PartProgress < 4; PartProgress++)
                            itemvalues.Add(CurrentWSG.WeaponValues[Progress][PartProgress]);

                        LockerSave.AddItem(WeaponTree.Nodes[Progress].Text, "Weapon", itemparts, itemvalues);
                    }
            }
        }

        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< QUESTS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\

        private void QuestTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            Clicked = false;
            try
            {
                SelectedQuestGroup.Text = QuestTree.SelectedNode.Text;
                //if (QuestTree.SelectedNode.Parent.Text == "Playthrough 1 Quests" && QuestTree.SelectedNode.HasChildNodes == false)

                //else if (QuestTree.SelectedNode.Parent.Text == "Playthrough 2 Quests" && QuestTree.SelectedNode.HasChildNodes == false)
                if (QuestTree.SelectedNode.Name == "PT1")
                {

                    QuestString.Text = CurrentWSG.PT1Strings[QuestTree.SelectedNode.Index];
                    if (CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0] > 2)
                        QuestProgress.SelectedIndex = 3;
                    else QuestProgress.SelectedIndex = CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0];
                    NumberOfObjectives.Value = CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 3];
                    Objectives.Items.Clear();
                    XmlFile Quest = new XmlFile(AppDir + "\\Data\\Quests.ini");
                    if (NumberOfObjectives.Value > 0)
                        for (int Progress = 0; Progress < NumberOfObjectives.Value; Progress++)
                            Objectives.Items.Add(Quest.XmlReadValue(QuestString.Text, "Objectives" + Progress));
                    ObjectiveValue.Value = 0;
                    QuestSummary.Text = Quest.XmlReadValue(QuestString.Text, "MissionSummary");
                    QuestDescription.Text = Quest.XmlReadValue(QuestString.Text, "MissionDescription");
                }
                else if (QuestTree.SelectedNode.Name == "PT2")
                {
                    QuestString.Text = CurrentWSG.PT2Strings[QuestTree.SelectedNode.Index];
                    if (CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0] > 2)
                        QuestProgress.SelectedIndex = 3;
                    else QuestProgress.SelectedIndex = CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0];
                    NumberOfObjectives.Value = CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 3];
                    Objectives.Items.Clear();
                    XmlFile Quest = new XmlFile(AppDir + "\\Data\\Quests.ini");
                    if (NumberOfObjectives.Value > 0)
                        for (int Progress = 0; Progress < NumberOfObjectives.Value; Progress++)
                            Objectives.Items.Add(Quest.XmlReadValue(QuestString.Text, "Objectives" + Progress));
                    ObjectiveValue.Value = 0;
                    QuestSummary.Text = Quest.XmlReadValue(QuestString.Text, "MissionSummary");
                    QuestDescription.Text = Quest.XmlReadValue(QuestString.Text, "MissionDescription");
                }
            }
            catch { QuestString.Text = ""; Objectives.Items.Clear(); NumberOfObjectives.Value = 0; ObjectiveValue.Value = 0; QuestProgress.SelectedIndex = 0; }
        }

        private void QuestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedItem = QuestList.SelectedIndex;
            NewQuest.ClosePopup();
            try
            {
                //Ini.IniFile Quests = new Ini.IniFile(AppDir + "\\Data\\Quests.ini");
                XmlFile Quests = new XmlFile(AppDir + "\\Data\\Quests.ini");
                if (QuestTree.SelectedNode.Name == "PT1" || QuestTree.SelectedNode == QuestTree.Nodes[0])
                {
                    int TotalObjectives = 0;
                    CurrentWSG.TotalPT1Quests = CurrentWSG.TotalPT1Quests + 1;
                    ResizeArrayLarger(ref CurrentWSG.PT1Strings, CurrentWSG.TotalPT1Quests);
                    ResizeArrayLarger(ref CurrentWSG.PT1Values, CurrentWSG.TotalPT1Quests, 9);
                    ResizeArrayLarger(ref CurrentWSG.PT1Subfolders, CurrentWSG.TotalPT1Quests, 5);
                    CurrentWSG.PT1Strings[CurrentWSG.TotalPT1Quests - 1] = Quests.stListSectionNames()[SelectedItem];
                    CurrentWSG.PT1Values[CurrentWSG.TotalPT1Quests - 1, 0] = 1;
                    for (int Progress = 0; Progress < 5; Progress++)
                        if (Quests.XmlReadValue(Quests.stListSectionNames()[SelectedItem], "Objectives" + Progress) == "") break;
                        else TotalObjectives = Progress + 1;
                    CurrentWSG.PT1Values[CurrentWSG.TotalPT1Quests - 1, 3] = TotalObjectives;
                    for (int Progress = 0; Progress < 5; Progress++)
                        CurrentWSG.PT1Subfolders[CurrentWSG.TotalPT1Quests - 1, Progress] = "None";

                    DoQuestTree();
                    QuestTree.SelectedNode = QuestTree.Nodes[0].Nodes[CurrentWSG.TotalPT1Quests - 1];
                }
                if (QuestTree.SelectedNode.Name == "PT2" || QuestTree.SelectedNode.Text == "Playthrough 2 Quests")
                {
                    int TotalObjectives = 0;
                    CurrentWSG.TotalPT2Quests = CurrentWSG.TotalPT2Quests + 1;
                    ResizeArrayLarger(ref CurrentWSG.PT2Strings, CurrentWSG.TotalPT2Quests);
                    ResizeArrayLarger(ref CurrentWSG.PT2Values, CurrentWSG.TotalPT2Quests, 9);
                    ResizeArrayLarger(ref CurrentWSG.PT2Subfolders, CurrentWSG.TotalPT2Quests, 5);
                    CurrentWSG.PT2Strings[CurrentWSG.TotalPT2Quests - 1] = Quests.stListSectionNames()[SelectedItem];
                    CurrentWSG.PT2Values[CurrentWSG.TotalPT2Quests - 1, 0] = 1;
                    for (int Progress = 0; Progress < 5; Progress++)
                        if (Quests.XmlReadValue(Quests.stListSectionNames()[SelectedItem], "Objectives[" + Progress + "]") == "") break;
                        else TotalObjectives = Progress + 1;
                    CurrentWSG.PT2Values[CurrentWSG.TotalPT2Quests - 1, 3] = TotalObjectives;
                    for (int Progress = 0; Progress < 5; Progress++)
                        CurrentWSG.PT2Subfolders[CurrentWSG.TotalPT2Quests - 1, Progress] = "None";

                    DoQuestTree();
                    QuestTree.SelectedNode = QuestTree.Nodes[1].Nodes[CurrentWSG.TotalPT2Quests - 1];
                }
            }
            catch { }
        }

        private void Objectives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (QuestTree.SelectedNode.Name == "PT1" && Clicked == true)
                ObjectiveValue.Value = CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, Objectives.SelectedIndex + 4];
            else if (QuestTree.SelectedNode.Name == "PT2" && Clicked == true)
                ObjectiveValue.Value = CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, Objectives.SelectedIndex + 4];
        }

        private void ObjectiveValue_ValueChanged(object sender, EventArgs e)
        {
            if (Objectives.Items.Count > 0)
                if (QuestTree.SelectedNode.Name == "PT1" && Clicked == true)
                    CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, Objectives.SelectedIndex + 4] = (int)ObjectiveValue.Value;
                else if (QuestTree.SelectedNode.Name == "PT2" && Clicked == true)
                    CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, Objectives.SelectedIndex + 4] = (int)ObjectiveValue.Value;
        }

        private void QuestProgress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                XmlFile Quests = new XmlFile(AppDir + "\\Data\\Quests.ini");
                if (QuestTree.SelectedNode.Name == "PT1" && Clicked == true)
                {
                    if (CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0] == 4 && QuestProgress.SelectedIndex < 3)
                    {
                        Objectives.Items.Clear();
                        int TotalObjectives = 0;

                        //string curTxt;
                        for (int Progress = 0; Progress < 5; Progress++)
                        {
                            //curTxt = Quests.IniReadValue(QuestString.Text, "Objectives[" + Progress + "]");
                            if (Quests.XmlReadValue(QuestString.Text, "Objectives" + Progress) == "") break;
                            else TotalObjectives = Progress + 1;
                        }
                        CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 3] = TotalObjectives;
                        for (int Progress = 0; Progress < 5; Progress++)
                            CurrentWSG.PT1Subfolders[QuestTree.SelectedNode.Index, Progress] = "None";
                        NumberOfObjectives.Value = TotalObjectives;
                        if (TotalObjectives > 0)
                            for (int Progress = 0; Progress < TotalObjectives; Progress++)
                                Objectives.Items.Add(Quests.XmlReadValue(QuestString.Text, "Objectives" + Progress));
                        ObjectiveValue.Value = 0;
                        CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0] = QuestProgress.SelectedIndex;
                        CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 3] = TotalObjectives;
                        for (int Progress = 0; Progress < 5; Progress++)
                            CurrentWSG.PT1Subfolders[QuestTree.SelectedNode.Index, Progress] = "None";
                    }
                    else if (CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0] != 4 && QuestProgress.SelectedIndex == 3)
                    {
                        Objectives.Items.Clear();
                        CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0] = 4;
                        CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 3] = 0;
                    }
                    else if (CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0] != 4 && QuestProgress.SelectedIndex < 3)
                        CurrentWSG.PT1Values[QuestTree.SelectedNode.Index, 0] = QuestProgress.SelectedIndex;
                }
                else if (QuestTree.SelectedNode.Name == "PT2" && Clicked == true)
                {
                    if (CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0] == 4 && QuestProgress.SelectedIndex < 3)
                    {
                        Objectives.Items.Clear();
                        int TotalObjectives = 0;
                        for (int Progress = 0; Progress < 5; Progress++)
                            if (Quests.XmlReadValue(QuestString.Text, "Objectives" + Progress) == "") break;
                            else TotalObjectives = Progress + 1;
                        CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 3] = TotalObjectives;
                        for (int Progress = 0; Progress < 5; Progress++)
                            CurrentWSG.PT2Subfolders[QuestTree.SelectedNode.Index, Progress] = "None";
                        NumberOfObjectives.Value = TotalObjectives;
                        if (NumberOfObjectives.Value > 0)
                            for (int Progress = 0; Progress < NumberOfObjectives.Value; Progress++)
                                Objectives.Items.Add(Quests.XmlReadValue(QuestString.Text, "Objectives" + Progress));
                        ObjectiveValue.Value = 0;
                        CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0] = QuestProgress.SelectedIndex;
                        CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 3] = TotalObjectives;
                    }
                    else if (CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0] != 4 && QuestProgress.SelectedIndex == 3)
                    {
                        Objectives.Items.Clear();
                        CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0] = 4;
                        CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 3] = 0;
                        //for (int Progress = 0; Progress < 5; Progress++)
                        //    CurrentWSG.PT2Subfolders[QuestTree.SelectedNode.Index, Progress] = "None";
                    }
                    else if (CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0] != 4 && QuestProgress.SelectedIndex < 3)
                        CurrentWSG.PT2Values[QuestTree.SelectedNode.Index, 0] = QuestProgress.SelectedIndex;

                }
            }
            catch { }
        }

        private void QuestProgress_Click(object sender, EventArgs e)
        {
            Clicked = true;
        }

        private void DeleteQuest_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = QuestTree.SelectedNode.Index; ;
                if (QuestTree.SelectedNode.Name == "PT1")
                {
                    if (QuestTree.SelectedNode.Text != "Fresh Off The Bus" || MultipleIntroStateSaver(1) == true)
                    {
                        Selected = QuestTree.SelectedNode.Index;
                        CurrentWSG.TotalPT1Quests = CurrentWSG.TotalPT1Quests - 1;
                        for (int Position = Selected; Position < CurrentWSG.TotalPT1Quests; Position++)
                        {
                            CurrentWSG.PT1Strings[Position] = CurrentWSG.PT1Strings[Position + 1];
                            for (int Progress = 0; Progress < 4; Progress++)
                                CurrentWSG.PT1Values[Position, Progress] = CurrentWSG.PT1Values[Position + 1, Progress];
                            for (int Progress = 0; Progress < 5; Progress++)
                                CurrentWSG.PT1Subfolders[Position, Progress] = CurrentWSG.PT1Subfolders[Position + 1, Progress];
                        }
                        ResizeArraySmaller(ref CurrentWSG.PT1Strings, CurrentWSG.TotalPT1Quests);
                        ResizeArraySmaller(ref CurrentWSG.PT1Subfolders, CurrentWSG.TotalPT1Quests, 5);
                        ResizeArraySmaller(ref CurrentWSG.PT1Values, CurrentWSG.TotalPT1Quests, 9);
                        DoQuestTree();
                        QuestTree.SelectedNode = QuestTree.Nodes[0].Nodes[Selected];
                    }
                    else if (MultipleIntroStateSaver(1) == false)
                        MessageBox.Show("You must have the default quest.");
                }
                else if (QuestTree.SelectedNode.Name == "PT2")
                {
                    if (QuestTree.SelectedNode.Text != "Fresh Off The Bus" || MultipleIntroStateSaver(2) == true)
                    {
                        Selected = QuestTree.SelectedNode.Index;
                        CurrentWSG.TotalPT2Quests = CurrentWSG.TotalPT2Quests - 1;
                        for (int Position = Selected; Position < CurrentWSG.TotalPT2Quests; Position++)
                        {
                            CurrentWSG.PT2Strings[Position] = CurrentWSG.PT2Strings[Position + 1];
                            for (int Progress = 0; Progress < 4; Progress++)
                                CurrentWSG.PT2Values[Position, Progress] = CurrentWSG.PT2Values[Position + 1, Progress];
                            for (int Progress = 0; Progress < 5; Progress++)
                                CurrentWSG.PT2Subfolders[Position, Progress] = CurrentWSG.PT2Subfolders[Position + 1, Progress];
                        }
                        ResizeArraySmaller(ref CurrentWSG.PT2Strings, CurrentWSG.TotalPT2Quests);
                        ResizeArraySmaller(ref CurrentWSG.PT2Subfolders, CurrentWSG.TotalPT2Quests, 5);
                        ResizeArraySmaller(ref CurrentWSG.PT2Values, CurrentWSG.TotalPT2Quests, 9);

                        DoQuestTree();

                        try
                        {
                            QuestTree.SelectedNode = QuestTree.Nodes[1].Nodes[Selected];
                        }
                        catch { }
                    }
                    else if (MultipleIntroStateSaver(2) == false)
                        MessageBox.Show("You must have the default quest.");
                }
            }
            catch { }
        }


        private void ExportQuests_Click(object sender, EventArgs e)
        {
            try
            {
                if (QuestTree.SelectedNode.Name == "PT2")
                {
                    SaveFileDialog tempExport = new SaveFileDialog();
                    //string ExportText = "";
                    tempExport.DefaultExt = "*.quests";
                    tempExport.Filter = "Quest Data(*.quests)|*.quests";

                    tempExport.FileName = CurrentWSG.CharacterName + "'s PT2 Quests.quests";

                    if (tempExport.ShowDialog() == DialogResult.OK)
                    {
                        // Create empty xml file
                        XmlTextWriter writer = new XmlTextWriter(tempExport.FileName, new System.Text.ASCIIEncoding());
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        writer.WriteStartDocument();
                        writer.WriteStartElement("INI");
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Flush();
                        writer.Close();

                        XmlFile Quests = new XmlFile(tempExport.FileName);
                        List<string> subsectionnames = new List<string>();
                        List<string> subsectionvalues = new List<string>();

                        for (int Progress = 0; Progress < CurrentWSG.TotalPT2Quests; Progress++)
                        {
                            subsectionnames.Clear();
                            subsectionvalues.Clear();

                            subsectionnames.Add("Progress");
                            subsectionnames.Add("DLCValue1");
                            subsectionnames.Add("DLCValue2");
                            subsectionnames.Add("Objectives");

                            for (int i = 0; i < 4; i++)
                                subsectionvalues.Add(CurrentWSG.PT2Values[Progress, i].ToString());

                            //ExportText = ExportText + "[" + CurrentWSG.PT2Strings[Progress] + "]\r\nProgress=" + CurrentWSG.PT2Values[Progress, 0] + "\r\nDLCValue1=" + CurrentWSG.PT2Values[Progress, 1] + "\r\nDLCValue2=" + CurrentWSG.PT2Values[Progress, 2] + "\r\nObjectives=" + CurrentWSG.PT2Values[Progress, 3] + "\r\n";
                            for (int Folders = 0; Folders < CurrentWSG.PT2Values[Progress, 3]; Folders++)
                            {
                                subsectionnames.Add("FolderName" + Folders);
                                subsectionvalues.Add(CurrentWSG.PT2Subfolders[Progress, Folders]);
                                subsectionnames.Add("FolderValue" + Folders);
                                subsectionvalues.Add(CurrentWSG.PT2Values[Progress, Folders + 4].ToString());

                                //ExportText = ExportText + "FolderName" + Folders + "=" + CurrentWSG.PT2Subfolders[Progress, Folders] + "\r\nFolderValue" + Folders + "=" + CurrentWSG.PT2Values[Progress, Folders + 4] + "\r\n";
                            }
                            Quests.AddSection(CurrentWSG.PT2Strings[Progress], subsectionnames, subsectionvalues);
                        }
                    }
                }
                else if (QuestTree.SelectedNode.Name == "PT1")
                {
                    SaveFileDialog tempExport = new SaveFileDialog();
                    //string ExportText = "";
                    tempExport.DefaultExt = "*.quests";
                    tempExport.Filter = "Quest Data(*.quests)|*.quests";

                    tempExport.FileName = CurrentWSG.CharacterName + "'s PT1 Quests.quests";

                    if (tempExport.ShowDialog() == DialogResult.OK)
                    {
                        // Create empty xml file
                        XmlTextWriter writer = new XmlTextWriter(tempExport.FileName, new System.Text.ASCIIEncoding());
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        writer.WriteStartDocument();
                        writer.WriteStartElement("INI");
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Flush();
                        writer.Close();

                        XmlFile Quests = new XmlFile(tempExport.FileName);
                        List<string> subsectionnames = new List<string>();
                        List<string> subsectionvalues = new List<string>();

                        for (int Progress = 0; Progress < CurrentWSG.TotalPT1Quests; Progress++)
                        {
                            subsectionnames.Clear();
                            subsectionvalues.Clear();

                            subsectionnames.Add("Progress");
                            subsectionnames.Add("DLCValue1");
                            subsectionnames.Add("DLCValue2");
                            subsectionnames.Add("Objectives");

                            for (int i = 0; i < 4; i++)
                                subsectionvalues.Add(CurrentWSG.PT1Values[Progress, i].ToString());
                            //ExportText = ExportText + "[" + CurrentWSG.PT1Strings[Progress] + "]\r\nProgress=" + CurrentWSG.PT1Values[Progress, 0] + "\r\nDLCValue1=" + CurrentWSG.PT1Values[Progress, 1] + "\r\nDLCValue2=" + CurrentWSG.PT1Values[Progress, 2] + "\r\nObjectives=" + CurrentWSG.PT1Values[Progress, 3] + "\r\n";

                            for (int Folders = 0; Folders < CurrentWSG.PT1Values[Progress, 3]; Folders++)
                            {
                                subsectionnames.Add("FolderName" + Folders);
                                subsectionvalues.Add(CurrentWSG.PT1Subfolders[Progress, Folders]);
                                subsectionnames.Add("FolderValue" + Folders);
                                subsectionvalues.Add(CurrentWSG.PT1Values[Progress, Folders + 4].ToString());

                                //ExportText = ExportText + "FolderName" + Folders + "=" + CurrentWSG.PT1Subfolders[Progress, Folders] + "\r\nFolderValue" + Folders + "=" + CurrentWSG.PT1Values[Progress, Folders + 4] + "\r\n";
                            }
                            Quests.AddSection(CurrentWSG.PT1Strings[Progress], subsectionnames, subsectionvalues);
                        }
                    }
                }


            }
            catch { MessageBox.Show("Select a playthrough to extract first."); }

        }

        private void ImportQuests_Click(object sender, EventArgs e)
        {
            try
            {
                if (QuestTree.SelectedNode.Name == "PT2")
                {
                    OpenFileDialog tempImport = new OpenFileDialog();
                    tempImport.DefaultExt = "*.quests";
                    tempImport.Filter = "Quest Data(*.quests)|*.quests";

                    tempImport.FileName = CurrentWSG.CharacterName + "'s PT2 Quests.quests";
                    if (tempImport.ShowDialog() == DialogResult.OK)
                    {
                        //Ini.IniFile ImportQuests = new Ini.IniFile(tempImport.FileName);
                        XmlFile ImportQuests = new XmlFile(tempImport.FileName);
                        string[] TempQuestStrings = new string[ImportQuests.stListSectionNames().Count];
                        int[,] TempQuestValues = new int[ImportQuests.stListSectionNames().Count, 10];
                        string[,] TempQuestSubfolders = new string[ImportQuests.stListSectionNames().Count, 7];
                        for (int Progress = 0; Progress < ImportQuests.stListSectionNames().Count; Progress++)
                        {
                            TempQuestStrings[Progress] = ImportQuests.stListSectionNames()[Progress];
                            TempQuestValues[Progress, 0] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "Progress"));
                            TempQuestValues[Progress, 1] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "DLCValue1"));
                            TempQuestValues[Progress, 2] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "DLCValue2"));
                            TempQuestValues[Progress, 3] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "Objectives"));
                            for (int Folders = 0; Folders < TempQuestValues[Progress, 3]; Folders++)
                            {
                                TempQuestValues[Progress, Folders + 4] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "FolderValue" + Folders));
                                TempQuestSubfolders[Progress, Folders] = ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "FolderName" + Folders);
                            }
                        }
                        CurrentWSG.PT2Strings = TempQuestStrings;
                        CurrentWSG.PT2Values = TempQuestValues;
                        CurrentWSG.PT2Subfolders = TempQuestSubfolders;
                        CurrentWSG.TotalPT2Quests = ImportQuests.stListSectionNames().Count;
                        DoQuestTree();
                    }
                }
                else if (QuestTree.SelectedNode.Name == "PT1")
                {
                    OpenFileDialog tempImport = new OpenFileDialog();
                    tempImport.DefaultExt = "*.quests";
                    tempImport.Filter = "Quest Data(*.quests)|*.quests";

                    tempImport.FileName = CurrentWSG.CharacterName + "'s PT1 Quests.quests";
                    if (tempImport.ShowDialog() == DialogResult.OK)
                    {
                        XmlFile ImportQuests = new XmlFile(tempImport.FileName);
                        string[] TempQuestStrings = new string[ImportQuests.stListSectionNames().Count];
                        int[,] TempQuestValues = new int[ImportQuests.stListSectionNames().Count, 10];
                        string[,] TempQuestSubfolders = new string[ImportQuests.stListSectionNames().Count, 7];
                        for (int Progress = 0; Progress < ImportQuests.stListSectionNames().Count; Progress++)
                        {
                            TempQuestStrings[Progress] = ImportQuests.stListSectionNames()[Progress];
                            TempQuestValues[Progress, 0] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "Progress"));
                            TempQuestValues[Progress, 1] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "DLCValue1"));
                            TempQuestValues[Progress, 2] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "DLCValue2"));
                            TempQuestValues[Progress, 3] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "Objectives"));
                            for (int Folders = 0; Folders < TempQuestValues[Progress, 3]; Folders++)
                            {
                                TempQuestValues[Progress, Folders + 4] = Convert.ToInt32(ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "FolderValue" + Folders));
                                TempQuestSubfolders[Progress, Folders] = ImportQuests.XmlReadValue(ImportQuests.stListSectionNames()[Progress], "FolderName" + Folders);
                            }
                        }
                        CurrentWSG.PT1Strings = TempQuestStrings;
                        CurrentWSG.PT1Values = TempQuestValues;
                        CurrentWSG.PT1Subfolders = TempQuestSubfolders;
                        CurrentWSG.TotalPT1Quests = ImportQuests.stListSectionNames().Count;
                        DoQuestTree();
                    }
                }
            }
            catch { MessageBox.Show("Select a playthrough to replace first."); }




        }


        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< ITEMS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\

        private void ItemTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {

            try
            {



                CurrentItemParts.Items.Clear();
                CurrentPartsGroup.Text = ItemTree.SelectedNode.Text;

                if (IsDLCItemMode)
                {
                    for (int build_list = 0; build_list < 9; build_list++)
                    {
                        CurrentItemParts.Items.Add(CurrentWSG.DLC.ItemParts[ItemTree.SelectedNode.Index][build_list]);
                    }

                    Quantity.Value = CurrentWSG.DLC.ItemQuantity[ItemTree.SelectedNode.Index];
                    ItemQuality.Value = CurrentWSG.DLC.ItemQuality[ItemTree.SelectedNode.Index];
                    ItemItemGradeSlider.Value = CurrentWSG.DLC.ItemLevel[ItemTree.SelectedNode.Index];
                    if (CurrentWSG.DLC.ItemEquipped[ItemTree.SelectedNode.Index] == 0) Equipped.SelectedItem = "No";
                    else if (CurrentWSG.DLC.ItemEquipped[ItemTree.SelectedNode.Index] == 1) Equipped.SelectedItem = "Yes";
                }
                else
                {
                    for (int ndcnt = 0; ndcnt < ItemPartsSelector.Nodes.Count; ndcnt++)
                        ItemPartsSelector.Nodes[ndcnt].Style = elementStyle15;

                    for (int build_list = 0; build_list < 9; build_list++)
                    {
                        string curItempart = CurrentWSG.ItemStrings[ItemTree.SelectedNode.Index][build_list];
                        CurrentItemParts.Items.Add(curItempart);


                        // highlight the used partfamilies in the partscategories tree
                        if (curItempart.Contains('.'))
                        {
                            string curItempartclass = curItempart.Substring(0, curItempart.IndexOf('.'));
                            for (int ndcnt = 0; ndcnt < ItemPartsSelector.Nodes.Count; ndcnt++)
                            {
                                if (ItemPartsSelector.Nodes[ndcnt].Name == curItempartclass)
                                {
                                    ItemPartsSelector.Nodes[ndcnt].Style = elementStyle16;
                                    break;
                                }
                            }
                        }
                    }

                    Quantity.Value = CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][0];
                    ItemQuality.Value = CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][1];
                    if (CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][2] == 0) Equipped.SelectedItem = "No";
                    else if (CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][2] == 1) Equipped.SelectedItem = "Yes";
                    ItemItemGradeSlider.Value = CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][3];
                    //string hex = String.Format("{x}", CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][0]);
                    hex01.Text = String.Format("0x{0:x8}", CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][0]);
                    hex02.Text = String.Format("0x{0:x8}", CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][1]);
                    hex03.Text = String.Format("0x{0:x8}", CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][2]);
                    hex04.Text = String.Format("0x{0:x8}", CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][3]);
                }


            }
            catch { }

        }

        private void NewItems_Click(object sender, EventArgs e)
        {
            try
            {

                if (IsDLCItemMode)
                {
                    CurrentWSG.DLC.TotalItems++;
                    CurrentWSG.DLC.ItemParts.Add(new List<string>());
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Item Grade");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Item Type");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Body");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Left Side");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Right Side");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Material");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Manufacturer");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Prefix");
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add("Title");
                    CurrentWSG.DLC.ItemQuantity.Add(1);
                    CurrentWSG.DLC.ItemQuality.Add(0);
                    CurrentWSG.DLC.ItemEquipped.Add(0);


                    DoDLCItemTree();
                }
                else
                {
                    CurrentWSG.NumberOfItems++;
                    CurrentWSG.ItemStrings.Add(new List<string>());
                    CurrentWSG.ItemValues.Add(new List<int>());
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Item Grade");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Item Type");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Body");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Left Side");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Right Side");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Material");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Manufacturer");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Prefix");
                    CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add("Title");
                    CurrentWSG.ItemValues[(CurrentWSG.NumberOfItems - 1)].Add(1);
                    CurrentWSG.ItemValues[(CurrentWSG.NumberOfItems - 1)].Add(0);
                    CurrentWSG.ItemValues[(CurrentWSG.NumberOfItems - 1)].Add(0);
                    CurrentWSG.ItemValues[(CurrentWSG.NumberOfItems - 1)].Add(0);

                    Node TempNode = new Node();
                    TempNode.Text = "New Item";
                    ItemTree.Nodes.Add(TempNode);
                    //DoItemTree();
                }
            }
            catch { }
        }

        private void SaveChangesItem_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = ItemTree.SelectedNode.Index;


                if (IsDLCItemMode)
                {

                    for (int Progress = 0; Progress < 9; Progress++)
                        CurrentWSG.DLC.ItemParts[ItemTree.SelectedNode.Index][Progress] = (string)CurrentItemParts.Items[Progress];
                    CurrentWSG.DLC.ItemQuantity[ItemTree.SelectedNode.Index] = (int)Quantity.Value;
                    CurrentWSG.DLC.ItemQuality[ItemTree.SelectedNode.Index] = (int)ItemQuality.Value;
                    CurrentWSG.DLC.ItemEquipped[ItemTree.SelectedNode.Index] = Equipped.SelectedIndex;
                    CurrentWSG.DLC.ItemLevel[ItemTree.SelectedNode.Index] = ItemItemGradeSlider.Value;
                    //DoDLCItemTree();
                }

                else
                {
                    for (int Progress = 0; Progress < 9; Progress++)
                        CurrentWSG.ItemStrings[ItemTree.SelectedNode.Index][Progress] = (string)CurrentItemParts.Items[Progress];
                    CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][0] = (int)Quantity.Value;
                    CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][1] = (int)ItemQuality.Value;
                    CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][2] = Equipped.SelectedIndex;
                    CurrentWSG.ItemValues[ItemTree.SelectedNode.Index][3] = (int)ItemItemGradeSlider.Value;
                    //DoItemTree();
                }
                //DoItemTree();
                //ItemTree.SelectedNode = ItemTree.Nodes[Selected];
                ItemTree.SelectedNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, ItemTree.SelectedIndex, 14, "PartName", "Item");
            }
            catch { }
        } // Save Changes

        private void ExportItemToClipboard_Click(object sender, EventArgs e)
        {
            InOutPartsBox.Clear();
            for (int Progress = 0; Progress < 9; Progress++)
            {
                if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                InOutPartsBox.AppendText((string)CurrentItemParts.Items[Progress]);
            }
            InOutPartsBox.AppendText("\r\n" + Quantity.Value);
            InOutPartsBox.AppendText("\r\n" + ItemQuality.Value);
            InOutPartsBox.AppendText("\r\n" + Equipped.SelectedIndex);
            InOutPartsBox.AppendText("\r\n" + ItemItemGradeSlider.Value);
            InOutPartsBox.AppendText("\r\n");

            Clipboard.SetText(InOutPartsBox.Text);
        } // Export -> to Clipboard

        private void ImportItemFromClipboard_Click(object sender, EventArgs e)
        {
            InOutPartsBox.Clear();
            InOutPartsBox.Text = Clipboard.GetText();
            InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");

            try
            {
                for (int Progress = 0; Progress < 9; Progress++)
                {
                    ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                    CurrentItemParts.Items[Progress] = InOutPartsBox.Lines[Progress];
                }

                // Exception if these are not int values
                Quantity.Value = Convert.ToInt32(InOutPartsBox.Lines[9]);
                ItemQuality.Value = Convert.ToInt32(InOutPartsBox.Lines[10]);
                Convert.ToInt32(InOutPartsBox.Lines[11]); // validate the input but dont use
                Equipped.SelectedIndex = 0; // set equipped slot to 0
                ItemItemGradeSlider.Value = Convert.ToInt32(InOutPartsBox.Lines[12]);
            }
            catch
            {
                MessageBox.Show("Invalid clipboard data.  Reverting to saved values.");
                ItemTree_AfterNodeSelect(null, null);  // reloads the saved values from the node
            }
        }// Import -> from Clipboard

        private void DuplicateItems_Click(object sender, EventArgs e)
        {
            int Selected = ItemTree.SelectedNode.Index;

            if (IsDLCItemMode)
            {
                CurrentWSG.DLC.ItemParts.Add(new List<string>());
                foreach (string i in CurrentWSG.DLC.ItemParts[Selected])
                    CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add(i);
                CurrentWSG.DLC.ItemQuantity.Add(CurrentWSG.DLC.ItemQuantity[Selected]);
                CurrentWSG.DLC.ItemQuality.Add(CurrentWSG.DLC.ItemQuality[Selected]);
                CurrentWSG.DLC.ItemLevel.Add(CurrentWSG.DLC.ItemLevel[Selected]);
                CurrentWSG.DLC.ItemEquipped.Add(0);
                CurrentWSG.DLC.TotalItems++;
                ItemTree.Nodes.Add(ItemTree.SelectedNode.Copy());
                //DoDLCItemTree();
            }
            else
            {
                CurrentWSG.ItemStrings.Add(new List<string>());
                CurrentWSG.ItemValues.Add(new List<int>());
                foreach (string i in CurrentWSG.ItemStrings[Selected])
                    CurrentWSG.ItemStrings[CurrentWSG.ItemStrings.Count - 1].Add(i);
                foreach (int i in CurrentWSG.ItemValues[Selected])
                    CurrentWSG.ItemValues[CurrentWSG.ItemStrings.Count - 1].Add(i);
                //CurrentWSG.ItemValues.Add(CurrentWSG.ItemValues[Selected]);
                CurrentWSG.ItemValues[CurrentWSG.NumberOfItems][2] = 0;
                CurrentWSG.NumberOfItems++;
                //DoItemTree();
                ItemTree.Nodes.Add(ItemTree.SelectedNode.Copy());
            }

        } // Duplicate Item

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            try
            {

                int Selected = ItemTree.SelectedIndex;

                if (IsDLCItemMode)
                {
                    CurrentWSG.DLC.ItemParts.RemoveAt(Selected);
                    CurrentWSG.DLC.ItemQuantity.RemoveAt(Selected);
                    CurrentWSG.DLC.ItemQuality.RemoveAt(Selected);
                    CurrentWSG.DLC.ItemLevel.RemoveAt(Selected);
                    CurrentWSG.DLC.ItemEquipped.RemoveAt(Selected);
                    CurrentWSG.DLC.TotalItems++;
                    DoItemTree();
                }
                else
                {
                    ItemTree.DeselectNode(ItemTree.SelectedNode, new eTreeAction());
                    ItemTree.Nodes.RemoveAt(Selected);
                    CurrentWSG.ItemStrings.RemoveAt(Selected);
                    CurrentWSG.ItemValues.RemoveAt(Selected);
                    CurrentWSG.NumberOfItems--;
                    TrySelectedNode(ItemTree, Selected);

                    //DoItemTree();
                }

            }
            catch { MessageBox.Show("Select an item to delete first."); }
        } // Delete Item

        private void ExportItemToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog ToFile = new SaveFileDialog();
            ToFile.DefaultExt = "*.txt";
            ToFile.Filter = "Item Files(*.txt)|*.txt";
            ToFile.FileName = CurrentPartsGroup.Text + ".txt";
            if (ToFile.ShowDialog() == DialogResult.OK)
            {

                InOutPartsBox.Clear();

                for (int Progress = 0; Progress < 9; Progress++)
                {
                    if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                    InOutPartsBox.AppendText((string)CurrentItemParts.Items[Progress]);
                }
                InOutPartsBox.AppendText("\r\n" + Quantity.Value);
                InOutPartsBox.AppendText("\r\n" + ItemQuality.Value);
                InOutPartsBox.AppendText("\r\n" + Equipped.SelectedIndex);
                InOutPartsBox.AppendText("\r\n" + ItemItemGradeSlider.Value);
                InOutPartsBox.AppendText("\r\n");

                System.IO.File.WriteAllLines(ToFile.FileName, InOutPartsBox.Lines);
            }
        } // Export -> to File

        private void ThrowExceptionIfIntString(string StringVal)
        {
            int tempValue;
            if (int.TryParse(StringVal, out tempValue))
                throw new System.FormatException();
        }

        private int Int32FromString(string Int32AsString, int ValueIfEmpty)
        {
            if (Int32AsString == "")
                return ValueIfEmpty;
            return Convert.ToInt32(Int32AsString);
        }

        private void ImportItemFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.txt";
            FromFile.Filter = "Item Files(*.txt)|*.txt";

            FromFile.FileName = CurrentPartsGroup.Text + ".txt";

            if (FromFile.ShowDialog() == DialogResult.OK)
            {

                InOutPartsBox.Clear();
                InOutPartsBox.Text = System.IO.File.ReadAllText(FromFile.FileName);

                try
                {
                    for (int Progress = 0; Progress < 9; Progress++)
                    {
                        ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                        CurrentItemParts.Items[Progress] = InOutPartsBox.Lines[Progress];
                    }

                    Quantity.Value = Convert.ToInt32(InOutPartsBox.Lines[9]);
                    ItemQuality.Value = Convert.ToInt32(InOutPartsBox.Lines[10]);
                    Convert.ToInt32(InOutPartsBox.Lines[11]); // validate the input but dont use
                    Equipped.SelectedIndex = 0; // set equipped slot to 0
                    ItemItemGradeSlider.Value = Convert.ToInt32(InOutPartsBox.Lines[12]);
                }
                catch
                {
                    MessageBox.Show("Invalid file data.  Reverting to saved values.");
                    ItemTree_AfterNodeSelect(null, null);  // reloads the saved values from the node
                }
            }
        } // Import -> from File

        private void DeletePartItem_Click(object sender, EventArgs e)
        {
            CurrentItemParts.Items[CurrentItemParts.SelectedIndex] = "None";
        } // Delete Part

        private void InsertItemFromClipboard(object sender, EventArgs e) // Insert -> from Clipboard
        {
            try
            {
                InOutPartsBox.Clear();
                InOutPartsBox.Text = Clipboard.GetText();
                InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");

                if (IsDLCItemMode)
                {
                    CurrentWSG.DLC.ItemParts.Add(new List<string>());
                    CurrentWSG.DLC.TotalItems++;
                    for (int Progress = 0; Progress < 9; Progress++)
                    {
                        CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add(InOutPartsBox.Lines[Progress]);

                    }
                    CurrentWSG.DLC.ItemQuantity.Add(Convert.ToInt32(InOutPartsBox.Lines[9]));
                    CurrentWSG.DLC.ItemQuality.Add(Convert.ToInt32(InOutPartsBox.Lines[10]));
                    CurrentWSG.DLC.ItemEquipped.Add(Convert.ToInt32(InOutPartsBox.Lines[11]));
                    CurrentWSG.DLC.ItemLevel.Add(0);
                    DoDLCItemTree();
                }
                else
                {
                    // Create new lists
                    List<string> IS = new List<string>();
                    List<int> IV = new List<int>();

                    try
                    {
                        for (int Progress = 0; Progress < 9; Progress++)
                        {
                            ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                            IS.Add(InOutPartsBox.Lines[Progress]);
                        }

                        for (int Progress = 0; Progress < 4; Progress++)
                            IV.Add(Convert.ToInt32(InOutPartsBox.Lines[Progress + 9]));
                        IV[2] = 0;  // set equipped slot to 0
                    }
                    catch
                    {
                        MessageBox.Show("Invalid clipboard data.  Item not inserted.");
                        return;
                    }
                        
                    CurrentWSG.ItemStrings.Add(IS);
                    CurrentWSG.ItemValues.Add(IV);
                    CurrentWSG.NumberOfItems++;

                    Node TempNode = new Node();
                    TempNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, CurrentWSG.NumberOfItems - 1, 9, "PartName", "Item");
                    ItemTree.Nodes.Add(TempNode);
                }
            }
            catch { }
        }

        private void InsertItemFromFile_Click(object sender, EventArgs e) // Insert -> from File
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.txt";
            FromFile.Filter = "Item Files(*.txt)|*.txt";

            FromFile.FileName = CurrentPartsGroup.Text + ".txt";

            if (FromFile.ShowDialog() == DialogResult.OK)
            {

                InOutPartsBox.Clear();
                InOutPartsBox.Text = System.IO.File.ReadAllText(FromFile.FileName);

                if (IsDLCItemMode)
                {
                    CurrentWSG.DLC.ItemParts.Add(new List<string>());
                    CurrentWSG.NumberOfItems++;
                    for (int Progress = 0; Progress < 9; Progress++)

                        CurrentWSG.DLC.ItemParts[CurrentWSG.NumberOfItems - 1].Add(InOutPartsBox.Lines[Progress]);
                    CurrentWSG.DLC.ItemQuantity.Add(Convert.ToInt32(InOutPartsBox.Lines[9]));
                    CurrentWSG.DLC.ItemQuality.Add(Convert.ToInt32(InOutPartsBox.Lines[10]));
                    CurrentWSG.DLC.ItemEquipped.Add(Convert.ToInt32(InOutPartsBox.Lines[11]));
                    CurrentWSG.DLC.ItemLevel.Add(0);
                    Node TempNode = new Node();
                    TempNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, CurrentWSG.NumberOfItems - 1, 9, "PartName", "Item");
                    ItemTree.Nodes.Add(TempNode);
                    //DoDLCItemTree();
                }
                else
                {
                    List<string> itemstrings = new List<string>();
                    List<int> itemvalues = new List<int>();

                    try
                    {
                        for (int Progress = 0; Progress < 9; Progress++)
                        {
                            ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                            itemstrings.Add(InOutPartsBox.Lines[Progress]);
                        }

                        for (int Progress = 0; Progress < 4; Progress++)
                            itemvalues.Add(Convert.ToInt32(InOutPartsBox.Lines[Progress + 9]));
                        itemvalues[2] = 0; // set equipped slot to 0
                    }
                    catch
                    {
                        MessageBox.Show("Invalid file data.  Item not inserted.");
                        return;
                    }
   
                    CurrentWSG.ItemStrings.Add(itemstrings);
                    CurrentWSG.ItemValues.Add(itemvalues);
                    CurrentWSG.NumberOfItems++;

                    Node TempNode = new Node();
                    TempNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, CurrentWSG.NumberOfItems - 1, 9, "PartName", "Item");
                    ItemTree.Nodes.Add(TempNode);
                }
            }
        }

        private void ExportToLockerItem_Click(object sender, EventArgs e)
        {
            List<string> itemparts = new List<string>();
            List<int> itemvalues = new List<int>() { 0, 5, 0, 63 };

            itemvalues[0] = (int)Quantity.Value;
            itemvalues[1] = ItemQuality.Value;
            itemvalues[2] = 0;  // Override equipped slot
            itemvalues[3] = ItemItemGradeSlider.Value;

            InOutPartsBox.Clear();
            for (int Progress = 0; Progress < 9; Progress++)
            {
                //if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                //InOutPartsBox.AppendText((string)CurrentItemParts.Items[Progress]);
                itemparts.Add((string)CurrentItemParts.Items[Progress]);
            }
            string itemname = GetName(TitlesXml, itemparts, itemparts.Count, "PartName", "Item");
            //New Item

            List<string> iniListSectionNames = new List<string>();
            iniListSectionNames = XmlLocker.stListSectionNames();
            iniListSectionNames.TrimExcess();

            int Occurances = 0;

            if (iniListSectionNames.Contains(itemname))
            {
                // rename and try again until in can be added
                do
                {
                    Occurances++;
                } while (iniListSectionNames.Contains(itemname + " (Copy " + Occurances + ")"));
                //More than one found

                //Add new Item to the xml locker
                XmlLocker.AddItem(itemname + " (Copy " + Occurances + ")", "Weapon", itemparts, itemvalues);

                Node temp = new Node();
                temp.Text = itemname + " (Copy " + Occurances + ")";
                temp.Name = itemname + " (Copy " + Occurances + ")";
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text + " (Copy " + Occurances + ")");
            }
            else
            {
                //only one found
                XmlLocker.AddItem(itemname, "Item", itemparts, itemvalues);

                XmlLocker.Reload(); // Reload the locker next time

                Node temp = new Node();
                temp.Text = itemname;
                temp.Name = itemname;
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text);
            }


            /*int Occurances = 0;
            Ini.IniFile Locker = new Ini.IniFile(OpenedLocker);
            for (int Progress = 0; Progress < Locker.ListSectionNames().Length; Progress++)
                if (Locker.ListSectionNames()[Progress] == "New Item" || Locker.ListSectionNames()[Progress].Contains("New Item (Copy "))
                    Occurances++;


            if (Occurances > 0)
            {
                //MessageBox.Show("A new Item already exists.");
                string NewItemEntry = "\r\n\r\n[New Item (Copy " + Occurances + ")]\r\nType=Item\r\nRating=0\r\nDescription=\"Type in a description for the Item here.\"\r\nPart1=None\r\nPart2=None\r\nPart3=None\r\nPart4=None\r\nPart5=None\r\nPart6=None\r\nPart7=None\r\nPart8=None\r\nPart9=None\r\nPart10=\r\nPart11=\r\nPart12=\r\nPart13=\r\nPart14=";
                tempINI = tempINI + NewItemEntry;
                System.IO.File.WriteAllText(OpenedLocker, tempINI);
                Node temp = new Node();
                temp.Text = "New Item (Copy " + Occurances + ")";
                LockerTree.Nodes.Add(temp);
            }
            else
            {

                string NewItemEntry = "\r\n\r\n[New Item]\r\nType=Item\r\nRating=0\r\nDescription=\"Type in a description for the Item here.\"\r\nPart1=None\r\nPart2=None\r\nPart3=None\r\nPart4=None\r\nPart5=None\r\nPart6=None\r\nPart7=None\r\nPart8=None\r\nPart9=None\r\nPart10=\r\nPart11=\r\nPart12=\r\nPart13=\r\nPart14=";
                tempINI = tempINI + NewItemEntry;
                System.IO.File.WriteAllText(OpenedLocker, tempINI);
                Node temp = new Node();
                temp.Text = "New Item";
                LockerTree.Nodes.Add(temp);
            }
            LockerTree.SelectedNode = LockerTree.Nodes[LockerTree.Nodes.Count - 1];

            //Insert From Clipboard
            InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");

            NameLocker.Text = "";
            //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
            //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini");

            for (int Progress = 0; Progress < 9; Progress++)
            {
                PartsLocker.Items[Progress] = InOutPartsBox.Lines[Progress];
                if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text == "")
                    NameLocker.Text = GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                else if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text != "")
                    NameLocker.Text = NameLocker.Text + " " + GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                DescriptionLocker.Text = "Type in a description for the item here.";
                RatingLocker.Rating = 0;

            }
            SaveChangesToLocker();*/
        }

        private void ItemPartCategories_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (CurrentItemParts.SelectedItem != null && ItemPartsSelector.SelectedNode.HasChildNodes == false)
            {
                CurrentItemParts.Items[CurrentItemParts.SelectedIndex] = ItemPartsSelector.SelectedNode.Parent.Text + "." + ItemPartsSelector.SelectedNode.Text;
            }
        }



        private void ExportItems_Click(object sender, EventArgs e)
        {
            SaveFileDialog tempExport = new SaveFileDialog();
            tempExport.DefaultExt = "*.xml";
            tempExport.Filter = "WillowTree Locker(*.xml)|*.xml";

            tempExport.FileName = CurrentWSG.CharacterName + "'s Items.xml";
            //string ExportText = "";
            if (tempExport.ShowDialog() == DialogResult.OK)
            {
                // Create empty xml file
                XmlTextWriter writer = new XmlTextWriter(tempExport.FileName, new System.Text.ASCIIEncoding());
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartDocument();
                writer.WriteStartElement("INI");
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

                XmlFile LockerSave = new XmlFile(tempExport.FileName);
                List<string> itemparts = new List<string>();
                List<int> itemvalues = new List<int>();

                if (IsDLCItemMode)
                {
                    for (int Progress = 0; Progress < CurrentWSG.DLC.ItemParts.Count; Progress++)
                    {
                        itemparts.Clear();
                        for (int PartProgress = 0; PartProgress < 9; PartProgress++)
                            itemparts.Add(CurrentWSG.DLC.ItemParts[Progress][PartProgress]);

                        itemvalues.Add(CurrentWSG.DLC.WeaponAmmo[Progress]);
                        itemvalues.Add(CurrentWSG.DLC.WeaponQuality[Progress]);
                        itemvalues.Add(CurrentWSG.DLC.WeaponEquippedSlot[Progress]);
                        itemvalues.Add(CurrentWSG.DLC.WeaponLevel[Progress]);

                        LockerSave.AddItem(ItemTree.Nodes[Progress].Text, "Item", itemparts, itemvalues);
                    }
                }
                else
                {
                    for (int Progress = 0; Progress < CurrentWSG.NumberOfItems; Progress++)
                    {
                        itemparts.Clear();
                        itemvalues.Clear();
                        for (int PartProgress = 0; PartProgress < 9; PartProgress++)
                            itemparts.Add(CurrentWSG.ItemStrings[Progress][PartProgress]);

                        for (int PartProgress = 0; PartProgress < 4; PartProgress++)
                            itemvalues.Add(CurrentWSG.ItemValues[Progress][PartProgress]);

                        LockerSave.AddItem(ItemTree.Nodes[Progress].Text, "Item", itemparts, itemvalues);
                    }
                }
            }
        }

        private void CurrentItemParts_DoubleClick(object sender, EventArgs e)
        {
            string tempManualPart = Interaction.InputBox("Enter a new part", "Manual Edit", (string)CurrentItemParts.SelectedItem, 10, 10);
            if (tempManualPart != "")
                CurrentItemParts.Items[CurrentItemParts.SelectedIndex] = tempManualPart;

        }

        private void InsertItemsFromXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog tempImport = new OpenFileDialog();
            tempImport.DefaultExt = "*.xml";
            tempImport.Filter = "WillowTree Locker(*.xml)|*.xml";

            tempImport.FileName = CurrentWSG.CharacterName + "'s Items.xml";
            if (tempImport.ShowDialog() == DialogResult.OK)
            {
                //Ini.IniFile ImportWTL = new Ini.IniFile(tempImport.FileName);
                XmlFile ImportWTL = new XmlFile(tempImport.FileName);
                if (IsDLCItemMode)
                {

                    for (int Progress = 0; Progress < ImportWTL.stListSectionNames("Item").Count; Progress++)
                    {
                        CurrentWSG.DLC.ItemParts.Add(new List<string>());

                        for (int ProgressStrings = 0; ProgressStrings < 9; ProgressStrings++)
                            CurrentWSG.DLC.ItemParts[CurrentWSG.DLC.ItemParts.Count - 1].Add(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Item")[Progress], "Part" + (ProgressStrings + 1)));

                        CurrentWSG.DLC.ItemQuantity.Add(1);
                        CurrentWSG.DLC.ItemQuality.Add(0);
                        CurrentWSG.DLC.ItemLevel.Add(0);
                        CurrentWSG.DLC.ItemEquipped.Add(0);
                        CurrentWSG.DLC.TotalItems++;
                    }
                    DoDLCItemTree();
                }

                else
                {
                    for (int Progress = 0; Progress < ImportWTL.stListSectionNames("Item").Count; Progress++)
                    {
                        List<string> itemstrings = new List<string>();
                        List<int> itemvalues = new List<int>();

                        try 
                        {
                            for (int ProgressStrings = 0; ProgressStrings < 9; ProgressStrings++)
                            {
                                string part = ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Item")[Progress], "Part" + (ProgressStrings + 1));
                                ThrowExceptionIfIntString(part);
                                itemstrings.Add(part);
                            }

                            itemvalues.Add(Int32FromString(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Item")[Progress], "RemAmmo_Quantity"),0));
                            itemvalues.Add(Int32FromString(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Item")[Progress], "Quality"),0));
                            itemvalues.Add(0); // set equipped slot to 0
                            itemvalues.Add(Int32FromString(ImportWTL.XmlReadValue(ImportWTL.stListSectionNames("Item")[Progress], "Level"),0));
                        }
                        catch
                        {
                            MessageBox.Show("Error reading XML file.  Some items were not read.");
                            break;
                        }

                        CurrentWSG.ItemStrings.Add(itemstrings);
                        CurrentWSG.ItemValues.Add(itemvalues);
                        CurrentWSG.NumberOfItems++;
                    }
                    DoItemTree();
                }
            }
        }

        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< ECHOS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\

        private void DeleteEcho_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = EchoTree.SelectedNode.Index; ;
                if (EchoTree.SelectedNode.Name == "PT1")
                {
                    Selected = EchoTree.SelectedNode.Index;
                    CurrentWSG.NumberOfEchos = CurrentWSG.NumberOfEchos - 1;
                    for (int Position = Selected; Position < CurrentWSG.NumberOfEchos; Position++)
                    {
                        CurrentWSG.EchoStrings[Position] = CurrentWSG.EchoStrings[Position + 1];
                        for (int Progress = 0; Progress < 2; Progress++)
                            CurrentWSG.EchoValues[Position, Progress] = CurrentWSG.EchoValues[Position + 1, Progress];

                    }
                    ResizeArraySmaller(ref CurrentWSG.EchoStrings, CurrentWSG.NumberOfEchos);
                    ResizeArraySmaller(ref CurrentWSG.EchoValues, CurrentWSG.NumberOfEchos, 2);
                    DoEchoTree();
                    EchoTree.SelectedNode = EchoTree.Nodes[0].Nodes[Selected];
                }
                else if (EchoTree.SelectedNode.Name == "PT2")
                {
                    Selected = EchoTree.SelectedNode.Index;
                    CurrentWSG.NumberOfEchosPT2 = CurrentWSG.NumberOfEchosPT2 - 1;
                    for (int Position = Selected; Position < CurrentWSG.NumberOfEchosPT2; Position++)
                    {
                        CurrentWSG.EchoStringsPT2[Position] = CurrentWSG.EchoStringsPT2[Position + 1];
                        for (int Progress = 0; Progress < 2; Progress++)
                            CurrentWSG.EchoValuesPT2[Position, Progress] = CurrentWSG.EchoValuesPT2[Position + 1, Progress];

                    }
                    ResizeArraySmaller(ref CurrentWSG.EchoStringsPT2, CurrentWSG.NumberOfEchosPT2);
                    ResizeArraySmaller(ref CurrentWSG.EchoValuesPT2, CurrentWSG.NumberOfEchosPT2, 2);

                    DoEchoTree();

                    try
                    {
                        EchoTree.SelectedNode = EchoTree.Nodes[1].Nodes[Selected];
                    }
                    catch { }

                }
            }
            catch { }
        }
        private void EchoList_Click(object sender, EventArgs e)
        {

            XmlFile Echoes = new XmlFile(AppDir + "\\Data\\Echos.ini");
            if (EchoTree.SelectedNode.Name == "PT1" || EchoTree.SelectedNode.Text == "Playthrough 1 Echo Logs")
            {

                if (CheckIfNull(CurrentWSG.EchoStrings) == true)
                {
                    CurrentWSG.EchoStrings = new string[1];
                    CurrentWSG.EchoStrings[0] = Echoes.stListSectionNames()[EchoList.SelectedIndex];
                    CurrentWSG.EchoValues = new int[1, 2];
                    CurrentWSG.NumberOfEchos = 1;
                }
                else
                {
                    CurrentWSG.NumberOfEchos = CurrentWSG.NumberOfEchos + 1;
                    ResizeArrayLarger(ref CurrentWSG.EchoStrings, CurrentWSG.NumberOfEchos);
                    ResizeArrayLarger(ref CurrentWSG.EchoValues, CurrentWSG.NumberOfEchos, 2);
                    CurrentWSG.EchoStrings[CurrentWSG.NumberOfEchos - 1] = Echoes.stListSectionNames()[EchoList.SelectedIndex];

                }
                DoEchoTree();
                EchoTree.Nodes[0].Expand();
                EchoTree.SelectedNode = EchoTree.Nodes[0];
            }
            else if (EchoTree.SelectedNode.Name == "PT2" || EchoTree.SelectedNode.Text == "Playthrough 2 Echo Logs")
            {

                if (CheckIfNull(CurrentWSG.EchoStringsPT2) == true)
                {
                    if (CurrentWSG.TotalPT2Quests > 1)
                    {
                        CurrentWSG.EchoStringsPT2 = new string[1];
                        CurrentWSG.EchoStringsPT2[0] = Echoes.stListSectionNames()[EchoList.SelectedIndex];
                        CurrentWSG.EchoValuesPT2 = new int[1, 2];
                        CurrentWSG.NumberOfEchosPT2 = 1;
                    }
                    else
                        MessageBox.Show("You must have more than just the default quest.");
                }
                else
                {
                    CurrentWSG.NumberOfEchosPT2 = CurrentWSG.NumberOfEchosPT2 + 1;
                    ResizeArrayLarger(ref CurrentWSG.EchoStringsPT2, CurrentWSG.NumberOfEchosPT2);
                    ResizeArrayLarger(ref CurrentWSG.EchoValuesPT2, CurrentWSG.NumberOfEchosPT2, 2);
                    CurrentWSG.EchoStringsPT2[CurrentWSG.NumberOfEchosPT2 - 1] = Echoes.stListSectionNames()[EchoList.SelectedIndex];

                }
                DoEchoTree();
                EchoTree.Nodes[1].Expand();
                EchoTree.SelectedNode = EchoTree.Nodes[1];
            }
            //try
            //{ }
            //catch { MessageBox.Show("Could not add new echo log"); }
        }

        private void ExportEchoes_Click(object sender, EventArgs e)
        {
            try
            {
                if (EchoTree.SelectedNode.Name == "PT2" || EchoTree.SelectedNode.Text == "Playthrough 2 Echo Logs")
                {
                    SaveFileDialog tempExport = new SaveFileDialog();
                    //string ExportText = "";
                    tempExport.DefaultExt = "*.echologs";
                    tempExport.Filter = "Echo Logs(*.echologs)|*.echologs";

                    tempExport.FileName = CurrentWSG.CharacterName + "'s PT2 Echo Logs.echologs";

                    if (tempExport.ShowDialog() == DialogResult.OK)
                    {
                        // Create empty xml file
                        XmlTextWriter writer = new XmlTextWriter(tempExport.FileName, new System.Text.ASCIIEncoding());
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        writer.WriteStartDocument();
                        writer.WriteStartElement("INI");
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Flush();
                        writer.Close();

                        XmlFile Echos = new XmlFile(tempExport.FileName);
                        List<string> subsectionnames = new List<string>();
                        List<string> subsectionvalues = new List<string>();

                        for (int Progress = 0; Progress < CurrentWSG.NumberOfEchosPT2; Progress++)
                        {
                            subsectionnames.Clear();
                            subsectionvalues.Clear();
                            subsectionnames.Add("DLCValue1");
                            subsectionnames.Add("DLCValue2");
                            subsectionvalues.Add(CurrentWSG.EchoValuesPT2[Progress, 0].ToString());
                            subsectionvalues.Add(CurrentWSG.EchoValuesPT2[Progress, 1].ToString());

                            //ExportText = ExportText + "[" + CurrentWSG.EchoStringsPT2[Progress] + "]" + "\r\nDLCValue1=" + CurrentWSG.EchoValuesPT2[Progress, 0] + "\r\nDLCValue2=" + CurrentWSG.EchoValuesPT2[Progress, 1] + "\r\n";
                            Echos.AddSection(CurrentWSG.EchoStringsPT2[Progress], subsectionnames, subsectionvalues);
                        }
                        //File.WriteAllText(tempExport.FileName + "s", ExportText);
                    }
                }
                else if (EchoTree.SelectedNode.Name == "PT1" || EchoTree.SelectedNode.Text == "Playthrough 1 Echo Logs")
                {
                    SaveFileDialog tempExport = new SaveFileDialog();
                    //string ExportText = "";
                    tempExport.DefaultExt = "*.echologs";
                    tempExport.Filter = "Echo Logs(*.echologs)|*.echologs";

                    tempExport.FileName = CurrentWSG.CharacterName + "'s PT1 Echo Logs.echologs";

                    if (tempExport.ShowDialog() == DialogResult.OK)
                    {
                        // Create empty xml file
                        XmlTextWriter writer = new XmlTextWriter(tempExport.FileName, new System.Text.ASCIIEncoding());
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        writer.WriteStartDocument();
                        writer.WriteStartElement("INI");
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Flush();
                        writer.Close();

                        XmlFile Echos = new XmlFile(tempExport.FileName);
                        List<string> subsectionnames = new List<string>();
                        List<string> subsectionvalues = new List<string>();

                        for (int Progress = 0; Progress < CurrentWSG.NumberOfEchos; Progress++)
                        {
                            subsectionnames.Clear();
                            subsectionvalues.Clear();
                            subsectionnames.Add("DLCValue1");
                            subsectionnames.Add("DLCValue2");
                            subsectionvalues.Add(CurrentWSG.EchoValues[Progress, 0].ToString());
                            subsectionvalues.Add(CurrentWSG.EchoValues[Progress, 1].ToString());

                            //ExportText = ExportText + "[" + CurrentWSG.EchoStrings[Progress] + "]" + "\r\nDLCValue1=" + CurrentWSG.EchoValues[Progress, 0] + "\r\nDLCValue2=" + CurrentWSG.EchoValues[Progress, 1] + "\r\n";
                            Echos.AddSection(CurrentWSG.EchoStrings[Progress], subsectionnames, subsectionvalues);

                        }
                        //File.WriteAllText(tempExport.FileName + "s", ExportText);
                    }

                }
            }
            catch { MessageBox.Show("Select a playthrough to export first."); }
        }

        private void ImportEchoes_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (EchoTree.SelectedNode == null)
            {
                MessageBox.Show("Select a playthrough to import first.");
                return;
            }
            OpenFileDialog tempImport = new OpenFileDialog();
            if (EchoTree.SelectedNode.Name == "PT2" || EchoTree.SelectedNode.Text == "Playthrough 2 Echo Logs")
            {

                tempImport.DefaultExt = "*.echologs";
                tempImport.Filter = "Echo Logs(*.echologs)|*.echologs";

                tempImport.FileName = CurrentWSG.CharacterName + "'s PT2 Echo Logs.echologs";
                if (tempImport.ShowDialog() == DialogResult.OK)
                {
                    if (EchoTree.SelectedNode.Name == "PT2" || EchoTree.SelectedNode.Text == "Playthrough 2 Echo Logs")
                    {
                        //Ini.IniFile ImportLogs = new Ini.IniFile(tempImport.FileName);
                        XmlFile ImportLogs = new XmlFile(tempImport.FileName);
                        string[] TempEchoStrings = new string[ImportLogs.stListSectionNames().Count];
                        int[,] TempEchoValues = new int[ImportLogs.stListSectionNames().Count, 10];
                        for (int Progress = 0; Progress < ImportLogs.stListSectionNames().Count; Progress++)
                        {
                            TempEchoStrings[Progress] = ImportLogs.stListSectionNames()[Progress];
                            TempEchoValues[Progress, 0] = Convert.ToInt32(ImportLogs.XmlReadValue(ImportLogs.stListSectionNames()[Progress], "DLCValue1"));
                            TempEchoValues[Progress, 1] = Convert.ToInt32(ImportLogs.XmlReadValue(ImportLogs.stListSectionNames()[Progress], "DLCValue2"));


                        }
                        CurrentWSG.EchoStringsPT2 = TempEchoStrings;
                        CurrentWSG.EchoValuesPT2 = TempEchoValues;
                        CurrentWSG.NumberOfEchosPT2 = ImportLogs.stListSectionNames().Count;
                        DoEchoTree();
                    }
                }
            }
            else if (EchoTree.SelectedNode.Name == "PT1" || EchoTree.SelectedNode.Text == "Playthrough 1 Echo Logs")
            {
                tempImport.DefaultExt = "*.echologs";
                tempImport.Filter = "Echo Logs(*.echologs)|*.echologs";

                tempImport.FileName = CurrentWSG.CharacterName + "'s PT1 Echo Logs.echologs";



                if (tempImport.ShowDialog() == DialogResult.OK)
                {
                    //Ini.IniFile ImportLogs = new Ini.IniFile(tempImport.FileName);
                    XmlFile ImportLogs = new XmlFile(tempImport.FileName);
                    string[] TempEchoStrings = new string[ImportLogs.stListSectionNames().Count];
                    int[,] TempEchoValues = new int[ImportLogs.stListSectionNames().Count, 10];
                    for (int Progress = 0; Progress < ImportLogs.stListSectionNames().Count; Progress++)
                    {
                        TempEchoStrings[Progress] = ImportLogs.stListSectionNames()[Progress];
                        TempEchoValues[Progress, 0] = Convert.ToInt32(ImportLogs.XmlReadValue(ImportLogs.stListSectionNames()[Progress], "DLCValue1"));
                        TempEchoValues[Progress, 1] = Convert.ToInt32(ImportLogs.XmlReadValue(ImportLogs.stListSectionNames()[Progress], "DLCValue2"));


                    }
                    CurrentWSG.EchoStrings = TempEchoStrings;
                    CurrentWSG.EchoValues = TempEchoValues;
                    CurrentWSG.NumberOfEchos = ImportLogs.stListSectionNames().Count;
                    DoEchoTree();
                }

            }


            try
            {
            }
            catch { MessageBox.Show("Select a playthrough to import first."); }


        }

        private void EchoDLCValue1_ValueChanged(object sender, EventArgs e)
        {
            if (EchoTree.SelectedNode.Name == "PT2")
                CurrentWSG.EchoValuesPT2[EchoTree.SelectedIndex, 0] = (int)EchoDLCValue1.Value;
            else if (EchoTree.SelectedNode.Name == "PT1")
                CurrentWSG.EchoValues[EchoTree.SelectedIndex, 0] = (int)EchoDLCValue1.Value;
        }

        private void DLCValue2_ValueChanged(object sender, EventArgs e)
        {
            if (EchoTree.SelectedNode.Name == "PT2")
                CurrentWSG.EchoValuesPT2[EchoTree.SelectedIndex, 1] = (int)EchoDLCValue2.Value;
            else if (EchoTree.SelectedNode.Name == "PT1")
                CurrentWSG.EchoValues[EchoTree.SelectedIndex, 1] = (int)EchoDLCValue2.Value;

        }

        private void EchoTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            try
            {
                if (EchoTree.SelectedNode.Name == "PT2")
                {
                    EchoDLCValue1.Value = CurrentWSG.EchoValuesPT2[EchoTree.SelectedIndex, 0];
                    EchoDLCValue2.Value = CurrentWSG.EchoValuesPT2[EchoTree.SelectedIndex, 1];
                }
                else if (EchoTree.SelectedNode.Name == "PT1")
                {
                    EchoDLCValue1.Value = CurrentWSG.EchoValues[EchoTree.SelectedIndex, 0];
                    EchoDLCValue2.Value = CurrentWSG.EchoValues[EchoTree.SelectedIndex, 1];
                }
            }
            catch { }
        }


        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< GENERAL/LOCATIONS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\


        private void DeleteLocation_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = LocationTree.SelectedNode.Index;
                CurrentWSG.TotalLocations = CurrentWSG.TotalLocations - 1;
                for (int Position = Selected; Position < CurrentWSG.TotalLocations; Position++)
                {
                    CurrentWSG.LocationStrings[Position] = CurrentWSG.LocationStrings[Position + 1];
                }
                ResizeArraySmaller(ref CurrentWSG.LocationStrings, CurrentWSG.TotalLocations);
                DoLocationTree();

                LocationTree.SelectedIndex = Selected;
            }
            catch { }
        }

        private void LocationsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                XmlFile Locations = new XmlFile(AppDir + "\\Data\\Locations.ini");
                int SelectedItem = LocationsList.SelectedIndex;
                CurrentWSG.TotalLocations = CurrentWSG.TotalLocations + 1;
                ResizeArrayLarger(ref CurrentWSG.LocationStrings, CurrentWSG.TotalLocations);
                CurrentWSG.LocationStrings[CurrentWSG.TotalLocations - 1] = Locations.stListSectionNames()[SelectedItem];
                DoLocationTree();


            }
            catch { }
        }
        private void Level_ValueChanged(object sender, EventArgs e)
        {
            if (Level.Value > 0 && Level.Value < 70)
            {
                Experience.Minimum = XPChart[(int)Level.Value];

            }
            else
            {
                Experience.Minimum = 0;
            }
            DoWindowTitle();
        }

        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< SKILLS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\

        private void SkillLevel_ValueChanged(object sender, EventArgs e)
        {
            try
            { CurrentWSG.LevelOfSkills[SkillTree.SelectedIndex] = (int)SkillLevel.Value; }
            catch { }
        }

        private void SkillExp_ValueChanged(object sender, EventArgs e)
        {
            try
            { CurrentWSG.ExpOfSkills[SkillTree.SelectedIndex] = (int)SkillExp.Value; }
            catch { }
        }

        private void SkillActive_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (SkillActive.SelectedIndex == 1)
                    CurrentWSG.InUse[SkillTree.SelectedIndex] = 1;
                else
                    CurrentWSG.InUse[SkillTree.SelectedIndex] = -1;
            }
            catch { }
        }

        private void DeleteSkill_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = SkillTree.SelectedNode.Index;
                CurrentWSG.NumberOfSkills = CurrentWSG.NumberOfSkills - 1;
                for (int Position = Selected; Position < CurrentWSG.NumberOfSkills; Position++)
                {
                    CurrentWSG.SkillNames[Position] = CurrentWSG.SkillNames[Position + 1];
                    CurrentWSG.InUse[Position] = CurrentWSG.InUse[Position + 1];
                    CurrentWSG.ExpOfSkills[Position] = CurrentWSG.ExpOfSkills[Position + 1];
                    CurrentWSG.LevelOfSkills[Position] = CurrentWSG.LevelOfSkills[Position + 1];

                }
                ResizeArraySmaller(ref CurrentWSG.SkillNames, CurrentWSG.NumberOfSkills);
                ResizeArraySmaller(ref CurrentWSG.InUse, CurrentWSG.NumberOfSkills);
                ResizeArraySmaller(ref CurrentWSG.ExpOfSkills, CurrentWSG.NumberOfSkills);
                ResizeArraySmaller(ref CurrentWSG.LevelOfSkills, CurrentWSG.NumberOfSkills);
                DoSkillTree();

                SkillTree.SelectedIndex = Selected;
            }
            catch { }
        }

        private void ExportSkills_Click(object sender, EventArgs e)
        {
            SaveFileDialog tempExport = new SaveFileDialog();
            //string ExportText = "";
            tempExport.DefaultExt = "*.skills";
            tempExport.Filter = "Skills Data(*.skills)|*.skills";

            tempExport.FileName = CurrentWSG.CharacterName + "'s Skills.skills";

            if (tempExport.ShowDialog() == DialogResult.OK)
            {
                // Create empty xml file
                XmlTextWriter writer = new XmlTextWriter(tempExport.FileName, new System.Text.ASCIIEncoding());
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartDocument();
                writer.WriteStartElement("INI");
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

                XmlFile Skills = new XmlFile(tempExport.FileName);
                List<string> subsectionnames = new List<string>();
                List<string> subsectionvalues = new List<string>();

                for (int Progress = 0; Progress < CurrentWSG.NumberOfSkills; Progress++)
                {
                    subsectionnames.Clear();
                    subsectionvalues.Clear();

                    subsectionnames.Add("Level");
                    subsectionnames.Add("Experience");
                    subsectionnames.Add("InUse");
                    subsectionvalues.Add(CurrentWSG.LevelOfSkills[Progress].ToString());
                    subsectionvalues.Add(CurrentWSG.ExpOfSkills[Progress].ToString());
                    subsectionvalues.Add(CurrentWSG.InUse[Progress].ToString());

                    //ExportText = ExportText + "[" + CurrentWSG.SkillNames[Progress] + "]\r\nLevel=" + CurrentWSG.LevelOfSkills[Progress] + "\r\nExperience=" + CurrentWSG.ExpOfSkills[Progress] + "\r\nInUse=" + CurrentWSG.InUse[Progress] + "\r\n";
                    Skills.AddSection(CurrentWSG.SkillNames[Progress], subsectionnames, subsectionvalues);
                }
                //File.WriteAllText(tempExport.FileName + "s", ExportText);
            }

        }

        private void ImportSkills_Click(object sender, EventArgs e)
        {
            OpenFileDialog tempImport = new OpenFileDialog();
            tempImport.DefaultExt = "*.skills";
            tempImport.Filter = "Skills Data(*.skills)|*.skills";

            tempImport.FileName = CurrentWSG.CharacterName + "'s Skills.skills";
            if (tempImport.ShowDialog() == DialogResult.OK)
            {

                //Ini.IniFile ImportSkills = new Ini.IniFile(tempImport.FileName);
                XmlFile ImportSkills = new XmlFile(tempImport.FileName);
                string[] TempSkillNames = new string[ImportSkills.stListSectionNames().Count];
                int[] TempSkillLevels = new int[ImportSkills.stListSectionNames().Count];
                int[] TempSkillExp = new int[ImportSkills.stListSectionNames().Count];
                int[] TempSkillInUse = new int[ImportSkills.stListSectionNames().Count];
                for (int Progress = 0; Progress < ImportSkills.stListSectionNames().Count; Progress++)
                {
                    TempSkillNames[Progress] = ImportSkills.stListSectionNames()[Progress];
                    TempSkillLevels[Progress] = Convert.ToInt32(ImportSkills.XmlReadValue(ImportSkills.stListSectionNames()[Progress], "Level"));
                    TempSkillExp[Progress] = Convert.ToInt32(ImportSkills.XmlReadValue(ImportSkills.stListSectionNames()[Progress], "Experience"));
                    TempSkillInUse[Progress] = Convert.ToInt32(ImportSkills.XmlReadValue(ImportSkills.stListSectionNames()[Progress], "InUse"));
                }
                CurrentWSG.SkillNames = TempSkillNames;
                CurrentWSG.LevelOfSkills = TempSkillLevels;
                CurrentWSG.ExpOfSkills = TempSkillExp;
                CurrentWSG.InUse = TempSkillInUse;
                CurrentWSG.NumberOfSkills = ImportSkills.stListSectionNames().Count;
                DoSkillTree();
            }
        }
        private void SkillList_Click(object sender, EventArgs e)
        {
            try
            {
                XmlFile SkillINI = new XmlFile(AppDir + "\\Data\\gd_skills_common.txt");
                if ((string)Class.SelectedItem == "Soldier")
                    SkillINI = new XmlFile(AppDir + "\\Data\\gd_skills2_Roland.txt");

                else if ((string)Class.SelectedItem == "Siren")
                    SkillINI = new XmlFile(AppDir + "\\Data\\gd_Skills2_Lilith.txt");

                else if ((string)Class.SelectedItem == "Hunter")
                    SkillINI = new XmlFile(AppDir + "\\Data\\gd_skills2_Mordecai.txt");

                else if ((string)Class.SelectedItem == "Berserker")
                    SkillINI = new XmlFile(AppDir + "\\Data\\gd_Skills2_Brick.txt");

                CurrentWSG.NumberOfSkills = CurrentWSG.NumberOfSkills + 1;
                ResizeArrayLarger(ref CurrentWSG.SkillNames, CurrentWSG.NumberOfSkills);
                ResizeArrayLarger(ref CurrentWSG.LevelOfSkills, CurrentWSG.NumberOfSkills);
                ResizeArrayLarger(ref CurrentWSG.ExpOfSkills, CurrentWSG.NumberOfSkills);
                ResizeArrayLarger(ref CurrentWSG.InUse, CurrentWSG.NumberOfSkills);
                CurrentWSG.InUse[CurrentWSG.NumberOfSkills - 1] = -1;
                CurrentWSG.SkillNames[CurrentWSG.NumberOfSkills - 1] = SkillINI.stListSectionNames()[SkillList.SelectedIndex];
                DoSkillTree();
            }
            catch { MessageBox.Show("Could not add new Skill."); }
        }

        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< AMMO POOLS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\

        private void AmmoTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            try
            {
                AmmoPoolRemaining.Value = (decimal)CurrentWSG.RemainingPools[AmmoTree.SelectedIndex];
                AmmoSDULevel.Value = CurrentWSG.PoolLevels[AmmoTree.SelectedIndex];
            }
            catch { }
        }

        private void AmmoPoolRemaining_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentWSG.RemainingPools[AmmoTree.SelectedIndex] = (float)AmmoPoolRemaining.Value;
            }
            catch { }
        }

        private void AmmoSDULevel_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentWSG.PoolLevels[AmmoTree.SelectedIndex] = (int)AmmoSDULevel.Value;
            }

            catch { }
        }

        private void AmmoDelete_Click(object sender, EventArgs e)
        {
            if (AmmoTree.SelectedIndex != -1)
            {
                CurrentWSG.NumberOfPools = CurrentWSG.NumberOfPools - 1;
                ResizeArraySmaller(ref CurrentWSG.AmmoPools, CurrentWSG.NumberOfPools);
                ResizeArraySmaller(ref CurrentWSG.ResourcePools, CurrentWSG.NumberOfPools);
                ResizeArraySmaller(ref CurrentWSG.RemainingPools, CurrentWSG.NumberOfPools);
                ResizeArraySmaller(ref CurrentWSG.PoolLevels, CurrentWSG.NumberOfPools);
                DoAmmoTree();
            }
        }

        private void NewAmmo_Click(object sender, EventArgs e)
        {
            try
            {
                string New_d_resources = Interaction.InputBox("Enter the 'd_resources' for the new Ammo Pool", "New Ammo Pool", "", 10, 10);
                string New_d_resourcepools = Interaction.InputBox("Enter the 'd_resourcepools' for the new Ammo Pool", "New Ammo Pool", "", 10, 10);
                if (New_d_resourcepools != "" && New_d_resources != "")
                {
                    CurrentWSG.NumberOfPools = CurrentWSG.NumberOfPools + 1;
                    ResizeArrayLarger(ref CurrentWSG.AmmoPools, CurrentWSG.NumberOfPools);
                    ResizeArrayLarger(ref CurrentWSG.ResourcePools, CurrentWSG.NumberOfPools);
                    ResizeArrayLarger(ref CurrentWSG.RemainingPools, CurrentWSG.NumberOfPools);
                    ResizeArrayLarger(ref CurrentWSG.PoolLevels, CurrentWSG.NumberOfPools);
                    CurrentWSG.AmmoPools[CurrentWSG.NumberOfPools - 1] = New_d_resourcepools;
                    CurrentWSG.ResourcePools[CurrentWSG.NumberOfPools - 1] = New_d_resources;
                    DoAmmoTree();
                }
            }
            catch { MessageBox.Show("Couldn't add new ammo pool."); }
        }

        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< WILLOWTREE LOCKER >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  \\
        private XmlFile XmlLocker = new XmlFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data\\default.xml");

        private void LockerTree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            try
            {
                PartsLocker.Items.Clear();
                XmlLocker.path = OpenedLocker;

                //Ini.IniFile Locker = new Ini.IniFile(OpenedLocker);
                //int Lockerindex = Convert.ToInt32(LockerTree.SelectedNode.TagString);
                if (!LockerTree.SelectedNode.HasChildNodes)
                {
                    string SelectedItem = LockerTree.SelectedNode.Name;

                    NameLocker.Text = SelectedItem;

                    RatingLocker.Rating = Convert.ToInt32(XmlLocker.XmlReadValue(SelectedItem, "Rating"));
                    DescriptionLocker.Text = XmlLocker.XmlReadValue(SelectedItem, "Description");
                    DescriptionLocker.Text = DescriptionLocker.Text.Replace("$LINE$", "\r\n");
                    ItemTypeLocker.SelectedItem = XmlLocker.XmlReadValue(SelectedItem, "Type");

                    for (int Progress = 0; Progress < 14; Progress++)
                        PartsLocker.Items.Add(XmlLocker.XmlReadValue(SelectedItem, "Part" + (Progress + 1)));
                    
                    try
                    {
                        LockerRemAmmo.Value = Convert.ToInt32(XmlLocker.XmlReadValue(SelectedItem, "RemAmmo_Quantity"));
                    }
                    catch
                    {
                        LockerRemAmmo.Value = 0;
                    }
                    try
                    {
                        LockerQuality.Value = Convert.ToInt32(XmlLocker.XmlReadValue(SelectedItem, "Quality"));
                    }
                    catch
                    {
                        LockerQuality.Value = 0;
                    }
                    try
                    {
                        LockerLevel.Value = Convert.ToInt32(XmlLocker.XmlReadValue(SelectedItem, "Level"));
                    }
                    catch
                    {
                        LockerLevel.Value = 0;
                    }


                    /*                int Lockerindex = LockerTree.SelectedNode.Index;

                                    NameLocker.Text = XmlLocker.ListSectionNames()[Lockerindex];

                                    RatingLocker.Rating = Convert.ToInt32(XmlLocker.XmlReadValue(XmlLocker.ListSectionNames()[Lockerindex], "Rating"));
                                    DescriptionLocker.Text = XmlLocker.XmlReadValue(XmlLocker.ListSectionNames()[Lockerindex], "Description");
                                    DescriptionLocker.Text = DescriptionLocker.Text.Replace("$LINE$", "\r\n");
                                    ItemTypeLocker.SelectedItem = XmlLocker.XmlReadValue(XmlLocker.ListSectionNames()[Lockerindex], "Type");

                                    for (int Progress = 0; Progress < 14; Progress++)
                                        PartsLocker.Items.Add(XmlLocker.XmlReadValue(XmlLocker.ListSectionNames()[Lockerindex], "Part" + (Progress + 1)));
                    */

                }
            }
            catch { }
        }

        private void SaveChangesLocker_Click(object sender, EventArgs e)
        {
            SaveChangesToLocker();
        }

        private void NewWeaponLocker_Click(object sender, EventArgs e)
        {
            List<string> iniListSectionNames = new List<string>();
            iniListSectionNames = XmlLocker.stListSectionNames();
            iniListSectionNames.TrimExcess();

            // Ok all Lockeritems now in a list

            int Occurances = 0;
            List<string> itemparts = new List<string> { "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None" };
            List<int> itemvalues = new List<int> { 0, 5, 0, 63 };

            // Search the WeaponTree.Nodes[Progress].Text in the locker
            if (iniListSectionNames.Contains("New Weapon"))
            {
                // rename and try again until in can be added
                do
                {
                    Occurances++;
                } while (iniListSectionNames.Contains("New Weapon" + " (Copy " + Occurances + ")"));
                //More than one found

                //Add new Item to the xml locker
                XmlLocker.AddItem("New Weapon" + " (Copy " + Occurances + ")", "Weapon", itemparts, itemvalues);

                Node temp = new Node();
                temp.Text = "New Weapon" + " (Copy " + Occurances + ")";
                temp.Name = "New Weapon" + " (Copy " + Occurances + ")";
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text + " (Copy " + Occurances + ")");
            }
            else
            {
                //only one found
                XmlLocker.AddItem("New Weapon", "Weapon", itemparts, itemvalues);

                XmlLocker.Reload(); // Reload the locker next time

                Node temp = new Node();
                temp.Text = "New Weapon";
                temp.Name = "New Weapon";
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text);
            }


        }

        private void NewItemLocker_Click(object sender, EventArgs e)
        {

            List<string> iniListSectionNames = new List<string>();
            iniListSectionNames = XmlLocker.stListSectionNames();
            iniListSectionNames.TrimExcess();

            // Ok all Lockeritems now in a list

            int Occurances = 0;
            List<string> itemparts = new List<string> { "None", "None", "None", "None", "None", "None", "None", "None", "None" };
            List<int> itemvalues = new List<int> { 0, 5, 0, 63 };

            // Search the WeaponTree.Nodes[Progress].Text in the locker
            if (iniListSectionNames.Contains("New Item"))
            {
                // rename and try again until in can be added
                do
                {
                    Occurances++;
                } while (iniListSectionNames.Contains("New Item" + " (Copy " + Occurances + ")"));
                //More than one found

                //Add new Item to the xml locker
                XmlLocker.AddItem("New Item" + " (Copy " + Occurances + ")", "Weapon", itemparts, itemvalues);

                Node temp = new Node();
                temp.Text = "New Item" + " (Copy " + Occurances + ")";
                temp.Name = "New Item" + " (Copy " + Occurances + ")";
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text + " (Copy " + Occurances + ")");
            }
            else
            {
                //only one found
                XmlLocker.AddItem("New Item", "Weapon", itemparts, itemvalues);

                XmlLocker.Reload(); // Reload the locker next time

                Node temp = new Node();
                temp.Text = "New Item";
                temp.Name = "New Item";
                LockerTree.Nodes.Add(temp);

                //iniListSectionNames.Add(WeaponTree.Nodes[Progress].Text);
            }

        }

        private void ItemTypeLocker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ItemTypeLocker.SelectedIndex == 0)
                for (int Progress = 9; Progress < 14; Progress++)
                {

                    PartsLocker.Items[Progress] = "None";
                }

            else if (ItemTypeLocker.SelectedIndex == 1)
                for (int Progress = 9; Progress < 14; Progress++)
                {

                    PartsLocker.Items[Progress] = "";
                }
        }

        private void ImportFromTextBlockLocker(string Text)
        {
            InOutPartsBox.Clear();
            InOutPartsBox.Text = Text;
            InOutPartsBox.Text.Replace(" ", "");

            int Progress;
            int tempValue;

            NameLocker.Text = "";

            for (Progress = 0; Progress < 14; Progress++)
            {
                if (int.TryParse(InOutPartsBox.Lines[Progress], out tempValue))
                    break;  // Get out after the first int value
                PartsLocker.Items[Progress] = InOutPartsBox.Lines[Progress];
                if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text == "")
                    NameLocker.Text = GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                else if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text != "")
                    NameLocker.Text = NameLocker.Text + " " + GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                //                    CurrentWeaponParts.Items[Progress] = InOutPartsBox.Lines[Progress];
            }

            if (Progress == 9) 
            {
                ItemTypeLocker.SelectedIndex = 1;
                NameLocker.Text = GetLongName(TitlesXml, InOutPartsBox.Lines[1], InOutPartsBox.Lines[7], InOutPartsBox.Lines[8]);
            }
            else if (Progress == 14)
            {
                ItemTypeLocker.SelectedIndex = 0;
                NameLocker.Text = GetLongName(TitlesXml, InOutPartsBox.Lines[0], InOutPartsBox.Lines[12], InOutPartsBox.Lines[13]);
            }
            else
                throw new System.FormatException(); // exception if the format isn't righr for either weapon or item

            // Exception if any of these are not int values
            LockerRemAmmo.Value = Convert.ToInt32(InOutPartsBox.Lines[Progress++]);
            LockerQuality.Value = Convert.ToInt32(InOutPartsBox.Lines[Progress++]);
            Progress++;  // Just skip the equipped slot since items in the locker aren't equipped
            LockerLevel.Value = Convert.ToInt32(InOutPartsBox.Lines[Progress++]);

            //PartsLocker.Items.Clear();

            //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
            //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini");
        }

        private void ImportFromClipboardLocker_Click(object sender, EventArgs e)
        {
            try
            {
                ImportFromTextBlockLocker(Clipboard.GetText());
            }
            catch
            {
                MessageBox.Show("Invalid clipboard data.  Reverting to saved values.");
                LockerTree_AfterNodeSelect(null, null);  // reloads the saved values from the node
            }
        }            

//        private void ImportFromClipboardLocker_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                InOutPartsBox.Clear();
//                InOutPartsBox.Text = Clipboard.GetText();
//                InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");

//                int Progress;
//                int tempValue;

//                NameLocker.Text = "";

//                for (Progress = 0; Progress < 14; Progress++)
//                {
//                    if (int.TryParse(InOutPartsBox.Lines[Progress], out tempValue))
//                        break;  // Get out after the first int value
//                    PartsLocker.Items[Progress] = InOutPartsBox.Lines[Progress];
//                    if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text == "")
//                        NameLocker.Text = GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
//                    else if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text != "")
//                        NameLocker.Text = NameLocker.Text + " " + GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
////                    CurrentWeaponParts.Items[Progress] = InOutPartsBox.Lines[Progress];
//                }

//                if (Progress == 9)
//                    ItemTypeLocker.SelectedIndex = 1;
//                else if (Progress == 14)
//                    ItemTypeLocker.SelectedIndex = 0;
//                else
//                    throw new System.FormatException(); // exception if the format isn't righr for either weapon or item

//                // Exception if any of these are not int values
//                LockerRemAmmo.Value = Convert.ToInt32(InOutPartsBox.Lines[Progress++]);
//                LockerQuality.Value = Convert.ToInt32(InOutPartsBox.Lines[Progress++]);
//                Progress++;  // Just skip the equipped slot since items in the locker aren't equipped
//                LockerLevel.Value = Convert.ToInt32(InOutPartsBox.Lines[Progress++]);

//                //PartsLocker.Items.Clear();

//                //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
//                //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini");
//            }
//            catch
//            {
//                MessageBox.Show("Invalid clipboard data.  Reverting to saved values.");
//                LockerTree_AfterNodeSelect(null, null);  // reloads the saved values from the node
//            }
//        }

        private void ImportFromFileLocker_Click(object sender, EventArgs e)
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.txt";
            FromFile.Filter = "Item Files(*.txt)|*.txt";

            FromFile.FileName = CurrentPartsGroup.Text + ".txt";
            try
            {
                if (FromFile.ShowDialog() == DialogResult.OK)
                {

                    InOutPartsBox.Clear();
                    InOutPartsBox.Text = System.IO.File.ReadAllText(FromFile.FileName);
                    InOutPartsBox.Text = InOutPartsBox.Text.Replace(" ", "");
                    //PartsLocker.Items.Clear();
                    NameLocker.Text = "";
                    //Ini.IniFile Titles = new Ini.IniFile(AppDir + "\\Data\\Titles.ini");
                    //TitlesXml.XmlFilename(AppDir + "\\Data\\Titles.ini");

                    for (int Progress = 0; Progress < 14; Progress++)
                    {
                        PartsLocker.Items[Progress] = InOutPartsBox.Lines[Progress];
                        if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text == "")
                            NameLocker.Text = GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                        else if (GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != "" && GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName") != null && NameLocker.Text != "")
                            NameLocker.Text = NameLocker.Text + " " + GetName(TitlesXml, InOutPartsBox.Lines, Progress, "PartName");
                    }
                }
            }
            catch { MessageBox.Show("Couldn't insert from file."); }
        }

        private void ExportToFileLocker_Click(object sender, EventArgs e)
        {
            SaveFileDialog ToFile = new SaveFileDialog();
            ToFile.DefaultExt = "*.txt";
            ToFile.Filter = "Item Files(*.txt)|*.txt";
            ToFile.FileName = NameLocker.Text + ".txt";
            if (ToFile.ShowDialog() == DialogResult.OK)
            {
                int Loops = 0;
                InOutPartsBox.Clear();
                if (ItemTypeLocker.SelectedIndex == 0) Loops = 14;
                else if (ItemTypeLocker.SelectedIndex == 1) Loops = 9;
                for (int Progress = 0; Progress < Loops; Progress++)
                {
                    if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                    InOutPartsBox.AppendText((string)PartsLocker.Items[Progress]);
                }
                if (chkExpSettings.Checked == true)
                {
                    InOutPartsBox.AppendText("\r\n" + overrideRemAmmo.Value);
                    InOutPartsBox.AppendText("\r\n" + overrideQuality.Value);
                    InOutPartsBox.AppendText("\r\n0");
                    InOutPartsBox.AppendText("\r\n" + overrideLevel.Value);
                }
                else
                {
                    InOutPartsBox.AppendText("\r\n" + LockerRemAmmo.Value);
                    InOutPartsBox.AppendText("\r\n" + LockerQuality.Value);
                    InOutPartsBox.AppendText("\r\n0");
                    InOutPartsBox.AppendText("\r\n" + LockerLevel.Value);
                }
                System.IO.File.WriteAllLines(ToFile.FileName, InOutPartsBox.Lines);
            }
        }

        private void ExportToClipboardLocker_Click(object sender, EventArgs e)
        {
            int Loops = 0;
            InOutPartsBox.Clear();
            if (ItemTypeLocker.SelectedIndex == 0) Loops = 14;
            else if (ItemTypeLocker.SelectedIndex == 1) Loops = 9;
            for (int Progress = 0; Progress < Loops; Progress++)
            {
                if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                InOutPartsBox.AppendText((string)PartsLocker.Items[Progress]);
            }
            if (chkExpSettings.Checked == true)
            {
                InOutPartsBox.AppendText("\r\n" + overrideRemAmmo.Value);
                InOutPartsBox.AppendText("\r\n" + overrideQuality.Value);
                InOutPartsBox.AppendText("\r\n0");
                InOutPartsBox.AppendText("\r\n" + overrideLevel.Value);
            }
            else
            {
                InOutPartsBox.AppendText("\r\n" + LockerRemAmmo.Value);
                InOutPartsBox.AppendText("\r\n" + LockerQuality.Value);
                InOutPartsBox.AppendText("\r\n0");
                InOutPartsBox.AppendText("\r\n" + LockerLevel.Value);
            }
            InOutPartsBox.AppendText("\r\n");

            Clipboard.SetText(InOutPartsBox.Text);
        }

        private void ExportToBackpack_Click(object sender, EventArgs e)
        {
            List<string> selectednodes = new List<string>();

            foreach (Node selnode in LockerTree.SelectedNodes)
            {
                //Ini.IniFile Locker = new Ini.IniFile(OpenedLocker);
                //XmlFile Locker = new XmlFile(OpenedLocker);

                if (selnode.HasChildNodes)
                {
                    foreach (Node childnode in selnode.Nodes)
                    {
                        selectednodes.Add(childnode.Name);
                    }
                }
                else
                {
                    selectednodes.Add(selnode.Name);
                }
            }

            //We got all selected nodes in a list
            foreach (string LockerSelected in selectednodes)
            {
                //string LockerSelected = selnode.Name;

                PartsLocker.Items.Clear();
                NameLocker.Text = LockerSelected;

                RatingLocker.Rating = Convert.ToInt32(XmlLocker.XmlReadValue(LockerSelected, "Rating"));
                DescriptionLocker.Text = XmlLocker.XmlReadValue(LockerSelected, "Description");
                DescriptionLocker.Text = DescriptionLocker.Text.Replace("$LINE$", "\r\n");
                ItemTypeLocker.SelectedItem = XmlLocker.XmlReadValue(LockerSelected, "Type");

                for (int Progress = 0; Progress < 14; Progress++)
                    PartsLocker.Items.Add(XmlLocker.XmlReadValue(LockerSelected, "Part" + (Progress + 1)));

                if (ItemTypeLocker.SelectedIndex == 0)
                {

                    InOutPartsBox.Clear();

                    for (int Progress = 0; Progress < 14; Progress++)
                    {
                        if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                        InOutPartsBox.AppendText((string)PartsLocker.Items[Progress]);
                    }

                    if (chkExpSettings.Checked == true)
                    {
                        InOutPartsBox.AppendText("\r\n" + overrideRemAmmo.Value);
                        InOutPartsBox.AppendText("\r\n" + overrideQuality.Value);
                        InOutPartsBox.AppendText("\r\n0");
                        InOutPartsBox.AppendText("\r\n" + overrideLevel.Value);
                    }
                    else
                    {
                        InOutPartsBox.AppendText("\r\n" + LockerRemAmmo.Value);
                        InOutPartsBox.AppendText("\r\n" + LockerQuality.Value);
                        InOutPartsBox.AppendText("\r\n0");
                        InOutPartsBox.AppendText("\r\n" + LockerLevel.Value);
                    }


                    CurrentWSG.NumberOfWeapons = CurrentWSG.NumberOfWeapons + 1;
                    CurrentWSG.WeaponStrings.Add(new List<string>());
                    CurrentWSG.WeaponValues.Add(new List<int>());


                    for (int Progress = 0; Progress < 14; Progress++)
                        CurrentWSG.WeaponStrings[CurrentWSG.NumberOfWeapons - 1].Add(InOutPartsBox.Lines[Progress]);
                    for (int Progress = 0; Progress < 4; Progress++)
                        CurrentWSG.WeaponValues[CurrentWSG.NumberOfWeapons - 1].Add(Convert.ToInt32(InOutPartsBox.Lines[Progress + 14]));
                    Node TempNode = new Node();
                    TempNode.Text = GetName(TitlesXml, CurrentWSG.WeaponStrings, CurrentWSG.NumberOfWeapons - 1, 14, "PartName", "Weapon");
                    WeaponTree.Nodes.Add(TempNode);
                    //DoWeaponTree();
                }
                else if (ItemTypeLocker.SelectedIndex == 1)
                {

                    InOutPartsBox.Clear();

                    for (int Progress = 0; Progress < 9; Progress++)
                    {
                        if (Progress > 0) InOutPartsBox.AppendText("\r\n");
                        InOutPartsBox.AppendText((string)PartsLocker.Items[Progress]);
                    }
                    if (chkExpSettings.Checked == true)
                    {

                        if (overrideRemAmmo.Value == 0)
                            // Set quantity to 1 so noone gets hurt
                            InOutPartsBox.AppendText("\r\n1");
                        else
                            InOutPartsBox.AppendText("\r\n" + overrideRemAmmo.Value);

                        InOutPartsBox.AppendText("\r\n" + overrideQuality.Value);
                        InOutPartsBox.AppendText("\r\n0");
                        InOutPartsBox.AppendText("\r\n" + overrideLevel.Value);
                    }
                    else
                    {
                        if (LockerRemAmmo.Value == 0)
                            // Set quantity to 1 so noone gets hurt
                            InOutPartsBox.AppendText("\r\n1");
                        else
                            InOutPartsBox.AppendText("\r\n" + LockerRemAmmo.Value);

                        InOutPartsBox.AppendText("\r\n" + LockerQuality.Value);
                        InOutPartsBox.AppendText("\r\n0");
                        InOutPartsBox.AppendText("\r\n" + LockerLevel.Value);

                    }


                    CurrentWSG.ItemStrings.Add(new List<string>());
                    CurrentWSG.ItemValues.Add(new List<int>());
                    CurrentWSG.NumberOfItems = CurrentWSG.NumberOfItems + 1;
                    for (int Progress = 0; Progress < 9; Progress++)
                        CurrentWSG.ItemStrings[CurrentWSG.NumberOfItems - 1].Add(InOutPartsBox.Lines[Progress]);
                    for (int Progress = 0; Progress < 4; Progress++)
                        CurrentWSG.ItemValues[CurrentWSG.NumberOfItems - 1].Add(Convert.ToInt32(InOutPartsBox.Lines[Progress + 9]));
                    Node TempNode = new Node();
                    TempNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, CurrentWSG.NumberOfItems - 1, 9, "PartName", "Item");
                    ItemTree.Nodes.Add(TempNode);
                    //DoItemTree();
                }
            }
        }

        private void DeleteLocker_Click(object sender, EventArgs e)
        {
            int Selected = LockerTree.SelectedIndex;
            if (Selected == -1)
            {
                MessageBox.Show("Select an item to delete first.");
                return;
            }
            XmlLocker.RemoveItem(LockerTree.SelectedNode.Name);

            LockerTree.Nodes[LockerTree.SelectedIndex].Remove();

            //DoLockerTree(OpenedLocker);
            try
            {
                LockerTree.SelectedIndex = Selected;


            }
            catch { }

            /*            try
                        {
                            int Selected = LockerTree.SelectedIndex;
                            string search = "[" + LockerTree.SelectedNode.Text + "]";

                            string[] tempINI2 = System.IO.File.ReadAllLines(OpenedLocker);

                            int DiscoveredLine = -1;
                            for (int Progress = 0; Progress < tempINI2.Length; Progress++)
                            {
                                //string tewst = tempINI2[Progress].Substring(0, tempINI2[Progress].Length - 1);
                                if (tempINI2[Progress] == search)
                                    DiscoveredLine = Progress;


                            }
                            for (int Progress = 0; Progress < 18; Progress++)
                                tempINI2[DiscoveredLine + Progress] = "";
                            System.IO.File.WriteAllLines(OpenedLocker, tempINI2);
                            DoLockerTree(OpenedLocker);
                            try
                            {
                                LockerTree.SelectedIndex = Selected;
                            }
                            catch { }
                        }
                        catch { }*/
        }

        private void OpenLocker_Click(object sender, EventArgs e)
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.wtl";
            FromFile.Filter = "WillowTree Locker(*.xml;*.wtl)|*.xml;*.wtl";

            FromFile.FileName = CurrentPartsGroup.Text + ".xml";
            try
            {
                if (FromFile.ShowDialog() == DialogResult.OK)
                {
                    OpenedLocker = FromFile.FileName;
                    if (FromFile.FileName.EndsWith("wtl"))
                    {
                        //Convert wtl to xml
                        DoLockerTreetry(FromFile.FileName);
                        DoLockerTree(FromFile.FileName.Replace(".wtl", ".xml"));
                    } 
                    else if (FromFile.FileName.EndsWith("xml"))
                    {
                        XmlLocker.Reload(FromFile.FileName);
                        DoLockerTreeNodupe(FromFile.FileName);
                        DoLockerTree(FromFile.FileName);
                        
                    }
                }
            }
            catch { MessageBox.Show("Could not load the selected WillowTree Locker."); }
        }

        private void SaveAsLocker_Click(object sender, EventArgs e)
        {
            SaveFileDialog ToFile = new SaveFileDialog();
            ToFile.DefaultExt = "*.xml";
            ToFile.Filter = "WillowTree Locker(*.xml)|*.xml";

            ToFile.FileName = "default.xml";
            try
            {
                if (ToFile.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(OpenedLocker, ToFile.FileName, true);
                    DoLockerTree(ToFile.FileName);
                }
            }
            catch { MessageBox.Show("Could not save the selected WillowTree Locker."); }
        }

        private void ImportAllFromWeapons_Click(object sender, EventArgs e)
        {
            for (int Progress = 0; Progress < CurrentWSG.NumberOfWeapons; Progress++)
            {
                string ItemName = GetLongName(TitlesXml, CurrentWSG.WeaponStrings[Progress][0], CurrentWSG.WeaponStrings[Progress][12], CurrentWSG.WeaponStrings[Progress][13]);
                string UniqueName = XmlLocker.GetUniqueName(ItemName);
                XmlLocker.AddItem(UniqueName, "Weapon", CurrentWSG.WeaponStrings[Progress], CurrentWSG.WeaponValues[Progress]);
        
                Node temp = new Node();
                temp.Text = UniqueName;
                LockerTree.Nodes.Add(temp);

                XmlLocker.Reload();
            }
            DoLockerTree(OpenedLocker);
        }

        private void ImportAllFromItems_Click(object sender, EventArgs e)
        {
            for (int Progress = 0; Progress < CurrentWSG.NumberOfItems; Progress++)
            {
                string ItemName = GetLongName(TitlesXml, CurrentWSG.ItemStrings[Progress][1], CurrentWSG.ItemStrings[Progress][7], CurrentWSG.ItemStrings[Progress][8]);
                string UniqueName = XmlLocker.GetUniqueName(ItemName);
                XmlLocker.AddItem(UniqueName, "Item", CurrentWSG.ItemStrings[Progress], CurrentWSG.ItemValues[Progress]);

                Node temp = new Node();
                temp.Text = UniqueName;
                LockerTree.Nodes.Add(temp);
            }
            DoLockerTree(OpenedLocker);
        }

        private void ImportAllFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog tempRead = new OpenFileDialog();
            if (tempRead.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    XmlTextReader reader = new XmlTextReader(OpenedLocker);
                    XmlDocument lockerxml = new XmlDocument();
                    lockerxml.Load(reader);
                    reader.Close();

                    reader = new XmlTextReader(tempRead.FileName);
                    XmlDocument importxml = new XmlDocument();
                    importxml.Load(reader);
                    reader.Close();

                    XmlNode lockerroot = lockerxml.DocumentElement;
                    XmlNode inputroot = importxml.DocumentElement;

                    lockerroot.InnerXml = lockerroot.InnerXml + inputroot.InnerXml;
                    lockerxml.Save(OpenedLocker);

                    XmlLocker.Reload();

                    DoLockerTreeNodupe(OpenedLocker);
                    DoLockerTree(OpenedLocker);
                }
                catch
                {
                    MessageBox.Show("Import failed.");
                }
            }
        }


        private void UpdateButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + DownloadURLFromServer);
        }

        private void CurrentWeaponParts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void WeaponInfo_Click(object sender, EventArgs e)
        {
            string[] Parts = new string[14];

            int prog = 0;
            foreach (string i in CurrentWeaponParts.Items)
            {
                Parts[prog] = i;
                prog = prog + 1;
            }
            string WeaponInfo;
            DamageText.Text = "Expected Weapon Damage: " + GetWeaponDamage(Parts);
            if (WeaponItemGradeSlider.Value > 0)
                WeaponInfo = "Expected Damage: " + GetWeaponDamage(Parts, WeaponItemGradeSlider.Value);
            else
                WeaponInfo = "Expected Damage: " + GetWeaponDamage(Parts);
            if (GetExtraStats(Parts, "TechLevelIncrease") != 0)
                WeaponInfo = WeaponInfo + "\r\nElemental Tech Level: " + GetExtraStats(Parts, "TechLevelIncrease");
            if (GetExtraStats(Parts, "WeaponDamage") != 0)
                WeaponInfo = WeaponInfo + "\r\n" + GetExtraStats(Parts, "WeaponDamage") + "% Damage";
            if (GetExtraStats(Parts, "WeaponFireRate") != 0)
                WeaponInfo = WeaponInfo + "\r\n" + GetExtraStats(Parts, "WeaponFireRate") + "% Rate of Fire";
            if (GetExtraStats(Parts, "WeaponCritBonus") != 0)
                WeaponInfo = WeaponInfo + "\r\n" + GetExtraStats(Parts, "WeaponCritBonus") + "% Critical Damage";
            if (GetExtraStats(Parts, "WeaponReloadSpeed") != 0)
                WeaponInfo = WeaponInfo + "\r\n" + GetExtraStats(Parts, "WeaponReloadSpeed") + "% Reload Speed";
            if (GetExtraStats(Parts, "WeaponSpread") != 0)
                WeaponInfo = WeaponInfo + "\r\n" + GetExtraStats(Parts, "WeaponSpread") + "% Spread";
            if (GetExtraStats(Parts, "MaxAccuracy") != 0)
                WeaponInfo = WeaponInfo + "\r\n" + GetExtraStats(Parts, "MaxAccuracy") + "% Max Accuracy";
            if (GetExtraStats(Parts, "MinAccuracy") != 0)
                WeaponInfo = WeaponInfo + "\r\n" + GetExtraStats(Parts, "MinAccuracy") + "% Min Accuracy";
            if (Experience.Value == 1337)
                WeaponInfo = AmIACookie(false);
            if (Experience.Value == 1337 && BackpackSpace.Value == 1337)
                WeaponInfo = AmIACookie(true);
            MessageBox.Show(WeaponInfo);
        }

        private void PartCategories_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            WeaponPartInfo.Clear();
            try
            {
                // Read ALL subsections of a given XML section
                // File: (AppDir + "\\Data\\" + PartCategories.SelectedNode.Parent.Text + ".txt")
                XmlFile Category = new XmlFile(AppDir + "\\Data\\" + PartCategories.SelectedNode.Parent.Text + ".txt");

                // XML Section: PartCategories.SelectedNode.Text
                List<string> xmlSection = Category.XmlReadSection(PartCategories.SelectedNode.Text);

                WeaponPartInfo.Lines = xmlSection.ToArray();
                /*
                if (File.Exists(AppDir + "\\Data\\" + PartCategories.SelectedNode.Parent.Text + ".txt"))
                {
                    string[] InText = File.ReadAllLines(AppDir + "\\Data\\" + PartCategories.SelectedNode.Parent.Text + ".txt");
                    int start = 0;
                    int end = 1;
                    string search = "[" + PartCategories.SelectedNode.Text + "]";
                    ArrayList Lines = new ArrayList();
                    for (int i1 = 0; i1 < InText.Length; i1++)
                    {
                        if (InText[i1].Contains(search))
                        {
                            start = i1;
                            i1 = InText.Length;
                        }
                    }
                    for (int i2 = start + 1; i2 < InText.Length; i2++)
                    {
                        if (InText[i2].Contains("[") == true && InText[i2].Contains("=") == false)
                        {
                            end = i2;
                            i2 = InText.Length;
                        }
                    }
                    if(start > end)
                        for (start++; start < InText.Length; start++)
                        {
                            
                                InText[start].Replace("=", ": ");
                                Lines.Add(InText[start]);
                            
                        }
                    else
                    for (start++; start < end; start++)
                    {
                        
                            InText[start].Replace("=", ": ");
                            Lines.Add(InText[start]);
                        
                    }
                    WeaponPartInfo.Lines = (string[])Lines.ToArray(typeof(string));

                }*/
            }
            catch { }
        }


        private void DLCWeaponButton_Click(object sender, EventArgs e)
        {
            DoDLCWeaponTree();
            IsDLCWeaponMode = true;
            WeaponItemGradeSlider.Enabled = true;
            WeaponPanel1.Text = "DLC Backpack";
        }

        private void MainWeaponButton_Click(object sender, EventArgs e)
        {
            DoWeaponTree();
            IsDLCWeaponMode = false;
            WeaponItemGradeSlider.Value = 0;
            WeaponItemGradeSlider.Enabled = false;
            WeaponPanel1.Text = "Main Backpack";
        }

        private void ItemGradeSlider_ValueChanged(object sender, EventArgs e)
        {
            if (WeaponItemGradeSlider.Value > 0)
                WeaponGradeLevel.Text = "Level: " + (WeaponItemGradeSlider.Value - 2);
            else
            {
                WeaponGradeLevel.Text = "Level: Disabled";
            }
        }

        private void DLCItemButton_Click(object sender, EventArgs e)
        {
            DoDLCItemTree();
            IsDLCItemMode = true;
            ItemItemGradeSlider.Enabled = true;
            ItemPanel.Text = "DLC Backpack";
        }

        private void MainItemButton_Click(object sender, EventArgs e)
        {
            DoItemTree();
            IsDLCItemMode = false;
            ItemItemGradeSlider.Value = 0;
            ItemItemGradeSlider.Enabled = false;
            ItemPanel.Text = "Main Backpack";
        }

        private void ItemItemGradeSlider_ValueChanged(object sender, EventArgs e)
        {
            if (ItemItemGradeSlider.Value > 0)
                ItemWeaponGradeLevel.Text = "Level: " + (ItemItemGradeSlider.Value - 2);
            else
            {
                ItemWeaponGradeLevel.Text = "Level: Disabled";
            }
        }



        private void EditAll_Click(object sender, EventArgs e)
        {
            string tempNewLevels = Interaction.InputBox("All of the guns in your backpack will be adjusted to the following level:", "Edit All Levels", "", 10, 10);
            if (tempNewLevels != "" && tempNewLevels == "" + Convert.ToInt32(tempNewLevels))
            {
                foreach (List<int> item in CurrentWSG.WeaponValues)
                    item[3] = Convert.ToInt32(tempNewLevels) + 2;
                WeaponItemGradeSlider.Value = Convert.ToInt32(tempNewLevels) + 2;
            }
        }

        private void InsertWeaponsFromMultipleFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.txt";
            FromFile.Filter = "Weapon Files(*.txt)|*.txt";
            FromFile.Multiselect = true;

            FromFile.FileName = CurrentPartsGroup.Text + ".txt";

            if (FromFile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in FromFile.FileNames)
                {
                    InOutPartsBox.Clear();
                    InOutPartsBox.Text = System.IO.File.ReadAllText(file);
                    if (IsDLCWeaponMode)
                    {
                        CurrentWSG.DLC.WeaponParts.Add(new List<string>());
                        CurrentWSG.DLC.TotalWeapons++;
                        for (int Progress = 0; Progress < 14; Progress++)
                        {
                            CurrentWSG.DLC.WeaponParts[CurrentWSG.DLC.WeaponParts.Count - 1].Add(InOutPartsBox.Lines[Progress]);

                        }
                        CurrentWSG.DLC.WeaponAmmo.Add(Convert.ToInt32(InOutPartsBox.Lines[14]));
                        CurrentWSG.DLC.WeaponQuality.Add(Convert.ToInt32(InOutPartsBox.Lines[15]));
                        CurrentWSG.DLC.WeaponEquippedSlot.Add(Convert.ToInt32(InOutPartsBox.Lines[16]));
                        DoDLCWeaponTree();
                    }
                    else
                    {
                         // Create new lists
                        List<string> wpnstrings = new List<string>();
                        List<int> wpnvalues = new List<int>();

                        try
                        {
                            for (int Progress = 0; Progress < 14; Progress++)
                            {
                                ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                                wpnstrings.Add(InOutPartsBox.Lines[Progress]);
                            }

                            for (int Progress = 0; Progress < 4; Progress++)
                                wpnvalues.Add(Convert.ToInt32(InOutPartsBox.Lines[Progress + 14]));
                            wpnvalues[2] = 0; // set equipped slot to none
                        }
                        catch
                        {
                            MessageBox.Show("Invalid file data or unable to read file \"" + file + "\".");
                            continue;
                        }
                        CurrentWSG.WeaponStrings.Add(wpnstrings);
                        CurrentWSG.WeaponValues.Add(wpnvalues);
                        CurrentWSG.NumberOfWeapons++;

                        Node TempNode = new Node();
                        TempNode.Text = GetName(TitlesXml, CurrentWSG.WeaponStrings, CurrentWSG.NumberOfWeapons - 1, 14, "PartName", "Weapon");
                        WeaponTree.Nodes.Add(TempNode);
                        //DoWeaponTree();
                    }
                }
            }

        }

        private void InsertItemsFromMultipleFiles(object sender, EventArgs e)
        {
            OpenFileDialog FromFile = new OpenFileDialog();
            FromFile.DefaultExt = "*.txt";
            FromFile.Filter = "Item Files(*.txt)|*.txt";
            FromFile.Multiselect = true;

            FromFile.FileName = CurrentPartsGroup.Text + ".txt";

            if (FromFile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in FromFile.FileNames)
                {
                    InOutPartsBox.Clear();
                    InOutPartsBox.Text = System.IO.File.ReadAllText(file);

                    if (IsDLCItemMode)
                    {
                        CurrentWSG.DLC.ItemParts.Add(new List<string>());
                        CurrentWSG.NumberOfItems++;
                        for (int Progress = 0; Progress < 9; Progress++)

                            CurrentWSG.DLC.ItemParts[CurrentWSG.NumberOfItems - 1].Add(InOutPartsBox.Lines[Progress]);
                        CurrentWSG.DLC.ItemQuantity.Add(Convert.ToInt32(InOutPartsBox.Lines[9]));
                        CurrentWSG.DLC.ItemQuality.Add(Convert.ToInt32(InOutPartsBox.Lines[10]));
                        CurrentWSG.DLC.ItemEquipped.Add(Convert.ToInt32(InOutPartsBox.Lines[11]));
                        CurrentWSG.DLC.ItemLevel.Add(0);
                        Node TempNode = new Node();
                        TempNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, CurrentWSG.NumberOfItems - 1, 9, "PartName", "Item");
                        ItemTree.Nodes.Add(TempNode);
                        //DoDLCItemTree();
                    }
                    else
                    {
                        List<string> itemstrings = new List<string>();
                        List<int> itemvalues = new List<int>();

                        try
                        {
                            for (int Progress = 0; Progress < 9; Progress++)
                            {
                                ThrowExceptionIfIntString(InOutPartsBox.Lines[Progress]);
                                itemstrings.Add(InOutPartsBox.Lines[Progress]);
                            }

                            for (int Progress = 0; Progress < 4; Progress++)
                                itemvalues.Add(Convert.ToInt32(InOutPartsBox.Lines[Progress + 9]));
                            itemvalues[2] = 0; // set equipped slot to 0
                        }
                        catch
                        {
                            MessageBox.Show("Invalid item data in file \"" + file + "\".  Item not inserted.");
                            continue;
                        }
   
                        CurrentWSG.ItemStrings.Add(itemstrings);
                        CurrentWSG.ItemValues.Add(itemvalues);
                        CurrentWSG.NumberOfItems++;

                        Node TempNode = new Node();
                        TempNode.Text = GetName(TitlesXml, CurrentWSG.ItemStrings, CurrentWSG.NumberOfItems - 1, 9, "PartName", "Item");
                        ItemTree.Nodes.Add(TempNode);
                    }
                }
            }
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected = WeaponTree.SelectedIndex;
                WeaponTree.DeselectNode(WeaponTree.SelectedNode, new eTreeAction());
                WeaponTree.Nodes.RemoveAt(Selected);
                CurrentWSG.WeaponStrings.RemoveAt(Selected);
                CurrentWSG.WeaponValues.RemoveAt(Selected);
                CurrentWSG.NumberOfWeapons--;
                TrySelectedNode(WeaponTree, Selected);
                //DoWeaponTree();

            }
            catch { MessageBox.Show("Select a weapon to delete first."); }
        }

        private void WeaponQuality_ValueChanged(object sender, EventArgs e)
        {
            if (WeaponQuality.Value >= 0)
                WeaponQualityLabel.Text = "Quality: " + (WeaponQuality.Value);
            else
            {
                WeaponQualityLabel.Text = "Quality: Disabled";
            }
        }

        private void overrideQuality_ValueChanged(object sender, EventArgs e)
        {
            if (overrideQuality.Value >= 0)
                overrideQualitylabel.Text = "Quality: " + (overrideQuality.Value);
            else
            {
                overrideQualitylabel.Text = "Quality: Disabled";
            }
        }

        private void overrideLevel_ValueChanged(object sender, EventArgs e)
        {
            if (overrideLevel.Value > 0)
                overrideLevellabel.Text = "Level: " + (overrideLevel.Value - 2);
            else
            {
                overrideLevellabel.Text = "Level: Disabled";
            }
        }

        private void btnlockerSearch_Click(object sender, EventArgs e)
        {
            List<string> searchresults;
            searchresults = XmlLocker.XmlSearchSection(lockerSearch.Text);

            int start = 0;

            for (int ndcnt = start; ndcnt < LockerTree.Nodes.Count; ndcnt++)
                // Set all nodes to standard style
                LockerTree.Nodes[ndcnt].Style = elementStyle5;

            int srcnt = 0;
            int srfcnt = 0;
            foreach (string searchresult in searchresults)
            {
                srcnt++;
                for (int ndcnt = 0; ndcnt < LockerTree.Nodes.Count; ndcnt++)
                {

                    if (LockerTree.Nodes[ndcnt].Name == searchresult)
                    {
                        LockerTree.Nodes[ndcnt].Style = elementStyle6;
                        start = ndcnt + 1;
                        srfcnt++;
                        break;
                    }
                }
            }
        }

        private void lockerSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnlockerSearch_Click(this, KeyEventArgs.Empty);
            }
        }

        private void EditAllItems_Click(object sender, EventArgs e)
        {
            string tempNewLevels = Interaction.InputBox("All of the items in your backpack will be adjusted to the following level:", "Edit All Levels", "", 10, 10);
            if (tempNewLevels != "" && tempNewLevels == "" + Convert.ToInt32(tempNewLevels))
            {
                foreach (List<int> item in CurrentWSG.ItemValues)
                    item[3] = Convert.ToInt32(tempNewLevels) + 2;
                ItemItemGradeSlider.Value = Convert.ToInt32(tempNewLevels) + 2;
            }
        }


        private void CheckVerPopup(object sender, PaintEventArgs e)
        {
            if (VersionFromServer != Version.Text && VersionFromServer != "" && VersionFromServer != null)
            {
                UpdateButton.Text = "Version " + VersionFromServer + " is now available! Click here to download.";
                UpdateBar.Show();
                //t1.Join();
            }
        }

        private void CheckVerPopup(object sender, MouseEventArgs e)
        {
            if (VersionFromServer != Version.Text && VersionFromServer != "" && VersionFromServer != null)
            {
                UpdateButton.Text = "Version " + VersionFromServer + " is now available! Click here to download.";
                UpdateBar.Show();
                //t1.Join();
            }
        }

        private void ItemQuality_ValueChanged(object sender, EventArgs e)
        {
            if (ItemQuality.Value >= 0)
                ItemQualityLabel.Text = "Quality: " + (ItemQuality.Value);
            else
            {
                ItemQualityLabel.Text = "Quality: Disabled";
            }
        }

        private void LockerQuality_ValueChanged(object sender, EventArgs e)
        {
            if (LockerQuality.Value >= 0)
                LockerQualityLabel.Text = "Quality: " + (LockerQuality.Value);
            else
            {
                LockerQualityLabel.Text = "Quality: Disabled";
            }
        }

        private void LockerLevel_ValueChanged(object sender, EventArgs e)
        {
            if (LockerLevel.Value > 0)
                LockerLevelLabel.Text = "Level: " + (LockerLevel.Value - 2);
            else
            {
                LockerLevelLabel.Text = "Level: Disabled";
            }
        }

        private void DoWindowTitle()
        {
            this.Text = "WillowTree# - " + CharacterName.Text + "  Level " + Level.Value + " " + Class.Text + " (" + CurrentWSG.Platform + ")";
        }

        private void PCFormat_Click(object sender, EventArgs e)
        {
            if (CurrentWSG.Platform == "PC")
                return;
            if ((CurrentWSG.ContainsRawData == true) && (CurrentWSG.EndianWSG != ByteOrder.LittleEndian))
            {
                MessageBox.Show("This savegame contains raw data that could not be parsed so it cannot be exported to a different machine byte order.");
                return;
            }
                
            CurrentWSG.Platform = "PC";
            CurrentWSG.EndianWSG = ByteOrder.LittleEndian;
            DoWindowTitle();
            Save.Enabled = false;
        }
        private void PS3Format_Click(object sender, EventArgs e)
        {
            if (CurrentWSG.Platform == "PS3")
                return;
            if ((CurrentWSG.ContainsRawData == true) && (CurrentWSG.EndianWSG != ByteOrder.BigEndian))
            {
                MessageBox.Show("This savegame contains raw data that could not be parsed so it cannot be exported to a different machine byte order.");
                return;
            }
            CurrentWSG.Platform = "PS3";
            CurrentWSG.EndianWSG = ByteOrder.BigEndian;
            DoWindowTitle();
            MessageBox.Show("This save data will be stored in the PS3 format. Please note that you will require \r\nproper SFO, PNG, and PFD files to be transfered back to the \r\nPS3. These can be acquired from another Borderlands save \r\nfor the same profile.");
            Save.Enabled = false;
        }
        private void XBoxFormat_Click(object sender, EventArgs e)
        {
            if (CurrentWSG.Platform == "X360")
                return;
            if ((CurrentWSG.ContainsRawData == true) && (CurrentWSG.EndianWSG != ByteOrder.BigEndian))
            {
                MessageBox.Show("This savegame contains raw data that could not be parsed so it cannot be exported to a different machine byte order.");
                return;
            }

            if (CurrentWSG.DeviceID == null)
            {
                XBoxIDDialog dlgXBoxID = new XBoxIDDialog();
                if (dlgXBoxID.ShowDialog() == DialogResult.OK)
                {
                    CurrentWSG.ProfileID = dlgXBoxID.ID.ProfileID;
                    int DeviceIDLength = dlgXBoxID.ID.DeviceID.Count();
                    CurrentWSG.DeviceID = new byte[DeviceIDLength];
                    Array.Copy(dlgXBoxID.ID.DeviceID, CurrentWSG.DeviceID, DeviceIDLength);
                }
                else
                    return;
            }
            CurrentWSG.Platform = "X360";
            CurrentWSG.EndianWSG = ByteOrder.BigEndian;
            DoWindowTitle();
            CurrentWSG.OpenedWSG = "";
            Save.Enabled = false;
        }

        private void Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoWindowTitle();
        }

        private void CharacterName_TextChanged(object sender, EventArgs e)
        {
            DoWindowTitle();
        }

    }

}

