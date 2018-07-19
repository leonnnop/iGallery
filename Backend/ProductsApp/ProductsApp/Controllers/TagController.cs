using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Text;
using System.Data.OracleClient;
//using Oracle.ManagedDataAccess.Client;

namespace iGallery.Controllers
{
    public class TagController : ApiController
    {
        /// <summary>
        /// 用户关注Tag
        /// </summary>
        /// <param name="TagName"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage AddTag(string TagName)
        {
            HttpResponseMessage response = Request.CreateResponse();
            
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
            cmd.CommandText = "select * from TAG where CONTENT ='" + TagName + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if(!rd.Read()) //Tag不存在，创建
            {
                cmd.CommandText = "insert into TAG values ('" + TagName + "')";
                if (cmd.ExecuteNonQuery() == 0)  //插入失败
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    TagName = null;
                }
                else
                    response.StatusCode = HttpStatusCode.OK;

            }
            response.Content = new StringContent(TagName, Encoding.Unicode);
            conn.Close();
            return response;
        }
    }
}
