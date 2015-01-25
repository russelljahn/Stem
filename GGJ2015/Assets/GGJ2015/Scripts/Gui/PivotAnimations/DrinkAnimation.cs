using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class DrinkAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _drinkAnimationClip;

        [SerializeField] private float _shortenAnimationTime = .1f;

        [SerializeField] private float _fadeTime = 0.25f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _spriteRenderer;



        private void OnEnable() {
            Length = _fadeTime + _drinkAnimationClip.length - _shortenAnimationTime;     
            this.InvokeAfterTime(Length, FadeOut);
        }


        private void FadeOut() {
            TweenUtils.TweenAlpha(_spriteRenderer, 0f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}
