using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    private Coroutine _coroutine;
    private float _eventFeedTimer = 20f;

    public Text killer;
    public Image imageWeapon;
    public Text victim;
    public Sprite[] sprites;

    public void UpdateEventFeed(string tempKiller, int spriteID, string tempVictim)
    {
        killer.text = tempKiller;
        imageWeapon.sprite = sprites[spriteID];
        killer.text = tempVictim;
        StartCoroutine(EnableInfo());
    }

    private IEnumerator EnableInfo()
    {
        yield return new WaitForSeconds(_eventFeedTimer);
        Destroy(this.gameObject);
    }
}
