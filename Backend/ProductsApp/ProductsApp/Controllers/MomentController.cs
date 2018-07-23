using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;
using ProductsApp;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class MomentController : ApiController
    {
        DBAccess access = new DBAccess();
        GeneralAPI api = new GeneralAPI();
        //Moment moment = new Moment { ID = "001", SenderID = "123", Content = "Rainbow!", LikeNum = 23, ForwardNum = 9, CollectNum = 6, CommentNum = 15 };
        //Picture pic_01 = new Picture { ID = "001", URL = "https://10wallpaper.com/wallpaper/1366x768/1206/up_go_the_lights-Macro_photography_wallpaper_1366x768.jpg", MomentID = "001" };
        //Picture pic_02 = new Picture { ID = "002", URL = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1531927879926&di=6013413ba72bfff5b42e001515ab17e0&imgtype=0&src=http%3A%2F%2Fimg4.duitang.com%2Fuploads%2Fblog%2F201404%2F06%2F20140406232455_m5XVy.jpeg", MomentID = "001" };

        /*public Moment GetMoment()
        {
            return moment;
        }*/

        [HttpGet]
        public IHttpActionResult NextMomentID()
        {
            return Ok(api.NewIDOf("users"));
        }

        [HttpPost]
        public IHttpActionResult InsertMoment([FromBody] Moment moment)
        {
            //创建返回信息，先假设插入成功
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
            
            string currentTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            cmd.CommandText = "insert into MOMENT(ID,SENDER_ID,CONTENT,LIKE_NUM,FORWARD_NUM,COLLECT_NUM,COMMENT_NUM,TIME) " +
                    "values('" + moment.ID + "','" + moment.SenderID + "','" + moment.Content + "','" + moment.LikeNum + "','" + moment.ForwardNum + "','" + moment.CollectNum + "','" + moment.CommentNum + "',TO_TIMESTAMP ('" + currentTime + "','yyyy-mm-dd hh24:mi:ss.ff'))";
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

        [HttpPost]
        public HttpResponseMessage SendMoment([FromBody]Moment moment)   //将动态以Json格式发送给前端
        {
            string result = JsonConvert.SerializeObject(moment).ToString();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(result);
            return response;
        }

         /// <summary>
        /// 转发动态
        /// </summary>
        /// <param name="forward"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ForwardMoment([FromBody] Forward forward)
        {
            //创建返回信息，先假设转发成功
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
            cmd.CommandText = "select * from moment " +
                               "where sender_id='" + forward.USER_ID + "' and id='" + forward.MOMENT_ID + "'";
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                status = 1;
                return Ok(status);
            }
            cmd.CommandText = "insert into FORWARD(USER_ID,MOMENT_ID) " +
                    "values('" + forward.USER_ID + "','" + forward.MOMENT_ID + "')";

            int result = cmd.ExecuteNonQuery();
            if (result != 1)//插入出现错误
            {
                status = 2;
                return Ok(status);
            }

            cmd.CommandText = "select * from moment" +
                              "where id='" + forward.MOMENT_ID + "'";
            if (!rd.HasRows)
            {
                status = 2;
                return Ok(status);
            }
            Moment moment = new Moment();
            GeneralAPI api = new GeneralAPI();
            moment.ID = api.NewIDOf("moment");
            moment.SenderID = forward.USER_ID;
            moment.Content = rd["CONTENT"].ToString();
            moment.LikeNum = 0;
            moment.ForwardNum = 0;
            moment.CollectNum = 0;
            moment.CommentNum= 0;
            string currentTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            moment.Time = currentTime;
            moment.QuoteMID = forward.MOMENT_ID;
            
            cmd.CommandText = "insert into MOMENT(ID,SENDER_ID,CONTENT,LIKE_NUM,FORWARD_NUM,COLLECT_NUM,COMMENT_NUM,TIME,QUOTEMID) " +
                              "values('" + moment.ID + "','" + moment.SenderID + "','" + moment.Content + "','" + moment.LikeNum + "'," +
                                      "'" + moment.ForwardNum + "','" + moment.CollectNum + "','" + moment.CommentNum + "','" + moment.Time + "','" + moment.QuoteMID + "')";
            int result1 = cmd.ExecuteNonQuery();
            if (result1 != 1)//插入出现错误
            {
                status = 2;
                return Ok(status);
            }
            //关闭数据库连接
            conn.Close();

            //返回信息
            return Ok(status);
        }

        /// <summary>
        /// 转发动态的消息
        /// </summary>
        /// <param name="user_id">String</param>
        /// <returns></returns>
        [HttpGet]
        public Tuple<List<Moment>, List<Users>> ForawardList(string user_id)
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
            cmd.CommandText = "select * from MOMENT where SENDER_ID ='" + user_id + "' order by TIME desc";//查找用户三天内的动态，按时间排序
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            DateTime CutTime = DateTime.Now.AddDays(-3);
            List<Moment> moments = new List<Moment>();
            List<Users> users = new List<Users>();
            Tuple<List<Moment>, List<Users>> result = new Tuple<List<Moment>, List<Users>>(null, null);
            if (!rd.Read()) return result;
            while (rd.Read())
            {
                string time = rd["TIME"].ToString();
                DateTime send = Convert.ToDateTime(time);
                if (send > CutTime)//若是三天内的动态
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
                    cmd1.CommandText = "select USER_ID from Forward where MOMENT_ID ='" + id + "'";//找出转发者
                    cmd1.Connection = conn;
                    OracleDataReader rd1 = cmd1.ExecuteReader();
                    if (!rd1.Read()) users = null;
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
