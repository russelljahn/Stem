using Assets.Stem.Scripts.Gui;
using Assets.Stem.Scripts.Gui.PivotAnimations;
using Assets.Stem.Scripts.Pivots;
using UnityEngine;


namespace Assets.Stem.Scripts {
    public class GameplayManager : MonoBehaviour {
        private readonly Story _currentStory = new Story();
        [SerializeField] private Page _page;
        [SerializeField] private PivotAnimation _initialAnimation;
        [SerializeField] private TitleGui _titleGui;


        private void OnEnable() {
            _titleGui.Closed += StartGame;
        }


        private void StartGame() {
            _page.Setup(_currentStory, _initialAnimation);
            _page.LoadPivot(_currentStory.Root);
            _page.AnimatePivotTransition(_currentStory.Root);
        }



    }
}
