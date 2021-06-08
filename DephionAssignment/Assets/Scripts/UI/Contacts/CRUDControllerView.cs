using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Class responsible only for the visuals of the "CRUD Contact" UI Page.
/// </summary>
public class CRUDControllerView : MonoBehaviour
{
    [SerializeField] private TMP_InputField FirstName_InputField;
    [SerializeField] private TMP_InputField LastName_InputField;
    [SerializeField] private TMP_InputField Phone_InputField;
    [SerializeField] private TMP_InputField Description_InputField;
    [SerializeField] private TMP_InputField Email_InputField;
    [SerializeField] private TMP_InputField Twitter_InputField;
    [SerializeField] private Image ProfilePicture_Image;
    [SerializeField] private TextMeshProUGUI Header_Text;
    [SerializeField] private Button Save_Button;
    [SerializeField] private Button Back_Button;
    [SerializeField] private Button Delete_Button;
    [SerializeField] private Button EditPicture_Button;
    [SerializeField] private Button Discard_Button;
    [SerializeField] private Button ContinueEditing_Button;
    [SerializeField] private Button InputBlocker_Button;
    [SerializeField] private RectTransform AreYouSureUI;
    [SerializeField] private RectTransform EditPictureUI;
    private string m_CurrentlySelectedImage;

    public void RemoveListeners() 
    {
        Save_Button.onClick.RemoveAllListeners();
        Back_Button.onClick.RemoveAllListeners();
        Delete_Button.onClick.RemoveAllListeners();
        EditPicture_Button.onClick.RemoveAllListeners();
        Discard_Button.onClick.RemoveAllListeners();
        ContinueEditing_Button.onClick.RemoveAllListeners();
        InputBlocker_Button.onClick.RemoveAllListeners();
    }

    public void SetListeners(UnityAction SaveButtonFunctionality, UnityAction AreYouSureGoingBackButtonFunctionality,
        UnityAction DeleteContact,UnityAction EditPicture, UnityAction DiscardFunctionality,UnityAction ContinueEditingFunctionality) 
    {
        Save_Button.onClick.AddListener(SaveButtonFunctionality);
        Back_Button.onClick.AddListener(AreYouSureGoingBackButtonFunctionality);
        Delete_Button.onClick.AddListener(DeleteContact);
        EditPicture_Button.onClick.AddListener(EditPicture);
        Discard_Button.onClick.AddListener(DiscardFunctionality);
        ContinueEditing_Button.onClick.AddListener(ContinueEditingFunctionality);
        InputBlocker_Button.onClick.AddListener(ContinueEditingFunctionality);
    }

    public void ActivateDeleteButton(bool active) 
    {
        Delete_Button.gameObject.SetActive(active);
    }

    public void SetHeader(string header) 
    {
        Header_Text.text = header;
    }

    public void AreYouSureTurnActive(bool active)
    {
        AreYouSureUI.gameObject.SetActive(active);
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
        if(contact.Photo != null) 
        {
            SetProfilePicture(contact.Photo);
        }
    }

    public void SetProfilePicture(string image) 
    {
        ProfilePicture_Image.sprite = ContactsCatalogManager.Instance.GetProfileImage(image);
        m_CurrentlySelectedImage = image;
    }

    public void ShowEditPictureUI(bool active)
    {
        Vector2 goToScale = UIManager.Instance.GetCanvasSize();
        if (active) 
        {
            EditPictureUI.gameObject.SetActive(true);
            DOTween.Sequence()
                //.Append(EditPictureUI.DOSizeDelta(new Vector2(0,goToScale.y), 0.1f))
                .Append(EditPictureUI.DOSizeDelta(new Vector2(goToScale.x, goToScale.y), 0.2f));
            return;
        }
        DOTween.Sequence()
                //.Append(EditPictureUI.DOSizeDelta(new Vector2(0, goToScale.y), 0.1f))
                .Append(EditPictureUI.DOSizeDelta(new Vector2(0, 0), 0.2f))
                .AppendCallback(() => { EditPictureUI.gameObject.SetActive(false); });
    }

    public void ResetView() 
    {
        FirstName_InputField.text = null;
        LastName_InputField.text = null;
        Phone_InputField.text = null;
        Description_InputField.text = null;
        Email_InputField.text = null;
        Twitter_InputField.text= null;
        AreYouSureTurnActive(false);
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
 
    public string GetCurrentlySelectedImage()
    {
        return m_CurrentlySelectedImage;
    }
}
