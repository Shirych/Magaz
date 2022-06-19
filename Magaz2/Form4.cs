using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Lists;
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
    public partial class Корзина : Form
    {
        MagazineEntities DB = new MagazineEntities();
        public List<Product> cart = new List<Product>();

        public Корзина()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                cart.Remove(cart.First(p => p.Id == id));
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите товар");
            }
            dataGridView1.DataSource = null;
            Update();
        }

        void Update()
        {
            try
            {
                dataGridView1.DataSource = cart;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.DefaultCellStyle.Font = new Font("Arial", 25F, GraphicsUnit.Pixel);
                label1.Text = CostCalculation().ToString();
            }
            catch
            {
            }
        }

        private void Form4_Activated(object sender, EventArgs e)
        {
            Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Id = 0;
            order.TotalPrice = CostCalculation();
            order.TotalAmount = cart.Count;
            order.Date = DateTime.Now;
            Order order1 = DB.Order.Add(order);
            DB.SaveChanges();

            string guid = Guid.NewGuid().ToString();
            PdfDocument doc = new PdfDocument();
            PdfMargins margins = new PdfMargins(30);
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margins);
            PdfBrush brush = PdfBrushes.Black;
            PdfFont titleFont = new PdfFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Bold);
            PdfFont listFont = new PdfFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Regular);
            PdfOrderedMarker marker = new PdfOrderedMarker(PdfNumberStyle.UpperLatin, listFont);
            float x = 0;
            float y = 0;
            string title = "Check number:" + guid + "\nTotal cost:" + CostCalculation().ToString() + "\nOrder date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            page.Canvas.DrawString(title, titleFont, brush, x, y);
            y = y + (float)titleFont.MeasureString(title).Height;
            y = y + 5;
            string listContent = "";
            foreach (var product in cart)
            {
                listContent += "Article " + product.Article + " Name " + product.Name + " Cost " + product.Price + "\n";

                OrderProducts orderProducts = new OrderProducts();
                orderProducts.Amount = 1;
                orderProducts.Id_product = product.Id;
                orderProducts.Id_order = order1.Id;

                Product product1 = DB.Product.First(p => p.Id == product.Id);
                if (product1.Amount > 0)
                {
                    product1.Amount = product1.Amount - 1;
                }
                else
                {
                    MessageBox.Show("Товар " + product1.Name + " закончился");
                }
                DB.SaveChanges();
            }
            PdfSortedList list = new PdfSortedList(listContent);
            list.Font = listFont;
            list.Indent = 2;
            list.TextIndent = 4;
            list.Brush = brush;
            list.Marker = marker;
            list.Draw(page, 0, y);
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Выберете путь для чека");
                return;
            }
            doc.SaveToFile(saveFileDialog1.FileName + " Чек " + guid + ".pdf");
            cart.Clear();
            Close();

        }

        public decimal CostCalculation()
        {
            decimal summ = 0;
            foreach (var product in cart)
            {
                summ += Convert.ToDecimal(product.Price);
            }
            return summ;
        }
    }
}
