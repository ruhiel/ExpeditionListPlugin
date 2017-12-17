using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpeditionListPlugin
{
    public class ExpeditionInfo
    {
        /// <summary>
        /// 駆逐艦
        /// </summary>
        public static readonly string DESTROYER = "(?<駆>駆逐艦)";

        /// <summary>
        /// 軽巡洋艦
        /// </summary>
        public static readonly string LIGHTCRUISER = "(?<軽>軽巡洋艦)";

        /// <summary>
        /// 軽巡洋艦
        /// </summary>
        public static readonly string HEAVYCRUISER = "(?<重>重巡洋艦)";

        /// <summary>
        /// 航空戦艦
        /// </summary>
        public static readonly string AVIATIONBATTLESHIP = "(?<航戦>航空戦艦)";

        /// <summary>
        /// 潜水艦
        /// </summary>
        public static readonly string SUBMARINE = "(?<潜>潜水艦|潜水空母)";

        /// <summary>
        /// 練習巡洋艦
        /// </summary>
        public static readonly string TRAININGCRUISER = "(?<練>練習巡洋艦)";

        /// <summary>
        /// 水上機母艦
        /// </summary>
        public static readonly string SEAPLANETENDER = "(?<水母>水上機母艦)";

        /// <summary>
        /// 潜水母艦
        /// </summary>
        public static readonly string SUBMARINETENDER = "(?<潜母艦>潜水母艦)";

        /// <summary>
        /// 空母
        /// </summary>
        public static readonly string AIRCRAFTCARRIER = "(?<空母>正規空母|装甲空母|軽空母|水上機母艦)";

        /// <summary>
        /// 海防艦
        /// </summary>
        public static readonly string ESCORT = "(?<海防>海防艦)";

        /// <summary>
        /// 護衛空母
        /// </summary>
        public static readonly string ESCORTECARRIER = "(?<護母>護衛空母)";

        /// <summary>
        /// ドラム缶
        /// </summary>
        public static readonly string DRUMCANISTER = "ドラム缶(輸送用)";

        public string Area { get; set; }
        public string EName { get; set; }
        public string Time { get; set; }
        public bool isSuccess2 { get; set; } = false;
        public bool isSuccess3 { get; set; } = false;
        public bool isSuccess4 { get; set; } = false;
        public int Lv { get; set; }
        public int? SumLv { get; set; }
        public int ShipNum { get; set; }
        /// <summary>
        /// 必要艦種
        /// </summary>
        /// <value>
        /// 必要艦種名と数
        /// </value>
        public Dictionary<string, int>[] RequireShipType { get; set; }
        public Dictionary<string, int> RequireItemNum { get; set; }
        public Dictionary<string, int> RequireItemShipNum { get; set; }
        public string FlagShipType { get; set; }
        public string FlagShipTypeText
        {
            get
            {
                if (null == FlagShipType) return "";

                Regex regexp = new Regex("<(.+)>(.+)");
                var match = regexp.Match(FlagShipType);

                return match.Success ? match.Groups[1].ToString() : "";
            }
        }

        /// <summary>
        /// 必要艦種テキスト
        /// </summary>
        public string RequireShipTypeText
        {
            get
            {
                if (null == RequireShipType) return this.RequireSumShipTypeText;

                Regex re = new Regex("<(.+)>");
                var list = new List<string>();
                var list2 = new List<string>();
                foreach (var r in RequireShipType)
                {
                    foreach (var pair in r)
                    {
                        String regexText = pair.Key;
                        Match match = re.Match(regexText);
                        if (match.Success)
                        {
                            list.Add(match.Groups[1].Value + pair.Value);
                        }
                    }
                    list2.Add(string.Join(" ", list));
                    list.Clear();
                }
                return string.Join(" or ", list2) + " " + this.RequireSumShipTypeText;
            }
        }

        public string RequireDrum
        {
            get
            {
                if (null == RequireItemNum || null == RequireItemShipNum) return "";
                return RequireItemShipNum[DRUMCANISTER] + "隻 " + RequireItemNum[DRUMCANISTER] + "個";
            }
        }

        /// <summary>
        /// 必要合算艦種
        /// </summary>
        public string[] RequireSumShipType { get; set; }

        /// <summary>
        /// 必要合算艦種数
        /// </summary>
        public int RequireSumShipTypeNum { get; set; }

        /// <summary>
        /// 必要合算艦種テキスト
        /// </summary>
        public string RequireSumShipTypeText
        {
            get
            {
                if (null == RequireSumShipType) return "";

                Regex re = new Regex("<(.+)>");
                var list = new List<string>();
                foreach (var pair in RequireSumShipType)
                {
                    String regexText = pair;
                    Match match = re.Match(regexText);
                    if (match.Success)
                    {
                        list.Add(match.Groups[1].Value);
                    }
                }
                return string.Join(",", list) +"合計" + RequireSumShipTypeNum.ToString();
            }
        }

        /// <summary>
        /// 合計対空値
        /// </summary>
        public int? SumAA { get; set; }

        /// <summary>
        /// 合計対潜値
        /// </summary>
        public int? SumASW { get; set; }

        /// <summary>
        /// 合計索敵値
        /// </summary>
        public int? SumViewRange { get; set; }

        public int? Fuel { get; set; }
        public int? Ammunition { get; set; }
        public int? Bauxite { get; set; }
        public int? Steel { get; set; }
        public int? InstantBuildMaterials { get; set; }
        public int? InstantRepairMaterials { get; set; }
        public int? DevelopmentMaterials { get; set; }

        /// <summary>
        /// 家具箱
        /// </summary>
        public string FurnitureBox { get; set; }

        public bool isFuelNull { get { return Fuel == null; } }
        public bool isAmmunitionNull { get { return Ammunition == null; } }
        public bool isSteelNull { get { return Steel == null; } }
        public bool isBauxiteNull { get { return Bauxite == null; } }

        public static ExpeditionInfo[] _ExpeditionTable = new ExpeditionInfo[]
        {
            new ExpeditionInfo {Area="鎮守", EName="練習航海", Time="00:15", Lv=1, ShipNum=2, Ammunition=30},
            new ExpeditionInfo {Area="鎮守", EName="長距離練習航海", Time="00:30", Lv=2, ShipNum=4, Ammunition=100, Steel=30, InstantRepairMaterials=1},
            new ExpeditionInfo {Area="鎮守", EName="警備任務", Time="00:20", Lv=3, ShipNum=3, Fuel=30, Ammunition=30, Steel=40},
            new ExpeditionInfo {Area="鎮守", EName="対潜警戒任務", Time="00:50", Lv=3, RequireShipType=new[] {
             new Dictionary<string, int> { { LIGHTCRUISER, 1 }, { DESTROYER, 2 } },
             new Dictionary<string, int> { { DESTROYER, 1 },{ ESCORT ,3 } } ,
             new Dictionary<string, int> { { LIGHTCRUISER, 1 },{ ESCORT ,2 } }, 
             new Dictionary<string, int> { { TRAININGCRUISER, 1 }, { ESCORT, 2 } },
             new Dictionary<string, int> { { ESCORTECARRIER, 1 }, { ESCORT, 2 } },
             new Dictionary<string, int> { { ESCORTECARRIER, 1 }, { DESTROYER, 2 } }
            }, ShipNum=3, Ammunition=60, InstantRepairMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="鎮守", EName="海上護衛任務", Time="01:30", Lv=3, RequireShipType=new[] {
             new Dictionary<string, int> { { LIGHTCRUISER, 1 }, { DESTROYER, 2 } },
             new Dictionary<string, int> { { DESTROYER, 1 },{ ESCORT ,3 } } ,
             new Dictionary<string, int> { { LIGHTCRUISER, 1 },{ ESCORT ,2 } },
             new Dictionary<string, int> { { TRAININGCRUISER, 1 }, { ESCORT, 2 } },
             new Dictionary<string, int> { { ESCORTECARRIER, 1 }, { ESCORT, 2 } },
             new Dictionary<string, int> { { ESCORTECARRIER, 1 }, { DESTROYER, 2 } }
            }, ShipNum=4, Fuel=200, Ammunition=200, Steel=20, Bauxite=20, },
            new ExpeditionInfo {Area="鎮守", EName="防空射撃演習", Time="00:40", Lv=4, ShipNum=4, Bauxite=80, FurnitureBox="小1"},
            new ExpeditionInfo {Area="鎮守", EName="観艦式予行", Time="01:00", Lv=5,ShipNum=6, Steel=50, Bauxite=30, InstantBuildMaterials=1},
            new ExpeditionInfo {Area="鎮守", EName="観艦式", Time="03:00", Lv=6, ShipNum=6, Fuel=50, Ammunition=100, Steel=50, Bauxite=50, InstantBuildMaterials=2, DevelopmentMaterials=1},

            new ExpeditionInfo {Area="鎮守", EName="兵站強化任務", Time="00:25", Lv=5, ShipNum=4, Fuel=45, Ammunition=45, RequireSumShipType=new string[]{ DESTROYER , ESCORT }, RequireSumShipTypeNum = 3 },
            new ExpeditionInfo {Area="鎮守", EName="海峡警備行動", Time="00:55", Lv=20, ShipNum=4, Fuel=70, Ammunition=40, Bauxite=10,DevelopmentMaterials=1, InstantRepairMaterials=1, RequireSumShipType=new string[]{ DESTROYER , ESCORT }, RequireSumShipTypeNum = 4 , SumAA=70, SumASW=180},
            new ExpeditionInfo {Area="鎮守", EName="長時間対潜警戒", Time="02:15", Lv=35,SumLv=185, ShipNum=5, Fuel=120, Steel=60,Bauxite=60, InstantRepairMaterials=1, DevelopmentMaterials=2, RequireShipType=new[] {new Dictionary<string, int>() { { LIGHTCRUISER, 1 } } }, RequireSumShipType=new string[]{ DESTROYER , ESCORT }, RequireSumShipTypeNum =3 ,SumASW=280 },

            new ExpeditionInfo {Area="南西", EName="タンカー護衛任務", Time="04:00", Lv=3, RequireShipType=new[] {
             new Dictionary<string, int> { { LIGHTCRUISER, 1 }, { DESTROYER, 2 } },
             new Dictionary<string, int> { { DESTROYER, 1 },{ ESCORT ,3 } } ,
             new Dictionary<string, int> { { LIGHTCRUISER, 1 },{ ESCORT ,2 } },
             new Dictionary<string, int> { { TRAININGCRUISER, 1 }, { ESCORT, 2 } },
             new Dictionary<string, int> { { ESCORTECARRIER, 1 }, { ESCORT, 2 } },
             new Dictionary<string, int> { { ESCORTECARRIER, 1 }, { DESTROYER, 2 } }
            }, ShipNum=4, Fuel=350, InstantRepairMaterials=2, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="強行偵察任務", Time="01:30", Lv=3, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 2 } } }, ShipNum=3, Ammunition=50, Bauxite=30, InstantRepairMaterials=1, InstantBuildMaterials=1},
            new ExpeditionInfo {Area="南西", EName="ボーキサイト輸送任務", Time="05:00", Lv=6, RequireShipType=new[] { new Dictionary<string, int> { {DESTROYER, 2 } } }, ShipNum=4, Bauxite=250, InstantRepairMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="資源輸送任務", Time="08:00", Lv=4, RequireShipType=new[] { new Dictionary<string, int> { { DESTROYER, 2 } } }, ShipNum=4, Ammunition=250, Steel=200, DevelopmentMaterials=1, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南西", EName="鼠輸送作戦", Time="04:00", Lv=5, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 } , {DESTROYER, 4 } } }, ShipNum=6, Fuel=240, Ammunition=300, InstantRepairMaterials=2, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="包囲陸戦隊撤収作戦", Time="06:00", Lv=6, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 } , {DESTROYER, 3 } } }, ShipNum=6, Ammunition=240, Steel=200, InstantRepairMaterials=1, DevelopmentMaterials=1},
            new ExpeditionInfo {Area="南西", EName="囮機動部隊支援作戦", Time="12:00", Lv=8, RequireShipType=new[] { new Dictionary<string, int> { { AIRCRAFTCARRIER, 2 }, {DESTROYER, 2 } } }, ShipNum=6, Steel=300, Bauxite=400, DevelopmentMaterials=1, FurnitureBox="大1"},
            new ExpeditionInfo {Area="南西", EName="艦隊決戦援護作戦", Time="15:00", Lv=10, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 }, {DESTROYER, 2 } } }, ShipNum=6, Fuel=500, Ammunition=500, Steel=200, Bauxite=200, InstantBuildMaterials=2, DevelopmentMaterials=2},

            new ExpeditionInfo {Area="南西", EName="南西方面航空偵察作戦", Time="00:35", Lv=40,SumLv=150, ShipNum=6, Steel=10,Bauxite=30,InstantRepairMaterials=1,FurnitureBox="小1", RequireShipType =new[] { new Dictionary<string, int>() { { SEAPLANETENDER, 1 },{ LIGHTCRUISER, 1 },{ DESTROYER,2} } }, SumAA=200,SumASW=200,SumViewRange=140 },

            new ExpeditionInfo {Area="北方", EName="敵地偵察作戦", Time="00:45", Lv=20, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 }, { DESTROYER, 3 } } }, ShipNum=6, Fuel=70, Ammunition=70, Steel=50},
            new ExpeditionInfo {Area="北方", EName="航空機輸送作戦", Time="05:00", Lv=15, RequireShipType=new[] { new Dictionary<string, int> { { AIRCRAFTCARRIER, 1 }, { DESTROYER, 3 } } }, ShipNum=6, Steel=300, Bauxite=100, InstantRepairMaterials=1},
            new ExpeditionInfo {Area="北方", EName="北号作戦", Time="06:00", Lv=20, RequireShipType=new[] { new Dictionary<string, int> { { AVIATIONBATTLESHIP, 2 }, { DESTROYER, 2 } } }, ShipNum=6, Fuel=400, Steel=50, Bauxite=30, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="潜水艦哨戒任務",  Time="02:00", RequireShipType=new[] { new Dictionary<string, int> { { SUBMARINE, 1 }, { LIGHTCRUISER, 1 } } }, Lv=1, ShipNum=2, Steel=150, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="北方鼠輸送作戦", Time="02:20", Lv=15, SumLv=30, RequireItemNum=new Dictionary<string, int> { { DRUMCANISTER, 3 } }, RequireItemShipNum=new Dictionary<string, int>() { { DRUMCANISTER, 3 } }, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 }, {DESTROYER, 4 } } }, ShipNum=5, Fuel=320, Ammunition=270, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="艦隊演習", Time="03:00", Lv=30, SumLv=45, RequireShipType=new[] { new Dictionary<string, int> { { HEAVYCRUISER, 1 }, { LIGHTCRUISER, 1 } , {DESTROYER, 2 } } }, ShipNum=6, Ammunition=10},
            new ExpeditionInfo {Area="北方", EName="航空戦艦運用演習", Time="04:00", Lv=50, SumLv=200, RequireShipType=new[] { new Dictionary<string, int> { { AVIATIONBATTLESHIP, 2 }, {DESTROYER, 2 } } }, ShipNum=6, Ammunition=20, Bauxite=100},
            new ExpeditionInfo {Area="北方", EName="北方航路海上護衛", Time="08:20", Lv=50, SumLv=200, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 }, {DESTROYER, 4 } }}, ShipNum=6, Fuel=500, Bauxite=150, InstantRepairMaterials=1, DevelopmentMaterials=2, FlagShipType = LIGHTCRUISER},

            new ExpeditionInfo {Area="西方", EName="通商破壊作戦", Time="40:00", Lv=25, RequireShipType=new[] { new Dictionary<string, int> { { HEAVYCRUISER, 2 }, {DESTROYER, 2 } } }, ShipNum=4, Fuel=900, Steel=500, },
            new ExpeditionInfo {Area="西方", EName="敵母港空襲作戦", Time="80:00", Lv=30, RequireShipType=new[] { new Dictionary<string, int> { { AIRCRAFTCARRIER, 1 },  { LIGHTCRUISER, 1 } , {DESTROYER, 2 } } }, ShipNum=4, Bauxite=900, InstantRepairMaterials=3},
            new ExpeditionInfo {Area="西方", EName="潜水艦通商破壊作戦", Time="20:00", Lv=1, RequireShipType=new[] { new Dictionary<string, int> { { SUBMARINE, 2 } } }, ShipNum=2, Steel=800, DevelopmentMaterials=1, FurnitureBox="小2"},
            new ExpeditionInfo {Area="西方", EName="西方海域封鎖作戦", Time="25:00", Lv=30, RequireShipType=new[] { new Dictionary<string, int> { { SUBMARINE, 3 } } }, ShipNum=3, Steel=900, Bauxite=350, DevelopmentMaterials=2, FurnitureBox="中2"},
            new ExpeditionInfo {Area="西方", EName="潜水艦派遣演習", Time="24:00", Lv=50, RequireShipType=new[] { new Dictionary<string, int> { { SUBMARINE, 3 } } }, ShipNum=3, Bauxite=100, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="西方", EName="潜水艦派遣作戦", Time="48:00", Lv=55, RequireShipType=new[] { new Dictionary<string, int> { { SUBMARINE, 4 } } }, ShipNum=4, Bauxite=100, DevelopmentMaterials=3},
            new ExpeditionInfo {Area="西方", EName="海外艦との接触", Time="02:00", Lv=60,  SumLv=200, RequireShipType=new[]{ new Dictionary<string, int>() { { SUBMARINE, 4 } } }, ShipNum=4, Ammunition=30, FurnitureBox="小1"},
            new ExpeditionInfo {Area="西方", EName="遠洋練習航海", Time="24:00", Lv=5, RequireShipType=new[] { new Dictionary<string, int> { { TRAININGCRUISER, 1 }, { DESTROYER, 2 } } }, ShipNum=3, Fuel=50, Ammunition=50, Steel=50, Bauxite=50, FurnitureBox="大1", FlagShipType = TRAININGCRUISER},

            new ExpeditionInfo {Area="南方", EName="MO作戦", Time="07:00", Lv=40, RequireShipType=new[]{new Dictionary<string, int>() { { AIRCRAFTCARRIER, 2 }, { HEAVYCRUISER, 1 }, {DESTROYER, 1 } } }, ShipNum=6, Fuel=0, Ammunition=0, Steel=240, Bauxite=280, DevelopmentMaterials=1, FurnitureBox="小2"},
            new ExpeditionInfo {Area="南方", EName="水上機基地建設", Time="09:00", Lv=30, RequireShipType=new[]{ new Dictionary<string, int>() { { SEAPLANETENDER, 2 }, { LIGHTCRUISER, 1 }, { DESTROYER, 1 } } }, ShipNum=6, Fuel=480, Ammunition=0, Steel=200, Bauxite=200, InstantRepairMaterials=1, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南方", EName="東京急行", Time="02:45", Lv=50, SumLv=200, RequireItemNum=new Dictionary<string, int>() { { DRUMCANISTER, 4 } } , RequireItemShipNum=new Dictionary<string, int>() { { DRUMCANISTER, 3 } }, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 }, { DESTROYER, 5 } } }, ShipNum=6, Fuel=0, Ammunition=380, Steel=270, Bauxite=0, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南方", EName="東京急行(弐)", Time="02:55", Lv=65, SumLv=240, RequireItemNum=new Dictionary<string, int>() { { DRUMCANISTER, 8 } }, RequireItemShipNum=new Dictionary<string, int>() { { DRUMCANISTER, 4 } }, RequireShipType=new[] { new Dictionary<string, int> { {DESTROYER, 5 } } }, ShipNum=6, Fuel=420, Ammunition=0, Steel=200, Bauxite=0, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南方", EName="遠洋潜水艦作戦", Time="30:00", Lv=3, SumLv=180, RequireShipType=new[] { new Dictionary<string, int> { { SUBMARINETENDER, 1 }, { SUBMARINE, 4 } } }, ShipNum=5, Fuel=0, Ammunition=0, Steel=300, Bauxite=0, InstantRepairMaterials=2, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南方", EName="水上機前線輸送", Time="06:50", Lv=25, SumLv=150, RequireShipType=new[] { new Dictionary<string, int> { { LIGHTCRUISER, 1 }, { SEAPLANETENDER, 2 }, {DESTROYER, 2 } } }, ShipNum=6, Fuel=300, Ammunition=300, Steel=0, Bauxite=100, InstantRepairMaterials=1, FurnitureBox="小3", FlagShipType = LIGHTCRUISER},
        };

        public void Check()
        {
            isSuccess2 = CheckAll(2);
            isSuccess3 = CheckAll(3);
            isSuccess4 = CheckAll(4);
        }

        public static ExpeditionInfo[] ExpeditionList
        {
            get { return _ExpeditionTable; }
            set { _ExpeditionTable = value; }
        }

        public bool CheckAll(int index)
        {
            return CheckShipNum(index) && FlagshipLvCheck(index) && SumLvCheck(index)
                        && RequireShipTypeCheck(index) && RequireItemCheck(index)
                        && FlagShipTypeCheck(index)
                        && RequireSumShipTypeCheck(index)
                        && SumAACheck(index)
                        && SumASWCheck(index)
                        && SumViewRangeCheck(index);
        }

        /// <summary>
        /// 艦数チェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool CheckShipNum(int index)
        {
            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Length >= ShipNum;
        }

        /// <summary>
        /// 旗艦Lvチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool FlagshipLvCheck(int index)
        {
            if (KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Length == 0) return false;

            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.First().Level >= Lv;
        }

        /// <summary>
        /// 合計Lvチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool SumLvCheck(int index)
        {
            if (null == SumLv) return true;

            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(s => s.Level).Sum() >= SumLv;
        }

        /// <summary>
        /// 艦種チェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool RequireShipTypeCheck(int index)
        {
            if (null == RequireShipType) return true;

            string SHIPTYPE_ESCORTECARRIER = "護衛空母";
            //大鷹、大鷹改二を護衛空母として扱う
            var shiptype_names = KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(s => s.Info.Name.StartsWith("大鷹") ? SHIPTYPE_ESCORTECARRIER : s.Info.ShipType.Name);

            foreach (var rst in RequireShipType)
            {  
                if (rst.All(typ => shiptype_names
                     .Where(typename => new Regex(typ.Key).Match(typename).Success).Count() >= typ.Value) == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 装備のチェック
        /// </summary{
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool RequireItemCheck(int index)
        {
            if (null == RequireItemNum || null == RequireItemShipNum) return true;
            foreach (KeyValuePair<string, int> pair in RequireItemShipNum)
            {
                int shipNum = KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Where(
                    ship => ship.EquippedItems.Any(
                        item => pair.Key.Equals(item.Item.Info.Name))).Count();

                if (shipNum < pair.Value)
                {
                    return false;
                }
            }
            foreach (KeyValuePair<string, int> pair in RequireItemNum)
            {
                int itemNum = KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(
                    ship => ship.EquippedItems.Where(
                        item => pair.Key.Equals(item.Item.Info.Name)).Count()).Sum();

                if (itemNum < pair.Value)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 旗艦の艦種チェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool FlagShipTypeCheck(int index)
        {
            if (null == FlagShipType) return true;

            if (0 == KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Count()) return false;

            var shiptype = KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.First().Info.ShipType;
            Regex regexp = new Regex(FlagShipType);
            var match = regexp.Match(shiptype.Name);

            return match.Success;
        }

        /// <summary>
        /// 必要合算艦種のチェック（駆または海防合わせて3隻というようなのに使用）
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool RequireSumShipTypeCheck(int index)
        {
            //必要合算艦種が設定されていない場合は自動成功
            if (null == RequireSumShipType) return true;

            int sum = 0;
            foreach (var siptype in RequireSumShipType)
            {
                Regex re = new Regex(siptype);
                sum += KanColleClient.Current.Homeport.Organization.Fleets[index].Ships
                     .Where(fleet => re.Match(fleet.Info.ShipType.Name).Success).Count();

                if (sum >= RequireSumShipTypeNum)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 合計対空値のチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool SumAACheck(int index)
        {
            if (null == SumAA) return true;

            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(s => s.AA).Sum(s => s.Current) >= SumAA;
        }

        /// <summary>
        /// 合計対潜値のチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool SumASWCheck(int index)
        {
            if (null == SumASW) return true;

            var not_types = new SlotItemType[] { SlotItemType.水上偵察機, SlotItemType.水上爆撃機, SlotItemType.大型飛行艇 };

            //水偵・水爆・飛行艇の対潜値の合計を取得
            var not_sum_asw = KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(
                ship => ship.EquippedItems.Where(item => not_types.Any(t => t == item.Item.Info.Type)   //水偵・水爆・飛行艇の絞込み
                    ).Sum(s => s.Item.Info.ASW)).Sum(); //対潜値の合計

            //すべての装備込み対潜値の合計を取得
            var sum_asw = KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(s => s.ASW).Sum();

            //水偵・水爆・飛行艇の対潜値を無効にする
            return sum_asw - not_sum_asw >= SumASW;
        }

        /// <summary>
        /// 合計索敵値のチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool SumViewRangeCheck(int index)
        {
            if (null == SumViewRange) return true;

            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(s => s.ViewRange).Sum() >= SumViewRange;
        }
    }
}
