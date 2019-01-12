using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPack
{
    class CheckStuID
    {
        private HashSet<string> set;
        private string idaddress = "usedstudentid.txt";
        private StreamWriter sw;
        private FileStream fs;
        public CheckStuID()
        {

            set = new HashSet<string>();
            try
            {
                fs = new FileStream(idaddress, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    set.Add(str);
                    //Console.WriteLine(str);
                }
                sr.Close();
                fs.Close();

                fs = new FileStream(idaddress, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public bool Islegal(string student_id)
        {
            if (set.Contains(student_id))
            {
                return false;
            }
            else
            {
                set.Add(student_id);
                sw.WriteLine(student_id);
                sw.Flush();
                //sw.Close();
                //fs.Close();
                return true;
            }
        }
    }
}
