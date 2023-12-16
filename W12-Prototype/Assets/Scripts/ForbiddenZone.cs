using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForbiddenZone : MonoBehaviour
{
    public AudioSource deathSound;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            GameManager.Instance.isPlayerDead = true;
            deathSound.Play();
            GameManager.Instance.Restart();
            // hide current level
            // transform.parent.gameObject.SetActive(false);
        }
    }
}
