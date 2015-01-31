using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class PesticideAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _sequenceAnimationClip;
        [SerializeField]private AnimationClip _loopAnimationClip;


        [SerializeField] private SpriteRenderer _sequenceSpriteRenderer;
        [SerializeField] private SpriteRenderer _loopSpriteRenderer;

        [SerializeField] private WrapMode _wrapMode = WrapMode.Loop;

        [SerializeField] private float _pesticideSfxPlayDelay = 0.5f;
        [SerializeField] private float _pesticideSfxFadeOutTime = 2.0f;


  
        private void OnEnable() {
            _sequenceSpriteRenderer.color = Color.white;
            _loopSpriteRenderer.color = Color.clear;

            Length = _sequenceAnimationClip.length;
            this.InvokeAfterTime(Length, PlayLoop, RaiseFinishedEvent);

            this.InvokeAfterTime(_pesticideSfxPlayDelay, () => AudioManager.PlayTrackOneShot(AudioClips.SfxPesticide));
            this.InvokeAfterTime(_pesticideSfxFadeOutTime, () => AudioManager.Fade(AudioClips.SfxPesticide, 0f));
        }


        private void PlayLoop() {
            _sequenceSpriteRenderer.color = Color.clear;
            _loopSpriteRenderer.color = Color.white;

            _loopAnimationClip.wrapMode = _wrapMode;
        }


    }
}
