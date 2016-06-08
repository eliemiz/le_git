using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace TableInvalidTestTool.Pages
{
    /// <summary>
    /// Edit.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Edit : Page
    {
        List<TableInfo> table_list = new List<TableInfo>();

        Dictionary<string, List<string>> column_data_list = new Dictionary<string, List<string>>();

        TableInfo selected_table_info;

        public Edit()
        {
            InitializeComponent();

            Load();
        }

        public void Load()
        {
            // 로드 전 데이터 초기화
            table_list.Clear();
            column_data_list.Clear();
            tree_view_sheets.Items.Clear();

            try
            {
                // excel_list에 등록된 모든 테이블들에 대해 다음을 실행한다.
                foreach (ExcelInfo excel_info in SettingManager.Instance.excel_list)
                {
                    // Check 되어있지 않은 테이블의 경우 그냥 패스한다.
                    if (excel_info.Check == false)
                    {
                        continue;
                    }

                    Excel.Application excel_app = new Excel.Application();
                    Excel.Workbook work_book = excel_app.Workbooks.Open(excel_info.Path,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    // 부모 트리뷰(Excel File Name)
                    TreeViewItem new_parent = new TreeViewItem();
                    new_parent.Header = excel_info.Name;

                    foreach (Excel.Worksheet work_sheet in work_book.Worksheets)
                    {

                        // #1. sheet name에 Define.SHEET_PREFIX("BT_")가 포함되어있지 않을 경우 해당 sheet 패스
                        string sheet_prefix = Define.SHEET_PREFIX;
                        if (System.Text.RegularExpressions.Regex.IsMatch(work_sheet.Name, sheet_prefix) == false)
                        {
                            continue;
                        }

                        // #2. table_list에 추가할 새로운 TableInfo 생성
                        TableInfo new_table = new TableInfo();
                        new_table.TableName = work_sheet.Name;
                        new_table.ExcelFilePath = excel_info.Path;

                        // 자식 트리뷰(Sheet Name)
                        TreeViewItem new_child = new TreeViewItem();
                        new_child.Header = work_sheet.Name;
                        new_parent.Items.Add(new_child);

                        foreach (Excel.ListObject list_object in work_sheet.ListObjects)
                        {
                            // ???
                            if (list_object.Name != work_sheet.Name)
                            {
                                continue;
                            }

                            foreach (Excel.ListColumn list_column in list_object.ListColumns)
                            {
                                // ^ : 뒤에 오는 문자열로 시작
                                // [A-Za-z0-9_] : 알파벳, 숫자, _(언더바) 중 하나
                                // + : 1번 이상 반복됨
                                // $ : 앞에 오는 문자열로 끝남
                                // => 알파벳, 숫자, _(언더바)로 이루어진 문자열
                                string regular_expression = "^[A-Za-z0-9_]+$";

                                // 유효하지 않은 문자가 있을 경우 패스(column에 # 등이 존재할 경우 해당 column을 인식하지 않는다.)
                                if (System.Text.RegularExpressions.Regex.IsMatch(list_column.Name, regular_expression) == false)
                                {
                                    continue;
                                }

                                // column명 길이가 1 이하일 경우 패스(적어도 2자 이상이어야 함)
                                if (list_column.Name.Length <= 1)
                                {
                                    continue;
                                }

                                // 새 ColumnInfo 생성 후 위에서 생성한 테이블에 컬럼 추가
                                ColumnInfo column_info = new ColumnInfo();
                                column_info.ColumnName = list_column.Name;
                                new_table.ColumnList.Add(column_info);

                                Excel.Range range = list_column.DataBodyRange;

                                Array values = range.Value as Array;

                                if (values != null)
                                {
                                    List<string> datas = new List<string>();
                                    foreach (object obj in values)
                                    {
                                        string value = null;
                                        if (obj != null)
                                        {
                                            value = obj.ToString();
                                        }

                                        if (value != null)
                                        {
                                            datas.Add(value);
                                        }
                                        else
                                        {
                                            datas.Add("");
                                        }
                                    }

                                    column_data_list.Add(work_sheet.Name + "  " + column_info.ColumnName, datas);
                                }
                                else
                                {
                                    List<string> datas = new List<string>();
                                    datas.Add(range.Value.ToString());

                                    column_data_list.Add(work_sheet.Name + "  " + column_info.ColumnName, datas);
                                }
                            }
                        }

                        if (new_table.ColumnList.Count > 0)
                        {
                            table_list.Add(new_table);
                        }
                    }

                    tree_view_sheets.Items.Add(new_parent);
                    ExcelDispose(excel_app, work_book);
                }

                // (ExcelToDB 기준 : 각 컬럼의 설정을 저장하는 곳(server_dictionary 뭐 이런거...)
                XmlDocument xml = new XmlDocument();
                xml.Load("table_info_list.xml");

                XmlElement root = (XmlElement)xml.SelectSingleNode("root");

                foreach (TableInfo table in table_list)
                {
                    Debug.WriteLine("Table : " + table.TableName);

                    XmlElement element_table = (XmlElement)root.SelectSingleNode(table.TableName.ToLower());
                    if (element_table != null)
                    {
                        foreach (ColumnInfo column in table.ColumnList)
                        {
                            Debug.WriteLine("Column : " + column.ColumnName);

                            XmlElement element_column = (XmlElement)element_table.SelectSingleNode(column.ColumnName.ToLower());
                            if (element_column != null)
                            {
                                // column.TagName = Convert.ToString(element_column.GetAttribute("tag_name"));
                                column.TagName = (eTagName)Enum.Parse(typeof(eTagName), element_column.GetAttribute("tag_name"));
                                column.IsMaster = Convert.ToBoolean(element_column.GetAttribute("is_master"));
                                column.Condition_1 = Convert.ToString(element_column.GetAttribute("condition_1"));
                                column.Condition_2 = Convert.ToString(element_column.GetAttribute("condition_2"));
                                column.Condition_3 = Convert.ToString(element_column.GetAttribute("condition_3"));
                                column.Condition_4 = Convert.ToString(element_column.GetAttribute("condition_4"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error_message = string.Format("[ERROR] message : {0}, stack_trace : {1}", ex.Message, ex.StackTrace);

                ModernDialog.ShowMessage(error_message, "Error", MessageBoxButton.OK);
            }
        }


        #region 메모리 해제

        public static void ExcelDispose(Excel.Application excel_app, Excel.Workbook work_book, Excel._Worksheet work_sheet = null)
        {
            // work_book.Close(Type.Missing, Type.Missing, Type.Missing);
            work_book.Close(false, Type.Missing, Type.Missing); // : 현재 자리에서 문제가 발생했었다?

            excel_app.Quit();
            ReleaseObject(excel_app);
            ReleaseObject(work_sheet);
            ReleaseObject(work_book);

            // Excel 프로세스 삭제
            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

        }

        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


        #endregion // 메모리 해제


        private void OnSelectTreeViewSheets(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)e.NewValue;

            foreach (TableInfo table in table_list)
            {
                if (table.TableName == item.Header.ToString())
                {
                    data_grid_table.ItemsSource = null;
                    data_grid_table.ItemsSource = table.ColumnList;

                    data_grid_table.UpdateLayout();

                    selected_table_info = table;
                }
            }
        }

        private void OnSelectDataGridTable(object sender, SelectionChangedEventArgs e)
        {
            if (data_grid_table.SelectedCells != null && data_grid_table.SelectedCells.Count > 0)
            {
                DataGridCellInfo info = data_grid_table.SelectedCells[0];
                if (info != null)
                {
                    ColumnInfo selected = info.Item as ColumnInfo;
                    if (selected != null)
                    {
                        string name = selected.ColumnName;

                        List<string> data_list = column_data_list[selected_table_info.TableName + "  " + name];
                        if (data_list != null)
                        {
                            list_box_column_datas.ItemsSource = null;
                            list_box_column_datas.ItemsSource = data_list;
                        }
                    }
                }
            }
        }

        private void OnClickSave(object sender, RoutedEventArgs e)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = null;

            if (File.Exists("table_info_list.xml"))
            {
                xml.Load("table_info_list.xml");

                root = (XmlElement)xml.SelectSingleNode("root");
            }
            else
            {
                xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));

                root = xml.CreateElement("root");
            }

            foreach (TableInfo table in table_list)
            {
                XmlElement prev_element = (XmlElement)root.SelectSingleNode(table.TableName.ToLower());
                if (prev_element != null)
                {
                    root.RemoveChild(prev_element);
                }

                XmlElement element_table = xml.CreateElement(table.TableName.ToLower());
                element_table.SetAttribute("name", table.TableName);
                element_table.SetAttribute("excel_file_path", table.ExcelFilePath);

                foreach (ColumnInfo column in table.ColumnList)
                {
                    XmlElement element_column = xml.CreateElement(column.ColumnName.ToLower());
                    element_column.SetAttribute("tag_name", column.TagName.ToString());
                    element_column.SetAttribute("is_master", column.IsMaster.ToString());
                    element_column.SetAttribute("condition_1", column.Condition_1 ?? "");
                    element_column.SetAttribute("condition_2", column.Condition_2 ?? "");
                    element_column.SetAttribute("condition_3", column.Condition_3 ?? "");
                    element_column.SetAttribute("condition_4", column.Condition_4 ?? "");

                    element_table.AppendChild(element_column);
                }

                root.AppendChild(element_table);
            }

            if (File.Exists("table_info_list.xml") == false)
            {
                xml.AppendChild(root);
            }

            xml.Save("table_info_list.xml");

            ModernDialog.ShowMessage("저장했습니다.", "Save", MessageBoxButton.OK);

        }

        private bool Register(TableInfo table, ColumnInfo column, string condition = null)
        {
            // #0. tag_name
            eTagName tag_name = column.TagName;

            // #1. 기본 리스트(base_list)를 로드한다 / 밖으로 뺄 result_list 생성
            ColumnGroup column_group = new ColumnGroup();
            column_group.table_name = table.TableName;
            column_group.column_name = column.ColumnName;

            List<string> base_list = new List<string>();
            List<string> result_list = new List<string>();

            string base_key = table.TableName + "  " + column.ColumnName;
            if (column_data_list.TryGetValue(base_key, out base_list) == false)
            {
                ModernDialog.ShowMessage("Not found column key", "ERROR", MessageBoxButton.OK);
                return false;
            }

            // #2. 조건이 있는지 검색
            if (condition != null && condition.Contains(':'))
            {
                // #3. 조건이 있을 경우
                // 조건을 파싱
                string[] split_string = condition.Split(':');
                if (split_string.Length != 3)
                {
                    ModernDialog.ShowMessage("Not valid condition string", "ERROR", MessageBoxButton.OK);
                    return false;
                }

                tag_name = (eTagName)Enum.Parse(typeof(eTagName), split_string[0]);
                string condition_name = split_string[1];
                string condition_value = split_string[2];

                // 조건에 해당하는 컨디션 키 생성
                string condition_key = table.TableName + "  " + condition_name;

                // 컨디션 리스트 로드
                List<string> condition_list = new List<string>();
                if (column_data_list.TryGetValue(condition_key, out condition_list) == false)
                {
                    ModernDialog.ShowMessage("Not found condition key", "ERROR", MessageBoxButton.OK);
                    return false;
                }

                // 조건에 해당하는 row에 한해 result_list에 넣기
                int index = 0;
                foreach (string condition_string in condition_list)
                {
                    if (condition_string == condition_value)
                    {
                        result_list.Add(base_list[index]);
                    }

                    ++index;
                }

            }
            else
            {
                // #3. 조건이 없을 경우
                result_list = base_list;
            }

            // #4. column_group.id_list 세팅하기
            column_group.id_list = result_list;

            // #5. master column이라면 master에 추가
            if (column.IsMaster == true)
            {
                TagGroup tag_group = SettingManager.Instance.tag_group_list.Find(t => t.tag_name == tag_name);
                if (tag_group != null)
                {
                    if (tag_group.master_column != null)
                    {
                        ModernDialog.ShowMessage("Has master column already", "ERROR", MessageBoxButton.OK);
                        return false;
                    }

                    tag_group.master_column = column_group;
                }
                else
                {
                    tag_group = new TagGroup();
                    tag_group.tag_name = tag_name;
                    tag_group.master_column = column_group;
                    tag_group.slave_columns = new List<ColumnGroup>();

                    SettingManager.Instance.tag_group_list.Add(tag_group);
                }
            }
            else    // #5. slave column이라면 slave에 추가
            {
                TagGroup tag_group = SettingManager.Instance.tag_group_list.Find(t => t.tag_name == tag_name);
                if (tag_group != null)
                {
                    tag_group.slave_columns.Add(column_group);
                }
                else
                {
                    tag_group = new TagGroup();
                    tag_group.tag_name = tag_name;
                    tag_group.slave_columns = new List<ColumnGroup>();
                    tag_group.slave_columns.Add(column_group);

                    SettingManager.Instance.tag_group_list.Add(tag_group);
                }
            }

            return true;
        }

        private void OnClickMakeList(object sender, RoutedEventArgs e)
        {
            // 리스트 클리어 기능 추가할 것
            SettingManager.Instance.tag_group_list.Clear();


            // eTagName의 각 태그를 리스트로 변환
            List<eTagName> tag_list = Enum.GetValues(typeof(eTagName)).Cast<eTagName>().ToList();

            // 각 테이블 순회
            foreach (TableInfo table in table_list)
            {
                // 각 컬럼 순회
                foreach (ColumnInfo column in table.ColumnList)
                {

                    if (column.TagName == eTagName.None)    // #1. None...
                    {
                        continue;
                    }
                    else if (column.TagName == eTagName.MultipleTag)    // #2. MultipleTag...
                    {
                        // 조건을 파싱
                        if (column.Condition_1 != null && column.Condition_1.Contains(':'))
                        {
                            if (Register(table, column, column.Condition_1) == false)
                            {
                                return;
                            }
                        }

                        if (column.Condition_2 != null && column.Condition_2.Contains(':'))
                        {
                            if (Register(table, column, column.Condition_2) == false)
                            {
                                return;
                            }
                        }

                        if (column.Condition_3 != null && column.Condition_3.Contains(':'))
                        {
                            if (Register(table, column, column.Condition_3) == false)
                            {
                                return;
                            }
                        }

                        if (column.Condition_4 != null && column.Condition_4.Contains(':'))
                        {
                            if (Register(table, column, column.Condition_4) == false)
                            {
                                return;
                            }
                        }
                    }
                    else   // #3. The others...
                    {
                        if (Register(table, column) == false)
                        {
                            return;
                        }
                    }


                }
            }

            ModernDialog.ShowMessage("리스트 생성 완료", "SUCCESS", MessageBoxButton.OK);
        }

        private void OnClickNext(object sender, RoutedEventArgs e)
        {
            SettingManager.Instance.main_window.NextPage("/Pages/Check.xaml");
        }

        // ADD FUNCTIONS...
    }

    // ADD CLASS...
}
