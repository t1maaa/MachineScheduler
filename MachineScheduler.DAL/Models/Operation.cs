using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MachineScheduler.DAL.Models
{
    public sealed class Operation : IEntity
    {
        private int _machineId;
        private int _nomenclatureId;
        private int _duration;

        public int MachineId
        {
            get => _machineId;
            set
            {
                if (value == _machineId) return;
                _machineId = value;
                OnPropertyChanged(nameof(MachineId));
            }
        }

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

        public int Duration
        {
            get => _duration;
            set
            {
                if (value == _duration) return;
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public int Id { get; init; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}