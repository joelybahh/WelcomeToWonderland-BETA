using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WW.Puzzles {
    public class Quiz : Puzzle  {

        public List<string> PuzzleQuestions;
        public List<int> PuzzleAnswers;
        public List<Text> QuizTexts;
        public List<ScreenManipulator> Screens;
        [SerializeField]
        int m_currentQuestion = 0;
        int maxQuestions;
        bool canAnswer = false;


        public void StartQuiz( ) {
            maxQuestions = PuzzleQuestions.Count - 1;
            foreach ( var screen in Screens ) {
                screen.m_on.Invoke();

            }
            canAnswer = true;
            ChangeScreenText(0);
        }

        public void ButtonPressed(int aInt) {
            if ( canAnswer ) {
                if ( CheckAnswer(aInt) ) {
                    if ( m_currentQuestion <= maxQuestions ) m_currentQuestion += 1;
                    else if ( m_currentQuestion > maxQuestions ) m_completed = true;
                    ChangeScreenText(m_currentQuestion);

                }
            }
        }

        public bool CheckAnswer(int aInt) {
            canAnswer = false;
            if ( aInt == PuzzleAnswers[m_currentQuestion] ) {

                return true;
            }
            else {
                StartCoroutine(IncorrectAnswer(10));
                return false;
            }
        }

        public void ChangeScreenText(int aInt) {
            foreach ( var t in QuizTexts ) {
                t.text = PuzzleQuestions[aInt];
            }
            StartCoroutine(WaitToAnswer(3));
        }

        public bool CanAnswer( ) {
            return canAnswer;
        }

        IEnumerator IncorrectAnswer(int secs) {
            foreach ( var s in Screens ) {
                s.m_off.Invoke();
            }
            yield return new WaitForSeconds(secs);

            foreach ( var s in Screens ) {
                s.m_on.Invoke();
            }
            StartCoroutine(WaitToAnswer(3));

        }
        IEnumerator WaitToAnswer(int secs) {
            yield return new WaitForSeconds(secs);
            canAnswer = true;
        }

    }
}
