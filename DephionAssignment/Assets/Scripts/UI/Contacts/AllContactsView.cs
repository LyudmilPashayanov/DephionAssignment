using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class AllContactsView : MonoBehaviour
{
    [SerializeField] private TMP_InputField Search_InputField;
    [SerializeField] private TMP_InputField MyName_InputField;
    [SerializeField] private Button SortAlphabetically_Button;
    [SerializeField] private Button SortByCreationDate_Button;
    [SerializeField] private Button SearchField_Button;
    [SerializeField] private Image ClearOrSearch_Image;
    [SerializeField] private Button AddContact_Button;

    public void RemoveAllListeners() 
    {
        SortAlphabetically_Button.onClick.RemoveAllListeners();
        SortByCreationDate_Button.onClick.RemoveAllListeners();
        Search_InputField.onEndEdit.RemoveAllListeners();
        SearchField_Button.onClick.RemoveAllListeners();
        AddContact_Button.onClick.RemoveAllListeners();
        MyName_InputField.onEndEdit.RemoveAllListeners();
    }

    public void AddListeners(UnityAction<string> filterOnEndEdit, UnityAction sortAlphabetically, UnityAction sortByDate,UnityAction AddContactNavigation, UnityAction<string> OnEndAddingMyName, UnityAction<string> OnStartSearching) 
    {
        Search_InputField.onEndEdit.AddListener(filterOnEndEdit);
        Search_InputField.onSelect.AddListener(OnStartSearching);
        SortAlphabetically_Button.onClick.AddListener(sortAlphabetically);
        SortByCreationDate_Button.onClick.AddListener(sortByDate);
        SearchField_Button.onClick.AddListener(SelectSearchField);
        AddContact_Button.onClick.AddListener(AddContactNavigation);
        MyName_InputField.onEndEdit.AddListener(OnEndAddingMyName);
    }

    public void SelectSearchField()
    {
        Search_InputField.Select();
        SearchField_Button.onClick.RemoveAllListeners();
    }

    public void SetMyProfile(Contact myProfile) 
    {
        MyName_InputField.text = myProfile.FirstName;
    }

    public void ChangeButtonToClear(UnityAction clearSearchField) 
    {
        SearchField_Button.onClick.RemoveAllListeners();
        SearchField_Button.onClick.AddListener(clearSearchField);
    }

    public void SetButtonActive(bool active) 
    {
        SearchField_Button.gameObject.SetActive(active);
    }

    public void ChangeIconToMagnifierImage() 
    {
        ClearOrSearch_Image.sprite = UIManager.Instance.m_ContactsAtlas.GetSprite("magnifier");
    }

    public void ChangeIconToClearImage() 
    {
        ClearOrSearch_Image.sprite = UIManager.Instance.m_ContactsAtlas.GetSprite("x_button");
    }

    public void ChangeButtonToSearch() 
    {
        SearchField_Button.onClick.RemoveAllListeners();
        SearchField_Button.onClick.AddListener(SelectSearchField);
    }

    public bool IsSearchBarEmpty() 
    {
        if(Search_InputField.text == string.Empty) 
        {
            return true;
        }
        return false;
    }

    public void ClearSearchBar()
    {
        Search_InputField.text = string.Empty;
        Debug.Log("clearing the search bar");
    }
}
