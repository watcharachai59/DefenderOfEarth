using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private EnemyShooter enemy;
    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponentInParent<EnemyShooter>();
    }
    public void StarkATK()
    {
        anim.SetTrigger("Attack");
    }

}
