using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Audio {
    public class Title : MonoBehaviour {

        public event Action Closed = delegate { };

        public CanvasGroup CanvasGroup;

        public SpriteRenderer SpriteRenderer;

        private void Start() {
            this.InvokeAfterTime(2f, Fade);
        }


        private void Fade() {
            TweenUtils.TweenAlpha(CanvasGroup, 0f, 1f, null, Closed);
            TweenUtils.TweenAlpha(SpriteRenderer, 0f, 1f, null);

        }
    }
}
