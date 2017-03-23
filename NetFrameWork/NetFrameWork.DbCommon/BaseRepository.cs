using System;
using System.Data;
using NetFrameWork.Infrastructure;

namespace NetFrameWork.DbCommon
{
    /// <summary>
    /// 底层基础处理仓储
    /// </summary>
    public abstract class BaseRepository : IDisposable
    {
        /// <summary>
        /// IDbConnection
        /// </summary>
        private IDbConnection connection;

        /// <summary>
        /// IDbCommand
        /// </summary>
        private IDbCommand cmd;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="unit">工作单元</param>
        /// <param name="name">链接名称</param>
        protected BaseRepository(IUnitOfWork unit, string name)
        {
            if (unit != null)
            {
                this.cmd = unit.Command;
            }
            else
            {
                if (this.connection == null)
                {
                    this.connection = DbFactory.GetConnection(name);
                }

                if (this.connection.State != ConnectionState.Open)
                {
                    this.connection.Open();
                }

                this.cmd = this.connection.CreateCommand();
            }
        }

        #region Parameter
        /// <summary>
        /// The add parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        protected IDbDataParameter AddParameter(string name)
        {
            IDbDataParameter param = this.CreateParameter(name);
            this.cmd.Parameters.Add(param);
            return param;
        }

        /// <summary>
        /// The add parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        protected IDbDataParameter AddParameter(string name, object value)
        {
            IDbDataParameter param = this.CreateParameter(name, value);
            this.cmd.Parameters.Add(param);
            return param;
        }

        /// <summary>
        /// The add parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        protected IDbDataParameter AddParameter(string name, object value, DbType type)
        {
            IDbDataParameter param = this.CreateParameter(name, value, type);
            this.cmd.Parameters.Add(param);
            return param;
        }

        /// <summary>
        /// The add parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        protected IDbDataParameter AddParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            IDbDataParameter param = this.CreateParameter(name, value, type, direction);
            this.cmd.Parameters.Add(param);
            return param;
        }

        /// <summary>
        /// The add parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        protected IDbDataParameter AddParameter(string name, object value, DbType type, ParameterDirection direction, int size)
        {
            IDbDataParameter param = this.CreateParameter(name, value, type, direction, size);
            this.cmd.Parameters.Add(param);
            return param;
        }

        /// <summary>
        /// The add parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="scale">
        /// The scale.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        protected IDbDataParameter AddParameter(string name, object value, DbType type, ParameterDirection direction, int size, byte scale)
        {
            IDbDataParameter param = this.CreateParameter(name, value, type, direction, size, scale);
            this.cmd.Parameters.Add(param);
            return param;
        }

        /// <summary>
        /// 清除参数
        /// </summary>
        protected void ClearParameters()
        {
            this.cmd.Parameters.Clear();
        }
        #endregion

