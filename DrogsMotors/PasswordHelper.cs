using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DrogsMotors
{
    public static class PasswordHelper
    {
        public static string GetPasswordHash(string pwd)
        {
            var result = "";

            using (var md5 = MD5.Create())
            {
                result = GetMd5Hash(md5, pwd);
            }

            return result;
        }

        public static bool VerifyPassword(string pwd, string hash)
        {
            var r = false;

            using (var md5 = MD5.Create())
            {
                r = VerifyMd5Hash(md5, pwd, hash);
            }

            return r;
        }

        private static string GetMd5Hash(MD5 hasher, string str)
        {
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(str));

            var strBldr = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                strBldr.Append(bytes[i].ToString("x2"));
            }

            return strBldr.ToString();
        }

        private static bool VerifyMd5Hash(MD5 hasher, string input, string hash)
        {
            var currHash = GetMd5Hash(hasher, input);
            var cmp = StringComparer.OrdinalIgnoreCase;
            return 0 == cmp.Compare(currHash, hash);
        }
    }
}


