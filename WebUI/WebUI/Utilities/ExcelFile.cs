using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Suppliers;

namespace WebUI.Utilities
{
    public static class ExcelFile
    {
        public static bool Create(string filePath, IEnumerable<SupplierViewModel> list)
        {
            bool exported = false;
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Suppliers");
                int currentRow = 1;

                #region Header
                ws.Cell(currentRow, 1).Value = "SupplierId";
                ws.Cell(currentRow, 2).Value = "Fantasy Name";
                ws.Cell(currentRow, 3).Value = "Products";
                #endregion


                #region Body
                foreach (var supplier in list)
                {
                    currentRow++;
                    ws.Cell(currentRow, 1).Value = supplier.Id;
                    ws.Cell(currentRow, 2).Value = supplier.FantasyName;

                    if (supplier.Products.Count > 0)
                    {
                        int currentCell = 3;
                        foreach (var product in supplier.Products)
                        {
                            ws.Cell(currentRow, currentCell).Value = product.Name;
                            currentCell++;
                        }
                    }
                }
                #endregion

                ws.Columns().AdjustToContents();

                wb.SaveAs(filePath);
                exported = true;
            }

            return exported;
        }
    }
}
