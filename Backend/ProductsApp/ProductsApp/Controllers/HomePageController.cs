using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class HomePageController : ApiController
    {
        /// <summary>
        /// 获取个人主页内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMyMoments(string Sender_id)
        {
            //todo:连接数据库
            OracleConnection conn = new OracleConnection(DBAccess.connStr);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        
            //执行数据库操作
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select id,content,like_num,forward_num,collect_num,comment_num,time"+
                              " from MOMENT " +
                              "where sender_id = '" + Sender_id + "'" +
                              "order by time desc";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            //创建User_Moment对象List，并向其中添加读出的数据库信息
            List<Moment> MyMomentList = new List<Moment>();

            while (rd.Read())//当数据库能读出一条符合条件的元组，执行循环
            {
                Moment m = new Moment();
                m.ID = rd["ID"].ToString();
                m.Content = rd["CONTENT"].ToString();
                m.LikeNum = int.Parse(rd["LIKE_NUM"].ToString());
                m.ForwardNum = int.Parse(rd["FORWARD_NUM"].ToString());
                m.CollectNum = int.Parse(rd["COLLECT_NUM"].ToString());
                m.CommentNum = int.Parse(rd["COMMENT_NUM"].ToString());
                m.Time = rd["TIME"].ToString();
                MyMomentList.Add(m);
            }


            rd.Close();
            conn.Close();
            //以json格式返回数组
            return Json<List<Moment>>(MyMomentList);

        }

    }
}
