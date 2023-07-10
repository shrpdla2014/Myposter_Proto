using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTheme : MonoBehaviour
{
    GameObject distroy;
    LoadImage distroyImage;

    void Start()
    {
        distroy = GameObject.Find("SceneManager");
        distroyImage = this.GetComponent<LoadImage>();
    }

    public void GoToTheme()
    {
        SceneManager.LoadScene("Theme_Selection");
        if (distroyImage != null)
            distroyImage.DeleteImage();
        
        if (distroy != null)
            Destroy(distroy);
        
    }
    public void GoToAR()
    {
        SceneManager.LoadScene("CameraAr");
        if (distroyImage != null)
            distroyImage.DeleteImage();

    }

    public void GoToPreview()
    {
        SceneManager.LoadScene("Preview");
    }

    public void GoToConsent()
    {
        SceneManager.LoadScene("Agree");
    }

    public void GoToInput()
    {
        SceneManager.LoadScene("SenPic");
    }

    public void GoToEnd()
    {
        SceneManager.LoadScene("Complete");
    }

    public void EndProgram()
    {
        SceneManager.LoadScene("Start");
        if (distroy != null)
            Destroy(distroy);
    }
}
