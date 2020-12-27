using System.IO;

namespace MachineScheduler.DAL.Parsers
{
    public abstract class EntityParserFactory<T> : IEntityParserFactory<T>
    {
        public abstract IEntityParserContext<T> CreateContext(FileInfo fileInfo);
        private protected string GetFileExtension(FileSystemInfo fileInfo) => fileInfo.Extension; //TODO: enhance detection through MIME type
        public static bool IsSupportedFiletype(string extension) //TODO: add check for MIME-type
        {
            return !string.IsNullOrWhiteSpace(extension) && SupportedFiletypes.IsSupported(extension);
        }
    }
}