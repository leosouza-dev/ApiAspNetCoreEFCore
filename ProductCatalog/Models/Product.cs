using System;
using System.Collections.Generic;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; } //não trabalhamos com o binario da imagem - Trabalhamos com a URL da imagem
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        //propriedades de referencia para categoria
        public int CategoryId { get; set; }
        public Category Category { get; set; } //produto tem uma categoria
    }
}