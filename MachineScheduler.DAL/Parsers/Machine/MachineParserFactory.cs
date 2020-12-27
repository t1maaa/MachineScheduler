using System;
using System.IO;

namespace MachineScheduler.DAL.Parsers.Machine
{
    public sealed class MachineParserFactory : EntityParserFactory<Models.Machine>
    {
        public override IEntityParserContext<Models.Machine> CreateContext(FileInfo fileInfo)
        {
            IEntityParserContext<Models.Machine> context = GetFileExtension(fileInfo) switch
            {
                ".xlsx" => new MachineParserContext(new MachineParserExcel(fileInfo)),
               // ".csv" => throw new NotImplementedException(),
                _ => throw new NotImplementedException($"{GetFileExtension(fileInfo)} is not supported yet")
            };
            return context;
        }
    }
}