using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;
using ProductsApp;
using ProductsApp.Controllers;
using System.Data;

namespace ProductsApp.Controllers
{
    public class ModifyMomentController : ApiController
    {
        /// <summary>
        /// 删除一条动态
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult DeleteMoment(string email, string moment_id)
        {

            //删除所有评论
            CmtApi.DelCmt(moment_id);


            int status = 0;

            
            DBAccess dBAccess = new DBAccess();

            GeneralAPI api = new GeneralAPI();

            //获取用户ID
            string user_id = api.EmailToUserID(email);
            if (user_id == null)
            {
                status = 1;//不存在该用户
            }

            //转发了别人的：删自己，更新别人的Tran_Num  【删forward，删moment，更新moment转发字段】
            //被别人转发：删一群别人，删自己            【删forward，删moment，删moment】


            //查Moment 找到 quote_id where ID=moment_id   转发了别人
            //查Forward 找到 User_id where moment_id=moment_id 被别人转发了 [//查Moment 找到 sender_id  where Quote_id=moment_id;]



            //删Forward where user_id=user_id && moment_id=quote_id;
            //删Forward where user_id=sender_id && moment_id=moment_id;

            //删Moment where ID=moment_id
            //删Moment where ID=moment_id

            //更新 Moment 的 tran_Num-- where id=quote_id
            //
            
            bool tran = true;
            bool tree = true;
            

            OracleDataReader rd1 = dBAccess.GetDataReader("select QUOTE_MID from MOMENT where ID='" + moment_id+"'");
            string oid = "";
            while (rd1.Read())
            {
                oid = rd1[0].ToString();
            }
            OracleDataReader rd2 = dBAccess.GetDataReader("select FORWARD_NUM from MOMENT where ID='" + oid + "'");
            int fnum = 0;
            while (rd2.Read())
            {
                fnum = int.Parse(rd2[0].ToString());
            }

            DataSet dataSet = dBAccess.GetDataSet("select USER_ID from FORWARD where MOMENT_ID='" + moment_id + "'","unknown");
            List<string> uids = new List<string>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                uids.Add(dr[0].ToString());
            }
            
            if (oid.Trim().Equals(""))
            {
                tran = false;
            }
            if (uids.Count.Equals(0))
            {
                tree = false;
            }

            if (!tran && !tree)
            {
                dBAccess.ExecuteSql("delete from MOMENT where id = '" + moment_id + "'");
            }
            if (tran && !tree)
            {
                dBAccess.ExecuteSql("delete from FORWARD where moment_id = '" + oid + "'and user_id='"+user_id+"'");

                dBAccess.ExecuteSql("delete from MOMENT where id = '" + moment_id + "'");

                dBAccess.ExecuteSql("update MOMENT set FORWARD_NUM='"+fnum+"' where id = '" + oid + "'");

            }
            if (!tran && tree)
            {
                dBAccess.ExecuteSql("delete from FORWARD where moment_id = '" + moment_id + "'");

                dBAccess.ExecuteSql("delete from MOMENT where quote_mid = '" + moment_id + "'");

                dBAccess.ExecuteSql("delete from MOMENT where id = '" + moment_id + "'");
            }

            status = 0;

            ////执行数据库操作
            //OracleDataReader rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + moment_id + "'and SENDER_ID = '" + user_id + "'");
            //if (rd.Read())
            //{

            //    //获取该动态的quote_mid
            //    string quote_mid = rd["QUOTE_MID"].ToString();

            //    if (dBAccess.ExecuteSql("delete from MOMENT where sender_id = '" + user_id + "'and id = '" + moment_id + "'"))
            //    {
            //        status = 0;//成功删除
            //    }
            //    else
            //    {
            //        status = 3;//动态删除失败
            //    }
            //    if (quote_mid != "")//该条动态来自转发，同时修改forward表
            //    {
            //        rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + quote_mid + "'");
            //        if (rd.Read())
            //        {
            //            //设置新的转发数
            //            int new_forward_num = int.Parse(rd["FORWARD_NUM"].ToString()) - 1;

            //            if (dBAccess.ExecuteSql("delete from FORWARD where user_id = '" + user_id + "'and moment_id = '" + quote_mid + "'"))
            //            {
            //                if (dBAccess.ExecuteSql("update MOMENT set forward_num= ' " + new_forward_num + " 'where  ID='" + quote_mid + "' "))
            //                {
            //                    status = 0;//成功删除该动态和转发表项，并且源动态转发数减一
            //                }
            //                else
            //                {
            //                    status = 6;//成功删除该动态和转发表项，但源动态转发数没有更改
            //                }
            //            }
            //            else
            //            {
            //                status = 5;//转发表项删除失败
            //            }

            //        }
            //        else
            //        {
            //            status = 4;//该动态删除成功，找不到源动态
            //        }
            //    }
            //}
            //else
            //{
            //    status = 2;//该用户没有发表过该动态
            //}

            return Ok(status);
        }




        /// <summary>
        /// 修改一条动态
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="moment_id">string</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult ModifyMoment([FromBody]Modify_Moment modifyMoment)
        {
            int status = 0;

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //创建api对象
            GeneralAPI api = new GeneralAPI();

            //获取用户ID
            string user_id = api.EmailToUserID(modifyMoment.email);
            if (user_id == null)
            {
                status = 1;//不存在该用户
            }

            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + modifyMoment.moment_id + "'and SENDER_ID = '" + user_id + "'");
            if (rd.Read())
            {
                string currentTime = DateTime.Now.ToString();
                if (dBAccess.ExecuteSql("update MOMENT set content = '" + modifyMoment.content + "',time = TO_DATE ('" + currentTime + "','yyyy-mm-dd hh24:mi:ss') where sender_id = '" + user_id + "'and id = '" + modifyMoment.moment_id + "'"))
                {
                    status = 0;//修改成功
                }
                else
                {
                    status = 3;//修改失败
                }
            }
            else
            {
                status = 2;//该用户没有发表过该动态
            }

            return Ok(status);
        }
    }
}
