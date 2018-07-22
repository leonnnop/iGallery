using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class DisplayMomentsController : ApiController
    {
        DBAccess Access = new DBAccess();
        UserMomentAPI api = new UserMomentAPI();
        /// <summary>
        /// 展示用户的朋友圈，包含用户自己的动态和他所关注用户的动态
        /// </summary>
        /// <param name="Email">用户邮箱</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Followings(string Email, int Begin, int End)
        {
            List<DisplayedMoment> moments=new List<DisplayedMoment>();

            //获取用户ID
            string UserID = api.EmailToUserID(Email);

            //连接数据库
            //todo:更换成使用DBAcces的版本
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

            //获取朋友圈中所有Moment
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select moment.id, sender_id, content,  like_num, forward_num, collect_num, comment_num, time, quote_mid " +
                "from users,moment " +
                "where moment.sender_id = users.id and " +
                "(users.id = '" + UserID + "' or " +
                "users.id in (select following_id " +
                "from follow_user " +
                "where follow_user.user_id = '" + UserID + "')) " +
                "order by moment.time desc";
            OracleDataReader rd = cmd.ExecuteReader();

            for(int i = 1; i < Begin; i++)
            {
                if (!rd.Read()) { return Ok(false); }
            }
            int count = Begin - 1;
            while (rd.Read())
            {
                Moment mmt = new Moment();

                mmt.ID = rd[0].ToString();
                mmt.SenderID = rd[1].ToString();
                mmt.Content = rd[2].ToString();
                mmt.LikeNum = Convert.ToInt32(rd[3]);
                mmt.ForwardNum = Convert.ToInt32(rd[4]);
                mmt.CollectNum = Convert.ToInt32(rd[5]);
                mmt.CommentNum = Convert.ToInt32(rd[6]);
                if (rd[7] is DBNull)
                {
                    mmt.Time = null;
                }
                else
                {
                    mmt.Time = rd[7].ToString();
                }
                if (rd[8] is DBNull)
                {
                    mmt.QuoteMID = null;
                }
                else
                {
                    mmt.QuoteMID = rd[8].ToString();
                }

                //通过Moment取得每一条动态的相关信息，并加入list
                moments.Add(All_Info_Of(mmt, Email, 4, cmd));

                count++;
                if (count >= End)
                {
                    conn.Close();
                    return Json(moments);
                }
            }
            conn.Close();
            return Json(moments);
        }
        /// <summary>
        /// 获得一条动态的详情
        /// </summary>
        /// <param name="UserID">发送请求的用户的ID</param>
        /// <param name="MomentID">动态ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Detail(string UserID, string MomentID)
        {
            DisplayedMoment dm = new DisplayedMoment();
            string Email = api.UserIDToEmail(UserID);
            //todo:更换成DBAccess的版本
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
            OracleDataReader rd = Access.GetDataReader(Access.Select("*", "moment", "id='"+MomentID+"'"));
            Moment mmt = new Moment();
            if (rd.Read())
            {
                mmt.ID = rd[0].ToString();
                mmt.SenderID = rd[1].ToString();
                mmt.Content = rd[2].ToString();
                mmt.LikeNum = Convert.ToInt32(rd[3]);
                mmt.ForwardNum = Convert.ToInt32(rd[4]);
                mmt.CollectNum = Convert.ToInt32(rd[5]);
                mmt.CommentNum = Convert.ToInt32(rd[6]);
                if (rd[7] is DBNull)
                {
                    mmt.Time = null;
                }
                else
                {
                    mmt.Time = rd[7].ToString();
                }
                if (rd[8] is DBNull)
                {
                    mmt.QuoteMID = null;
                }
                else
                {
                    mmt.QuoteMID = rd[8].ToString();
                }
            }

            dm = All_Info_Of(mmt, Email, 1000, cmd);
            dm.follow_state = api.CheckFollowState(UserID, mmt.SenderID);

            return Json(dm);
        }
        
        
        /// <summary>
        /// 获得一条动态的相关信息
        /// </summary>
        /// <param name="mmt">Moment类型的动态</param>
        /// <param name="email">发送请求的用户的邮箱</param>
        /// <param name="cmd">数据库命令</param>
        /// <returns>用于展示的DisplayedMoment类型</returns>
        private DisplayedMoment All_Info_Of(Moment mmt, string email, int comment_limit, OracleCommand cmd)
        {
            DisplayedMoment dm = new DisplayedMoment();

            //获取动态内容
            dm.moment = mmt;

            //获取发送动态的用户信息
            Users user= GetUserInfo(mmt.SenderID, cmd);
            dm.user_email = user.Email;
            dm.user_username = user.Username;
            dm.user_bio = user.Bio;

            //获取原始动态的用户信息
            OracleDataReader rd = Access.GetDataReader(Access.Select("sender_ID", "moment", "ID='"+mmt.QuoteMID+"'"));
            if (rd.Read())
            {
                Users forwarded = GetUserInfo(rd[0].ToString(), cmd);
                dm.forwarded_email = forwarded.Email;
                dm.forwarded_username = forwarded.Username;
            }
            else
            {
                dm.forwarded_email = null;
                dm.forwarded_username = null;
            }

            //获取标签信息
            dm.tags = new List<string>();
            cmd.CommandText = "select tag from moment_tag where moment_id = '"+mmt.ID+"'";
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                dm.tags.Add(rd[0].ToString());
            }

            //获取评论信息
            if (comment_limit == 4)
            {
                GetComment(mmt.ID, comment_limit, false, cmd, dm);
            }
            if (comment_limit > 4)
            {
                GetComment(mmt.ID, comment_limit, true, cmd, dm);
            }
            

            //获知是否点赞过
            dm.liked = api.CheckLikeState(email, mmt.ID);
           
            //获知是否收藏过
            dm.collected = api.CheckCollectState(email, mmt.ID);
           
            return dm;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户的ID</param>
        /// <param name="cmd">数据库命令</param>
        /// <returns>User类型</returns>
        private Users GetUserInfo(string id, OracleCommand cmd)
        {
            Users user = new Users();
            cmd.CommandText = "select * from users where users.id = '" + id + "'";
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                user.ID = null;
                user.Email = rd["EMAIL"].ToString();
                user.Password = null;
                user.Username = rd["USERNAME"].ToString();
                if (rd["BIO"] is DBNull)
                {
                    user.Bio = null;
                }
                else
                {
                    user.Bio = rd["BIO"].ToString();
                }
               
            }
            return user;
        }

        /// <summary>
        /// 获取一条动态的评论
        /// </summary>
        /// <param name="id">动态的ID</param>
        /// <param name="limit">评论数的限制（若是所有评论，则赋一个很大的值）</param>
        /// <param name="cmd">数据库命令</param>
        /// <returns></returns>
        private List<DisplayedComment> GetComment(string id, int limit, bool showQuote, OracleCommand cmd, DisplayedMoment dm)
        {
            dm.more_comments = false;
            List<DisplayedComment> comments = new List<DisplayedComment>();
            cmd.CommandText = "select * from coment, users, publish_comment " +
                "where coment.ID = publish_comment.comment_ID and " +
                "publish_comment.user_ID = users.ID and " +
                "publish_comment.moment_ID = '" + id + "' " +
                "order by send_time asc";
            OracleDataReader rd = cmd.ExecuteReader();
            int count = 0;
            while (rd.Read() && ++count <= limit)
            {
                DisplayedComment dc = new DisplayedComment();

                //发送评论的用户，提供用户邮箱，用户名
                dc.sender_email = rd["EMAIL"].ToString();
                dc.sender_username = rd["USERNAME"].ToString();

                //评论内容
                dc.id = rd["ID"].ToString();
                dc.content = rd["CONTENT"].ToString();
                dc.send_time = rd["SEND_TIME"].ToString();
                if (showQuote==false)
                {
                    dc.quote = null;
                }
                else
                {
                    if (rd["QUOTE_ID"] is DBNull)//没有引用评论
                    {
                        dc.quote = null;
                    }
                    else//有引用评论，需要获取所引用评论的信息
                    {
                        //获取被引用的评论
                        cmd.CommandText = "select * from coment,users,publish_comment where coment.ID = '" + rd["quote_ID"].ToString() + "'";
                        OracleDataReader rd_cm = cmd.ExecuteReader();
                        if (rd_cm.Read())
                        {
                            dc.quote = new DisplayedComment();
                            //被引用的用户
                            dc.quote.sender_email = rd_cm["EMAIL"].ToString();
                            dc.quote.sender_username = rd_cm["USERNAME"].ToString();

                            //被引用的评论内容
                            dc.quote.id = rd_cm["coment.ID"].ToString();
                            dc.quote.content = rd_cm["coment.content"].ToString();
                            dc.quote.send_time = rd_cm["coment.send_time"].ToString();

                            //被引用评论的引用（不展示多级评论）
                            dc.quote.quote = null;
                        }
                    }
                }
                comments.Add(dc);
            }
            if (rd.Read()) { dm.more_comments = true; } 
            dm.comments = comments;
            return comments;
        }
        

    }
}
