using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class PlayerClass
{
    [SerializeField]
    CharacterController Controller;
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    RectTransform energyImageTransform;
    [SerializeField]
    Text energyText;
    private float hit_point;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

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
            if (value >= 0 && value <= maxEnergy + 0.2f)
            {
                curEnergy = value;
            }
        }
    }

    public PlayerClass(Transform mytransform, Canvas eCanvas,float hp,float ms, float p_energy, float rs)
    {
        myTransform = mytransform;
        Controller = mytransform.GetComponent<CharacterController>();
        cameraTransform = mytransform.GetChild(0).GetComponent<Transform>();
        energyImageTransform = eCanvas.transform.GetChild(0).GetComponent<RectTransform>();
        energyText = eCanvas.transform.GetChild(1).GetComponent<Text>();
        hit_point = hp;
        walkSpeed = ms;
        runSpeed = ms * 2f;
        rotationSpeed = rs;

        maxEnergy = p_energy;
        curEnergy = p_energy;
        energyRecovery = 10;
        energyReduction = 20;
    }

    public void Move()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), -5, Input.GetAxis("Vertical"));
        moveDirection = myTransform.TransformDirection(moveDirection);
        myTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));
        cameraTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotationSpeed, 0, 0));
        cameraTransform.eulerAngles = new Vector3(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, 0);
        EnegryUpdate();
        moveDirection.x *= moveSpeed;
        moveDirection.z *= moveSpeed;
        Controller.Move(moveDirection * Time.deltaTime);
    }

    void EnegryUpdate()
    {
        energyText.text = "" + ((int)curEnergy * Time.deltaTime)/Time.deltaTime;
        energyImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, curEnergy * 2);

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
    }
}
