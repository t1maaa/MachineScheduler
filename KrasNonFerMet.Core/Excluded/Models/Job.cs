namespace KrasNonFerMet.Core.Model
{
    public class Job
    {
        public int ConsignmentId { get; set; }
        public int MachineId { get; set; }
        public int StartedAt { get; set; }
        public int CompletedAt { get; set; }
    }
}