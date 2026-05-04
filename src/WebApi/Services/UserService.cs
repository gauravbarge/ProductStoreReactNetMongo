using WebApi.Models;
using MongoDB.Driver;

namespace WebApi.Service;

public class UserService: WebApi.Interface.IUserService
{
    public IMongoDatabase database {get; set;}
    public string usersCollection {get; set;}

    public UserService(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];
        usersCollection = configuration["MongoDb:UsersCollection"]!;

        var client = new MongoClient(connectionString);
        database = client.GetDatabase(databaseName);   
        
    }
    public IEnumerable<User> GetUsers()
    {
        return database.GetCollection<User>(usersCollection).Aggregate().Limit(10).ToList();
    }
    
}