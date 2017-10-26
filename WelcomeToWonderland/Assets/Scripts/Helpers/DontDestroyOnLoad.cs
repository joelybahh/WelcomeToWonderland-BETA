using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Helpers {
    /// <summary>
    /// Desc: A simple helper class that can be attached to anything 
    ///       we want to not destroy on scene load (Managers, Stats..).
    /// Author: Joel Gabriel
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour {

        void Awake () {
            DontDestroyOnLoad (transform.gameObject);
        }
    }
}