namespace KrasNonFerMet.DAL.Models
{
    public class Job
    {
        public int ConsignmentId { get; set; }
        public int MachineId { get; set; }
        public int StartAt { get; set; }
        public int CompleteAt { get; set; }
    }
}