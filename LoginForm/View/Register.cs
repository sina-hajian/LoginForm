using Core.Common.Encript;
using LoginForm.Data;
using LoginForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm.View
{
    public partial class Register : Form
    {
       
        public Register()
        {
            InitializeComponent();
        }

       
        private void SetButtonPlaceHolderLeave(TextBox txt , string PlaceHolder)
        {
            if (txt.Text == "")
            {
                txt.Text = PlaceHolder;
                if (txt == txt_Pass || txt == txt_ConfirmPass)
                {
                    txt.PasswordChar = '\0';
                }
            }
        }
        private void SetButtonPlaceHolderEnter(TextBox txt, string PlaceHolder)
        {
            if (txt.Text == PlaceHolder)
            {
                txt.Text = "";
                if(txt == txt_Pass ||txt == txt_ConfirmPass)
                {
                    txt.PasswordChar = '*';

                }
            }
        }

       
        private void CheckLength(int min , int max , TextBox txt , Label lbl)
        {
            if (txt.Text.Length < min || txt.Text.Length > max)
            {
                lbl.Text = StaticString.err_length;
                lbl.Visible = true;
            }
            else
            {
                lbl.Text = "";
                lbl.Visible = false;
            }
        }
        private void CheckPassConfirm()
        {
            if(txt_Pass.Text.Length < 8 || txt_Pass.Text.Length > 20)
            {
                err_Pass.Text = "کلمه عبور بایستی حداقل 8 حرف باشد";
                err_Pass.Visible = true;
            }
            else if (txt_Pass.Text != txt_ConfirmPass.Text)
            {
                err_Pass.Text = "کلمه عبور با تکرار آن مطابقت ندارد";
                err_Pass.Visible = true;


            }
            else
            {

                err_Pass.Visible = false;
            }
            
        }

        private void IsValidEmail()
        {
            string eMail = txt_Email.Text;
            bool Result = false;

            try
            {
                var eMailValidator = new System.Net.Mail.MailAddress(eMail);

                Result = (eMail.LastIndexOf(".") > eMail.LastIndexOf("@"));
                
            }
            catch
            {
                Result = false;
            };
            if (Result)
            {
                err_Email.Visible = false;
            }
            else
            {
                err_Email.Visible = true;
                err_Email.Text = StaticString.err_Email;
            }
           
        }
        private void txt_Leave_Leave(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            if (sender == txt_User)
            {
                CheckLength(5, 15, txt, err_User);
                SetButtonPlaceHolderLeave(txt, StaticString.userPlaceHolderText);
                
            }
            else if (sender == txt_Pass)
            {
                CheckPassConfirm();
                
                SetButtonPlaceHolderLeave(txt, StaticString.passwordPlaceHolderText);
                
            }
            else if (sender == txt_ConfirmPass)
            {
                CheckPassConfirm();
                    SetButtonPlaceHolderLeave(txt, StaticString.confirmPassPlaceHolderText);
                
                
            }
            else if (sender == txt_Name)
            {
                CheckLength(5, 15, txt, err_Name);
                SetButtonPlaceHolderLeave(txt, StaticString.namePlaceHolderText);
                
            }
            else if (sender == txt_Family)
            {
                CheckLength(5, 15, txt, err_Family);
                SetButtonPlaceHolderLeave(txt, StaticString.familyPlaceHolderText);
                
            }
            else if (sender == txt_Email)
            {
                IsValidEmail();
                SetButtonPlaceHolderLeave(txt, StaticString.emailPlaceHolderText);
               
            }
            else if (sender == txt_Phone)
            {
                CheckLength(5, 15, txt, err_Phone);
                
                SetButtonPlaceHolderLeave(txt, StaticString.phonePlaceHolderText);
                
            }

        }

        private void txt_Enter_Enter(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            if (sender == txt_User)
            {
                SetButtonPlaceHolderEnter(txt, StaticString.userPlaceHolderText);
            }
            else if (sender == txt_Pass)
            {
                SetButtonPlaceHolderEnter(txt, StaticString.passwordPlaceHolderText);
               
            }
            else if (sender == txt_ConfirmPass)
            {
                SetButtonPlaceHolderEnter(txt, StaticString.confirmPassPlaceHolderText);
                
            }
            else if (sender == txt_Name)
            {
                SetButtonPlaceHolderEnter(txt, StaticString.namePlaceHolderText);
            }
            else if (sender == txt_Family)
            {
                SetButtonPlaceHolderEnter(txt, StaticString.familyPlaceHolderText);
            }
            else if (sender == txt_Email)
            {
                SetButtonPlaceHolderEnter(txt, StaticString.emailPlaceHolderText);

            }
            else if (sender == txt_Phone)
            {
                SetButtonPlaceHolderEnter(txt, StaticString.phonePlaceHolderText);
            }

        }

        private void txt_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

           
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
           
            if(err_User.Visible == false && err_Pass.Visible == false && err_Name.Visible == false && err_Family.Visible == false && err_Email.Visible == false)
            {
                var isDupp = DbCommandContext.IsDupplicate(txt_User.Text.ToUpper().Trim());
                if (isDupp)
                {
                    MessageBox.Show("کلمه کاربری با این نام وجود دارد");
                }
                else
                {
                    var salt = Encrypter.GetSalt();
                    var hashedpass = Encrypter.GetHash(txt_Pass.Text, salt);
                    var randomNumber = new Random().Next(10000);
                    var res = DbCommandContext.Register(txt_Name.Text, txt_Family.Text, txt_User.Text.ToUpper(), hashedpass, txt_Email.Text, txt_Phone.Text, randomNumber, salt);
                    if (res)
                    {
                        Form frm = new Form1();
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            
            
        }
    
    }
}
