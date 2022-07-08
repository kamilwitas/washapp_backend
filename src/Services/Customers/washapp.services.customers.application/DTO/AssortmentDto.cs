namespace washapp.services.customers.application.DTO;

public class AssortmentDto
{
    public Guid AssortmentId { get; set; }
    public string AssortmentName { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public string MeasurementUnit { get; set; }
    public string WeightUnit { get; set; }
    public string AssortmentCategoryName { get; set; }
    public Guid CategoryId { get; set; }

    public AssortmentDto(Guid assortmentId, string assortmentName, double width, double height, double weight, string measurementUnit, string weightUnit,
        string assortmentCategoryName, Guid categoryId)
    {
        AssortmentId = assortmentId;
        AssortmentName = assortmentName;
        Width = width;
        Height = height;
        Weight = weight;
        MeasurementUnit = measurementUnit;
        WeightUnit = weightUnit;
        AssortmentCategoryName = assortmentCategoryName;
        CategoryId = categoryId;
    }
}