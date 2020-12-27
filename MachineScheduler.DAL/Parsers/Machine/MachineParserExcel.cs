using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MachineScheduler.DAL.Parsers.Machine
{
    internal sealed class MachineParserExcel : EntityParserExcel<Models.Machine>
    {
        #region Column Names

        private static readonly string[] Headers =
        {
            "id",
            "name"
        };
        
        #endregion

        public MachineParserExcel(FileInfo fileInfo) : base(fileInfo, Headers)
        {
        }

        public override IEnumerable<Models.Machine> Parse()
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
                            string name = worksheet.Cells[i, 2].Value.ToString();
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                yield return new Models.Machine
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