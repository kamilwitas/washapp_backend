using washapp.orders.application.Models.Pagination;
using System.ComponentModel.DataAnnotations;

namespace washapp.orders.api.Models
{
    public class GetAllPagedOrdersRequest
    {
        public string ComapnyNameSearchPhrase { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

        public GetAllPagedOrdersRequest(string companyNameSearchPhrase, int pageNumber, int pageSize, 
            string sortBy, SortDirection sortDirection)
        {
            ComapnyNameSearchPhrase = companyNameSearchPhrase;
            SortBy = sortBy;
            SortDirection = sortDirection;
        }
    }
}
