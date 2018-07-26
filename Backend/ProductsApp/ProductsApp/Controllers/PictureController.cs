﻿using System;
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
using ProductsApp.Models;

namespace ProductsApp.Controllers
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
                if (res == null)
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    return result;
                }
                result.Content=res.Item1; //头图返图
                string ext = res.Item2;   //格式解析
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/"+ext.Substring(1,ext.Length-1));
            }
            else
            {
                List<string> list = Util.GetPid(id);
                if (list.Count==0)
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    return result;
                }
                result.Content = new StringContent(JsonConvert.SerializeObject(list), Encoding.GetEncoding("UTF-8"), "application/json"); //非头图返文件号
                
            }
            return result ;
        }

        [HttpGet]
        public HttpResponseMessage Gets(string pid)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            Tuple<ByteArrayContent, string> res = Util.Get(pid, 1);
            result.Content = res.Item1; //动态组图返图pid
            string ext = res.Item2;   //格式解析
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + ext.Substring(1, ext.Length - 1));
            return result;
        }

        [HttpGet]
        public HttpResponseMessage GetSz(string mid)
        {
            //获得图片url
            HttpResponseMessage response = new HttpResponseMessage();
            DBAccess dBAccess = new DBAccess();
            OracleDataReader rd=dBAccess.GetDataReader("select '" + url + "' from PICTURE where mid='" + mid + "'");
            var path = "";
            if (rd.Read())
            {
                path = rd[0].ToString();
            }
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("width", image.Width);
            dic.Add("height", image.Height);
            string json = JsonConvert.SerializeObject(dic, Formatting.Indented);
            response.WriteLine(json);
        }
        
        
        [HttpPost]
        public HttpResponseMessage Save(string id, int type)
        {
            
            HttpResponseMessage result = new HttpResponseMessage();

            if(Util.Post(id, type))
            {
                result.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
            }
            return result;
        }
    }
}
