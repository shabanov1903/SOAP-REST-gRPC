using ClinicService.Data;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

#region Configure Kestrel

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5100, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });
    options.Listen(IPAddress.Any, 5101, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

#endregion

#region Configure Grpc

builder.Services
    .AddGrpc()
    .AddJsonTranscoding();

#endregion

#region Configure EF DBContext Service (CardStorageService Database)

builder.Services.AddDbContext<ClinicServiceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
});

#endregion

#region Configure Swagger

builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "ClinicService.Net7.xml");
    c.IncludeXmlComments(filePath);
    c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

#endregion

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.MapGrpcService<ClinicService.Services.Impl.ClinicService>().EnableGrpcWeb();
app.MapGet("/", () => "Main page work");

app.Run();
