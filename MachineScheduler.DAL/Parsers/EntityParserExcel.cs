using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace MachineScheduler.DAL.Parsers
{
    internal abstract class EntityParserExcel<T> : IEntityParserStrategy<T>
    {
        private readonly string[] _headers;
        private protected readonly ExcelPackage Package;
        public abstract IEnumerable<T> Parse();
       private protected EntityParserExcel(FileInfo fileInfo, string[] headers)
       {
           Package = new ExcelPackage(fileInfo);
           _headers = headers;
       }

       /// <summary>
       /// Array length should be the same like 'Headers' field and in the same order
       /// </summary>
       /// <param name="headers"></param>
       /// <exception cref="ArgumentException"></exception>
       /// <returns></returns>
       [Obsolete]
       private protected bool IsCorrectFile(params string[] headers)
       {
           if (_headers.Length != headers.Length)
               throw new ArgumentException(
                   $"Invalid argument. Array length ({headers.Length}) should be the same like '_headers' field {_headers.Length} and in the same order");
           
           for (int i = 0; i < _headers.Length; i++)
               if (!string.Equals(_headers[i], headers[i], StringComparison.InvariantCultureIgnoreCase)) return false;
           return true;
           // return !Headers.Where((t, i) => !string.Equals(t, headers[i], StringComparison.InvariantCultureIgnoreCase)).Any();
       }

       private protected bool IsCorrectHeaders(ExcelWorksheet worksheet)
       {
           return !_headers.Where((t, i) => !String.Equals(t, worksheet.Cells[1, i + 1].Value.ToString(), StringComparison.InvariantCultureIgnoreCase)).Any();
       }
    }
}