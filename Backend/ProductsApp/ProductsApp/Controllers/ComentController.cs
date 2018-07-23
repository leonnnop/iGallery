using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json;
using ProductsApp.Models;
using ProductsApp;
using System.Web;

namespace ProductsApp.Controllers
{
    public class ComentController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LdCmt(string Mid)
        {

            //打开数据库连接
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17";
            OracleConnection conn = new OracleConnection(connStr);
            conn.Open();


            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = "select USER_ID,COMMENT_ID from PUBLISH_COMMENT where MOMENT_ID='" + Mid + "'";
            cmd.Connection = conn;

            List<string> users = new List<string>();
            List<string> comments = new List<string>();


            //读出动态的所有评论的id和发评的用户id
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                users.Add(rd[0].ToString());
                comments.Add(rd[1].ToString());
            }

            //利用id查 用户表 【头像】 和 评论体 【内容+时间+引用】

            List<Comment> cmts = new List<Comment>();


            //逐条评论构造Comment
            for (int j = 0; j < users.Count; j++)
            {
                //根据id 查的 评论row
                cmd.CommandText = "select * from COMENT where ID='" + comments.ElementAt(j) + "'";
                rd = cmd.ExecuteReader();

                Comment cmt = new Comment();
                //评论体
                rd.Read();
                cmt.Mid = Mid;
                cmt.Sender_id = users.ElementAt(j);
                cmt.Cid = rd[0].ToString();
                cmt.Content = rd[1].ToString();
                cmt.Send_time = rd[2].ToString();
                cmt.Quote_id = rd[3].ToString(); //""-->null-->""?
                cmt.Type = rd[4].ToString();

                if (cmt.Type.Equals("1") || cmt.Type.Equals("2"))
                {
                    if (cmt.Type.Equals("1"))
                    {
                        //原评论信息
                        cmd.CommandText = "select USER_ID from PUBLISH_COMMENT where COMMENT_ID='" + cmt.Quote_id + "'"; //PUBLIC
                        rd = cmd.ExecuteReader();
                        string uid = "";
                        while (rd.Read())
                        {
                            uid = rd[0].ToString();
                        }
                        //@用户名
                        cmd.CommandText = "select USERNAME from USERS where ID='" + uid + "'"; //PUBLIC
                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            cmt.Quote_username = rd[0].ToString();
                        }
                    }
                    else
                    {
                        //评论被删的原平用户

                        //@用户名
                        cmd.CommandText = "select USERNAME from USERS where ID='" + cmt.Quote_id + "'"; //PUBLIC
                        rd = cmd.ExecuteReader();
                        rd.Read();
                        cmt.Quote_username = rd[0].ToString();
                    }

                    //原评论的内容
                    if (cmt.Type.Equals("1"))
                    {
                        cmd.CommandText = "select CONTENT from COMENT where ID='" + cmt.Quote_id + "'"; //PUBLIC
                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            cmt.Quote_content = rd[0].ToString();
                        }
                    }
                    else
                    {
                        cmt.Quote_content = "原评论已被删除";
                    }

                }

                //发评论者的头像和昵称
                cmd.CommandText = "select USERNAME from USERS where ID='" + users.ElementAt(j) + "'";
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cmt.Sender_username = rd[0].ToString();
                }
                //插入列表
                cmts.Add(cmt);

            }
            //返回按时间排好序的commenter的评论体
            //cmts.Sort();
            cmts.Sort(new SendTimeAsc());
            return Json<List<Comment>>(cmts);
        }




        [HttpPost]
        public HttpResponseMessage SvCmt()
        {
            bool success = true;
            HttpResponseMessage response = new HttpResponseMessage();

            var coment = HttpContext.Current.Request.Params;
            Comment comment = new Comment();

            //永久保存数据
            comment.Mid = coment["Mid"];
            comment.Sender_id = coment["Sender_id"];
            comment.Content = coment["Content"];
            //comment.Send_time = coment["Send_time"];
            comment.Send_time = DateTime.Now.ToString();
            comment.Quote_id = coment["Quote_id"];


            //打开数据库连接
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17";
            OracleConnection conn = new OracleConnection(connStr);
            conn.Open();



            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;



            //保存评论体
            GeneralAPI g = new GeneralAPI();

            comment.Cid = g.NewIDOf("COMENT");

            if (comment.Quote_id==null || comment.Quote_id.Trim().Equals("")) //搞定评论类型
            {
                comment.Type = "0";
            }
            else
            {
                comment.Type = "1";
            }
            cmd.CommandText = "insert into COMENT(ID,CONTENT,SEND_TIME,QUOTE_ID,TYPE) values('" + comment.Cid + "','" + comment.Content + "',TO_TIMESTAMP('"+comment.Send_time+"', 'yyyy-mm-dd hh24:mi:ss.ff'),'" + comment.Quote_id + "','"+ comment.Type + "')";
            int executeResult = cmd.ExecuteNonQuery();
            if (executeResult != 1)
            {
                success = false;
            }


            //评论动态关系表插入记录
            cmd.CommandText = "insert into PUBLISH_COMMENT(USER_ID,COMMENT_ID,MOMENT_ID) values('" + comment.Sender_id + "','" + comment.Cid + "','" + comment.Mid + "')";
            executeResult = cmd.ExecuteNonQuery();
            if (executeResult != 1)
            {
                success = false;
            }

            /* *
             * 评论数触发器级联修改动态Comment_Num
             * hh +2 DEBUG
             * */

            

            if (success != true)
            {
                response.StatusCode = HttpStatusCode.InternalServerError; //404
            }
            return response;
        }


       

        [HttpDelete]
        public HttpResponseMessage DelCmt()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //打开数据库连接
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17";
            OracleConnection conn = new OracleConnection(connStr);
            conn.Open();

            var coment = HttpContext.Current.Request.Params;
            Comment comment = new Comment();
            comment.Cid = coment["Cid"];

            OracleCommand cmd = new OracleCommand();





            //找到原平作者
            string quote_id="2";
            cmd.CommandText = "select USER_ID from PUBLISH_COMMENT where COMMENT_ID='"+comment.Cid+"'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                quote_id = rd[0].ToString();
            }
            //若被删评论被引了,将其作者ID直接写入QUOTE_ID
            cmd.CommandText = "update COMENT set TYPE=2，QUOTE_ID='" + quote_id + "' where QUOTE_ID='" + comment.Cid + "'";
            int executeResult = cmd.ExecuteNonQuery();

            //FK设了 SET NULL
            cmd.CommandText = "delete from COMENT where ID='" + comment.Cid + "'";
            executeResult = cmd.ExecuteNonQuery();
            
          

            if (executeResult == 1)
            {
                response.StatusCode = HttpStatusCode.OK; //204
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound; //404
            }
          
          
            conn.Close();
            return response;
        }

    }

    class SendTimeAsc : IComparer<Comment>
    {

        public int Compare(Comment x, Comment y)
        {
            return x.Send_time.CompareTo(y.Send_time);
        }

    }
    
}
