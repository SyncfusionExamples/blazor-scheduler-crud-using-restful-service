using RestfulServices.Models;
using Microsoft.OData.Edm;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;

var builder = WebApplication.CreateBuilder(args);

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder odataBuilder = new();
    odataBuilder.EntitySet<EventData>("Events");
    return odataBuilder.GetEdmModel();
}

var batchHandler = new DefaultODataBatchHandler();
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel(), batchHandler).Filter().Select().OrderBy().Expand().SetMaxTop(null).Count());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScheduleDataContext>(options => options.UseInMemoryDatabase("Events"));

var app = builder.Build();
// Resolve CORS errors
app.UseCors("AnyOrgins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// OData batch handling
app.UseODataBatching();
app.UseHttpsRedirection();
// Routing
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();









