namespace washapp.orders.application.DTO
{
    public class UserPrincipal
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }

        public UserPrincipal(Guid userId, string firstName, string lastName, string login)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
        }
    }
}
