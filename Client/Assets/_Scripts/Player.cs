using SharedLibrary;
using UnityEngine;


public class Player : MonoBehaviour
{
    private bool isPaused = true;

    float rotationX = 0;
    public float lookSpeed = 2f;
    public float lookXLimit = 49f;
    Vector3 moveDirection = Vector3.zero;


    public float speed = 5f;
    public float jumpForce = 5f;

    private CharacterController cc;
    private GameObject cam;
    private PlayerInfo playerInfo;

    public void Init(int playerId)
    {
        cam = transform.GetChild(0).gameObject;
        cc = GetComponent<CharacterController>();
        playerInfo = new PlayerInfo
        {
            Id = playerId,
            Health = 100
        };
        isPaused = false;
    }

    void Update()
    {
        if (isPaused) return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float speedX = Input.GetAxis("Horizontal") * speed;
        float speedY = Input.GetAxis("Vertical") * speed;

        float movementDirectionY = moveDirection.y;
        moveDirection = forward * speedY + right * speedX;

        if (Input.GetButton("Jump")  && cc.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!cc.isGrounded)
        {
            moveDirection.y -= 10 * Time.deltaTime;
        }

        cc.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);


        if(transform.position.y < -10f)
        {
            transform.position = new Vector3(0, 1, 0);
        }
    }

    public void Pause(bool pause)
    {
        isPaused = pause;
    }

    public PlayerInfo GetInfo()
    {
        Vector3 pos = transform.position;
        Vector3 vel = cc.velocity;

        playerInfo.Position = new float[] { pos.x, pos.y, pos.z };
        playerInfo.Velocity = new float[] { vel.x, vel.y, vel.z };
        playerInfo.Rotation = new float[] { cam.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0 };
        return playerInfo;
    }

}
