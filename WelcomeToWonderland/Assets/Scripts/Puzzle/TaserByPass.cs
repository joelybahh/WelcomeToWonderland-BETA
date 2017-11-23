using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW.Puzzles {
    public class TaserByPass : MonoBehaviour {

        public Puzzle puzzle;
        public AudioClip m_taserClip;
        private void Start () {
        }

        private void OnCollisionEnter(Collision collision) {
            if ( collision.gameObject.tag == "Lighter" ) {
                puzzle.CompletePuzzle();
                //TODO: PLAY VOICE LINE HERE
            }
        }
    }
}