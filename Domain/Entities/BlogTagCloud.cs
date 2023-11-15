namespace Domain.Entities;

public class BlogTagCloud
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
    public int TagId { get; set; }
    public virtual Tag Tag { get; set; }
}
