using System.IO;
using System.Windows.Input;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers.Operation;

namespace MachineScheduler.ViewModel
{
    public sealed class OperationViewModel : EntityViewModelBase<Operation>
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
                            var context = new OperationParserFactory()
                                .CreateContext(new FileInfo(Filename));
                            UpdateDataInProgress = true;
                            foreach (var operation in context.Parse())
                                Items.Add(operation);
                            UpdateDataInProgress = false;
                            OnPropertyChanged(nameof(Items));
                        }
                    },
                    obj => CanExecute(obj)
                );
            }
        }

        public OperationViewModel(MainWindowViewModel parentViewModel) : base(parentViewModel)
        {
        }
    }
}