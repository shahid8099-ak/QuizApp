using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Enteties;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

if (!dbContext.Questions.Any())
{
    var question1Answer =Guid.NewGuid();
    var question1 = new Question()
    {
        Text = "What is the capital of new zealand?",
        Options = new List<Option>
        {
             new()
            {
                Id = Guid.NewGuid(),
                Text = "Auckland",
                QuestionId = Guid.NewGuid()// Assign a valid QuestionId  
            },
            new()
            {
                Id = question1Answer,
                Text = "Wellinton",
                QuestionId = Guid.NewGuid()// Assign a valid QuestionId  
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Christchurch", // Added missing Text property  
                QuestionId = Guid.NewGuid() // Assign a valid QuestionId  
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Queenton", // Added missing Text property  
                QuestionId = Guid.NewGuid()// Assign a valid QuestionId  
            }
        },
        CorrectOption = question1Answer
    };

    var question2Answer = Guid.NewGuid();
    var question2 = new Question()
    {
        Text = "What is the capital of India?",
        Options = new List<Option>
        {
             new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Mumbai",
                QuestionId = Guid.NewGuid() // Assign a valid QuestionId  
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Hydrabad",
                QuestionId = Guid.NewGuid() // Assign a valid QuestionId  
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Goa", // Added missing Text property  
                QuestionId = Guid.NewGuid() // Assign a valid QuestionId  
            },
            new()
            {
                Id = question2Answer,
                Text = "Delhi", // Added missing Text property  
                QuestionId = Guid.NewGuid() // Assign a valid QuestionId  
            }
        },
        CorrectOption = question2Answer
    };

    dbContext.Questions.AddRange([question1, question2]);
    dbContext.SaveChanges();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Quiz}/{action=Index}/{id?}");

app.Run();
