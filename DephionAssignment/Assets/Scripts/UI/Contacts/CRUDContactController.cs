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
        m_view.SetListeners(SaveEditedContact, DiscardChanges,DeleteContact, OpenEditPicture);
        m_view.ActivateDeleteButton(true);
    }

    public void CreateNewContact()
    {
        m_view.RemoveListeners();
        m_view.SetHeader("create");
        m_view.ActivateDeleteButton(false);
        m_CurrentlyEditedContact = new Contact();
        m_view.EditContact(m_CurrentlyEditedContact);
        m_view.SetListeners(AddNewContact, DiscardChanges,null, OpenEditPicture);
    }

    public void EditMyProfile()
    {
        m_view.RemoveListeners();
        m_view.SetHeader("my profile");
        m_view.ActivateDeleteButton(false);
        m_CurrentlyEditedContact = ContactsCatalogManager.Instance.m_MyProfile;
        m_view.EditContact(m_CurrentlyEditedContact);
        m_view.SetListeners(UpdateMyProfile, DiscardChanges,null, OpenEditPicture);
    }

    public void DeleteContact() 
    {
        ContactsCatalogManager.Instance.DeleteContact(m_CurrentlyEditedContact);
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void UpdateMyProfile() 
    {
        UpdateContact(m_CurrentlyEditedContact);
        ContactsCatalogManager.Instance.UpdatedMyProfile();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void DiscardChanges() 
    {
        m_view.ClearAllFields();
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
        m_CurrentlyEditedContact.Photo = imageName;
        m_view.SetNewProfilePicture(imageName);
        m_view.ShowEditPictureUI(false);
    }

    public void SaveEditedContact() 
    {
        UpdateContact(m_CurrentlyEditedContact);
        ContactsCatalogManager.Instance.EditedContact();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void AddNewContact()
    {
        UpdateContact(m_CurrentlyEditedContact);
        ContactsCatalogManager.Instance.CreateContact(m_CurrentlyEditedContact);
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void UpdateContact(Contact contactToUpdate) 
    {
        contactToUpdate.FirstName = m_view.GetCurrentlyWrittenFirstName();
        contactToUpdate.LastName = m_view.GetCurrentlyWrittenLastName();
        contactToUpdate.Phone = m_view.GetCurrentlyWrittenPhoneNumber();
        contactToUpdate.Description = m_view.GetCurrentlyWrittenDescription();
        contactToUpdate.Email = m_view.GetCurrentlyWrittenEmail();
        contactToUpdate.Twitter = m_view.GetCurrentlyWrittenTwitter();
    }

    public override void OnPageLeaving()
    {
        m_view.ClearAllFields();
    }
}
