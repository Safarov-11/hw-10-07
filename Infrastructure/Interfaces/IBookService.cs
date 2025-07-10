using Domain.ApiResponse;
using Domain.Entites;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    Task<Response<string>> AddBookAsync(Book Book);
    Task<Response<string>> UpdateBookAsync(Book Book);
    Task<Response<string>> DeleteBookAsync(int id);
    Task<Response<Book?>> GetBookAsync(int id);
    Task<Response<List<Book>>> GetBooksAsync();
    Task<PagedResponse<List<Book>>> GetBooksAsync(BookFilter filter);
}
