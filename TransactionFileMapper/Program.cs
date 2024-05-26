using Quartz;
using TransactionFileMapper.BackgroundJobs;
using TransactionFileMapper.DataAccess.Settings;
using TransactionFileMapper.Repositories.Abstract;
using TransactionFileMapper.Repositories.Concrete;
using TransactionFileMapper.Services.Abstract;
using TransactionFileMapper.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var b = builder.Services.Configure<MongoConnection>(
               builder.Configuration.GetSection("MongoConnection"));

builder.Services.Configure<MongoConnection>(
               builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddCors(options =>
                            options.AddPolicy("AllowAny",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));
#region JobConfiguration
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var key = JobKey.Create(nameof(GetMT940FileJob));
    q.AddJob<GetMT940FileJob>(key);
    q.AddTrigger(t => t.ForJob(key)
    .WithSimpleSchedule(s => s.WithIntervalInMinutes(15).RepeatForever()));
});
builder.Services.AddQuartzHostedService();

#endregion

#region Repository
builder.Services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<ISubsidiaryRepository, SubsidiaryRepository>();
builder.Services.AddSingleton<ICompanyRepository, CompanyRepository>();
builder.Services.AddSingleton<IDocumentRepository, DocumentRepository>();
builder.Services.AddSingleton<IBankRepository, BankRepository>();
#endregion

#region Service
builder.Services.AddSingleton<ICompanyService, CompanyService>();
builder.Services.AddSingleton<IDocumentService, DocumentService>();
builder.Services.AddSingleton<ISubsidiaryService, SubsidiaryService>();
builder.Services.AddSingleton<IBankService, BankService>();
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAny");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
