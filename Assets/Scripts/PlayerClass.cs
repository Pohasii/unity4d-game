using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class PlayerClass
{
    CharacterController Controller;
    Transform myTransform;
    Transform cameraTransform;
    RectTransform energyImageTransform;
    private float hit_point;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    enum MoveState { Stay, Walk, Run}
    MoveState moveState;

    private float rotationSpeed;
    private Vector3 moveDirection;

    private float maxEnergy;
    private float curEnergy;
    private float energyRecovery;
    private float energyReduction;
    public float CurEnergy1
    {
        get { return curEnergy; }
        set
        {
            if (value >= 0 && value <= maxEnergy)
            {
                curEnergy = value;
            }
        }
    }

    public PlayerClass(Transform mytransform, Image eImage,float hp,float ms, float p_energy, float rs)
    {
        myTransform = mytransform;
        Controller = mytransform.GetComponent<CharacterController>();
        cameraTransform = mytransform.GetChild(0).GetComponent<Transform>();
        energyImageTransform = eImage.GetComponent<RectTransform>();

        hit_point = hp;
        walkSpeed = ms;
        runSpeed = ms * 2f;
        rotationSpeed = rs;

        maxEnergy = p_energy;
        curEnergy = p_energy;
        energyRecovery = 1;
        energyReduction = 2;
    }

    public void Move()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), -5, Input.GetAxis("Vertical"));
        moveDirection = myTransform.TransformDirection(moveDirection);
        myTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));
        cameraTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotationSpeed, 0, 0));
        cameraTransform.eulerAngles = new Vector3(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(moveDirection.x) > 0 || Mathf.Abs(moveDirection.z) > 0))
        {
            moveSpeed = runSpeed;
            moveState = MoveState.Run;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(moveDirection.x) > 0 || Mathf.Abs(moveDirection.z) > 0))
        {
            moveSpeed = walkSpeed;
            CurEnergy1 += energyRecovery * Time.deltaTime;
            moveState = MoveState.Walk;
        }
        if (Mathf.Abs(moveDirection.x) == 0 || Mathf.Abs(moveDirection.z) == 0)
        {
            CurEnergy1 += energyRecovery * Time.deltaTime;
            moveState = MoveState.Stay;
        }
        switch (moveState)
        {
            case MoveState.Stay: CurEnergy1 += (energyRecovery + 5) * Time.deltaTime; break;
            case MoveState.Walk: CurEnergy1 += (energyRecovery) * Time.deltaTime; break;
            case MoveState.Run: CurEnergy1 -= energyReduction * Time.deltaTime; break;
        }

        energyImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, curEnergy * 20);
        Controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
