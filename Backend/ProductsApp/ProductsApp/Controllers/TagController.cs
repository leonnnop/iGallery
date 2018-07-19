using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Text;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class TagController : ApiController
    {
        
        DBAccess Access = new DBAccess();
        /// <summary>
        /// 用户关注Tag
        /// </summary>
        /// <param name="TagName"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage AddTag(string TagName)
        {
            HttpResponseMessage response = Request.CreateResponse();

            string select = Access.Select("*", "TAG", "CONTENT = '" + TagName + "'");
            
            if(Access.GetRecordCount(select)==0) //Tag不存在，创建
            {
                string insert = Access.Insert("TAG", TagName);
                if (!Access.ExecuteSql(insert))  //插入失败
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    TagName = null;
                }
                else
                    response.StatusCode = HttpStatusCode.OK;

            }
            response.Content = new StringContent(TagName, Encoding.Unicode);
            return response;
        }
    }
}
