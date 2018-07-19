using System;
using ProductsApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Web.Http;
using System.Data;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class Moment_TagController : ApiController
    {
        /// <summary>
        /// 通过Tag的内容得到与之相关的所有动态
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpGet]
        public Tuple<List<Moment>,int> Followers(int Page, int PageSize, string TagContent)
        {
            //HttpRequestMessage response = new HttpRequestMessage();
            
            //连接数据库
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17";
            OracleConnection conn = new OracleConnection(connStr);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            //动态ID的数据集
            DataSet MIDSet = new DataSet();
            //动态的数据集
            DataSet MSet = new DataSet();
            //SQL操作
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select MOMENT_ID from MOMENT_TAG where TAG ='" + TagContent + "'";
            cmd.Connection = conn;
            //动态的数组
            List<Moment> moments = new List<Moment>();
            OracleDataAdapter Orda = new OracleDataAdapter(cmd);
            Orda.Fill(MIDSet, PageSize * (Page - 1),PageSize,"MOMENT_TAG");
            foreach (DataRow row in MIDSet.Tables[0].Rows)
            {
                cmd.CommandText = "select * from MOMENT where ID ='" + row[0] + "'";
                cmd.Connection = conn;
                OracleDataReader rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    string id = rd["ID"].ToString();
                    string sender_id = rd["SENDER_ID"].ToString();
                    string content = rd["CONTENT"].ToString();
                    int likes = int.Parse(rd["LIKE_NUM"].ToString());
                    int forwards = int.Parse(rd["FORWARD_NUM"].ToString());
                    int collects = int.Parse(rd["COLLECT_NUM"].ToString());
                    int comments = int.Parse(rd["COMMENT_NUM"].ToString());
                    DateTime time = DateTime.Parse(rd["TIME"].ToString().Replace('T', ' '));
                    moments.Add(new Moment(id, sender_id, content, likes, forwards, collects, comments, time));
                }
            }
            cmd.CommandText = "select * from FOLLOW_TAG where TAG ='" + TagContent + "'";
            cmd.Connection = conn;
            OracleDataReader r = cmd.ExecuteReader();
            int Flowers = 0;
            while(r.Read())
            {
                Flowers++;
            }
            conn.Close();

            Tuple<List<Moment>, int> result = new Tuple<List<Moment>, int>(null, 0);
            if (moments.Count != 0)
            {
                result = new Tuple<List<Moment>, int>(moments, Flowers);
            }
            else if (moments.Count == 0)
                result = new Tuple<List<Moment>, int>(null, 0);

            return result;
        }
    }
}
