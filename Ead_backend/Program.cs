/*Developed:it2204648 Nethu nipuna m 
 * Function: Shedule Management
 * FileName:Program.cs
 * Usage: BackEndApi
 */
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Ead_backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure the services for the application
builder.Services.AddControllers(); // Add controller services for handling HTTP requests
builder.Services.AddEndpointsApiExplorer(); // Add API endpoint explorer for documentation
builder.Services.AddSwaggerGen(); // Add Swagger documentation generation

// Load configuration settings from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("MongoDB"); // Get the MongoDB connection string

// Register the MongoDB client with a singleton lifetime
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

// Define a function to configure the MongoDB database using the IMongoClient service
IMongoDatabase ConfigureMongoDatabase(IServiceProvider serviceProvider)
{
    var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
    return mongoClient.GetDatabase("TrainSchedule");
}

// Register the ConfigureMongoDatabase function with a singleton lifetime
builder.Services.AddSingleton(ConfigureMongoDatabase);

// Register the TrainScheduleRepository with a scoped lifetime
builder.Services.AddScoped<TrainScheduleRepository>();

// Configure Cross-Origin Resource Sharing (CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Allow requests from http://localhost:3000
               .AllowAnyHeader() // Allow any HTTP headers in the request
               .AllowAnyMethod(); // Allow any HTTP methods (e.g., GET, POST, PUT, DELETE)
    });
});

// Build the application
var app = builder.Build();

// Enable Swagger documentation and UI in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseCors("AllowLocalhost"); // Use the CORS policy defined above
app.UseAuthorization(); // Enable authorization handling
app.MapControllers(); // Map HTTP routes to controller actions
app.Run(); // Start the application
