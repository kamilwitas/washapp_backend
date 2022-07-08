using System.ComponentModel.DataAnnotations.Schema;
using washapp.services.customers.domain.Enums;
using washapp.services.customers.domain.Events;
using washapp.services.customers.domain.Exceptions;

namespace washapp.services.customers.domain.Entities
{
    public class Assortment
    {
        public Guid Id { get; set; }
        public string AssortmentName { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }
        
        public WeightUnit WeightUnit { get; set; }
        public AssortmentCategory AssortmentCategory { get; set; }
        
        [NotMapped]
        public List<IDomainEvent>Events { get; set; }

        public Assortment() {}
        public Assortment(string assortmentName, double width, double height, double weight, 
            AssortmentCategory category, MeasurementUnit measurementUnit, WeightUnit weightUnit)
        {
            if (string.IsNullOrWhiteSpace(assortmentName))
            {
                throw new AssortmentNameCannotBeEmptyException();
            }
            
            AssortmentName = assortmentName;
            Width = width;
            Height = height;
            Weight = weight;
            MeasurementUnit = measurementUnit;
            WeightUnit = weightUnit;
            if (category is null)
            {
                throw new CategoryCannotBeNullException();
            }
            
            AssortmentCategory = category;

        }

        public static Assortment Create(string assortmentName, double width, double height, double weight,
            AssortmentCategory category, MeasurementUnit measurementUnit, WeightUnit weightUnit)
        {
            Assortment assortment = new Assortment(assortmentName, width, height, weight, 
                category,measurementUnit,weightUnit);
            return assortment;
        }

        public void Update(Assortment assortment)
        {
            if (string.IsNullOrWhiteSpace(assortment.AssortmentName))
            {
                throw new AssortmentNameCannotBeEmptyException();
            }
            
            AssortmentName = assortment.AssortmentName;
            AssortmentCategory = assortment.AssortmentCategory;
            Width = assortment.Width;
            Weight = assortment.Weight;
            Height = assortment.Height;
            MeasurementUnit = assortment.MeasurementUnit;
            WeightUnit = assortment.WeightUnit;
            
            AddEvent(new UpdatedAssortment(this));

        }

        private void AddEvent(IDomainEvent @event)
        {
            if (Events is null)
            {
                Events = new List<IDomainEvent>();
            }
            Events.Add(@event);
        }
        
        
    }
}
