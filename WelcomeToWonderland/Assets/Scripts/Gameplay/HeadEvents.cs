using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WW.Managers;
using WW.Puzzles;
//using SceneManagement;

public class HeadEvents : MonoBehaviour {

    [SerializeField] TimeManager Quiz;
    [SerializeField] TimeManager GameTimer;
    [SerializeField] Quiz QuizPuzzle;


    private void OnTriggerEnter ( Collider a_other ) {
        if (a_other.tag == "ShowFloor") {
            AudioManager.Instance.PlayVoiceLine (14);
        }
        if (a_other.tag == "StageQuiz") {
            AudioManager.Instance.PlayVoiceLine (18);
            StartCoroutine (delay (47));
        }
        if (a_other.tag == "END") {
            Application.Quit ();
        }
    }

    IEnumerator delay ( int delay ) {

        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlayVoiceLine (19);

        Quiz.gameObject.SetActive (true);
        GameTimer.gameObject.SetActive(false);
        QuizPuzzle.StartQuiz ();


    } 
}
