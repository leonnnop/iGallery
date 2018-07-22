<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using ProductsApp.Models;

/*API
    EmailToUserID   给用户邮箱，返回用户ID
    UserIDToEmail   给用户ID，返回用户邮箱
    CheckLikeState  给用户邮箱和动态ID，返回该用户是否赞过该动态
    CheckFollowState    给用户1、2的ID，返回用户1是否关注用户2
    CheckCollectState   给用户邮箱和动态ID，返回该用户是否收藏该动态
    GetUserInfoByEmail  给用户邮箱，返回Users类型（与数据库中一致）的用户信息
    GetUserInfoByID     给用户ID，返回Users类型（与数据库中一致）的用户信息
    GetMomentInfo   给动态ID，返回Moment类型（与数据库中一致）的动态信息
    GetComentInfo   给评论ID，返回Coment类型（与数据库中一致）的评论信息


*/

namespace ProductsApp
{
    public class GeneralAPI
    {
        DBAccess dBAccess = new DBAccess();

        /// <summary>
        /// 通过邮箱获取用户ID
        /// </summary>
        /// <param name="email">string</param>
        /// <returns></returns>
        public string EmailToUserID(string email)
        {
            string ID = null;

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
        /// 通过用户ID获取邮箱
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string UserIDToEmail(string userid)
        {
            string Email = null;

            //执行数据库操作,获取该用户ID
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS where ID='" + userid + "'");
            if (rd.Read())
            {
                Email = rd["EMAIL"].ToString();
            }
            else
            {
                return null;
            }
            return Email;
        }

        /// <summary>
        /// 检查用户是否点赞一条动态
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        /// 返回int值：“-1”表示没有该用户，“0”表示用户已点赞过，“1”表示未点赞过
        public int CheckLikeState(string email, string moment_id)
        {
            //bool result = false;
            string ID = EmailToUserID(email);

            if (ID == null)
                return -1;//找不到该邮箱代表的用户

            //执行数据库操作,获取该用户ID
            OracleDataReader rd = dBAccess.GetDataReader("select * from FAVORITE where moment_id='" + moment_id + "' and user_id = '" + ID + "'");

            if (rd.Read())
            {
                return 0;//找到该条点赞
            }

            return 1;//找不到该条点赞

        }


        /// <summary>
        /// 检查用户1是否关注用户2
        /// </summary>
        /// <param name="id_1">string</param>
        /// <param name="id_2">string</param>
        /// <returns></returns>
        /// 返回int值：“-1”表示是自己，“0”表示已关注，“1”表示未关注
        public int CheckFollowState(string id_1, string id_2)
        {
            //bool result = false;
            //string ID = null;
            if (id_1 == id_2)
            {
                return -1;
            }

            //执行数据库操作,获取该用户ID
            OracleDataReader rd = dBAccess.GetDataReader("select * from FOLLOW_USER where user_id='" + id_1 + "' and following_id = '" + id_2 + "'");
            if (rd.Read())//表示已关注
            {
                return 0;
            }
            else //表示未关注
            {
                return 1;
            }

        }


        /// <summary>
        /// Checks the state of the collect.
        /// </summary>
        /// <returns>The collect state.</returns>
        /// <param name="email">Email.</param>
        /// <param name="moment_id">Moment identifier.</param>
        /// 返回int值：“-1”表示没有该用户，“0”表示用户已收藏，“1”表示未收藏
        public int CheckCollectState(string email, string moment_id)
        {

            //执行数据库操作,获取该用户ID
            string ID = EmailToUserID(email);
            if (ID==null)//用户不存在
            {
                return -1;
            }

            OracleDataReader rd = dBAccess.GetDataReader("select * from collect where moment_id='" + moment_id + "' " +
                                                         "and founder_id='"+ID+"'");
            
            if (rd.HasRows)
            {
                return 0;//已收藏
            }
            else 
            {
                return 1;//未收藏
            }
        }
        
        /// <summary>
        /// 通过用户邮箱获取用户信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public Users GetUserInfoByEmail(string email)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS  where email='" + email + "'");

            //创建Users对象
            Users user = new Users();

            if (rd.Read())//数据库中有此用户，返回其个人信息
            {

                user.ID = rd["ID"].ToString();
                user.Email = rd["EMAIL"].ToString();
                user.Username = rd["USERNAME"].ToString();
                user.Password = rd["PASSWORD"].ToString();
                user.Bio = rd["BIO"].ToString();
                user.Photo = rd["PHOTO"].ToString();
            }

            return user;
        }

