using Microsoft.VisualStudio.TestTools.UnitTesting;
using DllUnit;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestClass]
        public class SubscriptionTests
        {

            public Subscription _repository;
            public Subscription _testSubscription;
            [TestInitialize]

            public void Initialize()
            {
                _repository = new Subscription();
                _testSubscription = new Subscription
                {
                    Id = 1,
                    UserId = "TestUser",
                    Type = "Monthly",
                    Status = "Active",
                    StartDate = DateTime.UtcNow,
                };
            }
            [TestMethod]
            public void CreateSubscription()
            {
                // Создание нового объекта
                var test = _repository.Create(_testSubscription);

                // Проверка значений по умолчанию
                Assert.IsNotNull(test);
                Assert.AreEqual(1, test.Id);
                Assert.AreEqual(_testSubscription.UserId, test.UserId);
                Assert.AreEqual(_testSubscription.Type, test.Type);
                Assert.AreEqual(_testSubscription.Status, test.Status);
            }

            [TestMethod]
            public void DeleteSubscription()
            {
                var created = _repository.Create(_testSubscription);
                _repository.Delete(created.Id);
                var Isdeleted = _repository.GetById(created.Id);
                Assert.IsNull(Isdeleted);
            }

            [TestMethod]
            public void Update_Subscription()
            {
                var created = _repository.Create(_testSubscription);
                var updated = _repository.Update(created);
                created.Status = "Inactive";
                Assert.AreEqual("Inactive", updated.Status);
            }

            [TestMethod]
            public void GetByIdTest()
            {
                var created = _repository.Create(_testSubscription);

                var result = _repository.GetById(created.Id);

                Assert.IsNotNull(result);
                Assert.AreEqual(created.Id, result.Id);
            }

            [TestMethod]
            public void GetByAllSubscripstions()
            {
                _repository.Create(_testSubscription);
                _repository.Create(new Subscription
                {
                    Id = 2,
                    UserId = "testUser2",
                    Type = "Yearly",
                    Status = "Active",
                    StartDate = DateTime.Now,
                });
                var result = _repository.GetAll();

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.ToList().Count);
            }

            [TestMethod]
            public void GetByUserTest()
            {
                _repository.Create(new Subscription
                {
                    Id = 3,
                    UserId = "testUser3",
                    Type = "Yearly",
                    Status = "Active",
                    StartDate = DateTime.Now,
                });
                var result = _repository.GetByUser("testUser3");

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.ToList().Count);
            }

            [TestMethod]
            public void GetByStatus()
            {
                _repository.Create(new Subscription
                {
                    Id = 3,
                    UserId = "testUser3",
                    Type = "Yearly",
                    Status = "InActive",
                    StartDate = DateTime.Now,
                });
                _repository.Create(new Subscription
                {
                    Id = 4,
                    UserId = "testUser4",
                    Type = "Montly",
                    Status = "Active",
                    StartDate = DateTime.Now,
                });
                var result = _repository.GetByStatus("Active");

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.ToList().Count);
            }
            [TestMethod]
            public void GetByTypeTest()
            {
                _repository.Create(new Subscription
                {
                    Id = 3,
                    UserId = "testUser3",
                    Type = "Yearly",
                    Status = "InActive",
                    StartDate = DateTime.Now,
                });
                _repository.Create(new Subscription
                {
                    Id = 4,
                    UserId = "testUser4",
                    Type = "Montly",
                    Status = "Active",
                    StartDate = DateTime.Now,
                });
                var result = _repository.GetByType("Yearly");

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.ToList().Count);
            }
            [TestMethod]
            public void GetCountTest()
            {
                _repository.Create(_testSubscription);
                
                var result = _repository.GetCount();

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result);
            }
            [TestMethod]
            public void GetSubscriptionsByStartDateTest()
            {
                _repository.Create(_testSubscription);

                // Предположим, что у вашей подписки есть свойство StartDate
                DateTime startDate = _testSubscription.StartDate;

                var result = _repository.GetByStartDate(startDate);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.ToList().Count);
                // Дополнительные проверки: убедитесь, что возвращенная подписка действительно имеет ожидаемую дату начала
                Assert.AreEqual(startDate, result.First().StartDate);
            }   
        }
    }   
}
