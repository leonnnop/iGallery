using System;
using ProductsApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Register([FromBody]User_register user)
        {

            //创建返回信息
            string status="success";
            HttpResponseMessage response = Request.CreateResponse();

            //todo:连接数据库
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
            cmd.CommandText="select * from USERS t where email='"+user.Email+"'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if(rd.HasRows)//邮箱已注册
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                status = "the email address has been used";
            }
            else
            {
                string id = "336";
                cmd.CommandText = "insert into USERS(ID,EMAIL,PASSWORD,USERNAME,BIO,PHOTO) values('"+id+"','"+user.Email+"','"+user.Password+"','"+user.Username+"','null','null')";

                cmd.Connection = conn;

                int result = cmd.ExecuteNonQuery();
                if (result == 1)//插入成功
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    status = "internal server error";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }
            
            conn.Close();

           response.Content = new StringContent(status, Encoding.Unicode);
           return response;
        }
    }
}
