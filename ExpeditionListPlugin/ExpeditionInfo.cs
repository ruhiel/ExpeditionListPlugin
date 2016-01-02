using Grabacr07.KanColleWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpeditionListPlugin
{
    public class ExpeditionInfo
    {
        public string Area { get; set; }
        public string EName { get; set; }
        public string Time { get; set; }
        public bool isSuccess2 { get; set; }
        public bool isSuccess3 { get; set; }
        public bool isSuccess4 { get; set; }
        public int Lv { get; set; }
        public int? SumLv { get; set; }
        public int ShipNum { get; set; }
        public Dictionary<string, int> RequireShipType { get; set; }
        public Dictionary<string, int> RequireItemNum { get; set; }
        public Dictionary<string, int> RequireItemShipNum { get; set; }
        public string RequireShipTypeText
        {
            get
            {
                if (null == RequireShipType) return "";

                Regex re = new Regex("<(.+)>");
                var list = new List<string>();
                foreach (KeyValuePair<string, int> pair in RequireShipType)
                {
                    String regexText = pair.Key;
                    Match match = re.Match(regexText);
                    if (match.Success)
                    {
                        list.Add(match.Groups[1].Value + pair.Value);
                    }
                }
                return string.Join(" ",list);
            }
        }
        public string RequireDrum
        {
            get
            {
                if (null == RequireItemNum || null == RequireItemShipNum) return "";
                return RequireItemShipNum["ドラム缶(輸送用)"] + "隻 " + RequireItemNum["ドラム缶(輸送用)"] + "個";
            }
        }
        public int? Fuel { get; set; }
        public int? Ammunition { get; set; }
        public int? Bauxite { get; set; }
        public int? Steel { get; set; }
        public int? InstantBuildMaterials { get; set; }
        public int? InstantRepairMaterials { get; set; }
        public int? DevelopmentMaterials { get; set; }
        public string FurnitureBox { get; set; }
        public bool isFuelNull { get { return Fuel == null; } }
        public bool isAmmunitionNull { get { return Ammunition == null; } }
        public bool isSteelNull { get { return Steel == null; } }
        public bool isBauxiteNull { get { return Bauxite == null; } }

        public static ExpeditionInfo[] _ExpeditionTable = new ExpeditionInfo[]
        {
            new ExpeditionInfo {Area="鎮守", EName="練習航海", Time="00:15", Lv=1, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=2, Ammunition=30},
            new ExpeditionInfo {Area="鎮守", EName="長距離練習航海", Time="00:30", Lv=2, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Ammunition=100, Steel=30, InstantRepairMaterials=1},
            new ExpeditionInfo {Area="鎮守", EName="警備任務", Time="00:20", Lv=3, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=3, Fuel=30, Ammunition=30, Steel=40},
            new ExpeditionInfo {Area="鎮守", EName="対潜警戒任務", Time="00:50", Lv=3, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=3, Ammunition=60, InstantRepairMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="鎮守", EName="海上護衛任務", Time="01:30", Lv=3, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Fuel=200, Ammunition=200, Steel=20, Bauxite=20, },
            new ExpeditionInfo {Area="鎮守", EName="防空射撃演習", Time="00:40", Lv=4, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Bauxite=80, FurnitureBox="小1"},
            new ExpeditionInfo {Area="鎮守", EName="観艦式予行", Time="01:00", Lv=5,isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Steel=50, Bauxite=30, InstantBuildMaterials=1},
            new ExpeditionInfo {Area="鎮守", EName="観艦式", Time="03:00", Lv=6, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=50, Ammunition=100, Steel=50, Bauxite=50, InstantBuildMaterials=2, DevelopmentMaterials=1},

            new ExpeditionInfo {Area="南西", EName="タンカー護衛任務", Time="04:00", Lv=3, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Fuel=350, InstantRepairMaterials=2, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="強行偵察任務", Time="01:30", Lv=3, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=3, Ammunition=50, Bauxite=30, InstantRepairMaterials=1, InstantBuildMaterials=1},
            new ExpeditionInfo {Area="南西", EName="ボーキサイト輸送任務", Time="05:00", Lv=6, RequireShipType=new Dictionary<string, int>() { {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Bauxite=250, InstantRepairMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="資源輸送任務", Time="08:00", Lv=4, RequireShipType=new Dictionary<string, int>() { {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Ammunition=250, Steel=200, DevelopmentMaterials=1, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南西", EName="鼠輸送作戦", Time="04:00", Lv=5, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 4 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=240, Ammunition=300, InstantRepairMaterials=2, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南西", EName="包囲陸戦隊撤収作戦", Time="06:00", Lv=6, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 3 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Ammunition=240, Steel=200, InstantRepairMaterials=1, DevelopmentMaterials=1},
            new ExpeditionInfo {Area="南西", EName="囮機動部隊支援作戦", Time="12:00", Lv=8, RequireShipType=new Dictionary<string, int>() { { "(?<空母>正規空母|軽空母|水上機母艦)", 2 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Steel=300, Bauxite=400, DevelopmentMaterials=1, FurnitureBox="大1"},
            new ExpeditionInfo {Area="南西", EName="艦隊決戦援護作戦", Time="15:00", Lv=10, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=500, Ammunition=500, Steel=200, Bauxite=200, InstantBuildMaterials=2, DevelopmentMaterials=2},

            new ExpeditionInfo {Area="北方", EName="敵地偵察作戦", Time="00:45", Lv=20, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 3 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=70, Ammunition=70, Steel=50},
            new ExpeditionInfo {Area="北方", EName="航空機輸送作戦", Time="05:00", Lv=15, RequireShipType=new Dictionary<string, int>() { { "(?<空母>正規空母|軽空母|水上機母艦)", 1 }, {"(?<駆>駆逐艦)", 3 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Steel=300, Bauxite=100, InstantRepairMaterials=1},
            new ExpeditionInfo {Area="北方", EName="北号作戦", Time="06:00", Lv=20, RequireShipType=new Dictionary<string, int>() { { "(?<航戦>航空戦艦)", 2 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=400, Steel=50, Bauxite=30, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="潜水艦哨戒任務",  Time="02:00", RequireShipType=new Dictionary<string, int>() { { "(?<潜>潜水艦|潜水空母)", 1 }, { "(?<軽>軽巡洋艦)", 1 } }, Lv=1, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=2, Steel=150, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="北方鼠輸送作戦", Time="02:20", Lv=15, SumLv=30, RequireItemNum=new Dictionary<string, int>() { { "ドラム缶(輸送用)", 3 } }, RequireItemShipNum=new Dictionary<string, int>() { { "ドラム缶(輸送用)", 3 } }, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 4 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=5, Fuel=320, Ammunition=270, FurnitureBox="小1"},
            new ExpeditionInfo {Area="北方", EName="艦隊演習", Time="03:00", Lv=30, SumLv=45, RequireShipType=new Dictionary<string, int>() { { "(?<重>重巡洋艦)", 1 }, { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Ammunition=10},
            new ExpeditionInfo {Area="北方", EName="航空戦艦運用演習", Time="04:00", Lv=50, SumLv=200, RequireShipType=new Dictionary<string, int>() { { "(?<航戦>航空戦艦)", 2 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Ammunition=20, Bauxite=100},
            new ExpeditionInfo {Area="北方", EName="北方航路海上護衛", Time="08:20", Lv=50, SumLv=200, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 4 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=500, Bauxite=150, InstantRepairMaterials=1, DevelopmentMaterials=2},

            new ExpeditionInfo {Area="西方", EName="通商破壊作戦", Time="40:00", Lv=25, RequireShipType=new Dictionary<string, int>() { { "(?<重>重巡洋艦)", 2 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Fuel=900, Steel=500, },
            new ExpeditionInfo {Area="西方", EName="敵母港空襲作戦", Time="80:00", Lv=30, RequireShipType=new Dictionary<string, int>() { { "(?<空母>正規空母|軽空母|水上機母艦)", 1 },  { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Bauxite=900, InstantRepairMaterials=3},
            new ExpeditionInfo {Area="西方", EName="潜水艦通商破壊作戦", Time="20:00", Lv=1, RequireShipType=new Dictionary<string, int>() { { "(?<潜>潜水艦|潜水空母)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=2, Steel=800, DevelopmentMaterials=1, FurnitureBox="小2"},
            new ExpeditionInfo {Area="西方", EName="西方海域封鎖作戦", Time="25:00", Lv=30, RequireShipType=new Dictionary<string, int>() { { "(?<潜>潜水艦|潜水空母)", 3 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=3, Steel=900, Bauxite=350, DevelopmentMaterials=2, FurnitureBox="中2"},
            new ExpeditionInfo {Area="西方", EName="潜水艦派遣演習", Time="24:00", Lv=50, RequireShipType=new Dictionary<string, int>() { { "(?<潜>潜水艦|潜水空母)", 3 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=3, Bauxite=100, DevelopmentMaterials=1, FurnitureBox="小1"},
            new ExpeditionInfo {Area="西方", EName="潜水艦派遣作戦", Time="48:00", Lv=55, RequireShipType=new Dictionary<string, int>() { { "(?<潜>潜水艦|潜水空母)", 4 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Bauxite=100, DevelopmentMaterials=3},
            new ExpeditionInfo {Area="西方", EName="海外艦との接触", Time="02:00", Lv=60,  SumLv=200, RequireShipType=new Dictionary<string, int>() { { "(?<潜>潜水艦|潜水空母)", 4 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=4, Ammunition=30, FurnitureBox="小1"},
            new ExpeditionInfo {Area="西方", EName="遠洋練習航海", Time="24:00", Lv=5, RequireShipType=new Dictionary<string, int>() { { "(?<練>練習巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=3, Fuel=50, Ammunition=50, Steel=50, Bauxite=50, FurnitureBox="大1"},

            new ExpeditionInfo {Area="南方", EName="MO作戦", Time="07:00", Lv=40, RequireShipType=new Dictionary<string, int>() { { "(?<空母>正規空母|軽空母|水上機母艦)", 2 }, { "(?<重>重巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 1 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=0, Ammunition=0, Steel=240, Bauxite=280, DevelopmentMaterials=1, FurnitureBox="小2"},
            new ExpeditionInfo {Area="南方", EName="水上機基地建設", Time="09:00", Lv=30, RequireShipType=new Dictionary<string, int>() { { "(?<水母>水上機母艦)", 2 }, { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 1 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=480, Ammunition=0, Steel=200, Bauxite=200, InstantRepairMaterials=1, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南方", EName="東京急行", Time="02:45", Lv=50, SumLv=200, RequireItemNum=new Dictionary<string, int>() { { "ドラム缶(輸送用)", 4 } }, RequireItemShipNum=new Dictionary<string, int>() { { "ドラム缶(輸送用)", 3 } }, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, {"(?<駆>駆逐艦)", 5 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=0, Ammunition=380, Steel=270, Bauxite=0, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南方", EName="東京急行（弐）", Time="02:55", Lv=65, SumLv=240, RequireItemNum=new Dictionary<string, int>() { { "ドラム缶(輸送用)", 8 } }, RequireItemShipNum=new Dictionary<string, int>() { { "ドラム缶(輸送用)", 4 } }, RequireShipType=new Dictionary<string, int>() { {"(?<駆>駆逐艦)", 5 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=420, Ammunition=0, Steel=200, Bauxite=0, FurnitureBox="小1"},
            new ExpeditionInfo {Area="南方", EName="遠洋潜水艦作戦", Time="30:00", Lv=3, SumLv=180, RequireShipType=new Dictionary<string, int>() { { "(?<潜母艦>潜水母艦)", 1 }, { "(?<潜>潜水艦|潜水空母)", 4 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=5, Fuel=0, Ammunition=0, Steel=300, Bauxite=0, InstantRepairMaterials=2, FurnitureBox="中1"},
            new ExpeditionInfo {Area="南方", EName="水上機前線輸送", Time="06:50", Lv=25, SumLv=150, RequireShipType=new Dictionary<string, int>() { { "(?<軽>軽巡洋艦)", 1 }, { "(?<水母>水上機母艦)", 2 }, {"(?<駆>駆逐艦)", 2 } }, isSuccess2=false, isSuccess3=false, isSuccess4=false, ShipNum=6, Fuel=300, Ammunition=300, Steel=0, Bauxite=100, InstantRepairMaterials=1, FurnitureBox="小3"},
        };

        public void Check()
        {
            isSuccess2 = CheckShipNum(2) && FlagshipLvCheck(2) && SumLvCheck(2) && RequireShipTypeCheck(2) && RequireItemCheck(2);
            isSuccess3 = CheckShipNum(3) && FlagshipLvCheck(3) && SumLvCheck(3) && RequireShipTypeCheck(3) && RequireItemCheck(3);
            isSuccess4 = CheckShipNum(4) && FlagshipLvCheck(4) && SumLvCheck(4) && RequireShipTypeCheck(4) && RequireItemCheck(4);
        }

        public static ExpeditionInfo[] ExpeditionList
        {
            get { return _ExpeditionTable; }
            set { _ExpeditionTable = value; }
        }

        private bool CheckShipNum(int index)
        {
            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Length >= ShipNum;
        }

        private bool FlagshipLvCheck(int index)
        {
            if (KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Length == 0) return false;

            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.First().Level >= Lv;
        }

        private bool SumLvCheck(int index)
        {
            if (null == SumLv) return true;

            return KanColleClient.Current.Homeport.Organization.Fleets[index].Ships.Select(s => s.Level).Sum() >= SumLv;
        }

        private bool RequireShipTypeCheck(int index)
        {
            if (null == RequireShipType) return true;

            foreach (KeyValuePair<string, int> pair in RequireShipType)
            {
                Regex re = new Regex(pair.Key);
                if (KanColleClient.Current.Homeport.Organization.Fleets[index].Ships
                    .Where(fleet => re.Match(fleet.Info.ShipType.Name).Success).Count() < pair.Value)
                    return false;
            }

            return true;
        }

        private bool RequireItemCheck(int index)
        {
            if (null == RequireItemNum || null == RequireItemShipNum) return true;
            foreach(KeyValuePair<string, int> pair in RequireItemShipNum)
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
    }
}
