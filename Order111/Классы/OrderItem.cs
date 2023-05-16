using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace OrderGen.Domain
{
    public class OrderItem
    {
        private int fk; 
        private int id;
        private string sepecification;
        private decimal count;
        private decimal price;
        private decimal nds;      

        private Order order;
        public Order Order
        {
            get { return order; }
            set { order = value; }
        }
        public int FK
        {
            get { return fk; }
            set { fk = value; }
        }
        [Key]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Название")]
        public string Sepecification
        {
          
            get { return sepecification; }
          
            set
            {
                //Проверка строки на пустоту (IsValidSpecification)
                Contract.Requires(IsValidSepecification(value));
                //Проверка записалось ли значение
                Contract.Ensures(sepecification == value);
                //Установить название
                sepecification = value;
            }
        }
        //Проверка строки на пустоту
        public static bool IsValidSepecification(string value)=>!string.IsNullOrWhiteSpace(value);

        [DisplayName("Количество")]
        public decimal Count//Получение/Установка Количества
        {
            //Получить количество
            get { return count; }
            //Установить количество
            set
            {
                //Проверка количества (IsValidCount)
                Contract.Requires(IsValidCount(value));
                //Проверка записалось ли значение
                Contract.Ensures(count == value);
                //Установка значения
                count = value;
            }
        }
        //Проверка количества (не может быть <= 0)
        public static bool IsValidCount(decimal value)=>value >= 0.0M;

        [DisplayName("Цена")]
        public decimal Price
        {
            get { return price; }
            set
            {
                Contract.Requires(IsValidPrice(value));
                Contract.Ensures(price == value);
                price = value;
            }
        }

        public static bool IsValidPrice(decimal value)=>value >= 0.0M;

        [DisplayName("НДС")]
        public decimal Nds
        {
            get { return nds; }
            set
            {
                Contract.Requires(IsValidNds(value));
                Contract.Ensures(nds == value);
                nds = value;
            }
        }
        public static bool IsValidNds(decimal value) => value >= 0.0M;
        [DisplayName("Сумма")]
        public decimal Total_Sum { get { return Count * Price; } }
        [DisplayName("НДС в рублях")]
        public decimal Rubl_Nds { get { return Total_Sum / 100 * Nds; } }
        [DisplayName("Сумма с НДС")]
        public decimal Sum_Nds { get { return Total_Sum + Rubl_Nds; } }

    }
}
