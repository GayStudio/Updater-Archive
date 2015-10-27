using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

namespace MCUpdater
{
    class cdn
    {
        FileStream fs;
        XmlDocument f;
        XmlElement root;
        public string path = x.path + x.updpath + "cdn.xml";
        /// <summary>
        /// 构造函数
        /// </summary>
        public cdn()
        {
            fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            f = new XmlDocument();
            f.Load(fs);
            root = f.DocumentElement;
        }

        void save()
        {
            fs.SetLength(0);
            f.Save(fs);
        }

        public Dictionary<string,string> getByIndex(int index)
        {
            var e = root.ChildNodes[index];
            return new Dictionary<string, string>
            {
                {"url", e.InnerText},
                {"xml", e.Attributes["xml"].Value},
                {"desc", e.Attributes["desc"].Value},
                {"id", e.Name},
            };
        }

        public Dictionary<string,string> get(string id)
        {
            var e = root.GetElementsByTagName(id)[0];
            return new Dictionary<string, string>
            {
                {"url", e.InnerText},
                {"xml", e.Attributes["xml"].Value},
                {"desc", e.Attributes["desc"].Value},
                {"id", id},
            };
        }

        public XmlNodeList list()
        {
            return root.ChildNodes;
        }
    }
}
