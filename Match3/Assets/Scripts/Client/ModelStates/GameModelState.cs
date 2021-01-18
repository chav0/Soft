using Client.Model;
using Client.Scene;
using UnityEngine;

namespace Client.ModelStates
{
    public class GameModelState : BaseModelState
    {
        private GameLogic _gameLogic;
        private World _world;
        private UnityScene _scene;
        private GameInput _input;
        
        public override World World => _world;
        public override ModelStatus Status => ModelStatus.Battle; 

        public override void OnEnter()
        {
            _scene = Context.Scene;
            _gameLogic = new GameLogic(Context.Settings, _scene);
            _world = _gameLogic.CreateNewWorld(1);
            _gameLogic.Init(_world);
        }
        
        public override void OnExit()
        {
            _scene.Dispose();
            _world.Dispose();
        }
        
        public override void Update()
        {
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
    }
}
