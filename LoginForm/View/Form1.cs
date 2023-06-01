using LoginForm.Data;
using LoginForm.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new Register();
            frm.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_User.Text != "" && txt_pass.Text.Length > 7 && txt_User.Text != "\r\nنام کاربری\r\n\r\n\r\n" && txt_pass.Text != " \"\\r\\nکلمه عبور\\r\\n\\r\\n\\r\\n\\r\\n\"")
                {
                    if (DbCommandContext.Login(txt_User.Text.ToUpper(), txt_pass.Text))
                    {
                        Form frm = new MainForm();
                        frm.ShowDialog();
                        this.Hide();
                    }
                }
                lbl_err.Visible = true;
            }
            catch
            {
                MessageBox.Show("کانکشن استرینگ اشتباه است");
            }
            
         
           
               
            
        }

        private void txt_User_Enter(object sender, EventArgs e)
        {
            if (txt_User.Text.Trim() == "نام کاربری")
            {
                txt_User.Text = "";
               
            }
        }

        private void txt_User_Leave(object sender, EventArgs e)
        {
            if (txt_User.Text.Trim() == "")
            {
                txt_User.Text = "\r\nنام کاربری\r\n\r\n\r\n";
               
            }
        }

        private void txt_pass_Enter(object sender, EventArgs e)
        {

            if (txt_pass.Text.Trim() == "کلمه عبور")
            {
                txt_pass.Text = "";
                txt_pass.PasswordChar = '*';
            }

        }

        private void txt_pass_Leave(object sender, EventArgs e)
        {
            if (txt_pass.Text.Trim() == "")
            {
                txt_pass.Text = "\r\nکلمه عبور\r\n\r\n\r\n";
               
                    txt_pass.PasswordChar = '\0';
                
            }
        }

        private void CreateDatabase(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form frm = new GetConnectionStringForm();
            frm.Show();
        }
    }
}
