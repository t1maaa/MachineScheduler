using KrasNonFerMet.Core.Interfaces;

namespace KrasNonFerMet.Core.Model
{
    public class Consignment : IEntity
    {
        public int Id { get; set; }
        public int NomenclatureId { get; set; }
    }
}
