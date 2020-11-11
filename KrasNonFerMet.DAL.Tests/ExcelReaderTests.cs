using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KrasNonFerMet.DAL.Extensions;
using KrasNonFerMet.DAL.Files.Excel;
using KrasNonFerMet.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KrasNonFerMet.DAL.Tests
{
    [TestClass()]
    public class ExcelReaderTests
    {
        [TestMethod()]
        public void ExcelReaderTest_Machines_ValidNonEmptyFile()
        {
            IExcelReader er = new ExcelReader(Data.File.Excel.Machines.Directory,
                Data.File.Excel.Machines.Filename);
            List<Machine> machines = er.GetWorksheets().FirstOrDefault().ToList<Machine>();
            
            Assert.AreEqual(machines.Count, 3);
            Assert.IsNotNull(machines);

            for (var i = 0; i < machines.Count; i++)
            {
                Assert.AreEqual(machines[i].Id, i);
                Assert.AreEqual(String.Compare(machines[i].Name, $"Печь {i + 1}", StringComparison.InvariantCultureIgnoreCase), 0);
            }
        }

        [TestMethod()]
        public void ExcelReaderTest_Nomenclature_ValidNonEmptyFile()
        {
            IExcelReader er = new ExcelReader(Data.File.Excel.Nomenclature.Directory,
                Data.File.Excel.Nomenclature.Filename);
            List<Nomenclature> nomenclatures = er.GetWorksheets().FirstOrDefault().ToList<Nomenclature>();

            Assert.AreEqual(nomenclatures.Count, 3);
            Assert.IsNotNull(nomenclatures);

            for (var i = 0; i < nomenclatures.Count; i++)
            {
                Assert.AreEqual(nomenclatures[i].Id, i);
            }
        }

        [TestMethod()]
        public void ExcelReaderTest_Consignment_ValidNonEmptyFile()
        {
            IExcelReader er = new ExcelReader(Data.File.Excel.Consignment.Directory,
                Data.File.Excel.Consignment.Filename);
            List<Consignment> consignments = er.GetWorksheets().FirstOrDefault().ToList<Consignment>();

            Assert.AreEqual(consignments.Count, 50);
            Assert.IsNotNull(consignments);

            for (var i = 0; i < consignments.Count; i++)
            {
                Assert.AreEqual(consignments[i].Id, i);
            }
        }

        [TestMethod()]
        public void ExcelReaderTest_Operation_ValidNonEmptyFile()
        {
            IExcelReader er = new ExcelReader(Data.File.Excel.Operation.Directory,
                Data.File.Excel.Operation.Filename);
            List<Operation> operations = er.GetWorksheets().FirstOrDefault().ToList<Operation>();

            Assert.AreEqual(operations.Count, 7);
            Assert.IsNotNull(operations);
        }

        [TestMethod()]
        [Description("Make sure that file contain at least TWO worksheet where The last one is completely empty")]
        public void ExcelReaderTest_FileExistAndEmpty() 
        {
            IExcelReader er = new ExcelReader(Data.File.Excel.Machines.Directory,
                Data.File.Excel.Machines.Filename);
            //TODO:add condition that worksheet > 1
            Assert.ThrowsException<ArgumentException>(() =>
            {
                List<Machine> machines = er.GetWorksheets().Last().ToList<Machine>();
            });
            //TODO:add ESLE to another AssertException
        }

        [TestMethod()]
        public void ExcelReaderTest_NotExistedFile()
        {
            IExcelReader s = new ExcelReader(Data.File.Excel.Machines.Directory, "\\notexistedfile.xlsx");

            Assert.ThrowsException<FileNotFoundException>(() =>
            {
                s.GetWorksheets();
            });
        }

        [TestMethod()]
        public void ExcelReaderTest_NotSupportedType()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                IExcelReader er = new ExcelReader(Data.File.Excel.Machines.Directory,
                    Data.File.Excel.Machines.Filename);
                er.GetWorksheets().FirstOrDefault().ToList<NotSupportedType>();
            });
        }
    }

    internal class NotSupportedType : IEntity
    {
        public int Id { get; set; }
    }
}
