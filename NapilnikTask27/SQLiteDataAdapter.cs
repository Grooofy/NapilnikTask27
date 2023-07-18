using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace NapilnikTask27
{
    internal class SQLiteDataAdapter
    {
        private SqliteCommand sqliteCommand;

        public SQLiteDataAdapter(SqliteCommand sqliteCommand)
        {
            this.sqliteCommand = sqliteCommand;
        }

        internal void Fill(DataTable dataTable2)
        {
            throw new NotImplementedException();
        }
    }
}