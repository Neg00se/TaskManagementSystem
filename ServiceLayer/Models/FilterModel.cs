using DataAccessLayer.Enums;

namespace ServiceLayer.Models;
public class FilterModel
{
    public DateTime? DueDate { get; set; }
    public PriorityEnum? Priority { get; set; }
    public StatusEnum? Status { get; set; }
}
