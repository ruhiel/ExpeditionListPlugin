﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpeditionListPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionListPlugin.Tests
{
    [TestClass()]
    public class ExpeditionInfoTests
    {
        [TestMethod()]
        public void RequireShipTypeTextTest()
        {
            var s = ExpeditionInfo._ExpeditionTable[3].RequireShipTypeText;
            Console.WriteLine(s);
            s.Is("軽1 駆2 or 駆1 海防3 or 軽1 海防2 or 練1 海防2 or 護母1 海防2 or 護母1 駆2 ");

            s = ExpeditionInfo._ExpeditionTable[8].RequireShipTypeText;
            Console.WriteLine(s);
            s.Is("駆,海防合計3");

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "長時間対潜警戒").RequireShipTypeText;
            Console.WriteLine(s);
            s.Is("軽1 駆,海防合計3");
        }

        [TestMethod()]
        public void RequireSumParamText()
        {
            string s;
            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "兵站強化任務").RequireSumParamText;
            Console.WriteLine(s);
            s.Is("");

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "海峡警備行動").RequireSumParamText;
            Console.WriteLine(s);
            s.Is("対空70/対潜180");

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "長時間対潜警戒").RequireSumParamText;
            Console.WriteLine(s);
            s.Is("対潜280");

            s = ExpeditionInfo._ExpeditionTable.First(t => t.EName == "南西方面航空偵察作戦").RequireSumParamText;
            Console.WriteLine(s);
            s.Is("対空200/対潜200/索敵140");
        }
    }
}