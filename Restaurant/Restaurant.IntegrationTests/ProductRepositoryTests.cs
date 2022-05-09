using NUnit.Framework;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Shouldly;
using System;

namespace Restaurant.IntegrationTests
{
    public class ProductRepositoryTests : BaseTest
    {
        private IProductRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = container.Resolve<IProductRepository>();
        }

        [Test]
        public void given_valid_product_should_add_to_db()
        {
            var product = new Product { Id = Guid.NewGuid(), Price = 105.50M, ProductName = "Product #105" };
            
            repository.Add(product);

            var productFromDb = repository.Get(product.Id);
            productFromDb.ShouldNotBeNull();
            productFromDb.ProductName.ShouldBe(product.ProductName);
        }

        [Test]
        public void given_valid_id_should_delete_product()
        {
            var product = new Product { Id = Guid.NewGuid(), Price = 105.50M, ProductName = "Product #107" };
            repository.Add(product);

            repository.Delete(product.Id);

            var productFromDb = repository.Get(product.Id);
            productFromDb.ShouldBeNull();
        }

        [Test]
        public void should_return_products()
        {
            var products = repository.GetAll();

            products.ShouldNotBeNull();
            products.ShouldNotBeEmpty();
        }

        [Test]
        public void given_valid_product_should_update()
        {
            var product = new Product { Id = Guid.NewGuid(), Price = 105.50M, ProductName = "Product #107" };
            repository.Add(product);
            var productModified = new Product { Id = product.Id, Price = 125.55M, ProductName = "Product #1" };

            repository.Update(productModified);

            var productFromDb = repository.Get(product.Id);
            productFromDb.ShouldNotBeNull();
            productFromDb.Price.ShouldBe(productModified.Price);
            productFromDb.ProductName.ShouldBe(productModified.ProductName);
        }

        [Test]
        public void given_valid_product_id_should_return_from_db()
        {
            var productId = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd");

            var product = repository.Get(productId);

            product.ShouldNotBeNull();
            product.Price.ShouldBe(500M);
        }
    }
}
