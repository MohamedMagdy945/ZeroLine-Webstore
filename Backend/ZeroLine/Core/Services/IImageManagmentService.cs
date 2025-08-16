using Microsoft.AspNetCore.Http;

namespace ZeroLine.API.Services
{
    public interface IImageManagmentService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
        Task DeleteImageAsync(string src);
    }
}
