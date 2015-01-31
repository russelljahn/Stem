using System.Security.Cryptography;
using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GetBusyAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _flytrapWaggleClip;
        [SerializeField] private AnimationClip _heartsClip;

        [SerializeField] private Animator _heartsAnimator;


        [SerializeField] private float _shortenFlytrapWaggleAnimationTime = .5f;
        [SerializeField]private float _shortenHeartsAnimationTime = .15f;


        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _flytrapWaggleSpriteRenderer;
        [SerializeField] private SpriteRenderer _heartsSpriteRenderer;
        [SerializeField] private SpriteRenderer _growFlowerSpriteRenderer;


        [SerializeField] private Animator _growFlowerAnimator;
        [SerializeField] private AnimationClip _growFlowerAnimationClip;

        [SerializeField] private float _bgNormalFadeTime = 4f;

        private void OnEnable() {
            _growFlowerAnimator.speed = 0f;
            _flytrapWaggleSpriteRenderer.color = Color.white;
            Length = _flytrapWaggleClip.length + _heartsClip.length + 3f*_fadeTime - _shortenFlytrapWaggleAnimationTime + _growFlowerAnimationClip.length;
            _heartsAnimator.speed = 0f;
            this.InvokeAfterTime(_flytrapWaggleClip.length - _shortenFlytrapWaggleAnimationTime, FadeInHearts);

            _heartsSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            AudioManager.LoadClip(AudioClips.BgGetBusy);
            AudioManager.PlayTrack(AudioClips.BgGetBusy);
            AudioManager.Crossfade(AudioClips.BgNormal, AudioClips.BgGetBusy);
            this.InvokeAfterTime(_bgNormalFadeTime, () => AudioManager.Crossfade(AudioClips.BgGetBusy, AudioClips.BgNormal));
        }


        private void FadeInHearts() {
            TweenUtils.TweenAlpha(_heartsSpriteRenderer, 1f, _fadeTime, _fadeEasing);
            _heartsAnimator.speed = 1f;
            this.InvokeAfterTime(_heartsClip.length - _shortenHeartsAnimationTime, FadeOutHearts);

        }


        private void FadeOutHearts() {
            _flytrapWaggleSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            TweenUtils.TweenAlpha(_heartsSpriteRenderer, 0f, _fadeTime, _fadeEasing, FadeInFlowerGrow);
        }

        private void FadeInFlowerGrow() {

            TweenUtils.TweenAlpha(_growFlowerSpriteRenderer, 1f, _fadeTime, _fadeEasing, GrowAnimation);
        }


        private void GrowAnimation() {
            _growFlowerAnimator.speed = 1f;
            this.InvokeAfterTime(_growFlowerAnimationClip.length, RaiseFinishedEvent);
        }
    }
}
