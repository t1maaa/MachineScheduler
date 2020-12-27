using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MachineScheduler.DAL.Models
{
    public sealed class Consignment : IEntity
    {
        private int _nomenclatureId;
        
        public int Id { get; init; }

        public int NomenclatureId
        {
            get => _nomenclatureId;
            set
            {
                if (value == _nomenclatureId) return;
                _nomenclatureId = value;
                OnPropertyChanged(nameof(NomenclatureId));
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