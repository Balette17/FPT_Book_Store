using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FPT_Books_Store.Models;

namespace FPT_Books_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FPT_Books_Store.Models.Book> Book { get; set; } = default!;
        public DbSet<FPT_Books_Store.Models.Category> Category { get; set; } = default!;
        public DbSet<FPT_Books_Store.Models.PublishingCompany> PublishingCompany { get; set; } = default!;
        public DbSet<FPT_Books_Store.Models.Author> Author { get; set; } = default!;
    }
}