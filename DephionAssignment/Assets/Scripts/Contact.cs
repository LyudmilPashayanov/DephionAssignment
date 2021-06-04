using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Contact 
{
    private string Id;
    public string id
    {
        get
        {
            if (this.Id == null)
            {
                this.Id = Guid.NewGuid().ToString();
                Debug.Log("########## Id : " + this.Id);
            }
            return this.Id;
        }
        set => this.Id = value;
    }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string description { get; set; }
    public string email { get; set; }
    public string twitter { get; set; }
    public string phone { get; set; }
    private long DateAddedTimestamp;
    public long dateAddedTimestamp
    {
        get
        {
            if (this.DateAddedTimestamp == 0)
            {
                this.DateAddedTimestamp = DateTime.UtcNow.Ticks;
                Debug.Log("########## dateAddedTimestamp : " + this.DateAddedTimestamp);
            }
            return this.DateAddedTimestamp;
        }
        set => this.DateAddedTimestamp = value;
    }
}
