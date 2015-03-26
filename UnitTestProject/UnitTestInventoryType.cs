using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database_complex;
using Moq;
using System.Data;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestInventoryType
    {
        [TestMethod]
        public void TestSetProperties()
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
                InventoryType it = new InventoryType(1, "title", "desc");
                InventoryType.queryEx = null;
                it.Update("123", "123");
                Assert.Fail();
            }
            catch
            {

            }
        }

        [TestMethod]
        public void InsertAndNew_QueryNull_Error()
        {
            try
            {
                InventoryType.queryEx = null;
                InventoryType it = InventoryType.InsertAndNew("title", "desc");
                Assert.Fail();
            }
            catch
            {

            }
        }

        [TestMethod]
        public void Delete_QueryNull_Error()
        {
            try
            {
                InventoryType.queryEx = null;
                InventoryType it = new InventoryType(1, "title", "desc");
                it.Delete();
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

        [TestMethod]
        public void Update_Good()
        {
            InventoryType it = new InventoryType(1, "title", "desc");
            var mock = new Mock<IQueryExecuter>(MockBehavior.Strict);
            mock.Setup(st => st.ExecuteNonQuery(It.IsAny<CommandType>(), It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>()));
            InventoryType.queryEx = mock.Object;
            Assert.AreEqual("title", it.title);
            it.Update("newTitle", "123");
            Assert.AreEqual("newTitle", it.title);
            mock.Verify();
            mock.Verify(st => st.ExecuteNonQuery(CommandType.Text, It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>()), Times.Once());
        }

        [TestMethod]
        public void InsertAndNew_Good()
        {
            var mock = new Mock<IQueryExecuter>(MockBehavior.Strict);
            mock.Setup(st => st.ExecuteNonQuery(It.IsAny<CommandType>(), It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>()));
            mock.Setup(st => st.ExecuteScalar(It.IsAny<CommandType>(), It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>())).Returns(123);
            InventoryType.queryEx = mock.Object;

            InventoryType it = InventoryType.InsertAndNew("title", "desc");

            Assert.AreEqual(123, it.id);

            mock.Verify();
            mock.Verify(st => st.ExecuteNonQuery(CommandType.Text, It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>()), Times.Once());
            mock.Verify(st => st.ExecuteScalar(CommandType.Text, It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>()), Times.Once());
        }

        [TestMethod]
        public void Delete_Good()
        {
            var mock = new Mock<IQueryExecuter>(MockBehavior.Strict);
            mock.Setup(st => st.ExecuteNonQuery(It.IsAny<CommandType>(), It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>()));
            InventoryType.queryEx = mock.Object;

            InventoryType it = new InventoryType(1, "title", "desc");
            it.Delete();

            mock.Verify();
            mock.Verify(st => st.ExecuteNonQuery(CommandType.Text, It.IsAny<string>(),
                It.IsAny<Dictionary<string, object>>()), Times.Once());
        }
    }
    
}
