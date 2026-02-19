using LoginProductMinimalApi.Extensions;
using LoginProductMinimalApi.Extensions.mapper;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterAntiforgeryToken();
builder.RegisterDbContext();
builder.RegisterCors();
builder.AddMediatr();
builder.RegisterJwtAuthorization();
builder.AddMapperConfiguration();
builder.AddLogging();
var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();



app.RegisterEndPointConfiguration();


app.Run();
