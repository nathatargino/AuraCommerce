using AuraCommerce.Services;
using AuraCommerce.Models;
using Microsoft.AspNetCore.Mvc;

public class SalesRecordsController : Controller
{
    private readonly SalesRecordService _salesRecordService;

    public SalesRecordsController(SalesRecordService salesRecordService)
    {
        _salesRecordService = salesRecordService;
    }

    public IActionResult Index()
    {
        return View();
    }

    // Ação da Busca Simples
    public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
    {
        if (!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if (!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
        return View(result);
    }

    // Ação da Busca Agrupada
    public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
    {
        if (!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if (!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
        return View(result);
    }
}