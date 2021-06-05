using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class AllContactsView : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_SearchField;

    public void AddListeners(UnityAction<string> filterOnEndEdit) 
    {
        m_SearchField.onEndEdit.AddListener(filterOnEndEdit);
    }
}
