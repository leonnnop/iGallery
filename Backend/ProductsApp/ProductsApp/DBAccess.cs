using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
namespace ProductsApp
{
    public class DBAccess
    {
        public OracleConnection Connection;
        //public static string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60 )(PORT = 1521)))(CONNECT_DATA =(SID = orcl)));User Id=vector;Password=Mustafa17;Pooling=true;Connection Timeout=60; Max Pool Size=300";
        public static string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.8 )(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=c##vector;Password=Mustafa17;Pooling=true;Connection Timeout=60; Max Pool Size=300";
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBAccess()
        {
            Connection = new OracleConnection(connStr);
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void OpenConn()
        {
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConn()
        {
            if (this.Connection.State == ConnectionState.Open)
                this.Connection.Close();
        }

        /// <summary>
        /// 执行sql语句，返回数据到DataSet中
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="DataSetName"></param>
        /// <returns>返回DataSet</returns>
        public DataSet GetDataSet(string sql, string DataSetName)
        {
            DataSet ds = new DataSet();
            OpenConn();
            OracleDataAdapter OraDA = new OracleDataAdapter(sql, Connection);
            OraDA.Fill(ds, DataSetName);
            CloseConn();
            return ds;
            
        }

        /// <summary>
        /// 执行sql语句，返回数据到带有分页功能的DataSet中，一个页面下放一定数目的数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="DataSetName"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <returns>返回DataSet</returns>
        public DataSet GetDataSet(string sql, string DataSetName, int PageSize, int CurrentPage)
        {
            DataSet ds = new DataSet();
            OpenConn();
            OracleDataAdapter OraDA = new OracleDataAdapter(sql, Connection);
            OraDA.Fill(ds, PageSize * (CurrentPage - 1), PageSize, DataSetName);
            CloseConn();
            return ds;
        }

        /// <summary>
        /// 执行select语句，使用之前需要调用Read()方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回OracleDataReader</returns>
        public OracleDataReader GetDataReader(string sql)
        {
            OpenConn();
            OracleCommand cmd = new OracleCommand(sql, Connection);
            //OracleDataReader rd = cmd.ExecuteReader();
            //CloseConn();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 返回符合查询条件的记录总数，使用select语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回记录总数</returns>
        public int GetRecordCount(string sql)
        {
            int nums = 0;
            OpenConn();
            OracleCommand cmd = new OracleCommand(sql, Connection);
            OracleDataReader read = cmd.ExecuteReader();
            while (read.Read())
                nums++;
            read.Close();
            CloseConn();
            return nums;
        }

        /// <summary>
        /// 执行insert，update，delete语句并判断执行是否成功
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回增删改语句的执行结果</returns>
        public bool ExecuteSql(string sql)
        {
            OpenConn();
            OracleCommand cmd = new OracleCommand(sql, Connection);
            if (cmd.ExecuteNonQuery() == 0)
            {
                CloseConn();
                return false;
            }
            else
            {
                CloseConn();
                return true;
            }
        }

        /// <summary>
        /// 构造select语句
        /// </summary>
        /// <param name="Colums">要选的内容</param>
        /// <param name="TableName">表名称</param>
        /// <param name="Condition">选择条件</param>
        /// <returns></returns>
        public string Select(string Colums,string TableName,string Condition)
        {
            return  "select " + Colums + " from " + TableName + " where " + Condition;
        }

        /// <summary>
        /// 构造update语句
        /// </summary>
        /// <param name="TableName">要更新的表名</param>
        /// <param name="Result">更新的结果</param>
        /// <param name="Condition">更新条件</param>
        /// <returns></returns>
        public string Update(string TableName,string Result,string Condition)
        {
            return "update " + TableName + " set " + Result + " where " + Condition;
        }

        /// <summary>
        /// 构造insert语句
        /// </summary>
        /// <param name="TableName">要插入的表名</param>
        /// <param name="Result">要插入的内容</param>
        /// <returns></returns>
        public string Insert(string TableName,string Result)
        {
            return "insert into " + TableName + " values (" + Result + " )";
        }

        /// <summary>
        /// 构造insert语句
        /// </summary>
        /// <param name="TableName">要插入的表名</param>
        /// <param name="Colums">表的列</param>
        /// <param name="Result">要插入的结果</param>
        /// <returns></returns>
        public string Insert(string TableName,string Colums,string Result)
        {
            return "insert into " + TableName + " (" + Colums + " ) values (" + Result + " )";
        }

        /// <summary>
        /// 构造delete语句
        /// </summary>
        /// <param name="TableName">要进行删除的表名</param>
        /// <param name="Conditions">删除条件</param>
        /// <returns></returns>
        public string Delete(string TableName,string Conditions)
        {
            return "delete from " + TableName + " where " + Conditions;
        }
        
    }
}