using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using LibraryNet.Domain;

namespace WinForms
{
    //Код главной формы (Список накладных)
    public partial class OrderGenMain : Form
    {
        public OrderGenMain()
        {
            InitializeComponent();
        }
        //Загрузка формы
        private void OrderGenMain_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = Program.DB.Order.ToList();
            listBox1.DisplayMember = "Number";
        }
        //Открыть выбранную накладную
        private void OpenNaklad_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem==null)
                MessageBox.Show("Накладная не выбрана");
            else
            {
                Naklad dlg = new Naklad((Order)listBox1.SelectedItem);
                dlg.ShowDialog();
            } 
        }

        //Ограничение на ввод в ТекстБокс только цифр (событие TextBox1)
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar);
        }
        //Удалить выбранную накладную
        private void DeleteNaklad_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Program.DB.Delete_Naklad((Order)listBox1.SelectedItem);
                MessageBox.Show("Накладная удалена");
                listBox1.DataSource = Program.DB.Order.ToList();
            }
            else
            {
                MessageBox.Show("Накладная не выбрана");
            }
        }
        //Поиск накладных по сумме >= заданной
        private void SearchSum_Click(object sender, EventArgs e)
        {
            var text = textBox2.Text;
            if (text == "") MessageBox.Show("Сумма не указана!");
            Decimal.TryParse(text, out var znach);
            if (znach <= 0) MessageBox.Show("Сумма указана неправильно!");
            else
            {
                listBox1.DataSource = new BindingList<Order>(Program.DB.SearchNakladInSum(znach));
                MessageBox.Show("Найдено " + listBox1.Items.Count + " наклад.");
            }
        }
        //Отмена
        private void CanselSearch_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = Program.DB.Order.ToList();
            listBox1.DisplayMember = "Number";

        }
        //Ограничение на ввод цифр
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar);
        }
        //Кнопка добавить накладную
        private void AddNaklad_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int.TryParse(textBox1.Text, out var number);
                if (!Program.DB.Order_Add(number))
                    MessageBox.Show("Накладная с таким номером уже существует");
                else MessageBox.Show("Накладная добавлена");
            }
            else MessageBox.Show("Не указан номер накладной");

            listBox1.DataSource = Program.DB.Order.ToList();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
