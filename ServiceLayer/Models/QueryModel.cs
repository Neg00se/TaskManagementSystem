using DataAccessLayer.Enums;

namespace ServiceLayer.Models;
public class QueryModel
{
    public DateTime? DueDate { get; set; } = null;

    public PriorityEnum? Priority { get; set; } = null;

    public StatusEnum? Status { get; set; } = null;

    public string? SortBy { get; set; } = null;

    public bool IsDescending { get; set; } = false;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}
