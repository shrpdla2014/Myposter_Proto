using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class SaveManager : MonoBehaviour
{
    GameObject sceneControll;
    GetData getData;
    BackToTheme SceneScript;

    public GameObject saveButton;

    public GameObject timeParent;
    public Image timeOBJ;
    public Sprite[] times;
    public GameObject shootBack;

    void Start()
    {
        sceneControll = GameObject.Find("SceneManager");
        getData = sceneControll.GetComponent<GetData>();
        SceneScript = sceneControll.GetComponent<BackToTheme>();

        // Ȥ�ó� ���� ���·� ���� �Ǵ� ���� �����ϱ� ���� �ʱ�ȭ
        saveButton.SetActive(true);
        
        // ���߿� ������ �ϴ� �̹���
        timeParent.SetActive(false);
        shootBack.SetActive(false);
    }

    // Start is called before the first frame update
    public void OnSave()
    {
        // ��ư�� �Ⱥ��̰� ������
        saveButton.SetActive(false);

        // Ÿ�̸� ���
        timeParent.SetActive(true);

        // ���α׷� �󿡼� OnSave �Լ��� ����Ǵ� ��쿡�� �ڷ�ƾ �Լ��� ����ǵ��� ����
        StartCoroutine(Rendering());
    }

    // �ڷ�ƾ �Լ� ����
    IEnumerator Rendering()
    {
        // 5�� ��� �ð�
        for (int i = 0; i < 5; i++)
        {
            timeOBJ.sprite = times[i];
            yield return new WaitForSecondsRealtime(1);
        }
        
        timeParent.SetActive(false);
        shootBack.SetActive(true);

        // ���� �ൿ�� ���� �� ���� ����ߴٰ� ����
        yield return new WaitForEndOfFrame();

        // ����� ���� ����
        byte[] imgBytes;
        string path;
        string time = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss");

        // �ȵ���̵忡�� �����ϴ� ���� PC���� �����ϴ� ��츦 �����ؼ� path ����
        if (Application.platform == RuntimePlatform.Android)
        {
            // ���� Download ������ FourCut ������ ���ٸ� �����ϱ�
            if (!Directory.Exists(AndroidRootPath() + "Download/FourCut"))
            {
                Directory.CreateDirectory(AndroidRootPath() + "Download/FourCut");
            }
            // ������ ������ path ����
            path = AndroidRootPath() + "Download/FourCut/" + time + ".png";
        }
        else {
            // PC�� ����� path
            path = "./" + time + ".png";
        }

        // �����ϰ� ���� ȭ���� �κ��� ������� ���� ����
        Texture2D texture = new Texture2D(Screen.width, Screen.height - ((Screen.height*2)/7), TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, ((Screen.height * 2) / 7), Screen.width, Screen.height - ((Screen.height * 2) / 7)), 0, 0, false);
        texture.Apply();

        // ������ ������ �κ��� Texture�� PNG ���Ϸ� ����
        imgBytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, imgBytes);
        getData.GetPhotoLink(path);

        yield return new WaitForEndOfFrame();
        saveButton.SetActive(true);

        SceneScript.GoToPreview();
    }

    // �ȵ���̵忡�� Download������ �ּҸ� return�ϴ� �Լ�
    string AndroidRootPath()
    {
        string[] temp = (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" }, System.StringSplitOptions.None);
        return (temp[0] + "/");
    }
}
