using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KrasNonFerMet.Core.Interfaces;
using KrasNonFerMet.DAL.Models;
using OfficeOpenXml;

namespace KrasNonFerMet.Core
{
    [Obsolete]
    public class MachineExcelReader<T> : IExcelFileReader<T> where T : Machine, new()
    {
        public string DirectoryPath { get; set; }
        public string Filename { get; set; }
        
        public MachineExcelReader(string path, string filename)
        {
            DirectoryPath = path;
            Filename = filename;
        }
        public List<T> Read()
        {
            IExcelFileReader<T> excelFileReader = this;
            List<T> list = new List<T>();

            ExcelWorksheet workSheet;
            try
            {
                workSheet = excelFileReader
                    ?.GetPackage()
                    ?.Workbook
                    ?.Worksheets
                    ?.First();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }

            
            int totalRows = workSheet.Dimension.Rows;

            for (int i = 1; i <= totalRows; i++)
            {
                if (int.TryParse(workSheet.Cells[i, 1].Value.ToString(), out int id))
                {
                    list.Add(new T
                    {
                        Id = id,
                        Name = workSheet.Cells[i, 2].Value.ToString()
                    });
                }
            }
            return list;
        }
    }
}
