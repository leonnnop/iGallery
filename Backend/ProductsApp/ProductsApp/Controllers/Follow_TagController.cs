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
        public HttpResponseMessage FollowTag(string UserId, string tag)
        {
           HttpResponseMessage response = Request.CreateResponse();
           if (Access.ExecuteSql(Access.Insert("FOLLOW_TAG", "'" + UserId + "', '" + tag + "'")))
           {
               response.StatusCode = HttpStatusCode.OK;
               response.Content = new StringContent("关注成功", Encoding.Unicode);
           }
           else
           {
               response.StatusCode = HttpStatusCode.Forbidden;
               response.Content = new StringContent("关注失败", Encoding.Unicode);
           }
           return response;
        }

        [HttpPut]
        public HttpResponseMessage CancleFollowTag(string UserId, string tag)
        {
            HttpResponseMessage response = Request.CreateResponse();
            if (Access.ExecuteSql(Access.Delete("FOLLOW_TAG", "USER_ID= '" + UserId + "' and TAG= '" + tag + "'")))
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent("取关成功", Encoding.Unicode);
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                response.Content = new StringContent("取关失败", Encoding.Unicode);
            }
            return response;
        }
    }
}
