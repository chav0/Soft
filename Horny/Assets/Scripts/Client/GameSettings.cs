using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Points For Merge")] 
        public int PointsForTwoMerge; 
        public int PointsForThreeMerge; 
        public int PointsForFourMerge; 
        public int PointsForFiveMerge; 
        
        public int GetScoreForMerge(int mergeCount)
        {
            return mergeCount switch
            {
                1 => PointsForTwoMerge,
                2 => PointsForThreeMerge,
                3 => PointsForFourMerge,
                4 => PointsForFiveMerge,
                _ => 0
            };
        }
    }
}
