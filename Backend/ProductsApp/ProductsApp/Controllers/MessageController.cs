using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;
using Oracle.ManagedDataAccess.Client;



namespace ProductsApp.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SendMessage([FromBody]Message message)
        {
            int status = 0;
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
            OracleCommand cmd = new OracleCommand();

            cmd.Connection = conn;
            cmd.CommandText = "insert into MESSAGE(SENDER_ID,RECEIVER_ID,SEND_TIME,CONTENT) " +
                    "values('" + message.Sender_ID + "','" + message.Receiver_ID + "','" + message.Send_Time + "','" + message.Content + "')";
            OracleDataReader rd = cmd.ExecuteReader();
            int result = cmd.ExecuteNonQuery();
            if (result != 1)//插入出现错误
            {
                status = 1;
            }

            //关闭数据库连接
            conn.Close();

            //返回信息
            return Ok(status);


        }

         [HttpGet]
        public IHttpActionResult GetMessage(string Sender_ID, string Receiver_ID)
        {
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

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select sender_id,content " +
                              "from message " +
                              "where sender_id =  '" + Sender_ID + "' and " +
                              "receiver_id = '" + Receiver_ID + "'"+
                              "order by send_time desc";
            OracleDataReader rd = cmd.ExecuteReader();
            List<string> ContentList = new List<string>();
            List<int> IdentityList = new List<int>();
            while (rd.Read())
            {
                string name = rd["SENDER_ID"].ToString();
                if (name == Sender_ID) IdentityList.Add(0);
                else IdentityList.Add(1);
                string content = rd["CONTENT"].ToString();
                ContentList.Add(content);
            }
            Tuple<List<int>, List<string>> result = new Tuple<List<int>, List<string>>(null, null);
            result = new Tuple<List<int>, List<string>> (IdentityList, ContentList);
            return Json(result);
        }


    }
}
