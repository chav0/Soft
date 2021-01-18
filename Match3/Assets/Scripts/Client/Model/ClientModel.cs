using Client.ModelStates;
using Client.Scene;

namespace Client.Model
{
    public class ClientModel
    {
        public readonly GameSettings Settings;
        public readonly UnityScene Scene;
        private readonly Resources Resources; 
        
        public BaseModelState CurrentState { get; set; }
        public ModelStatus Status => CurrentState.Status; 
        public World World => CurrentState.World;
        
        public ClientModel(GameSettings gameSettings, UnityScene scene, Resources resources)
        {
            Settings = gameSettings;
            Scene = scene;
            Resources = resources; 
            CurrentState = new InitModelState();
            CurrentState.Context = this; 
            CurrentState.OnEnter();
        }

        public void Update()
        {
            CurrentState.Update();
        }

        public void CreateWorld()
        {
            CurrentState.SetState(new GameModelState());
        }

        public void DeleteWorld()
        {
            CurrentState.SetState(new InitModelState());
        }
        
        public void AddGameInput(GameInput input)
        {
            CurrentState.AddGameInput(input);
        }
    }

    public enum ModelStatus
    {
        Init,
        Battle,
    }
}
