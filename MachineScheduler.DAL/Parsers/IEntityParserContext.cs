using System.Collections.Generic;

namespace MachineScheduler.DAL.Parsers
{
    public interface IEntityParserContext<T>
    {
        IEnumerable<T> Parse();
    }

}