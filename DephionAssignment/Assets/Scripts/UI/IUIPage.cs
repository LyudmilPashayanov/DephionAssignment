using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class IUIPage : MonoBehaviour 
{
    public Vector2 GetPageLocation() 
    {
        return gameObject.transform.localPosition;
    }

    public abstract void OnPageLeaving();
}
