using WhoruWebSocketServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var allowPolicy = "AllowAll";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(allowPolicy, p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowPolicy,
        configure => configure.WithOrigins("ws://localhost:13875")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
});
builder.Services.AddSignalR(hubOptions => { hubOptions.EnableDetailedErrors = true; hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(10); hubOptions.HandshakeTimeout = TimeSpan.FromSeconds(5); });
var app = builder.Build();
//app.UseCors(allowPolicy);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(allowPolicy);
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
