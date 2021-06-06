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
    [SerializeField] private Button m_SearchField_button;
    [SerializeField] private Image m_ClearOrSearch_image;


    public void RemoveAllListeners() 
    {
        m_SortAlphabetically_button.onClick.RemoveAllListeners();
        m_SortByCreationDate_button.onClick.RemoveAllListeners();
        m_SearchField.onEndEdit.RemoveAllListeners();
        m_SearchField_button.onClick.RemoveAllListeners();
    }

    public void AddListeners(UnityAction<string> filterOnEndEdit, UnityAction sortAlphabetically, UnityAction sortByDate) 
    {
        m_SearchField.onEndEdit.AddListener(filterOnEndEdit);
        m_SortAlphabetically_button.onClick.AddListener(sortAlphabetically);
        m_SortByCreationDate_button.onClick.AddListener(sortByDate);
        m_SearchField_button.onClick.AddListener(SelectSearchField);
    }

    public void SelectSearchField()
    {
        m_SearchField.Select();
        m_SearchField_button.onClick.RemoveAllListeners();
    }

    public void ChangeButtonToClear(UnityAction clearSearchField) 
    {
        m_SearchField_button.onClick.RemoveAllListeners();
        m_SearchField_button.onClick.AddListener(clearSearchField);
    }

    public void SetInteractableButton(bool active) 
    {
        m_SearchField_button.interactable = active;
    }

    public void ChangeIconToMagnifierImage() 
    {
        m_ClearOrSearch_image.sprite = UIManager.Instance.m_ContactsAtlas.GetSprite("magnifier");
    }

    public void ChangeIconToClearImage() 
    {
        m_ClearOrSearch_image.sprite = UIManager.Instance.m_ContactsAtlas.GetSprite("x_button");
    }

    public void ChangeButtonToSearch() 
    {
        m_SearchField_button.onClick.RemoveAllListeners();
        m_SearchField_button.onClick.AddListener(SelectSearchField);
    }

    public void ClearSearchBar()
    {
        m_SearchField.text = string.Empty;
        Debug.Log("clearing the search bar");
    }
}
