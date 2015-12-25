using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetkenAsync;

namespace UnitTest
{
    [TestClass]
    public class GetkenAsync_UnitTest1
    {
        [TestMethod]
        public void TestMethod11()
        {
            CS_GetkenAsync getken = new CS_GetkenAsync();

            #region 対象：要素１つ
            getken.Clear();
            getken.Wbuf = @"Test";
            getken.Exec();

            Assert.AreEqual(1, getken.Wcnt, "Wcnt[1]");
            Assert.AreEqual("Test", getken.Array[0], "Arry[0] = [Test]");
            #endregion

            #region 対象：要素２つ
            getken.Clear();
            getken.Wbuf = @"Test Sample";
            getken.Exec();

            Assert.AreEqual(2, getken.Wcnt, "Wcnt[2]");
            Assert.AreEqual("Test", getken.Array[0], "Arry[0] = [Test]");
            Assert.AreEqual("Sample", getken.Array[1], "Arry[1] = [Sample]");
            #endregion

            #region 対象："This is a Pen."
            getken.Clear();
            getken.Wbuf = @"This is a Pen.";
            getken.Exec();

            Assert.AreEqual(4, getken.Wcnt, "Wcnt[4]");
            Assert.AreEqual("This", getken.Array[0], "Arry[0] = [This]");
            Assert.AreEqual("is", getken.Array[1], "Arry[1] = [is]");
            Assert.AreEqual("a", getken.Array[2], "Arry[2] = [a]");
            Assert.AreEqual("Pen.", getken.Array[3], "Arry[3] = [Pen.]");
            #endregion
        }
    }
}
