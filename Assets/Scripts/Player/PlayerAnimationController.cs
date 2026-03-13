using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    private PlayerMovement playerMovement;
    public float currentSpeed;

    private void FixeUpdate()
    {      
        currentSpeed = playerMovement.currentSpeed;
        animator.SetFloat("currentSpeed", currentSpeed);
    }
}
