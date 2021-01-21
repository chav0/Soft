using System.Linq;
using Client.View;
using UnityEngine;

namespace Client.ViewStates
{
    public class ChooseWorldViewState : BaseViewState
    {
        public override void OnEnter()
        {
            Context.Screens.SetChooseWorldView();

            var lastOpenedWorld = Context.AppModel.PlayerProfileStorage.LastCompletedWorld + 1;
            var chooseWorldWidget = Context.Screens.ChooseWorld;
            var worlds = Context.AppModel.Resources.WorldBlueprints; 
            var count = worlds.Length; 
            for (var i = 0; i < count; i++)
            {
                var world = worlds[i];
                var button = chooseWorldWidget.Buttons.Count > i ? chooseWorldWidget.Buttons[i] : Context.Screens.ChooseWorld.CreateButton(world);
                var worldInfo = Context.AppModel.PlayerProfileStorage.WorldInfos.FirstOrDefault(x => x.WorldId == world.WorldId); 

                button.Button.interactable = world.WorldId <= lastOpenedWorld; 
                button.Button.onClick.RemoveAllListeners();
                button.Button.onClick.AddListener(() => { SetState(new LoadingViewState(world.WorldId)); });
                button.WorldId.text = world.WorldId.ToString();
                
                if (world.WorldId < lastOpenedWorld)
                {
                    button.WorldId.gameObject.SetActive(true);
                    button.Locked.gameObject.SetActive(false);
                    button.WorldId.color = button.ColorSimple; 
                    button.NewWorld.gameObject.SetActive(false);
                    button.StarsGroup.SetActive(true);
                    if (worldInfo != null)
                    {
                        for (var j = 0; j < button.Stars.Length; j++)
                        {
                            var star = button.Stars[j];
                            star.SetReceived(j + 1 <= worldInfo.Stars); 

                        }
                    }
                } 
                else if (world.WorldId == lastOpenedWorld)
                {
                    button.WorldId.gameObject.SetActive(true);
                    button.Locked.gameObject.SetActive(false);
                    button.WorldId.color = button.ColorNew; 
                    button.NewWorld.gameObject.SetActive(true);
                    button.StarsGroup.SetActive(false);
                }
                else
                {
                    button.NewWorld.gameObject.SetActive(false);
                    button.Locked.gameObject.SetActive(true);
                    button.WorldId.gameObject.SetActive(false);
                    button.StarsGroup.SetActive(false);
                }
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