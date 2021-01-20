using Client.View;
using UnityEngine;

namespace Client.ViewStates
{
    public class ChooseWorldViewState : BaseViewState
    {
        public override void OnEnter()
        {
            Context.Screens.SetChooseWorldView();

            var chooseWorldWidget = Context.Screens.ChooseWorld;
            var worlds = Context.AppModel.Resources.WorldBlueprints; 
            var count = worlds.Length; 
            for (var i = 0; i < count; i++)
            {
                var world = worlds[i];
                var button = chooseWorldWidget.Buttons.Count > i ? chooseWorldWidget.Buttons[i] : Context.Screens.ChooseWorld.CreateButton(world);

                button.Button.interactable = world.WorldId <= Context.AppModel.PlayerProfileStorage.LastCompletedWorld + 1; 
                button.Button.onClick.AddListener(() => { SetState(new LoadingViewState(world.WorldId)); });
            }
        }
        
        public override void PreModelUpdate()
        {
            
        }

        public override void PostModelUpdate()
        {
            
        }
    }
}