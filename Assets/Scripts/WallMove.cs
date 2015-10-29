using UnityEngine;
using System.Collections;
using System;

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
    Transform myTransform;
    Vector3 sign;
    
    void Start()
    {
        enabled = Network.isServer;
        myTransform = transform;
    }

    void Update()
    {
        MoveWall(NeedFullDistance, Speed, MoveDirection, moveBack , DelayOfMove);
    }
    void MoveWall(float pDistance, float pSpeed,moveDirection pMoveDirection, bool pMoveBack, float pDelayOfMove)
    {
        float tim_e = (float)Math.Round(Time.deltaTime, 2);
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
            time += tim_e;
            time = Mathf.Clamp(time, 0, pDelayOfMove);
            if (time >= pDelayOfMove)
            {
                myTransform.Translate(vectorDirection * tim_e * Speed);
                CurDistance += tim_e * Speed;

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
                time += tim_e;
                time = Mathf.Clamp(time, 0, pDelayOfMove);
                if (time >= pDelayOfMove)
                {
                    myTransform.Translate((vectorDirection * tim_e) * -Speed);
                    CurDistance -= tim_e * Speed;

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
