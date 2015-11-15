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
        XmlElement setd;
        XmlElement lib;
        XmlElement player;
        public string path = x.path + x.updpath + "config.xml";
        /// <summary>
        /// 构造函数
        /// </summary>
        public config()
        {
            fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            f = new XmlDocument();
            f.Load(fs);
            root = f.DocumentElement;
            setd = (XmlElement)root.SelectSingleNode("set");
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
        public string get(string id)
        {
            return setd.GetElementsByTagName(id)[0].InnerText;
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="v">值</param>
        public void set(string id, string v)
        {
            var d = setd.GetElementsByTagName(id);
            if (d.Count < 1)
            {
                add(id, v);
            }
            else
            {
                d[0].InnerText = v;
                save();
            }
        }
        /// <summary>
        /// 添加启动器设置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="v">值</param>
        public void padd(string id, string v)
        {
            player.AppendChild(f.CreateElement(id)).InnerText = v;
            save();
        }

        /// <summary>
        /// 获取启动器设置
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>值</returns>
        public string pget(string id)
        {
            return player.GetElementsByTagName(id)[0].InnerText;
        }
        /// <summary>
        /// 保存启动器设置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="v">值</param>
        public void pset(string id, string v)
        {
            var d = player.GetElementsByTagName(id);
            if (d.Count < 1)
            {
                padd(id, v);
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
        public void add(string id, string v)
        {
            player.AppendChild(f.CreateElement(id)).InnerText = v;
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
