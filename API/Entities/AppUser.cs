namespace API.Entities;

//Represents the user of the application, we will use this class to create a table in our database using Entity Framework Core. We will also use this class to create a user in our database and to authenticate the user when they log in.
public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}
