using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTeste.Infrastructure.Storage
{
    public class GoogleDriveStorageService : IStorageService
    {

        private readonly GoogleAuthorizationCodeFlow _googleAuthorizationCodeFlow;

        public GoogleDriveStorageService(GoogleAuthorizationCodeFlow googleAuthorizationCodeFlow)
        {
            _googleAuthorizationCodeFlow = googleAuthorizationCodeFlow;
        }

        public string Upload(IFormFile file, User user)
        {
            var credential = new UserCredential(null, user.Email, new TokenResponse
            {
                AccessToken = user.AccessToken,
                RefreshToken = user.RefreshToken
            });

            var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
            {
                ApplicationName = "GoogleDriveTest",
                HttpClientInitializer = credential
            });

            var driveFile = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.Name,
                MimeType = file.ContentType,
            };

            var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
            command.Fields = "Id";

            var response = command.Upload();

            if (response.Status is not Google.Apis.Upload.UploadStatus.Completed
                               or Google.Apis.Upload.UploadStatus.NotStarted)
            {
                throw response.Exception;
            }

            return command.ResponseBody.Id;
        }
    }

}
