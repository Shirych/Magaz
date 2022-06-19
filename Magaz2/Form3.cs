using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magaz2
{
    public partial class Form3 : Form
    {
        public Product product;
        MagazineEntities DB = new MagazineEntities();
        byte[] image;
        public Form3()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Article = textBox1.Text;
            product.Name = textBox2.Text;
            product.Image = image;
            product.Price = Convert.ToInt32(textBox4.Text);
            product.Amount = Convert.ToInt32(textBox5.Text);
            DB.Product.Add(product);
            DB.SaveChanges();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = File.ReadAllBytes(openFileDialog1.FileName);
                pictureBox1.Image = (Bitmap)(new ImageConverter()).ConvertFrom(image);
                
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
