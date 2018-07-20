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
        [HttpGet]
        public IHttpActionResult SendMessage(Message message)
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


    }
}
