using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    Vector3 mousePosition;
    
    Rigidbody2D rb;
    Vector2 position = new Vector2(22f, 0f);

    private Vector3 topRightLimit;
    private Vector3 bottomLeftLimit;
    public GameObject topRightLimitGameobject;
    public GameObject bottomLeftLimitGameobject;

    [Range(0f, 0.1f)]
    public float moveSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        topRightLimit = topRightLimitGameobject.transform.position;
        bottomLeftLimit = bottomLeftLimitGameobject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        // we need mouse position in world space, not pixels
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // linearly interpolates between two points
        // define invisible border
        var borderDefinedMousePos = new Vector2();

        borderDefinedMousePos.x = Mathf.Min(Mathf.Max(mousePosition.x, bottomLeftLimit.x), topRightLimit.x);
        borderDefinedMousePos.y = Mathf.Min(Mathf.Max(mousePosition.y, bottomLeftLimit.y), topRightLimit.y);
        position = Vector2.Lerp(transform.position, borderDefinedMousePos, moveSpeed);

        rb.MovePosition(position);
    }

    // fixed update for physics-based systems -> used for the rigidbody system, which as the moveposition function
    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}
