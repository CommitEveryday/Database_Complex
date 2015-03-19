using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database_complex;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestInventoryType
    {
        [TestMethod]
        public void TestSetPropertys()
        {
            string title = "title";
            string desc = "desc";
            int id = 101;
            InventoryType it = new InventoryType(id, title, desc);
            Assert.AreEqual(it.id, id);
            Assert.AreEqual(it.title, title);
            Assert.AreEqual(it.descrition, desc);
        }

        [TestMethod]
        public void UpdateObject_QueryNull_Error()
        {
            try
            {
                InventoryType it = new InventoryType(1, "test", "test");
                InventoryType.queryEx = null;
                it.Update("123", "123");
                Assert.Fail();
            }
            catch
            {

            }
        }

        [TestMethod]
        public void TestToString()
        {
            string title = "title";
            InventoryType it = new InventoryType(1, title, "test");
            Assert.AreEqual(it.ToString(), title);
        }
    }
    
}
