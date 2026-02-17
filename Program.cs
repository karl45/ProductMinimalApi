using LoginProductMinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterAntiforgeryToken();
builder.RegisterDbContext();
builder.RegisterCors();
builder.AddMediatr();
builder.RegisterJwtAuthorization();

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();



app.RegisterEndPointConfiguration();


app.Run();
