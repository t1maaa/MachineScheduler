namespace MachineScheduler.DAL.Parsers.Operation
{
    internal sealed class OperationParserContext : EntityParserContext<Models.Operation>
    {
        public OperationParserContext(IEntityParserStrategy<Models.Operation> strategy) : base(strategy)
        {
        }
    }
}