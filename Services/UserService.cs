using Newtonsoft.Json;

public class UserService
{
    private readonly IConfiguration _configuration;
    private readonly string _usersDirectory;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
        _usersDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Config/Users");
    }

    public bool AuthenticateUser(int userId, string password)
    {
        var user = GetUserById(userId);

        if (user != null && user.Pwd == password)
        {
            return true;
        }

        return false;
    }

    public User GetUserById(int id)
    {
        var filePath = Path.Combine(_usersDirectory, $"user_{id}.json");
        if (File.Exists(filePath))
        {
            return JsonConvert.DeserializeObject<User>(File.ReadAllText(filePath));
        }

        return null;
    }
}
