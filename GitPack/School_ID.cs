using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPack
{
    public sealed class SCHOOL_ID : ConfigurationSection
    {
        [ConfigurationProperty("school_id", DefaultValue = "2333")]
        public String School_id
        {
            get
            {
                return (string)this["school_id"];
            }
            set
            {
                this["school_id"] = value;
            }
        }
    }
}