        #region  ExecuteReader

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql, CommandType type, CommandBehavior behavior, int timeout)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql");
            }

            this.cmd.CommandText = sql;
            this.cmd.CommandType = type;
            this.cmd.CommandTimeout = timeout;
            var result = this.cmd.ExecuteReader(behavior);

            return result;
        }

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql, CommandType type, CommandBehavior behavior)
        {
            return this.ExecuteReader(sql, type, behavior, 0);
        }

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql, CommandType type, int timeout)
        {
            return this.ExecuteReader(sql, type, CommandBehavior.Default, timeout);
        }

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql, CommandType type)
        {
            return this.ExecuteReader(sql, type, CommandBehavior.Default, 0);
        }

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql, CommandBehavior behavior, int timeout)
        {
            return this.ExecuteReader(sql, CommandType.Text, behavior, timeout);
        }

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql, CommandBehavior behavior)
        {
            return this.ExecuteReader(sql, CommandType.Text, behavior, 0);
        }

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql, int timeout)
        {
            return this.ExecuteReader(sql, CommandType.Text, CommandBehavior.Default, timeout);
        }

        /// <summary>
        /// The execute reader.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <returns>
        /// The <see cref="IDataReader"/>.
        /// </returns>
        protected IDataReader ExecuteReader(string sql)
        {
            return this.ExecuteReader(sql, CommandType.Text, CommandBehavior.Default, 0);
        }

        #endregion

        #region ExecuteTable

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql, CommandType type, CommandBehavior behavior, int timeout)
        {
            using (IDataReader dr = this.ExecuteReader(sql, type, behavior, timeout))
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
        }

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql, CommandType type, CommandBehavior behavior)
        {
            return this.ExecuteTable(sql, type, behavior, 0);
        }

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql, CommandType type, int timeout)
        {
            return this.ExecuteTable(sql, type, CommandBehavior.Default, timeout);
        }

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql, CommandType type)
        {
            return this.ExecuteTable(sql, type, CommandBehavior.Default, 0);
        }

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql, CommandBehavior behavior, int timeout)
        {
            return this.ExecuteTable(sql, CommandType.Text, behavior, timeout);
        }

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql, CommandBehavior behavior)
        {
            return this.ExecuteTable(sql, CommandType.Text, behavior, 0);
        }

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql, int timeout)
        {
            return this.ExecuteTable(sql, CommandType.Text, CommandBehavior.Default, timeout);
        }

        /// <summary>
        /// The execute table.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        protected DataTable ExecuteTable(string sql)
        {
            return this.ExecuteTable(sql, CommandType.Text, CommandBehavior.Default, 0);
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// The execute data set.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        protected DataSet ExecuteDataSet(string sql, params string[] tableName)
        {
            return this.ExecuteDataSet(sql, CommandType.Text, tableName);
        }

        /// <summary>
        /// The execute data set.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        protected DataSet ExecuteDataSet(string sql, CommandType type, params string[] tableName)
        {
            using (IDataReader dr = this.ExecuteReader(sql, type, CommandBehavior.Default, 0))
            {
                DataSet ds = new DataSet();
                ds.Load(dr, LoadOption.Upsert, tableName);
                return ds;
            }
        }

        #endregion

        #region ExecuteScalar
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="type">type</param>
        /// <param name="timeout">timeout</param>
        /// <returns>result</returns>
        protected object ExecuteScalar(string sql, CommandType type, int timeout)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql");
            }

            this.cmd.CommandText = sql;
            this.cmd.CommandType = type;
            this.cmd.CommandTimeout = timeout;
            object result = this.cmd.ExecuteScalar();

            return result == DBNull.Value ? null : result;
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>result</returns>
        protected object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(sql, CommandType.Text, 0);
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// The execute non query.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int ExecuteNonQuery(string sql, CommandType type, int timeout)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql");
            }

            this.cmd.CommandText = sql;
            this.cmd.CommandType = type;
            this.cmd.CommandTimeout = timeout;
            var result = this.cmd.ExecuteNonQuery();

            return result;
        }

        /// <summary>
        /// The execute non query.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int ExecuteNonQuery(string sql, CommandType type)
        {
            return this.ExecuteNonQuery(sql, type, 0);
        }

        /// <summary>
        /// The execute non query.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int ExecuteNonQuery(string sql, int timeout)
        {
            return this.ExecuteNonQuery(sql, CommandType.Text, timeout);
        }

        /// <summary>
        /// The execute non query.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int ExecuteNonQuery(string sql)
        {
            return this.ExecuteNonQuery(sql, CommandType.Text, 0);
        }
        #endregion

        #region CreateParameter
        /// <summary>
        /// The create parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        private IDbDataParameter CreateParameter(string name)
        {
            IDbDataParameter param = this.cmd.CreateParameter();
            param.ParameterName = name;
            return param;
        }

        /// <summary>
        /// The create parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        private IDbDataParameter CreateParameter(string name, object value)
        {
            IDbDataParameter param = this.CreateParameter(name);
            param.Value = value ?? DBNull.Value;
            return param;
        }

        /// <summary>
        /// The create parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        private IDbDataParameter CreateParameter(string name, object value, DbType type)
        {
            IDbDataParameter param = this.CreateParameter(name, value);
            param.DbType = type;
            return param;
        }

        /// <summary>
        /// The create parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        private IDbDataParameter CreateParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            IDbDataParameter param = this.CreateParameter(name, value, type);
            param.Direction = direction;
            return param;
        }

        /// <summary>
        /// The create parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        private IDbDataParameter CreateParameter(string name, object value, DbType type, ParameterDirection direction, int size)
        {
            IDbDataParameter param = this.CreateParameter(name, value, type, direction);
            param.Size = size;
            return param;
        }

        /// <summary>
        /// The create parameter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="scale">
        /// The scale.
        /// </param>
        /// <returns>
        /// The <see cref="IDbDataParameter"/>.
        /// </returns>
        private IDbDataParameter CreateParameter(string name, object value, DbType type, ParameterDirection direction, int size, byte scale)
        {
            IDbDataParameter param = this.CreateParameter(name, value, type, direction, size);
            param.Scale = scale;
            return param;
        }
        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (this.cmd != null)
            {
                this.cmd.Dispose();
                this.cmd = null;
            }

            if (this.connection == null)
            {
                return;
            }

            this.connection.Close();
            this.connection.Dispose();
            this.connection = null;
        }
    }
}
