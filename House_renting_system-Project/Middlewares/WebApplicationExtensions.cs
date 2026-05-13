namespace House_renting_system_Project.Middlewares;

	public static class CustomMiddlewareExtensions
	{
    public static IApplicationBuilder UseStatistics(
        this IApplicationBuilder builder)
        => builder.UseMiddleware<CustomMiddleware>();
}
