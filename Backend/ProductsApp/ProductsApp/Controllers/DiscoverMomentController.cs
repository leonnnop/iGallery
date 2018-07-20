using IGallery;
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
        /// 检查用户是否点赞一条动态
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        /// 返回int值：“-1”表示没有该用户，“0”表示用户已点赞过，“1”表示未点赞过
        private int CheckLikeState(string email, string moment_id )
        {
            //bool result = false;
            string ID = null;

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库操作,获取该用户ID
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS where email='" + email + "'");
            if (rd.Read())
            {
                ID = rd["ID"].ToString();
            }
            else
            {
                return -1;
            }

            rd = dBAccess.GetDataReader("select * from FAVORITE where moment_id='" + moment_id + "'");

            //List<string> userList = new List<string>();
            while (rd.Read())
            {
                //string user_id = rd["USER_ID"].ToString();
                //userList.Add(user_id);
                if (rd["USER_ID"].ToString() == ID)
                    return 0;
            }

            return 1;
        }


        /// <summary>
        /// 通过邮箱获取用户ID
        /// </summary>
        /// <param name="email">string</param>
        /// <returns></returns>
        
        private string GetUserID(string email)
        {
            string ID = null;

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库操作,获取该用户ID
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS where email='" + email + "'");
            if (rd.Read())
            {
                ID = rd["ID"].ToString();
            }
            else
            {
                return null;
            }
            return ID;
        }



        /// <summary>
        /// “发现”界面点赞更新
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateLiking ( string email, string moment_id )
        {
            int status = 0;
            int likeState = -1;

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //记录该用户是否点赞该动态
            likeState = CheckLikeState(email, moment_id);

            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + moment_id + "'");
            if (rd.Read())
            {
                //设置新的点赞数
                int new_like_num;
                if (likeState == 0)//取消点赞，赞数减一
                {
                    new_like_num = int.Parse(rd["LIKE_NUM"].ToString()) - 1;
                }
                else if (likeState == 1)//点赞，赞数加一
                {
                    new_like_num = int.Parse(rd["LIKE_NUM"].ToString()) + 1;
                }
                else//用户不存在
                {
                    status = 4;
                    return Ok(status);
                }

                //获取用户id
                string user_id;
                if (GetUserID(email) != null)
                {
                    user_id = GetUserID(email);
                }
                else//用户不存在
                {
                    status = 4;
                    return Ok(status);
                }
                 
                //更新数据库中的点赞数
                if (dBAccess.ExecuteSql("update MOMENT set like_num= ' " + new_like_num + " 'where  ID='" + moment_id + "' "))
                {
                    if (likeState == 1)//点赞
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
                    else if (likeState == 0)//取消点赞
                    {
                        if (dBAccess.ExecuteSql("delete from FAVORITE where user_id = '"+user_id+"'"))
                        {
                            status = 0;//moment表和favorite表都修改成功，返回成功状态码0
                        }
                        else
                        {
                            status = 1;//moment表修改成功，favorite表修改失败
                        }
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
        /// <param name="email">string</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRankingMoments( string email )
        {

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库select操作
            OracleDataReader rd = dBAccess.GetDataReader("select* from (select m.ID, m.content, m.like_num, m.forward_num, m.collect_num, m.comment_num, m.time, u.username, u.email, u.bio, u.photo, ROWNUM rn from MOMENT m,USERS u where m.sender_id = u.ID order by m.like_num desc) where rn<=20");

            //创建User_Moment对象List，并向其中添加读出的数据库信息
            List<User_Moment> resultList = new List<User_Moment>();
            
            while (rd.Read())//当数据库能读出一条符合条件的元组，执行循环
            {
                User_Moment um = new User_Moment();
                um.MomentID = rd["ID"].ToString();
                um.Username = rd["USERNAME"].ToString();
                um.Email = rd["EMAIL"].ToString();
                um.Bio = rd["BIO"].ToString();
                um.Photo = rd["PHOTO"].ToString();
                um.Content = rd["CONTENT"].ToString();
                um.LikeNum = int.Parse(rd["LIKE_NUM"].ToString());
                um.ForwardNum = int.Parse(rd["FORWARD_NUM"].ToString());
                um.CollectNum = int.Parse(rd["COLLECT_NUM"].ToString());
                um.CommentNum = int.Parse(rd["COMMENT_NUM"].ToString());
                um.Time =  rd["TIME"].ToString();
                //获取该用户的点赞状态
                int likeState = CheckLikeState(email, um.MomentID);
                if (likeState == 1)//未点赞过
                {
                    um.LikeState = "false";
                }
                else if (likeState == 0)//点赞过
                {
                    um.LikeState = "true";
                }
                else
                {
                    um.LikeState = "error";
                }

                resultList.Add(um);
                
            }
            
            //以json格式返回数组
            return Json<List<User_Moment>> (resultList);

        }
    }
}
