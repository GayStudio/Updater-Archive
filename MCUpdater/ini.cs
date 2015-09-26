using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace MCUpdater
{
    /// <summary>
    /// INI 读取类
    /// </summary>
    public class ini
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string sPath;

        public ini(string path)
        {
            sPath = path;
            if(!File.Exists(path)) {
                File.Create(path);
            }
        }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>写了多少</returns>
        public long write(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, sPath);
        }
        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string read(string section, string key)
        {
            // 每次从ini中读取多少字节
            StringBuilder temp = new StringBuilder(255);
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, sPath);
            return temp.ToString();
        }
    }
}
