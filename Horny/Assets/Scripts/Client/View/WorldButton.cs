using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.View
{
    public class WorldButton : MonoBehaviour
    {
        public Button Button;
        public GameObject NewWorld;
        public GameObject Locked;
        public TMP_Text WorldId; 
        public Color ColorSimple; 
        public Color ColorNew;
        public GameObject StarsGroup;
        public StarObject[] Stars; 
    }
}