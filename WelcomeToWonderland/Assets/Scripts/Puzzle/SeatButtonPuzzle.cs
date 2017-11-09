using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WW.Puzzles {
    public class SeatButtonPuzzle : Puzzle {
        List<NewtonVR.NVRButton> buttons;
        


        // Update is called once per frame
        void Update( ) {
            m_completed = true;
            foreach ( var b in buttons ) {
                if ( !b.ButtonIsPushed ) m_completed = false; 
            }
            if ( m_completed ) CompletePuzzle();

        }
    }
}