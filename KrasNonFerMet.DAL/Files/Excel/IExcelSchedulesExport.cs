using System;
using System.Collections.Generic;
using KrasNonFerMet.DAL.Models;

namespace KrasNonFerMet.DAL.Files.Excel
{
    public interface IExcelSchedulesExport<T> : IExcelWriter
    {
        public ExportMode ExportMode { get; set; }
        public List<T> Schedules { get; set; }
    }
}