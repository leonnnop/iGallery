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
        DBAccess Access = new DBAccess();
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
                "values('" + id.ToString() + "','" + user.Email + "','" + user.Password + "','" + user.Username + "','','')";
                
                int result = cmd.ExecuteNonQuery();
                if (result != 1)//插入出现错误
                {
                    status = 2;
                }
                //创建用户的默认收藏夹
                cmd.CommandText = "insert into COLLECTION(FOUNDER_ID,NAME) " +
                    "values('" + id.ToString() + "','默认收藏夹')";
                result = cmd.ExecuteNonQuery();
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
            cmd.CommandText = "select * from USERS t where email='" + Email + "'";
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
              Random r = new Random();
              string yzm = null;
              Random random = new Random();
              yzm += random.Next(100000, 999999);
              message.IsBodyHtml = true;
              message.BodyEncoding = System.Text.Encoding.UTF8;
              string htmlBodyContent = "<img src=\"cid:img_logo\">";
              htmlBodyContent += "<p>【iGallery】您的邮箱验证码为：" + yzm +"</p>";
              //htmlBodyContent += "<p>您的iGallery账号正在重置密码，若非本人操作请及时登录处理。</p>";
              htmlBodyContent += "<p>注册iGallery，分享精彩视界</p>";
              AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(htmlBodyContent, null, "text/html");
              LinkedResource lrlmage = new LinkedResource(@"C:\Users\mac\Desktop\logo_img.png","image/gif");
              lrlmage.ContentId = "img_logo";
              htmlBody.LinkedResources.Add(lrlmage);
              message.AlternateViews.Add(htmlBody); 
              message.IsBodyHtml = true;
              message.Priority = MailPriority.High;    //发送邮件的优先等级
              SmtpClient sc = new SmtpClient();        //简单邮件传输协议
              sc.Host = "smtp.qq.com";                 //指定发送邮件端口
              sc.Port = 25;
              sc.UseDefaultCredentials = true;
              sc.EnableSsl = false;
              sc.Credentials = new System.Net.NetworkCredential("1871373978@qq.com", "rneyzgzhukkpcfbf");
              sc.Send(message);   //发送邮件
              status=yzm;
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
            cmd.CommandText = "select Password from USERS where email='" + Email + "'";//根据邮箱查找该用户的正确密码
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                string password = rd["Password"].ToString();
                conn.Close();
                if (password == Password)
                {
                    cmd.CommandText = "select ID from USERS where email='" + Email + "'";//根据邮箱查找该用户的正确密码
                    cmd.Connection = conn;
                    conn.Open();
                    rd = cmd.ExecuteReader();
                    if(rd.Read())
                    {
                       string id=rd["ID"].ToString();
                       return LoginResult(id);//如果用户输入的密码正确
                    }
                    else return LoginResult("Error");
                }
                else return LoginResult("Error");
            }
            else return LoginResult("NotFound");//未找到此用户名

        }
        public HttpResponseMessage LoginResult(string result)//返回true
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
           
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
                message.Subject = "欢迎注册iGallery";
                Random r = new Random();
                string yzm = null;
                Random random = new Random();
                yzm += random.Next(100000, 999999);
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                string htmlBodyContent = "<img src=\"cid:img_logo\">";
                htmlBodyContent += "<p>【iGallery】您的邮箱验证码为：" + yzm +"</p>";
                //htmlBodyContent += "<p>您的iGallery账号正在重置密码，若非本人操作请及时登录处理。</p>";
                htmlBodyContent += "<p>注册iGallery，分享精彩视界</p>";
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(htmlBodyContent, null, "text/html");
                LinkedResource lrlmage = new LinkedResource(@"C:\Users\mac\Desktop\logo_img.png","image/gif");
                lrlmage.ContentId = "img_logo";
                htmlBody.LinkedResources.Add(lrlmage);
                message.AlternateViews.Add(htmlBody);
                message.IsBodyHtml = true;
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
            string select = Access.Select("*", "USERS", "EMAIL='" + email + "'");
            OracleDataReader rd = Access.GetDataReader(select);
            {
                if(rd.Read()) //存在
                {
                    response.StatusCode = HttpStatusCode.OK;
                    mess = "true";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    mess = "false";
                }
                
                response.Content = new StringContent(mess, Encoding.Unicode);
                return response;
            }
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
       [HttpPut]
        public HttpResponseMessage ChangePassword(string email,string NewPassword)
        {
            string mess = "true";
            HttpResponseMessage response = Request.CreateResponse();

            string update = Access.Update("USERS", "PASSWORD = '" + NewPassword + "'", "EMAIL='" + email + "'");
            if(Access.ExecuteSql(update))
            {
                response.StatusCode = HttpStatusCode.OK;
                mess = "true";
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                mess = "false";
            }
          
            response.Content = new StringContent(mess, Encoding.Unicode);
            return response;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="email">string</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserInfo(string email)
        {

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();


            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select t.* from USERS t where email='" + email + "'");

            //创建Users对象
            Users user = new Users();

            if (rd.Read())//数据库中有此用户，返回其个人信息
            {

                user.ID = rd["ID"].ToString();
                user.Email = rd["EMAIL"].ToString();
                user.Username = rd["USERNAME"].ToString();
                user.Password = rd["PASSWORD"].ToString();
                user.Bio = rd["BIO"].ToString();
                user.Photo = rd["PHOTO"].ToString();
            }

            return Ok<Users>(user);
        }



        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user">Users</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ModifyUserInfo([FromBody]User_Modify user_Modify)
        {

            int status;

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库操作
            if (dBAccess.ExecuteSql("update USERS set username='" + user_Modify.UserName + "', bio='" + user_Modify.Bio + "' where id ='" + user_Modify.ID + "'"))
            {
                status = 0;//成功更新用户信息
            }
            else
            {
                status = 1;//更新失败
            }

            return Ok<int>(status);
        }




        /// <summary>
        /// (取消)关注用户
        /// </summary>
        /// <param name="followID">Users</param>
        /// <param name="followedID">Users</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Follow(string followID, string followedID)//若用户没有关注，则把关注用户和被关注用户加入关注联系集
                                                                             //若用户已经关注，则把关注用户和被关注用户的联系从联系集中删除
        {
            string state = "0";//状态码0：成功，1：失败
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
            cmd.Connection = conn;
            cmd.CommandText="select * " +
                            "from Follow_User " +
                            "where user_id='" + followID + "'and following_id='"+followedID+"'";
            OracleDataReader rd = cmd.ExecuteReader();
            if (!rd.HasRows)
            {
                cmd.CommandText = "insert into Follow_User(USER_ID,FOLLOWING_ID) values(" + followID + "," + followedID + ")";//插入数据库
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
            else
            {
                cmd.CommandText = "delete from Follow_User " +
                             "where user_id='" + followID + "'and following_id='" + followedID + "'";//把此条联系从数据库删除
                cmd.Connection = conn;
                int result = cmd.ExecuteNonQuery();
                if (result == 1)//删除成功
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
                else           //删除失败
                {
                    state = "1";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
                conn.Close();
                response.Content = new StringContent(state, Encoding.Unicode);//返回状态码
                return response;
            }
        }

        /// <summary>
        /// 返回关注列表
        /// </summary>
        /// <param name="userID">Users</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult FollowList(string userID)       //返回关注列表
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
            cmd.CommandText = "select FOLLOWING_ID from Follow_User where USER_ID='" + userID + "'";//找到该用户所关注的用户的ID
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            List<Users> following_list = new List<Users>();
            if (!rd.HasRows)
            {
                conn.Close();
                return Ok("Not found");
            }
            while (rd.Read())
            {
                string followed_id = rd["FOLLOWING_ID"].ToString();
                OracleCommand cmd1 = new OracleCommand();
                cmd1.CommandText = "select * from Users where ID='" + followed_id + "'";//根据ID查找被关注用户的所有信息
                cmd1.Connection = conn;
                OracleDataReader rd1 = cmd1.ExecuteReader();
                if (rd1.Read())
                {
                    Users temp = new Users();
                    temp.ID = rd1["ID"].ToString();
                    temp.Email = rd1["EMAIL"].ToString();
                    temp.Password = rd1["PASSWORD"].ToString();
                    temp.Username = rd1["USERNAME"].ToString();
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
    }
}
