namespace MachineScheduler.DAL.Models
{
    /// <summary>
    /// Merged - Single table with aggregated schedules for all machines
    /// Paged - Single file, only one machine's schedule per page 
    /// Splitted - Each machine's schedule in a new file
    /// </summary>
    public enum ExportMode
    {
        /// <summary>
        /// Single table with aggregated schedules for all machines
        /// </summary>
        Merged,

        /// <summary>
        /// Single file but each machine's schedule on a new page 
        /// </summary>
        Paged,

        /// <summary>
        /// Each machine's schedule in a new file
        /// </summary>
        Splitted
    }
}