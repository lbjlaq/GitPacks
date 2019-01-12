using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace GitPack
{
    public static class RandCreat
    {
        public static void Run(string filepath,Int64 num,int len=7)
        {
            
            StreamWriter sw = new StreamWriter(      
                new FileStream(filepath, FileMode.Create, FileAccess.Write)
            );
            HashSet<string> set = new HashSet<string>();
            Random rd = new Random();
            Int64 cnt = 0;
            StringBuilder name = new StringBuilder();
            while (cnt<num)
            {
                for (int i=0; i<len; ++i)
                {
                    int ra = rd.Next(3);
                    if (ra == 1)
                        name.Append((char)(rd.Next('a', 'z'+1)));
                    else if (ra == 2) name.Append((char)(rd.Next('A', 'Z'+1)));
                    else name.Append(rd.Next(0, 10).ToString());

                }
                if (!set.Contains(name.ToString()))
                {
                    set.Add(name.ToString());
                    sw.WriteLine(name.ToString());
                    cnt++;
                    name.Clear();
                }
            }
            sw.Close();
        }
    }
}
