using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CRUDContactController : IUIPage
{
    public CRUDControllerView m_view;
    private Contact CurrentlyEditedContact;

    public void InitContactEditor(Contact ContactToEdit = null)
    {
        m_view.RemoveListeners();
        if (ContactToEdit != null) 
        {
            m_view.SetHeader("edit");
            CurrentlyEditedContact = ContactToEdit;
            m_view.EditContact(CurrentlyEditedContact);
            m_view.SetListeners(SaveEditedContact, DiscardChanges);

        }
        else 
        {
            m_view.SetHeader("create");
            m_view.SetListeners(AddNewContact, DiscardChanges);
        }
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
        //m_view.ClearAllFields();
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

    public override void OnPageLeft()
    {
        m_view.ClearAllFields();
    }
}
