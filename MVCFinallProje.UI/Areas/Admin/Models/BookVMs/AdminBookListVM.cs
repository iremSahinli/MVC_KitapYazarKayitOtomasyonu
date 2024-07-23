namespace MVCFinallProje.UI.Areas.Admin.Models.BookVMs
{
    public class AdminBookListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
    }
}
