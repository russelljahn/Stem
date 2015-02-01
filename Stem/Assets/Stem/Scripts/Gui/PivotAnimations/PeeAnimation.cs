using Assets.Stem.Scripts.Extensions;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class PeeAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private AudioClip _sfxClip;
        [SerializeField] private float _sfxPlayTime = 0.75f;

        [SerializeField] private bool _sfxFadeOut = false;
        [SerializeField] private float _sfxFadeOutTime = 2.25f;

        [SerializeField] private float _peeParticlesPlayTime = 0.6f;
        [SerializeField] private float _stoppedPeeingTime = 3.3f;
        [SerializeField] private ParticleSystem _peeParticles;


        private float _initialPeeParticlesEmissionRate;
  
        private void OnEnable() {
            Length = Mathf.Max(_animationClip.length, _sfxPlayTime);
            _initialPeeParticlesEmissionRate = _peeParticles.emissionRate;
            _peeParticles.emissionRate = 0f;
            _peeParticles.Play();

            this.InvokeAfterTime(_sfxPlayTime, () => AudioManager.PlayTrackOneShot(_sfxClip.name));

            if (_sfxFadeOut) {
                this.InvokeAfterTime(_sfxFadeOutTime, () => AudioManager.Fade(_sfxClip.name, 0f));                
            }

            this.InvokeAfterTime(_peeParticlesPlayTime, () => _peeParticles.emissionRate = _initialPeeParticlesEmissionRate);
            this.InvokeAfterTime(_stoppedPeeingTime, () => _peeParticles.emissionRate = 0f);

            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


        private void OnDisable() {
            _peeParticles.emissionRate = _initialPeeParticlesEmissionRate;
        }


    }
}
