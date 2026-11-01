using UnityEngine;

public class Bob : MonoBehaviour
{
    [Header("References")]
    public AWSD player;  
    [Header("Base Position")]
    public Vector3 basePosition = new Vector3(0.3f, -0.3f, 0.6f);

    [Header("Bob Settings")]
    public float walkBobSpeed = 6f;
    public float walkBobAmount = 0.02f;

    public float runBobSpeed = 12f;
    public float runBobAmount = 0.06f;

    [Header("Sway")]
    public float swayAmount = 0.02f;
    public float swaySmooth = 6f;

    private float bobTimer = 0f;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        if (player == null)
            player = FindObjectOfType<AWSD>();
    }

    void Update()
    {
        bool isMoving = Input.GetAxisRaw("Horizontal") != 0 ||
                        Input.GetAxisRaw("Vertical")   != 0;

        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        float speed  = isRunning ? runBobSpeed  : walkBobSpeed;
        float amount = isRunning ? runBobAmount : walkBobAmount;

        Vector3 bobOffset = Vector3.zero;

        if (isMoving && player != null && player.enabled)
        {
            bobTimer += Time.deltaTime * speed;

            bobOffset.y = Mathf.Sin(bobTimer) * amount;
            bobOffset.x = Mathf.Sin(bobTimer * 0.5f) * amount * 0.5f;
        }
        else
        {
            bobTimer = Mathf.Lerp(bobTimer, 0f, Time.deltaTime * 5f);
        }

        targetPosition = basePosition + bobOffset;
        transform.localPosition = Vector3.Lerp(
            transform.localPosition, targetPosition, Time.deltaTime * swaySmooth);

        float tilt = isMoving ? Mathf.Sin(bobTimer) * (isRunning ? 4f : 1.5f) : 0f;
        targetRotation = Quaternion.Euler(0f, 0f, tilt);
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation, targetRotation, Time.deltaTime * swaySmooth);
    }
}