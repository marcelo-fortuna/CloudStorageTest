using CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UserCases.Users.UploadProfilePhoto
{
    public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase
    {
        private readonly IStorageService _storageService;

        public UploadProfilePhotoUseCase(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public void Execute(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            
            // Validate if Image is really a Image
            bool isImage = fileStream.Is<JointPhotographicExpertsGroup>() 
                           || fileStream.Is<PortableNetworkGraphic>();

            if (isImage == false)
            {
                throw new BadImageFormatException("This file is not an Image.");
            }

            var user = GetFromDatabase();

            _storageService.Upload(file, user);
        }

        private User GetFromDatabase()
        {
            return new User
            {
                Id = 1,
                Name = "Marcelo",
                Email = "celofortuna@gmail.com",
                RefreshToken = "https://developers.google.com/oauthplayground",
                AccessToken = "https://developers.google.com/oauthplayground"
            };
        }
    }

}
