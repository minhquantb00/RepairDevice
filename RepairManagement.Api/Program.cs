using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepairManagement.Application.Handle.Email;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Service.Implement;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.Configuration;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using RepairManagement.Infrastructure.DataAccess;
using RepairManagement.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(x => x.AddPolicy("corsGlobalPolicy", builder =>
{
    builder.WithOrigins("http://localhost:8080", "http://localhost:8081", "https://localhost:8081", "https://localhost:8080", "http://localhost:5173", "http://localhost:5174", "http://localhost:7027")
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
}));
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(5);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

//Add Email Configs
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IDbContext, AppDbContext>();
builder.Services.AddScoped<NguoiDungConverter>();
builder.Services.AddScoped<KhachHangConverter>();
builder.Services.AddScoped<DanhGiaDichVuConverter>();
builder.Services.AddScoped<DichVuConverter>();
builder.Services.AddScoped<DatLichConverter>();
builder.Services.AddScoped<LoaiThietBiConverter>();
builder.Services.AddScoped<ThietBiConverter>();
builder.Services.AddScoped<LichSuSuaChuaConverter>();
builder.Services.AddScoped<ThietBiSuaChuaConverter>();
builder.Services.AddScoped<LichSuTichDiemConverter>();
builder.Services.AddScoped<LinhKienConverter>();
builder.Services.AddScoped<LinhKienSuaChuaConverter>();
builder.Services.AddScoped<PhanCongCongViecConverter>();
builder.Services.AddScoped<XuatNhapKhoConverter>();

builder.Services.AddScoped<IRepository<KhachHang>, Repository<KhachHang>>();
builder.Services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
builder.Services.AddScoped<IRepository<NguoiDung>, Repository<NguoiDung>>();
builder.Services.AddScoped<IRepository<Role>, Repository<Role>>();
builder.Services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();
builder.Services.AddScoped<IRepository<ConfirmEmail>, Repository<ConfirmEmail>>();
builder.Services.AddScoped<IRepository<DichVu>, Repository<DichVu>>();
builder.Services.AddScoped<IRepository<DanhGiaDichVu>, Repository<DanhGiaDichVu>>();
builder.Services.AddScoped<IRepository<DatLichSuaChua>, Repository<DatLichSuaChua>>();
builder.Services.AddScoped<IRepository<ThietBi>,  Repository<ThietBi>>();
builder.Services.AddScoped<IRepository<LoaiThietBi>, Repository<LoaiThietBi>>();
builder.Services.AddScoped<IRepository<ThietBiSuaChua>, Repository<ThietBiSuaChua>>();
builder.Services.AddScoped<IRepository<LichSuTichDiem>, Repository<LichSuTichDiem>>();
builder.Services.AddScoped<IRepository<LichSuSuaChua>, Repository<LichSuSuaChua>>();
builder.Services.AddScoped<IRepository<LinhKien>, Repository<LinhKien>>();
builder.Services.AddScoped<IRepository<LinhKienSuaChuaThietBi>, Repository<LinhKienSuaChuaThietBi>>();
builder.Services.AddScoped<IRepository<PhanCongCongViec>, Repository<PhanCongCongViec>>();
builder.Services.AddScoped<IRepository<HangTonKho>, Repository<HangTonKho>>();
builder.Services.AddScoped<IRepository<XuatNhapKho>, Repository<XuatNhapKho>>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDichVuService, DichVuService>();
builder.Services.AddScoped<IDatLichService, DatLichService>();
builder.Services.AddScoped<IThietBiService, ThietBiService>();
builder.Services.AddScoped<ILoaiThietBiService, LoaiThietBiService>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
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

app.UseCors("corsGlobalPolicy");

app.MapControllers();

app.Run();
