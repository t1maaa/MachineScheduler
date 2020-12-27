using System.IO;
using System.Windows.Input;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers.Nomenclature;

namespace MachineScheduler.ViewModel
{
    public sealed class NomenclatureViewModel : EntityViewModelBase<Nomenclature>
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
                            var context = new NomenclatureParserFactory()
                                .CreateContext(new FileInfo(Filename));
                            UpdateDataInProgress = true;
                            foreach (var nomenclature in context.Parse())
                                Items.Add(nomenclature);
                            UpdateDataInProgress = false;
                            OnPropertyChanged(nameof(Items));
                        }
                    },
                    obj => CanExecute(obj)
                );
            }
        }

        public NomenclatureViewModel(MainWindowViewModel parentViewModel) : base(parentViewModel)
        {
        }
    }
}