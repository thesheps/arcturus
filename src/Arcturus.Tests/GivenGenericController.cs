using Arcturus.Abstract;
using Arcturus.Concrete;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Arcturus.Tests
{
    [TestFixture]
    public class GivenGenericController : IGenericRepository<TestEntity, TestContext>
    {
        [SetUp]
        public void Setup()
        {
            _controller = new GenericController<TestEntity, TestContext>(this, new FieldMapper());
        }

        [Test]
        public void WhenIEditEntity_ThenEntityIsEdited()
        {
            _controller.Edit(0, "Surname", "Test");
            Assert.AreEqual(true, _updated);
        }

        [Test]
        public void WhenIDeleteEntity_ThenEntityIsDeleted()
        {
            _controller.Delete(0);
            Assert.AreEqual(true, _deleted);
        }

        [Test]
        public void WhenIGetIndexView_IndexIsReturned()
        {
            var test = _controller.Index();
            Assert.IsTrue(test is ActionResult);
        }

        public TestEntity Get(int key)
        {
            return new TestEntity();
        }

        public IList<TestEntity> Get(Func<TestEntity, bool> query)
        {
            throw new NotImplementedException();
        }

        public IList<TestEntity> GetAll()
        {
            return new List<TestEntity>();
        }

        public void Insert(TestEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(TestEntity item)
        {
            _updated = true;
        }

        public void Delete(TestEntity item)
        {
            _deleted = true;
        }

        public void Delete(Func<TestEntity, bool> query)
        {
            _deleted = true;
        }

        private GenericController<TestEntity, TestContext> _controller;
        private bool _updated;
        private bool _deleted;
    }

    public class TestEntity
    {
        public string Surname { get; set; }
    }
}