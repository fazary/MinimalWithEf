namespace MinimalWithEf.Models;
public class Todo : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}