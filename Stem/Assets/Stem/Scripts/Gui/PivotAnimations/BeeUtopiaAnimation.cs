using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class BeeUtopiaAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _sequenceAnimationClip;
        [SerializeField] private AnimationClip _loopAnimationClip;


        [SerializeField] private SpriteRenderer _sequenceSpriteRenderer;
        [SerializeField] private SpriteRenderer _loopSpriteRenderer;

        [SerializeField] private WrapMode _wrapMode = WrapMode.Loop;
        [SerializeField] private float _bgNormalFadeTime = 2f;
        [SerializeField] private float _bgNormalFadeVolume = 0.25f;


  
        private void OnEnable() {
            _sequenceSpriteRenderer.color = Color.white;
            _loopSpriteRenderer.color = Color.clear;

            Length = _sequenceAnimationClip.length;
            this.InvokeAfterTime(Length, PlayLoop, RaiseFinishedEvent);

            AudioManager.LoadClip(AudioClips.SfxBees);
            AudioManager.PlayTrack(AudioClips.SfxBees);
            AudioManager.Crossfade(AudioClips.BgNormal, AudioClips.SfxBees, _bgNormalFadeVolume);

            this.InvokeAfterTime(_bgNormalFadeTime, () => AudioManager.Crossfade(AudioClips.SfxBees, AudioClips.BgNormal));
            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


        private void PlayLoop() {
            _sequenceSpriteRenderer.color = Color.clear;
            _loopSpriteRenderer.color = Color.white;

            _loopAnimationClip.wrapMode = _wrapMode;
        }


    }
}
