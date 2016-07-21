using System.Runtime.Serialization;
using System.Collections;

namespace MoecraftPkgInstaller
{
    public class pkgJsonData
    {
        public string name { get; set; }
        public string desc { get; set; }
        public float ver { get; set; }
        public string path { get; set; }
        public string script { get; set; }
        public bool unpack { get; set; }
    }
}