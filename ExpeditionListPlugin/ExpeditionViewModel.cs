using Grabacr07.KanColleWrapper;
using Livet;
using Livet.EventListeners;
using System.Collections.Generic;
using MetroTrilithon.Mvvm;
using System;
using System.Reactive.Linq;
using System.Linq;
using System.Windows;
using Grabacr07.KanColleViewer.Composition;

namespace ExpeditionListPlugin
{
    public class ExpeditionViewModel : ViewModel
    {
        #region isAllArea
        private Boolean _isAllArea;
        public Boolean isAllArea
        {
            get
            {
                return _isAllArea;
            }
            set
            {
                _isAllArea = value;
                isArea1Contain = value;
                isArea2Contain = value;
                isArea3Contain = value;
                isArea4Contain = value;
                isArea5Contain = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion
        #region isArea1Contain 変更通知プロパティ
        private Boolean _isArea1Contain;
        public Boolean isArea1Contain
        {
            get
            {
                return _isArea1Contain;
            }
            set
            {
                _isArea1Contain = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion
        #region isArea2Contain 変更通知プロパティ
        private Boolean _isArea2Contain;
        public Boolean isArea2Contain
        {
            get
            {
                return _isArea2Contain;
            }
            set
            {
                _isArea2Contain = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion
        #region isArea3Contain 変更通知プロパティ
        private Boolean _isArea3Contain;
        public Boolean isArea3Contain
        {
            get
            {
                return _isArea3Contain;
            }
            set
            {
                _isArea3Contain = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion
        #region isArea4Contain 変更通知プロパティ
        private Boolean _isArea4Contain;
        public Boolean isArea4Contain
        {
            get
            {
                return _isArea4Contain;
            }
            set
            {
                _isArea4Contain = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion
        #region isArea5Contain 変更通知プロパティ
        private Boolean _isArea5Contain;
        public Boolean isArea5Contain
        {
            get
            {
                return _isArea5Contain;
            }
            set
            {
                _isArea5Contain = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion

        #region ExpeditionInfos 変更通知プロパティ
        private List<ExpeditionInfo> _ExpeditionInfos;

        public List<ExpeditionInfo> ExpeditionInfos
        {
            get
            {
                return _ExpeditionInfos;
            }
            set
            {
                this._ExpeditionInfos = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion

        private ExpeditionListPlugin plugin;

        private Action<int> StartExpeditionCheckAction;

        public ExpeditionViewModel(ExpeditionListPlugin plugin)
        {
            //KanColleClient.Current.Homeport.Organization
            //    .Subscribe(nameof(Organization.Fleets), this.InitializeFleets).AddTo(this);
            this.plugin = plugin;
            StartExpeditionCheckAction = (int index) =>
            {
                if (KanColleClient.Current.Homeport.Organization.Fleets[index].Expedition.IsInExecution)
                {
                    String name = KanColleClient.Current.Homeport.Organization.Fleets[index].Expedition.Mission.Title;

                    var list = ExpeditionInfo.ExpeditionList.ToList().Where(expedition => expedition.EName.Equals(name));

                    if (list.Count() > 0)
                    {
                        if(!list.First().CheckAll(index))
                        {
                            Notify("ExpeditionStart", "遠征確認", "第" + index + "艦隊の[" + name + "]は失敗する可能性があります。" +
                                Environment.NewLine + "編成を確認してください。");
                        }
                    }
                }
            };

            InitializeExpedition();
            KanColleProxy proxy = KanColleClient.Current.Proxy;
            proxy.api_port.Subscribe(x => this.UpdateView());
            proxy.api_get_member_ship3.Subscribe(x => this.UpdateView());
            proxy.api_req_hensei_change.Subscribe(x => this.UpdateView());
            proxy.ApiSessionSource
                .Where(x => x.Request.PathAndQuery == "/kcsapi/api_req_hensei/preset_select").Subscribe(x => this.UpdateView());
        }

        private void InitializeExpedition()
        {
            isAllArea = true;
            UpdateExpedition();
            
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Organization)
            {
                {
                    () => KanColleClient.Current.Homeport.Organization.Ships,
                    (_, __) => { DispatcherHelper.UIDispatcher.Invoke(this.UpdateView); }
                }
            });
            this.CompositeDisposable.Add(new PropertyChangedEventListener(this)
            {
                {
                    () => this.isArea1Contain,
                    (_, __) => { DispatcherHelper.UIDispatcher.Invoke(this.UpdateView); }
                },
                {
                    () => this.isArea2Contain,
                    (_, __) => { DispatcherHelper.UIDispatcher.Invoke(this.UpdateView); }
                },
                {
                    () => this.isArea3Contain,
                    (_, __) => { DispatcherHelper.UIDispatcher.Invoke(this.UpdateView); }
                },
                {
                    () => this.isArea4Contain,
                    (_, __) => { DispatcherHelper.UIDispatcher.Invoke(this.UpdateView); }
                },
                {
                    () => this.isArea5Contain,
                    (_, __) => { DispatcherHelper.UIDispatcher.Invoke(this.UpdateView); }
                }
            });

            // ループでなぜか登録できないのでべた書き
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Organization.Fleets[2].Expedition)
            {
                {
                    () => KanColleClient.Current.Homeport.Organization.Fleets[2].Expedition.IsInExecution,
                    (_, __) => {
                            
                        DispatcherHelper.UIDispatcher.Invoke(StartExpeditionCheckAction, new object[] { 2 });
                    }
                }
            });
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Organization.Fleets[3].Expedition)
            {
                {
                    () => KanColleClient.Current.Homeport.Organization.Fleets[3].Expedition.IsInExecution,
                    (_, __) => {

                        DispatcherHelper.UIDispatcher.Invoke(StartExpeditionCheckAction, new object[] { 3 });
                    }
                }
            });
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Organization.Fleets[4].Expedition)
            {
                {
                    () => KanColleClient.Current.Homeport.Organization.Fleets[4].Expedition.IsInExecution,
                    (_, __) => {

                        DispatcherHelper.UIDispatcher.Invoke(StartExpeditionCheckAction, new object[] { 4 });
                    }
                }
            });
        }

        public void SetArea(String area)
        {
            isAllArea = false;
            isArea1Contain = "鎮守".Equals(area) ? true : false;
            isArea2Contain = "南西".Equals(area) ? true : false;
            isArea3Contain = "北方".Equals(area) ? true : false;
            isArea4Contain = "西方".Equals(area) ? true : false;
            isArea5Contain = "南方".Equals(area) ? true : false;
        }

        private void UpdateExpedition()
        {
            var list = new List<ExpeditionInfo>();

            ExpeditionInfo.ExpeditionList.ToList().ForEach(info =>
            {
                if ((isArea1Contain && "鎮守".Equals(info.Area)) ||
                    (isArea2Contain && "南西".Equals(info.Area)) ||
                    (isArea3Contain && "北方".Equals(info.Area)) ||
                    (isArea4Contain && "西方".Equals(info.Area)) ||
                    (isArea5Contain && "南方".Equals(info.Area))
                    )
                {

                    info.Check();
                    list.Add(info);
                }
            });

            ExpeditionInfos = list;
        }
        private void UpdateView()
        {
            if (!KanColleClient.Current.IsStarted) return;

            UpdateExpedition();
        }

        private void Notify(string type, string title, string message)
        {            this.plugin.InvokeNotifyRequested(new NotifyEventArgs(type, title, message)            {
                Activated = () =>
                {
                    DispatcherHelper.UIDispatcher.Invoke(() =>                    {
                        var window = Application.Current.MainWindow;
                        if (window.WindowState == WindowState.Minimized)
                            window.WindowState = WindowState.Normal;
                        window.Activate();
                    });
                },
            });
        }
    }
}
