using System.IO;

namespace MachineScheduler.DAL.Parsers
{
    public interface IEntityParserFactory<T>
    {
        public IEntityParserContext<T> CreateContext(FileInfo fileInfo);
    }
}