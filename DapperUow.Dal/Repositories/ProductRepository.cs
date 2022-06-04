using Dapper;
using DapperUow.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DapperUow.Dal.Repositories
{
    // bu 2 sınıftan kalıtım alacağız
    internal class ProductRepository : RepositoryBase, IProductRepository
    {
        // aşağıdakileri ctrl + . ile implement ettim :)
        // referansını ekliyoruz. | her işlem işlem transaction oluşturmak istiyorum.
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {

        } 

        public void Add(Product entity)
        {
            // bu kütüphaneleri kullanmak için Dapper indirmemiz gerekiyor.
            entity.ProductId = Connection.ExecuteScalar<int>(
                "INSERT INTO PRODUCT(Name, CategoryId, Price, Weight, Height) Values(@Name, @CategoryId, @Price, @Weight, @Height); SELECT SCOPE_IDENTITY()",
                param: new {Name = entity.Name, CategoryId = entity.CategoryId, Price = entity.Price, Weight = entity.Weight, Height = entity.Height},
                transaction: Transaction
                );
        }

        public IEnumerable<Product> All()
        { 
            return Connection.Query<Product>(
                "SELECT * FROM Product",
                transaction: Transaction
                ).ToList();   
             
        }

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM Product WHERE ProductId = @ProductId",
                param: new {ProductId = id},
                transaction: Transaction
                );
        }

        public void Delete(Product entity)
        {
            Delete(entity.ProductId);
        }

        public Product Find(int id)
        {
            return Connection.Query<Product>(
                "SELECT * FROM Product Where ProductId = @ProductId ",
                param: new { ProductId = id},
                transaction: Transaction
                ).FirstOrDefault();
        }

        public Product FindByName(string name)
        { 
            return Connection.Query<Product>(
                "SELECT * FROM Product WHERE Name = @Name",
                param: new {Name = name },
                transaction: Transaction
                ).FirstOrDefault();
        }

        public void Update(Product entity)
        {
            Connection.Execute(
                @"UPDATE Product SET Name = @Name, 
                                     CategoryId = @CategoryId,
                                     Price = @Price,
                                     Weight = @Weight,
                                     Height = @Height
                                     WHERE ProductId = @ProductId",   
                param:new {Name = entity.Name, CategoryId =entity.CategoryId, Price = entity.Price, Weight= entity.Weight, Height = entity.Height, ProductId = entity.ProductId },  
                transaction: Transaction
                );

             
        }
    }
}
 