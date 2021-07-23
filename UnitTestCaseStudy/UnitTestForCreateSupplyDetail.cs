using MyFoodSupply;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestCaseStudy
{
    class UnitTestForCreateSupplyDetail
    {
        Program mainClassObj;
        FoodDetail objFoodDetails;
        [OneTimeSetUp]
        public void Setup()
        {
            mainClassObj = new Program();
            objFoodDetails = new FoodDetail
            {
                Name= "Pizza",
                Price = 99.0,
                DishType = (FoodDetail.Category) 1,
                ExpiryDate = new DateTime(2021,07,19)
            };
        }
        //int foodItemCount, DateTime requestDate, string sellerName, double packingCharge, FoodDetail foodItem

        [TestCase(1,"Tarun",50,null)]
        
        public void CreateSupplyDetail_WhenfoodItemisNull(int foodItemCount,  string sellerName, double packingCharge, FoodDetail foodItem)
        {
            DateTime requestDate = new DateTime(2021, 07, 12);
            var result = mainClassObj.CreateSupplyDetail(foodItemCount, requestDate, sellerName, packingCharge, foodItem);
            Assert.AreEqual(null,result);
        }

        [TestCase(0, "Tarun", 50)]
        [TestCase(-1, "Tarun", 50)]
        public void CreateSupplyDetail_WhenfoodItemCountisLessOrEqualsZero(int foodItemCount, string sellerName, double packingCharge)
        {
            FoodDetail foodItem = objFoodDetails;
            DateTime requestDate = new DateTime(2021, 07, 12);
            var resultException = Assert.Throws<Exception>( ()=> mainClassObj.CreateSupplyDetail(foodItemCount, requestDate, sellerName, packingCharge, foodItem));
            Assert.AreEqual(resultException.Message, "Incorrect food item count. Please check");
        }


        [TestCase(1, "Tarun", 50)]
        [TestCase(10, "Tarun", 50)]
        public void CreateSupplyDetail_WhenRequestDateisLessThanToday(int foodItemCount, string sellerName, double packingCharge)
        {
            FoodDetail foodItem = objFoodDetails;
            DateTime requestDate = new DateTime(2020, 07, 12);
            var resultException = Assert.Throws<Exception>(() => mainClassObj.CreateSupplyDetail(foodItemCount, requestDate, sellerName, packingCharge, foodItem));
            Assert.AreEqual(resultException.Message, "Incorrect food request date. Please provide valid value");
        }

        [TestCase(1, "Tarun", 50)]
        [TestCase(10, "Tarun", 50)]
        public void CreateSupplyDetail_WhenRequestDateisGreaterThanExpiry(int foodItemCount, string sellerName, double packingCharge)
        {
            FoodDetail foodItem = objFoodDetails;
            DateTime requestDate = new DateTime(2021, 07, 31);
            var resultException = Assert.Throws<Exception>(() => mainClassObj.CreateSupplyDetail(foodItemCount, requestDate, sellerName, packingCharge, foodItem));
            Assert.AreEqual(resultException.Message, "Request date greater than expiry date. Please check");
        }

        [TestCase(1, "", 50)]
        [TestCase(10, null, 50)]
        public void CreateSupplyDetail_WhenSellerNameisNullOrEmpty(int foodItemCount, string sellerName, double packingCharge)
        {
            FoodDetail foodItem = objFoodDetails;
            DateTime requestDate = new DateTime(2021, 07, 12);
            var exceptionThrown = Assert.Throws<Exception>(() => mainClassObj.CreateSupplyDetail(foodItemCount, requestDate, sellerName, packingCharge, foodItem));
            Assert.AreEqual(exceptionThrown.Message, "Incorrect seller name. Please check");
        }

        [TestCase(1, "Tarun", 50)]
        [TestCase(10, "Tarun", 50)]
        public void CreateSupplyDetail_WhenAllDetailsOk(int foodItemCount, string sellerName, double packingCharge)
        {
            FoodDetail foodItem = objFoodDetails;
            DateTime requestDate = new DateTime(2021, 07, 12);

            var resultReturned = mainClassObj.CreateSupplyDetail(foodItemCount, requestDate, sellerName, packingCharge, foodItem);
            double totalCost = (foodItem.Price * foodItemCount) + packingCharge;
            Assert.AreEqual(resultReturned.SellerName, sellerName);
            Assert.AreEqual(resultReturned.Count, foodItemCount);
            Assert.AreEqual(resultReturned.RequestDate, requestDate);
            Assert.AreEqual(resultReturned.TotalCost, totalCost);
            Assert.AreEqual(resultReturned.FoodItem, foodItem);

        }
    }
}
