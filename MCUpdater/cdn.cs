using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System;

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

        /// <summary>
        /// 用ID获取CDN节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 用索引获取CDN信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Dictionary<string, string> get(int index)
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

        /// <summary>
        /// 用索引获取CDN Header信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Dictionary<string, string> getHeaders(int index)
        {
            return getHeaders(root.ChildNodes[index].Name);
        }

        /// <summary>
        /// 用ID获取CDN Header信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Dictionary<string, string> getHeaders(string id)
        {
            var e = root.GetElementsByTagName(id)[0];
            Dictionary<string,string> headers;
            if (e.Attributes["headers"] != null)
            {
                string[] headerList = e.Attributes["headers"].Value.Split(',');
                headers = new Dictionary<string, string>();
                foreach (string xhead in headerList)
                {
                    if (e.Attributes["header_" + xhead] != null)
                    {
                        headers.Add(xhead, e.Attributes["header_" + xhead].Value);
                    }
                    else
                    {
                        headers.Add(xhead, "");
                    }
                }
            }
            else
            {
                headers = null;
            }
            return headers;
        }

        public XmlNodeList list()
        {
            return root.ChildNodes;
        }
    }
}
