﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class PlayerClass
{
    [SerializeField]
    Rigidbody RigidBody;
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    RectTransform energyImageTransform;
    [SerializeField]
    Text energyText;
    Canvas canvas;
    private Animator vAimator;

    private float hit_point;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float JumpForce;

    private float rotationSpeed;
    private Vector3 moveDirection;
    private float distToGround;

    private float maxEnergy;
    private float curEnergy;
    private float energyRecovery;
    private float energyReduction;
    public float CurEnergy1
    {
        get { return curEnergy; }
        set
        {
            if (value >= 0 && value <= maxEnergy + 0.2f)
            {
                curEnergy = value;
            }
        }
    }

    private float cameraRotation;
    public float minimumY = - 65;
    public float maximumY = 65;

    private NetworkView networkView1;

    bool jumping, run;

    public bool run2
    {
        get { return run; }
        set { run = value; }
    }

    public bool Jumping
    {
        get { return jumping; }
        set { jumping = value; }
    }

    float x, z;

    public float Z
    {
        get { return z; }
        set { z = value; }
    }

    public float X
    {
        get { return x; }
        set { x = value; }
    }

    public PlayerClass(Transform mytransform, float hp,float ms, float jumpforce,float p_energy, float rs)
    {
        myTransform = mytransform;
        vAimator = mytransform.GetComponent<Animator>();
        networkView1 = mytransform.GetComponent<NetworkView>();
        RigidBody = mytransform.GetComponent<Rigidbody>();
        cameraTransform = mytransform.GetChild(0).GetComponent<Transform>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        energyImageTransform = canvas.transform.GetChild(0).GetComponent<RectTransform>();
        energyText = canvas.transform.GetChild(1).GetComponent<Text>();
        cameraTransform.GetComponent<Camera>().enabled = networkView1.isMine;

        distToGround = myTransform.GetComponent<Collider>().bounds.extents.y;

        hit_point = hp;
        walkSpeed = ms;
        runSpeed = ms * 2f;
        rotationSpeed = rs;
        JumpForce = jumpforce;

        maxEnergy = p_energy;
        curEnergy = p_energy;
        energyRecovery = 10;
        energyReduction = 20;
    }

    public void Move()
    {
        Cursor.visible = false;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        moveDirection = new Vector3(x, RigidBody.velocity.y, z);
        moveDirection = myTransform.TransformDirection(moveDirection);
        myTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));

        cameraRotation += Input.GetAxis("Mouse Y") * rotationSpeed;
        cameraRotation = Mathf.Clamp(cameraRotation, minimumY, maximumY);

        cameraTransform.localEulerAngles = new Vector3(-cameraRotation, cameraTransform.localEulerAngles.y, 0);

        moveDirection.x *= moveSpeed;
        moveDirection.z *= moveSpeed;

        //if (networkView1.isMine)
        RigidBody.velocity = moveDirection;
    }

    public void Jump()
    {
        run2 = Input.GetKey(KeyCode.LeftShift);
        jumping = Physics.Raycast(myTransform.position, Vector3.down, distToGround - 0.9f);
        if(jumping && Input.GetKeyDown(KeyCode.Space))
        {
            RigidBody.AddForce(Vector3.up * JumpForce);
        }
    }

    public void Animation()
    {
        vAimator.SetFloat("Speed", Mathf.Abs(X));
        vAimator.SetFloat("Speed", Mathf.Abs(Z));
        vAimator.SetFloat("Energy", CurEnergy1);
        vAimator.SetBool("Jumping", !Jumping);
        vAimator.SetBool("Run", run2);
    }

    public void EnegryUpdate()
    {
        energyText.text = "" + (int)((((curEnergy * Time.deltaTime)/Time.deltaTime) / maxEnergy) * 100);
        energyImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (curEnergy / maxEnergy) * 100 * 2);

        if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f))
        {
            if (CurEnergy1 > 1f)
            {
                moveSpeed = runSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }
            CurEnergy1 -= energyReduction * Time.deltaTime;
        }
        else
        {
            moveSpeed = walkSpeed;
            CurEnergy1 += energyRecovery * Time.deltaTime;
        }
        if(moveDirection.x == 0 && moveDirection.z == 0)
        {
            CurEnergy1 += energyRecovery * 1.5f * Time.deltaTime;
        }
    }
}
