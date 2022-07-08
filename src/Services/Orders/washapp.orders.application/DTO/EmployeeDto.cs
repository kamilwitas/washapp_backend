namespace washapp.orders.application.DTO
{
    public class EmployeeDto
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Login { get; }

        public EmployeeDto(Guid id, string firstName, string lastName, string login)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
        }
    }
}
