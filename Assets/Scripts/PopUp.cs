using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI phoneNum;
    public TextMeshProUGUI CheckPhoneNum;

    void Start()
    {
        Panel.SetActive(false);
    }

    public void OpenPanel()
    {
        if(Panel != null)
        {
            Panel.SetActive(true);
            CheckPhoneNum.text = phoneNum.text;
        }
    }

    public void ClosePanel()
    {
        if(Panel != null)
        {
            Panel.SetActive(false);
        }
    }

    //씬 넘기는 함수 추가
}
