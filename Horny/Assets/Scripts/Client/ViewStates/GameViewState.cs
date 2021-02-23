using System.Linq;
using Client.Model;
using Client.View;
using UnityEngine;

namespace Client.ViewStates
{
    public class GameViewState : BaseViewState
    {
        private GameInput _input;
        private GameMenu _gameMenu; 
        
        public override void OnEnter()
        {
            _gameMenu = Context.Screens.GameMenu; 
            
            Context.Screens.SetGameMenu();
            
            _gameMenu.Exit.onClick.RemoveAllListeners();
            _gameMenu.Exit.onClick.AddListener(() =>
            {
                Context.AppModel.DeleteWorld();
                SetState(new ChooseWorldViewState());
            });
            
            _gameMenu.Restart.onClick.RemoveAllListeners();
            _gameMenu.Restart.onClick.AddListener(() =>
            {
                Context.AppModel.CreateWorld(Context.AppModel.World.WorldId);
                SetState(new GameViewState());
            });

            AnimateCells();
            SetProgress(); 
        }
        
        public override void PreModelUpdate()
        {
            var worldInfo = Context.AppModel.PlayerProfileStorage.WorldInfos.FirstOrDefault(x => x.WorldId == Context.AppModel.World.WorldId);
            var record = worldInfo?.Record ?? 0; 
            _gameMenu.SetScoreAndRecord(Context.GameState.WorldState.Score, record);
            _gameMenu.SetSwipeCount(Context.GameState.Rules.SwipeCount - Context.GameState.WorldState.SwipeCount);
            if (Context.AppModel.World.GameCells.Any(x => x.IsMoving))
            {
                _input = null;
            }
            else
            {
                _input = Context.Screens.GameMenu.SwipeController.GetInput(); 
            }
            
            Context.AppModel.AddGameInput(_input);
        }

        public override void PostModelUpdate()
        {
            if (_input != null)
            {
                AnimateCells();
            }
            
            SetProgress(); 
            CheckEnd(); 
        }

        private void CheckEnd()
        {
            if (Context.GameState.WorldState.IsTheEnd)
            {
                SetState(new ResultViewState());
            }
        }

        private void SetProgress()
        {
            var score = Context.GameState.WorldState.Score;
            var rules = Context.GameState.Rules; 
            
            _gameMenu.ProgressView.SetProgress(score, rules.FirstStarScore, rules.SecondStarScore, rules.ThirdStarScore, rules.GetStarByScore(score));
        }

        private void AnimateCells()
        {
            var cells = Context.AppModel.World.GameCells;
            for (var i = cells.Count - 1; i >= 0; i--)
            {
                var gameCell = cells[i];
                if (gameCell.NeedDelete)
                    cells.RemoveAt(i);
                gameCell.MoveToFieldCell(Context.AppModel.Settings.GetScoreForMerge(gameCell.MergeCount));
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
