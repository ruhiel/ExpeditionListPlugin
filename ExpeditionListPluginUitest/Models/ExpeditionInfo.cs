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
        public static readonly string AIRCRAFTCARRIER = "(?<空母>正規空母|装甲空母|軽空母|水上機母艦|護衛空母)";

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
        public Dictionary<Tuple<string, int>[], Tuple<string, int>[]> RequireShipType { get; set; }
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
        public string RequireShipTypeText => string.Join("/", new[] { RequireShipTypeTextInner, RequireSumShipTypeText }.Where(x => x != string.Empty));

        public string RequireShipTypeTextInner => RequireShipType == null ? string.Empty :
            string.Join(" ",
                RequireShipType.Select(x =>
                    string.Join("", x.Key.Select(y => $"{Regex.Replace(y.Item1, @".+<(.+)>.+", "$1")}{y.Item2}"))
                    + (x.Value == null ? string.Empty : string.Join("or", x.Value.Select(z => $"{Regex.Replace(z.Item1, @".+<(.+)>.+", "$1")}{z.Item2}")))));

        public string RequireDrum => (null == RequireItemNum || null == RequireItemShipNum) ? string.Empty : $"{RequireItemShipNum[DRUMCANISTER]}隻 {RequireItemNum[DRUMCANISTER]}個";

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
        public string RequireSumShipTypeText => null == RequireSumShipType ? string.Empty : $"{string.Join(",", RequireSumShipType.Select(x => $"{Regex.Replace(x, @".+<(.+)>.+", "$1")}"))}合計{RequireSumShipTypeNum}";

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

        public static readonly string AA = "対空";
        public static readonly string ASW = "対潜";
        public static readonly string VIEWRANGE = "索敵";

        /// <summary>
        /// 第二～四艦隊パラメータ
        /// </summary>
        /// <value>
        /// 対空、対潜、索敵が要件を満たしているか。条件がない場合はnull
        /// </value>
        public Dictionary<int, Dictionary<string, bool?>> isParameter { get; set; } = new Dictionary<int, Dictionary<string, bool?>>();

        /// <summary>
        /// 第二～四艦隊パラメータの要件を満たしているか
        /// </summary>
        /// <value>
        /// 対空、対潜、索敵がひとつでもfalseの場合はfalseそうでない場合はtrue
        /// </value>
        public Dictionary<int,bool> isParameterValid
        {
            get
            {
                for (var i = 2; i <= 4; i++)
                {
                    _isParameterValid[i] = isParameter[i].Any(p => p.Value == false) == true ? false : true;
                }

                return _isParameterValid;
            }
        }

        private Dictionary<int, bool> _isParameterValid = new Dictionary<int, bool>();

        /// <summary>
        /// 必須合計パラメータ
        /// </summary>
        public string RequireSumParamText
        {
            get
            {
                var buf = new string[] {
                    SumAA != null ? AA + SumAA.ToString() : string.Empty,
                    SumASW != null ? ASW + SumASW.ToString() : string.Empty,
                    SumViewRange != null ? VIEWRANGE + SumViewRange.ToString() : string.Empty};

                return string.Join("/", buf.Where(s => s.Length > 0));
            }
        }

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
            new ExpeditionInfo {Area="鎮守", EName="対潜警戒任務", Time="00:50", Lv=3, RequireShipType= new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> {
                    { new Tuple<string, int>[] {Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 2)}, null },
                    { new Tuple<string, int>[] {Tuple.Create(DESTROYER, 1), Tuple.Create(ESCORT, 3)}, null },
                    { new Tuple<string, int>[] {Tuple.Create(ESCORT, 2)}, new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(TRAININGCRUISER, 1) } },
                    { new Tuple<string, int>[] {Tuple.Create(ESCORTECARRIER, 1)}, new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2), Tuple.Create(ESCORT, 2) } },
                }, ShipNum=3, Ammunition=60, InstantRepairMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="鎮守", EName="海上護衛任務", Time="01:30", Lv=3, RequireShipType = new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> {
                    { new Tuple<string, int>[] {Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 2)}, null },
                    { new Tuple<string, int>[] {Tuple.Create(DESTROYER, 1), Tuple.Create(ESCORT, 3)}, null },
                    { new Tuple<string, int>[] {Tuple.Create(ESCORT, 2)}, new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(TRAININGCRUISER, 1) } },
                    { new Tuple<string, int>[] {Tuple.Create(ESCORTECARRIER, 1)}, new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2), Tuple.Create(ESCORT, 2) } },
                }, ShipNum=4, Fuel=200, Ammunition=200, Steel=20, Bauxite=20, },
            new ExpeditionInfo {Area="鎮守", EName="防空射撃演習", Time="00:40", Lv=4, ShipNum=4, Bauxite=80, FurnitureBox="小1"},
            new ExpeditionInfo {Area="鎮守", EName="観艦式予行", Time="01:00", Lv=5,ShipNum=6, Steel=50, Bauxite=30, InstantBuildMaterials=1},
            new ExpeditionInfo {Area="鎮守", EName="観艦式", Time="03:00", Lv=6, ShipNum=6, Fuel=50, Ammunition=100, Steel=50, Bauxite=50, InstantBuildMaterials=2, DevelopmentMaterials=1},

            new ExpeditionInfo {Area="鎮守", EName="兵站強化任務", Time="00:25", Lv=5, ShipNum=4, Fuel=45, Ammunition=45, RequireSumShipType=new string[]{ DESTROYER , ESCORT }, RequireSumShipTypeNum = 3 },
            new ExpeditionInfo {Area="鎮守", EName="海峡警備行動", Time="00:55", Lv=20, ShipNum=4, Fuel=70, Ammunition=40, Bauxite=10,DevelopmentMaterials=1, InstantRepairMaterials=1, RequireSumShipType=new string[]{ DESTROYER , ESCORT }, RequireSumShipTypeNum = 4 , SumAA=70, SumASW=180},
            new ExpeditionInfo {Area="鎮守", EName="長時間対潜警戒", Time="02:15", Lv=35,SumLv=185, ShipNum=5, Fuel=120, Steel=60,Bauxite=60, InstantRepairMaterials=1, DevelopmentMaterials=2, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1) }, null } }, RequireSumShipType=new string[]{ DESTROYER , ESCORT }, RequireSumShipTypeNum =3 ,SumASW=280 },

            new ExpeditionInfo {Area="南西", EName="タンカー護衛任務", Time="04:00", Lv=3, RequireShipType= new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> {
                    { new Tuple<string, int>[] {Tuple.Create(LIGHTCRUISER, 1)}, new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2)} },
                    { new Tuple<string, int>[] {Tuple.Create(DESTROYER, 1)}, new Tuple<string, int>[] { Tuple.Create(ESCORT, 3) } },
                    { new Tuple<string, int>[] {Tuple.Create(ESCORT, 2)}, new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(TRAININGCRUISER, 1) } },
                    { new Tuple<string, int>[] {Tuple.Create(ESCORTECARRIER, 1)}, new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2), Tuple.Create(ESCORT, 2) } },
            }, ShipNum=4, Fuel=350, InstantRepairMaterials=2, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="強行偵察任務", Time="01:30", Lv=3, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 2) }, null } }, ShipNum=3, Ammunition=50, Bauxite=30, InstantRepairMaterials=1, InstantBuildMaterials=1},
            new ExpeditionInfo {Area="南西", EName="ボーキサイト輸送任務", Time="05:00", Lv=6, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=4, Bauxite=250, InstantRepairMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="資源輸送任務", Time="08:00", Lv=4, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=4, Ammunition=250, Steel=200, DevelopmentMaterials=1, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南西", EName="鼠輸送作戦", Time="04:00", Lv=5, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 4) }, null } }, ShipNum=6, Fuel=240, Ammunition=300, InstantRepairMaterials=2, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="包囲陸戦隊撤収作戦", Time="06:00", Lv=6, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1) }, new Tuple<string, int>[] { Tuple.Create(DESTROYER, 3) } } }, ShipNum=6, Ammunition=240, Steel=200, InstantRepairMaterials=1, DevelopmentMaterials=1},
            new ExpeditionInfo {Area="南西", EName="囮機動部隊支援作戦", Time="12:00", Lv=8, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(AIRCRAFTCARRIER, 2) }, new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2) } } }, ShipNum=6, Steel=300, Bauxite=400, DevelopmentMaterials=1, FurnitureBox="大1"},
            new ExpeditionInfo {Area="南西", EName="艦隊決戦援護作戦", Time="15:00", Lv=10, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1) }, new Tuple<string, int>[] { Tuple.Create(DESTROYER, 2) } } }, ShipNum=6, Fuel=500, Ammunition=500, Steel=200, Bauxite=200, InstantBuildMaterials=2, DevelopmentMaterials=2},

            new ExpeditionInfo {Area="南西", EName="南西方面航空偵察作戦", Time="00:35", Lv=40,SumLv=150, ShipNum=6, Steel=10,Bauxite=30,InstantRepairMaterials=1,FurnitureBox="小1", RequireShipType =new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SEAPLANETENDER, 1), Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 2) }, null } }, SumAA=200,SumASW=200,SumViewRange=140 },

            new ExpeditionInfo {Area="北方", EName="敵地偵察作戦", Time="00:45", Lv=20, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 3) }, null } }, ShipNum=6, Fuel=70, Ammunition=70, Steel=50},
            new ExpeditionInfo {Area="北方", EName="航空機輸送作戦", Time="05:00", Lv=15, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(AIRCRAFTCARRIER, 3), Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=6, Steel=300, Bauxite=100, InstantRepairMaterials=1},
            new ExpeditionInfo {Area="北方", EName="北号作戦", Time="06:00", Lv=20, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(AVIATIONBATTLESHIP, 2), Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=6, Fuel=400, Steel=50, Bauxite=30, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="潜水艦哨戒任務",  Time="02:00", RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SUBMARINE, 1), Tuple.Create(LIGHTCRUISER, 1) }, null } }, Lv=1, ShipNum=2, Steel=150, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="北方鼠輸送作戦", Time="02:20", Lv=15, SumLv=30, RequireItemNum=new Dictionary<string, int> { { DRUMCANISTER, 3 } }, RequireItemShipNum=new Dictionary<string, int>() { { DRUMCANISTER, 3 } }, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 4) }, null } }, ShipNum=5, Fuel=320, Ammunition=270, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="艦隊演習", Time="03:00", Lv=30, SumLv=45, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(HEAVYCRUISER, 1), Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=6, Ammunition=10},
            new ExpeditionInfo {Area="北方", EName="航空戦艦運用演習", Time="04:00", Lv=50, SumLv=200, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(AVIATIONBATTLESHIP, 2), Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=6, Ammunition=20, Bauxite=100},
            new ExpeditionInfo {Area="北方", EName="北方航路海上護衛", Time="08:20", Lv=50, SumLv=200, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 4) }, null } },ShipNum=6, Fuel=500, Bauxite=150, InstantRepairMaterials=1, DevelopmentMaterials=2, FlagShipType = LIGHTCRUISER},

            new ExpeditionInfo {Area="西方", EName="通商破壊作戦", Time="40:00", Lv=25, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(HEAVYCRUISER, 2), Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=4, Fuel=900, Steel=500, },
            new ExpeditionInfo {Area="西方", EName="敵母港空襲作戦", Time="80:00", Lv=30, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(AIRCRAFTCARRIER, 1), Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 2) }, null } },ShipNum=4, Bauxite=900, InstantRepairMaterials=3},
            new ExpeditionInfo {Area="西方", EName="潜水艦通商破壊作戦", Time="20:00", Lv=1, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SUBMARINE, 2) }, null } }, ShipNum=2, Steel=800, DevelopmentMaterials=1, FurnitureBox="小2"},
            new ExpeditionInfo {Area="西方", EName="西方海域封鎖作戦", Time="25:00", Lv=30, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SUBMARINE, 3) }, null } }, ShipNum=3, Steel=900, Bauxite=350, DevelopmentMaterials=2, FurnitureBox="中2"},
            new ExpeditionInfo {Area="西方", EName="潜水艦派遣演習", Time="24:00", Lv=50, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SUBMARINE, 3) }, null } }, ShipNum=3, Bauxite=100, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="西方", EName="潜水艦派遣作戦", Time="48:00", Lv=55, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SUBMARINE, 4) }, null } }, ShipNum=4, Bauxite=100, DevelopmentMaterials=3},
            new ExpeditionInfo {Area="西方", EName="海外艦との接触", Time="02:00", Lv=60,  SumLv=200, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SUBMARINE, 4) }, null } }, ShipNum=4, Ammunition=30, FurnitureBox="小1"},
            new ExpeditionInfo {Area="西方", EName="遠洋練習航海", Time="24:00", Lv=5, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(TRAININGCRUISER, 1), Tuple.Create(DESTROYER, 2) }, null } }, ShipNum=3, Fuel=50, Ammunition=50, Steel=50, Bauxite=50, FurnitureBox="大1", FlagShipType = TRAININGCRUISER},

            new ExpeditionInfo {Area="南方", EName="MO作戦", Time="07:00", Lv=40, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(AIRCRAFTCARRIER, 2), Tuple.Create(HEAVYCRUISER, 1), Tuple.Create(DESTROYER, 1) }, null } }, ShipNum=6, Fuel=0, Ammunition=0, Steel=240, Bauxite=280, DevelopmentMaterials=1, FurnitureBox="小2"},
            new ExpeditionInfo {Area="南方", EName="水上機基地建設", Time="09:00", Lv=30, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SEAPLANETENDER, 2), Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 1) }, null } }, ShipNum=6, Fuel=480, Ammunition=0, Steel=200, Bauxite=200, InstantRepairMaterials=1, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南方", EName="東京急行", Time="02:45", Lv=50, SumLv=200, RequireItemNum=new Dictionary<string, int>() { { DRUMCANISTER, 4 } } , RequireItemShipNum=new Dictionary<string, int>() { { DRUMCANISTER, 3 } }, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(DESTROYER, 5) }, null } }, ShipNum=6, Fuel=0, Ammunition=380, Steel=270, Bauxite=0, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南方", EName="東京急行(弐)", Time="02:55", Lv=65, SumLv=240, RequireItemNum=new Dictionary<string, int>() { { DRUMCANISTER, 8 } }, RequireItemShipNum=new Dictionary<string, int>() { { DRUMCANISTER, 4 } }, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(DESTROYER, 5) }, null } },ShipNum=6, Fuel=420, Ammunition=0, Steel=200, Bauxite=0, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南方", EName="遠洋潜水艦作戦", Time="30:00", Lv=3, SumLv=180, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(SUBMARINETENDER, 1), Tuple.Create(SUBMARINE, 4), }, null } }, ShipNum=5, Fuel=0, Ammunition=0, Steel=300, Bauxite=0, InstantRepairMaterials=2, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南方", EName="水上機前線輸送", Time="06:50", Lv=25, SumLv=150, RequireShipType=new Dictionary<Tuple<string, int>[], Tuple<string, int>[]> { { new Tuple<string, int>[] { Tuple.Create(LIGHTCRUISER, 1), Tuple.Create(SEAPLANETENDER, 2), Tuple.Create(DESTROYER, 2) }, null } },ShipNum=6, Fuel=300, Ammunition=300, Steel=0, Bauxite=100, InstantRepairMaterials=1, FurnitureBox="小3", FlagShipType = LIGHTCRUISER},
        };

        public ExpeditionInfo()
        {
            for (var i = 2; i <= 4; i++)
            {
                isParameter[i] = new Dictionary<string, bool?>();
                isParameter[i].Add(AA, null);
                isParameter[i].Add(ASW, null);
                isParameter[i].Add(VIEWRANGE, null);
            }
        }

        public void Check()
        {
            isSuccess2 = CheckAll(KanColleClient.Current.Homeport.Organization.Fleets[2]);
            isSuccess3 = CheckAll(KanColleClient.Current.Homeport.Organization.Fleets[3]);
            isSuccess4 = CheckAll(KanColleClient.Current.Homeport.Organization.Fleets[4]);

            CheckParam();
        }

        private void CheckParam()
        {
            for (var i = 2; i <= 4; i++)
            {
                var flags = new Dictionary<string, bool?>();
                flags[AA] = SumAA != null ? SumAACheck(KanColleClient.Current.Homeport.Organization.Fleets[i]) : (bool?)null;
                flags[ASW] = SumASW != null ? SumASWCheck(KanColleClient.Current.Homeport.Organization.Fleets[i]) : (bool?)null;
                flags[VIEWRANGE] = SumViewRange != null ? SumViewRangeCheck(KanColleClient.Current.Homeport.Organization.Fleets[i]) : (bool?)null;

                isParameter[i] = flags;
            }
        }

        public static ExpeditionInfo[] ExpeditionList
        {
            get { return _ExpeditionTable; }
            set { _ExpeditionTable = value; }
        }

        public bool CheckAll(Fleet fleet) => CheckShipNum(fleet) &&
                        FlagshipLvCheck(fleet) &&
                        SumLvCheck(fleet) &&
                        RequireShipTypeCheck(fleet) &&
                        RequireItemCheck(fleet) &&
                        FlagShipTypeCheck(fleet) &&
                        RequireSumShipTypeCheck(fleet) &&
                        SumAACheck(fleet) &&
                        SumASWCheck(fleet) &&
                        SumViewRangeCheck(fleet);

        /// <summary>
        /// 艦数チェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool CheckShipNum(Fleet fleet) => fleet.Ships.Length >= ShipNum;

        /// <summary>
        /// 旗艦Lvチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool FlagshipLvCheck(Fleet fleet)
        {
            if (fleet.Ships.Length == 0) return false;

            return fleet.Ships.First().Level >= Lv;
        }

        /// <summary>
        /// 合計Lvチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool SumLvCheck(Fleet fleet)
        {
            if (null == SumLv) return true;

            return fleet.Ships.Select(s => s.Level).Sum() >= SumLv;
        }

        /// <summary>
        /// 艦種取得
        /// </summary>
        /// <param name="ship">艦娘</param>
        /// <returns>艦種</returns>
        private string GetShipType(Ship ship) => ship.Info.Name.StartsWith("大鷹") ? "護衛空母" : ship.Info.ShipType.Name;

        /// <summary>
        /// 艦種チェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool RequireShipTypeCheck(Fleet fleet)
        {
            if (null == RequireShipType) return true;

            return RequireShipType.Any(x =>
                x.Key.All(y => fleet.Ships.Count(i => Regex.IsMatch(GetShipType(i), y.Item1)) >= y.Item2) &&
                (x.Value == null ? true : x.Value.Any(z => fleet.Ships.Count(j => Regex.IsMatch(z.Item1, GetShipType(j))) >= z.Item2)));
        }

        /// <summary>
        /// 装備のチェック
        /// </summary{
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool RequireItemCheck(Fleet fleet)
        {
            if (null == RequireItemNum || null == RequireItemShipNum) return true;
            foreach (var pair in RequireItemShipNum)
            {
                var shipNum = fleet.Ships.Where(
                    ship => ship.EquippedItems.Any(
                        item => pair.Key.Equals(item.Item.Info.Name))).Count();

                if (shipNum < pair.Value)
                {
                    return false;
                }
            }
            foreach (var pair in RequireItemNum)
            {
                var itemNum = fleet.Ships.Select(
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
        private bool FlagShipTypeCheck(Fleet fleet)
        {
            if (null == FlagShipType) return true;

            if (0 == fleet.Ships.Length) return false;

            var shiptype = fleet.Ships.First().Info.ShipType;
            var regexp = new Regex(FlagShipType);
            var match = regexp.Match(shiptype.Name);

            return match.Success;
        }

        /// <summary>
        /// 必要合算艦種のチェック（駆または海防合わせて3隻というようなのに使用）
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool RequireSumShipTypeCheck(Fleet fleet)
        {
            //必要合算艦種が設定されていない場合は自動成功
            if (null == RequireSumShipType) return true;

            var sum = 0;
            foreach (var siptype in RequireSumShipType)
            {
                var re = new Regex(siptype);
                sum += fleet.Ships
                     .Where(f => re.Match(f.Info.ShipType.Name).Success).Count();

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
        private bool SumAACheck(Fleet fleet) => null == SumAA ? true : fleet.Ships.Sum(s => s.AA.Current + s.EquippedItems.Sum(i => i.Item.Info.AA)) >= SumAA;

        /// <summary>
        /// 合計対潜値のチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool SumASWCheck(Fleet fleet)
        {
            if (null == SumASW) return true;

            var not_types = new SlotItemType[] { SlotItemType.水上偵察機, SlotItemType.水上爆撃機, SlotItemType.大型飛行艇 };

            //水偵・水爆・飛行艇の対潜値の合計を取得
            var not_sum_asw = fleet.Ships.Select(
                ship => ship.EquippedItems.Where(item => not_types.Any(t => t == item.Item.Info.Type)   //水偵・水爆・飛行艇の絞込み
                    ).Sum(s => s.Item.Info.ASW)).Sum(); //対潜値の合計

            //すべての装備込み対潜値の合計を取得
            var sum_asw = fleet.Ships.Select(s => s.ASW).Sum();

            //水偵・水爆・飛行艇の対潜値を無効にする
            return sum_asw - not_sum_asw >= SumASW;
        }

        /// <summary>
        /// 合計索敵値のチェック
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private bool SumViewRangeCheck(Fleet fleet) => null == SumViewRange ? true : fleet.Ships.Sum(s => s.ViewRange + s.EquippedItems.Sum(e => e.Item.Info.ViewRange)) >= SumViewRange;
    }
}
