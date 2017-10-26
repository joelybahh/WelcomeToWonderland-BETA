using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Helpers {
    public class DontDestroyOnLoad : MonoBehaviour {

        void Awake () {
            DontDestroyOnLoad (transform.gameObject);

        }
    }
}