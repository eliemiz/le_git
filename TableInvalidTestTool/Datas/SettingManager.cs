using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TableInvalidTestTool
{
    class SettingManager
    {
        #region SINGLETON

        private static SettingManager instance;

        public static SettingManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingManager();
                }

                return instance;
            }
        }

        #endregion // SINGLETON

        public ObservableCollection<ExcelInfo> excel_list = new ObservableCollection<ExcelInfo>();
        public ObservableCollection<TagInfo> tag_list = new ObservableCollection<TagInfo>();

        public List<TagGroup> tag_group_list = new List<TagGroup>();


        //public DatabaseInfo database_info = new DatabaseInfo();
        //public FilePathInfo filepath_info = new FilePathInfo();

        public string last_error_message = "";

        public MainWindow main_window = null;

        public bool SaveXml()
        {
            try
            {
                XmlDocument xml = new XmlDocument();

                xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));

                XmlElement root = xml.CreateElement("root");

                // excel files
                {
                    XmlElement elements = xml.CreateElement("excel_list");
                    foreach (ExcelInfo excel in excel_list)
                    {
                        XmlElement element = xml.CreateElement("excel");
                        element.SetAttribute("name", excel.Name);
                        element.SetAttribute("path", excel.Path);

                        elements.AppendChild(element);
                    }

                    root.AppendChild(elements);
                }
                

                // tag list
                {
                    XmlElement elements = xml.CreateElement("tag_list");
                    foreach (TagInfo tag in tag_list)
                    {
                        XmlElement element = xml.CreateElement("tag");
                        element.SetAttribute("tag_name", tag.TagName);

                        elements.AppendChild(element);
                    }

                    root.AppendChild(elements);
                }

                xml.AppendChild(root);
                xml.Save("ExcelToDB.xml");

                return true;
            }
            catch (Exception ex)
            {
                last_error_message = ex.Message;
            }

            return false;
        }

        public bool LoadXml()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("ExcelToDB.xml");

                XmlElement root = xml.DocumentElement;

                // node_db...

                // node_path...

                XmlNodeList node_list = xml.SelectNodes("/root/excel_list/excel");
                if (node_list != null)
                {
                    excel_list.Clear();

                    foreach (XmlElement node in node_list)
                    {
                        ExcelInfo info = new ExcelInfo();
                        info.Name = node.GetAttribute("name");
                        info.Path = node.GetAttribute("path");

                        excel_list.Add(info);
                    }

                    node_list = null;
                }

                // node_tag
                node_list = xml.SelectNodes("/root/tag_list/tag");
                if (node_list != null)
                {
                    tag_list.Clear();

                    foreach (XmlElement node in node_list)
                    {
                        TagInfo info = new TagInfo();
                        info.TagName = node.GetAttribute("tag_name");

                        tag_list.Add(info);
                    }

                    node_list = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                last_error_message = ex.Message;
            }

            return false;
        }

        // ADD FUNCTIONS...
    }

    // ADD CLASS...

}
