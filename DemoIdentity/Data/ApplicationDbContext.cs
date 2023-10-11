using FPTBookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;

namespace FPTBookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FPTBook.Models.Book> Book { get; set; } = default!;
        public DbSet<FPTBook.Models.Author> Author { get; set; } = default!;
        public DbSet<FPTBook.Models.PublishingCompany> PublishingCompany { get; set; } = default!;
        public DbSet<FPTBook.Models.Category> Category { get; set; } = default!;
    }
}