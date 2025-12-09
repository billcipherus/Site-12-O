using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float normalSpeed = 5f;
    [SerializeField] float gravity = 50f;
    [SerializeField] float jumpForce = 40f;
    [SerializeField] float mouseSense = 1f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float currentSpeed;
    [SerializeField] float stamina = 100f;
    [SerializeField] TextMeshProUGUI tMP1;
    [SerializeField] TextMeshProUGUI tMP2;
    [SerializeField] Collider groundCheckCollider; // Новое поле для триггера-детектора

    bool running = false;
    bool isGrounded = false;
    private Vector3 moveDirection;

    void Start()
    {
        currentSpeed = normalSpeed;
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false; // Наша гравитация
    }

    void Update()
    {
        Running();

        // Получаем ввод (как в оригинальном коде)
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");

        // Рассчитываем направление движения (как в оригинальном коде)
        moveDirection = new Vector3(mH, 0, mV);
        moveDirection = transform.TransformDirection(moveDirection);

        // Прыжок
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Поворот камеры (твой оригинальный код)
        Cursor.lockState = CursorLockMode.Locked;
        float rX = Input.GetAxis("Mouse X") * mouseSense;
        float rY = Input.GetAxis("Mouse Y") * mouseSense;
        Vector3 rP = transform.rotation.eulerAngles;
        rP.x -= rY;
        rP.z = 0;
        rP.y += rX;
        transform.rotation = Quaternion.Euler(rP);

        // Логика стамины и скорости (твой оригинальный код)
        if (stamina < 0)
        {
            running = false;
            stamina = 0;
        }

        if (stamina > 100) stamina = 100;

        if (currentSpeed < normalSpeed) currentSpeed = normalSpeed;
        else if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;

        if (running)
        {
            currentSpeed += normalSpeed * Time.deltaTime;
            stamina -= normalSpeed * Time.deltaTime * 5;
        }
        else
        {
            currentSpeed -= normalSpeed * Time.deltaTime;
            stamina += normalSpeed * Time.deltaTime;
        }

        tMP1.text = stamina.ToString("F0");
        tMP2.text = currentSpeed.ToString("F1");
    }

    void FixedUpdate()
    {
        // ПРИМЕНЯЕМ ДВИЖЕНИЕ ТОЛЬКО НА ЗЕМЛЕ
        if (isGrounded)
        {
            Vector3 horizontalMove = moveDirection * currentSpeed;
            // Сохраняем вертикальную скорость для прыжка/падения
            rb.linearVelocity = new Vector3(horizontalMove.x, rb.linearVelocity.y, horizontalMove.z);
        }

        // ВСЕГДА применяем гравитацию
        rb.AddForce(Vector3.down * gravity * rb.mass);
    }

    void Running()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            running = !running;
        }
    }

    // Триггер для определения земли
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}