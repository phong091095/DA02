using ASM_C_3.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_C_3
{
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text;
            string password = textBox2.Text;

            // Kiểm tra thông tin đăng nhập
            Lib libra = new Lib();
            string role = libra.GetChucVu(username, password);
            if (libra.checklogin(username, password)) // Gọi hàm checklogin và kiểm tra kết quả
            {
                MessageBox.Show("Đăng nhập thành công!");
                //this.Hide();
                // Thực hiện các hành động sau khi đăng nhập thành công
                if (role != null)
                {
                    if (role == "DT")
                    {
                        QLSV qLSV = new QLSV();
                        qLSV.Show();
                    }
                    if (role == "GV")
                    {
                        QLD qLD = new QLD();
                        qLD.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu. Vui lòng thử lại!");
            }
            
        }
    }
}
