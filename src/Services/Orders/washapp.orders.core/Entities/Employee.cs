namespace washapp.orders.core.Entities
{
    public class Employee
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Login { get; }

        public Employee(Guid id, string firstName, string lastName, string login)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
        }
    }
}
