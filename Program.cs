using EmployeeNavigatorV3.Repository;
using EmployeeNavigatorV3.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

//repository
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IProfessionRepository, ProfessionRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();
