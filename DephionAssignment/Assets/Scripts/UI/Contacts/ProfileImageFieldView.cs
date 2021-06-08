using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ProfileImageFieldView : MonoBehaviour
{
    [SerializeField] private Button Select_Button;
    [SerializeField] private Image ProfilePhoto_Sprite;
    [SerializeField] private RectTransform SelectedImage_Transform;

    public void RemoveListeners() 
    {
        Select_Button.onClick.RemoveAllListeners();
    }

    public void SetListeners(UnityAction OnSelectedImage) 
    {
        Select_Button.onClick.AddListener(OnSelectedImage);
    }

    public void SetSelectedActive(bool active) 
    {
        SelectedImage_Transform.gameObject.SetActive(active);
    }

    public void SetImage(Sprite sprite) 
    {
        ProfilePhoto_Sprite.sprite = sprite;
    }
}
