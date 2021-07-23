using MyFoodSupply;
using NUnit.Framework;
using System;

namespace UnitTestCaseStudy
{
    [TestFixture]
    public class UnitTestForCreateFoodDetail
    {
        Program mainClassObj;
        [OneTimeSetUp]
        public void Setup()
        {
            mainClassObj  = new Program(); 
        }


        [TestCase("")]
        [TestCase(null)]
        public void CreateFoodDetail_WhenNameisNullOrEmpty(string name)
        {
            DateTime dt = DateTime.Now;
            var exceptionThrown = Assert.Throws<Exception>(() => mainClassObj.CreateFoodDetail(name, 1, dt, 51));
            Assert.AreEqual(exceptionThrown.Message, "Dish name cannot be empty. Please provide valid value");
        }


        [TestCase(0)]
        [TestCase(-10)]
        public void CreateFoodDetail_WhenPriceisLessOrEqualsZero(double price)
        {
            DateTime dt = DateTime.Now;
            var exceptionThrown =  Assert.Throws<Exception>(() => mainClassObj.CreateFoodDetail("Name", 1, dt, price));
            Assert.AreEqual(exceptionThrown.Message, "Incorrect value for dish price. Please provide valid value");
        }

        [Test]
        public void CreateFoodDetail_WhenExpiryDateisLessThanToday()
        {
            DateTime dt = new DateTime(2020, 12, 1);
            var exceptionThrown = Assert.Throws<Exception>(() => mainClassObj.CreateFoodDetail("Name", 1, dt, 20));
            Assert.AreEqual(exceptionThrown.Message, "Incorrect expiry date. Please provide valid value");
        }


        [TestCase("Pizza",1,20)]
        [TestCase("Burger", 1, 200)]

        public void CreateFoodDetail_WhenAllArgumentAreCorrect(string name, int dishType, float price)
        {
            // 1. How to Pass DateTime in TestCase
            // 2. How to check enum

            DateTime expiryDate = new DateTime(2021, 12, 1);
            FoodDetail foodDetailsReturned = mainClassObj.CreateFoodDetail(name, dishType, expiryDate, price);

            Assert.AreEqual(foodDetailsReturned.Name, name);
            Assert.AreEqual(foodDetailsReturned.Price, price);
            Assert.AreEqual(foodDetailsReturned.ExpiryDate, expiryDate);
            Assert.AreEqual(foodDetailsReturned.DishType, (FoodDetail.Category)dishType);
         }

    }
}
