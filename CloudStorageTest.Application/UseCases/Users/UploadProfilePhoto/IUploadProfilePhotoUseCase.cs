using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto
{
    public interface IUploadProfilePhotoUseCase
    {
        public void Execute(IFormFile file);
    }
}
