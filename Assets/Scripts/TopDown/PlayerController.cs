using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Vector2 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 bottomeLeftLimit;
    private Vector3 topRightLimit;



    public static PlayerController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        theRB.velocity = moveDirection * moveSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomeLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomeLeftLimit.y, topRightLimit.y), transform.position.z);

    }
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomeLeftLimit = botLeft + new Vector3(0.5f, 0.8f, 0);
        topRightLimit = topRight - new Vector3(0.5f, 0.8f, 0);
    }
}
