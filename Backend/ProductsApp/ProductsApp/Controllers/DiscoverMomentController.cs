using IGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace IGallery.Controllers
{
    public class DiscoverMomentController : ApiController
    {
        
        /// <summary>
        /// “发现”界面点赞更新
        /// </summary>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateLiking ( string moment_id )
        {
            int status = 0;
            //HttpResponseMessage response = Request.CreateResponse();

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
            cmd.CommandText = "select * from MOMENT where ID='" + moment_id + "'";
            //cmd.CommandText = "update MOMENT set email='"+user.Email+"', username='"+user.Username+"', password='"+user.Password+"' where email='" + user.Email + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                int new_like_num = int.Parse(rd["LIKE_NUM"].ToString()) + 2; 
                cmd.CommandText = "update MOMENT set like_num= ' " + new_like_num + " 'where  ID='" + moment_id + "' ";
                int executeResult = cmd.ExecuteNonQuery();
                if (executeResult == 1)//修改成功，返回成功状态码0
                {
                    status = 0;
                }
                else//修改失败，返回修改失败状态码0
                {
                    status = 1;
                }
            }
            else//找不到指定动态，返回失败状态码0
            {
                status = 2;
            }
            rd.Close();
            conn.Close();
            return Ok(status);
        }



        /// <summary>
        /// 获取“发现”界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRankingMoments()
        {
            //HttpResponseMessage response = Request.CreateResponse();

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
            cmd.CommandText = "select* from (select m.content, m.like_num, m.forward_num, m.collect_num, m.comment_num, m.time, u.username, u.email, u.bio, u.photo, ROWNUM rn from MOMENT m,USERS u where m.sender_id = u.ID order by m.like_num desc) where rn<=20";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            //创建User_Moment对象List，并向其中添加读出的数据库信息
            List<User_Moment> resultList = new List<User_Moment>();
            
            while (rd.Read())//当数据库能读出一条符合条件的元组，执行循环
            {
                User_Moment um = new User_Moment();
                um.Username = rd["USERNAME"].ToString();
                um.Email = rd["EMAIL"].ToString();
                um.Bio = rd["BIO"].ToString();
                um.Photo = rd["PHOTO"].ToString();
                um.Content = rd["CONTENT"].ToString();
                um.LikeNum = int.Parse(rd["LIKE_NUM"].ToString());
                um.ForwardNum = int.Parse(rd["FORWARD_NUM"].ToString());
                um.CollectNum = int.Parse(rd["COLLECT_NUM"].ToString());
                um.CommentNum = int.Parse(rd["COMMENT_NUM"].ToString());
                /*DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy/mm/dd";
                um.Time = Convert.ToDateTime(rd["TIME"].ToString(), dtFormat);*/
                //um.Time = Convert.ToDateTime(rd["TIME"].ToString());
                um.Time =  rd["TIME"].ToString();
                resultList.Add(um);
                
            }
            

            rd.Close();
            conn.Close();
            //以json格式返回数组
            return Json<List<User_Moment>> (resultList);

        }
    }
}
