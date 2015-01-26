using UnityEngine;
using System;
using System.ComponentModel;
using Assets.Stem.Scripts.PropertyAttributes;


namespace Assets.Stem.Scripts.Gui.PivotAnimations {
    public abstract class PivotAnimation : MonoBehaviour {

        public event Action Finished = delegate { };

        [SerializeField, Readonly] private float _length;
        public float Length { get { return _length; } protected set { _length = value; } }


        public void Play() {
            gameObject.SetActive(true);
        }


        public void Stop() {
            gameObject.SetActive(false);
        }


        protected void RaiseFinishedEvent() {
            Finished.Invoke();
        }
    }
}
