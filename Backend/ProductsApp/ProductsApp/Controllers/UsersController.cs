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
using System.Net.Mail;
using Newtonsoft.Json;

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

            
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            
               ///为用户生成一个ID
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
                    status = 1;
                }
               
            

            //关闭数据库连接
            conn.Close();

            //返回信息
            return Ok(status);
        }

        /// <summary>
        /// 注册邮箱验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult VerifyRegister(string Email)
        {
            //创建返回信息，先假设邮箱被注册过
            string status = "1";

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
                status = "1";
            }
            else//邮箱未注册
            {
                MailMessage message = new MailMessage();    //创建一个邮件信息的对象
                message.From = new MailAddress("1871373978@qq.com");
                message.To.Add(Email);
                message.Subject = "欢迎注册iGallery";
                string yzm = null;
                Random rde = new Random();
                yzm += rde.Next(100000, 999999);
                message.Body = "您的验证码为 " + yzm;
                message.IsBodyHtml = false;              //是否为html格式
                message.Priority = MailPriority.High;    //发送邮件的优先等级
                SmtpClient sc = new SmtpClient();        //简单邮件传输协议
                sc.Host = "smtp.qq.com";                 //指定发送邮件端口
                sc.Port = 25;
                sc.UseDefaultCredentials = true;
                sc.EnableSsl = false;
                sc.Credentials = new System.Net.NetworkCredential("1871373978@qq.com", "rneyzgzhukkpcfbf");
                sc.Send(message);   //发送邮件
                response.StatusCode = HttpStatusCode.OK;
                status = yzm;
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
        [HttpGet]
        public HttpResponseMessage Login(string Email, string Password)//测试输入邮箱和密码是否正确
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
            cmd.CommandText = "select Password from USERS t where email='" + Email + "'";//根据邮箱查找该用户的正确密码
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                string password = rd["Password"].ToString();
                conn.Close();
                if (password == Password) return Right();//如果用户输入的密码正确
                else return Wrong();
            }
            else return No_user();//未找到此用户名

        }
        public HttpResponseMessage Right()//返回true
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string result = "0";
            response.Content = new StringContent(result);
            return response;
        }
        public HttpResponseMessage Wrong()//返回true
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            string result = "1";
            response.Content = new StringContent(result);
            return response;
        }
        public HttpResponseMessage No_user()//返回false
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            string result = "2";
            response.Content = new StringContent(result);
            return response;
        }

        //邮箱验证
        [HttpGet]
        public HttpResponseMessage VerifyMail(String Email)
        {
            string mess = "false";
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
            //SQL操作
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select * from USERS where EMAIL='" + Email + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read()) //存在
            {
                MailMessage message = new MailMessage();    //创建一个邮件信息的对象
                message.From = new MailAddress("1871373978@qq.com");
                message.To.Add(Email);
                message.Subject = "iGallery重置密码";
                string yzm = null;
                Random rde = new Random();
                yzm += rde.Next(100000, 999999);
                message.Body = "您的验证码为 " + yzm;
                message.IsBodyHtml = false;              //是否为html格式
                message.Priority = MailPriority.High;    //发送邮件的优先等级
                SmtpClient sc = new SmtpClient();        //简单邮件传输协议
                sc.Host = "smtp.qq.com";                 //指定发送邮件端口
                sc.Port = 25;
                sc.UseDefaultCredentials = true;
                sc.EnableSsl = false;
                sc.Credentials = new System.Net.NetworkCredential("1871373978@qq.com", "rneyzgzhukkpcfbf");
                sc.Send(message);   //发送邮件
                response.StatusCode = HttpStatusCode.OK;
                mess = yzm;
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound;
                mess = "false";
            }
            conn.Close();
            response.Content = new StringContent(mess);
            return response;
           
        }


        /// <summary>
        /// 查找用户邮箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage SearchEmail(string email)
        {
            //返回信息
            string mess = "false";
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
            //SQL操作
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select * from USERS where EMAIL='" + email + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read()) //存在
            {
                response.StatusCode = HttpStatusCode.OK;
                mess = "true";
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound;
                mess = "false";
            }
            conn.Close();
            response.Content = new StringContent(mess);
            return response;
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage ChangePassword(string email, string NewPassword)
        {
            string mess = "true";
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
            //SQL操作
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "update USERS set PASSWORD ='" + NewPassword + "' where EMAIL='" + email + "'";
            cmd.Connection = conn;
            if (cmd.ExecuteNonQuery() != 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                mess = "true";
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                mess = "false";
            }
            conn.Close();
            response.Content = new StringContent(mess, Encoding.Unicode);
            return response;
        }



        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="email">string</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUserInfo(string email)
        {
            string result = null;
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

            //执行数据库操作
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select t.* from USERS t where email='" + email + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read())//数据库中有此用户，返回其个人信息
            {
                Users user = new Users();
                user.ID = rd["ID"].ToString();
                user.Email = rd["EMAIL"].ToString();
                user.Username = rd["USERNAME"].ToString();
                user.Password = rd["PASSWORD"].ToString();
                user.Bio = rd["BIO"].ToString();
                user.Photo = rd["PHOTO"].ToString();
                result = JsonConvert.SerializeObject(user);
                response.StatusCode = HttpStatusCode.OK;
            }
            else//查无此人，返回错误状态码404
            {
                result = "NotFound";
                response.StatusCode = HttpStatusCode.NotFound;
            }
            response.Content = new StringContent(result, Encoding.Unicode);
            rd.Close();
            conn.Close();
            return response;
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user">Users</param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage ModifyUserInfo([FromBody]Users user)
        {
            //将更新信息存入新建object
            Users newUser = new Users();
            newUser.Email = user.Email;
            newUser.Username = user.Username;
            newUser.Password = user.Password;
            newUser.Bio = user.Bio;
            newUser.Photo = user.Password;

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

            //执行数据库操作
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "update USERS set email='" + user.Email + "', username='" + user.Username + "', password='" + user.Password + "' where email='" + user.Email + "'";
            cmd.Connection = conn;

            int executeResult = cmd.ExecuteNonQuery();
            if (executeResult == 1)//修改成功，返回成功状态码202
            {
                response.StatusCode = HttpStatusCode.NoContent;
            }
            else//修改失败，返回失败状态码404
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }

            conn.Close();
            return response;
        }

        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="follow">Users</param>
        /// <param name="followed">Users</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Follow([FromBody]Users follow, [FromBody]Users followed)//把关注用户和被关注用户加入关注联系集
        {
            string state = "0";//状态码0：成功，1：关注失败
            HttpResponseMessage response = Request.CreateResponse();
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
            cmd.CommandText = "insert into Follow_User(USER_ID,FOLLOWING_ID) values(" + follow.ID + "," + followed.ID + ")";//插入数据库
            cmd.Connection = conn;
            int result = cmd.ExecuteNonQuery();
            if (result == 1)//插入成功
            {
                response.StatusCode = HttpStatusCode.OK;
            }
            else           //插入失败
            {
                state = "1";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            conn.Close();
            response.Content = new StringContent(state, Encoding.Unicode);//返回状态码
            return response;
        }

        /// <summary>
        /// 返回关注列表
        /// </summary>
        /// <param name="user">Users</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult FollowList([FromBody]Users user)       //返回关注列表
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
            cmd.CommandText = "select FOLLOWING_ID from Follow_User where USER_ID='" + user.ID + "'";//找到该用户所关注的用户的ID
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            List<Users> following_list = new List<Users>();
            while (rd.Read())
            {
                string followed_id = rd["FOLLOWING_ID"].ToString();
                cmd.CommandText = "select * from User where ID='" + followed_id + "'";//根据ID查找被关注用户的所有信息
                cmd.Connection = conn;
                OracleDataReader rd1 = cmd.ExecuteReader();
                if (rd1.Read())
                {
                    Users temp = new Users();
                    temp.ID = rd1["ID"].ToString();
                    temp.Email = rd1["EMAIL"].ToString();
                    temp.Password = rd1["PASSWORD"].ToString();
                    temp.Bio = rd1["BIO"].ToString();
                    temp.Photo = rd1["PHOTO"].ToString();
                    following_list.Add(temp);
                }
                rd1.Close();
            }
            rd.Close();
            conn.Close();
            return Json<List<Users>>(following_list);
        }
        /// <summary>
        /// 返回搜索匹配用户
        /// </summary>
        /// <param name="keyword">String</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult search_user(string keyword)
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
            cmd.CommandText = "select * from USERS where ID like'%" + keyword + "%'";//查找匹配的字符串
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            List<Users> User_list = new List<Users>();                               //用户列表
            while (rd.Read())
            {
                Users temp = new Users();
                temp.ID = rd["ID"].ToString();
                temp.Email = rd["EMAIL"].ToString();
                temp.Password = rd["PASSWORD"].ToString();
                temp.Bio = rd["BIO"].ToString();
                temp.Photo = rd["PHOTO"].ToString();
                User_list.Add(temp);
            }
            rd.Close();
            conn.Close();
            return Json<List<Users>>(User_list);
        }
    }
}
