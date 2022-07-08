using washapp.orders.core.Entities;
using washapp.orders.core.Enums;
using Convey.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace washapp.orders.infrastructure.Mongo.Documents
{
    public class AssortmentDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string AssortmentName { get; set; }
        public double Weight { get; set; }
        public WeightUnit WeightUnit { get; set; }

        public AssortmentDocument(Guid id, string assortmentName, double weight, WeightUnit weightUnit)
        {
            Id = id;
            AssortmentName = assortmentName;
            Weight = weight;
            WeightUnit = weightUnit;
        }
    }
}
