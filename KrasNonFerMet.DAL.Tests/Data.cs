using System;

namespace KrasNonFerMet.DAL.Tests
{
    public static class Data
    {
        public static class File
        {
            public static class Excel
            {
                public static string Directory => String.Concat(new[]
                {
                    System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.FullName,
                    "\\DataSamples\\Excel"
                });

                public static string ExportDirectory => String.Concat(new[]
                {
                    Directory,
                    "\\Reports"
                });

                public static class Machines
                {
                   public static string Directory => Excel.Directory;
                   public static string Filename = "\\machine_tools.xlsx";
                }

                public static class Consignment
                {
                    public static string Directory => Excel.Directory;
                    public static string Filename = "\\parties.xlsx";
                }

                public static class Nomenclature
                {
                    public static string Directory => Excel.Directory;
                    public static string Filename = "\\nomenclatures.xlsx";
                }

                public static class Operation
                {
                    public static string Directory => Excel.Directory;
                    public static string Filename = "\\times.xlsx";
                }
            }
        }
    }
}
