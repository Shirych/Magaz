using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magaz2
{
    public partial class Form2 : Form
    {
        Form3 frm3 = new Form3();
        MagazineEntities DB = new MagazineEntities();
        public Users user = new Users();
        Корзина form4 = new Корзина();  
        Form5 form5 = new Form5();  

        public Form2()
        {
            InitializeComponent();
            Vblvod();
        }
        void Vblvod() 
        {
            var product = from Product in DB.Product select new 
            {
                Id = Product.Id,
                Article = Product.Article,
                Name = Product.Name,
                Image = Product.Image,
                Price = Product.Price,
                Amount = Product.Amount,
            };
            dataGridView1.DataSource = product.ToList();
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 25F, GraphicsUnit.Pixel);
            label1.Text = form4.CostCalculation().ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm3.ShowDialog();
            Vblvod();
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            if (user == null)
            {
                button1.Visible = false;
                button4.Visible = false;
            }
            else
            {
                button1.Visible = true;
                button4.Visible = true;
            }
            dataGridView1.Columns[0].Visible = false;
            label1.Text = form4.CostCalculation().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Hide();
                form4.ShowDialog();
                Show();
                Vblvod();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                var products = DB.Product.ToList();
                Product product = products.First(p => p.Id == id);
                if (product.Amount > 0)
                {
                    form4.cart.Add(product);
                }
                else
                {
                    MessageBox.Show("Товар " + product.Name + " закончился");
                }
                label1.Text = form4.CostCalculation().ToString();
            }
            catch 
            {
                MessageBox.Show("Выберите товар");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                form5.ProductId = id;
                Hide();
                form5.ShowDialog();
                Show();
                Vblvod();
            }
            catch
            {
                MessageBox.Show("Выберите товар");
            }

        }
    }
}
