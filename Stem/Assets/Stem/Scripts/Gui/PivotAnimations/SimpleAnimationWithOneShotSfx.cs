using Assets.Stem.Scripts.Extensions;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class SimpleAnimationWithOneShotSfx : PivotAnimation {

        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private AudioClip _sfxClip;
        [SerializeField] private float _sfxPlayTime = 1f;

        [SerializeField] private bool _sfxFadeOut = false;
        [SerializeField] private float _sfxFadeOutTime = 1f;


  
        private void OnEnable() {
            Length = Mathf.Max(_animationClip.length, _sfxPlayTime);

            this.InvokeAfterTime(_sfxPlayTime, () => AudioManager.PlayTrackOneShot(_sfxClip.name));

            if (_sfxFadeOut) {
                this.InvokeAfterTime(_sfxFadeOutTime, () => AudioManager.Fade(_sfxClip.name, 0f));                
            }

            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


    }
}
