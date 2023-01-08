using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Data.SQLite;

namespace GoodsTime.Context
{
    public class StoreBase
    {
        public string ConnectionString { get; set; } = $"Data Source=db.sqlite;Version=3;";

        public ILogger? Logger { get; set; }

        protected IDbConnection Connection => new SQLiteConnection(ConnectionString);

        protected Compiler Compiler => new SqliteCompiler();

        protected QueryFactory QueryFactory
        {
            get
            {
                var db = new QueryFactory(Connection, Compiler)
                {
                    Logger = compiled =>
                    {
                        var message = compiled.ToString();
                        if (Logger != null)
                        {
                            Logger.LogInformation(message);
                        }
                        else
                        {
                            Console.WriteLine(message);
                        }
                    }
                };
                return db;
            }
        }
    }
}
