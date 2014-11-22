using Arcturus.Concrete;
using NUnit.Framework;
using System;

namespace Arcturus.Tests
{
    [TestFixture]
    public class GivenFieldMapper
    {
        [Test]
        public void WhenSetField_ThenFieldIsSet()
        {
            var mapper = new FieldMapper();
            var obj = new TestClass();

            mapper.SetField(obj, "MyInt", "5");
            mapper.SetField(obj, "MyString", "Test");

            Assert.AreEqual(5, obj.MyInt);
            Assert.AreEqual("Test", obj.MyString);
        }

        [Test]
        public void WhenSetField_ThenExceptionThrown()
        {
            var mapper = new FieldMapper();
            var obj = new TestClass();

            Assert.Throws<PropertyNotFoundException>(() => mapper.SetField(obj, "MineInt", "5"));
        }
    }

    public class TestClass
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }
        public DateTime MyDate { get; set; }
    }
}