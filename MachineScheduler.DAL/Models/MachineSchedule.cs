using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MachineScheduler.DAL.Models
{
    public sealed class MachineSchedule : IEntity
    {
        private readonly Job _emptyJob;
        public int Id { get; init; }

        public ObservableCollection<Job> Schedule { get; }

        public Job LastJob => Schedule.Count > 0
            ? Schedule.Last()
            : _emptyJob;

        public MachineSchedule(int machineId)
        {
            Id = machineId;
            Schedule = new ObservableCollection<Job>();
            _emptyJob = new Job
            {
                CompleteAt = 0,
                ConsignmentId = -1,
                MachineId = Id,
                StartAt = -1,
                Nomenclature = ""
            };
        }

        public void AddJob(Job job)
        {
            Schedule.Add(job);
            OnPropertyChanged(nameof(LastJob));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}