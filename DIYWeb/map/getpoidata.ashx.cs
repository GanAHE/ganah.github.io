using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MySql.Data.MySqlClient;

namespace WebGISWhu07
{
    /// <summary>
    /// getpoidata 的摘要说明
    /// 
    /// </summary>
    public class getpoidata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            

            //HTTP协议服务器端返回数据类型，此处为文本内容
           context.Response.ContentType = "text/xml";
            //context.Response.Write("Hello World");
            //创建数据库连接对象
            MySqlConnection conn = CreateConn();
            //构建SQL数据查询语句，此处我们选择 poi表中所有记录
            string sqlQuery = "SELECT * FROM poi";
            //创建数据集查询对象
            MySqlCommand comm = new MySqlCommand(sqlQuery, conn);
            //打开数据库
            conn.Open();
            //执行数据查询命令，并返回查询数据集
            MySqlDataReader dr = comm.ExecuteReader();
          
            //循环遍历所有数据，并构建XML数据结构
            string strXML = "<?xml version=\"1.0\"?><poi>";
            while(dr.Read())
            {
                int id = dr.GetInt32("id");
                string sName = dr.GetString("name");
                float x  = dr.GetFloat("x");
                float y  = dr.GetFloat("y");
                strXML += "<p id=\""+ id +"\" name=\""+sName+"\" x=\""+x+"\" y=\""+ y +"\"/>";
            }
            strXML += "</poi>";
            context.Response.Write(strXML);
             //关闭数据库连接
            conn.Close();
        }

        public static MySqlConnection CreateConn()
        {
            //从配置文件中获取数据库连接字符串
            string _conn = WebConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            //创建Mysql数据库连接对象
            MySqlConnection conn = new MySqlConnection(_conn);
            //返回数据库连接对象引用
            return conn;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}