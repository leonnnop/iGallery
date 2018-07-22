using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp
{
    public class UserMomentAPI
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
        /// 返回int值：“0”表示已关注，“1”表示未关注
        public int CheckFollowState(string id_1, string id_2)
        {
            //bool result = false;
            //string ID = null;

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

        
    }
}