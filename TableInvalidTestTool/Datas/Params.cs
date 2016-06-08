using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableInvalidTestTool
{
    public enum eTagName
    {
        None = 0,
        MultipleTag,
        ship_id,
        crew_id,
        part_id,
        item_id,
        edifice_id,
        edifice_type,
        tier_id,
        reward_id,
        stage_id,
    }



    #region SETTING MANAGER

    public class ExcelInfo : INotifyPropertyChanged
    {
        private string name;
        private string path;
        private bool check;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("name");
            }
        }

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyPropertyChanged("path");
            }
        }

        public bool Check
        {
            get { return check; }
            set
            {
                check = value;
                NotifyPropertyChanged("check");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }

        // Add FUNCTIONS...
    }

    public class TagInfo : INotifyPropertyChanged
    {
        private string tag_name;
        private bool check;

        public string TagName
        {
            get { return tag_name; }
            set
            {
                tag_name = value;
                NotifyPropertyChanged("tag_name");
            }
        }

        public bool Check
        {
            get { return check; }
            set
            {
                check = value;
                NotifyPropertyChanged("check");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }

        // Add FUNCTIONS...
    }

    #endregion // SETTING MANAGER



    #region TABLE DATA

    public class ColumnInfo : INotifyPropertyChanged
    {
        private string column_name;
        private eTagName tag_name;
        private bool is_master;
        private string condition_1;
        private string condition_2;
        private string condition_3;
        private string condition_4;

        public string ColumnName
        {
            get { return column_name; }
            set
            {
                column_name = value;
                NotifyPropertyChanged("column_name");
            }
        }

        public eTagName TagName
        {
            get { return tag_name; }
            set
            {
                tag_name = value;
                NotifyPropertyChanged("tag_name");
            }
        }

        public bool IsMaster
        {
            get { return is_master; }
            set
            {
                is_master = value;
                NotifyPropertyChanged("is_master");
            }
        }

        public string Condition_1
        {
            get { return condition_1; }
            set
            {
                condition_1 = value;
                NotifyPropertyChanged("condition_1");
            }
        }

        public string Condition_2
        {
            get { return condition_2; }
            set
            {
                condition_2 = value;
                NotifyPropertyChanged("condition_2");
            }
        }

        public string Condition_3
        {
            get { return condition_3; }
            set
            {
                condition_3 = value;
                NotifyPropertyChanged("condition_3");
            }
        }

        public string Condition_4
        {
            get { return condition_4; }
            set
            {
                condition_4 = value;
                NotifyPropertyChanged("condition_4");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // ADD FUNCTIONS...
    }

    public class TableInfo : INotifyPropertyChanged
    {
        private string table_name;
        private string excel_file_path;

        private ObservableCollection<ColumnInfo> column_list = new ObservableCollection<ColumnInfo>();

        public string TableName
        {
            get { return table_name; }
            set
            {
                table_name = value;
                NotifyPropertyChanged("table_name");
            }
        }

        public string ExcelFilePath
        {
            get { return excel_file_path; }
            set
            {
                excel_file_path = value;
                NotifyPropertyChanged("excel_file_path");
            }
        }

        public ObservableCollection<ColumnInfo> ColumnList
        {
            get { return column_list; }
            set
            {
                column_list = value;
                NotifyPropertyChanged("column_list");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }

    }

    #endregion // TABLE DATA



    #region TAG GROUP DATA

    public class TagGroup
    {
        public eTagName tag_name;
        public ColumnGroup master_column;
        public List<ColumnGroup> slave_columns;
    }

    public class ColumnGroup
    {
        public string table_name;
        public string column_name;
        public List<string> id_list;
    }

    #endregion // TAG GROUP DATA

    #region CHECK ROWS DATA
    
    public class CheckRows : INotifyPropertyChanged
    {
        public int index;
        public string tag_name;
        public string master_key;
        public string slave_key;

        public string master_table_name;
        public string master_column_name;
        public string slave_table_name;
        public string slave_column_name;

        public string result;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                NotifyPropertyChanged("index");
            }
        }

        public string TagName
        {
            get { return tag_name; }
            set
            {
                tag_name = value;
                NotifyPropertyChanged("tag_name");
            }
        }

        public string MasterKey
        {
            get { return master_key; }
            set
            {
                master_key = value;
                NotifyPropertyChanged("master_key");
            }
        }

        public string SlaveKey
        {
            get { return slave_key; }
            set
            {
                slave_key = value;
                NotifyPropertyChanged("slave_key");
            }
        }

        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                NotifyPropertyChanged("result");
            }
        }
            
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }

    }

    #endregion


    #region NOT USING

    // ADD CLASS...

    //public class DatabaseInfo : INotifyPropertyChanged
    //{
    //    private string ip;
    //    private string port;
    //    private string name;
    //    private string id;
    //    private string password;

    //    public string IP
    //    {
    //        get { return ip; }
    //        set 
    //        {
    //            ip = value;
    //            NotifyPropertyChanged("ip");
    //        }
    //    }

    //    public string Port
    //    {
    //        get { return port; }
    //        set 
    //        {
    //            port = value;
    //            NotifyPropertyChanged("port");
    //        }
    //    }

    //    public string Name
    //    {
    //        get { return name; }
    //        set
    //        {
    //            name = value;
    //            NotifyPropertyChanged("name");
    //        }
    //    }

    //    public string Id
    //    {
    //        get { return id; }
    //        set 
    //        {
    //            id = value;
    //            NotifyPropertyChanged("id");
    //        }
    //    }

    //    public string Password
    //    {
    //        get { return password; }
    //        set
    //        {
    //            password = value;
    //            NotifyPropertyChanged("password");
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public void NotifyPropertyChanged(string property_name)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(property_name));
    //        }
    //    }
    //}

    //public class FilePathInfo : INotifyPropertyChanged
    //{
    //    private string xml_file_path;
    //    private string client_source_path;
    //    private string server_source_path;

    //    public string XmlFilePath
    //    {
    //        get { return xml_file_path; }
    //        set { xml_file_path = value; }
    //    }

    //    public string ClientSourcePath
    //    {
    //        get { return client_source_path; }
    //        set { client_source_path = value; }
    //    }

    //    public string ServerSourcePath
    //    {
    //        get { return server_source_path; }
    //        set { server_source_path = value; }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public void NotifyPropertyChanged(string property_name)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(property_name));
    //        }
    //    }
    //}

    #endregion // NOT USING
}
