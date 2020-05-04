using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPanel : MonoBehaviour
{
    [SerializeField]
    Button picPreview;

    [SerializeField]
    Text textPreview;

    public void SetPicture(Texture2D pic)
    {
        Sprite picSprite = Sprite.Create(pic, new Rect(0.0f, 0.0f, pic.width, pic.height), new Vector2(0.5f, 0.5f), 100.0f);
        picPreview.image.sprite = picSprite;
    }

    public void SetText(string s)
    {
        textPreview.text = s;
    }
}
