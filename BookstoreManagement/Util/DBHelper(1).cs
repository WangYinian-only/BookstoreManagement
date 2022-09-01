using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace BookstoreManagement
{
    /// <summary>
    /// ����ά�����ݿ������ַ������� Connection ����
    /// </summary>
  public class DBHelper
    {
        // ���ݿ������ַ���
      private string connString = @"Database=bookdb;Data Source=127.0.0.1;User Id=root;Password=123456;pooling=false;CharSet=utf8;port=3306";

        // ���ݿ����� Connection ����
        private MySqlConnection connection;

        /// <summary>
        /// Connection����
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new MySqlConnection(connString);
                }
                return connection;
            }            
        }

        /// <summary>
        /// �����ݿ�����
        /// </summary>
        public void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            else if (Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
                Connection.Open();
            }
        }

        /// <summary>
        /// �ر����ݿ�����
        /// </summary>
        public void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
            }
        }
    }
}
