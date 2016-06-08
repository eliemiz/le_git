using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace TableInvalidTestTool
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.DispatcherUnhandledException += UnExpected;
            SettingManager.Instance.main_window = this;
        }

        public void NextPage(string path)
        {
            Uri uri = new Uri(path, UriKind.Relative);

            this.ContentSource = uri;
        }

        public void UnExpected(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format("Dump_{0:yyyyMMdd}.txt", DateTime.Now), true))
            {
                file.WriteLine(string.Format("[{0:yyyy-MM-dd HH:mm:ss}] Application Name", DateTime.Now));
                file.WriteLine("Module : " + e.Exception.Source);
                file.WriteLine("Message : " + e.Exception.Message);
                file.WriteLine(e.Exception.StackTrace.ToString());
                file.WriteLine("\n\n");

                file.Flush();
                file.Close();
            }
        }
    }
}
