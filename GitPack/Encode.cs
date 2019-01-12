using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GitPack
{
    class Encode
    {
        private byte[] secret_key;
        private string school_id;
        public Encode(string secret_key, string school_id)
        {
            this.secret_key = Encoding.ASCII.GetBytes(secret_key);
            this.school_id = school_id;
        }
        public Encode()
        {

        }
        public string encode(string student_id)
        {
            HMACSHA256 hMACSHA256 = new HMACSHA256(secret_key);
            byte[] byteArray = Encoding.ASCII.GetBytes(school_id + student_id);
            MemoryStream stream = new MemoryStream(byteArray);
            return hMACSHA256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
}
