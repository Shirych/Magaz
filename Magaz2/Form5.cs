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
    public partial class Form5 : Form
    {
        MagazineEntities DB = new MagazineEntities();
        public int ProductId = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox1.Text) > 0)
                {
                    Product product = DB.Product.First(p => p.Id == ProductId);
                    product.Amount += Convert.ToInt32(textBox1.Text);
                    DB.SaveChanges();
                    Close();

                }
            }
            catch
            {
                MessageBox.Show("Необходимо ввести положительное число.");
            }
        }
    }
}
