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
        private UnityScene _scene;
        private GameInput _input;
        private int _initialBestScore;
        
        public override World World => _world;
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
            var previousGamesInfo = Context.PlayerProfileStorage.WorldInfos.FirstOrDefault(x => x.WorldId == _worldId);
            _initialBestScore = previousGamesInfo?.Record ?? 0; 
            Context.Score = 0; 
        }
        
        public override void OnExit()
        {
            _scene.Dispose();
            _world.Dispose();
        }
        
        public override void Update()
        {
            AddScore();
            
            if (_input != null)
            {
                var haveMoving = _gameLogic.ApplyInput(_input);

                if (haveMoving && _gameLogic.CanCreateNewGameCell())
                {
                    _world.GameCells.Add(_gameLogic.CreateNewGameCell()); 
                }
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
                Context.Score += Context.Settings.GetScoreForMerge(gameCell.MergeCount);
                gameCell.MergeCount = 0; 
            }

            if (_initialBestScore != Context.Score)
            {
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

                previousGamesInfo.Record = Context.Score;
                Context.PlayerProfileStorage.WorldInfos = infos; 
            }
        }
    }
}
