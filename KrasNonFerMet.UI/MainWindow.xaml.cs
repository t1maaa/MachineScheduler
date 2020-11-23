using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KrasNonFerMet.Core;
using KrasNonFerMet.DAL.Extensions;
using KrasNonFerMet.DAL.Files.Excel;
using KrasNonFerMet.DAL.Models;
using Microsoft.Win32;

namespace KrasNonFerMet.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IExcelReader ExcelReader;
        
       // private void clearTextBox (object sender, MouseButtonEventArgs args) => (sender as TextBox).Text = ""; 
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private List<T> OpenFile<T>(FileInfo file) where T : class, IEntity, new()
        {
            ExcelReader = new ExcelReader(file.FullName);
            return ExcelReader.GetWorksheets().FirstOrDefault()?.ToList<T>();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var type = ((FrameworkElement) (((Button) sender).Parent)).Name;

            string path;
            FileInfo file;
            OpenFileDialog openFileDialog;

            switch (type) //TODO: smth smarter
            {
                case "Consignment":
                    path = TbConsignmentsPath.Text;
                    if (File.Exists(path))
                    {
                        file = new FileInfo(path);
                        if (file.Extension.Equals(".xlsx"))
                        {
                            DgConsignments.ItemsSource = OpenFile<Consignment>(file);
                            return;
                        }
                    }

                    if (Directory.Exists(path))
                    {
                        openFileDialog = new OpenFileDialog
                        {
                            InitialDirectory = path,
                            Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                        };
                        if (openFileDialog.ShowDialog() == true)
                        {
                            DgConsignments.ItemsSource = OpenFile<Consignment>(new FileInfo(openFileDialog.FileName));
                            return;
                        }
                    }
                    
                    openFileDialog = new OpenFileDialog
                    {
                        InitialDirectory = Directory.GetCurrentDirectory(),
                        Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                    };
                    if (openFileDialog.ShowDialog() == true)
                    {
                        DgConsignments.ItemsSource = OpenFile<Consignment>(new FileInfo(openFileDialog.FileName));
                    }
                    return;

                case "Machine":
                    path = TbMachinesPath.Text;
                    if (File.Exists(path))
                    {
                        file = new FileInfo(path);
                        if (file.Extension.Equals(".xlsx"))
                        {
                           DgMachines.ItemsSource = OpenFile<Machine>(file);
                            return;
                        }
                    }

                    if (Directory.Exists(path))
                    {
                        openFileDialog = new OpenFileDialog
                        {
                            InitialDirectory = path,
                            Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                        };
                        if (openFileDialog.ShowDialog() == true)
                        {
                            DgMachines.ItemsSource = OpenFile<Machine>(new FileInfo(openFileDialog.FileName));
                            return;
                        }
                    }

                    openFileDialog = new OpenFileDialog
                    {
                        InitialDirectory = Directory.GetCurrentDirectory(),
                        Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                    };
                    if (openFileDialog.ShowDialog() == true)
                    {
                        DgMachines.ItemsSource = OpenFile<Machine>(new FileInfo(openFileDialog.FileName));
                    }
                    return;
                case "Nomenclature":
                    path = TbNomenclaturesPath.Text;
                    if (File.Exists(path))
                    {
                        file = new FileInfo(path);
                        if (file.Extension.Equals(".xlsx"))
                        {
                            DgNomenclatures.ItemsSource = OpenFile<Nomenclature>(file);
                            return;
                        }
                    }

                    if (Directory.Exists(path))
                    {
                        openFileDialog = new OpenFileDialog
                        {
                            InitialDirectory = path,
                            Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                        };
                        if (openFileDialog.ShowDialog() == true)
                        {
                            DgNomenclatures.ItemsSource = OpenFile<Nomenclature>(new FileInfo(openFileDialog.FileName));
                            return;
                        }
                    }

                    openFileDialog = new OpenFileDialog
                    {
                        InitialDirectory = Directory.GetCurrentDirectory(),
                        Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                    };
                    if (openFileDialog.ShowDialog() == true)
                    {
                        DgNomenclatures.ItemsSource = OpenFile<Nomenclature>(new FileInfo(openFileDialog.FileName));
                    }
                    return;

                case "Operation":
                    path = TbOperationsPath.Text;
                    if (File.Exists(path))
                    {
                        file = new FileInfo(path);
                        if (file.Extension.Equals(".xlsx"))
                        {
                            DgOperations.ItemsSource = OpenFile<Operation>(file);
                            return;
                        }
                    }

                    if (Directory.Exists(path))
                    {
                        openFileDialog = new OpenFileDialog
                        {
                            InitialDirectory = path,
                            Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                        };
                        if (openFileDialog.ShowDialog() == true)
                        {
                            var operations = OpenFile<Operation>(new FileInfo(openFileDialog.FileName));
                                //.Select(o => new {o.MachineId, o.NomenclatureId, o.Duration});
                            DgOperations.ItemsSource = operations;
                            return;
                        }
                    }

                    openFileDialog = new OpenFileDialog
                    {
                        InitialDirectory = Directory.GetCurrentDirectory(),
                        Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
                    };
                    if (openFileDialog.ShowDialog() == true)
                    {
                        DgOperations.ItemsSource = OpenFile<Operation>(new FileInfo(openFileDialog.FileName));
                    }
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BtnScheduleWindow_Click(object sender, RoutedEventArgs e)
        {
            var s = DgConsignments.ItemsSource;

           var result = new SimpleScheduler().MakeSchedule(
                DgConsignments.Items.SourceCollection?.Cast<Consignment>(),
                DgMachines.Items.SourceCollection?.Cast<Machine>(),
                DgNomenclatures.Items.SourceCollection?.Cast<Nomenclature>(),
                DgOperations.Items.SourceCollection?.Cast<Operation>());


            Schedule scheduleWindow = new Schedule(ref result);
            scheduleWindow.Show();
        }


        private void ClearTextBox_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Clear();
        }
    }
}
