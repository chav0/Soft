using UnityEngine;

namespace Client.View
{
    public class Screens : MonoBehaviour
    {
        public MainMenu MainMenu; 
        public GameMenu GameMenu; 
        public Transform WorldContainer;

        public void SetMainMenu()
        {
            MainMenu.gameObject.SetActive(true);
            GameMenu.gameObject.SetActive(false);
        }
        
        public void SetGameMenu()
        {
            MainMenu.gameObject.SetActive(false);
            GameMenu.gameObject.SetActive(true);
        }
    }
}
