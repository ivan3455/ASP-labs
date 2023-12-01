var builder = WebApplication.CreateBuilder();

builder.Services.AddTransient<CalcService>();
builder.Services.AddTransient<CurrentTimeService>();
builder.Services.AddTransient<CalcController>();
builder.Services.AddTransient<CurrentTimeController>();

var app = builder.Build();

app.Map("/calc", app =>
{
    var calcController = app.ApplicationServices.GetRequiredService<CalcController>();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/add/{a}/{b}", async context =>
        {
            double a = double.Parse(context.Request.RouteValues["a"].ToString());
            double b = double.Parse(context.Request.RouteValues["b"].ToString());
            var result = calcController.Add(a, b);

            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync($"Результат додавання: {result}");
        });

        endpoints.MapGet("/subtract/{a}/{b}", async context =>
        {
            double a = double.Parse(context.Request.RouteValues["a"].ToString());
            double b = double.Parse(context.Request.RouteValues["b"].ToString());
            var result = calcController.Subtract(a, b);

            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync($"Результат віднімання: {result}");
        });

        endpoints.MapGet("/divide/{a}/{b}", async context =>
        {
            double a = double.Parse(context.Request.RouteValues["a"].ToString());
            double b = double.Parse(context.Request.RouteValues["b"].ToString());
            var result = calcController.Divide(a, b);

            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync($"Результат ділення: {result}");
        });

        endpoints.MapGet("/multiply/{a}/{b}", async context =>
        {
            double a = double.Parse(context.Request.RouteValues["a"].ToString());
            double b = double.Parse(context.Request.RouteValues["b"].ToString());
            var result = calcController.Multiply(a, b);

            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync($"Результат множення: {result}");
        });
    });
});

app.Map("/time", app =>
{
    var timeController = app.ApplicationServices.GetRequiredService<CurrentTimeController>();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/current-daytime", async context =>
        {
            var timeOfDay = timeController.GetCurrentTimeOfDay();

            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync($"Поточний час дня: {timeOfDay}");
        });
    });
});

app.Run();