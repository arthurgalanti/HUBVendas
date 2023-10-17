using HUBVendas.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureMvc();
builder.ConfigureServices();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction()) {
    app.UseHttpsRedirection();
}
app.MapControllers();
app.Run();
