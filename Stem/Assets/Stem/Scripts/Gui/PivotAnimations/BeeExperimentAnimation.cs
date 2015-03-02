using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class BeeExperimentAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _sequenceAnimationClip;
        [SerializeField] private AnimationClip _loopAnimationClip;


        [SerializeField] private SpriteRenderer _sequenceSpriteRenderer;
        [SerializeField] private SpriteRenderer _loopSpriteRenderer;

        [SerializeField] private WrapMode _wrapMode = WrapMode.Loop;

        [SerializeField] private float _sfx1PlayTime = 1f;
        [SerializeField] private float _sfx2PlayTime = 1f;

  
        private void OnEnable() {
            _sequenceSpriteRenderer.color = Color.white;
            _loopSpriteRenderer.color = Color.clear;

            Length = _sequenceAnimationClip.length;
            this.InvokeAfterTime(Length, PlayLoop, RaiseFinishedEvent);
            this.InvokeAfterTime(_sfx1PlayTime, () => AudioManager.PlayTrackOneShot(AudioClips.SfxBeeExperiment));
            this.InvokeAfterTime(_sfx2PlayTime, () => AudioManager.PlayTrackOneShot(AudioClips.SfxBeeMonster));

        }


        private void PlayLoop() {
            _sequenceSpriteRenderer.color = Color.clear;
            _loopSpriteRenderer.color = Color.white;

            _loopAnimationClip.wrapMode = _wrapMode;
        }


    }
}
