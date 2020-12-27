using System.IO;
using System.Windows.Input;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers.Consignment;

namespace MachineScheduler.ViewModel
{
    public sealed class ConsignmentViewModel : EntityViewModelBase<Consignment>
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
                            var context = new ConsignmentParserFactory()
                                .CreateContext(new FileInfo(Filename));
                            UpdateDataInProgress = true;
                           foreach (var consignment in context.Parse())
                                Items.Add(consignment);
                            UpdateDataInProgress = false;
                            OnPropertyChanged(nameof(Items));
                        }
                    },
                    obj => CanExecute(obj)
                );
            }
        }

        public ConsignmentViewModel(MainWindowViewModel parentViewModel) : base(parentViewModel)
        {
        }
    }
}