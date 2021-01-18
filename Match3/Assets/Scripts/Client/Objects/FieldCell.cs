using System;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Objects
{
    public class FieldCell : Cell
    {
        public FieldCellState State;
        public GameCell GameCell;
        
#if UNITY_EDITOR
        public void OnEnable()
        {
            Image = GetComponent<Image>();
        }
#endif

        public void Colorize(FieldCellState state)
        {
            State = state;
        }
    }

    public enum FieldCellState
    {
        Active,
        Inactive
    }
}