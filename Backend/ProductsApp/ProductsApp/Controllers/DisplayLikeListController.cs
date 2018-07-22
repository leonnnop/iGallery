using ProductsApp;
using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class DisplayLikeListController : ApiController

    {

        /// <summary>
        /// 获取一条动态的点赞用户列表
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetLikeList(string email, string moment_id)
        {
            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库select操作
            OracleDataReader rd = dBAccess.GetDataReader(" select u.* from FAVORITE f, USERS u where f.moment_id = '" + moment_id + "' and f.user_id = u.id  ");
            
            //创建Users对象List，并向其中添加读出的数据库信息
            List<User_Follow> resultList = new List<User_Follow>();

            //创建api对象
            GeneralAPI api = new GeneralAPI();

            while ( rd.Read() )//当数据库能读出一条符合条件的元组，执行循环
            {
                User_Follow uf = new User_Follow();
                uf.ID = rd["ID"].ToString();
                uf.Username = rd["USERNAME"].ToString();
                uf.Photo = rd["PHOTO"].ToString();
                uf.Email = rd["EMAIL"].ToString();
                uf.Bio = rd["BIO"].ToString();
                string user_id = api.EmailToUserID(email);
                int follow_state = api.CheckFollowState(user_id, rd["ID"].ToString());
                if (follow_state == 0)//关注
                {
                    uf.FollowState = "true";
                }
                else//未关注
                {
                    uf.FollowState = "false";
                }

                resultList.Add(uf);

            }

            return Json<List<User_Follow>>(resultList);//返回符合条件的Users列表

        }
    }
}
