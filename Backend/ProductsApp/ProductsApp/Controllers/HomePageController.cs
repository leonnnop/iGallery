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
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17";
            OracleConnection conn = new OracleConnection(connStr);
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
            cmd.CommandText = "select m.id as m_id,m.content, m.like_num, m.forward_num, m.collect_num, m.comment_num, m.time, p.id as p_id, p.url"+
                              " from MOMENT m,USERS u " +
                              "where m.id = p.moment_id and m.sender_id= '" + Sender_id + "'" +
                              "order by m.time desc";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            //创建User_Moment对象List，并向其中添加读出的数据库信息
            List<MyMoments> MyMomentList = new List<MyMoments>();

            while (rd.Read())//当数据库能读出一条符合条件的元组，执行循环
            {
                MyMoments m = new MyMoments();
                m.M_ID = rd["M_ID"].ToString();
                m.Content = rd["CONTENT"].ToString();
                m.LikeNum = int.Parse(rd["LIKE_NUM"].ToString());
                m.ForwardNum = int.Parse(rd["FORWARD_NUM"].ToString());
                m.CollectNum = int.Parse(rd["COLLECT_NUM"].ToString());
                m.CommentNum = int.Parse(rd["COMMENT_NUM"].ToString());
                m.Time = rd["TIME"].ToString();
                m.P_ID = rd["P_ID"].ToString();
                m.URL = rd["URL"].ToString();
                MyMomentList.Add(m);
            }


            rd.Close();
            conn.Close();
            //以json格式返回数组
            return Json<List<MyMoments>>(MyMomentList);

        }

    }
}
