using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolver.Autofac;
using Core.DependencyResolves;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//�u an dependency yap�m�z� dependency �njecti�on autofac etkisiyle yapt�k!
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModel()));






// Add services to the container.
builder.Services.AddControllers();




//web taray�c�s�ndan gelen http isteklerinin izin olup olmad���n� sorgulayan kod blo�u!

//Site bazl� izin vermek istiyorsan buras� kullan�lmal�:
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowOrigin",
//        builder => builder.WithOrigins("https://localhost:4200", "yeni site2"));
//});


//e�er t�m istekleri kar��lamak istiyorsak!
builder.Services.AddCors(options =>
options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


//JWT AYARI:

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{


    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true, //token de�erini kimler kontrol etmeli
        ValidateIssuer = true, //token de�erini kimden ald���n� belirteyim mi
        ValidateLifetime = false, //token s�resi ayar� -> iptal vs. -> s�resi bitse bile kullanabiliyoruz ; performans a��s�ndan daha iyi g�venlik a��s�ndan zay�f
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero //expression s�resine art� olarak bu s�reyi ekler!
    };
});



builder.Services.AddDependencyResolvers(new ICoreModule[]{
    //coreMopdulke  i�erisine ekledi�im her t�rl� servisi otomatik olarak dahil etmi� oluyoruz
    new CoreModule()
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfiguureCustomExceptionMiddleware();


app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();  //kullan�c�n�n  tokana sahip olup olmad���n� (giri� yetkisi) olup olmad���n� 

app.UseAuthentication();

app.UseAuthorization();  //kullan�c�n�n  rolleme kontrol�n� yapar!

app.MapControllers();

app.Run();
