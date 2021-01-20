using UnityEngine;

namespace Client.ViewStates
{
    public class LobbyViewState : BaseViewState
    {
        public override void OnEnter()
        {
            Context.Screens.SetMainMenu();
            Context.Screens.MainMenu.ToGame.onClick.RemoveAllListeners();
            Context.Screens.MainMenu.ToGame.onClick.AddListener(() => SetState(new ChooseWorldViewState()));
        }
        
        public override void PreModelUpdate()
        {

        }

        public override void PostModelUpdate()
        {

        }
    }
}
