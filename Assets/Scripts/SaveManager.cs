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

        // 혹시나 꺼진 상태로 유지 되는 것을 방지하기 위한 초기화
        saveButton.SetActive(true);
        
        // 나중에 켜저야 하는 이미지
        timeParent.SetActive(false);
        shootBack.SetActive(false);
    }

    // Start is called before the first frame update
    public void OnSave()
    {
        // 버튼은 안보이게 가리기
        saveButton.SetActive(false);

        // 타이머 출력
        timeParent.SetActive(true);

        // 프로그램 상에서 OnSave 함수가 실행되는 경우에만 코루틴 함수가 실행되도록 설정
        StartCoroutine(Rendering());
    }

    // 코루틴 함수 선언
    IEnumerator Rendering()
    {
        // 5초 대기 시간
        for (int i = 0; i < 5; i++)
        {
            timeOBJ.sprite = times[i];
            yield return new WaitForSecondsRealtime(1);
        }
        
        timeParent.SetActive(false);
        shootBack.SetActive(true);

        // 이전 행동이 끝날 때 까지 대기했다가 실행
        yield return new WaitForEndOfFrame();

        // 사용할 변수 선언
        byte[] imgBytes;
        string path;
        string time = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss");

        // 안드로이드에서 실행하는 경우와 PC에서 실행하는 경우를 구분해서 path 설정
        if (Application.platform == RuntimePlatform.Android)
        {
            // 만약 Download 폴더에 FourCut 폴더가 없다면 생성하기
            if (!Directory.Exists(AndroidRootPath() + "Download/FourCut"))
            {
                Directory.CreateDirectory(AndroidRootPath() + "Download/FourCut");
            }
            // 파일을 저장할 path 설정
            path = AndroidRootPath() + "Download/FourCut/" + time + ".png";
        }
        else {
            // PC인 경우의 path
            path = "./" + time + ".png";
        }

        // 저장하고 싶은 화면의 부분이 어디인지 직접 설정
        Texture2D texture = new Texture2D(Screen.width, Screen.height - ((Screen.height*2)/7), TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, ((Screen.height * 2) / 7), Screen.width, Screen.height - ((Screen.height * 2) / 7)), 0, 0, false);
        texture.Apply();

        // 위에서 설정한 부분의 Texture를 PNG 파일로 저장
        imgBytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, imgBytes);
        getData.GetPhotoLink(path);

        yield return new WaitForEndOfFrame();
        saveButton.SetActive(true);

        SceneScript.GoToPreview();
    }

    // 안드로이드에서 Download폴더의 주소를 return하는 함수
    string AndroidRootPath()
    {
        string[] temp = (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" }, System.StringSplitOptions.None);
        return (temp[0] + "/");
    }
}
