using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.GGJ2015.Scripts {
    [RequireComponent(typeof(CanvasGroup))]
    public class Page : MonoBehaviour {
        public List<Text> _choicesTexts = new List<Text>();
    }
}
