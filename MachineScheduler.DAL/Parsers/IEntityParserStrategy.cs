using System.Collections.Generic;

namespace MachineScheduler.DAL.Parsers
{
    public interface IEntityParserStrategy<T>
    {
        IEnumerable<T> Parse();
    }
}