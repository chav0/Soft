using Client.Model;
using Client.Objects;
using UnityEngine;

namespace Client.ViewStates
{
    public class GameViewState : BaseViewState
    {
        private GameInput _input; 
        
        public override void OnEnter()
        {
            Context.Screens.SetGameMenu();
            Context.Screens.GameMenu.Exit.onClick.RemoveAllListeners();
            Context.Screens.GameMenu.Exit.onClick.AddListener(() =>
            {
                Context.AppModel.DeleteWorld();
                SetState(new LobbyViewState());
            });

            AnimateCells(); 
        }
        
        public override void PreModelUpdate()
        {
            _input = FillInput(); 
            Context.AppModel.AddGameInput(_input);
        }

        public override void PostModelUpdate()
        {
            if (_input != null)
            {
                AnimateCells();
            }
        }

        private void AnimateCells()
        {
            var cells = Context.AppModel.World.GameCells;
            for (var i = cells.Count - 1; i >= 0; i--)
            {
                var gameCell = cells[i];
                if (gameCell.NeedDelete)
                    cells.RemoveAt(i);
                gameCell.MoveToFieldCell();
            }
        }
        
        private static GameInput FillInput()
        {
            if (Input.GetKeyDown("a"))
            {
                return new GameInput
                {
                    Direction = Direction.Left
                }; 
            }

            if (Input.GetKeyDown("d"))
            {
                return new GameInput
                {
                    Direction = Direction.Right
                }; 
            }

            if (Input.GetKeyDown("w"))
            {
                return new GameInput
                {
                    Direction = Direction.Up
                };
            }

            if (Input.GetKeyDown("s"))
            {
                return new GameInput
                {
                    Direction = Direction.Down
                };
            }

            return null;
        }
    }
}
