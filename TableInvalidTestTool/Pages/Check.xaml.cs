using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TableInvalidTestTool.Pages
{
    /// <summary>
    /// Check.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Check : Page
    {
        public List<TagGroup> tag_group_list = new List<TagGroup>();
        public List<CheckRows> rows = new List<CheckRows>();

        public Check()
        {
            InitializeComponent();

            tag_group_list = SettingManager.Instance.tag_group_list.FindAll(t => t.master_column != null);

            // ObservableCollection<CheckRows> rows = new ObservableCollection<CheckRows>();
            int index = 0;
            foreach (var tag_group in tag_group_list)
            {
                // 1. master
                
                // 2. slave
                foreach (var slave in tag_group.slave_columns)
                {
                    CheckRows row = new CheckRows();
                    row.index = index;
                    row.tag_name = tag_group.tag_name.ToString();
                    row.master_table_name = tag_group.master_column.table_name;
                    row.master_column_name = tag_group.master_column.column_name;
                    row.master_key = row.master_table_name + " : " + row.master_column_name;
                    row.slave_table_name = slave.table_name;
                    row.slave_column_name = slave.column_name;
                    row.slave_key = row.slave_table_name + " : " + row.slave_column_name;

                    rows.Add(row);

                    ++index;
                }
            }

            list_view_rows.ItemsSource = rows;

        }

        private void OnClickCheck(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = list_view_rows.Items.IndexOf(item);
            
            CheckRows row = rows.Find(r => r.index == index);
            if (row == null)
            {
                ModernDialog.ShowMessage("그런 row 없다", "ERROR", MessageBoxButton.OK);
            }

            // Find Master
            TagGroup tag_group = tag_group_list.Find(t => t.master_column.table_name == row.master_table_name && t.master_column.column_name == row.master_column_name);
            if (tag_group == null)
            {
                ModernDialog.ShowMessage("그런 TagGroup 없다", "ERROR", MessageBoxButton.OK);
            }

            List<string> master_list = tag_group.master_column.id_list;

            // Find Slave
            ColumnGroup column_group = tag_group.slave_columns.Find(s => s.table_name == row.slave_table_name && s.column_name == row.slave_column_name);
            if (column_group == null)
            {
                ModernDialog.ShowMessage("그런 ColumnGroup 없다", "ERROR", MessageBoxButton.OK);
            }

            List<string> slave_list = column_group.id_list;

            string added_string = "";
            foreach (var slave in slave_list)
            {
                if (master_list.Find(m => m == slave) == null)
                {
                    string log = string.Format("존재하지 않는 id입니다. {0} : {1}\r\n", tag_group.tag_name, slave);
                    added_string += log;
                }
            }

            if (added_string == "")
            {
                rows[index].Result = "Success";
            }
            else
            {
                rows[index].Result = "Failed";
                result_box.Text += added_string;
            }
            
            result_box.Text += string.Format("Check를 완료했습니다. Checked index : {0}\r\n", index);
            result_box.Text += "=========================================================\r\n";
        }

        // ADD FUNCTIONS...
    }

    // ADD CLASS...
}
