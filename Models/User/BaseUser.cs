namespace mHealthProject.Models.User;

public class BaseUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserTypeEnum UserType { get; set; }
}