using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MCUpdater
{
    /// <summary>
    /// 更新器配置文件操作类
    /// </summary>
    class config
    {
        FileStream fs;
        XmlDocument f;
        XmlElement root;
        XmlElement set;
        XmlElement lib;
        XmlElement player;
        public string path = x.path + x.updpath + "config.xml";
        /// <summary>
        /// 构造函数
        /// </summary>
        public config()
        {
            fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            f = new XmlDocument();
            f.Load(fs);
            root = f.DocumentElement;
            set = (XmlElement)root.SelectSingleNode("set");
            lib = (XmlElement)root.SelectSingleNode("lib");
            player = (XmlElement)root.SelectSingleNode("player");
        }

        void save()
        {
            fs.SetLength(0);
            f.Save(fs);
        }
        /// <summary>
        /// 获取设置
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>值</returns>
        public string getOpt(string id)
        {
            return set.GetElementsByTagName(id)[0].InnerText;
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="v">值</param>
        public void setOpt(string id,string v)
        {
            var d = set.GetElementsByTagName(id);
            if(d.Count < 1)
            {
                addOpt(id,v);
            }
            else
            {
                d[0].InnerText = v;
                save();
            }
        }
        /// <summary>
        /// 添加设置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="v">值</param>
        public void addOpt(string id, string v)
        {
            set.AppendChild(f.CreateElement(id)).InnerText = v;
            save();
        }
        /// <summary>
        /// 获取组件信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public Dictionary<string,string> getLib(string id)
        {
            var e = lib.GetElementsByTagName(id)[0];
            return new Dictionary<string, string>
            {
                {"id",id },
                {"ver", e.Attributes["ver"].Value},
                {"path", e.Attributes["path"].Value},
                {"desc",e.InnerText},
            };
        }

        public void setLibVer(string id, string ver)
        {
            lib.GetElementsByTagName(id)[0].Attributes["ver"].Value = ver;
            save();
        }
    }
}
