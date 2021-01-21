using UnityEngine;

namespace Client.Model
{
    public class GameState
    {
        public WorldState WorldState;
        public WorldRules Rules;

        public GameState(World world)
        {
            WorldState = new WorldState
            {
                Score = 0,
                SwipeCount = 0,
            }; 
            
            Rules = new WorldRules(world);
        }
    }
}
