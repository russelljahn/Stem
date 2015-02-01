using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class SpaceBeesAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _spaceBeesClip;
        [SerializeField] private AnimationClip _flowerGrowsClip;

        [SerializeField] private Animator _flowerGrowsAnimator;

        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _spaceBeesSpriteRenderer;
        [SerializeField] private SpriteRenderer _flowerGrowsSpriteRenderer;

        [SerializeField] private float _crossfadeNormalBgTime = 2.0f;


        private void OnEnable() {
            Length = _spaceBeesClip.length + _flowerGrowsClip.length - _fadeTime;
            _spaceBeesSpriteRenderer.color = Color.white;

            _flowerGrowsAnimator.speed = 0f;
            this.InvokeAfterTime(_spaceBeesClip.length - _fadeTime, FadeToFlowerGrows);

            _flowerGrowsSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            AudioManager.LoadClip(AudioClips.BgSpaceBees);
            AudioManager.PlayTrack(AudioClips.BgSpaceBees);
            AudioManager.Crossfade(AudioClips.BgNormal, AudioClips.BgSpaceBees);
            this.InvokeAfterTime(_crossfadeNormalBgTime, () => AudioManager.Crossfade(AudioClips.BgSpaceBees, AudioClips.BgNormal));
        }


        private void FadeToFlowerGrows() {
            _flowerGrowsSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            TweenUtils.TweenAlpha(_spaceBeesSpriteRenderer, 0f, _fadeTime, _fadeEasing);

            _flowerGrowsAnimator.speed = 1f;
            this.InvokeAfterTime(_flowerGrowsClip.length, RaiseFinishedEvent);

        }

    }
}
