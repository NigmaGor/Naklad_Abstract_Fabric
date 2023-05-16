using LibraryNet.Классы;
using LibraryNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static LibraryNet.Классы.Function;
using static System.Data.Entity.Infrastructure.Design.Executor;
using HtmlAgilityPack;
using System.Web;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.ComponentModel;
using System.Threading;

namespace WinForms
{
    //Код формы с товарами в выбранной накладной
    public partial class Naklad : Form
    {
        public Function f = new Function();
        void Update(List<OrderItem> items)
        {
            dataGridView1.DataSource = items.ToList();
            textBox2.Text = o.Sum.ToString();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }
        Order o;
        bool IsSearch = false;
        public Naklad(Order o)
        {
            InitializeComponent();
            this.o = o;
            //Application.ThreadException += Application_ThreadException;
        }
        //~Naklad() 
        //{
        //    Application.ThreadException -= Application_ThreadException;
        //}

        //private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        //{
        //    MessageBox.Show(e.Exception.Message);
        //}

        //Загрузка формы
        private void Naklad_Load(object sender, EventArgs e)
        {
            Update(o.OrderItems);
            dateTimePicker1.Value = o.Data;
            textBox1.Text = o.Number.ToString();

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                if(item.Visible)
                comboBox1.Items.Add(item.HeaderText);
            }
        }

        //Кнопка ОК (Обновление базы) 
        private void DataBaseUpdate_Click(object sender, EventArgs e)
        {
            //foreach(DataGridViewRow row in dataGridView1.Rows)
            //{
            //    var order = 
            //    o.OrderItems[o.OrderItems.FindIndex(id => id.ID == ((OrderItem)row.DataBoundItem).ID)] = (OrderItem)row.DataBoundItem;
            //    Program.DB.OrderItem.ToList()[Program.DB.OrderItem.ToList().FindIndex(id => id.ID == ((OrderItem)row.DataBoundItem).ID)] = (OrderItem)row.DataBoundItem;
            //}
            if (IsSearch)
                Update(f.Search(comboBox1.Text, o.OrderItems, textBox3.Text));
            Update(o.OrderItems);
            MessageBox.Show("База обновлена");
            Program.DB.SaveChanges();
        }

        //Кнопка Удалить товар
        private void DeleteTovar_Click(object sender, EventArgs e)
        {
            if (!Program.DB.Delete_Tovar((OrderItem)dataGridView1.CurrentRow.DataBoundItem))
                MessageBox.Show("Товар не выбран");
            if (IsSearch)
                Update(f.Search(comboBox1.Text, o.OrderItems, textBox3.Text));
            else Update(o.OrderItems);
            MessageBox.Show("Товар удален");
        }

        //Ввод в textbox3 только цифр, если это не название товара (поиск)
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.Text != "Название")
            {
                e.Handled = !Char.IsDigit(e.KeyChar);
            }
        }
        //Кнопка Поиск
        private void SearchTovars_Click(object sender, EventArgs e)
        {
            IsSearch = true;
            var search = f.Search(comboBox1.Text, o.OrderItems, textBox3.Text);
            if (search?.Count > 0)
            {
                Update(search);
                MessageBox.Show("Найдено " + search.Count + " т.");
            }
            else
            {
                MessageBox.Show("Товары не найдены!");
            }
        }
        //Кнопка Отмена
        private void CanselSearch_Click(object sender, EventArgs e)
        {
            Update(o.OrderItems);
            IsSearch = false;
        }
        //Добавить товар
        private void AddTovar_Click(object sender, EventArgs e)
        {
            Program.DB.Tovar_Create(o);
            dataGridView1.DataSource = null;
            Update(o.OrderItems);
        }
        //Сохранить в HTML
        private void SaveInHTML_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            
            saveFileDialog1.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save text file";
            saveFileDialog1.FileName = "Накладная № " + o.Number + ".html";
            
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                f.OutputInHTML(saveFileDialog1.FileName,o.OrderItems);
                MessageBox.Show("Файл сохранен");
            }
        }
    }
}
