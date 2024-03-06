using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ASM_C_3.Library
{
    public class Lib
    {
        //Login
        public bool checklogin(string id, string pw)
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";
            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string query = "SELECT Role FROM Users where UserName = @UserID and PassWord = @Password";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@UserID", id);
                        command.Parameters.AddWithValue("@Password", pw);
                        object result = command.ExecuteScalar();
                        return result != null && result != DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không tồn tại: " + ex.Message);
                return false;
            }
        }
        public string GetChucVu(string id, string pw)
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";
            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string query = "SELECT Role FROM Users where UserName = @UserID and PassWord = @Password";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@UserID", id);
                        command.Parameters.AddWithValue("@Password", pw);
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không tồn tại: " + ex.Message);
                return null;
            }
        }
        //QLĐ
        public DataTable GetInfo()
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";
            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string query = "select gr.MASV,\r\n\t\tgr.TiengAnh,\r\n\t\tgr.TinHoc,\r\n\t\tgr.GDTC,\r\n\t\tst.HoTen \r\nfrom Grade gr\r\njoin Student st\r\non gr.MASV = st.MASV";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dtResult = new DataTable();
                        adapter.Fill(dtResult);
                        return dtResult;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
                return null;
            }
        }
        public DataTable SearchInfo(string id)
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";
            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string query = "select gr.MASV,\r\n\t\tgr.TiengAnh,\r\n\t\tgr.TinHoc,\r\n\t\tgr.GDTC,\r\n\t\tst.HoTen\r\nfrom Grade gr\r\njoin Student st\r\non st.MASV = gr.MASV\r\nwhere gr.MASV like @Id ";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        // Đối tượng SqlDataAdapter để lưu trữ dữ liệu kết quả từ câu truy vấn
                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        // Đối tượng DataTable để lưu trữ dữ liệu kết quả
                        DataTable dtResult = new DataTable();

                        // Đổ dữ liệu từ adapter vào DataTable
                        adapter.Fill(dtResult);

                        // Trả về DataTable chứa kết quả tìm kiếm
                        return dtResult;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message);
                return null;
            }
        }
        public void Update(string masv, double ta, double th, double gdtc)
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";

            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();

                    // Câu truy vấn UPDATE để cập nhật thông tin điểm của sinh viên có mã số sinh viên (MASV) cụ thể
                    string query = "UPDATE Grade SET TiengAnh = @TiengAnh, TinHoc = @TinHoc, GDTC = @GDTC WHERE MASV = @MASV";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        // Thêm các tham số vào câu truy vấn để tránh lỗi SQL Injection và bảo mật hơn
                        command.Parameters.AddWithValue("@TiengAnh", ta);
                        command.Parameters.AddWithValue("@TinHoc", th);
                        command.Parameters.AddWithValue("@GDTC", gdtc);
                        command.Parameters.AddWithValue("@MASV", masv);

                        // Thực hiện câu truy vấn UPDATE
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã cập nhật thông tin điểm của sinh viên có MASV = " + masv);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên có MASV = " + masv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin điểm: " + ex.Message);
            }
        }
        public DataTable GetTop3Students()
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";

            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();

                    // Câu truy vấn lấy danh sách 3 sinh viên có điểm tổng kết cao nhất cho mỗi môn học
                    string query = "SELECT TOP 3 gr.MASV, gr.TiengAnh, gr.TinHoc, gr.GDTC, st.HoTen " +
                                   "FROM Grade gr " +
                                   "JOIN Student st ON gr.MASV = st.MASV " +
                                   "ORDER BY gr.TiengAnh + gr.TinHoc + gr.GDTC DESC";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dtResult = new DataTable();
                        adapter.Fill(dtResult);

                        // Trả về DataTable chứa kết quả danh sách 3 sinh viên có điểm tổng kết cao nhất
                        return dtResult;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu danh sách: " + ex.Message);
                return null;
            }
        }
        public void Addres(string masv,double ta, double th,double gdtc)
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";

            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();

                    // Câu truy vấn INSERT INTO để thêm điểm mới cho sinh viên có mã số sinh viên (MASV) cụ thể
                    string query = "INSERT INTO Grade (TiengAnh, TinHoc, GDTC) VALUES (@TiengAnh, @TinHoc, @GDTC) where Masv=@MASV";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        // Thêm các tham số vào câu truy vấn để tránh lỗi SQL Injection và bảo mật hơn
                        command.Parameters.AddWithValue("@MASV", masv);
                        command.Parameters.AddWithValue("@TiengAnh", ta);
                        command.Parameters.AddWithValue("@TinHoc", th);
                        command.Parameters.AddWithValue("@GDTC", gdtc);

                        // Thực hiện câu truy vấn INSERT INTO để thêm điểm mới cho sinh viên
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm điểm cho sinh viên có MASV = " + masv);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên có MASV = " + masv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm điểm: " + ex.Message);
            }
        }
        //QLSV
        public DataTable GetInfoSV()
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";
            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string query = "SELECT MASV,HoTen,Email,SDT,DiaChi,Hinh,CASE WHEN Gioitinh = 0 THEN 'Nam' ELSE 'Nu' END AS GioiTinh\r\nFROM\r\n    Student;";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dtResult = new DataTable();
                        adapter.Fill(dtResult);
                        return dtResult;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
                return null;
            }
        }
        string imgname = "";
        public Image GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp|All Files (*.*)|*.*";
            openFileDialog.Title = "Chọn hình ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImageFileName = openFileDialog.FileName;
                int lastIndexOfSlash = selectedImageFileName.LastIndexOf('\\'); // Tìm vị trí của ký tự '\\' cuối cùng trong chuỗi
                if (lastIndexOfSlash >= 0)
                {
                    imgname = selectedImageFileName.Substring(lastIndexOfSlash + 1); // Cắt ra các ký tự sau ký tự '\\'
                }
                return Image.FromFile(selectedImageFileName);
                
            }

            return null;
        }
        public void SaveInfo(string MASV, string HoTen, string Email, string SDT, string Gioitinh, string DiaChi)
        {
            string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";

            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Student WHERE MASV = @MASV";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connect))
                    {
                        checkCommand.Parameters.AddWithValue("@MASV", MASV);
                        int existingCount = (int)checkCommand.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("Mã SV đã tồn tại trong cơ sở dữ liệu. Vui lòng nhập mã SV khác!");
                            return;
                        }
                    }

                    // Chuyển đổi giới tính từ chuỗi "Nam" và "Nữ" thành giá trị 0 và 1
                    int gioiTinhValue = (Gioitinh == "Nam") ? 0 : 1;

                    // Câu truy vấn INSERT INTO để thêm dòng mới vào bảng Student
                    string query = "INSERT INTO Student VALUES (@MASV, @HoTen, @Email, @SDT, @Gioitinh, @DiaChi, @Hinh)";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        // Thêm các tham số vào câu truy vấn để tránh lỗi SQL Injection và bảo mật hơn
                        command.Parameters.AddWithValue("@MASV", MASV);
                        command.Parameters.AddWithValue("@HoTen", HoTen);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@SDT", SDT);
                        command.Parameters.AddWithValue("@Gioitinh", gioiTinhValue);
                        command.Parameters.AddWithValue("@DiaChi", DiaChi);
                        command.Parameters.AddWithValue("@Hinh", imgname);

                        // Thực hiện câu truy vấn INSERT INTO để thêm dòng mới vào bảng Student
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã lưu thông tin thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi lưu thông tin!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin: " + ex.Message);
            }
        }

        public void UpdateSV(string MASV, string HoTen, string Email, string SDT, int Gioitinh, string DiaChi)
    {
        string conString = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";

        try
        {
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();

                // Câu truy vấn UPDATE để cập nhật thông tin sinh viên có mã số sinh viên (MASV) cụ thể
                string query = "UPDATE Student SET HoTen = @HoTen, Email = @Email, SDT = @SDT, Gioitinh = @Gioitinh, DiaChi = @DiaChi WHERE MASV = @MASV";

                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    // Thêm các tham số vào câu truy vấn để tránh lỗi SQL Injection và bảo mật hơn
                    command.Parameters.AddWithValue("@HoTen", HoTen);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@SDT", SDT);
                    command.Parameters.AddWithValue("@Gioitinh", Gioitinh);
                    command.Parameters.AddWithValue("@DiaChi", DiaChi);
                    command.Parameters.AddWithValue("@MASV", MASV);

                        // Thực hiện câu truy vấn UPDATE
                        int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã cập nhật thông tin của sinh viên có MASV = " + MASV, "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên có MASV = " + MASV, "Thông báo");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi cập nhật thông tin sinh viên: " + ex.Message, "Lỗi");
        }
    }
        public void DelSV(string masv)
        {
            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên có MASV = " + masv + "?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";

                try
                {
                    using (SqlConnection connect = new SqlConnection(con))
                    {
                        connect.Open();

                        // Câu truy vấn DELETE để xóa thông tin sinh viên có mã số sinh viên (MASV) cụ thể
                        string query = "DELETE FROM Student WHERE MASV = @MASV";

                        using (SqlCommand command = new SqlCommand(query, connect))
                        {
                            // Thêm tham số vào câu truy vấn để tránh lỗi SQL Injection và bảo mật hơn
                            command.Parameters.AddWithValue("@MASV", masv);

                            // Thực hiện câu truy vấn DELETE
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Đã xóa thông tin sinh viên có MASV = " + masv, "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy sinh viên có MASV = " + masv, "Thông báo");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa thông tin sinh viên: " + ex.Message, "Lỗi");
                }
            }
        }


    }
}



