using System.Collections.Generic;
using System.Linq;

namespace KrasNonFerMet.DAL.Models
{
    public class MachineSchedule
    {
        public int Id { get; set; }

        public List<Job> Schedule { get; set; } = new List<Job>();

        public Job LastJob => Schedule.Count > 0
            ? Schedule.Last()
            : new Job
            {
                CompleteAt = 0,
                ConsignmentId = -1,
                MachineId = Id,
                StartAt = -1
            };

        public MachineSchedule(int machineId) => Id = machineId;

        public void AddJob(Job job)
        {
            Schedule.Add(job);
        }
    }
}