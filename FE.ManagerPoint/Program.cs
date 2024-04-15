using BLL.Extentions.Automapper;
using BLL.Services.Implement;
using BLL.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// F5 Reload lại giao diện khi Dev
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddTransient<IClassServices, ClassServices>();
builder.Services.AddAutoMapper(typeof(Class_Mapping).Assembly);
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

app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();