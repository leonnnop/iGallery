代码中每个函数还有具体解释，不懂的可以来问我

需要连接数据库时，先创建一个DBAccess对象。
例：DBAccess   Access = new  DBAccess();
之后可以直接调用函数进行操作，不需要考虑打开以及关闭数据库。

public DataSet GetDataSet(string sql, string DataSetName)
该函数将从数据库中搞出来的数据放进DataSet中，
直接创建一个DataSet对象，
例：DataSet set = Access.GetDataSet(sql, "此处为数据库中表名");
可以用foreach一行行循环记录：foreach(var row in set.Tables[0].Rows)
然后row["列名称"]可以直接得到该行某列的数据

public DataSet GetDataSet(string sql, string DataSetName, int PageSize, int CurrentPage)
该函数得到带有分页功能的DataSet，
分页功能是指，前端要展示很多东西的时候，进行分页，会返回当前页号，以及每页最大的内容量


public OracleDataReader GetDataReader(string sql)
该函数使用select语句读取数据库，使用前要使用一下Read().
如果不想用DataSet可考虑用这个

public int GetRecordCount(string sql)
返回select语句读取的记录的总数，可以计算记录数目

public bool ExecuteSql(string sql)
判断增删改语句是否执行成功

public string Select(string Colums,string TableName,string Condition)
构造select语句，按照语义，依次填入内容即可

public string Update(string TableName,string Result,string Condition)
构造update语句

public string Insert(string TableName,string Result)
构造insert语句

public string Insert(string TableName,string Colums,string Result)
构造insert语句，重载

public string Delete(string TableName,string Conditions)
构造delete语句

