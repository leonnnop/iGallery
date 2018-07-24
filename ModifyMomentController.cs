using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;
using MyIGallery;
using MyIGallery.Controllers;

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

            //todo:连接数据库
            DBAccess dBAccess = new DBAccess();

            //创建api对象
            GeneralAPI api = new GeneralAPI();

            //获取用户ID
            string user_id = api.EmailToUserID(email);
            if (user_id == null)
            {
                status = 1;//不存在该用户
            }

            

            //执行数据库操作
            OracleDataReader rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + moment_id + "'and SENDER_ID = '" + user_id + "'");
            if (rd.Read())
            {
                
                //获取该动态的quote_mid
                string quote_mid = rd["QUOTE_MID"].ToString();

                if (dBAccess.ExecuteSql("delete from MOMENT where sender_id = '" + user_id + "'and id = '" + moment_id + "'"))
                {
                    status = 0;//成功删除
                }
                else
                {
                    status = 3;//动态删除失败
                }
                if (quote_mid != "")//该条动态来自转发，同时修改forward表
                {
                    rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + quote_mid + "'");
                    if (rd.Read())
                    {
                        //设置新的转发数
                        int new_forward_num = int.Parse(rd["FORWARD_NUM"].ToString()) - 1;

                        if (dBAccess.ExecuteSql("delete from FORWARD where user_id = '" + user_id + "'and moment_id = '" + quote_mid + "'"))
                        {
                            if (dBAccess.ExecuteSql("update MOMENT set forward_num= ' " + new_forward_num + " 'where  ID='" + quote_mid + "' "))
                            {
                                status = 0;//成功删除该动态和转发表项，并且源动态转发数减一
                            }
                            else
                            {
                                status = 6;//成功删除该动态和转发表项，但源动态转发数没有更改
                            }
                        }
                        else
                        {
                            status = 5;//转发表项删除失败
                        }

                    }
                    else
                    {
                        status = 4;//该动态删除成功，找不到源动态
                    }
                }
            }
            else
            {
                status = 2;//该用户没有发表过该动态
            }

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
                string currentTime = DateTime.Now.ToString("yyyyMMddhhmmss");
                if (dBAccess.ExecuteSql("update MOMENT set content = '"+ modifyMoment.content + "',time = TO_TIMESTAMP ('" + currentTime + "','yyyy-mm-dd hh24:mi:ss.ff') where sender_id = '" + user_id + "'and id = '" + modifyMoment.moment_id + "'"))
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
