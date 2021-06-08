using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CRUDContactController : IUIPage
{
    [SerializeField] private CRUDControllerView m_view;
    public Contact m_CurrentlyEditedContact;
    [SerializeField] private EditPictureController EditPictureController;
    
    public void EditExistingContact(Contact contactToEdit)
    {
        m_view.RemoveListeners();
        m_view.SetHeader("edit");
        m_CurrentlyEditedContact = contactToEdit;
        m_view.EditContact(m_CurrentlyEditedContact);
        m_view.SetListeners(SaveEditedContact,new UnityAction(()=> OpenAreYouSureDialog(true)), DeleteContact, OpenEditPicture,DiscardChanges, new UnityAction(() => OpenAreYouSureDialog(false)));
        m_view.ActivateDeleteButton(true);
    }

    public void CreateNewContact()
    {
        m_view.RemoveListeners();
        m_view.SetHeader("create");
        m_view.ActivateDeleteButton(false);
        m_CurrentlyEditedContact = new Contact();
        m_view.EditContact(m_CurrentlyEditedContact);
        m_view.SetListeners(AddNewContact, new UnityAction(() => OpenAreYouSureDialog(true)), null, OpenEditPicture,DiscardChanges, new UnityAction(() => OpenAreYouSureDialog(false)));
    }

    public void EditMyProfile()
    {
        m_view.RemoveListeners();
        m_view.SetHeader("my profile");
        m_view.ActivateDeleteButton(false);
        m_CurrentlyEditedContact = ContactsCatalogManager.Instance.m_MyProfile;
        m_view.EditContact(m_CurrentlyEditedContact);
        m_view.SetListeners(UpdateMyProfile, new UnityAction(() => OpenAreYouSureDialog(true)), null, OpenEditPicture,DiscardChanges, new UnityAction(() => OpenAreYouSureDialog(false)));
    }

    public void DeleteContact() 
    {
        ContactsCatalogManager.Instance.DeleteContact(m_CurrentlyEditedContact);
        m_CurrentlyEditedContact = null;
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void UpdateMyProfile() 
    {
        UpdateContact(m_CurrentlyEditedContact);
        ContactsCatalogManager.Instance.UpdatedMyProfile();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }


    public void OpenAreYouSureDialog(bool active) 
    {
        m_view.AreYouSureTurnActive(active);   
    }

    public void DiscardChanges() 
    {
        m_CurrentlyEditedContact = null;
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void OpenEditPicture() 
    {
        EditPictureController.InitEditPicture(SetNewProfilePicture,
            ContactsCatalogManager.Instance.m_ProfileImages);
        m_view.ShowEditPictureUI(true);
    }

    public void SetNewProfilePicture(string imageName) 
    {
        m_view.SetProfilePicture(imageName);
        m_view.ShowEditPictureUI(false);
    }

    public void SaveEditedContact() 
    {
        UpdateContact(m_CurrentlyEditedContact);
        if (CheckIfContactIsNull(m_CurrentlyEditedContact))
        {
            DeleteContact();
            return;
        }
        ContactsCatalogManager.Instance.EditedContact();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void AddNewContact()
    {
        UpdateContact(m_CurrentlyEditedContact);
        if (CheckIfContactIsNull(m_CurrentlyEditedContact))
        {
            DiscardChanges();
            return;
        }

        ContactsCatalogManager.Instance.CreateContact(m_CurrentlyEditedContact);
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public bool CheckIfContactIsNull(Contact contact) 
    {
        if (string.IsNullOrEmpty(contact.Email) && string.IsNullOrEmpty(contact.Twitter) &&
            string.IsNullOrEmpty(contact.Phone) && string.IsNullOrEmpty(contact.FirstName) &&
            string.IsNullOrEmpty(contact.LastName) && string.IsNullOrEmpty(contact.Description))
            return true;

        return false;
    }

    public string GetCurrentlyDisplayedImage() 
    {
        return m_view.GetCurrentlySelectedImage();
    }

    public void UpdateContact(Contact contactToUpdate) 
    {
        contactToUpdate.FirstName = m_view.GetCurrentlyWrittenFirstName();
        contactToUpdate.LastName = m_view.GetCurrentlyWrittenLastName();
        contactToUpdate.Phone = m_view.GetCurrentlyWrittenPhoneNumber();
        contactToUpdate.Description = m_view.GetCurrentlyWrittenDescription();
        contactToUpdate.Email = m_view.GetCurrentlyWrittenEmail();
        contactToUpdate.Twitter = m_view.GetCurrentlyWrittenTwitter();
        contactToUpdate.Photo = m_view.GetCurrentlySelectedImage();
    }

    public override void OnPageLeaving()
    {
        m_view.ResetView();
    }
}
