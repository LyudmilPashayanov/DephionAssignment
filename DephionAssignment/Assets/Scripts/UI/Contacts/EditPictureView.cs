using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class responsible only for the visuals of the "Edit a picture" UI.
/// </summary>
public class EditPictureView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Header_text;
    
    public void SetHeaderText(string text) 
    {
        Header_text.text = text;
    }
}
