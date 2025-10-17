namespace Rasta.CityService.Domain;

public class SearchCity
{
    public string? Name { get; set; }
    public long? CityId { get; set; }
    public required long ProvinceId { get; set; }
}
