using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class MomentController : ApiController
    {
        //Moment moment = new Moment { ID = "001", SenderID = "123", Content = "Rainbow!", LikeNum = 23, ForwardNum = 9, CollectNum = 6, CommentNum = 15 };
        //Picture pic_01 = new Picture { ID = "001", URL = "https://10wallpaper.com/wallpaper/1366x768/1206/up_go_the_lights-Macro_photography_wallpaper_1366x768.jpg", MomentID = "001" };
        //Picture pic_02 = new Picture { ID = "002", URL = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1531927879926&di=6013413ba72bfff5b42e001515ab17e0&imgtype=0&src=http%3A%2F%2Fimg4.duitang.com%2Fuploads%2Fblog%2F201404%2F06%2F20140406232455_m5XVy.jpeg", MomentID = "001" };

        /*public Moment GetMoment()
        {
            return moment;
        }*/

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
            int id = 1;
            cmd.CommandText = "select count(*) from moment";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            rd.Read();
            id += rd.GetInt32(0);
            string currentTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            cmd.CommandText = "insert into MOMENT(ID,SENDER_ID,CONTENT,LIKE_NUM,FORWARD_NUM,COLLECT_NUM,COMMENT_NUM,TIME) " +
                    "values('" + id.ToString() + "','" + moment.SenderID + "','" + moment.Content + "','" + moment.LikeNum + "','" + moment.ForwardNum + "','" + moment.CollectNum + "','" + moment.CommentNum + "',TO_TIMESTAMP ('" + currentTime + "','yyyy-mm-dd hh24:mi:ss.ff'))";
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


        //搜索与关键词有关动态
        [HttpGet]
        public IHttpActionResult Search(string keyword)
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
            cmd.CommandText = "select MOMENT_ID from Moment_tag where tag like '%" + keyword + "%'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            List<Moment> moment_list = new List<Moment>();
            while (rd.Read())
            {
                string moment_id = rd["MOMENT_ID"].ToString();
                cmd.CommandText = "select * from Moment where ID='" + moment_id + "'";
                OracleDataReader rd1 = cmd.ExecuteReader();
                if (rd1.Read())
                {
                    string ID = rd1["ID"].ToString();
                    string Sender_id = rd1["SENGER_ID"].ToString();
                    string Content = rd1["CONTENE"].ToString();
                    int Like_num = Convert.ToInt32(rd1["LIKE_NUM"]);
                    int Forward_num = Convert.ToInt32(rd1["FORWARD_NUM"]);
                    int Collect_num = Convert.ToInt32(rd1["COLLECT_NUM"]);
                    int Comment_num = Convert.ToInt32(rd1["COMMENT_NUM"]);
                    string Time = rd1["SEND_TIME"].ToString();
                    Moment temp = new Moment(ID, Sender_id, Content, Like_num, Forward_num, Collect_num, Comment_num, Time.ToString());
                    moment_list.Add(temp);
                }
                rd1.Close();
            }
            conn.Close();
            return Json<List<Moment>>(moment_list);
        }

    }
}
