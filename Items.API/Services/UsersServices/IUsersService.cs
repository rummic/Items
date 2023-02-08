namespace Items.API.Services.UsersServices
{
    public interface IUsersService
    {
        public string Authenticate(string role);
    }
}