using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc; // 2 - namespace para usar o controller
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller //1 - herda de controller
    {
        //3 - injeção de dependencia do conexto - acesso ao banco de dados
        private readonly StoreDataContext _context;
        public CategoryController (StoreDataContext context)
        {
            _context = context;
        }

        //4 - Vamos trabalhar com o "metodo Get" - vamos retornar uma lista de categorias
        [HttpGet]
        [Route("v1/categories")]
        public IEnumerable<Category> Get()
        {
            //return _context.Categories.ToList();
            return _context.Categories.AsNoTracking().ToList(); //AsNoTrackiing - para otimização -> para evitar o Proxy(informações adicionais da categoria - se foi atualizado, removido, inluido  ) que o EF traz em leituras ao BD
        }

        // 5 - Vamos trabalhar com o "metodo Get". Só que dessa vez ele retorna uma categoria
        [HttpGet]
        [Route("v1/categories/{id}")]
        public Category Get(int id)
        {
            //return _context.Categories.Find(id); //AsNoTracjing não possui o metodo find()
            return _context.Categories.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();            
        }

        // 6 - Vamos implementar o "metodo GetProducts" que retorna uma lista de produtos de uma categoria especifica
        [HttpGet]
        [Route("v1/categories/{id}/products")]
        public IEnumerable<Product> GetProducts(int id)
        {
            return _context.Products.AsNoTracking().Where(p => p.CategoryId == id).ToList();
        }

        // 7 - Vamos implementar o "metodo Post" para criar uma categoria
        [Route("v1/categories")]
        [HttpPost]
        public Category Post([FromBody]Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }
 
        // 8 - Vamos implementar o "metodo Put" para atualizar uma categoria
        [HttpPut]
        [Route("v1/categories")]
        public Category Put([FromBody]Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();

            return category;
        }

        //9 - Vamos implementar o metodo delete - vamos deletar uma categoria
        [HttpDelete]
        [Route("v1/categories")]
        public Category Delete([FromBody]Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category;
        }
    }
}