using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLCuaHangNoiThat.Forms
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            loadImage();
        }


        private void loadImage()
        {
            pictureBox1.Image = Properties.Resources.images;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // Icon user nhỏ bên trái ô username (giả sử tên là picUserIcon)
            userpic.Image = Properties.Resources.user;
            userpic.SizeMode = PictureBoxSizeMode.Zoom;

            // Icon lock bên trái ô password (giả sử tên là picLockIcon)
            lockpic.Image = Properties.Resources.lock1;
            lockpic.SizeMode = PictureBoxSizeMode.Zoom;

            showpic.Image = Properties.Resources.eyeopen; // Mắt mở
            showpic.SizeMode = PictureBoxSizeMode.Zoom;
            showpic.Cursor = Cursors.Hand;

            hidepic.Image = Properties.Resources.unhide; // Mắt nhắm (hoặc eye_poen)
            hidepic.SizeMode = PictureBoxSizeMode.Zoom;
            hidepic.Cursor = Cursors.Hand;
            hidepic.BringToFront();

        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clear_Click_1(object sender, EventArgs e)
        {
            username.Clear();
            passuser.Clear();
            username.Focus();
        }

        private void blogin_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(passuser.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            // ⭐ Dùng chung DatabaseHelper (giống form update đơn hàng)
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string hashedPassword = SecurityHelper.HashSha256(passuser.Text);

                    string query = @"SELECT nv.MaNhanVien, nv.Ho, nv.Ten, nv.MaChucVu
                                     FROM NhanVien nv
Join chucvu cv ON nv.MaChucVu = cv.MaChucVu
Join taikhoan tk ON nv.MaNhanVien = tk.MaNhanVien
                                     WHERE tk.TenDangNhap = @user 
                                     AND tk.MatKhau = @pass 
                                     AND nv.TrangThai = 'Active'";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username.Text);
                        cmd.Parameters.AddWithValue("@pass", hashedPassword);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int macv = reader.GetInt32("MaChucVu");
                                int[] allowedRoles = {1};

                                if (allowedRoles.Contains(macv))
                                {
                                    FormMain main = new FormMain();
                                    main.Show();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Bạn không có quyền truy cập");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

       

        private void hidepic_Click(object sender, EventArgs e)
        {
            
                showpic.BringToFront();
                passuser.PasswordChar = '\0';
            
        }

        private void showpic_Click(object sender, EventArgs e)
        {
            
                hidepic.BringToFront();
                passuser.PasswordChar = '*';
            

        }
    }
}
