using System;
using System.Data;
using System.Data.SQLite;

namespace NapilnikTask27
{
    internal class SQLiteDataAdapter
    {
        private SQLiteConnection sQLiteConnection;
        private SQLiteConnection connection;
        private SQLiteCommand sQLiteCommand;

        public SQLiteDataAdapter(SQLiteCommand sQLiteCommand)
        {
            this.sQLiteCommand = sQLiteCommand;
        }

        public SQLiteDataAdapter(SQLiteConnection sQLiteConnection, SQLiteConnection connection) 
        {
            this.connection = connection;
        }

        internal void Fill(DataTable dataTable2)
        {
            throw new NotImplementedException();
        }
    }
}