using System;
using System.Collections.Generic;
using KrasNonFerMet.DAL.Models;
using OfficeOpenXml;

namespace KrasNonFerMet.DAL.Extensions
{
    public static class ExcelWorksheetsExtensions
    {
        public static List<T> ToList<T>(this ExcelWorksheet worksheet /*, out IList duplicates*/)
            where T : class, IEntity, new()
        {
            int? totalRows = worksheet?.Dimension?.Rows;
            // duplicates = null;
            if (totalRows != null && totalRows > 0)
            {
                switch (typeof(T)) //TODO: Are Unique Id guaranteed? To HashSet/Dictionary?
                {
                    case var _ when typeof(T) == typeof(Machine):
                        List<Machine> machines = new List<Machine>();
                        for (int i = 1; i <= totalRows; i++)
                        {
                            if (int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int id))
                            {
                                /*
                                machines.Any(t => t.Id == id)
                                {
                                    if (duplicates == null)
                                    {
                                        duplicates = new List<Machine>();
                                    }
                                    duplicates.Add(new Machine
                                    {
                                        Id = id,
                                        Name = name
                                    });
                                }//проверить остальные столбцы, если совпадает просто скипнуть, если нет то закинуть в дупликаты
                                */
                                string name = worksheet.Cells[i, 2].Value.ToString();
                                if (!string.IsNullOrWhiteSpace(name))
                                {
                                    machines.Add(new Machine
                                    {
                                        Id = id,
                                        Name = name
                                    });
                                }
                            }
                        }

                        return machines as List<T>;

                    case var _ when typeof(T) == typeof(Consignment):
                        List<Consignment> consignments = new List<Consignment>();
                        for (int i = 1; i <= totalRows; i++)
                        {
                            if (int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int id))
                            {
                                if (int.TryParse(worksheet.Cells[i, 2].Value.ToString(), out int nomId))
                                {
                                    consignments.Add(new Consignment
                                    {
                                        Id = id,
                                        NomenclatureId = nomId
                                    });
                                }
                            }
                        }

                        return consignments as List<T>;


                    case var _ when typeof(T) == typeof(Nomenclature):
                        List<Nomenclature> nomenclatures = new List<Nomenclature>();
                        for (int i = 1; i <= totalRows; i++)
                        {
                            if (int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int id))
                            {
                                string name = worksheet.Cells[i, 2].Value.ToString();
                                if (!string.IsNullOrWhiteSpace(name))
                                    nomenclatures.Add(new Nomenclature
                                    {
                                        Id = id,
                                        Name = name
                                    });
                            }
                        }

                        return nomenclatures as List<T>;

                    case var _ when typeof(T) == typeof(Operation):
                        List<Operation> operations = new List<Operation>();
                        for (int i = 1; i <= totalRows; i++)
                        {
                            if (int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int machId))
                            {
                                if (int.TryParse(worksheet.Cells[i, 2].Value.ToString(), out int nomId))
                                {
                                    if (int.TryParse(worksheet.Cells[i, 3].Value.ToString(), out int opTime))
                                    {
                                        operations.Add(new Operation
                                        {
                                            MachineId = machId,
                                            NomenclatureId = nomId,
                                            Duration = opTime
                                        });
                                    }
                                }
                            }
                        }

                        return operations as List<T>;
                    default:
                        throw new NotSupportedException($"Type {typeof(T)} is not supported yet");
                }
            }

            throw new ArgumentException("Empty worksheet");
        }
    }
}