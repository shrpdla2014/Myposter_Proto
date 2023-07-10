using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    GetData getData;

    public Image previewOBJ;
    // Start is called before the first frame update
    void Start()
    {
        getData = GameObject.Find("SceneManager").GetComponent<GetData>();
        BringImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BringImage()
    {
        FileInfo fi = new FileInfo(getData.themePath);
        if (fi.Exists)
        {
            Byte[] byteTexture = File.ReadAllBytes(getData.themePath);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(byteTexture);

            Rect rect = new Rect(0, 0, texture.width, texture.height);
            previewOBJ.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        }
    }

    public void DeleteImage()
    {
        FileInfo fi = new FileInfo(getData.themePath);
        if (fi.Exists)
        {
            File.Delete(getData.themePath);
        }
    }
}
