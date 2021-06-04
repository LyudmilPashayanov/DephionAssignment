using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactsManager : MonoBehaviour
{
    public List<Contact> myContacts = new List<Contact>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddContact();
        }
    }
    public void AddContact() 
    {
        Contact newContact1 = new Contact()
        {
            firstName = "ivan",
            lastName = "petrov",
            description = "tapakov",
            email = "asd",
            phone = "_12341151"
        };
        myContacts.Add(newContact1);
        PlayfabManager.Instance.SaveNewContacts(myContacts);
    }
}
