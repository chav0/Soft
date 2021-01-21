using UnityEngine;

namespace Client.View
{
    public class Screens : MonoBehaviour
    {
        public MainMenu MainMenu; 
        public GameMenu GameMenu; 
        public ChooseWorldMenu ChooseWorld; 
        public ResultScreen ResultScreen; 
        public AudioController AudioController; 
        public Transform WorldContainer;

        public void SetMainMenu()
        {
            MainMenu.gameObject.SetActive(true);
            GameMenu.gameObject.SetActive(false);
            ChooseWorld.gameObject.SetActive(false);
            ResultScreen.gameObject.SetActive(false);
        }
        
        public void SetGameMenu()
        {
            MainMenu.gameObject.SetActive(false);
            GameMenu.gameObject.SetActive(true);
            ChooseWorld.gameObject.SetActive(false);
            ResultScreen.gameObject.SetActive(false);
        }

        public void SetChooseWorldView()
        {
            MainMenu.gameObject.SetActive(false);
            GameMenu.gameObject.SetActive(false);
            ChooseWorld.gameObject.SetActive(true);
            ResultScreen.gameObject.SetActive(false);
        }
        
        public void SetResultView()
        {
            MainMenu.gameObject.SetActive(false);
            ChooseWorld.gameObject.SetActive(false);
            ResultScreen.gameObject.SetActive(true);
        }
    }
}
