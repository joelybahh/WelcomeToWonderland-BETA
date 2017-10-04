using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_LockScreen : MonoBehaviour {

    [SerializeField] private UnityEngine.UI.Text m_inputField;
    private string m_passcode = "1234";

    [SerializeField] private UnityEngine.UI.Text m_puzzlePieceOne;
    [SerializeField] private UnityEngine.UI.Text m_puzzlePieceTwo;
    [SerializeField] private UnityEngine.UI.Text m_puzzlePieceThree;
    [SerializeField] private UnityEngine.UI.Text m_puzzlePieceFour;

    void Start() {
        int firstNum = Random.Range(0,9);
        int secondNum = Random.Range(0, 9);
        int thirdNum = Random.Range(0, 9);
        int fourthNum = Random.Range(0, 9);

        m_puzzlePieceOne.text = firstNum.ToString();
        m_puzzlePieceTwo.text = secondNum.ToString();
        m_puzzlePieceThree.text = thirdNum.ToString();
        m_puzzlePieceFour.text = fourthNum.ToString();

        m_passcode = m_puzzlePieceOne.text + 
                     m_puzzlePieceTwo.text + 
                     m_puzzlePieceThree.text + 
                     m_puzzlePieceFour.text;
    }

    public void UpdateLockScreen(Collider a_other, string a_buttonTag){
        if (a_other.tag == a_buttonTag) {
            int entered = (int.Parse(a_other.gameObject.name) + 1);
            Debug.Log("You Pressed Keypad" + (int.Parse(a_other.gameObject.name) + 1));

            if (entered == 10)
            {
                m_inputField.text = "";
            }
            else if (entered == 11)
            {
                if (m_inputField.text == m_passcode)
                {
                    m_inputField.text = "success";
                    ClearInputFields();
                    SE_TabletScreenState.SwapToState(SE_TabletScreenState.eScreenState.HOME_SCREEN);
                }
                else
                {
                    m_inputField.text = "nope";
                    StartCoroutine(ClearOnFail(1.0f));
                }
            }
            else {
                if(m_inputField.text.Length < 4)  m_inputField.text += entered.ToString();
            }
        }
    }

    private void ClearInputFields() {
        m_inputField.text = "";
    }
    
    private IEnumerator ClearOnFail(float a_timeToClear) {
        yield return new WaitForSeconds(a_timeToClear);
        m_inputField.text = "";
    }
}
