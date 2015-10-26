using UnityEngine;
using System.Collections;

public class WallMove : MonoBehaviour
{
    public float CurDistance = 0;
    bool moveForward = true;
    Vector3 vectorDirection = Vector3.zero;

    public enum moveDirection {forward, back, Up, Down, left, right }
    public moveDirection MoveDirection;
    public bool moveBack;
    public float NeedFullDistance;
    public float DelayOfMove;
    public float Speed;
    float time = 0;

    void Start()
    {
        
    }

    void Update()
    {
        MoveWall(NeedFullDistance, Speed, MoveDirection, moveBack , DelayOfMove);
    }
    void MoveWall(float pDistance, float pSpeed,moveDirection pMoveDirection, bool pMoveBack, float pDelayOfMove)
    {
        switch (pMoveDirection)
        {
            case moveDirection.forward: vectorDirection = Vector3.forward; break;
            case moveDirection.back: vectorDirection = Vector3.back; break;
            case moveDirection.Up: vectorDirection = Vector3.up; break;
            case moveDirection.Down: vectorDirection = Vector3.down; break;
            case moveDirection.left: vectorDirection = Vector3.left; break;
            case moveDirection.right: vectorDirection = Vector3.right; break;
        }
        if (CurDistance <= pDistance && moveForward)
        {
            time += Time.deltaTime;
            time = Mathf.Clamp(time, 0, pDelayOfMove);
            if (time >= pDelayOfMove)
            {
                transform.Translate(vectorDirection * Time.deltaTime * Speed);
                CurDistance += Time.deltaTime * Speed;

                if (CurDistance >= pDistance)
                {
                    moveForward = false;
                    time = 0;
                }
            }
        }
        else
        {
            if (pMoveBack)
            {
                time += Time.deltaTime;
                time = Mathf.Clamp(time, 0, pDelayOfMove);
                if (time >= pDelayOfMove)
                {
                    transform.Translate((vectorDirection * Time.deltaTime) * -Speed);
                    CurDistance -= Time.deltaTime * Speed;

                    if (CurDistance <= 0)
                    {
                        moveForward = true;
                        time = 0;
                    }
                }
            }
        }
    }
}
