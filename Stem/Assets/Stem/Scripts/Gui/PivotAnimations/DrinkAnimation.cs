using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class DrinkAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _drinkAnimationClip;
        [SerializeField] private AnimationClip _getSickAnimationClip;
        [SerializeField] private Animator _getSickAnimator;

        [SerializeField] private float _waitTime = 0.25f;
        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();


        [SerializeField] private SpriteRenderer _drinkSpriteRenderer;
        [SerializeField] private SpriteRenderer _getSickSpriteRenderer;
        [SerializeField] private SpriteRenderer _hospitalSpriteRenderer;





        private void OnEnable() {
            _drinkSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _getSickSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            _hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            _getSickAnimator.speed = 0f;

            Length = _drinkAnimationClip.length + _getSickAnimationClip.length + _fadeTime + _waitTime;
            this.InvokeAfterTime(_drinkAnimationClip.length, GetSickAnimation);
        }


        private void GetSickAnimation() {
            _drinkSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            _getSickSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            _getSickAnimator.speed = 1f;

            this.InvokeAfterTime(_getSickAnimationClip.length + _waitTime, FadeInHospital);
        }


        private void FadeInHospital() {
            TweenUtils.TweenAlpha(_getSickSpriteRenderer, 0f, _fadeTime, _fadeEasing);
            TweenUtils.TweenAlpha(_hospitalSpriteRenderer, 1f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}
