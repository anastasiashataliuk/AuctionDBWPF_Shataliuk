using AutoMapper;
using DAL.Interface;
using DALEF.Context;
using DALEF.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Concrete
{
    public class ProductDalEf : IProductDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public ProductDalEf(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public List<Product> GetAll()
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                return _mapper.Map<List<Product>>(context.Product.ToList());
            }
        }

        public Product GetById(int id)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblProduct = context.Product.Find(id);
                return _mapper.Map<Product>(tblProduct);
            }
        }

        public Product Insert(Product product)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblProduct = _mapper.Map<TblProduct>(product);
                context.Product.Add(tblProduct);
                context.SaveChanges();

                product.Product_Id = tblProduct.Product_Id; // Отримуємо збережене значення
                return product;
            }
        }

        public void Update(Product product)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblProduct = _mapper.Map<TblProduct>(product);
                context.Product.Update(tblProduct);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblProduct = context.Product.Find(id);
                if (tblProduct != null)
                {
                    context.Product.Remove(tblProduct);
                    context.SaveChanges();
                }
            }
        }

        public List<Product> SearchByName(string keyword)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                // Пошук продуктів за ключовим словом у назві
                var products = context.Product
                    .Where(p => p.Name.Contains(keyword))
                    .ToList();

                // Мапимо їх до DTO класу Product
                return _mapper.Map<List<Product>>(products);
            }
        }

    }
}
