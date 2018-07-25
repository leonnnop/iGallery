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
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17;Pooling=true";
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
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17;Pooling=true";
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

        [HttpGet]
        public IHttpActionResult GetUser(string Sender_ID)
        {
            //连接数据库
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17;Pooling=true";
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
            cmd.CommandText = "select receiver_id " +
                              "from message " +
                              "where sender_id =  '" + Sender_ID + "'" +
                              "order by send_time desc";
            OracleDataReader rd = cmd.ExecuteReader();
            List<Users> UsersList = new List<Users>();
            while (rd.Read())
            {
                string user_id = rd["RECEIVER_ID"].ToString();
                cmd.CommandText = "select * " +
                                  "from users " +
                                  "where id =  '" + user_id + "'";
                OracleDataReader rd1 = cmd.ExecuteReader();
                Users user = new Users();
                user.ID = rd1["ID"].ToString();
                user.Email = rd1["EMAIL"].ToString();
                user.Username = rd1["USERNAME"].ToString();
                user.Bio = rd1["BIO"].ToString();
                user.Photo = rd1["PHOTO"].ToString();
                UsersList.Add(user);

            }

            return Json(UsersList);
        }



        //返回评论信息
        [HttpGet]
        public Tuple<List<Moment>, List<Users>, List<Coment>> LikeState(string user_id)
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
            cmd.CommandText = "select * from MOMENT where SENDER_ID ='" + user_id + "' order by TIME desc";//查找该用户的动态
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            DateTime CutTime = DateTime.Now.AddDays(-3);
            List<Moment> moments = new List<Moment>();
            List<Users> users = new List<Users>();
            List<Coment> comments = new List<Coment>();
            Tuple<List<Moment>, List<Users>, List<Coment>> result = new Tuple<List<Moment>, List<Users>, List<Coment>>(null, null, null);
            if (!rd.HasRows) return result;
            while (rd.Read())//返回动态详情
            {
                string time = rd["TIME"].ToString();
                DateTime send = Convert.ToDateTime(time);
                if (send > CutTime)
                {
                    string id = rd["ID"].ToString();
                    string sender_id = rd["SENDER_ID"].ToString();
                    string content = rd["CONTENT"].ToString();
                    int like_num = Convert.ToInt32(rd["LIKE_NUM"]);
                    int forward_num = Convert.ToInt32(rd["FORWARD_NUM"]);
                    int collect_num = Convert.ToInt32(rd["COLLECT_NUM"]);
                    int comment_num = Convert.ToInt32(rd["COMMENT_NUM"]);
                    moments.Add(new Moment(id, sender_id, content, like_num, forward_num, collect_num, comment_num, time));
                    OracleCommand cmd1 = new OracleCommand();
                    cmd1.CommandText = "select USER_ID,COMMENT_ID from Publish_Comment where MOMENT_ID ='" + id + "'";
                    cmd1.Connection = conn;
                    OracleDataReader rd1 = cmd1.ExecuteReader();
                    if (!rd1.HasRows)
                    {
                        users = null;
                        comments = null;
                        return result;
                    }
                    while (rd1.Read())
                    {
                        Users temp = new Users();
                        Coment ctemp = new Coment();
                        temp.ID = rd1["USER_ID"].ToString();
                        ctemp.ID = rd1["COMMENT_ID"].ToString();
                        OracleCommand cmd2 = new OracleCommand();
                        cmd2.CommandText = "select * from Users where ID ='" + temp.ID + "'";//返回用户详情
                        cmd2.Connection = conn;
                        OracleDataReader rd2 = cmd2.ExecuteReader();
                        temp.Email = rd2["EMAIL"].ToString();
                        temp.Password = rd2["PASSWORD"].ToString();
                        temp.Username = rd2["USERNAME"].ToString();
                        temp.Bio = rd2["BIO"].ToString();
                        temp.Photo = rd2["PHOTO"].ToString();
                        users.Add(temp);
                        cmd2.CommandText = "select * from Coment where ID ='" + ctemp.ID + "'order by SEND_TIME desc";//返回评论详情
                        rd2 = cmd2.ExecuteReader();
                        ctemp.Content = rd2["CONTENT"].ToString();
                        ctemp.SendTime = rd2["SEND_TIME"].ToString();
                        ctemp.QuoteID = rd2["QUOTE_ID"].ToString();
                        comments.Add(ctemp);
                        rd2.Close();
                    }
                    rd1.Close();
                }
            }
            result = new Tuple<List<Moment>, List<Users>, List<Coment>>(moments, users, comments);
            rd.Close();
            conn.Close();
            return result;
        }

        //返回点赞信息
        [HttpGet]
        public Tuple<List<Moment>, List<Users>> LikeState(string user_id)
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
            cmd.CommandText = "select * from MOMENT where SENDER_ID ='" + user_id + "' order by TIME desc";//查找该用户的动态
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            DateTime CutTime = DateTime.Now.AddDays(-3); //三天内的动态
            List<Moment> moments = new List<Moment>();
            List<Users> users = new List<Users>();
            Tuple<List<Moment>, List<Users>> result = new Tuple<List<Moment>, List<Users>>(null, null);
            if (!rd.HasRows) return result;
            while (rd.Read())//返回动态详情
            {
                string time = rd["TIME"].ToString();
                DateTime send = Convert.ToDateTime(time);
                if (send > CutTime)
                {
                    string id = rd["ID"].ToString();
                    string sender_id = rd["SENDER_ID"].ToString();
                    string content = rd["CONTENT"].ToString();
                    int like_num = Convert.ToInt32(rd["LIKE_NUM"]);
                    int forward_num = Convert.ToInt32(rd["FORWARD_NUM"]);
                    int collect_num = Convert.ToInt32(rd["COLLECT_NUM"]);
                    int comment_num = Convert.ToInt32(rd["COMMENT_NUM"]);
                    moments.Add(new Moment(id, sender_id, content, like_num, forward_num, collect_num, comment_num, time));
                    OracleCommand cmd1 = new OracleCommand();
                    cmd1.CommandText = "select USER_ID from FAVORITE where MOMENT_ID ='" + id + "'";
                    cmd1.Connection = conn;
                    OracleDataReader rd1 = cmd1.ExecuteReader();
                    if (!rd1.HasRows)
                    {
                        users = null;
                        return result;
                    }
                    while (rd1.Read())//返回用户详情
                    {
                        Users temp = new Users();
                        temp.ID = rd1["USER_ID"].ToString();
                        OracleCommand cmd2 = new OracleCommand();
                        cmd2.CommandText = "select * from Users where ID ='" + temp.ID + "'";
                        OracleDataReader rd2 = cmd2.ExecuteReader();
                        temp.Email = rd2["EMAIL"].ToString();
                        temp.Password = rd2["PASSWORD"].ToString();
                        temp.Username = rd2["USERNAME"].ToString();
                        temp.Bio = rd2["BIO"].ToString();
                        temp.Photo = rd2["PHOTO"].ToString();
                        users.Add(temp);
                        rd2.Close();
                    }
                    rd1.Close();
                }
            }
            result = new Tuple<List<Moment>, List<Users>>(moments, users);
            rd.Close();
            conn.Close();
            return result;
        }

        //返回转发信息
        [HttpGet]
        public Tuple<List<Moment>, List<Users>> ForwardState(string user_id)
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
            cmd.CommandText = "select * from MOMENT where SENDER_ID ='" + user_id + "' order by TIME desc";//查找该用户的动态
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            DateTime CutTime = DateTime.Now.AddDays(-3);
            List<Moment> moments = new List<Moment>();
            List<Users> users = new List<Users>();
            Tuple<List<Moment>, List<Users>> result = new Tuple<List<Moment>, List<Users>>(null, null);
            if (!rd.HasRows) return result;
            while (rd.Read())
            {
                string time = rd["TIME"].ToString();
                DateTime send = Convert.ToDateTime(time);
                if (send > CutTime)//返回动态详情
                {
                    string id = rd["ID"].ToString();
                    string sender_id = rd["SENDER_ID"].ToString();
                    string content = rd["CONTENT"].ToString();
                    int like_num = Convert.ToInt32(rd["LIKE_NUM"]);
                    int forward_num = Convert.ToInt32(rd["FORWARD_NUM"]);
                    int collect_num = Convert.ToInt32(rd["COLLECT_NUM"]);
                    int comment_num = Convert.ToInt32(rd["COMMENT_NUM"]);
                    moments.Add(new Moment(id, sender_id, content, like_num, forward_num, collect_num, comment_num, time));
                    OracleCommand cmd1 = new OracleCommand();
                    cmd1.CommandText = "select USER_ID from Forward where MOMENT_ID ='" + id + "'";
                    cmd1.Connection = conn;
                    OracleDataReader rd1 = cmd1.ExecuteReader();
                    if (!rd1.HasRows)
                    {
                        users = null;
                        return result;
                    }
                    while (rd1.Read())//返回用户详情
                    {
                        Users temp = new Users();
                        temp.ID = rd1["USER_ID"].ToString();
                        OracleCommand cmd2 = new OracleCommand();
                        cmd2.CommandText = "select * from Users where ID ='" + temp.ID + "'";
                        cmd2.Connection = conn;
                        OracleDataReader rd2 = cmd2.ExecuteReader();
                        temp.Email = rd2["EMAIL"].ToString();
                        temp.Password = rd2["PASSWORD"].ToString();
                        temp.Username = rd2["USERNAME"].ToString();
                        temp.Bio = rd2["BIO"].ToString();
                        temp.Photo = rd2["PHOTO"].ToString();
                        users.Add(temp);
                        rd2.Close();
                    }
                    rd1.Close();
                }
            }
            result = new Tuple<List<Moment>, List<Users>>(moments, users);
            rd.Close();
            conn.Close();
            return result;
        }
    }
}
