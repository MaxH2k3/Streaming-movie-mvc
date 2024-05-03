using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using SMovie.Domain.Models;

namespace SMovie.Infrastructure.DBContext;

public class SMovieMongoContext
{
    private readonly MongoClient _client;
    private static bool _isIndexCreated = false;

    public IMongoDatabase Database { get; set; }
    public IMongoCollection<UserTemporary> Users { get; set; }
    public IMongoCollection<VerifyToken> Tokens { get; set; }
    public IMongoCollection<AnalystMovie> CurrentTopMovies { get; set; }
    public IMongoCollection<AnalystMovie> PreviousTopMovies { get; set; }
    public IMongoCollection<BlackIP> BlackListIP { get; set; }
    public IMongoCollection<Notification> Notifications { get; set; }
    public IMongoCollection<string> Geminis { get; set; }

    public SMovieMongoContext()
    {
        // Create a MongoClient with the connection string
        _client = new MongoClient(GetConnectionString());

        // Access a specific database
        Database = _client.GetDatabase("Movie");

        // Access a specific collection
        Users = Database.GetCollection<UserTemporary>("User");
        Tokens = Database.GetCollection<VerifyToken>("Token");
        CurrentTopMovies = Database.GetCollection<AnalystMovie>("CurrentTopMovie");
        PreviousTopMovies = Database.GetCollection<AnalystMovie>("PreviousTopMovie");
        BlackListIP = Database.GetCollection<BlackIP>("BlackListIP");
        Notifications = Database.GetCollection<Notification>("Notification");
        Geminis = Database.GetCollection<string>("Gemini");

        //Create Index for collection
        if (!_isIndexCreated)
        {
            CreateIndex();
        }
    }

    private static string GetConnectionString()
    {
        IWebHostEnvironment environment = new HttpContextAccessor().HttpContext!.RequestServices
                                    .GetRequiredService<IWebHostEnvironment>();

        IConfiguration config = new ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json", true, true)

                .Build();

        if (environment.IsProduction())
        {
            return config["ConnectionStrings:MongoDB"]!;
        }

        return config["LocalDB:MongoDB"]!;
        


    }

    private void CreateIndex()
    {
        try
        {
            //token
            var tokensIndexKeyDefinition = Builders<VerifyToken>.IndexKeys.Ascending(token => token.ExpiredDate);
            var tokensIndexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };

            Tokens.Indexes.CreateOne(new CreateIndexModel<VerifyToken>(tokensIndexKeyDefinition, tokensIndexOptions));

            //user
            var usersIndexKeyDefinition = Builders<UserTemporary>.IndexKeys.Ascending(user => user.ExpiredDate);
            var usersIndexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };

            Users.Indexes.CreateOne(new CreateIndexModel<UserTemporary>(usersIndexKeyDefinition, usersIndexOptions));

            //notification
            var notificationIndexKeyDefinition = Builders<Notification>.IndexKeys.Ascending(notification => notification.ExpiredDate);
            var notificationIndexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };

            Notifications.Indexes.CreateOne(new CreateIndexModel<Notification>(notificationIndexKeyDefinition, notificationIndexOptions));

            //set complete config index
            _isIndexCreated = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
