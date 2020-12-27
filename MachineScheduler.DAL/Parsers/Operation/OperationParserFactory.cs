using System;
using System.IO;

namespace MachineScheduler.DAL.Parsers.Operation
{
    public sealed class OperationParserFactory : EntityParserFactory<Models.Operation>
    {
        public override IEntityParserContext<Models.Operation> CreateContext(FileInfo fileInfo)
        {
            IEntityParserContext<Models.Operation> context = GetFileExtension(fileInfo) switch
            {
                ".xlsx" => new OperationParserContext(new OperationParserExcel(fileInfo)),
               // ".csv" => throw new NotImplementedException(),
                _ => throw new NotImplementedException($"{GetFileExtension(fileInfo)} is not supported yet")
            };
            return context;
        }
    }
}