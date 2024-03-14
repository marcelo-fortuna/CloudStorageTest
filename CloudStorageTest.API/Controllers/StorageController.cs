using CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
using CloudStorageTest.Application.UserCases.Users.UploadProfilePhoto;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorageTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadImage([FromServices] IUploadProfilePhotoUseCase useCase, IFormFile file)
        {
            useCase.Execute(file);

            return Created();
        }
    }
}
