using LibraryNet.Классы;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace WinForms
{
    internal static class Program
    {
        public static OrderContext DB;
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DB = new OrderContext();
            DB.factory = new NakladFactory();//Создаем фабрику
            DB.Database.CreateIfNotExists();
            DB.Order.Load();
            DB.OrderItem.Load();
            Application.Run(new OrderGenMain());
        }
    }
}
