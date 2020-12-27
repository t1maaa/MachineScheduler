using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MachineScheduler.DAL.Parsers.Nomenclature
{
    internal sealed class NomenclatureParserExcel : EntityParserExcel<Models.Nomenclature>
    {
        #region Column Names
        
        private static readonly string[] Headers =
        {
            "id",
            "nomenclature"
        };
        
        #endregion
        
        public NomenclatureParserExcel(FileInfo fileInfo) : base(fileInfo, Headers)
        {
        }

        public override IEnumerable<Models.Nomenclature> Parse()
        {
            var worksheet = Package.Workbook.Worksheets.FirstOrDefault();
            
            int? totalRows = worksheet?.Dimension?.Rows;
            if (totalRows > 0)
            {
                if (IsCorrectHeaders(worksheet))
                {
                    for (var i = 1; i <= totalRows; i++)
                    {
                        if (int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out var id))
                        {
                            var name = worksheet.Cells[i, 2].Value.ToString();
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                yield return new Models.Nomenclature
                                {
                                    Id = id,
                                    Name = name
                                };
                            }
                        }
                    }
                }
            }
        }
    }
}