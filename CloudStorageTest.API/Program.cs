using CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
using CloudStorageTest.Application.UserCases.Users.UploadProfilePhoto;
using CloudStorageTest.Domain.Storage;
using CloudStorageTeste.Infrastructure.Storage;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUploadProfilePhotoUseCase, UploadProfilePhotoUseCase>();
builder.Services.AddScoped<IStorageService>(options =>
{
    // Catch from appsettings
    var clientId = builder.Configuration.GetValue<string>("CloudStorage:ClientId");
    var clientSecret = builder.Configuration.GetValue<string>("CloudStorage:ClientSecret");

    var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
    {
        ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        },
        Scopes = [Google.Apis.Drive.v3.DriveService.Scope.Drive],
        DataStore = new FileDataStore("GoogleDriveTest")
    });

    return new GoogleDriveStorageService(apiCodeFlow);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
