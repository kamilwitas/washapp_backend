namespace washapp.orders.core.Entities;

public class Customer 
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string CustomerColor { get; set; }
    public Guid LocationId { get; set; }
    public List<Assortment>Assortments { get; set; }
    public List<Guid> Orders { get; set; }
    public bool IsDeleted { get; set; }

    public Customer()
    {
        
    }
    public Customer(Guid id, string companyName, string customerColor, Guid locationId, 
        List<Assortment> assortments,List<Guid>? orders =null, bool isDeleted=false)
    {
        Id = id;
        CompanyName = companyName;
        CustomerColor = customerColor;
        LocationId = locationId;
        Assortments = assortments;
        Orders = orders is null ? new List<Guid>() : orders;
        IsDeleted = isDeleted;
    }

    public void AddOrder(Guid orderId)
    {
        Orders.Add(orderId);
    }

    public void DeleteOrder(Guid orderId)
    {
        Orders.Remove(orderId);
    }

    public void Update(string companyName, string customerColor, Guid locationId, List<Assortment> assortments)
    {
        CompanyName = companyName;
        CustomerColor = customerColor;
        LocationId = locationId;
        Assortments = assortments;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}