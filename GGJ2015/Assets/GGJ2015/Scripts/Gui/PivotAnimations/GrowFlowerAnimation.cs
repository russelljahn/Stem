using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class GrowFlowerAnimation : PivotAnimation {

        [SerializeField] private Animator _growFlowerAnimator;
        [SerializeField] private AnimationClip _growFlowerAnimationClip;

        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _spriteRenderer;



        private void OnEnable() {
            _growFlowerAnimator.speed = 0f;
            Length = _fadeTime + _growFlowerAnimationClip.length;

            TweenUtils.TweenAlpha(_spriteRenderer, 1f, _fadeTime, _fadeEasing, () => { _growFlowerAnimator.speed = 1f; });
            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }

    }
}