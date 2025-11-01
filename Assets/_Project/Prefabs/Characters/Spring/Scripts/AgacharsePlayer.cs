using UnityEngine;

public class AgacharsePlayer : MonoBehaviour
{
    private float crouchHeight;
    private Vector2 crounchOffset;
    private float originalHeight;
    private Vector2 originalOffset;
    private LayerMask obstacleLayer;
    private bool isCrouching = false;
    private bool isGrounded;
    private bool inputAgacharse;
    private CapsuleCollider2D capsuleCollider;


    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        originalHeight = 2.3f;
        originalOffset = new Vector2(capsuleCollider.offset.x, -0.18f);

        crouchHeight = 1.5f;
        crounchOffset = new Vector2(capsuleCollider.offset.x, -0.58f);

        obstacleLayer = LayerMask.GetMask("Obstacles");

    }

    void Update()
    {
        inputAgacharse = Input.GetKey(KeyCode.LeftControl);
        isGrounded = GetComponent<SaltoPlayer>().IsGrounded();

        if (inputAgacharse && isGrounded)
        {
            Agacharse();
        }
        else if (!inputAgacharse && isCrouching)
        {
            Levantarse();
        }

    }

    private void Agacharse()
    {
        isCrouching = true;
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, crouchHeight);
        capsuleCollider.offset = crounchOffset;
        Debug.Log("Agachado");

    }

    private void Levantarse()
    {
        if (CanStandUp())
        {
            isCrouching = false;
            capsuleCollider.size = new Vector2(capsuleCollider.size.x, originalHeight);
            capsuleCollider.offset = originalOffset;
        }

    }

    private bool CanStandUp()
    {
        Vector2 standingCenter = (Vector2)transform.position + originalOffset;
        Vector2 standingSize = new Vector2(capsuleCollider.size.x, originalHeight);

        Collider2D obstacleFound = Physics2D.OverlapBox(
            standingCenter,
            standingSize,
            0f,
            obstacleLayer
            );

        return (obstacleFound == null);
    }

}
