using Arcturus.Concrete;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Arcturus.Tests
{
    [TestFixture]
    [Ignore]
    public class GivenEntityFrameworkRepository
    {
        [SetUp]
        public void Setup()
        {
            _factory = new TestContextFactory();
            _repository = new EntityFrameworkRepository<Student, TestContext>(_factory);
        }

        [Test]
        public void WhenIInsertAnEntity_ThenTheEntityIsInserted()
        {
            _repository.Insert(new Student { DateOfBirth = DateTime.Now, FirstName = "Chris", LastName = "Shepherd" });
            var results = _repository.GetAll();
            var entity = _repository.Get(t => t.LastName == "Shepherd");
            Assert.IsNotEmpty(results);
            Assert.IsNotNull(entity);
        }

        [Test]
        public void WhenIUpdateAnEntity_ThenTheEntityIsUpdated()
        {
            _repository.Insert(new Student { DateOfBirth = DateTime.Now, FirstName = "Dave", LastName = "Chapelle" });
            var entity = _repository.Get(t => t.LastName == "Chapelle").First();
            
            entity.LastName = "Baggins";
            _repository.Update(entity);

            entity = _repository.Get(t => t.LastName == "Baggins").First();
            Assert.IsNotNull(entity);
        }

        [Test]
        public void WhenIDeleteAnEntity_ThenTheEntityIsDeleted()
        {
            _repository.Insert(new Student { DateOfBirth = DateTime.Now, FirstName = "Dave", LastName = "Baguette" });
            var entity = _repository.Get(t => t.LastName == "Baguette").First();

            _repository.Delete(entity);
            var entities = _repository.Get(t => t.LastName == "Baguette");

            Assert.IsEmpty(entities);
        }

        private EntityFrameworkRepository<Student, TestContext> _repository;
        private TestContextFactory _factory;

        public class Student
        {
            public int StudentId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
        }

        public class TestContextFactory : IDbContextFactory<TestContext>
        {
            public TestContext Create()
            {
                return new TestContext();
            }
        }

        public class TestContext : DbContext
        {
            public DbSet<Student> Student { get; set; }

            public TestContext()
            {
            }

            public TestContext(string connectionString)
                :base(connectionString)
            {
            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}