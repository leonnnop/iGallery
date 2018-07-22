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
        public bool AddTag([FromUri]string[] TagNames, string Moment_Id)
        {
            bool state = false;
            
            foreach(string TagName in TagNames)
            {
                string select = Access.Select("*", "TAG", "CONTENT = '" + TagName + "'");

                if (Access.GetRecordCount(select) == 0) //Tag不存在，创建
                {
                    if (TagName.Length <= 20)
                    {
                        string insert = Access.Insert("TAG", "'" + TagName + "'");
                        if (!Access.ExecuteSql(insert))  //插入失败
                        {
                            state = false;
                        }
                        else
                        {
                            state = true;
                        }
                    }
                    else
                        state = false;
                }
                //Tag存在

                string read = Access.Select("*", "MOMENT_TAG", "MOMENT_ID='" + Moment_Id + "' and TAG='" + TagName + "'");
                if(Access.GetRecordCount(read)==0) //这条记录在表中没有
                {
                    //在表中增加这条记录
                    string sql = Access.Insert("MOMENT_TAG", "'" + Moment_Id + "'" + ",'" + TagName + "'");
                    if (!Access.ExecuteSql(sql))
                    {
                        state = false;
                    }
                    else
                        state = true;
                }
                else  //记录在表中存在
                    state = true;
                
            }
            string S = Access.Select("TAG", "MOMENT_TAG", "MOMENT_ID='" + Moment_Id + "'");
            OracleDataReader r = Access.GetDataReader(S);
            while (r.Read())
            {
                string name = r[0].ToString();
                if(Array.IndexOf(TagNames,name)==-1)
                {
                    string delete = Access.Delete("MOMENT_TAG", "MOMENT_ID='" + Moment_Id + "' and TAG='" + name + "'");
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = delete;
                    cmd.Connection = Access.Connection;
                    if (cmd.ExecuteNonQuery() == 0)
                        state = false;
                    else
                        state = true;
                }
            }
            return state;
        }
    }
}
