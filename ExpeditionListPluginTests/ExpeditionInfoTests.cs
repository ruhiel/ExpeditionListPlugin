using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpeditionListPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionListPlugin.Tests
{
    [TestClass]
    public class ExpeditionInfoTests
    {
        [TestMethod]
        public void RequireShipTypeTextTest()
        {
            var s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "対潜警戒任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1 駆2 or 駆1 海防3 or 軽1 海防2 or 練1 海防2 or 護母1 海防2 or 護母1 駆2 ", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "兵站強化任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("駆,海防合計3", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "長時間対潜警戒").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1 駆,海防合計3", s);
        }

        [TestMethod]
        public void RequireSumParamText()
        {
            string s;
            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "兵站強化任務").RequireSumParamText;
            Console.WriteLine(s);
            Assert.AreEqual("", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "海峡警備行動").RequireSumParamText;
            Console.WriteLine(s);
            Assert.AreEqual("対空70/対潜180", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "長時間対潜警戒").RequireSumParamText;
            Console.WriteLine(s);
            Assert.AreEqual("対潜280", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "南西方面航空偵察作戦").RequireSumParamText;
            Console.WriteLine(s);
            Assert.AreEqual("対空200/対潜200/索敵140", s);
        }
    }
}