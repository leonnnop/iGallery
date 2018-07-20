using ProductsApp;
using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class DisplayLikeListController : ApiController

    {

        /// <summary>
        /// 获取一条动态的点赞用户列表
        /// </summary>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetLikeList(string moment_id)
        {
            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库select操作
            OracleDataReader rd = dBAccess.GetDataReader("select u.* from FAVORITE f, USERS u where f.moment_id = '" + moment_id + "' and f.user_id = u.id");
            
            //创建Users对象List，并向其中添加读出的数据库信息
            List<Users> resultList = new List<Users>();

            while ( rd.Read() )//当数据库能读出一条符合条件的元组，执行循环
            {
                Users u = new Users();
                u.ID = rd["ID"].ToString();
                u.Username = rd["USERNAME"].ToString();
                u.Password = "null";
                u.Photo = rd["PHOTO"].ToString();
                u.Email = rd["EMAIL"].ToString();
                u.Bio = rd["BIO"].ToString();
                resultList.Add(u);

            }

            return Json<List<Users>>(resultList);//返回符合条件的Users列表

        }
    }


    /// <summary>
    /// 获取用户三天内动态点赞情况
    /// </summary>
    /// <param name="user_id">string</param>
    /// <returns></returns>
    public class Like_MessageController : ApiController
    {
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
            cmd.CommandText = "select * from MOMENT where SENDER_ID ='" + user_id + "' order by TIME desc";//按时间顺序查找动态
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            DateTime CutTime = DateTime.Now.AddDays(-3);//三天前的时间点
            List<Moment> moments = new List<Moment>();  //动态列表
            List<Users> users = new List<Users>();      //点赞用户列表
            Tuple<List<Moment>, List<Users>> result = new Tuple<List<Moment>, List<Users>>(null, null);
            if (!rd.Read()) return result;              //如果无动态，返回空
            while (rd.Read())
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
                    cmd1.CommandText = "select USER_ID from FAVORITE where MOMENT_ID ='" + id + "'";//找到点赞用户ID
                    cmd1.Connection = conn;
                    OracleDataReader rd1 = cmd1.ExecuteReader();
                    if (!rd1.Read()) users = null;    //若无点赞用户
                    while (rd1.Read())
                    {
                        Users temp = new Users();
                        temp.ID = rd1["ID"].ToString();
                        temp.Email = rd1["EMAIL"].ToString();
                        temp.Password = rd1["PASSWORD"].ToString();
                        temp.Username = rd1["USERNAME"].ToString();
                        temp.Bio = rd1["BIO"].ToString();
                        temp.Photo = rd1["PHOTO"].ToString();
                        users.Add(temp);

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
