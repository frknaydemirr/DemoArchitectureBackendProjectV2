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

//þu an dependency yapýmýzý dependency ýnjectiþon autofac etkisiyle yaptýk!
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModel()));






// Add services to the container.
builder.Services.AddControllers();




//web tarayýcýsýndan gelen http isteklerinin izin olup olmadýðýný sorgulayan kod bloðu!

//Site bazlý izin vermek istiyorsan burasý kullanýlmalý:
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowOrigin",
//        builder => builder.WithOrigins("https://localhost:4200", "yeni site2"));
//});


//eðer tüm istekleri karþýlamak istiyorsak!
builder.Services.AddCors(options =>
options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


//JWT AYARI:

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{


    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true, //token deðerini kimler kontrol etmeli
        ValidateIssuer = true, //token deðerini kimden aldýðýný belirteyim mi
        ValidateLifetime = false, //token süresi ayarý -> iptal vs. -> süresi bitse bile kullanabiliyoruz ; performans açýsýndan daha iyi güvenlik açýsýndan zayýf
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero //expression süresine artý olarak bu süreyi ekler!
    };
});



builder.Services.AddDependencyResolvers(new ICoreModule[]{
    //coreMopdulke  içerisine eklediðim her türlü servisi otomatik olarak dahil etmiþ oluyoruz
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

app.UseAuthorization();  //kullanýcýnýn  tokana sahip olup olmadýðýný (giriþ yetkisi) olup olmadýðýný 

app.UseAuthentication();

app.UseAuthorization();  //kullanýcýnýn  rolleme kontrolünü yapar!

app.MapControllers();

app.Run();
