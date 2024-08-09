/*using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.EntityFrameworkCore;
using ASP.NETCOREWEBAPICRUD;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{

    Log.Information("Starting an application");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.ConfigureSerilog();
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add the configuration to use the appsettings.json file
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    // Configure services
    builder.Services.AddDbContext<UsersDbContext>(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("UsersDB")));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp",
            policyBuilder =>
            {
                policyBuilder.WithOrigins("http://localhost:3000", "http://localhost:3001")
                             .AllowAnyHeader()
                             .AllowAnyMethod();
            });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowReactApp");
    app.UseAuthorization();
    app.MapControllers();
    app.UseDeveloperExceptionPage();
    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex, "Host terminated unexceptedly");
}
finally
{
    Log.CloseAndFlush();
}*/

/*using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ASP.NETCOREWEBAPICRUD;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.File(new JsonFormatter(), "log.txt", rollingInterval: RollingInterval.Day, // Optional: Rolling log files daily
     fileSizeLimitBytes: 10_000_000, // Optional: Limit file size to 10 MB
    rollOnFileSizeLimit: true, // Optional: Roll over when file size limit is reached
    shared: true) // Allows multiple processes to share the log file
    .WriteTo.MSSqlServer(
    connectionString: "server=MUNEEZAKHAN; database=logsDB;Trusted_Connection=True;",
    sinkOptions: new MSSqlServerSinkOptions
    {
        TableName = "Logs",
        AutoCreateSqlTable = true
    })

     .CreateLogger();
try
{
    Log.Information("This is an information message");
    Log.Warning("This is a warning message");
    Log.Error("This is an error message");


    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341") // Adjust the Seq URL as necessary
    );

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add the configuration to use the appsettings.json file
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    // Configure services
    builder.Services.AddDbContext<UsersDbContext>(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("UsersDB")));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp",
            policyBuilder =>
            {
                policyBuilder.WithOrigins("http://localhost:3000", "http://localhost:3001")
                             .AllowAnyHeader()
                             .AllowAnyMethod();
            });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowReactApp");
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}*/



using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.File(new JsonFormatter(), "log.txt", rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10_000_000,
        rollOnFileSizeLimit: true,
        shared: true)
    .WriteTo.MSSqlServer(
connectionString: "server=MUNEEZAKHAN; database=UsersDB; Trusted_Connection=True; TrustServerCertificate=True;",
        sinkOptions: new MSSqlServerSinkOptions
{
TableName = "Logs",
AutoCreateSqlTable = true
})
    .CreateLogger();


try
{
    Log.Information("This is an information message");
    Log.Warning("This is a warning message");
    Log.Error("This is an error message");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    // Configure services
    builder.Services.AddDbContext<UsersDbContext>(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("UsersDB")));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp",
            policyBuilder =>
            {
                policyBuilder.WithOrigins("http://localhost:3000", "http://localhost:3001")
                             .AllowAnyHeader()
                             .AllowAnyMethod();
            });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowReactApp");
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
