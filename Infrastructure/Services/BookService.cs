using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.Entites;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookService(DataContext context) : IBookService
{
    public async Task<Response<string>> AddBookAsync(Book book)
    {
        await context.Books.AddAsync(book);
        var result = await context.SaveChangesAsync();

        return result > 0
            ? new Response<string>(null, "Success")
            : new Response<string>("Something went wrong", HttpStatusCode.InternalServerError);
    }


    public async Task<Response<string>> DeleteBookAsync(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
        {
            return new Response<string>("Book not found", HttpStatusCode.NotFound);
        }

        context.Books.Remove(book);
        var res = await context.SaveChangesAsync();
        return res == 0
        ? new Response<string>("Someting went wrong", HttpStatusCode.InternalServerError)
        : new Response<string>(null, "Success");
    }

    public async Task<Response<string>> UpdateBookAsync(Book book)
    {
        var foundedBook = await context.Books.FindAsync(book.Id);
        if (foundedBook == null)
        {
            return new Response<string>("Book not found", HttpStatusCode.NotFound);
        }

        foundedBook.Title = book.Title;
        foundedBook.Author = book.Author;
        foundedBook.Genre = book.Genre;
        foundedBook.PublishedDate = book.PublishedDate;

        var res = await context.SaveChangesAsync();
        return res == 0
        ? new Response<string>("Someting went wrong", HttpStatusCode.InternalServerError)
        : new Response<string>(null, "Success");
    }

    public async Task<Response<Book?>> GetBookAsync(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
        {
            return new Response<Book?>("Book not found", HttpStatusCode.NotFound);
        }

        return new Response<Book?>(book, "Success");
    }


    public async Task<Response<List<Book>>> GetBooksAsync()
    {
        var books = await context.Books.ToListAsync();

        return new Response<List<Book>>(books);

    }
    public async Task<PagedResponse<List<Book>>> GetBooksAsync(BookFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var books = context.Books.AsQueryable();

        if (filter.Title != null)
        {
            books = books.Where(b => b.Title.ToLower().Trim().Contains(filter.Title.ToLower().Trim()));
        }

        if (filter.Author != null)
        {
            books = books.Where(b => b.Author.ToLower().Trim().Contains(filter.Author.ToLower().Trim()));
        }

        if (filter.Genre != null)
        {
            books = books.Where(b => b.Genre.ToLower().Trim().Contains(filter.Genre.ToLower().Trim()));
        }

        if (filter.FromPublishedDate != null)
        {
            books = books.Where(b => b.PublishedDate >= filter.FromPublishedDate);
        }

        if (filter.ToPublishedDate != null)
        {
            books = books.Where(b => b.PublishedDate <= filter.ToPublishedDate);
        }

        var totalRecords = books.Count();

        var paged = await books
        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        .Take(validFilter.PageSize)
        .ToListAsync();


        return new PagedResponse<List<Book>>(paged, totalRecords, validFilter.PageNumber, validFilter.PageSize );
    }


}
