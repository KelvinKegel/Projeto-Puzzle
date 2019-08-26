using UnityEngine;

public class Platform : MonoBehaviour
{
    private enum MovementType
    {
        Circular,
        Straight,
        RotateAround
    }

    Player player;

    [SerializeField]
    private Transform rotationCenter;

    private Vector3 rotationPosition;

    [SerializeField]
    private float rotationRadius = 2f, angularSpeed = 2f;

    private float angle = 0f;

    [SerializeField]
    private MovementType movementType_;

    // Start is called before the first frame update
    private void Start()
    {
        rotationPosition = rotationCenter.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (movementType_)
        {
            case MovementType.RotateAround:
                rotateAround();
                break;

            case MovementType.Circular:
                circularMovement();
                break;

            default:
                circularMovement();
                break;
        }
    }

    private void circularMovement()
    {
        float posX, posY = 0f;

        posX = rotationPosition.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationPosition.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector3(posX, posY, transform.position.z);
        angle += Time.deltaTime * angularSpeed;

        if (angle >= 360f)
            angle = 0f;
    }

    private void rotateAround()
    {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, 20 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("chabdcbs");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("chabdcbs");
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}