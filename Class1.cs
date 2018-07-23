using System;
using System.Collections.Generic;
using System.Data;

namespace ProductsApp.Controllers
{
    //删除联系集 PUBLISH_COMMENT
    //删除评论   COMENT
    //删除动态   MOMENT
    public class CmtApi
    {
        //给我所有这个动态ID下的评论ID
        public static void DelCmt(string mid)
        {
            DBAccess db = new DBAccess();
            
            //通过联系集得到实体ID
            string sql= "select COMMENT_ID from PUBLISH_COMMENT where MOMENT_ID='" + mid + "'";
            string name = "delComents";
            DataSet dataSet=db.GetDataSet(sql,name);

            List<string> cids = new List<string>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                cids.Add(dr[0].ToString());
            }
            
            //过河拆桥
            sql = "delete from PUBLISH_COMMENT where MOMENT_ID='" + mid + "'";
            if (db.ExecuteSql(sql))
            {
                Console.Write("relationship set cleared");
            }
            else
            {
                Console.Write("problems");
            }
            
            //删评论实体
            foreach (string cid in cids)
            {
                sql = "delete from COMENT where ID='" + cid + "'";
                db.ExecuteSql(sql);
            }
            
        }
    }
}