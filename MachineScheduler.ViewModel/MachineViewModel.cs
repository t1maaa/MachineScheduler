using System;
using System.IO;
using System.Windows.Input;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers.Machine;

namespace MachineScheduler.ViewModel
{
    public sealed class MachineViewModel : EntityViewModelBase<Machine>
    {
        public override ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ??= new RelayCommand(obj =>
                    {
                        if (!string.IsNullOrWhiteSpace(Filename))
                        {
                            if (!WillExecute()) return;
                            try
                            {
                                var context = new MachineParserFactory()
                                    .CreateContext(new FileInfo(Filename));
                                UpdateDataInProgress = true;
                                foreach (var machine in context.Parse())
                                    Items.Add(machine);
                                UpdateDataInProgress = false;
                                OnPropertyChanged(nameof(Items));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    },
                    obj => CanExecute(obj)
                );
            }
        }

        public MachineViewModel(MainWindowViewModel parentViewModel) : base(parentViewModel)
        {
        }
    }
}