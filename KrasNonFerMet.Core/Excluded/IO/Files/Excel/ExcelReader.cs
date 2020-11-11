using KrasNonFerMet.Core.Interfaces;

namespace KrasNonFerMet.Core
{
    public class ExcelReader : IExcelReader
    {
        public string DirectoryPath { get; set; }
        public string Filename { get; set; }

        public ExcelReader(string directory, string filename)
        {
            DirectoryPath = directory;
            Filename = filename;
        }
    }
}
