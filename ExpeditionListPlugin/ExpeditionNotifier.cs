using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using Livet;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace ExpeditionListPlugin
{
    public class ExpeditionNotifier : NotificationObject
    {
        private readonly ExpeditionListPlugin plugin;

        public ExpeditionNotifier(ExpeditionListPlugin plugin)
        {
            this.plugin = plugin;

            var proxy = KanColleClient.Current.Proxy;
            proxy.api_get_member_deck
                .Subscribe(_ => StartExpeditionCheckAction());
        }

        private void StartExpeditionCheckAction()
        {
            for(var index = 2; index <= 4; index++)
            {
                if (KanColleClient.Current.Homeport.Organization.Fleets[index].Expedition.IsInExecution)
                {
                    var name = KanColleClient.Current.Homeport.Organization.Fleets[index].Expedition.Mission.Title;

                    var list = ExpeditionInfo.ExpeditionList.ToList().Where(expedition => expedition.EName.Equals(name));

                    if (list.Any())
                    {
                        if (!list.First().CheckAll(KanColleClient.Current.Homeport.Organization.Fleets[index]))
                        {
                            Notify("ExpeditionStart", "遠征確認", $"第{index}艦隊の[{name}]は失敗する可能性があります。{Environment.NewLine}編成を確認してください。");
                        }
                        else if (KanColleClient.Current.Homeport.Organization.Fleets[index].State.Situation.HasFlag(FleetSituation.InShortSupply))
                        {
                            Notify("ExpeditionStart", "遠征確認", $"第{index}艦隊の[{ name}]は失敗する可能性があります。{Environment.NewLine}艦隊に完全に補給されていない艦娘がいます。");
                        }
                    }
                }
            }
            
        }


        private void Notify(string type, string title, string message) => plugin.InvokeNotifyRequested(new NotifyEventArgs(type, title, message)
        {
            Activated = () =>
            {
                DispatcherHelper.UIDispatcher.Invoke(() =>
                {
                    var window = Application.Current.MainWindow;
                    if (window.WindowState == WindowState.Minimized)
                        window.WindowState = WindowState.Normal;
                    window.Activate();
                });
            },
        });
    }
}
