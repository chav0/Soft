using System.Linq;
using Client.Model;
using Client.Scene;
using UnityEngine;

namespace Client.ModelStates
{
    public class GameModelState : BaseModelState
    {
        private readonly int _worldId;
        private GameLogic _gameLogic;
        private World _world;
        private GameState _gameState;
        private UnityScene _scene;
        private GameInput _input;
        private int _initialBestScore;

        public override World World => _world;
        public override GameState GameState => _gameState;
        public override ModelStatus Status => ModelStatus.Battle;

        public GameModelState(int worldId)
        {
            _worldId = worldId;
        }

        public override void OnEnter()
        {
            _scene = Context.Scene;
            _gameLogic = new GameLogic(Context.Settings, _scene);
            _world = _gameLogic.CreateNewWorld(_worldId);
            _gameLogic.Init(_world);
            _gameState = new GameState(_world);
            var previousGamesInfo = Context.PlayerProfileStorage.WorldInfos.FirstOrDefault(x => x.WorldId == _worldId);
            _initialBestScore = previousGamesInfo?.Record ?? 0;
        }
        
        public override void OnExit()
        {
            _scene.Dispose();
            _world.Dispose();
        }
        
        public override void Update()
        {
            AddScore();
            UpdateWorldInfo(); 
            CheckToOpenNewWorld();
            CheckTheEnd(); 
            
            if (_input != null)
            {
                var haveMoving = _gameLogic.ApplyInput(_input);

                if (haveMoving && _gameLogic.CanCreateNewGameCell())
                {
                    _world.GameCells.Add(_gameLogic.CreateNewGameCell(GameState.Rules.Colors));
                    GameState.WorldState.SwipeCount++; 
                }

                _input = null; 
            }
        }

        public override void AddGameInput(GameInput input)
        {
            _input = input; 
        }

        private void AddScore()
        {
            foreach (var gameCell in _world.GameCells)
            {
                GameState.WorldState.Score += Context.Settings.GetScoreForMerge(gameCell.MergeCount);
                gameCell.MergeCount = 0; 
            }
        }

        private void UpdateWorldInfo()
        {
            var currentScore = GameState.WorldState.Score;
            
            var infos = Context.PlayerProfileStorage.WorldInfos;
            var previousGamesInfo = Context.PlayerProfileStorage.WorldInfos.FirstOrDefault(x => x.WorldId == _worldId);
            if (previousGamesInfo == null)
            {
                previousGamesInfo = new WorldInfo
                {
                    WorldId = _worldId
                };
                infos.Add(previousGamesInfo);
            }

            var newStars = GameState.Rules.GetStarByScore(currentScore);

            if (previousGamesInfo.Stars < newStars)
            {
                previousGamesInfo.Stars = newStars;
                Context.PlayerProfileStorage.WorldInfos = infos;
            }

            if (previousGamesInfo.Record < currentScore)
            {
                previousGamesInfo.Record = currentScore;
                Context.PlayerProfileStorage.WorldInfos = infos;
            }
            
        }

        private void CheckToOpenNewWorld()
        {
            if (Context.PlayerProfileStorage.LastCompletedWorld >= _world.WorldId)
                return;
            
            if (GameState.Rules.FirstStarScore <= GameState.WorldState.Score && _world.WorldObject.SwipeCount >= GameState.WorldState.SwipeCount)
            {
                Context.PlayerProfileStorage.LastCompletedWorld = Mathf.Max(_world.WorldId, Context.PlayerProfileStorage.LastCompletedWorld);
            }
        }

        private void CheckTheEnd()
        {
            var swipes = GameState.WorldState.SwipeCount;
            var score = GameState.WorldState.Score;
            var rules = GameState.Rules;
            var stars = rules.GetStarByScore(score);

            if (rules.SwipeCount - swipes <= 0 || stars == 3)
                GameState.WorldState.IsTheEnd = true;
        }
    }
}
