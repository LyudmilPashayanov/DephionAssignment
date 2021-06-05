using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContactFieldView : MonoBehaviour
{
    [SerializeField] private Image profilePhoto_image;
    [SerializeField] private TextMeshProUGUI name_text;
    [SerializeField] private TextMeshProUGUI secondary_text;
    public void UpdateProfilePhoto(Sprite sprite) 
    {
        profilePhoto_image.sprite = sprite; 
    }

    public void UpdateName(string name, string lastName) 
    {
        name_text.text = string.Concat(name, " ", lastName);
    }

    public void UpdateSecondaryText(string secondaryText = "")
    {
        secondary_text.text = secondaryText;
    }
}
