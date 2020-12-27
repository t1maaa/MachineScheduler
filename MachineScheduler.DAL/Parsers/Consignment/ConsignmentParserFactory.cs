using System;
using System.IO;

namespace MachineScheduler.DAL.Parsers.Consignment
{
    public sealed class ConsignmentParserFactory : EntityParserFactory<Models.Consignment>
    {
        public override IEntityParserContext<Models.Consignment> CreateContext(FileInfo fileInfo)
        {
            IEntityParserContext<Models.Consignment> context = GetFileExtension(fileInfo) switch
            {
                ".xlsx" => new ConsignmentParserContext(new ConsignmentParserExcel(fileInfo)),
               // ".csv" => throw new NotImplementedException(),
                _ => throw new NotImplementedException($"{GetFileExtension(fileInfo)} is not supported yet")
            };

            return context;
        }
    }
}