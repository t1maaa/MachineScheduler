using KrasNonFerMet.Core.Interfaces;

namespace KrasNonFerMet.Core.Model
{
    public class Machine : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
