using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerStatus playerStatus;       //ÕÊº“ Ù–‘
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //“∆∂Ø
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 go = new Vector3(h, v, 0);
        if (go != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            if (h > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (h < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                go = new Vector3(-h, v, 0);
            }
            var speed = playerStatus.GetSpeed(4);
            transform.Translate(go * speed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
