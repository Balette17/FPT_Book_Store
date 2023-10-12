namespace FPTBook.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int BooksID { get; set; }
        public Book?  Books { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        public Order? Order { get; set; }
    }
}
