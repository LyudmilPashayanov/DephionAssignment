using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CRUDControllerView : MonoBehaviour
{
    [SerializeField] private TMP_InputField FirstName_InputField;
    [SerializeField] private TMP_InputField LastName_InputField;
    [SerializeField] private TMP_InputField Phone_InputField;
    [SerializeField] private TMP_InputField Description_InputField;
    [SerializeField] private TMP_InputField Email_InputField;
    [SerializeField] private TMP_InputField Twitter_InputField;

    [SerializeField] private TextMeshProUGUI Header_Text;

    [SerializeField] private Button Save_Button;
    [SerializeField] private Button Cancel_Button;

    public void RemoveListeners() 
    {
        Save_Button.onClick.RemoveAllListeners();
        Cancel_Button.onClick.RemoveAllListeners();
    }

    public void SetListeners(UnityAction SaveButtonFunctionality, UnityAction CancelButtonFunctionality) 
    {
        Save_Button.onClick.AddListener(SaveButtonFunctionality);
        Cancel_Button.onClick.AddListener(CancelButtonFunctionality);

    }

    public void SetHeader(string header) 
    {
        Header_Text.text = header;
    }

    public void EditContact(Contact contact)
    {
        if (contact.FirstName != null) 
        {
            FirstName_InputField.text = contact.FirstName;
        }
        if (contact.LastName != null)
        {
            LastName_InputField.text = contact.LastName;
        }
        if (contact.Phone != null)
        {
            Phone_InputField.text = contact.Phone;
        }
        if (contact.Description != null)
        {
            Description_InputField.text = contact.Description;
        }
        if (contact.Email != null)
        {
            Email_InputField.text = contact.Email;
        }
        if (contact.Twitter != null)
        {
            Twitter_InputField.text = contact.Twitter;
        }
    }

    public void ClearAllFields() 
    {
        FirstName_InputField.text = null;
        LastName_InputField.text = null;
        Phone_InputField.text = null;
        Description_InputField.text = null;
        Email_InputField.text = null;
        Twitter_InputField.text= null;
    }
    public string GetCurrentlyWrittenFirstName() 
    {
        return FirstName_InputField.text;
    }

    public string GetCurrentlyWrittenLastName()
    {
        return LastName_InputField.text;
    }

    public string GetCurrentlyWrittenPhoneNumber()
    {
        return Phone_InputField.text;
    }

    public string GetCurrentlyWrittenDescription()
    {
        return Description_InputField.text;
    }

    public string GetCurrentlyWrittenEmail()
    {
        return Email_InputField.text;
    }

    public string GetCurrentlyWrittenTwitter()
    {
        return Twitter_InputField.text;
    }
}
