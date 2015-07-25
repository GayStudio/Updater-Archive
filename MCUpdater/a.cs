using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace MCUpdater
{
    /// <summary>
    /// Access数据库操作类
    /// </summary>
    class a
    {
        private OleDbConnection conn;
        private OleDbDataReader reader;

        /// <summary>
        /// 构造函数
        /// </summary>
        public a()
        {
            try
            {
                conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + x.path + x.updpath + x.db);
                conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message , ex);
            }
        }

        /// <summary>
        /// 只是执行sql查询
        /// </summary>
        /// <param name="sql">sql语句，可以是queryMode结构内的成员</param>
        public void query(string sql)
        {
            try
            {
                reader = new OleDbCommand(sql, conn).ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n错误语句：" + sql, "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 查询sql并返回结果
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public DataSet fetch(string sql)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 获取设置（option表内）
        /// </summary>
        /// <param name="k">设置名</param>
        /// <returns></returns>
        public string getOpt(string k)
        {
            DataSet d = fetch("select * from `option` where `k` = '" + k + "'");
            return d.Tables[0].Rows[0]["v"].ToString();
        }

        /// <summary>
        /// 保存设置（option表内）
        /// </summary>
        /// <param name="k">设置名</param>
        /// <param name="k">设置值</param>
        /// <returns></returns>
        public void setOpt(string k , string v)
        {
            query("update `option` set `v` = '" + v + "' where `k` = '" + k + "'");
        }

        /// <summary>
        /// 获取lib信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<String, String> getLib(string id)
        {
            DataSet d = fetch("select * from `lib` where `id` = '" + id + "'");
            Dictionary<String, String> r = new Dictionary<String, String>();
            r.Add("id", d.Tables[0].Rows[0]["id"].ToString());
            r.Add("desc", d.Tables[0].Rows[0]["desc"].ToString());
            r.Add("ver", d.Tables[0].Rows[0]["ver"].ToString());
            r.Add("path", d.Tables[0].Rows[0]["path"].ToString());
            return r;
        }

        /// <summary>
        /// 设置组件库版本
        /// </summary>
        /// <param name="id">组件标识符</param>
        /// <param name="ver">新版本</param>
        public void setLibVer(string id, string ver)
        {
            query("update `lib` set `ver` = '" + ver + "' where `id` = '" + id + "'");
        }

        /*
        /// <summary>
        /// 读一行。需要先执行query，返回读取是否成功
        /// </summary>
        /// <param name="i">item结构</param>
        /// <returns>读取是否成功</returns>
        public bool read(ref item i)
        {
            if (reader.Read())
            {
                i.id = (int)reader["id"];
                i.isValid = (bool)reader["isValid"];
                i.process = reader["process"].ToString();
                i.title = reader["title"].ToString();
                i.className = reader["className"].ToString();
                i.width = (int)reader["width"];
                i.height = (int)reader["height"];
                i.beWrite = reader["beWrite"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        /// <summary>
        /// 析构函数
        /// </summary>
        ~a() {
            if (conn.State == ConnectionState.Connecting)
            {
                conn.Close();
            }

        }
    }
}
