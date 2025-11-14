using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController cC;
    [SerializeField] float normalSpeed = 5f;
    [SerializeField] float gravity = 50f;
    [SerializeField] float jumpForce = 40f;
    [SerializeField] float mouseSense = 1f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float currentSpeed;
    [SerializeField] float stamina = 100f;
    [SerializeField] TextMeshProUGUI tMP1;
    [SerializeField] TextMeshProUGUI tMP2;
    bool running = false;

    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Running();
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        if (cC.isGrounded)
        {
            direction = new Vector3(mH, 0, mV);
            direction = transform.TransformDirection(direction) * currentSpeed;
            if (Input.GetKey(KeyCode.Space))
            {
                direction.y = jumpForce;
            }
        }
        direction.y -= gravity * Time.deltaTime;
        cC.Move(direction * Time.deltaTime);

        Cursor.lockState = CursorLockMode.Locked;
        float rX = Input.GetAxis("Mouse X") * mouseSense;
        float rY = Input.GetAxis("Mouse Y") * mouseSense;
        Vector3 rP = transform.rotation.eulerAngles;
        rP.x -= rY;
        rP.z = 0;
        rP.y += rX;
        transform.rotation = Quaternion.Euler(rP);

        if (stamina < 0)
        {
            running = false;
            stamina = 0;
        }

        if (stamina > 100)
        {
            stamina = 100;
        }
        if (currentSpeed < normalSpeed)
        {
            currentSpeed = normalSpeed;
        }
        else if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        if (running == true)
        {
            currentSpeed += normalSpeed * Time.deltaTime;
            stamina -= normalSpeed * Time.deltaTime * 5;
        }
        else if (running == false)
        {
            currentSpeed -= normalSpeed * Time.deltaTime;
            stamina += normalSpeed * Time.deltaTime;
        }

        tMP1.text = stamina.ToString();
        tMP2.text = currentSpeed.ToString();

    }

    void Running()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && cC.isGrounded)
        {
            running = !running;
        }
    }
}