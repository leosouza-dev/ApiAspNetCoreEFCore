using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;

namespace ProductCatalog.Data.Maps
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
           builder.ToTable("Category"); //alterando o nome da tabela
           builder.HasKey(x => x.Id); //definindo a chave primaria como Id
           builder.Property(x=>x.Title).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)"); //colocando limites - requirido, tem no maximo 120 caracter e define o tipo
        }
    }
}