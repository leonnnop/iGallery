using System;
using ProductsApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;

namespace ProductsApp.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Register([FromBody]User_register user)
        {
            //创建返回信息，先假设注册成功
            int status = 0;

            //连接数据库
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

            //检查邮箱是否已被用于注册
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText="select * from USERS t where email='"+user.Email+"'";
            cmd.Connection = conn;
            OracleDataReader rd = cmd.ExecuteReader();
            if(rd.HasRows)//邮箱已注册
            {
                status = 1;
            }
            else//邮箱未注册
            {
                //为用户生成一个ID
                int id = 1;
                cmd.CommandText = "select count(*) from users";
                rd = cmd.ExecuteReader();
                rd.Read();
                id += rd.GetInt32(0);

                //将新建用户插入数据库
                cmd.CommandText = "insert into USERS(ID,EMAIL,PASSWORD,USERNAME,BIO,PHOTO) " +
                    "values('"+id.ToString()+"','"+user.Email+"','"+user.Password+"','"+user.Username+"','null','null')";

                int result = cmd.ExecuteNonQuery();
                if (result != 1)//插入出现错误
                {
                    status = 2;
                }
            }

            //关闭数据库连接
            conn.Close();

            //返回信息
           return Ok(status);
        }
    }
}
