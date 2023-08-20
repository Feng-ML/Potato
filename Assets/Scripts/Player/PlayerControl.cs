using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerStatus playerStatus;       //ÕÊº“ Ù–‘
    private Animator animator;
    public Bag playerBag;
    public List<Vector2> weaponPsList;      //Œ‰∆˜Œª÷√

    void Start()
    {
        animator = GetComponent<Animator>();
        LoadPlayerWeapon();
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

    //º”‘ÿŒ‰∆˜
    private void LoadPlayerWeapon()
    {
        for (int i = 0; i < playerBag.weaponList.Count; i++)
        {
            Instantiate(playerBag.weaponList[i].weaponPrefab, weaponPsList[i], Quaternion.identity, transform);
        }
    }

    //ª≠Œ‰∆˜Œª÷√
    private void OnDrawGizmosSelected()
    {
        weaponPsList.ForEach(item =>
        {
            Gizmos.DrawSphere(item, 0.1f);
        });
    }
}
