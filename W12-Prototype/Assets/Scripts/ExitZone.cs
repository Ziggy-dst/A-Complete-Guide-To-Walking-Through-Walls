using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public AudioSource completeSound;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") & !GameManager.Instance.isPlayerDead)
        {
            completeSound.Play();
            GameManager.Instance.ProceedToNextLevel();
        }
    }
}
