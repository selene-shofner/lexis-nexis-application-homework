using Microsoft.EntityFrameworkCore;

using RecipeBook.Repository;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile(

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RecipeBookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RecipeBookContext"));

#if DEBUG
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddScoped<IRecipeBookRepository, RecipeBookRepository>();

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<RecipeBook.Models.SearchTerms, RecipeBook.Repository.SearchTerms>();

    config.CreateMap<RecipeBook.Repository.Entities.Recipe, RecipeBook.Models.Recipe>();
    config.CreateMap<RecipeBook.Repository.Entities.RecipeIngredient, RecipeBook.Models.Ingredient>();
    config.CreateMap<RecipeBook.Repository.Entities.QuantityUnit, RecipeBook.Models.QuantityUnit>();


    config.CreateMap<RecipeBook.Models.Recipe, RecipeBook.Repository.Entities.Recipe>();
    config.CreateMap<RecipeBook.Models.Ingredient, RecipeBook.Repository.Entities.RecipeIngredient>();
    config.CreateMap<RecipeBook.Models.QuantityUnit, RecipeBook.Repository.Entities.QuantityUnit>();
});



var app = builder.Build();

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
    pattern: "{controller=Recipe}/{action=Index}/{id?}");

app.Run();
