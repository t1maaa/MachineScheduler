using KrasNonFerMet.Core.Interfaces;

namespace KrasNonFerMet.Core.Model
{
    public class Operation : IEntity
    {
        public int MachineId { get; set; }
        public int NomenclatureId { get; set; }
        public int Duration { get; set; }
        public int Id { get; set; }
    }
}
