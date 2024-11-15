using GestionTriage.Data;
using GestionTriage.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ITriageService, TriageService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IReglaAsignacionService, ReglaAsignacionService>();
builder.Services.AddScoped<INivelTriageService, NivelTriageService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();