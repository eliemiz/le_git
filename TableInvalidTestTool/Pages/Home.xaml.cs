using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TableInvalidTestTool
{
    /// <summary>
    /// Home.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : Page
    {
        ObservableCollection<ExcelInfo> excel_list;
        ObservableCollection<TagInfo> tag_list;

        public Home()
        {
            InitializeComponent();

            excel_list = SettingManager.Instance.excel_list;
            tag_list = SettingManager.Instance.tag_list;

            // grid_data_base...
            // grid_file_path...

            list_view_excel.ItemsSource = excel_list;
            list_view_tag.ItemsSource = tag_list;

            if (SettingManager.Instance.LoadXml() == false)
            {
                string error_log = string.Format("[ERROR] Xml Load Error : {0}", SettingManager.Instance.last_error_message);
                ModernDialog.ShowMessage(error_log, "읽기 실패", System.Windows.MessageBoxButton.OK);

                return;
            }

        }

        //
        private void OnClickCheckAllTable(object sender, RoutedEventArgs e)
        {
            CheckBox check_box = (CheckBox)sender;

            foreach (ExcelInfo info in excel_list)
            {
                info.Check = (bool)check_box.IsChecked;
            }
        }

        private void OnClickAddTable(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("AddTable clicked");

            OpenFileDialog file_dialog = new OpenFileDialog();

            file_dialog.Multiselect = false;
            file_dialog.Filter = "Excel files (*.xls, *.xlsx, *.xlsm)|*.xls; *.xlsx; *.xlsm";
            file_dialog.Title = "Select excel file to add!";

            bool? result = file_dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string file_name = file_dialog.SafeFileName;        // 파일 이름만 들어있는 문자열
                string file_path = file_dialog.FileName;            // 전체 경로가 들어있는 문자열

                foreach (ExcelInfo info in excel_list)
                {
                    if (info.Name == file_name)
                    {
                        string err_log = string.Format("같은 이름의 파일이 이미 등록되어있습니다.\r\nFile Name : {0}", file_name);
                        ModernDialog.ShowMessage(err_log, "Error", System.Windows.MessageBoxButton.OK);

                        return;
                    }
                }

                ExcelInfo new_info = new ExcelInfo();
                new_info.Name = file_name;
                new_info.Path = file_path;

                excel_list.Add(new_info);

                foreach (GridViewColumn column in (list_view_excel.View as GridView).Columns)
                {
                    column.Width = column.ActualWidth;
                    column.Width = double.NaN;
                }
            }

        }

        private void OnClickRemoveTable(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("RemoveTable clicked");

            string file_list = "";

            List<ExcelInfo> remove_list = new List<ExcelInfo>();

            // check 되어있는 파일에 대해 remove_list에 추가
            foreach (ExcelInfo info in excel_list)
            {
                if (info.Check == true)
                {
                    file_list += info.Name;
                    file_list += "\r\n";

                    remove_list.Add(info);
                }
            }

            // 선택(체크)된 파일이 없을 경우
            if (remove_list.Count == 0)
            {
                ModernDialog.ShowMessage("제거할 파일을 선택해주세요", "Delete", System.Windows.MessageBoxButton.OK);
                
                return;
            }

            MessageBoxResult result = ModernDialog.ShowMessage("등록된 리스트\r\n" + file_list + "제거하시겠습니까?", "Delete", System.Windows.MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                foreach (ExcelInfo info in remove_list)
                {
                    if (info.Check == true)
                    {
                        excel_list.Remove(info);
                    }
                }

                remove_list.Clear();
            }
        }

        private void OnClickCheckAllTag(object sender, RoutedEventArgs e)
        {
            CheckBox check_box = (CheckBox)sender;

            foreach (TagInfo info in tag_list)
            {
                info.Check = (bool)check_box.IsChecked;
            }
        }

        private void OnClickAddTag(object sender, RoutedEventArgs e)
        {
            string tag_string = text_box_tag.Text;
            if (tag_string.Length == 0)
            {
                ModernDialog.ShowMessage("태그 이름을 입력하세요.", "Add Tag", MessageBoxButton.OK);
                return;
            }

            foreach (var tag in tag_list)
            {
                if (tag.TagName == tag_string)
                {
                    ModernDialog.ShowMessage("해당하는 태그가 이미 존재합니다.\r\n새로운 태그 이름을 입력하세요.", "Add Tag", MessageBoxButton.OK);
                    return;
                }
            }

            TagInfo new_tag = new TagInfo();
            new_tag.TagName = tag_string;

            tag_list.Add(new_tag);

            text_box_tag.Text = "";
        }

        private void OnClickRemoveTag(object sender, RoutedEventArgs e)
        {
            string tags = "";

            List<TagInfo> remove_list = new List<TagInfo>();

            // check 되어있는 태그에 대해 remove_list에 추가
            foreach (TagInfo info in tag_list)
            {
                if (info.Check == true)
                {
                    tags += info.TagName;
                    tags += "\r\n";

                    remove_list.Add(info);
                }
            }

            // 선택된 파일이 없을 경우
            if (remove_list.Count == 0)
            {
                ModernDialog.ShowMessage("제거할 태그를 선택하세요.", "Remove Tag", MessageBoxButton.OK);
                return;
            }

            MessageBoxResult result = ModernDialog.ShowMessage("등록된 태그\r\n" + tags + "제거하시겠습니까?", "Remove", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                foreach (TagInfo info in remove_list)
                {
                    if (info.Check == true)
                    {
                        tag_list.Remove(info);
                    }
                }

                remove_list.Clear();
            }
        }



        private void OnClickStart(object sender, RoutedEventArgs e)
        {
            ExcelInfo[] ordered_excel_list = excel_list.OrderBy(x => x.Name).ToArray();
            excel_list.CopyTo(ordered_excel_list, 0);

            SettingManager.Instance.main_window.NextPage("/Pages/Edit.xaml");

            SettingManager.Instance.SaveXml();
        }


        // ADD FUNCTIONS...
    }

    // ADD CLASS...
}
