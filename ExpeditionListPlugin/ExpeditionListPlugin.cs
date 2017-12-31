using Grabacr07.KanColleViewer.Composition;
using System;
using System.ComponentModel.Composition;

namespace ExpeditionListPlugin
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [Export(typeof(IRequestNotify))]
    [ExportMetadata("Title", "遠征一覧プラグイン")]
    [ExportMetadata("Description", "遠征一覧と各艦隊が成功条件を満たすか表示します。")]
    [ExportMetadata("Version", "1.0.9")]
    [ExportMetadata("Author", "@ruhiel_murrue")]
    [ExportMetadata("Guid", "B8BDDEA7-1AEC-420B-8F6D-8F4EE906DC1B")]
    public class ExpeditionListPlugin : IPlugin, ITool, IRequestNotify
    {
        public string Name => "ExpeditionList";
        public object View => new UserControl1 { DataContext = new ExpeditionViewModel(this) };
        public void Initialize()
        {
        }
        public event EventHandler<NotifyEventArgs> NotifyRequested;

        public void InvokeNotifyRequested(NotifyEventArgs e) => this.NotifyRequested?.Invoke(this, e);

    }
}
