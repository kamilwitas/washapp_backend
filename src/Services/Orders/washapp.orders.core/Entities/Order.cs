using washapp.orders.core.Value_objects;
using washapp.orders.core.Enums;
using washapp.orders.core.Exceptions;
using washapp.orders.core.Value_objects.OrderActions;

namespace washapp.orders.core.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string CompanyName { get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public List<OrderAction> OrderActions { get; set; }
    public OrderState OrderState { get;  set; }
    public Employee CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public Order(Guid orderId,Guid customerId,string companyName, List<OrderLine>orderLines, 
        OrderState orderState, Employee createdBy, DateTime createdAt, List<OrderAction>orderActions=null)
    {
        Id = orderId;
        OrderLines = orderLines is null? new List<OrderLine>() : orderLines ;
        OrderState = orderState;
        CreatedAt = createdAt;
        CustomerId = customerId;
        CreatedBy = createdBy;
        CompanyName = companyName;
        OrderActions = orderActions is null ? new List<OrderAction>() : orderActions;
    } 
    public Order() {}

    public static Order Create(Guid customerId,string companyName, Employee createdBy, List<OrderLine>? orderLines = null)
    {
        var order = new Order(Guid.NewGuid(), customerId, companyName, 
            orderLines is null? new List<OrderLine>() : orderLines, 
            OrderState.New, createdBy, DateTime.UtcNow);

        return order;
    }

    public void AddOrderLine(OrderLine orderLine)
    {
        if (OrderState == OrderState.Completed || OrderState == OrderState.Pending)
        {
            throw new InvalidOrderStateToPerformActionException(this.OrderState);
        }

        if (orderLine is null)
        {
            throw new OrderLineNullException();
        }

        if (OrderLines is null)
        {
            OrderLines = new List<OrderLine>();
        }
        
        OrderLines.Add(orderLine);
    }

    public void DeleteOrderLine(OrderLine orderLine)
    {
        if (OrderState == OrderState.Completed || OrderState==OrderState.Pending)
        {
            throw new InvalidOrderStateToPerformActionException(this.OrderState);
        }
        var orderLineToDelete = OrderLines.FirstOrDefault(x => x.Id == orderLine.Id);
        if (orderLineToDelete is null)
        {
            throw new OrderLineDoesNotExistsException();
        }

        OrderLines.Remove(orderLine);
    }

    public bool IsDeletePossible()
    {
        if (OrderState == OrderState.Pending || OrderState==OrderState.Completed)
        {
            return false;
        }
        return true;
    }

    public void ChangeOrderStateToPending()
    {

        OrderState = OrderState.Pending;
    }

    public void ChangeOrderStateToCompleted()
    {
        OrderState = OrderState.Completed;
    }

    public void AddOrderExecution(OrderExecution orderExecution)
    {
        ValidateOrderExecution(orderExecution);
        OrderActions.Add(orderExecution);
        ApplyOrderExecution(orderExecution);
        ScanOrderLines();
    }

    private void CloseOrderLine(Guid orderLineId)
    {
        var orderLine = OrderLines.FirstOrDefault(x => x.Id == orderLineId);

        if (orderLine is null)
        {
            throw new OrderLineDoesNotExistsException();
        }

        orderLine.CloseOrderLine();
    }

    private void ScanOrderLines()
    {
        if (OrderState == OrderState.New)
        {
            ChangeOrderStateToPending();
        }
        var notExecutedOrderLine = OrderLines.FirstOrDefault(x => !x.IsExecuted);

        if (notExecutedOrderLine is null)
        {
            ChangeOrderStateToCompleted();
        }
    }

    private void ApplyOrderExecution(OrderExecution orderExecution)
    {
        var orderLine = OrderLines.FirstOrDefault(x => x.Id == orderExecution.OrderLineId);

        var executedQuantityOfOrderLine = OrderActions
            .Where(x => ((OrderExecution)x).OrderLineId == orderExecution.OrderLineId)
            .Sum(x => ((OrderExecution)x).ExecutedQuantity);

        if (orderLine?.Quantity-executedQuantityOfOrderLine==0)
        {
            CloseOrderLine(orderExecution.OrderLineId);
        }
    }

    private void ValidateOrderExecution(OrderExecution orderExecution)
    {
        if (OrderState == OrderState.Completed)
        {
            throw new InvalidOrderExecutionRequestException("Order is closed");
        }
        if (orderExecution.OrderLineId == Guid.Empty)
        {
            throw new InvalidOrderExecutionRequestException("Order line id cannot be empty");
        }

        var orderLine = OrderLines.FirstOrDefault(x => x.Id == orderExecution.OrderLineId);

        if (orderLine is null)
        {
            throw new 
                InvalidOrderExecutionRequestException($"Order line with id: {orderExecution.OrderLineId} does not exists");
        }

        if (orderLine.IsExecuted)
        {
            throw new InvalidOrderExecutionRequestException("Order line has been already executed");
        }

        if (orderExecution.ExecutedQuantity <= 0)
        {
            throw new InvalidOrderExecutionRequestException("Executed quantity must be greater than 0");
        }

        if (orderExecution.ExecutedQuantity>orderLine.Quantity)
        {
            throw new InvalidOrderExecutionRequestException("Executed quantity cannot be greater than order line quantity");
        }

        var otherOrderExecutions = OrderActions
            .Where(x => x is OrderExecution && ((OrderExecution)x).OrderLineId == orderExecution.OrderLineId);

        if (otherOrderExecutions.Any())
        {
            var executedQuantity = otherOrderExecutions.Sum(x=>((OrderExecution)x).ExecutedQuantity);
            var orderLineQuantity = orderLine.Quantity;

            if (executedQuantity+orderExecution.ExecutedQuantity>orderLineQuantity)
            {
                throw new InvalidOrderExecutionRequestException("Executed quantity in order execution is greater than remaining quantity");
            }
        }
        else
        {
            var remainingQuantity = orderLine.Quantity;

            if (orderExecution.ExecutedQuantity>remainingQuantity)
            {
                throw new InvalidOrderExecutionRequestException("Executed quantity in order execution is greater than remaining quantity");
            }
        }
        orderExecution.SetAssortmentName(orderLine.Assortment.AssortmentName);
    }
}