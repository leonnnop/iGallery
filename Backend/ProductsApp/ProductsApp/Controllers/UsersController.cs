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
    public class UsersController : ApiController
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">Users</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Register([FromBody]Users user)
        {
            //创建返回信息，先假设注册成功
            int status = 0;

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
            cmd.CommandText = "select * from USERS t where email='" + user.Email + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)//邮箱已注册
            {
                status = 1;
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
                    "values('" + id.ToString() + "','" + user.Email + "','" + user.Password + "','" + user.Username + "','null','null')";

                int result = cmd.ExecuteNonQuery();
                if (result != 1)//插入出现错误
                {
                    status = 2;
                }
            }

            //关闭数据库连接
            conn.Close();

            //返回信息
            return Ok(status);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="Login_user">Users</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage TestAccount([FromBody]Users Login_user)//测试输入邮箱和密码是否正确
        {
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
            cmd.CommandText = "select Password from USERS t where email='" + Login_user.Email + "'";//根据邮箱查找该用户的正确密码
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                string password = rd["Password"].ToString();
                conn.Close();
                if (password == Login_user.Password) return Right();//如果用户输入的密码正确
                else return Wrong();
            }
            else return No_user();//未找到此用户名

        }
        public HttpResponseMessage Right()//返回true
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string result = "Success!";
            response.Content = new StringContent(result);
            return response;
        }
        public HttpResponseMessage Wrong()//返回true
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            string result = "Wrong email or password!";
            response.Content = new StringContent(result);
            return response;
        }
        public HttpResponseMessage No_user()//返回false
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            string result = "User not found!";
            response.Content = new StringContent(result);
            return response;
        }

    }
}
