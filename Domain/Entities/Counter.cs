using Domain.Entities.Base;

namespace Domain.Entities;

public class Counter:BaseEntity
{
    public int Year { get; set; }
    public string? YearText { get; set; }
    public string? YearTextAz { get; set; }
}
