using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public ARManager aRManager;
    public int Theme_Num;

    public void OnButtonClick()
    {
        aRManager.SwitchFace(Theme_Num);
    }
}
