using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public AudioSource completeSound;

    public float stayTime = 0.5f;
    private float timer = 0f;

    private bool isInsideTrigger = false;

    private SpriteRenderer _spriteRenderer;
    Color startColor;
    Color endColor;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = _spriteRenderer.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    private void Update()
    {
        CountDown();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isInsideTrigger = true;
            timer = stayTime; // 重置计时器
        }
    }

    private void CountDown()
    {
        if (isInsideTrigger)
        {
            timer -= Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(endColor, startColor, timer / stayTime);
            // print(timer);
            if (timer <= 0)
            {
                if (GameManager.Instance.isPlayerDead) return;
                Proceed();
                isInsideTrigger = false;
            }
        }
    }

    private void Proceed()
    {
        completeSound.Play();
        GameManager.Instance.ProceedToNextLevel();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            _spriteRenderer.color = startColor;
            isInsideTrigger = false;
            timer = stayTime;
        }
    }
}
