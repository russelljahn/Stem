using Assets.GGJ2015.Scripts.Audio;
using Assets.GGJ2015.Scripts.Gui.PivotAnimations;
using Assets.GGJ2015.Scripts.Pivots;
using UnityEngine;


namespace Assets.GGJ2015.Scripts {
    public class GameplayManager : MonoBehaviour {
        private readonly Story _currentStory = new Story();
        [SerializeField] private Page _page;
        [SerializeField] private PivotAnimation _initialAnimation;
        [SerializeField] private Title _title;


        private void OnEnable() {
            _title.Closed += StartGame;
        }


        private void StartGame() {
            _page.Setup(_currentStory, _initialAnimation);
            _page.LoadPivot(_currentStory.Root);
            _page.AnimatePivotTransition(_currentStory.Root);
        }



    }
}
