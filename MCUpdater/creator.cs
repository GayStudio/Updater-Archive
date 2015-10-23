using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MCUpdater
{
    static class creator
    {
        static void createConfig(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            File.Copy(path + "config.xml.new", path + "config.xml");
        }
    }
}
