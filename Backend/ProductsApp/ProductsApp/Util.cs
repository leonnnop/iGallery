using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;
namespace Utility
{
    public class Util
    {
        // GET api/values/5  
        //给出用户id或动态id
        
        public static List<ByteArrayContent> Get(string id, int type)
        {
            
            //返回的图片字节数组
            List<ByteArrayContent> pics = new List<ByteArrayContent>();

            //打开数据库
            string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17";
            OracleConnection conn = new OracleConnection(connStr);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            int pid = 1;
            //文件路径不重名
            string str  = @"d:\iGallery\" + (Convert.ToString(pid));
            //取图片全路径
            if (type == 1)
            {
                cmd.CommandText = "select URL from PICTURE where ID='" + id + "'";  //多路径
            }
            else if(type==2)
            {
                cmd.CommandText = "select PHOTO from USERS where ID='"+id+"'";
            }
            OracleDataReader rd = cmd.ExecuteReader();
            
            while (rd.Read())
            {
                string filePath = rd[0].ToString();

                //本地读文件
                if (filePath.Trim().Equals(""))
                {
                    return null;
                }
                Image img = Image.FromFile(filePath);  //这里我把路径给出了，他只用给我文件名


                //图片文件转字节流
                MemoryStream ms = new MemoryStream();


                //分割后缀
                string fileExt = Path.GetExtension(filePath);

                //判断后缀
                ImageFormat format = null;
                switch (fileExt.ToLower())
                {
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                }

                //从格式图像转成字节流
                img.Save(ms, format);


                //字节流转回前端字节数组
                pics.Add(new ByteArrayContent(ms.ToArray())); //字节流转数组
                                                              //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                                                              //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/Png"); //设置http响应上的Content-Type 为image/Png媒体类型 // fileExt.Substring(1,fileExt.Length-1)
            }
            
            return pics;
        }


        //post api/values  
        //动态加组图（动态ID）or 个人信息修改加头图（用户ID） 
          public static void Post(string id,int type)
              {
                  //打开数据库
                  string connStr = @"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 112.74.55.60)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=vector;Password=Mustafa17";
                  OracleConnection conn = new OracleConnection(connStr);
                  conn.Open();
                  OracleCommand cmd = new OracleCommand();
                  cmd.Connection=conn;
                  //浏览器上传文件的body
                  var SrcRequest = HttpContext.Current.Request;
                  

                  string filepath = "";
                  //逐一存下body中给出的文件集合
                  foreach (string f in SrcRequest.Files.AllKeys)
                  {
                      HttpPostedFile file = SrcRequest.Files[f];
                      //分割后缀
                      string fileExt = Path.GetExtension(file.FileName);

                      if (type == 1)
                      {
                          //插入PICTURE一条记录
                          cmd.CommandText = "select count(*) from PICTURE";
                          int pid = Convert.ToInt32( cmd.ExecuteScalar())+1;
                          //文件路径不重名
                          filepath = @"d:\mmps\" + (Convert.ToString(pid))+ fileExt; 
                          cmd.CommandText = "insert into PICTURE(ID,URL,MOMENT_ID) values('" + Convert.ToString(pid) + "','" + filepath + "','" + id + "')";
                          cmd.ExecuteNonQuery();
                      }
                      else if (type == 2)
                      {
                          //更新用户个人头像
                          filepath = @"d:\heads\"+ id+fileExt; 
                          cmd.CommandText = "update USERS set PHOTO='"+filepath+"' where ID='"+id+"'";
                          cmd.ExecuteNonQuery();
                      }

                      //存本地
                      file.SaveAs(filepath);
                  }
              }
    }

}

