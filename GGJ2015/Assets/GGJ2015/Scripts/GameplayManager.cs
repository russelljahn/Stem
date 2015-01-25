using Assets.GGJ2015.Scripts.Pivots;
using UnityEngine;


namespace Assets.GGJ2015.Scripts {
    public class GameplayManager : MonoBehaviour {
        private Story _currentStory = new Story();
        [SerializeField] private Page _currentPage;

        private void Start() {
            _currentPage.Setup(_currentStory);

            //var storyRoot = 
            //_currentPage.LoadPivot(_c);
        }



    }
}
