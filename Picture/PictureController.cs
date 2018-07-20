using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utility;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text;
using MyIGallery.Models;

namespace MyIGallery.Controllers
{
   
    public class PictureController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage FirstGet(string id,int type)
        {

            HttpResponseMessage result = new HttpResponseMessage();
            
            if (type == 2)
            {
                result.StatusCode = HttpStatusCode.OK;
                Tuple<ByteArrayContent, string> res = Util.Get(id, 2);
                result.Content=res.Item1; //头图返图
                string ext = res.Item2;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/"+ext.Substring(1,ext.Length-1));
            }
            else
            {
                List<string> list = Util.GetPid(id);
                result.Content = new StringContent(JsonConvert.SerializeObject(list), Encoding.GetEncoding("UTF-8"), "application/json"); //非头图返文件号
                
            }
            return result ;
        }

        [HttpGet]
        public HttpResponseMessage Gets(string pid)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            Tuple<ByteArrayContent, string> res = Util.Get(pid, 1);
            result.Content = res.Item1; //动态组图返图
            string ext = res.Item2;
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + ext.Substring(1, ext.Length - 1));
            return result;
        }

        [HttpPost]
        public void Save()
        {
            var data=HttpContext.Current.Request.Params;
            string id = data["id"];
            int type = Convert.ToInt32(data["type"]);

            Util.Post(id, type);
        }
    }
}

/*
 *    [HttpPost]
        public void Test()
        {
            var SrcRequest = HttpContext.Current.Request;

            foreach(string p in SrcRequest.Params.AllKeys)
            {
                var par= SrcRequest.Params[p];

            }
            
        }

*
* 
[HttpGet]
public ApiResultModel GetMmps(string id, int type)
{
    List<Byte[]> list = Util.Get(id, type);

    //return Json<List<ByteArrayContent>>(list);

    JObject joReturn = new JObject();
    foreach (Byte[] b in list)
    {
        joReturn.Add(new JProperty(Convert.ToString(list.IndexOf(b) + 1), Convert.ToBase64String(b)));
    }
    joReturn.Add(new JProperty("0", list.Count));

    ApiResultModel result = new ApiResultModel(HttpStatusCode.OK, joReturn, "");

    return result;
}
*/
