using DataAccessLayer.Enums;

namespace ServiceLayer.Models;
public class TaskModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public PriorityEnum Priority { get; set; }

    public DateTime? DueDate { get; set; }


    public StatusEnum Status { get; set; } = StatusEnum.Pending;

    public Guid UserId { get; set; }
}
