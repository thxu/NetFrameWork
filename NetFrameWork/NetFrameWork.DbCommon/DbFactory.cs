using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace NetFrameWork.DbCommon
{
    /// <summary>
    /// 数据库链接工厂
    /// </summary>
    public static class DbFactory
    {
        /// <summary>
        /// 创建数据库链接
        /// </summary>
        /// <param name="name">链接名称</param>
        /// <returns>数据库链接</returns>
        public static IDbConnection GetConnection(string name)
        {
            ConnectionStringSettings connectionSetting = ConfigurationManager.ConnectionStrings[name];
            DbConnection conn;
            switch (connectionSetting.ProviderName.ToLower())
            {
                case "mysql.data.mysqlclient":
                    conn = new MySqlConnection(connectionSetting.ConnectionString);
                    break;
                default:
                    conn = new SqlConnection(connectionSetting.ConnectionString);
                    break;
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }
    }
}
