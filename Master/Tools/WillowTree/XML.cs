using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace WillowTree
{
    /// <summary>

    /// Create a New XML file to store or load data

    /// </summary>

    public class XmlFile
    {
        
        public string path;
        private XmlDocument xmlrdrdoc = null;
        //private XmlNodeList xnrdrList = null;
        //private string[] arrListSectionNames = null;
        private List<string> listListSectionNames = new List<string>();

        /// <summary>

        /// INIFile Constructor.

        /// </summary>

        /// <PARAM name="INIPath"></PARAM>

        public XmlFile(string filePath)
        {
            List<string> listfilePath=new List<string>();
            string targetfile = "";

            path = filePath;
            listfilePath.Add(filePath); //Contains all ini style filenames
            if (filePath.EndsWith(".ini"))
            {
                targetfile = filePath.Replace(".ini", ".xml"); // change File ext
                targetfile = targetfile.Replace("\\Data\\", "\\Data\\xml\\"); // change subdir
                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(targetfile)) == false)
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetfile));

                ConvertIni2Xml(listfilePath, targetfile);
                path = targetfile;
            }

            if (filePath.EndsWith(".txt"))
            {
                targetfile = filePath.Replace(".txt", ".xml"); // change File ext
                targetfile = targetfile.Replace("\\Data\\", "\\Data\\xml\\"); // change subdir
                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(targetfile)) == false)
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetfile));
                
                ConvertIni2Xml(listfilePath, targetfile);
                path = targetfile;
            }
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            listListSectionNames.Clear();
        }
        
        public XmlFile(List<string> filePaths, string targetfile)
        {
            //List<string> listfilePath = new List<string>();

            ConvertIni2Xml(filePaths, targetfile);
            path = targetfile;

            xmlrdrdoc = null;
            //arrListSectionNames = null;
        }
        /// <summary>

        /// Write Data to the INI File

        /// </summary>

        /// <PARAM name="Section"></PARAM>

        /// Section name

        /// <PARAM name="Key"></PARAM>

        /// Key Name

        /// <PARAM name="Value"></PARAM>

        /// Value Name
        public void Reload()
        {
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            listListSectionNames.Clear();
        }
        public void Reload(string filename)
        {
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            path = filename;
            listListSectionNames.Clear();
        }
        public void XmlWriteValue(string Section, string Key, string Value)
        {
            //WritePrivateProfileString(Section, Key, Value, this.path);

            // search node
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            //Select the old item with the matching name

            XmlElement root = doc.DocumentElement;
            XmlNode item = root.SelectSingleNode("/INI/Section[Name='" + Section + "']/" + Key);

            if (!(item==null))
                item.InnerText = Value;
            else
            {
                // Create new section with supplied parameters
                XmlElement newSection = doc.CreateElement("Section");
                //newitem.SetAttribute("Name", itemname);
                string innerxml = "<Name>" + Section + "</Name>";
                innerxml = innerxml + "<" + Key + ">" + Value + "</" + Key + ">";

                newSection.InnerXml = innerxml;
                root.AppendChild(newSection);
            }

            //save the output to a file

            doc.Save(path);
            xmlrdrdoc = null;
        }

        /// <summary>

        /// Read Data Value From the Ini File

        /// </summary>

        /// <PARAM name="Section"></PARAM>

        /// <PARAM name="Key"></PARAM>

        /// <PARAM name="Path"></PARAM>

        /// <returns></returns>

        public string XmlReadValue(string Section, string Key)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            string temp = "";

            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("/INI/Section[Name=\"" + Section + "\"]/" + Key);
            foreach (XmlNode xn in xnrdrList)
            {
                temp = xn.InnerText.ToString();
                //Console.WriteLine(xn.InnerText);
            }
            
            return temp;

        }

        public List<string> XmlReadSection(string Section)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            List<string> temp = new List<string>();

            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("/INI/Section[Name=\"" + Section + "\"]");
            foreach (XmlNode xn in xnrdrList)
            {
                foreach (XmlNode cnd in xn.ChildNodes)
                {
                    if (cnd.Name !="Name")
                        temp.Add(cnd.Name + ":" + cnd.InnerText.ToString());
                    //Console.WriteLine(xn.InnerText);
                }
            }

            return temp;

        }

        /*
        public string[] ListSectionNames_strarr()
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (arrListSectionNames == null)
            {
                StringBuilder temp = new StringBuilder(255);


                XPathNavigator navigator = xmlrdrdoc.CreateNavigator();

                XPathExpression expression = navigator.Compile("/GEAR/Item/Name");// "//@Name"); //

                //expression.AddSort("Item"
                expression.AddSort("../Type", XmlSortOrder.Descending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);
                expression.AddSort("../Name", XmlSortOrder.Ascending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);

                XPathNodeIterator iterator = navigator.Select(expression);

                foreach (XPathNavigator item in iterator)
                {
                    temp.Append(item.Value.ToString());
                    temp.Append('\n');
                    //Console.WriteLine(xn.InnerText);
                }
                temp.Length = temp.Length - 1; //remove last element
                arrListSectionNames = temp.ToString().Split('\n');

                XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("//@Name");
                foreach (XmlNode xn in xnrdrList)
                {
                    temp.Append(xn.InnerText.ToString());
                    temp.Append('\n');
                    //Console.WriteLine(xn.InnerText);
                }
                temp.Length = temp.Length - 1; //remove last element
                arrListSectionNames = temp.ToString().Split('\n');
 
            }

            return arrListSectionNames;
            
        } */

        public List<string> stListSectionNames()
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (listListSectionNames.Count == 0)
            {
                //StringBuilder temp = new StringBuilder(255);

                XPathNavigator navigator = xmlrdrdoc.CreateNavigator();

                XPathExpression expression = navigator.Compile("/INI/Section/Name");// "//@Name"); //

                //expression.AddSort("Item"
                expression.AddSort("../Type", XmlSortOrder.Descending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);
                expression.AddSort("../Name", XmlSortOrder.Ascending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);

                XPathNodeIterator iterator = navigator.Select(expression);

                foreach (XPathNavigator item in iterator)
                {
                    listListSectionNames.Add(item.Value.ToString());
                }
            }
            return listListSectionNames;
        }
        public List<string> stListSectionNames(string Type)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (listListSectionNames.Count == 0)
            {
                //StringBuilder temp = new StringBuilder(255);

                XPathNavigator navigator = xmlrdrdoc.CreateNavigator();

                XPathExpression expression = navigator.Compile("/INI/Section[Type=\"" + Type + "\"]/Name");// "//@Name"); //

                //expression.AddSort("Item"
                expression.AddSort("../Type", XmlSortOrder.Descending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);
                expression.AddSort("../Name", XmlSortOrder.Ascending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);

                XPathNodeIterator iterator = navigator.Select(expression);

                foreach (XPathNavigator item in iterator)
                {
                    listListSectionNames.Add(item.Value.ToString());
                }
            }
            return listListSectionNames;
        }

        /*
        public void WriteSectionNames(string SelectionName, string TypeString)
        {
            arrListSectionNames = null;
                //WritePrivateProfileSectionNames(SelectionName, "Type="+TypeString , path);
 
        }*/

        public string GetUniqueName(string basename)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode nd = doc.SelectSingleNode("/INI/Section[Name=\"" + basename + "\"]");
            if (nd == null)
                return basename;  // basename is ok since its not in the xml table already

            string newname;
            int UniqueIndex = 0;
            do // find a name that isn't in the xml table by adding " (Copy <number>)"
            {
                UniqueIndex++;
                newname = basename + " (Copy " + UniqueIndex + ")";
                nd = doc.SelectSingleNode("/INI/Section[Name=\"" + newname + "\"]");
            } while (nd != null);

            xmlrdrdoc = null;
            return newname;
        }

        public void AddItem(string itemname, string itemtype, List<string> itemparts, List<int> itemvalues)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlElement root = doc.DocumentElement;
            XmlElement newSection = doc.CreateElement("Section");
            //newitem.SetAttribute("Name", itemname);
            string innerxml = "";
            //innerxml = "<Type>" + itemtype + "</Type>" +
            innerxml = "<Name>" + itemname + "</Name>" +
                "<Type>" + itemtype + "</Type>" +
                "<Rating>0</Rating>" +
                "<Description>Type in a description here.</Description>";

            for (int PartProgress = 0; PartProgress < itemparts.Count ; PartProgress++)
                innerxml = innerxml + "<Part" + (PartProgress + 1) + ">" + itemparts[PartProgress] + "</Part" + (PartProgress + 1) + ">";

            for (int PartProgress = itemparts.Count; PartProgress < 14; PartProgress++)
                innerxml = innerxml + "<Part" + (PartProgress + 1) + "/>";

            // Add itemvalues
            innerxml = innerxml + "<RemAmmo_Quantity>" + itemvalues[0] + "</RemAmmo_Quantity>";
            innerxml = innerxml + "<Quality>" + itemvalues[1] + "</Quality>";
            innerxml = innerxml + "<Level>" + itemvalues[3] + "</Level>";

            newSection.InnerXml = innerxml;
            root.AppendChild(newSection);

            doc.Save(path);

            listListSectionNames.Add(itemname);
            
            // Read in new
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            //listListSectionNames.Clear();
        }
        public void AddSection(string sectionname, List<string> subsectionnames, List<string> subsectionvalues)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlElement root = doc.DocumentElement;
            XmlElement newSection = doc.CreateElement("Section");
            //newitem.SetAttribute("Name", itemname);
            string innerxml = "";
            //innerxml = "<Type>" + itemtype + "</Type>" +
            innerxml = "<Name>" + sectionname + "</Name>";

            for (int Progress = 0; Progress < subsectionnames.Count; Progress++)
                innerxml = innerxml + "<" + subsectionnames[Progress] + ">" + subsectionvalues[Progress] + "</" + subsectionnames[Progress] + ">";

            newSection.InnerXml = innerxml;
            root.AppendChild(newSection);

            doc.Save(path);

            listListSectionNames.Add(sectionname);

            // Read in new
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            //listListSectionNames.Clear();
        }
        public void RemoveItem(string itemname)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode nd = doc.SelectSingleNode("/INI/Section[Name=\"" + itemname + "\"]");

            nd.ParentNode.RemoveChild(nd);

            doc.Save(path);

            // Read in new
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            listListSectionNames.Remove(itemname);
        }
        public void RenameItem(string olditemname, string newitemname)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlNode nd = doc.SelectSingleNode("/INI/Section[Name=\"" + olditemname + "\"]/Name");
            nd.InnerText = newitemname;
            doc.Save(path);

            // Read in new
            xmlrdrdoc = null;

            int oldindex = listListSectionNames.IndexOf(olditemname);
            if (oldindex != -1)
                listListSectionNames[oldindex] = newitemname;
        }

        private static void ConvertIni2Xml(List<string> ininames, string xmlname)
        {
            // Read the file and display it line by line.
            foreach (string ininame in ininames)
            {
                //check if any of the inifiles is newer than the xml file, that means changes to ini file
                if (System.IO.File.Exists(xmlname))
                {
                    if (System.IO.File.GetLastWriteTimeUtc(ininame) >= System.IO.File.GetLastWriteTimeUtc(xmlname))
                        System.IO.File.Delete(xmlname);
                }
            }
            // XML exists? -> done 
            if (!System.IO.File.Exists(xmlname))
            {
                // Read Ini (Textreader)

                // Write XML
                XmlTextWriter writer = new XmlTextWriter(xmlname, new System.Text.ASCIIEncoding());
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartDocument();
                writer.WriteComment("Comment");
                writer.WriteStartElement("INI");

                string line;
                bool bOpenSection = false;

                // Read the file and display it line by line.
                foreach (string ininame in ininames)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(ininame);
                    while ((line = file.ReadLine()) != null)
                    {
                        // Section
                        if (line.Length > 0) // no empty lines
                        {
                            if (line.StartsWith("[") && line.EndsWith("]")) //Section found
                            {
                                // There should be no "copy" texts
                                if (bOpenSection)
                                {
                                    writer.WriteEndElement();
                                    bOpenSection = false;
                                }
                                if (!bOpenSection)
                                {
                                    writer.WriteStartElement("Section");
                                    bOpenSection = true;
                                }


                                string section = line.Substring(1, line.Length - 2);
                                writer.WriteElementString("Name", section);
                            }
                            if (line.Contains("="))
                            {
                                string propName = line.Substring(0, line.IndexOf("="));
                                propName = propName.Replace("[", "");
                                propName = propName.Replace("]", "");
                                propName = propName.Replace("(", "");
                                propName = propName.Replace(")", "");
                                string propValue = line.Substring(line.IndexOf("=") + 1);

                                if (propValue.StartsWith("\""))
                                {
                                    propValue = propValue.Substring(1, propValue.Length - 2);
                                }
                                writer.WriteElementString(propName, propValue);
                            }
                        }
                    }
                    file.Close();
                }
                if (bOpenSection)
                    writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
        }
        public List<string> XmlSearchSection(string Searchfor)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            List<string> searchresult = new List<string>();

            // Search for //INI/Section[contains(.,"searchtext")]/Name

            //Gives a nodelist of all Name nodes, just iterate and highlight them
            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("//INI/Section[contains(translate(.,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + Searchfor.ToUpperInvariant() + "\")]/Name");
            foreach (XmlNode xn in xnrdrList)
            {
                searchresult.Add(xn.InnerText);
            }

            return searchresult;

        }
    }
}