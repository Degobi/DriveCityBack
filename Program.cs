using DriveOfCity.Infra;
using DriveOfCity.IServices.IEmailService;
using DriveOfCity.IServices.IEmpresaService;
using DriveOfCity.IServices.IPagamentoService;
using DriveOfCity.IServices.IUsuarioService;
using DriveOfCity.IServices.IVeiculoService;
using DriveOfCity.Services.EmailService;
using DriveOfCity.Services.EmpresaService;
using DriveOfCity.Services.PagamentoService;
using DriveOfCity.Services.UsuarioService;
using DriveOfCity.Services.VeiculoService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ContextDataBase>(builder.Configuration["ConnectionString:DriveOC"]);

builder.Services.AddControllers();

#region USUARIO ============================
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
#endregion

#region EMPRESA ============================
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
#endregion

#region VEICULO ============================
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
#endregion

#region PAGAMENTO ==========================
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
#endregion

#region EMAIL ==============================
builder.Services.AddScoped<IEmailService, SendEmailService>();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddCors();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
