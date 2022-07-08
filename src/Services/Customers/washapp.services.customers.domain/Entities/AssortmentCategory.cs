using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using washapp.services.customers.domain.Exceptions;

namespace washapp.services.customers.domain.Entities
{
    public class AssortmentCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        
        public AssortmentCategory() {}
        public AssortmentCategory(string categoryName)
        {
            ValidCategory(categoryName);
            this.CategoryName = categoryName;
        }

        public static AssortmentCategory Create(string categoryName)
        {
            AssortmentCategory category = new AssortmentCategory(categoryName);
            return category;
        }

        public void Update(string categoryName)
        {
            ValidCategory(categoryName);
            this.CategoryName = categoryName;
        }

        private void ValidCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new InvalidCategoryNameException();
            }
        }

    }


}
