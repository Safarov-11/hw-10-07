using Domain.ApiResponse;
using Domain.Entites;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService bkServ) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Book>>> GetBooksAsync()
    {
        return await bkServ.GetBooksAsync();
    }

    [HttpGet("by-id/{id:int}")]
    public async Task<Response<Book?>> GetBookAsync(int id)
    {
        return await bkServ.GetBookAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> AddBookAsync(Book book)
    {
        return await bkServ.AddBookAsync(book);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateBookAsync(Book book)
    {
        return await bkServ.UpdateBookAsync(book);
    }

    [HttpDelete("by-id/{id:int}")]
    public async Task<Response<string>> DeleteBookAsync(int id)
    {
        return await bkServ.DeleteBookAsync(id);
    }
}
