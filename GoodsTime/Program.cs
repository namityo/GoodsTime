using GoodsTime.Context;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

// Build DataBase
var cs = $"Data Source=db.sqlite;Version=3;";
using (var connection = new SQLiteConnection(cs))
{
    connection.Open();
    var sqlGoodsTable = """
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
    new SQLiteCommand(sqlGoodsTable, connection).ExecuteNonQuery();

    var sqlStocktakingEventTable = """
        create table if not exists StocktakingEvent (
            Id integer primary key autoincrement,
            Name text,
            CreatedAt text not null
            )
        """;
    new SQLiteCommand(sqlStocktakingEventTable, connection).ExecuteNonQuery();

	var sqlStocktakingGoodsEventTable = """
        create table if not exists StocktakingGoodsEvent (
            StocktakingId integer not null,
            GoodsId integer not null,
            primary key ( StocktakingId, GoodsId )
            )
        """;
	new SQLiteCommand(sqlStocktakingGoodsEventTable, connection).ExecuteNonQuery();
}



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<GoodsStore>();
builder.Services.AddScoped<StocktakingEventStore>();
builder.Services.AddScoped<StocktakingGoodsEventStore>();

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
