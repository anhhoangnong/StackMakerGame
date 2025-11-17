using StackMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection
{
    None = 0,
    Forward = 1,
    Back = 2,
    Left = 3,
    Right = 4
}

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 20;
    [SerializeField] private Vector2 startTouch, endTouch;
    [SerializeField] private Transform tfPlayerBrickPrefab;
    [SerializeField] private Transform tfPlayerAsset;
    [SerializeField] private Transform tfBrickHolder;


    private List<Transform> playerBricks = new List<Transform>();
    private bool isMoving;
    private bool isPlay;

    public LayerMask layerBrick;
    private Vector3 moveNextPoint;

    public float Speed { get => speed; set => speed = value; }
    

    

    public void OnInit()
    {
        isMoving = false;

        ClearBrick();
        tfPlayerAsset.localPosition = Vector3.zero;

    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.isState(GameState.Play) && !isMoving)
        {
            if (Input.GetMouseButtonDown(0) && !isPlay)
            {
                isPlay = true;
                startTouch = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0) && isPlay)
            {
                isPlay = false;
                endTouch = Input.mousePosition;
                EDirection direction = GetDirect(startTouch, endTouch);

                if (direction != EDirection.None)
                {
                    Vector3 point = GetNextPoint(direction);
                    if (point != Vector3.zero) // tìm thấy gạch, tránh reset điểm di chuyển về (0,0,0)
                    {
                        moveNextPoint = point;
                        isMoving = true;
                    }
                }
                //Debug.Log(GetNextPoint(GetDirect(startTouch,endTouch)));
            }
        }
        else if(isMoving)
        {

            if (Vector3.Distance(transform.position, moveNextPoint) < 0.05f)
            {
               
                isMoving = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, moveNextPoint, Time.deltaTime * speed);

        }
    }

    //xử lý di chuyển
    private EDirection GetDirect(Vector3 startTouch, Vector3 endTouch)
    {
        EDirection direction = EDirection.None;
        float deltaX = endTouch.x - startTouch.x;
        float deltaY = endTouch.y - startTouch.y;

        if (Vector3.Distance(startTouch, endTouch) < 100)
        {
            direction = EDirection.None;

        }
        else
        {
            if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                if (deltaY > 0)
                {
                    direction = EDirection.Forward;
                }
                else
                {
                    direction = EDirection.Back;
                }
            }
            else
            {
                if (deltaX > 0)
                {
                    direction = EDirection.Right;
                }
                else
                {
                    direction = EDirection.Left;
                }
            }
        }
        return direction;
    }

    private Vector3 GetNextPoint(EDirection direct)
    {
        RaycastHit hit;
        Vector3 nextPoint = Vector3.zero;
        Vector3 dir = Vector3.zero;

        switch (direct)
        {
            case EDirection.Forward: dir = Vector3.forward; break;    
            case EDirection.Back: dir = Vector3.back; break;   
            case EDirection.Left: dir = Vector3.left; break;   
            case EDirection.Right: dir = Vector3.right; break;   
            case EDirection.None: break;
            default: break;
        }



        for (int i = 1; i < 100; i++) // check tối đa viên gạch
        {
            if (Physics.Raycast(transform.position + dir * i + Vector3.up * 2, Vector3.down, out hit, 10f, layerBrick))
            {
                //nextPoint = hit.collider.transform.position +Vector3.up*0.3f;
                nextPoint = hit.collider.bounds.center + Vector3.up * hit.collider.bounds.extents.y;

            }
            else
            {
                //Debug.Log("No more brick in direction: " + direct);
                break;
            }
        }

        return nextPoint;
    }



    //bắt va chạm với gạch trên platform
    public void AddBrick()
    {
        int index = playerBricks.Count;

        Transform playerBrick = Instantiate(tfPlayerBrickPrefab, tfBrickHolder);
        playerBrick.localPosition = Vector3.down * 0.25f  + index * 0.25f * Vector3.up;
        playerBricks.Add(playerBrick);
        tfPlayerAsset.localPosition = tfPlayerAsset.localPosition + Vector3.up * 0.25f;

        
    }

    public void RemoveBrick()
    {
        int index = playerBricks.Count - 1;
        if (index >= 0)
        {
            Transform playerBrick = playerBricks[index]; 
            playerBricks.Remove(playerBrick);
            Destroy(playerBrick.gameObject);
            tfPlayerAsset.localPosition = tfPlayerAsset.localPosition - Vector3.up * 0.25f; // giảm chiều cao nhân vật
        }    

    }

    private void ClearBrick()
    {
        for (int i = 0; i <playerBricks.Count; i++)
        {
            Destroy(playerBricks[i].gameObject);
        }    

        playerBricks.Clear();
    }    

   
}

