using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumBtn : MonoBehaviour
{
    public TextMeshProUGUI displayText; // 입력된 숫자를 표시할 텍스트 컴포넌트

    public string inputNumber = ""; // 입력된 숫자를 저장할 변수

    public void OnButtonClicked(int number)
    {
        // 입력된 숫자를 저장
        inputNumber += number.ToString();

        // 텍스트 컴포넌트에 입력된 숫자를 표시
        displayText.text = inputNumber;
    }

    public void DeleteNumber()
    {
        // 입력된 숫자의 마지막 자리 제거
        if (inputNumber.Length > 0)
        {
            inputNumber = inputNumber.Substring(0, inputNumber.Length - 1);
            displayText.text = inputNumber;
        }
    }

}
