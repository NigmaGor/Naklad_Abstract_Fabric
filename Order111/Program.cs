using Order111.Классы;
using System;
using System.Windows.Forms;
using System.Data.Entity;
using OrderGen.Domain;
using static Order111.Классы.AbstractFabric;

namespace Order111
{
    internal static  class Program
    {
        public static OrderContext DB;


        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
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
