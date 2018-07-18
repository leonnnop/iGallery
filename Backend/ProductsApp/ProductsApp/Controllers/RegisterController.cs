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

            //检查邮箱是否已被用于注册
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText="select * from USERS t where email='"+user.Email+"'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if(rd.HasRows)//邮箱已注册
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                status = "used email address ";
            }
            else//邮箱未注册
            {
                //为用户生成一个ID
                int id = 1;
                cmd.CommandText = "select count(*) from users";
                rd = cmd.ExecuteReader();
                rd.Read();
                id += rd.GetInt32(0);

                //将新建用户插入数据库
                cmd.CommandText = "insert into USERS(ID,EMAIL,PASSWORD,USERNAME,BIO,PHOTO) " +
                    "values('"+id.ToString()+"','"+user.Email+"','"+user.Password+"','"+user.Username+"','null','null')";

                int result = cmd.ExecuteNonQuery();
                if (result == 1)//插入成功
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
                else//插入失败
                {
                    status = "internal server error";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }

            //关闭数据库连接
            conn.Close();

            //返回信息
           response.Content = new StringContent(status, Encoding.Unicode);
           return response;
        }
    }
}
