namespace MachineScheduler.DAL.Parsers.Nomenclature
{
    internal sealed class NomenclatureParserContext : EntityParserContext<Models.Nomenclature>
    {
        public NomenclatureParserContext(IEntityParserStrategy<Models.Nomenclature> strategy) : base(strategy)
        {
        }
    }
}