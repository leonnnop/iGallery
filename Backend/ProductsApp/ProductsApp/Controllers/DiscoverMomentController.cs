using ProductsApp;
using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class DiscoverMomentController : ApiController
    {
        
        /// <summary>
        /// “发现”界面点赞更新
        /// </summary>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateLiking ( string user_id, string moment_id )
        {
            int status = 0;

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库操作
            
            OracleDataReader rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + moment_id + "'");
            if (rd.Read())
            {
                int new_like_num = int.Parse(rd["LIKE_NUM"].ToString()) + 1; 
                /*cmd.CommandText = "update MOMENT set like_num= ' " + new_like_num + " 'where  ID='" + moment_id + "' ";
                int executeResult = cmd.ExecuteNonQuery();*/

                if (dBAccess.ExecuteSql("update MOMENT set like_num= ' " + new_like_num + " 'where  ID='" + moment_id + "' "))
                {
                    if (dBAccess.ExecuteSql("insert into FAVORITE(USER_ID,MOMENT_ID) values('" + user_id + "','" + moment_id + "')"))
                    {
                        status = 0;//moment表和favorite表都修改成功，返回成功状态码0
                    }
                    else
                    {
                        status = 1;//moment表修改成功，favorite表修改失败
                    }

                }
                else//moment表修改失败，未修改favorite表
                {
                    status = 2;
                }
            }
            else//找不到指定动态
            {
                status = 3;
            }

            //返回状态码
            return Ok(status);
        }



        /// <summary>
        /// 获取“发现”界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRankingMoments()
        {

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库select操作
            OracleDataReader rd = dBAccess.GetDataReader("select* from (select m.content, m.like_num, m.forward_num, m.collect_num, m.comment_num, m.time, u.username, u.email, u.bio, u.photo, ROWNUM rn from MOMENT m,USERS u where m.sender_id = u.ID order by m.like_num desc) where rn<=20");

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
            
            //以json格式返回数组
            return Json<List<User_Moment>> (resultList);

        }
    }
}
