using System.Collections;
using System.Security.Cryptography;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;



namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public class InfectOthersAnimation : PivotAnimation {

        [SerializeField] private SpriteRenderer _ebolaSpriteRenderer;
        [SerializeField] private Text _text;

        private string _initialText;

        [SerializeField] private float _delayPerLetter = 0.5f;
        [SerializeField] private float _extraTotalLength = 1f;


        private void OnEnable() {
            _initialText = _text.text;
            _text.text = "";

            _ebolaSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            Length = _initialText.Length * _delayPerLetter  + _extraTotalLength;
            _ebolaSpriteRenderer.color = Color.clear;

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
