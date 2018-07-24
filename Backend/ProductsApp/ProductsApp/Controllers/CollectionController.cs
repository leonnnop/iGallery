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
    public class CollectionController : ApiController
    {
        //创建收藏夹
        [HttpPost]
        public IHttpActionResult InsertCollection(string founder_id, string name)
        {
            //创建返回信息，先假设插入成功
            int status = 0;

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
            OracleCommand cmd = new OracleCommand();

            //检查是否会与同一用户的其他收藏夹重名
            cmd.CommandText = "select NAME from COLLECTION where FOUNDER_ID='" + founder_id + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                string currentCollectionName = rd["NAME"].ToString();
                if(currentCollectionName == name)
                {
                    status = 1; //出现重名
                    break;
                }
            }

            if(status != 1)
            {
                //向数据库中插入一条新收藏夹的记录
                cmd.CommandText = "insert into COLLECTION(FOUNDER_ID,NAME) " +
                        "values('" + founder_id + "','" + name + "')";
                cmd.Connection = conn;
                int result = cmd.ExecuteNonQuery();
                if (result != 1)//插入出现错误
                {
                    status = 2;
                }
            }

            //关闭数据库连接
            rd.Close();
            conn.Close();

            //返回信息
            return Ok(status);
        }

        //删除收藏夹
        [HttpPost]
        public IHttpActionResult DeleteCollection(string founder_id, string name)
        {
            //创建返回信息，先假设删除成功
            int status = 0;

            if(name == "默认收藏夹")
            {
                status = 1;  //默认收藏夹不可删除
            }

            if(status != 1)
            {
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
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "delete from COLLECTION where FOUNDER_ID = '" + founder_id + "' and NAME = '" + name + "'";  //收藏夹下的所有动态应该也被级联删除
                cmd.Connection = conn;
                int result = cmd.ExecuteNonQuery();
                if (result != 1) //删除出现错误
                {
                    status = 2;
                }

                //关闭数据库连接
                conn.Close();
            }

            //返回信息
            return Ok(status);
        }

        //重命名收藏夹
        [HttpPut]
        public IHttpActionResult RenameCollection(string founder_id, string original_name, string new_name)
        {
            //创建返回信息，先假设更新成功
            int status = 0;

            if(original_name == "默认收藏夹")
            {
                status = 1; //默认收藏夹不可重命名
            }

            if(status != 1)
            {
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
                OracleCommand cmd = new OracleCommand();

                //检查是否会与同一用户的其他已有收藏夹重名
                cmd.CommandText = "select NAME from COLLECTION where FOUNDER_ID='" + founder_id + "'";
                cmd.Connection = conn;
                OracleDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string currentCollectionName = rd["NAME"].ToString();
                    if (currentCollectionName == new_name)
                    {
                        status = 2; //出现重名
                        break;
                    }
                }

                if (status != 2)
                {
                    //修改数据库中该收藏夹的名字
                    cmd.CommandText = "update COLLECTION set NAME='" + new_name + "' where FOUNDER_ID='" + founder_id + "' and NAME='" + original_name + "'";
                    cmd.Connection = conn;
                    int result = cmd.ExecuteNonQuery();
                    if (result != 1)//更新出现错误
                    {
                        status = 3;
                    }
                }

                //关闭数据库连接
                rd.Close();
                conn.Close();
            }

            //返回信息
            return Ok(status);
        }

        //统计某一收藏夹下的动态总数
        public int ReturnMomentNumInACollection(string founder_id, string name)
        {
            int momentNum = 0;

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
            cmd.CommandText = "select count(*) from COLLECT where FOUNDER_ID='" + founder_id + "' and NAME='" + name + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            if(rd.Read())
            {
                momentNum = rd.GetInt32(0);
            }
            rd.Close();
            conn.Close();

            return momentNum;
        }

        //根据动态ID获取该动态第一张图片的ID
        public string GetFirstPicIDbyMomentID(string moment_id)
        {
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
            cmd.CommandText = "select ID from PICTURE where MOMENT_ID='" + moment_id + "'"; //获取该动态下的所有图片ID
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            string pic_id = "";
            if (rd.Read())
            {
                pic_id = rd.GetString(0);
            }

            rd.Close();
            conn.Close();

            return pic_id;
        }

        //获取收藏夹中最新加入的一条动态里的第一张图片的ID作为收藏夹封面
        public string ReturnCollectionCoverPicID(string FounderID, string Name)
        {
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
            cmd.CommandText = "select MOMENT_ID from COLLECT where FOUNDER_ID='" + FounderID + "' and NAME='" + Name + "' order by TIME desc"; //获取该收藏夹下按收藏时间由新到旧排序的所有动态的ID
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            string moment_id = "";
            string cover_pic_id = "1";  //默认封面图片ID
            if (rd.Read())
            {
                moment_id = rd.GetString(0);
                cover_pic_id = GetFirstPicIDbyMomentID(moment_id);
            }

            rd.Close();
            conn.Close();

            return cover_pic_id;
        }

        //从数据库中获取某一用户的全部收藏夹（除默认收藏夹外）并以Json格式返回给前端
        [HttpGet]
        public IHttpActionResult ReturnUserCollections(string user_id)
        {
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
            cmd.CommandText = "select * from COLLECTION where FOUNDER_ID='" + user_id + "'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            //创建Collection对象List，并向其中添加读出的数据库信息
            List<Collection> resultList = new List<Collection>();

            while (rd.Read())
            {
                if(rd["NAME"].ToString() != "默认收藏夹")
                {
                    Collection col = new Collection();
                    col.FounderID = rd["FOUNDER_ID"].ToString();
                    col.Name = rd["NAME"].ToString();
                    resultList.Add(col);
                }
            }
            rd.Close();
            conn.Close();

            return Json<List<Collection>>(resultList);
        }

        //从数据库中获取某一收藏夹下全部动态的ID(按动态收藏时间由新到旧排序)并以string列表形式返回给前端
        public List<string> ReturnCollectionContentID(string FounderID, string Name)
        {
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
            cmd.CommandText = "select MOMENT_ID from COLLECT where FOUNDER_ID='" + FounderID + "' and NAME='" + Name + "' order by TIME desc";//获取按收藏时间由新到旧排序的该收藏夹下的所有动态ID
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();

            //创建string对象List，并向其中添加读出的数据库信息
            List<string> resultList = new List<string>();

            while (rd.Read())
            {
                string moment_id = rd.GetString(0);
                resultList.Add(moment_id);
            }
            rd.Close();
            conn.Close();

            return resultList;
        }

        //移动收藏的动态到其他收藏夹
        [HttpPut]
        public IHttpActionResult MoveMomentToAnotherCollection(string moment_id, string founder_id, string original_collection_name, string new_collection_name)
        {
            //创建返回信息，先假设更新成功
            int status = 0;

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
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "update COLLECT set NAME='" + new_collection_name + "' where MOMENT_ID='" + moment_id + "' and FOUNDER_ID='" + founder_id + "' and NAME='" + original_collection_name + "'";
            cmd.Connection = conn;
            int result = cmd.ExecuteNonQuery();
            if (result != 1)//更新出现错误
            {
                status = 1;
            }

            //关闭数据库连接
            conn.Close();

            //返回信息
            return Ok(status);
        }
    }
}
