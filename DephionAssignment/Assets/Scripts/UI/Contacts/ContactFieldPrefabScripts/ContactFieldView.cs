using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

/// <summary>
/// Class responsible only for the visuals of the Contact Field.
/// </summary>
public class ContactFieldView : MonoBehaviour
{
    [SerializeField] private Image ProfilePhoto_Image;
    [SerializeField] private TextMeshProUGUI Name_Text;
    [SerializeField] private TextMeshProUGUI Secondary_Text;
    [SerializeField] private Button MainFunctionality_Button;
    public void UpdateProfilePhoto(Sprite sprite) 
    {
        ProfilePhoto_Image.sprite = sprite; 
    }

    public void UpdateName(string name, string lastName) 
    {
        Name_Text.text = string.Concat(name, " ", lastName);
    }

    public void UpdateSecondaryText(string secondaryText = "")
    {
        Secondary_Text.text = secondaryText;
    }
    
    public void OnClickBehaviour(UnityAction action) 
    {
        MainFunctionality_Button.onClick.RemoveAllListeners();
        MainFunctionality_Button.onClick.AddListener(action);
    }
}