        /// <summary>
        /// 通过用户ID获取用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Users GetUserInfoByID(string ID)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS where ID='" + ID + "'");

            //创建Users对象
            Users user = new Users();

            if (rd.Read())//数据库中有此用户，返回其个人信息
            {

                user.ID = rd["ID"].ToString();
                user.Email = rd["EMAIL"].ToString();
                user.Username = rd["USERNAME"].ToString();
                user.Password = rd["PASSWORD"].ToString();
                user.Bio = rd["BIO"].ToString();
                user.Photo = rd["PHOTO"].ToString();
            }

            return user;
        }

        public Moment GetMomentInfo(string ID)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader(dBAccess.Select("*", "moment", "id='" + ID + "'"));
            //创建Moment对象
            Moment moment = new Moment();
            //读取数据
            if (rd.Read())
            {
                moment.ID = rd[0].ToString();
                moment.SenderID = rd[1].ToString();
                moment.Content = rd[2].ToString();
                moment.LikeNum = Convert.ToInt32(rd[3]);
                moment.ForwardNum = Convert.ToInt32(rd[4]);
                moment.CollectNum = Convert.ToInt32(rd[5]);
                moment.CommentNum = Convert.ToInt32(rd[6]);
                if (rd[7] is DBNull)
                {
                    moment.Time = null;
                }
                else
                {
                    moment.Time = rd[7].ToString();
                }
                if (rd[8] is DBNull)
                {
                    moment.QuoteMID = null;
                }
                else
                {
                    moment.QuoteMID = rd[8].ToString();
                }
            }
            //返回对象
            return moment;
        }

        /// <summary>
        /// 通过评论ID获取评论信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Coment GetComentInfo(string ID)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader(dBAccess.Select("*", "coment", "id='" + ID + "'"));
            //创建Coment对象
            Coment coment = new Coment();
            //读取数据
            if (rd.Read())
            {
                coment.ID = rd[0].ToString();
                coment.Content = rd[1].ToString();
                coment.SendTime = rd[2].ToString();
                if (rd[3] is DBNull)
                {
                    coment.QuoteID = null;
                }
                else
                {
                    coment.QuoteID = rd[3].ToString();
                }
                coment.Type = rd[4].ToString();
            }
            //返回对象
            return coment;

        }


    }

=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using ProductsApp.Models;

/*API
    EmailToUserID   给用户邮箱，返回用户ID
    UserIDToEmail   给用户ID，返回用户邮箱
    CheckLikeState  给用户邮箱和动态ID，返回该用户是否赞过该动态
    CheckFollowState    给用户1、2的ID，返回用户1是否关注用户2
    CheckCollectState   给用户邮箱和动态ID，返回该用户是否收藏该动态
    GetUserInfoByEmail  给用户邮箱，返回Users类型（与数据库中一致）的用户信息
    GetUserInfoByID     给用户ID，返回Users类型（与数据库中一致）的用户信息
    GetMomentInfo   给动态ID，返回Moment类型（与数据库中一致）的动态信息
    GetComentInfo   给评论ID，返回Coment类型（与数据库中一致）的评论信息


*/

namespace ProductsApp
{
    public class GeneralAPI
    {
        DBAccess dBAccess = new DBAccess();

