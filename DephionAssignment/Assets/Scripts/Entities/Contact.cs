using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Contact : IPoolData
{
    private string id;
    public string Id
    {
        get
        {
            if (this.id == null)
            {
                this.id = Guid.NewGuid().ToString();
                Debug.Log("########## Id : " + this.id);
            }
            return this.id;
        }
        set => this.id = value;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Twitter { get; set; }
    public string Phone { get; set; }
    public string Photo { get; set; }
    private long dateAddedTimestamp;
    public long DateAddedTimestamp
    {
        get
        {
            if (this.dateAddedTimestamp == 0)
            {
                this.dateAddedTimestamp = DateTime.UtcNow.Ticks;
                Debug.Log("########## dateAddedTimestamp : " + this.dateAddedTimestamp);
            }
            return this.dateAddedTimestamp;
        }
        set => this.dateAddedTimestamp = value;
    }
}
