using System.ComponentModel;

namespace MachineScheduler.DAL.Models
{
    public interface IEntity : INotifyPropertyChanged
    {
        public int Id { get; init; }
    }
}