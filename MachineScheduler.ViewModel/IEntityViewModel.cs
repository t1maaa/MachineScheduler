using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MachineScheduler.ViewModel
{
    public interface IEntityViewModel<T> : INotifyPropertyChanged//, INotifyCollectionChanged
    {
        public string Filename { get; set; }
        public ObservableCollection<T> Items { get; set; }
        public ICommand OpenFileCommand { get; }

    }
}