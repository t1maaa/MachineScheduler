using System;
using System.IO;

namespace MachineScheduler.DAL.Parsers.Nomenclature
{
    public sealed class NomenclatureParserFactory : EntityParserFactory<Models.Nomenclature>
    {
       public override IEntityParserContext<Models.Nomenclature> CreateContext(FileInfo fileInfo)
       {
           IEntityParserContext<Models.Nomenclature> context = GetFileExtension(fileInfo) switch
           {
               ".xlsx" => new NomenclatureParserContext(new NomenclatureParserExcel(fileInfo)),
               //".csv" => throw new NotImplementedException(),
               _ => throw new NotImplementedException($"{GetFileExtension(fileInfo)} is not supported yet")
           };
           return context;
       }
    }
}