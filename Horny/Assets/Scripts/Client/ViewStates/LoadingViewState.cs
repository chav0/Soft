using Client.Model;
using UnityEngine;

namespace Client.ViewStates
{
    public class LoadingViewState : BaseViewState
    {
        private readonly int _worldId;

        public LoadingViewState(int worldId)
        {
            _worldId = worldId;
        }
        
        public override void PreModelUpdate()
        {
            if (Context.AppModel.Status == ModelStatus.Init)
            {
                Context.AppModel.CreateWorld(_worldId);
            }
        }

        public override void PostModelUpdate()
        {
            if (Context.AppModel.Status == ModelStatus.Battle)
            {
                if (Context.AppModel.World != null)
                {
                    SetState(new GameViewState());
                }
            }
        }
    }
}
