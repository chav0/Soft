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
            Image = GetComponentInChildren<Image>();
        }
#endif

        public void Colorize(FieldCellState state)
        {
            State = state;
            switch (state)
            {
                case FieldCellState.Active:
                    Image.gameObject.SetActive(true);
                    break;
                case FieldCellState.Inactive:
                    Image.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public enum FieldCellState
    {
        Active,
        Inactive
    }
}