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
            //设置初始状态码
            int status = 0;

            DBAccess dBAccess = new DBAccess();
            GeneralAPI api = new GeneralAPI();

            //获取当前用户ID
            string user_id = api.EmailToUserID(email);
            if (user_id == null)
            {
                status = 1;//不存在该用户
            }

            //获取本条动态信息
            OracleDataReader rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + moment_id + "'and SENDER_ID = '" + user_id + "'");
            if (rd.Read())
            {

                //获取该动态的quote_mid
                string quote_mid = rd["QUOTE_MID"].ToString();

                //
                //判断本条动态是否来自转发
                //
                if (quote_mid != "")
                {
                    rd = dBAccess.GetDataReader("select * from MOMENT where ID='" + quote_mid + "'");
                    if (rd.Read())
                    {
                        //设置新的转发数
                        int new_forward_num = int.Parse(rd["FORWARD_NUM"].ToString()) - 1;

                        //删除本条动态的所有评论
                        CmtApi.DelCmt(moment_id);

                        //删除本条动态
                        if (dBAccess.ExecuteSql("delete from MOMENT where sender_id = '" + user_id + "'and id = '" + moment_id + "'"))
                        {
                            status = 0;//成功删除
                        }
                        else
                        {
                            status = 3;//动态删除失败
                        }

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

                //
                //本条动态为源动态
                //
                else
                {
                    //获取转发了本条动态的动态id
                    DataSet ds = dBAccess.GetDataSet("select ID from MOMENT where QUOTE_MID='" + moment_id + "'", "unknown");
                    List<string> list = new List<string>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(dr[0].ToString());
                    }

                    bool forward = true;

                    //判断是否有人转发本动态
                    if (list.Count.Equals(0))
                    {
                        forward = false;
                    }

                    if (forward) //有人转发本条动态，需要删除所有转发动态
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string mid = dr[0].ToString();
                            CmtApi.DelCmt(mid); //删除对应动态的全部评论
                            dBAccess.ExecuteSql("delete from MOMENT where id = '" + mid + "'"); //从Moment表删除全部转发自本动态的动态

                            if (dBAccess.ExecuteSql("delete from FORWARD where moment_id = '" + moment_id + "'")) //从forward表删除全部转发表项
                            {
                                status = 0;
                            }
                            else
                            {
                                status = 7;
                            }

                            //删除本条动态的所有评论
                            CmtApi.DelCmt(moment_id);

                            
                        }
                    }

                    //删除本条动态
                    if (dBAccess.ExecuteSql("delete from MOMENT where id = '" + moment_id + "'"))
                    {
                        status = 0;//成功删除
                    }
                    else
                    {
                        status = 3;//动态删除失败
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
