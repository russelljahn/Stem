using Assets.Stem.Scripts.Extensions;
using UnityEngine;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class WaterFlowerAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private AudioClip _sfxClip;
        [SerializeField] private float _sfxPlayTime = 0.2f;

        [SerializeField] private float _peeParticlesPlayTime = 0.5f;
        [SerializeField] private float _stoppedPouringTime = 2.0f;
        [SerializeField] private ParticleSystem _waterParticles;


        private float _initialPeeParticlesEmissionRate;
  
        private void OnEnable() {
            Length = Mathf.Max(_animationClip.length, _sfxPlayTime);
            _initialPeeParticlesEmissionRate = _waterParticles.emissionRate;
            _waterParticles.emissionRate = 0f;
            _waterParticles.Play();

            this.InvokeAfterTime(_sfxPlayTime, () => AudioManager.PlayTrackOneShot(_sfxClip.name));

            this.InvokeAfterTime(_peeParticlesPlayTime, () => _waterParticles.emissionRate = _initialPeeParticlesEmissionRate);
            this.InvokeAfterTime(_stoppedPouringTime, () => _waterParticles.emissionRate = 0f);

            this.InvokeAfterTime(Length, RaiseFinishedEvent);
        }


        private void OnDisable() {
            _waterParticles.emissionRate = _initialPeeParticlesEmissionRate;
        }


    }
}
