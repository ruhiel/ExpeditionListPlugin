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
            // 鎮守府
            var s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "練習航海").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual(string.Empty, s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "長距離練習航海").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual(string.Empty, s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "警備任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual(string.Empty, s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "対潜警戒任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆2 駆1海防3 海防2軽1or練1 護母1駆2or海防2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "海上護衛任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆2 駆1海防3 海防2軽1or練1 護母1駆2or海防2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "防空射撃演習").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual(string.Empty, s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "観艦式予行").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual(string.Empty, s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "観艦式").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual(string.Empty, s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "兵站強化任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("駆,海防合計3", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "海峡警備行動").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("駆,海防合計4", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "長時間対潜警戒").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1/駆,海防合計3", s);


            // 南西
            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "タンカー護衛任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆2 駆1海防3 海防2軽1or練1 護母1駆2or海防2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "強行偵察任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "ボーキサイト輸送任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "資源輸送任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "鼠輸送作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆4", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "包囲陸戦隊撤収作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆3", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "囮機動部隊支援作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("空母2駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "艦隊決戦援護作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "南西方面航空偵察作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("水母1軽1駆2", s);

            // 北方
            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "敵地偵察作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆3", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "航空機輸送作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("空母3駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "北号作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("航戦2駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "潜水艦哨戒任務").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("潜1軽1", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "北方鼠輸送作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆4", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "艦隊演習").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("重1軽1駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "航空戦艦運用演習").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("航戦2駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "北方航路海上護衛").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆4", s);

            // 西方
            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "通商破壊作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("重2駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "敵母港空襲作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("空母1軽1駆2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "潜水艦通商破壊作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("潜2", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "西方海域封鎖作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("潜3", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "潜水艦派遣演習").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("潜3", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "潜水艦派遣作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("潜4", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "海外艦との接触").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("潜4", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "遠洋練習航海").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("練1駆2", s);

            // 南方
            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "MO作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("空母2重1駆1", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "水上機基地建設").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("水母2軽1駆1", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "東京急行").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1駆5", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "遠洋潜水艦作戦").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("潜母艦1潜4", s);

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "水上機前線輸送").RequireShipTypeText;
            Console.WriteLine(s);
            Assert.AreEqual("軽1水母2駆2", s);
        }

        [TestMethod]
        public void RequireSumParamText()
        {
            string s;
            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "兵站強化任務").RequireSumParamText;
            Console.WriteLine(s);
            Assert.AreEqual(string.Empty, s);

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