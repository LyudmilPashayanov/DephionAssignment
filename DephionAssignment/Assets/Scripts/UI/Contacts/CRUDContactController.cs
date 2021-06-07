using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CRUDContactController : IUIPage
{
    public CRUDControllerView m_view;
    private Contact CurrentlyEditedContact;

    public void EditExistingContact(Contact contactToEdit)
    {
        m_view.RemoveListeners();
        m_view.SetHeader("edit");
        CurrentlyEditedContact = contactToEdit;
        m_view.EditContact(CurrentlyEditedContact);
        m_view.SetListeners(SaveEditedContact, DiscardChanges,DeleteContact);
        m_view.ActivateDeleteButton(true);
    }

    public void CreateNewContact()
    {
        m_view.RemoveListeners();
        m_view.SetHeader("create");
        m_view.ActivateDeleteButton(false);
        m_view.SetListeners(AddNewContact, DiscardChanges,null);
    }

    public void EditMyProfile()
    {
        m_view.RemoveListeners();
        m_view.SetHeader("my profile");
        m_view.ActivateDeleteButton(false);
        m_view.EditContact(ContactsCatalogManager.Instance.m_MyProfile);
        m_view.SetListeners(UpdateMyProfile, DiscardChanges,null);
    }

    public void DeleteContact() 
    {
        ContactsCatalogManager.Instance.DeleteContact(CurrentlyEditedContact);
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void UpdateMyProfile() 
    {
        UpdateContact(ContactsCatalogManager.Instance.m_MyProfile);
        ContactsCatalogManager.Instance.UpdatedMyProfile();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void DiscardChanges() 
    {
        m_view.ClearAllFields();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void SaveEditedContact() 
    {
        UpdateContact(CurrentlyEditedContact);
        ContactsCatalogManager.Instance.EditedContact();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }

    public void AddNewContact()
    {
        Contact newContact = new Contact();
        UpdateContact(newContact);
        ContactsCatalogManager.Instance.CreateContact(newContact);
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
