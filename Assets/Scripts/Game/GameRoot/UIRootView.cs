using UnityEngine;

namespace RhythmGame
{
    public sealed class UIRootView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _loadingScreen;

        private void Awake()
        {
            HideLoadingScreen();
        }
        
        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
            Debug.Log("true");
        }
        
        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
            Debug.Log("false");
        }
    }
}