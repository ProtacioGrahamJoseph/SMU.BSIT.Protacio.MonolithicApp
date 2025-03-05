using SMU.BSIT.Protacio.MonolithicApp.Models;
using SQLite;

namespace SMU.BSIT.Protacio.MonolithicApp.Services
{
    public class ProductServices
    {
        private readonly SQLiteConnection db = new SQLiteConnection("Products.db");

        public ProductServices()
        {
            db.CreateTable<Product>();
        }

        public void Add(Product product)
        {
            db.Insert(product);
        }

        public Product GetById(int id)
        {
            TableQuery<Product> result = db.Table<Product>().Where(query => query.Id.Equals(id));
            return result.FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
           CheckProductIfExisting(product.Id);
           db.Update(product);
        }

        public void DeleteProductById(int id)
        {
            CheckProductIfExisting(id);
            db.Delete<Product>(id);
        }

        private void CheckProductIfExisting(int id)
        {
            var _product = GetById(id);
            if (_product == null)
            {
                throw new Exception("Product does not exist!");
            }
        }


    }
}
