using RPGGame.AutoMapper;
using RPGGame.Services.CharacterService;
using Microsoft.EntityFrameworkCore;
using RPGGame.Database;
using RPGGame.Repositories.BaseReppository;
using RPGGame.Models;
using RPGGame.Services.UserService;
using Microsoft.AspNetCore.Authentication.Cookies;
using RPGGame.Repositories.UserRepository;
using RPGGame.Services.WeaponService;
using RPGGame.Repositories.WeaponRepository;
using RPGGame.Services.SkillService;
using RPGGame.Services.SkillSerivce;
using RPGGame.Repositories.CharacterRepository;
using RPGGame.Services.FightsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(x =>
{
    x.AddProfile<DomainProfile>();
});

builder.Services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Development"));
});
builder.Services.AddTransient<IDataContext, DataContext>();

builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IWeaponService, WeaponService>();
builder.Services.AddTransient<ISkillService, SkillService>();
builder.Services.AddTransient<IFightsService, FightsService>();

builder.Services.AddTransient<IRepositoryBase<User>, RepositoryBase<User>>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IWeaponRepository, WeaponRepository>();
builder.Services.AddTransient<IRepositoryBase<Skill>, RepositoryBase<Skill>>();
builder.Services.AddTransient<ICharacterRepository, CharacterRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.ExpireTimeSpan = TimeSpan.FromDays(7);
        opts.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    if (serviceScope != null)
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<IDataContext>();
        context.Database.Migrate();
        context.Seed();
    }
}

app.Run();
