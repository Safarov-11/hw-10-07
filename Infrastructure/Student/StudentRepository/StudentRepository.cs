using Domain.ApiResponse;
using Domain.Entites;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Data;
using Infrastructure.Student.StudentInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Student.StudentRepository;

public class StudentRepository(DataContext context) : IStudentRepository
{
    public async Task<PagedResponse<List<Domain.Entites.Student>>> GetAllAsync(StudentFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageSize, filter.PageNumber);

        var query = context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            query = query.Where(s => s.FirstName.ToLower().Trim().Contains(filter.FirstName.ToLower().Trim()));
        }

        if (!string.IsNullOrEmpty(filter.LastName))
        {
            query = query.Where(s => s.LastName.ToLower().Trim().Contains(filter.LastName.ToLower().Trim()));
        }

        if (filter.GradeHigherThan != null)
        {
            query = query.Where(s => s.Grade >= filter.GradeHigherThan);
        }

        if (filter.GradeLoverThan != null)
        {
            query = query.Where(s => s.Grade >= filter.GradeLoverThan);
        }

        var totalRecords = query.Count();

        var paged = await query
        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        .Take(validFilter.PageSize).ToListAsync();

        return new

    }
    public async Task<int> CreateAsync(Domain.Entites.Student student)
    {
        await context.Students.AddAsync(student);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Domain.Entites.Student student)
    {
        context.Students.Update(student);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Domain.Entites.Student student)
    {
        context.Students.Remove(student);
        return await context.SaveChangesAsync();
    }

    public async Task<int> GetAsync(int id)
    {
        await context.Students.FindAsync(id);
        return await context.SaveChangesAsync();
    }
}
