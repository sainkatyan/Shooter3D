using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFeed : MonoBehaviour
{
    public EventPanel eventPanel;
    public static EventFeed instance;
    private void Awake()
    {
        instance = this;
    }

    public void CreatePanel(string attacker, int idSprite, string victim)
    {
        var objPanel = Instantiate(eventPanel, this.transform);
        objPanel.UpdateEventFeed(attacker, idSprite, victim);
    }
}
