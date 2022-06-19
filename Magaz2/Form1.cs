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
    public partial class Form1 : Form
    {
        MagazineEntities DB = new MagazineEntities();
        Form2 frm2 = new Form2();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var User = DB.Users.FirstOrDefault(users => users.login == textBox1.Text && users.password == textBox2.Text);
            if (User != null)
            {
                Hide();
                frm2.user = User;
                frm2.ShowDialog();
                Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            frm2.user = null;
            frm2.ShowDialog();
            Show();
        }

    }
}
