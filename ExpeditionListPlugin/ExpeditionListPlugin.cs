using Grabacr07.KanColleViewer.Composition;

using System.ComponentModel.Composition;

namespace ExpeditionListPlugin
{
    [Export(typeof(IPlugin))]
    [ExportMetadata("Title", "遠征一覧プラグイン")]
    [ExportMetadata("Description", "遠征一覧と各艦隊が成功条件を満たすか表示します。")]
    [ExportMetadata("Version", "1.0.0")]
    [ExportMetadata("Author", "@ruhiel_murrue")]
    [ExportMetadata("Guid", "B8BDDEA7-1AEC-420B-8F6D-8F4EE906DC1B")]
    [Export(typeof(ITool))]
    public class ExpeditionListPlugin : IPlugin, ITool
    {
        public string Name => "ExpeditionList";
        public object View => new UserControl1 { DataContext = new ExpeditionViewModel() };
        public void Initialize()
        {
        }
    }
}
