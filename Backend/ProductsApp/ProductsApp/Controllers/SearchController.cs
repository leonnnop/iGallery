using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Search.Controllers
{
    public class SearchController : ApiController
    {
        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="keyword">string</param>
        /// <returns></returns>
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
                    Moment temp = new Moment(ID, Sender_id, Content, Like_num, Forward_num, Collect_num, Comment_num, Convert.ToDateTime(Time));
                    moment_list.Add(temp);
                }
                rd1.Close();
            }
            conn.Close();
            return Json<List<Moment>>(moment_list);
        }
        /// <summary>
        /// 搜索有关动态（用户）、标签
        /// </summary>
        /// <param name="keyword">string</param>
        /// <returns></returns>
        [HttpGet]
        public Tuple<List<Tag>,List<Users>, List<Moment>> Search_all(string keyword)
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
            List<Tag> tags = new List<Tag>();
            cmd.CommandText = "select CONTENT from TAG where CONTENT='%"+keyword+"%'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Tag temp = new Tag();
                temp.Content = rd["CONTENT"].ToString();
                tags.Add(temp);
            }
            List<Moment> moments = new List<Moment>();
            List<Users> user_list = new List<Users>();
            cmd.CommandText = "select MOMENT_ID from Moment_Tag where Tag='%"+keyword+"%'";
            cmd.Connection = conn;
            OracleDataReader rd1 = cmd.ExecuteReader();
            while(rd1.Read())
            {
                string id = rd1["MOMENT_ID"].ToString();
                OracleCommand cmd1 = new OracleCommand();
                cmd1.CommandText = "select * from Moment where ID=" + id + "'or CONTENT like '%"+keyword+"%'";
                cmd1.Connection = conn;
                rd = cmd1.ExecuteReader();
                if (rd.Read())
                {
                    string Id = rd["ID"].ToString();
                    string sender_id = rd["SENDER_ID"].ToString();
                    string content = rd["CONTENT"].ToString();
                    int likenum = Convert.ToInt32(rd["LIKE_NUM"]);
                    int forwardnum = Convert.ToInt32(rd["FORWARD_NUM"]);
                    int collectnum = Convert.ToInt32(rd["COLLECT_NUM"]);
                    int commentnum = Convert.ToInt32(rd["COMMENT_NUM"]);
                    string time = rd["TIME"].ToString();
                    moments.Add(new Moment(Id, sender_id, content, likenum, forwardnum, collectnum, commentnum, time));
                    OracleCommand cmd2 = new OracleCommand();
                    cmd2.CommandText = "select * from Users where ID='" + sender_id+"'";
                    cmd2.Connection = conn;
                    OracleDataReader rd3 = cmd.ExecuteReader();
                    while(rd3.Read())
                    {
                        Users temp = new Users();
                        temp.ID = rd3["ID"].ToString();
                        temp.Password = rd3["PASSWORD"].ToString();
                        temp.Email = rd3["EMAIL"].ToString();
                        temp.Username = rd3["USERNAME"].ToString();
                        temp.Bio = rd3["BIO"].ToString();
                        temp.Photo = rd3["PHOTO"].ToString();
                        user_list.Add(temp);
                    }
                }
            }
            Tuple<List<Tag>, List<Users>,List<Moment>> result = new Tuple<List<Tag>, List<Users>, List<Moment>>(null,null,null);
            if (moments.Count == 0&&tags.Count==0&&user_list.Count==0)
            {
                return result;
            }
           else
            {
                result = new Tuple<List<Tag>, List<Users>, List<Moment>>(tags, user_list, moments);
                return result;
            }
        }
    }
}
