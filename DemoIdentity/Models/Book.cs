using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations;

namespace BookCan.Models
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
        public string? ImgFileExt { get; set; } // Thêm thuộc tính kiểu string để lưu trữ tên tệp hình ảnh




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
        public ICollection<Book>? Book { get; set; }
    }

    public class PublishingCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Book>? Book { get; set; }
    }
}

