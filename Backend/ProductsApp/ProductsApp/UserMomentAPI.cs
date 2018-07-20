using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp
{
    public class UserMomentAPI
    {
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

        public string GetUserID(string email)
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



    }
}