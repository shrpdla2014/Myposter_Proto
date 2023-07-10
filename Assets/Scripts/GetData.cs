using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    // 선택한 Theme의 정보를 받아오기 위한 변수
    public string themeName;
    public string themePath;
    BackToTheme sceneControl;

    void Start()
    {
        // SceneControll 스크립트를 활용하기 위한 선언
        sceneControl = this.GetComponent<BackToTheme>();
    }

    // 화면상의 버튼을 클릭하면 이 함수가 실행되어 선택한 데이터가 무엇인지 저장한 뒤에 다음 화면으로 이동
    public void OnButtonClicked(Btn buttonScript)
    {
        themeName = buttonScript.GetVariableData();
        sceneControl.GoToAR();
    }

    // 사진을 찍고 나서 저장된 이미지의 주소를 받아오기 위한 함수
    public void GetPhotoLink(string photoLink)
    {
        themePath = photoLink;
    }
}
