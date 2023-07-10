using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    // AR 활용을 위한 component
    ARFaceManager arFaceManager;
    ARSessionOrigin arSessionOrigin;
    // AR 화면에 출력할 Mask list를 저장할 빈 list 생성
    public List<Material> faceMaterials = new List<Material>();
    private int faceMaterialIndex; // 선택한 list의 값

    public List<Sprite> faceTheme = new List<Sprite>();
    public Image ThemeOBJ;

    // 오른쪽 귀
    public GameObject rightPrefab;
    private GameObject rightobj;
    // 왼쪽 귀
    public GameObject leftPrefab;
    private GameObject leftobj;
    // 안경
    public GameObject nosePrefab;
    private GameObject noseobj;
    // 목걸이
    public GameObject neckPrefab;
    private GameObject neckobj;
    public GameObject neckPrefab_2;
    private GameObject neckobj_2;

    // mask 적용을 위한 region파일 블러오기
    private NativeArray<ARCoreFaceRegionData> regions;

    GameObject sceneControll;
    string choosenName;

    void Start()
    {
        // component 설정
        arFaceManager = GetComponent<ARFaceManager>();
        arSessionOrigin = GetComponent<ARSessionOrigin>();
        sceneControll = GameObject.Find("SceneManager");
        choosenName = sceneControll.GetComponent<GetData>().themeName;
        Facetrack();
        OpenSetting(choosenName);
    }

    // Update is called once per frame
    void Update()
    {
        Facetrack();
    }

    public void Facetrack()
    {
        ARCoreFaceSubsystem arCoreSystem = (ARCoreFaceSubsystem)arFaceManager.subsystem;

        // 미리 생성해 놓은 mask와 뿔을 화면에 출력하기 위한 foreach문
        foreach (var face in arFaceManager.trackables)
        {
            // 뿔 생성을 위한 눈과 코의 위치 데이터 가져오기
            arCoreSystem.GetRegionPoses(face.trackableId, Unity.Collections.Allocator.Persistent, ref regions);

            // 앞에서 가져온 위치 데이터에 생성해야하는 오브젝트 생성하기
            foreach (ARCoreFaceRegionData faceregion in regions)
            {
                ARCoreFaceRegion regiontype = faceregion.region;

                if (regiontype == ARCoreFaceRegion.ForeheadRight)
                {
                    // 오브젝트가 설정되어있는 경우, 해당 오브젝트를 화면에 출력
                    if (!rightobj)
                    {
                        rightobj = Instantiate(rightPrefab, arSessionOrigin.trackablesParent);
                    }
                    // 눈 위치에 뿔이 생기면 이상하기 때문에 position 값을 변경하여 이마로 오브젝트 이동
                    rightobj.transform.localPosition = faceregion.pose.position + new Vector3(-0.04f, 0.04f, 0f);
                    rightobj.transform.localRotation = faceregion.pose.rotation;
                }
                // 위에서 진행했던 오른쪽과 동일하게 왼쪽 진행
                else if (regiontype == ARCoreFaceRegion.ForeheadLeft)
                {
                    if (!leftobj)
                    {
                        leftobj = Instantiate(leftPrefab, arSessionOrigin.trackablesParent);

                    }
                    leftobj.transform.localPosition = faceregion.pose.position + new Vector3(0.04f, 0.04f, 0f);
                    leftobj.transform.localRotation = faceregion.pose.rotation;
                }
                // 안경 생성
                else if (regiontype == ARCoreFaceRegion.NoseTip)
                {
                    if (!noseobj)
                    {
                        noseobj = Instantiate(nosePrefab, arSessionOrigin.trackablesParent);

                    }
                    if (!neckobj)
                    {
                        neckobj = Instantiate(neckPrefab, arSessionOrigin.trackablesParent);

                    }
                    if (!neckobj_2)
                    {
                        neckobj_2 = Instantiate(neckPrefab_2, arSessionOrigin.trackablesParent);

                    }
                    noseobj.transform.localPosition = faceregion.pose.position + new Vector3(0f, 0.01f, 0f);
                    noseobj.transform.localRotation = faceregion.pose.rotation;

                    neckobj.transform.localPosition = faceregion.pose.position + new Vector3(0f, -0.065f, 0.5f);
                    neckobj.transform.localRotation = faceregion.pose.rotation;

                    neckobj_2.transform.localPosition = faceregion.pose.position + new Vector3(0f, -0.065f, 0.5f);
                    neckobj_2.transform.localRotation = faceregion.pose.rotation;
                }
            }
        }
    }

    // 위의 update문은 뿔의 위치를 설정하기 위한 것이라면 해당 함수는 코의 위치에 mask를 생성하기 위한 함수
    public void SwitchFace(int n)
    {
        // 미리 만들어 놓은 mask 코에 생성
        foreach (var face in arFaceManager.trackables)
        {
            face.GetComponent<Renderer>().material = faceMaterials[n];

        }
        // 테마의 sprite 값을 변경
        ThemeOBJ.sprite = faceTheme[n];

        switch (n)
        {
            case 0:
                sceneControll.GetComponent<GetData>().themeName = "ar_pic01";
                rightobj.SetActive(true);
                leftobj.SetActive(true);
                noseobj.SetActive(true);
                neckobj.SetActive(true);
                neckobj_2.SetActive(false);
                break;
            case 1:
                sceneControll.GetComponent<GetData>().themeName = "ar_pic02";
                rightobj.SetActive(true);
                leftobj.SetActive(true);
                noseobj.SetActive(false);
                neckobj.SetActive(false);
                neckobj_2.SetActive(true);
                break;
            case 2:
                sceneControll.GetComponent<GetData>().themeName = "ar_pic03";
                rightobj.SetActive(false);
                leftobj.SetActive(false);
                noseobj.SetActive(false);
                neckobj.SetActive(false);
                neckobj_2.SetActive(false);
                break;
            case 3:
                sceneControll.GetComponent<GetData>().themeName = "ar_pic04";
                rightobj.SetActive(true);
                leftobj.SetActive(true);
                noseobj.SetActive(false);
                neckobj.SetActive(false);
                neckobj_2.SetActive(true);
                break;
            case 4:
                sceneControll.GetComponent<GetData>().themeName = "ar_pic05";
                rightobj.SetActive(false);
                leftobj.SetActive(false);
                noseobj.SetActive(true);
                neckobj.SetActive(false);
                neckobj_2.SetActive(false);
                break;
            case 5:
                sceneControll.GetComponent<GetData>().themeName = "ar_pic06";
                rightobj.SetActive(false);
                leftobj.SetActive(false);
                noseobj.SetActive(false);
                neckobj.SetActive(true);
                neckobj_2.SetActive(false);
                break;
        }
    }

    public void OpenSetting(string themeName)
    {
        if (string.IsNullOrEmpty(themeName))
        {
            SwitchFace(0);
        }
        else
        {
            switch (themeName)
            {
                case "ar_pic01":
                    SwitchFace(0);
                    break;
                case "ar_pic02":
                    SwitchFace(1);
                    break;
                case "ar_pic03":
                    SwitchFace(2);
                    break;
                case "ar_pic04":
                    SwitchFace(3);
                    break;
                case "ar_pic05":
                    SwitchFace(4);
                    break;
                case "ar_pic06":
                    SwitchFace(5);
                    break;
            }
        }
    }
}
