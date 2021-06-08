using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ProfileImage : IPoolData
{
    public string PhotoName { get; }

    public Sprite PhotoSprite { get; }

    public ProfileImage(string name, Sprite sprite) 
    {
        PhotoName = name;
        PhotoSprite = sprite;
    }
}
