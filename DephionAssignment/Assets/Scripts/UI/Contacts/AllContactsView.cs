using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class AllContactsView : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_SearchField;
    [SerializeField] private Button m_SortAlphabetically_button;
    [SerializeField] private Button m_SortByCreationDate_button;


    public void RemoveAllListeners() 
    {
        m_SortAlphabetically_button.onClick.RemoveAllListeners();
        m_SortByCreationDate_button.onClick.RemoveAllListeners();
        m_SearchField.onEndEdit.RemoveAllListeners();

    }
    public void AddListeners(UnityAction<string> filterOnEndEdit, UnityAction sortAlphabetically, UnityAction sortByDate) 
    {
        m_SearchField.onEndEdit.AddListener(filterOnEndEdit);
        m_SortAlphabetically_button.onClick.AddListener(sortAlphabetically);
        m_SortByCreationDate_button.onClick.AddListener(sortByDate);

    }
}
