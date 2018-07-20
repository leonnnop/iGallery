using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ProductsApp.Models;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    public class Follow_TagController : ApiController
    {
        DBAccess Access = new DBAccess();
        [HttpPut]
        public bool FollowTag(string UserId, string tag)
        {
            HttpResponseMessage response = Request.CreateResponse();
            bool WhetherExit = false;
            string sql = Access.Select("*", "FOLLOW_TAG", "USER_ID='" + UserId + "' and TAG='" + tag + "'");
            if (Access.GetRecordCount(sql) == 0)  //用户未关注Tag
            {
                if (Access.ExecuteSql(Access.Insert("FOLLOW_TAG", "'" + UserId + "', '" + tag + "'")))
                {
                    response.StatusCode = HttpStatusCode.OK;
                    return true; //修改成功
                }
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    return false;
                }
            }
            else
            {
                if (Access.ExecuteSql(Access.Delete("FOLLOW_TAG", "USER_ID='" + UserId + "' and TAG='" + tag + "'")))
                {
                    response.StatusCode = HttpStatusCode.OK;
                    return true; //修改成功
                }
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    return false;
                }
            }
        }
    }
}
