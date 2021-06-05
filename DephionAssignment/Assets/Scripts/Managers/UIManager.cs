using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public SpriteAtlas m_ContactsAtlas;

    public ContactsUIController m_ContactsUIController;

    void Awake()
    {
        instance = this;
    }
}
