using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;

namespace ShareMoney
{
    public class DatabaseQueries
    {

        public static OrmLiteConnectionFactory ConnectToMysqlDatabase()
        {
            var connectionString = ConfigurationHelper.ConnString();

            var dbFactory = new OrmLiteConnectionFactory(
                            connectionString, MySqlDialect.Provider);

            return dbFactory;
        }

        public static void InsertGroupRow(Group g)
        {
            var dbFactory = ConnectToMysqlDatabase();

            using (var db = dbFactory.Open())
            {
                try
                {
                    db.Insert(g);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static void InsertUserRow(User user)
        {
            var dbFactory = ConnectToMysqlDatabase();

            using (var db = dbFactory.Open())
            {
                try
                {
                    db.Insert(user);
                }
                finally
                {
                    db.Close();
                }
            }
        }

    }
}
