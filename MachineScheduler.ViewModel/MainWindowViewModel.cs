using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MachineScheduler.Core;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers;

namespace MachineScheduler.ViewModel
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MachineSchedule> _schedules;
        public ConsignmentViewModel ConsignmentViewModel { get; }
        public MachineViewModel MachineViewModel { get; }
        public NomenclatureViewModel NomenclatureViewModel { get; }
        public OperationViewModel OperationViewModel { get; }

        public ObservableCollection<MachineSchedule> Schedules
        {
            get => _schedules;
            private set
            {
                if(value == _schedules) return;
                _schedules = value;
               // OnPropertyChanged(nameof(Schedules));
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _inputDataChanged = false;

        private ICommand _makeSchedule;
        

        public MainWindowViewModel(string consignmentsPath, string machinesPath, string nomenclaturesPath, string operationsPath)
        {
            ConsignmentViewModel = new ConsignmentViewModel(this);
            ConsignmentViewModel.PropertyChanged += OnInputDataChanged;
            if (ConsignmentViewModel.OpenFileCommand.CanExecute(consignmentsPath))
                ConsignmentViewModel.OpenFileCommand.Execute(consignmentsPath); //TODO:try execute

            MachineViewModel = new MachineViewModel(this);
            MachineViewModel.PropertyChanged += OnInputDataChanged;
            if (MachineViewModel.OpenFileCommand.CanExecute(machinesPath))
                MachineViewModel.OpenFileCommand.Execute(machinesPath);

            NomenclatureViewModel = new NomenclatureViewModel(this);
            NomenclatureViewModel.PropertyChanged += OnInputDataChanged;
            if (NomenclatureViewModel.OpenFileCommand.CanExecute(nomenclaturesPath))
                NomenclatureViewModel.OpenFileCommand.Execute(nomenclaturesPath);

            OperationViewModel = new OperationViewModel(this);
            OperationViewModel.PropertyChanged += OnInputDataChanged;
            if (OperationViewModel.OpenFileCommand.CanExecute(operationsPath))
                OperationViewModel.OpenFileCommand.Execute(operationsPath);


            if (MakeSchedule.CanExecute(null))
                MakeSchedule.Execute(null);
            else Schedules = new ObservableCollection<MachineSchedule>();
        }

        public ICommand MakeSchedule
        {
            get
            {
                return _makeSchedule ??= new RelayCommand(data =>
                {
                    if (_inputDataChanged)
                    {
                        try
                        {
                            Schedules = new SimpleScheduler()
                                .MakeSchedule(
                                    ConsignmentViewModel.Items,
                                    MachineViewModel.Items,
                                    NomenclatureViewModel.Items,
                                    OperationViewModel.Items);
                        }
                        catch (ArgumentException e)
                        {
                            throw;
                        }
                        finally
                        {
                            OnPropertyChanged(nameof(Schedules));
                            _inputDataChanged = false;
                            
                        }
                    }
                }, data => ConsignmentViewModel?.Items.Count > 0
                           && MachineViewModel?.Items.Count > 0
                           && NomenclatureViewModel?.Items.Count > 0
                           && OperationViewModel?.Items.Count > 0);
            }
        }

        public static string FileTypesFilter => SupportedFiletypes.GetFilterString();
        
        private void OnInputDataChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName.Equals("Items")) {
                
                _inputDataChanged = true;
                
                    if(MakeSchedule.CanExecute(null))
                        MakeSchedule.Execute(null);
            }
        }
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // if (propertyName.Equals(nameof(Items)))
                 _inputDataChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}