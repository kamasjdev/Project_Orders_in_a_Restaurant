using NUnit.Framework;
using Restaurant.Domain.Entities;
using Shouldly;
using System;

namespace Restaurant.UnitTests
{
    public class OrdersTests
    {
        [Test]
        public void given_valid_order_should_create()
        {
            var orderNumber = "ORDER/1/2";
            var price = 100M;
            var email = "email@test.email.com";

            var order = new Order(Guid.NewGuid(), orderNumber, DateTime.UtcNow, price, email);

            order.ShouldNotBeNull();
            order.OrderNumber.ShouldBe(orderNumber);
            order.Price.ShouldBe(price);
            order.Email.ShouldBe(email);
        }

        [Test]
        public void given_invalid_order_number_should_throw_an_exception()
        {
            var firstOrderNumber = "ORDER/1/2";
            var secondOrderNumber = "";
            var order = new Order(Guid.NewGuid(), firstOrderNumber, DateTime.UtcNow, 100M, "email@test.email.com");

            var exception = Should.Throw<Exception>(() => order.ChangeOrderNumber(secondOrderNumber));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidOperationException>();
            exception.Message.ShouldContain("cannot be empty");
        }

        [Test]
        public void given_too_short_order_number_should_throw_an_exception()
        {
            var firstOrderNumber = "ORDER/1/2";
            var secondOrderNumber = "O";
            var order = new Order(Guid.NewGuid(), firstOrderNumber, DateTime.UtcNow, 100M, "email@test.email.com");

            var exception = Should.Throw<Exception>(() => order.ChangeOrderNumber(secondOrderNumber));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidOperationException>();
            exception.Message.ShouldContain("at least 3 characters");
        }

        [Test]
        public void given_valid_order_number_should_change()
        {
            var firstOrderNumber = "ORDER/1/2";
            var secondOrderNumber = "ORDER/1/2/3";
            var order = new Order(Guid.NewGuid(), firstOrderNumber, DateTime.UtcNow, 100M, "email@test.email.com");

            order.ChangeOrderNumber(secondOrderNumber);

            order.OrderNumber.ShouldNotBe(firstOrderNumber);
            order.OrderNumber.ShouldBe(secondOrderNumber);
        }

        [Test]
        public void given_negative_price_should_throw_an_exception()
        {
            var firstPrice = 100M;
            var secondPrice = -200M;
            var order = new Order(Guid.NewGuid(), "ORDER/1/2", DateTime.UtcNow, firstPrice, "email@test.email.com");

            var exception = Should.Throw<Exception>(() => order.ChangePrice(secondPrice));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidOperationException>();
            exception.Message.ShouldContain("cannot be negative");
        }

        [Test]
        public void given_valid_price_should_change()
        {
            var firstPrice = 100M;
            var secondPrice = 200M;
            var order = new Order(Guid.NewGuid(), "ORDER/1/2", DateTime.UtcNow, firstPrice, "email@test.email.com");

            order.ChangePrice(secondPrice);

            order.Price.ShouldNotBe(firstPrice);
            order.Price.ShouldBe(secondPrice);
        }

        [Test]
        public void given_empty_email_should_throw_an_exception()
        {
            var firstEmail = "email@email.com";
            var secondEmail = "";
            var order = new Order(Guid.NewGuid(), "ORDER/1/2", DateTime.UtcNow, 100M, firstEmail);

            var exception = Should.Throw<Exception>(() => order.ChangeEmail(secondEmail));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidOperationException>();
            exception.Message.ShouldContain("Email cannot be empty");
        }

        [Test]
        public void given_invalid_email_should_throw_an_exception()
        {
            var firstEmail = "email@email.com";
            var secondEmail = "admin.com";
            var order = new Order(Guid.NewGuid(), "ORDER/1/2", DateTime.UtcNow, 100M, firstEmail);

            var exception = Should.Throw<Exception>(() => order.ChangeEmail(secondEmail));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidOperationException>();
            exception.Message.ShouldContain("Invalid Email");
        }

        [Test]
        public void given_valid_email_should_change()
        {
            var firstEmail = "email@email.com";
            var secondEmail = "admin@gmail.com";
            var order = new Order(Guid.NewGuid(), "ORDER/1/2", DateTime.UtcNow, 100M, firstEmail);

            order.ChangeEmail(secondEmail);

            order.Email.ShouldNotBe(firstEmail);
            order.Email.ShouldBe(secondEmail);
        }
    }
}
