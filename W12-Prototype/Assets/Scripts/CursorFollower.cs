using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    // public bool isHeadCursor = false;

    // public Sprite normalSprite;
    // public Sprite clickSprite;

    // public GameObject cursorPrefab;
    // public int spawnClickTimes = 10;
    // private int clickToNextSpawn = 0;

    // private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    // private bool hasChildCursor = false;
    private Vector2 lastPosition;
    public float soundTriggerDistance = 5f;

    public AudioSource swoosh;
    // private HingeJoint2D connectedJoint;

    // Start is called before the first frame update
    void Start()
    {
        // _spriteRenderer = GetComponent<SpriteRenderer>();
        // _spriteRenderer.sprite = normalSprite;
        //
        _rigidbody2D = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        //
        // // set joint
        // if (!isHeadCursor)
        // {
        //     // gameObject.AddComponent<HingeJoint2D>();
        //     if (cursorPrefab == null) cursorPrefab = gameObject;
        // }
        //
        // clickToNextSpawn = spawnClickTimes;
    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
        // Click();
    }

    private void FollowCursor()
    {
        // if (!isHeadCursor) return;
        Vector3 mousePos = Input.mousePosition; // 获取鼠标位置
        mousePos.z = Camera.main.nearClipPlane; // 设置一个Z坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos); // 将鼠标位置转换为世界坐标

        // print(Vector2.Distance(worldPosition, lastPosition));
        if (Vector2.Distance(worldPosition, lastPosition) >= soundTriggerDistance)
        {
            if (!swoosh.isPlaying) swoosh.Play();
        }
        // 更新物体的位置
        _rigidbody2D.MovePosition(worldPosition);

        lastPosition = worldPosition;
        // transform.position = worldPosition;
    }

    // private void Click()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if (isHeadCursor) CheckMouseClick();
    //
    //         _spriteRenderer.sprite = clickSprite;
    //
    //         // click sound
    //
    //         // click effect
    //
    //     }
    //
    //     // if (Input.GetMouseButton(0))
    //     // {
    //     //     CheckMouseClick();
    //     // }
    //
    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         clickToNextSpawn--;
    //         if (clickToNextSpawn == 0) SpawnCursor();
    //
    //         if (connectedJoint != null) ReleaseObject();
    //
    //         _spriteRenderer.sprite = normalSprite;
    //     }
    // }


    // private void CheckMouseClick()
    // {
    //     // 将鼠标屏幕位置转换为世界位置
    //     Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //
    //     // 发射一个Raycast
    //     RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
    //
    //     Collider2D col = hit.collider;
    //
    //     // 检查Raycast是否击中了Sprite
    //     if (col != null)
    //     {
    //         if (col.tag.Equals("Draggable"))
    //         {
    //             // connect
    //             Debug.Log("Sprite clicked: " + col);
    //             ConnectObject(col, hit.collider.transform.position);
    //             // DragObject(col.transform.position);
    //         }
    //     }
    // }

    // private void ConnectObject(Collider2D col, Vector2 hitPoint)
    // {
    //     // col.GetComponent<CursorFollower>().has
    //     connectedJoint = col.GetComponent<HingeJoint2D>();
    //     connectedJoint.connectedBody = _rigidbody2D;
    //     connectedJoint.anchor = new Vector2(0.5f, 0);
    //     connectedJoint.enabled = true;
    // }

    // private void ReleaseObject()
    // {
    //     connectedJoint.connectedBody = null;
    //     connectedJoint.enabled = false;
    //     connectedJoint = null;
    // }

    // private void SpawnCursor()
    // {
    //     // GameObject cursor = gameObject;
    //     // cursor.GetComponent<CursorFollower>().isHeadCursor = false;
    //     transform.localScale.Scale(new Vector3(2, 2, 0));
    //
    //     // Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
    //     // Instantiate(cursorPrefab, spawnPosition, Quaternion.identity);
    //     clickToNextSpawn = spawnClickTimes;
    // }
}
