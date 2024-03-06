using ASM_C_3.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_C_3
{
    public partial class QLD : Form
    {
        private Lib library;
        public QLD()
        {
            InitializeComponent();
            library = new Lib();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Trim();
            DataTable result = library.SearchInfo(id);
            if (result != null && result.Rows.Count > 0)
            {
                dataGridView1.DataSource = result;
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu phù hợp.");
                // Nếu muốn xóa hiển thị dữ liệu trước đó trong dataGridView1
                //dataGridView1.DataSource = null;
            }
        }
        private void Lock()
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = false;
        }
        private void Unlock()
        {
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = true;
        }
        private void QLD_Load(object sender, EventArgs e)
        {
            Lock();
            dataGridView1.DataSource = library.GetInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string masv = textBox2.Text.Trim();
            double dta = double.Parse(textBox3.Text);
            double dth = double.Parse(textBox4.Text);
            double dgdtc = double.Parse(textBox5.Text);
            library.Update(masv, dta, dth, dgdtc);
            double diemTB = (dta + dth+ dgdtc) / 3;
            label10.Text = diemTB.ToString("N2");
            dataGridView1.DataSource = library.GetTop3Students();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource= library.GetInfo();
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            label4.Text = string.Empty;
            label10.Text = string.Empty;
            Unlock();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Unlock();
            textBox2.Enabled = false; 
            button2.Enabled = false;
            int rowIndex = e.RowIndex;

            // Chắc chắn rằng người dùng không click vào tiêu đề của cột
            if (rowIndex >= 0)
            {
                // Lấy dữ liệu từ dòng được click
                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                // Kiểm tra nếu ô cần lấy dữ liệu không phải là null trước khi lấy dữ liệu
                string maSV = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : "";
                string hoten = row.Cells[4].Value != null ? row.Cells[4].Value.ToString() : "";
                string ta = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "";
                string th = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : "";
                string gdtc = row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : "";

                // Sử dụng phương thức TryParse để chuyển đổi giá trị sang số nguyên an toàn
                double  taValue, thValue, gdtcValue;
                if (double.TryParse(ta, out taValue) && double.TryParse(th, out thValue) && double.TryParse(gdtc, out gdtcValue))
                {
                    // Tính điểm trung bình của 3 môn học
                    double diemTB = (taValue + thValue + gdtcValue) / 3;

                    // Gán dữ liệu lấy được vào các điều khiển trên giao diện
                    label4.Text = hoten;
                    textBox2.Text = maSV;
                    textBox3.Text = ta;
                    textBox4.Text = th;
                    textBox5.Text = gdtc;
                    label10.Text = diemTB.ToString("N2");
                }
              
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
