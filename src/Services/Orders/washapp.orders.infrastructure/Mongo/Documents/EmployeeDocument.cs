namespace washapp.orders.infrastructure.Mongo.Documents
{
    public class EmployeeDocument
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }

        public EmployeeDocument(Guid id, string firstName, string lastName, string login)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
        }
    }
}
