using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MachineScheduler.DAL.Parsers.Operation
{
    internal sealed class OperationParserExcel : EntityParserExcel<Models.Operation>
    {
        #region Column Names
        
        private static readonly string[] Headers =
        {
            "machine tool id",
            "nomenclature id",
            "operation time"
        };
        
        #endregion
        
        public OperationParserExcel(FileInfo fileInfo) : base(fileInfo, Headers)
        {
        }

        public override IEnumerable<Models.Operation> Parse()
        {
            var worksheet = Package.Workbook.Worksheets.FirstOrDefault();
            
            int? totalRows = worksheet?.Dimension?.Rows;
            if (totalRows > 0)
            {
                if (IsCorrectHeaders(worksheet))
                {
                    for (int i = 1; i <= totalRows; i++)
                    {
                        if (int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int machId))
                        {
                            if (int.TryParse(worksheet.Cells[i, 2].Value.ToString(), out int nomId))
                            {
                                if (int.TryParse(worksheet.Cells[i, 3].Value.ToString(), out int opTime))
                                {
                                    yield return new Models.Operation
                                    {
                                        MachineId = machId,
                                        NomenclatureId = nomId,
                                        Duration = opTime
                                    };
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}