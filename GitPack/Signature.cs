using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPack
{
    public class Signature
    {
        private string destination;
        private string source; //关于随机创建可参考我用c++写的 https://github.com/kriszhengs/Hash/tree/master/RandCreat
        const string Format = "https://education.github.com/student/verify?school_id={0}&student_id={1}&signature={2}";
        private string secret_key;
        private string school_id;

        public Signature(string destination, string source, string secret_key, string school_id)
        {
            this.destination = destination;
            this.source = source;
            this.secret_key = secret_key;
            this.school_id = school_id;
        }

        public void Run(out int falure, out int sussuess)
        {
            falure = sussuess = 0;
            FileStream sfs = new FileStream(source, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(sfs);
            FileStream dfs = new FileStream(destination, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(dfs);
            Encode encode = new Encode(secret_key, school_id);
            CheckStuID checkStu = new CheckStuID();
            string student_id;
            while ((student_id = sr.ReadLine()) != null)
            {

                if (checkStu.Islegal(student_id))
                {
                    sussuess++;
                    sw.WriteLine(Format, school_id, student_id, encode.encode(student_id));
                }
                else falure++;
            }
            sw.Flush();
            sr.Close();
            sfs.Close();
            sw.Close();
            dfs.Close();
        }
    }
}
