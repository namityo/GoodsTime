using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

// Build DataBase
var cs = $"Data Source=db.sqlite;Version=3;";
using (var connection = new SQLiteConnection(cs))
{
    connection.Open();
    var sql = """
        create table if not exists Goods (
            Id integer primary key autoincrement,
            Number text,
            Description text,
            GetDate text,
            ReleaseDate text,
            ReleaseFlag integer default 0 not null,
            ReleasedDate text,
            ReleaseDescription text,
            RegisterDate text not null,
            UpdateDate text not null,
            UpdateId text not null
            )
        """;
    var com = new SQLiteCommand(sql, connection);
    com.ExecuteNonQuery();
}



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
