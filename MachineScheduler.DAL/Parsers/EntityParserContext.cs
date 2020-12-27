using System.Collections.Generic;

namespace MachineScheduler.DAL.Parsers
{
    internal abstract class EntityParserContext<T> : IEntityParserContext<T>
    {
        private readonly IEntityParserStrategy<T> _strategy;

        private protected EntityParserContext(IEntityParserStrategy<T> strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<T> Parse()
        {
           return _strategy?.Parse();
        }
    }
}