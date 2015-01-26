using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GrowFlowerAnimation : PivotAnimation {

        [SerializeField] private Animator _growFlowerAnimator;
        [SerializeField] private AnimationClip _growFlowerAnimationClip;

        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _spriteRenderer;



        private void OnEnable() {
            _growFlowerAnimator.speed = 0f;
            Length = _fadeTime + _growFlowerAnimationClip.length;

            _spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            TweenUtils.TweenAlpha(_spriteRenderer, 1f, _fadeTime, _fadeEasing, () => { _growFlowerAnimator.speed = 1f; });
            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }

    }
}
