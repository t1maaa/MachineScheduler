using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MachineScheduler.DAL.Models
{
    public sealed class Nomenclature : IEntity
    {
        private string _name;
        public int Id { get; init; }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}