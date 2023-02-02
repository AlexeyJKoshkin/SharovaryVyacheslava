using System.Collections.Generic;
using Core.Data.Provider;
using GameKit;
using RoyalAxe.CoreLevel;
using VContainer.Unity;

namespace RoyalAxe 
{
    public class DevelopSelectParamsEntryPoint : IInitializable
    {
        private readonly IDataStorage _storage;
        private readonly IUltimateCheatAdapter _ultimateCheatAdapter;
        private readonly DevelopSelectLevelParamsUIView _view;
        private readonly IReadOnlyList<IDevelopSelectCoreParamsWorker> _workers;

        public DevelopSelectParamsEntryPoint(IDataStorage storage,
                                             IUltimateCheatAdapter ultimateCheatAdapter,
                                             DevelopSelectLevelParamsUIView developSelectLevelParamsUiView,
                                             IReadOnlyList<IDevelopSelectCoreParamsWorker> workers)
        {
            _storage              = storage;
            _ultimateCheatAdapter = ultimateCheatAdapter;
            _view                 = developSelectLevelParamsUiView;
            _workers = workers;
        }

        public void Initialize()
        {
            if (_ultimateCheatAdapter.EnableCheats)
            {
                _workers.ForEach(e=> e.PrepareViews());
                /*new DevelopLevelSelection(_storage, _ultimateCheatAdapter, _view).Initialize();
                new DevelopHeroSelection(_storage, _ultimateCheatAdapter, _view).Initialize();*/
            }
            else
            {
                _view.Close();
            }
        }
    }
}