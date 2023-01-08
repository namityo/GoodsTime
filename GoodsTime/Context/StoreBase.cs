using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Data.SQLite;

namespace GoodsTime.Context
{
    public class StoreBase
    {
        public string ConnectionString { get; set; } = $"Data Source=db.sqlite;Version=3;";

        protected IDbConnection Connection => new SQLiteConnection(ConnectionString);

        protected Compiler Compiler => new SqliteCompiler();

        protected QueryFactory QueryFactory
        {
            get
            {
                var db = new QueryFactory(Connection, Compiler);
                db.Logger = compiled =>
                {
                    // TODO ILoggerで出力したい
                    Console.WriteLine(compiled.ToString());
                };
                return db;
            }
        }
    }
}
