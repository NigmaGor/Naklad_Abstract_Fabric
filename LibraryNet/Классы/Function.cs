using LibraryNet.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
namespace LibraryNet.Классы
{
    public class Function
    {
        public bool OutputInHTML(string puth, List<OrderItem> items)
        {
            if (items.Count == 0) return false;
            using (var writer = new StreamWriter(puth))
            {
                writer.WriteLine("<html>");
                writer.WriteLine("<body>");
                writer.WriteLine("<table border='1px' cellpadding='5' cellspacing='0' style='border: solid 1px Silver; font-size: small;'>");
                // Заголовки столбцов
                writer.WriteLine("<tr align='left' valign='top'>");
                writer.WriteLine("<td align='left' valign='top'><b>{0}</b></td>", "Название");
                writer.WriteLine("<td align='left' valign='top'><b>{0}</b></td>", "Количество");
                writer.WriteLine("<td align='left' valign='top'><b>{0}</b></td>", "Цена");
                writer.WriteLine("<td align='left' valign='top'><b>{0}</b></td>", "НДС");
                writer.WriteLine("<td align='left' valign='top'><b>{0}</b></td>", "Сумма");
                writer.WriteLine("<td align='left' valign='top'><b>{0}</b></td>", "НДС в рублях");
                writer.WriteLine("<td align='left' valign='top'><b>{0}</b></td>", "Сумма с НДС");
                writer.WriteLine("</tr>");
                for (int i = 0; i < items.Count; i++)
                {
                    var fields = items[i].GetType().GetProperties();
                    writer.WriteLine("<tr align='left' valign='top'>");
                    foreach (var field in fields)
                    {
                        if (field.GetCustomAttribute<DisplayNameAttribute>() != null)

                            writer.WriteLine("<td align='left' valign='top'>{0}</td>", field.GetValue(items[i]));
                    }
                    writer.WriteLine("</tr>");
                }
                writer.WriteLine("</table>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }
            return true;
        }
        public List<OrderItem> Search(string p, List<OrderItem> z, string stroka)
        {
            if ((z == null) || (p == null) || (stroka == null)) return null;
            else
                switch (p)
                {
                    case "Название": return z.FindAll(Sepecification => Sepecification.Sepecification == stroka);
                    case "Количество": return z.FindAll(Count => Count.Count == Convert.ToDecimal(stroka));
                    case "Цена": return z.FindAll(Price => Price.Price == Convert.ToDecimal(stroka));
                    case "НДС": return z.FindAll(Nds => Nds.Nds == Convert.ToDecimal(stroka));
                    case "Сумма": return z.FindAll(Total_Sum => Total_Sum.Total_Sum == Convert.ToDecimal(stroka));
                    case "НДС в рублях": return z.FindAll(Rubl_Nds => Rubl_Nds.Rubl_Nds == Convert.ToDecimal(stroka));
                    case "Сумма с НДС": return z.FindAll(Sum_Nds => Sum_Nds.Sum_Nds == Convert.ToDecimal(stroka));
                }
            return null;
        }
    }
}
