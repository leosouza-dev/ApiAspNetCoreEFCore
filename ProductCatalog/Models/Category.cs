using System.Collections.Generic;

namespace ProductCatalog.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Product> Produtcs { get; set; } //categoria tem varios produtos
    }
}