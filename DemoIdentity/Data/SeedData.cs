using FPTBook.Models;
using FPTBook.Data;
using System;
using System.Linq;

namespace FPTBook.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Book.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Legal politics" },
                    new Category { Name = "Science and technology – Economics" },
                    new Category { Name = "Socio-Culture – History" },
                     new Category { Name = "Literature and art" },
                     new Category { Name = "Curriculum" },
                     new Category { Name = "Novel" },
                      new Category { Name = "Psychology, spirituality, religion" },
                       new Category { Name = "Children's books" },
                    // Thêm các loại khác ở đây
                };

                context.Category.AddRange(categories);

                var authors = new Author[]
                {
                    new Author { Name = "Le_trong_the" },
                    new Author { Name = "Nguyen_ngoc_can" },
                    new Author { Name = "tran_ngoc_tinh" },
                    // Thêm các tác giả khác ở đây
                };

                context.Author.AddRange(authors);

                var publishingCompanies = new PublishingCompany[]
                {
                    new PublishingCompany { Name = "VN" },
                    new PublishingCompany { Name = "USA" },
                    // Thêm các công ty phát hành khác ở đây
                };

                context.PublishingCompany.AddRange(publishingCompanies);

                var books = new Book[]
                {
                    new Book
                    {
                        Name = "Book 1",
                        Category = categories[0],
                        Price = 19.99M,
                        Author = authors[0],
                        PublishingCompany = publishingCompanies[0],
                        Quantity = 10,
                        Description = "Description for Book 1",
                        ImgFileName = "61zXcdFXvyL._AC_UY327_FMwebp_QL65_231125975.webp",
                        ImgFileExt = ".webp"
                    },
                    new Book
                    {
                        Name = "Book 2",
                        Category = categories[1],
                        Price = 29.99M,
                        Author = authors[1],
                        PublishingCompany = publishingCompanies[1],
                        Quantity = 15,
                        Description = "Description for Book 2",
                        ImgFileName = "61zXcdFXvyL._AC_UY327_FMwebp_QL65_231125975.webp",
                        ImgFileExt = ".jpg"
                    },
                    new Book
                    {
                        Name = "Book 3",
                        Category = categories[3],
                        Price = 29.99M,
                        Author = authors[1],
                        PublishingCompany = publishingCompanies[1],
                        Quantity = 15,
                        Description = "Description for Book 2",
                        ImgFileName = "71e042kmtAL._AC_UY327_FMwebp_QL65_234521811.webp",
                        ImgFileExt = ".jpg"
                    },
                    new Book
                    {
                        Name = "Book 4",
                        Category = categories[4],
                        Price = 29.99M,
                        Author = authors[2],
                        PublishingCompany = publishingCompanies[1],
                        Quantity = 15,
                        Description = "Description for Book 2",
                        ImgFileName = "819mOa6wJIL._AC_UY327_FMwebp_QL65_234719373.webp",
                        ImgFileExt = ".webp"
                    },
                     new Book
                    {
                        Name = "Book 5",
                        Category = categories[6],
                        Price = 29.99M,
                        Author = authors[1],
                        PublishingCompany = publishingCompanies[1],
                        Quantity = 15,
                        Description = "Description for Book 2",
                        ImgFileName = "81o3eY5E-rL._AC_UY327_FMwebp_QL65_234533384.webp",
                        ImgFileExt = ".webp"
                    },
                      new Book
                    {
                        Name = "Book 6",
                        Category = categories[7],
                        Price = 29.99M,
                        Author = authors[2],
                        PublishingCompany = publishingCompanies[1],
                        Quantity = 15,
                        Description = "Description for Book 2",
                        ImgFileName = "91fmMnpV3JL._AC_UY327_FMwebp_QL65_231835086.webp",
                        ImgFileExt = ".webp"
                    },
                      new Book
                    {
                        Name = "Book 7",
                        Category = categories[5],
                        Price = 29.99M,
                        Author = authors[2],
                        PublishingCompany = publishingCompanies[1],
                        Quantity = 15,
                        Description = "Description for Book 2",
                        ImgFileName = "91fmMnpV3JL._AC_UY327_FMwebp_QL65_234541898.webp",
                        ImgFileExt = ".jpg"
                    },
                       new Book
                    {
                        Name = "Book 8",
                        Category = categories[5],
                        Price = 29.99M,
                        Author = authors[2],
                        PublishingCompany = publishingCompanies[1],
                        Quantity = 15,
                        Description = "Description for Book 2",
                        ImgFileName = "91fmMnpV3JL._AC_UY327_FMwebp_QL65_234705470.webp",
                        ImgFileExt = ".webp"
                    },


                    // Thêm sách khác ở đây
                };

                context.Book.AddRange(books);

                context.SaveChanges();
            }
        }
    }
}
