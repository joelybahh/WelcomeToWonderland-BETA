using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quiz : MonoBehaviour {

    public List<string> PuzzleQuestions;
    public List<int> PuzzleAnswers;
    public List<Text> QuizTexts;
    public List<ScreenManipulator> Screens;
    int m_currentQuestion;
    bool canAnswer;


    private void Update( ) {
        if(Input.GetKeyUp(KeyCode.Space) )StartQuiz();
    }

    public void StartQuiz( ) {
        foreach ( var screen in Screens ) {
            screen.m_on.Invoke();
        }
        ChangeScreenText(0);
    }

    public bool CheckAnswer(int aInt) {
        canAnswer = false;
        if( aInt == PuzzleAnswers[m_currentQuestion] ) {
            return true;
        }
        else {
            StartCoroutine(IncorrectAnswer(10));
            return false;
        }
    }

    public void ChangeScreenText(int aInt ) {
        foreach ( var t in QuizTexts ) {
            t.text = PuzzleQuestions[aInt];
        }
        canAnswer = true;
    }

    public bool CanAnswer( ) {
        return canAnswer;
    }

    IEnumerator IncorrectAnswer(int secs ) {
        foreach ( var s in Screens ) {
            s.m_off.Invoke();
        }
        yield return new WaitForSeconds(secs);

        foreach ( var s in Screens ) {
            s.m_on.Invoke();
        }
        canAnswer = true;

    } 
	
}
