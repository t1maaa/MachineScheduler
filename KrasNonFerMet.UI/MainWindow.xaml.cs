using System.IO;
using System.Windows;
using KrasNonFerMet.DAL.Files.Excel;
using Microsoft.Win32;

namespace KrasNonFerMet.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IExcelReader ExcelReader;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConsignments_Click(object sender, RoutedEventArgs e)
        {
            var path = TbConsignmentsPath.Text;
            if (File.Exists(path))
            {
                var file = new FileInfo(path);
                if (file.Extension.Equals("xlsx"))
                {
                    ExcelReader = new ExcelReader(file.DirectoryName, file.Name);
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                }

            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = Directory.GetCurrentDirectory(),
                    Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    var s = openFileDialog.FileName;
                    var file = new FileInfo(s); 
                    //ExcelReader = new ExcelReader(new FileInfo(s) );
                }

            }
        }
    }
}
