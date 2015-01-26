using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GetSickAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _getSickAnimationClip;

        [SerializeField] private float _fadeTime = 0.25f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _spriteRenderer;



        private void OnEnable() {
            Length = _fadeTime + _getSickAnimationClip.length;     
            this.InvokeAfterTime(Length, FadeOut);
        }


        private void FadeOut() {
            TweenUtils.TweenAlpha(_spriteRenderer, 0f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}
