using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KrasNonFerMet.Core.Interfaces;
using KrasNonFerMet.DAL.Models;

namespace KrasNonFerMet.Core
{
    [Obsolete]
    public class _ConsignmentExcelReader<T> : IExcelFileReader<T> where T : Consignment, new()
    {
        public string DirectoryPath { get; set; } = Directory.GetCurrentDirectory();
        public string Filename { get; set; } = "parties.xlsx"; //TODO: read from settings
        public List<T> Read()
        {
            IExcelFileReader<T> excelFileReader = this;
            List<T> list = new List<T>();
            var workSheet = excelFileReader.GetPackage().Workbook.Worksheets.First();
            int totalRows = workSheet.Dimension.Rows;

            for (int i = 0; i < totalRows; i++)
            {
                if (int.TryParse(workSheet.Cells[i, 1].Value.ToString(), out int id) && int.TryParse(workSheet.Cells[i, 2].Value.ToString(), out int nomId))
                {
                    list.Add(new T
                    {
                        Id = id,
                        NomenclatureId = nomId
                    });
                }
            }
            return list;
        }
    }
}
