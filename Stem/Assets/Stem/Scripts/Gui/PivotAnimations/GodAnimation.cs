using System.Collections;
using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class GodAnimation : PivotAnimation {

        [SerializeField] private Text _text;

        private string _initialText;

        [SerializeField] private float _delayPerLetter = 0.5f;
        [SerializeField] private float _extraAnimationLength = 1f;

        [SerializeField] private float _fourthWallSfxPitch = 0.75f;

        [SerializeField] private float _bgNormalFadeVolume = 0.25f;

        private void OnEnable() {
            _initialText = _text.text;
            _text.text = "";

            Length = _initialText.Length * _delayPerLetter + _extraAnimationLength;

            AudioManager.Fade(AudioClips.BgNormal, _bgNormalFadeVolume);
            AudioManager.PlayTrackOneShot(AudioClips.SfxOrchestra);

            var fadeInTime = AudioClips.GetClip(AudioClips.SfxOrchestra).length;
            this.InvokeAfterTime(fadeInTime, () => AudioManager.Fade(AudioClips.BgNormal));

            StartCoroutine(TextAnimation());
        }


        private IEnumerator TextAnimation() {
            int currentLetter = 0;
            while (currentLetter < _initialText.Length) {
                _text.text += _initialText[currentLetter];
                yield return new WaitForSeconds(_delayPerLetter);
                ++currentLetter;
            }
            RaiseFinishedEvent();
        }

    }
}
