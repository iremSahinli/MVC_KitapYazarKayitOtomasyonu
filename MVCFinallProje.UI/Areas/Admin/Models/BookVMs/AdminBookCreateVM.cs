using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCFinallProje.UI.Areas.Admin.Models.BookVMs
{
    public class AdminBookCreateVM
    {
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }
        public SelectList Authors { get; set; } //Create kısmı için ekledik. Id lerindeki değerleri temsil ediyor .
        public SelectList Publishers { get; set; } //Create kısmı için ekledik. Id lerindeki değerleri temsil ediyor .

        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
    }
}
