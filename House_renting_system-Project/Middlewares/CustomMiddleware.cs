namespace House_renting_system_Project.Middlewares;

using House_Renting_System_Project.Services.Contracts;

public class CustomMiddleware(RequestDelegate next)
    {
	private readonly RequestDelegate next = next;

        public async Task InvokeAsync(
		HttpContext httpContext,
		IStatisticsService service)
	{
		service.RegisterRequest();
		await next(httpContext);
	}
}
