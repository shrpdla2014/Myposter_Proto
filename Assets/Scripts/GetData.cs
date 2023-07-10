using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    // ������ Theme�� ������ �޾ƿ��� ���� ����
    public string themeName;
    public string themePath;
    BackToTheme sceneControl;

    void Start()
    {
        // SceneControll ��ũ��Ʈ�� Ȱ���ϱ� ���� ����
        sceneControl = this.GetComponent<BackToTheme>();
    }

    // ȭ����� ��ư�� Ŭ���ϸ� �� �Լ��� ����Ǿ� ������ �����Ͱ� �������� ������ �ڿ� ���� ȭ������ �̵�
    public void OnButtonClicked(Btn buttonScript)
    {
        themeName = buttonScript.GetVariableData();
        sceneControl.GoToAR();
    }

    // ������ ��� ���� ����� �̹����� �ּҸ� �޾ƿ��� ���� �Լ�
    public void GetPhotoLink(string photoLink)
    {
        themePath = photoLink;
    }
}
