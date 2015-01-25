using Assets.GGJ2015.Scripts.Pivots;
using UnityEngine;


namespace Assets.GGJ2015.Scripts {
    public class GameplayManager : MonoBehaviour {
        private readonly Story _currentStory = new Story();
        [SerializeField] private Page _currentPage;

        private void Start() {
            Debug.Log("_currentStory.Root.Description: " + _currentStory.Root.Description);

            _currentPage.Setup(_currentStory);
            _currentPage.LoadPivot(_currentStory.Root);
        }



    }
}
