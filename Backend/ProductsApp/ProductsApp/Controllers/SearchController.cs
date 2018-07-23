using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class SearchController : ApiController
    {
        /// <summary>
        /// 返回搜索匹配用户
        /// </summary>
        /// <param name="keyword">String</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Search_user(string user_id,string keyword)
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
            cmd.CommandText = "select * from USERS where USERNAME like'%" + keyword + "%'";//查找匹配的字符串
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            List<User_Follow> User_list = new List<User_Follow>();                                     //用户列表
            if(!rd.HasRows)
            {
                return Ok("Not found");
            }
            while (rd.Read())
            {
                User_Follow temp = new User_Follow();
                temp.ID = rd["ID"].ToString();
                temp.Email = rd["EMAIL"].ToString();
                //temp.Password = rd["PASSWORD"].ToString();
                temp.Username = rd["USERNAME"].ToString();
                temp.Bio = rd["BIO"].ToString();
                temp.Photo = rd["PHOTO"].ToString();
                OracleCommand cmd1 = new OracleCommand();
                cmd1.CommandText = "select * from FOLLOW_USER where USER_ID='" + user_id + "'and FOLLOWING_ID='" + temp.ID + "'";
                cmd1.Connection = conn;
                OracleDataReader rd1 = cmd1.ExecuteReader();
                if (!rd1.HasRows) temp.FollowState = "False";
                else temp.FollowState = "True";
                User_list.Add(temp);
            }
            rd.Close();
            conn.Close();
            return Json<List<User_Follow>>(User_list);
        }
        /// <summary>
        /// 搜索有关动态、标签
        /// </summary>
        /// <param name="keyword">string</param>
        /// <returns></returns>
        [HttpGet]
        public Tuple<List<Follow_Tag>, List<Moment>> Search_all(string user_id,string keyword)
        {
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

            //搜索标签
            List <Follow_Tag> tags= new List<Follow_Tag>();
            cmd.CommandText = "select CONTENT from TAG where CONTENT like'%"+keyword+"%' and " +
                "CONTENT in (select TAG from MOMENT_TAG)";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Follow_Tag temp = new Follow_Tag();
                temp.Tag=rd["CONTENT"].ToString();
                temp.UserId = user_id;
                OracleCommand cmd1= new OracleCommand();
                cmd1.CommandText = "select *from Follow_Tag where USER_ID='" + user_id + "'and Tag='" + temp.Tag + "'";
                cmd1.Connection = conn;
                OracleDataReader Rd = cmd1.ExecuteReader();
                if (Rd.HasRows) temp.FollowState = "True";
                else temp.FollowState = "False";
                cmd1.CommandText = "select PICTURE.ID from moment_tag natural join picture where tag ='" + temp.Tag + "'";
                Rd = cmd1.ExecuteReader();
                if (Rd.Read())
                {
                    temp.Pic = Rd["ID"].ToString();
                }

                tags.Add(temp);

            }

            //搜索动态
            //动态含符合条件的标签
            List<Moment> moments = new List<Moment>();
            cmd.CommandText = "select MOMENT_ID from Moment_Tag where Tag like'%"+keyword+"%'";
            cmd.Connection = conn;
            OracleDataReader rd1 = cmd.ExecuteReader();
            while(rd1.Read())
            {
                string id = rd1["MOMENT_ID"].ToString();
                OracleCommand cmd1 = new OracleCommand();
                cmd1.CommandText = "select * from Moment where ID='" + id + "'";
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
                }
            }

            //动态含符合条件的内容
            cmd.CommandText = "select * from Moment where CONTENT like '%" + keyword + "%'";
            rd = cmd.ExecuteReader();
            while (rd.Read())
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
            }

            Tuple<List<Follow_Tag> ,List<Moment>> result = new Tuple<List<Follow_Tag> ,List<Moment>>(null,null);
            if (moments.Count == 0&&tags.Count==0)
            {
                return result;
            }
           else
            {
                result = new Tuple<List<Follow_Tag>, List<Moment>>(tags,moments);
                return result;
            }
        }
    }
}
