using UnityEngine;
using System;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public abstract class PivotAnimation : MonoBehaviour {

        public event Action Finished = delegate { };
        public float Length { get; protected set; }


        public void Play() {
            enabled = true;
        }


        public void Stop() {
            enabled = false;
        }
    }
}
