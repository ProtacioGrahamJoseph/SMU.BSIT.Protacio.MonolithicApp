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
            return db.Table<Product>().FirstOrDefault(p => p.Id == id);
        }

        public void Update(Product product)
        {
            db.Update(product);
        }

        public void Delete(int id)
        {
            var product = db.Table<Product>().FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                db.Delete(product);
            }
        }
    }
}