        /// <summary>
        /// 通过邮箱获取用户ID
        /// </summary>
        /// <param name="email">string</param>
        /// <returns></returns>
        public string EmailToUserID(string email)
        {
            string ID = null;

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
        /// 通过用户ID获取邮箱
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string UserIDToEmail(string userid)
        {
            string Email = null;

            //执行数据库操作,获取该用户ID
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS where ID='" + userid + "'");
            if (rd.Read())
            {
                Email = rd["EMAIL"].ToString();
            }
            else
            {
                return null;
            }
            return Email;
        }

        /// <summary>
        /// 检查用户是否点赞一条动态
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        /// 返回int值：“-1”表示没有该用户，“0”表示用户已点赞过，“1”表示未点赞过
        public int CheckLikeState(string email, string moment_id)
        {
            //bool result = false;
            string ID = null;

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
        /// 检查用户1是否关注用户2
        /// </summary>
        /// <param name="id_1">string</param>
        /// <param name="id_2">string</param>
        /// <returns></returns>
        /// 返回int值：“-1”表示是自己，“0”表示已关注，“1”表示未关注
        public int CheckFollowState(string id_1, string id_2)
        {
            //bool result = false;
            //string ID = null;
            if (id_1 == id_2)
            {
                return -1;
            }

            //执行数据库操作,获取该用户ID
            OracleDataReader rd = dBAccess.GetDataReader("select * from FOLLOW_USER where user_id='" + id_1 + "' and following_id = '" + id_2 + "'");
            if (rd.Read())//表示已关注
            {
                return 0;
            }
            else //表示未关注
            {
                return 1;
            }

        }


        /// <summary>
        /// Checks the state of the collect.
        /// </summary>
        /// <returns>The collect state.</returns>
        /// <param name="email">Email.</param>
        /// <param name="moment_id">Moment identifier.</param>
        /// 返回int值：“-1”表示没有该用户，“0”表示用户已收藏，“1”表示未收藏
        public int CheckCollectState(string email, string moment_id)
        {

            //执行数据库操作,获取该用户ID
            string ID = EmailToUserID(email);
            if (ID==null)//用户不存在
            {
                return -1;
            }

            OracleDataReader rd = dBAccess.GetDataReader("select * from collect where moment_id='" + moment_id + "' " +
                                                         "and founder_id='"+ID+"'");
            
            if (rd.HasRows)
            {
                return 0;//已收藏
            }
            else 
            {
                return 1;//未收藏
            }
        }
        
        /// <summary>
        /// 通过用户邮箱获取用户信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public Users GetUserInfoByEmail(string email)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS  where email='" + email + "'");

            //创建Users对象
            Users user = new Users();

            if (rd.Read())//数据库中有此用户，返回其个人信息
            {

                user.ID = rd["ID"].ToString();
                user.Email = rd["EMAIL"].ToString();
                user.Username = rd["USERNAME"].ToString();
                user.Password = rd["PASSWORD"].ToString();
                user.Bio = rd["BIO"].ToString();
                user.Photo = rd["PHOTO"].ToString();
            }

            return user;
        }

        /// <summary>
        /// 通过用户ID获取用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Users GetUserInfoByID(string ID)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select * from USERS where ID='" + ID + "'");

            //创建Users对象
            Users user = new Users();

            if (rd.Read())//数据库中有此用户，返回其个人信息
            {

                user.ID = rd["ID"].ToString();
                user.Email = rd["EMAIL"].ToString();
                user.Username = rd["USERNAME"].ToString();
                user.Password = rd["PASSWORD"].ToString();
                user.Bio = rd["BIO"].ToString();
                user.Photo = rd["PHOTO"].ToString();
            }

            return user;
        }

        public Moment GetMomentInfo(string ID)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader(dBAccess.Select("*", "moment", "id='" + ID + "'"));
            //创建Moment对象
            Moment moment = new Moment();
            //读取数据
            if (rd.Read())
            {
                moment.ID = rd[0].ToString();
                moment.SenderID = rd[1].ToString();
                moment.Content = rd[2].ToString();
                moment.LikeNum = Convert.ToInt32(rd[3]);
                moment.ForwardNum = Convert.ToInt32(rd[4]);
                moment.CollectNum = Convert.ToInt32(rd[5]);
                moment.CommentNum = Convert.ToInt32(rd[6]);
                if (rd[7] is DBNull)
                {
                    moment.Time = null;
                }
                else
                {
                    moment.Time = rd[7].ToString();
                }
                if (rd[8] is DBNull)
                {
                    moment.QuoteMID = null;
                }
                else
                {
                    moment.QuoteMID = rd[8].ToString();
                }
            }
            //返回对象
            return moment;
        }

        /// <summary>
        /// 通过评论ID获取评论信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Coment GetComentInfo(string ID)
        {
            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader(dBAccess.Select("*", "coment", "id='" + ID + "'"));
            //创建Coment对象
            Coment coment = new Coment();
            //读取数据
            if (rd.Read())
            {
                coment.ID = rd[0].ToString();
                coment.Content = rd[1].ToString();
                coment.SendTime = rd[2].ToString();
                if (rd[3] is DBNull)
                {
                    coment.QuoteID = null;
                }
                else
                {
                    coment.QuoteID = rd[3].ToString();
                }
                coment.Type = rd[4].ToString();
            }
            //返回对象
            return coment;

        }


    }

>>>>>>> 96f694c8f0ba26cc2f78da25590696cc87e26b98
}