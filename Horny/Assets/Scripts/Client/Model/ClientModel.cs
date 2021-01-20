using Client.ModelStates;
using Client.Scene;

namespace Client.Model
{
    public class ClientModel
    {
        public readonly GameSettings Settings;
        public readonly UnityScene Scene;
        public readonly Resources Resources; 
        public readonly PlayerProfileStorage PlayerProfileStorage; 
        
        public BaseModelState CurrentState { get; set; }
        public ModelStatus Status => CurrentState.Status; 
        public World World => CurrentState.World;
        public int Score { get; set; }
        
        public ClientModel(GameSettings gameSettings, UnityScene scene, Resources resources)
        {
            Settings = gameSettings;
            Scene = scene;
            Resources = resources; 
            PlayerProfileStorage = new PlayerProfileStorage();
            CurrentState = new InitModelState();
            CurrentState.Context = this; 
            CurrentState.OnEnter();
        }

        public void Update()
        {
            CurrentState.Update();
        }

        public void CreateWorld(int worldId)
        {
            CurrentState.SetState(new GameModelState(worldId));
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
