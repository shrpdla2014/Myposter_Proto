using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumBtn : MonoBehaviour
{
    public TextMeshProUGUI displayText; // �Էµ� ���ڸ� ǥ���� �ؽ�Ʈ ������Ʈ

    public string inputNumber = ""; // �Էµ� ���ڸ� ������ ����

    public void OnButtonClicked(int number)
    {
        // �Էµ� ���ڸ� ����
        inputNumber += number.ToString();

        // �ؽ�Ʈ ������Ʈ�� �Էµ� ���ڸ� ǥ��
        displayText.text = inputNumber;
    }

    public void DeleteNumber()
    {
        // �Էµ� ������ ������ �ڸ� ����
        if (inputNumber.Length > 0)
        {
            inputNumber = inputNumber.Substring(0, inputNumber.Length - 1);
            displayText.text = inputNumber;
        }
    }

}
