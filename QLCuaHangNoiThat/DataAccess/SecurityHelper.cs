using System;
using System.Security.Cryptography;
using System.Text;

namespace QLCuaHangNoiThat.DataAccess
{
    public static class SecurityHelper // Đổi thành static class
    {
        public static string HashSha256(string rawPassword)
        {
            if (string.IsNullOrEmpty(rawPassword))
            {
                return string.Empty;
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 1. Chuyển đổi chuỗi đầu vào thành mảng byte
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));

                // 2. Chuyển đổi mảng byte thành chuỗi hex (chuỗi hash)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}