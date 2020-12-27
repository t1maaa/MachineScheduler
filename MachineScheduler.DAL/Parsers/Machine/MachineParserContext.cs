namespace MachineScheduler.DAL.Parsers.Machine
{
    internal sealed class MachineParserContext : EntityParserContext<Models.Machine>
    {
        public MachineParserContext(IEntityParserStrategy<Models.Machine> strategy) : base(strategy)
        {
        }
    }
}