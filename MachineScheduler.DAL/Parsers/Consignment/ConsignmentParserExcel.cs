using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MachineScheduler.DAL.Parsers.Consignment
{
    internal sealed class ConsignmentParserExcel : EntityParserExcel<Models.Consignment>
    {
        #region Column Names

        private static readonly string[] Headers =
        {
            "id",
            "nomenclature id"
        };

        #endregion

        public ConsignmentParserExcel(FileInfo fileInfo) : base(fileInfo, Headers)
        {
        }

        public override IEnumerable<Models.Consignment> Parse()
        {
            var worksheet = Package.Workbook.Worksheets.FirstOrDefault();

            int? totalRows = worksheet?.Dimension?.Rows;
            if (totalRows > 0)
            {
                if (IsCorrectHeaders(worksheet))
                {
                    for (int i = 1; i <= totalRows; i++)
                    {
                        if (int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int id))
                        {
                            if (int.TryParse(worksheet.Cells[i, 2].Value.ToString(), out int nomId))
                            {
                                yield return new Models.Consignment
                                {
                                    Id = id,
                                    NomenclatureId = nomId
                                };
                            }
                        }
                    }
                }
            }
        }
    }
}