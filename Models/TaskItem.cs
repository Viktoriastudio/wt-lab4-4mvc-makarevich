namespace wt_lab4_4mvc_makarevich.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string Assignee { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public bool IsCompleted { get; set; }
}