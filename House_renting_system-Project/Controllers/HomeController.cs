namespace House_renting_system_Project.Controllers;

using House_Renting_System_Project.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

public class HomeController(IStatisticsService service) : Controller
{
    public IActionResult Index()
    {
		this.ViewBag.TotalRequests = service.TotalRequests;
		return this.View();
    }

	public IActionResult Error(int statusCode)
	{
		if (statusCode == 401)
		{
			return this.View("Unauthorized");
		}

		return this.View("NotFound");
	}

	public IActionResult Crash()
		=> throw new Exception("Test exception");

	public IActionResult ServerError()
		=> this.View();
}
