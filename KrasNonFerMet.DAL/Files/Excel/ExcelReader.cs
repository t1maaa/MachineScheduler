namespace KrasNonFerMet.DAL.Files.Excel
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