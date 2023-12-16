using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartZone : MonoBehaviour
{
    private bool isInsideTrigger = false;

    public GameObject level;

    public float stayTime = 1f;
    private float timer = 0f;

    private SpriteRenderer _spriteRenderer;
    Color startColor;
    Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = _spriteRenderer.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        timer = stayTime;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 检查是否是特定的物体（例如标记为"Player"的物体）
        {
            isInsideTrigger = true;
            timer = stayTime; // 重置计时器
        }
    }

    private void CountDown()
    {
        if (level.activeSelf) return;
        if (isInsideTrigger)
        {
            timer -= Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(endColor, startColor, timer / stayTime);
            // print(timer);
            if (timer <= 0)
            {
                GenerateLevel();
                isInsideTrigger = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!level.activeSelf) _spriteRenderer.color = startColor;
            isInsideTrigger = false;
            timer = stayTime;
        }
    }

    private void GenerateLevel()
    {
        // print("level Start");
        level.SetActive(true);
    }
}
