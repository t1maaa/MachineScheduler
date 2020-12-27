using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineScheduler.DAL.Parsers
{
    public static class SupportedFiletypes
    {
        private static List<FileType> _list = new List<FileType>
        {
           new FileType(
               name: "Excel 2007 and newer",
               extensions: "*.xlsx")
        };

        public static string GetFilterString() => String.Join("|", _list.Select(t => t.GetDialogFilter()));
        public static bool IsSupported (string extension)
        {
            var s =  _list.Any(t =>
                t.GetExtension()
                    .Split(new string[] {"*", ";*"}, StringSplitOptions.RemoveEmptyEntries)
                    .Contains(extension)); 
            return s;
                
        }
        
    }
}