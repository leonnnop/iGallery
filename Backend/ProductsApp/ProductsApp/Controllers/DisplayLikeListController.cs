using IGallery;
using IGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;

namespace IGallery.Controllers
{
    public class DisplayLikeListController : ApiController

    {
        [HttpGet]
        public IHttpActionResult GetLikeList(string moment_id)
        {
            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //执行数据库select操作
            OracleDataReader rd = dBAccess.GetDataReader("select u.* from FAVORITE f, USERS u where f.moment_id = '" + moment_id + "' and f.user_id = u.id");
            
            //创建Users对象List，并向其中添加读出的数据库信息
            List<Users> resultList = new List<Users>();

            while ( rd.Read() )//当数据库能读出一条符合条件的元组，执行循环
            {
                Users u = new Users();
                u.ID = rd["ID"].ToString();
                u.Username = rd["USERNAME"].ToString();
                u.Password = "null";
                u.Photo = rd["PHOTO"].ToString();
                u.Email = rd["EMAIL"].ToString();
                u.Bio = rd["BIO"].ToString();
                resultList.Add(u);

            }

            return Json<List<Users>>(resultList);//返回符合条件的Users列表

        }
    }
}
