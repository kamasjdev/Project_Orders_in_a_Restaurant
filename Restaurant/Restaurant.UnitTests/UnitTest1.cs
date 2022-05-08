using System.Collections.Generic;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Restaurant.UI;
using Restaurant.UI.Components;
using Restaurant.UI.ConcreteComponents;
using Shouldly;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Restaurant.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestMethod1()         // Test metody czy string który zwraca jest w postaci html
        {
            //Arrange
            Product mc = new MainCourse("Schabowy z fryktami",30);
            Product pizza = new Pizza("Pizza Tosca z podwójnym serem",27);
            Product cola = new DrinkSoup("Cola",5);
            List<Product> list = new List<Product>();
            list.Add(mc);
            list.Add(pizza);
            list.Add(cola);
            Order order = new Order(list);

            //Act 
            string htmlText = MailSender.ContentEmail(order);
            //htmlText = "cos"; // powinien byc blad

            //Assert
            Assert.IsTrue(htmlText != HttpUtility.HtmlEncode(htmlText)); // konwertuje text na html
        }

        [TestMethod]
        public void TestMethod2() // test sprawdzajacy czy 2 osobne zamówienia będą miały ten sam nr
        {
            //Arrange
            Product mc = new MainCourse("Schabowy z fryktami", 30);
            Product pizza = new Pizza("Pizza Tosca z podwójnym serem", 27);
            Product cola = new DrinkSoup("Cola", 5);
            List<Product> list = new List<Product>();
            list.Add(mc);
            list.Add(pizza);
            list.Add(cola);

            //Act
            Order order = new Order(list);
            Order order2 = new Order(list);

            //Assert
            Assert.IsTrue(order.Get_Order_Nr() != order2.Get_Order_Nr());
        }

        [Test]
        public void given_valid_context_should_create_content_email()
        {
            //Arrange
            Product mc = new MainCourse("Schabowy z fryktami", 30);
            Product pizza = new Pizza("Pizza Tosca z podwójnym serem", 27);
            Product cola = new DrinkSoup("Cola", 5);
            List<Product> list = new List<Product>();
            list.Add(mc);
            list.Add(pizza);
            list.Add(cola);
            Order order = new Order(list);

            //Act 
            string htmlText = MailSender.ContentEmail(order);

            //Assert
            htmlText.ShouldNotBeNull();
            htmlText.ShouldNotBeNullOrWhiteSpace();
        }

    }
}
