using CloudStorageTest.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Domain.Storage
{
    public interface IStorageService
    {
        string Upload(IFormFile file, User user);
    }
}
