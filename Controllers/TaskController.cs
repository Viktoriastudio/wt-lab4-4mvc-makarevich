using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_makarevich.Models;

namespace wt_lab4_4mvc_makarevich.Controllers;

public class TaskController : Controller
{
    private static readonly List<TaskItem> Tasks = new()
    {
        new TaskItem { Id = 1, Title = "Сбор требований", Project = "CRM для банка", Assignee = "Ольга Смирнова", EstimatedHours = 16, IsCompleted = true },
        new TaskItem { Id = 2, Title = "Проектирование БД", Project = "CRM для банка", Assignee = "Иван Петров", EstimatedHours = 24, IsCompleted = true },
        new TaskItem { Id = 3, Title = "Разработка API", Project = "CRM для банка", Assignee = "Иван Петров", EstimatedHours = 40, IsCompleted = false },
        new TaskItem { Id = 4, Title = "Тестирование", Project = "CRM для банка", Assignee = "Пётр Иванов", EstimatedHours = 20, IsCompleted = false },
        new TaskItem { Id = 5, Title = "Деплой", Project = "Мобильное приложение", Assignee = "Дмитрий Козлов", EstimatedHours = 8, IsCompleted = false }
    };

    public IActionResult Index()
    {
        return View(Tasks);
    }

    public IActionResult Details(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
            return NotFound();
        return View(task);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        if (!ModelState.IsValid)
            return View(task);

        task.Id = Tasks.Any() ? Tasks.Max(t => t.Id) + 1 : 1;
        Tasks.Add(task);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
            return NotFound();
        return View(task);
    }

    [HttpPost]
    public IActionResult Edit(TaskItem task)
    {
        var existing = Tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing == null)
            return NotFound();

        existing.Title = task.Title;
        existing.Project = task.Project;
        existing.Assignee = task.Assignee;
        existing.EstimatedHours = task.EstimatedHours;
        existing.IsCompleted = task.IsCompleted;

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
            Tasks.Remove(task);

        return RedirectToAction("Index");
    }

    // Дополнительная операция: поиск по названию
    public IActionResult Search(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return View("Index", Tasks);

        var result = Tasks
            .Where(t => t.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();

        ViewData["SearchText"] = text;
        return View("Index", result);
    }
}