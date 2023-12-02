using Newtonsoft.Json;

public class UserService
{
    private readonly IConfiguration _configuration;
    private readonly string _usersDirectory;

    // Конструктор, який отримує IConfiguration для отримання налаштувань
    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;

        // Визначення шляху до директорії з користувачами на основі поточного робочого каталогу
        _usersDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Config/Users");
    }

    public bool AuthenticateUser(int userId, string password)
    {
        var user = GetUserById(userId);

        // Перевірка, чи користувач існує та чи вірний пароль
        if (user != null && user.Pwd == password)
        {
            return true;
        }

        return false;
    }

    public User GetUserById(int id)
    {
        // Формування шляху до файлу з інформацією про користувача за його ID
        var filePath = Path.Combine(_usersDirectory, $"user_{id}.json");
        if (File.Exists(filePath))
        {
            // Читання вмісту файлу та десеріалізація JSON в об'єкт користувача
            return JsonConvert.DeserializeObject<User>(File.ReadAllText(filePath));
        }

        return null;
    }
}
