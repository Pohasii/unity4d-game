using UnityEngine;
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

    private float cameraRotation;
    public float minimumY = - 65;
    public float maximumY = 65;
    private NetworkView networkView1;

    public PlayerClass(Transform mytransform, float hp,float ms, float p_energy, float rs)
    {
        myTransform = mytransform;
        networkView1 = mytransform.GetComponent<NetworkView>();
        RigidBody = mytransform.GetComponent<Rigidbody>();
        cameraTransform = mytransform.GetChild(0).GetComponent<Transform>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        energyImageTransform = canvas.transform.GetChild(0).GetComponent<RectTransform>();
        energyText = canvas.transform.GetChild(1).GetComponent<Text>();
        cameraTransform.GetComponent<Camera>().enabled = networkView1.isMine;
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
        Cursor.visible = false;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), RigidBody.velocity.y, Input.GetAxis("Vertical"));
        moveDirection = myTransform.TransformDirection(moveDirection);
        myTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));

        cameraRotation += Input.GetAxis("Mouse Y") * rotationSpeed;
        cameraRotation = Mathf.Clamp(cameraRotation, minimumY, maximumY);

        cameraTransform.localEulerAngles = new Vector3(-cameraRotation, cameraTransform.localEulerAngles.y, 0);

        moveDirection.x *= moveSpeed;
        moveDirection.z *= moveSpeed;

        if (networkView1.isMine)
        RigidBody.velocity = moveDirection;
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RigidBody.AddForce(Vector3.up * 200);
        }
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
