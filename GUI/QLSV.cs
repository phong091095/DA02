using ASM_C_3.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ASM_C_3
{
    public partial class QLSV : Form
    {
        Lib library;
        public QLSV()
        {
            InitializeComponent();
            library = new Lib();
        }
        public void Lock()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }
        public void Unlock()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            button2.Enabled = true;
        }
        private void QLSV_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = library.GetInfoSV();
            button5.Visible = false;
            Lock();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Unlock();
            button5.Visible=false;
            button4.Enabled = true;
            pictureBox1.Visible = true;
            button1.Enabled = true;
            button3.Enabled = true;
            button2.Enabled =false;
            int rowIndex = e.RowIndex;

            // Chắc chắn rằng người dùng không click vào tiêu đề của cột
            if (rowIndex >= 0)
            {
                // Lấy dữ liệu từ dòng được click
                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                // Kiểm tra nếu ô cần lấy dữ liệu không phải là null trước khi lấy dữ liệu
                string maSV = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : "";
                string hoten = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "";
                string email = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : "";
                string sdt = row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : "";
                string dc = row.Cells[4].Value != null ? row.Cells[4].Value.ToString() : "";
                string hinh = row.Cells[5].Value != null ? row.Cells[5].Value.ToString() : "";
                string gtinh = row.Cells[6].Value != null ? row.Cells[6].Value.ToString() : "";
                string img = @"ASM_C#3\Image\" + hinh;
                textBox1.Text = maSV;
                textBox2.Text = hoten;
                textBox3.Text = email;
                textBox4.Text = sdt;
                textBox5.Text = dc;
                if (gtinh == "Nam")
                {
                    radioButton1.Checked = true;
                }
                else { radioButton2.Checked = true; }
                pictureBox1.Image = Image.FromFile(img);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Thay đổi chế độ hiển thị hình ảnh
                pictureBox1.Refresh();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text= string.Empty;
            button2.Enabled = true;
            pictureBox1.Visible = false;
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Image image = library.GetImage();

            if (image != null)
            {
                button5.Visible = false;
                pictureBox1.Visible = true;
                // Đưa hình ảnh lên PictureBox
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Thay đổi chế độ hiển thị hình ảnh
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string masv = textBox1.Text;
            string hoten = textBox2.Text;
            string email = textBox3.Text;
            string sdt = textBox4.Text;

            // Kiểm tra biểu thức chính quy cho email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValidEmail = Regex.IsMatch(email, emailPattern);

            // Kiểm tra số điện thoại là số và không quá 11 số
            string phonePattern = @"^[0-9]{1,10}$";
            bool isValidPhoneNumber = Regex.IsMatch(sdt, phonePattern);

            if (!isValidEmail)
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập đúng định dạng email.", "Thông báo");
                return;
            }

            if (!isValidPhoneNumber)
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại là số và không quá 10 số.", "Thông báo");
                return;
            }

            // Tiếp tục lưu thông tin vào cơ sở dữ liệu
            string gioitinh = "";
            if (radioButton1.Checked)
            {
                gioitinh = "Nam";
            }
            else if (radioButton2.Checked)
            {
                gioitinh = "Nữ";
            }
            string diachi = textBox5.Text;
            // Gọi phương thức SaveInfo trong class Lib để lưu thông tin vào cơ sở dữ liệu
            library.SaveInfo(masv, hoten, email, sdt, gioitinh, diachi);
            dataGridView1.DataSource = library.GetInfoSV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string masv = textBox1.Text;
            string hoten = textBox2.Text;
            string email = textBox3.Text;
            string sdt = textBox4.Text;

            // Kiểm tra biểu thức chính quy cho email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValidEmail = Regex.IsMatch(email, emailPattern);

            // Kiểm tra số điện thoại là số và không quá 11 số
            string phonePattern = @"^[0-9]{1,10}$";
            bool isValidPhoneNumber = Regex.IsMatch(sdt, phonePattern);

            if (!isValidEmail)
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập đúng định dạng email.", "Thông báo");
                return;
            }

            if (!isValidPhoneNumber)
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại là số và không quá 10 số.", "Thông báo");
                return;
            }

            // Tiếp tục lưu thông tin vào cơ sở dữ liệu
            int gioitinh=0 ;
            if (radioButton1.Checked)
            {
                gioitinh = 0;
            }
            else if (radioButton2.Checked)
            {
                gioitinh = 1;
            }
            string diachi = textBox5.Text;
            // Gọi phương thức SaveInfo trong class Lib để lưu thông tin vào cơ sở dữ liệu
            library.UpdateSV(masv, hoten, email, sdt, gioitinh, diachi);
            dataGridView1.DataSource = library.GetInfoSV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string masv = textBox1.Text;
            library.DelSV(masv);
            dataGridView1.DataSource = library.GetInfoSV();
        }
    }
}
