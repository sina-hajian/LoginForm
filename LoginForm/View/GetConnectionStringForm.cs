using LoginForm.Data;
using LoginForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm.View
{
    public partial class GetConnectionStringForm : Form
    {
        public GetConnectionStringForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DbCommandContext.CreatDatabase();
                DbCommandContext.SqlRegisterCommand();
                MessageBox.Show("با موفقیت انجام شد");
            }catch(Exception ex) { MessageBox.Show("خطایی رخ داد"); }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DbCommandContext.dropDatabase();
               
                MessageBox.Show("با موفقیت انجام شد");
            }
            catch (Exception ex) { MessageBox.Show("خطایی رخ داد"); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Constant.ConnectionString = txt_connection.Text + ";Database=TestSinaHajian;";
            Constant.ConnectionStringWhole = txt_connection.Text;
        }
    }
}
