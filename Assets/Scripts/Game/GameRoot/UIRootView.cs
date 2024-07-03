using UnityEngine;

namespace RhythmGame
{
    public sealed class UIRootView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _loadingScreen;

        [SerializeField] 
        private Transform _uiSceneContainer;

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

        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();
            
            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        private void ClearSceneUI()
        {
            var childCount = _uiSceneContainer.childCount;
            for (var i = 0; i < childCount; i++)
            {
                Destroy(_uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}