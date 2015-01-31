using Assets.Stem.Scripts.Extensions;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class SimpleAnimationWithOneShotSfx : PivotAnimation {

        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private AudioClip _sfxClip;
        [SerializeField] private float _sfxPlayTime = 1f;

  
        private void OnEnable() {
            Length = Mathf.Max(_animationClip.length, _sfxPlayTime);
            this.InvokeAfterTime(_sfxPlayTime, () => AudioManager.PlayTrackOneShot(_sfxClip.name));
            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


    }
}
