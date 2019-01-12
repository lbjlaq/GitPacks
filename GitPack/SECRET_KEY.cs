using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPack
{
    public sealed class SECRET_KEY : ConfigurationSection
    {
        [ConfigurationProperty("secret_key", DefaultValue = "2333")]
        public String secret_key
        {
            get
            {
                return (string)this["secret_key"];
            }
            set
            {
                this["secret_key"] = value;
            }
        }
    }
}
