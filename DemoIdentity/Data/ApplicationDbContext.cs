using DemoIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookCan.Models;

namespace DemoIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BookCan.Models.Book> Book { get; set; } = default!;
        public DbSet<BookCan.Models.Category> Category { get; set; } = default!;
        public DbSet<BookCan.Models.Author> Author { get; set; } = default!;
        public DbSet<BookCan.Models.PublishingCompany> PublishingCompany { get; set; } = default!;
    }
}