using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Marks a class as a UI Page, to which the user can navigate.
/// </summary>
public abstract class IUIPage : MonoBehaviour 
{
    public Vector2 GetPageLocation() 
    {
        return gameObject.transform.localPosition;
    }
    /// <summary>
    /// Called every time you are going away from a page. Used to resets the page you are going away from (if needed).
    /// </summary>
    public abstract void OnPageLeaving();
}
