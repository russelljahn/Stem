using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class SimpleAnimationWithEndLoop : PivotAnimation {

        [SerializeField] private AnimationClip _sequenceAnimationClip;
        [SerializeField]private AnimationClip _loopAnimationClip;


        [SerializeField] private SpriteRenderer _sequenceSpriteRenderer;
        [SerializeField] private SpriteRenderer _loopSpriteRenderer;

        [SerializeField] private WrapMode _wrapMode = WrapMode.Loop;

  
        private void OnEnable() {
            _sequenceSpriteRenderer.color = Color.white;
            _loopSpriteRenderer.color = Color.clear;

            Length = _sequenceAnimationClip.length;
            this.InvokeAfterTime(Length, PlayLoop, RaiseFinishedEvent);
        }


        private void PlayLoop() {
            _sequenceSpriteRenderer.color = Color.clear;
            _loopSpriteRenderer.color = Color.white;

            _loopAnimationClip.wrapMode = _wrapMode;
        }


    }
}
