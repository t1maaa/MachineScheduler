using System;

namespace KrasNonFerMet.DAL.Files.Excel
{
    public class ExcelReader : IExcelReader
    {
        public string DirectoryName { get; set; }
        public string Filename { get; set; }
        public string Fullname { get; set; }

        public ExcelReader(string directory, string filename)
        {
            DirectoryName = directory;
            Filename = filename;

			//TODO:
            //if (directory.EndsWith("\\"))
            //{
            //    if (filename.StartsWith("\\"))
            //    {

            //    }
            //}
			
            Fullname = String.Concat(new[] {directory,"\\", filename});
        }

        public ExcelReader(string fullname)
        {
            Fullname = fullname;
        }
    }
}