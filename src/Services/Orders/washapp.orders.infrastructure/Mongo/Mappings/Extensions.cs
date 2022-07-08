using washapp.orders.application.DTO;
using washapp.orders.core.Entities;
using washapp.orders.core.Value_objects;
using washapp.orders.core.Value_objects.OrderActions;
using washapp.orders.infrastructure.Mongo.Documents;

namespace washapp.orders.infrastructure.Mongo.Mappings
{
    public static class Extensions
    {
        public static AssortmentDocument AsDocument(this Assortment entity)
        {
            return new AssortmentDocument(entity.Id, entity.AssortmentName, entity.Weight, entity.WeightUnit);
        }
        public static Assortment AsBusiness(this AssortmentDocument document)
        {
            return new Assortment(document.Id, document.AssortmentName, document.Weight, document.WeightUnit);
        }

        public static OrderLineDocument AsDocument(this OrderLine entity)
        {
            return new OrderLineDocument(entity.Id,entity.Assortment?.AsDocument(),entity.Quantity,entity.TotalWeight,
                entity.WeightUnit,entity.IsExecuted);
        }

        public static OrderLine AsBusiness(this OrderLineDocument document)
        {
            return new OrderLine(document.Id,document.Assortment?.AsBusiness(), document.Quantity,document.TotalWeight,document.WeightUnit,
                document.IsExecuted);
        }

        public static OrderDocument AsDocument(this Order entity)
        {
            return new OrderDocument(entity.Id, entity.CustomerId, entity.CompanyName, entity.OrderLines.Select(x=>x?.AsDocument()).ToList(),
                entity.OrderState, entity.CreatedBy?.AsDocument(), entity.CreatedAt, entity.OrderActions.Select(x=>x.AsDocument()).ToList());
        }

        public static Order AsBusiness(this OrderDocument document)
        {
            return new Order(document.Id, document.CustomerId,document.CompanyName, document.OrderLines?.Select(x=>x?.AsBusiness()).ToList(),
                document.OrderState, document.CreatedBy.AsBusiness(), document.CreatedAt, document.OrderActions.Select(x=>x.AsBusiness()).ToList());
        }
        public static OrderDto AsDto(this OrderDocument document)
        {
            return new OrderDto(document.Id,document.OrderState.ToString(),document.CustomerId, document.CompanyName,
                document.CreatedAt);
        }

        public static CustomerDocument AsDocument(this Customer entity)
        {
            return new CustomerDocument(entity.Id, entity.CompanyName, entity.CustomerColor, entity.LocationId,
                entity.Assortments.Select(x => x.AsDocument()).ToList(), entity.Orders,
                entity.IsDeleted);
        }

        public static Customer AsBusiness(this CustomerDocument document)
        {
            return new Customer(document.Id,document.CompanyName,document.CustomerColor,document.LocationId,
                document.Assortments.Select(x=>x.AsBusiness()).ToList(), document.Orders,document.IsDeleted);
        }

        public static EmployeeDocument AsDocument(this Employee entity)
        {
            return new EmployeeDocument(entity.Id, entity.FirstName, entity.LastName, entity.Login);
        }

        public static Employee AsBusiness(this EmployeeDocument document)
        {
            return new Employee(document.Id,document.FirstName,document.LastName,document.Login); 
        }

        public static OrderExecutionDocument AsDocument(this OrderExecution entity)
        {
            return new OrderExecutionDocument(entity.Id, entity.Employee.AsDocument(), entity.ActionDateTime, entity.OrderLineId, entity.AssortmentName,
                entity.ExecutedQuantity);
        }

        public static OrderExecution AsBusiness(this OrderExecutionDocument document)
        {
            return new OrderExecution(document.Id, document.Employee.AsBusiness(), document.OrderLineId, document.AssortmentName, 
                document.ExecutedQuantity);
        }

        public static OrderActionDocument AsDocument(this OrderAction entity)
        {
            switch (entity)
            {
                case OrderExecution:
                    return new OrderExecutionDocument(
                        entity.Id, 
                        ((OrderExecution)entity).Employee.AsDocument(),
                        ((OrderExecution)entity).ActionDateTime,
                        ((OrderExecution)entity).OrderLineId,
                        ((OrderExecution)entity).AssortmentName,
                        ((OrderExecution)entity).ExecutedQuantity);
                default:
                    return null;
            }
        }

        private static OrderAction AsBusiness(this OrderActionDocument document)
        {
            switch (document)
            {
                case OrderExecutionDocument:
                    return new OrderExecution(
                        document.Id,
                        document.Employee.AsBusiness(),
                        ((OrderExecutionDocument)document).OrderLineId,
                        ((OrderExecutionDocument)document).AssortmentName,
                        ((OrderExecutionDocument)document).ExecutedQuantity);
                default:
                    return null;
            }
        }
    }
}
