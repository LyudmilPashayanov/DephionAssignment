using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRUDContactController : IUIPage
{
    public CRUDControllerView m_view;

    public void AddContact()
    {
        Contact newContact1 = new Contact()
        {
            FirstName = "ivan",
            LastName = "petrov",
            Description = "tapakov",
            Email = "asd",
            Phone = "_12341151"
        };
        //myContacts.Add(newContact1);
        //PlayfabManager.Instance.SaveNewContacts(myContacts);
    }

}
