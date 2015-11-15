using System.Runtime.Serialization;
using System.Collections;

namespace MCUpdater
{
    [DataContract]
    public class forgeJsonData
    {
        [DataMember(IsRequired = true)]
        public string minecraftArguments { get; set; }

        [DataMember(IsRequired = true)]
        public string mainClass { get; set; }

        [DataMember(IsRequired = true)]
        public string assetIndex { get; set; }

        [DataMember(IsRequired = true)]
        public ArrayList libraries { get; set; }

        public string inheritsFrom { get; set; }
        public string jar { get; set; }
    }
}