using System.ComponentModel.DataAnnotations;
using FPTBookStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FPTBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Author")]
        public int AuthorID { get; set; }
        public Author? Author { get; set; }


        [Display(Name = "Publishing Company")]
        public int PublishingCompanyID { get; set; }
        public PublishingCompany? PublishingCompany { get; set; }


        public int Quantity { get; set; }
        public string? Description { get; set; }



        public string? ImgFileName { get; set; } // Thêm thuộc tính kiểu string để lưu trữ tên tệp hình ảnh

        public string? ImgFileExt { get; set; }
        // Thêm thuộc tính kiểu string để lưu trữ tên tệp hình ảnh

    }

    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Book>? Books { get; set; }
        public Category() { }
        public Category(AcceptCatogory tc)
        {
            this.Name = tc.Name;
        }
    }

    public class PublishingCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Book>? Books { get; set; }
    }


public static class BookEndpoints
{
	public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.Book.ToListAsync();
        })
        .WithName("GetAllBooks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Book>, NotFound>> (int id, ApplicationDbContext db) =>
        {
            return await db.Book.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Book model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBookById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Book book, ApplicationDbContext db) =>
        {
            var affected = await db.Book
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, book.Id)
                  .SetProperty(m => m.Name, book.Name)
                  .SetProperty(m => m.CategoryID, book.CategoryID)
                  .SetProperty(m => m.Price, book.Price)
                  .SetProperty(m => m.AuthorID, book.AuthorID)
                  .SetProperty(m => m.PublishingCompanyID, book.PublishingCompanyID)
                  .SetProperty(m => m.Quantity, book.Quantity)
                  .SetProperty(m => m.Description, book.Description)
                  .SetProperty(m => m.ImgFileName, book.ImgFileName)
                  .SetProperty(m => m.ImgFileExt, book.ImgFileExt)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBook")
        .WithOpenApi();

        group.MapPost("/", async (Book book, ApplicationDbContext db) =>
        {
            db.Book.Add(book);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Book/{book.Id}",book);
        })
        .WithName("CreateBook")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ApplicationDbContext db) =>
        {
            var affected = await db.Book
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBook")
        .WithOpenApi();
    }
}}

