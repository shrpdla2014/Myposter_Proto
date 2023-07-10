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
    // AR Ȱ���� ���� component
    ARFaceManager arFaceManager;
    ARSessionOrigin arSessionOrigin;
    // AR ȭ�鿡 ����� Mask list�� ������ �� list ����
    public List<Material> faceMaterials = new List<Material>();
    private int faceMaterialIndex; // ������ list�� ��

    public List<Sprite> faceTheme = new List<Sprite>();
    public Image ThemeOBJ;

    // ������ ��
    public GameObject rightPrefab;
    private GameObject rightobj;
    // ���� ��
    public GameObject leftPrefab;
    private GameObject leftobj;
    // �Ȱ�
    public GameObject nosePrefab;
    private GameObject noseobj;
    // �����
    public GameObject neckPrefab;
    private GameObject neckobj;
    public GameObject neckPrefab_2;
    private GameObject neckobj_2;

    // mask ������ ���� region���� ������
    private NativeArray<ARCoreFaceRegionData> regions;

    GameObject sceneControll;
    string choosenName;

    void Start()
    {
        // component ����
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

        // �̸� ������ ���� mask�� ���� ȭ�鿡 ����ϱ� ���� foreach��
        foreach (var face in arFaceManager.trackables)
        {
            // �� ������ ���� ���� ���� ��ġ ������ ��������
            arCoreSystem.GetRegionPoses(face.trackableId, Unity.Collections.Allocator.Persistent, ref regions);

            // �տ��� ������ ��ġ �����Ϳ� �����ؾ��ϴ� ������Ʈ �����ϱ�
            foreach (ARCoreFaceRegionData faceregion in regions)
            {
                ARCoreFaceRegion regiontype = faceregion.region;

                if (regiontype == ARCoreFaceRegion.ForeheadRight)
                {
                    // ������Ʈ�� �����Ǿ��ִ� ���, �ش� ������Ʈ�� ȭ�鿡 ���
                    if (!rightobj)
                    {
                        rightobj = Instantiate(rightPrefab, arSessionOrigin.trackablesParent);
                    }
                    // �� ��ġ�� ���� ����� �̻��ϱ� ������ position ���� �����Ͽ� �̸��� ������Ʈ �̵�
                    rightobj.transform.localPosition = faceregion.pose.position + new Vector3(-0.04f, 0.04f, 0f);
                    rightobj.transform.localRotation = faceregion.pose.rotation;
                }
                // ������ �����ߴ� �����ʰ� �����ϰ� ���� ����
                else if (regiontype == ARCoreFaceRegion.ForeheadLeft)
                {
                    if (!leftobj)
                    {
                        leftobj = Instantiate(leftPrefab, arSessionOrigin.trackablesParent);

                    }
                    leftobj.transform.localPosition = faceregion.pose.position + new Vector3(0.04f, 0.04f, 0f);
                    leftobj.transform.localRotation = faceregion.pose.rotation;
                }
                // �Ȱ� ����
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

    // ���� update���� ���� ��ġ�� �����ϱ� ���� ���̶�� �ش� �Լ��� ���� ��ġ�� mask�� �����ϱ� ���� �Լ�
    public void SwitchFace(int n)
    {
        // �̸� ����� ���� mask �ڿ� ����
        foreach (var face in arFaceManager.trackables)
        {
            face.GetComponent<Renderer>().material = faceMaterials[n];

        }
        // �׸��� sprite ���� ����
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
