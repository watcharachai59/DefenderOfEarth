using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public AudioClip soundbomb;
    AudioSource sound;
    public float speedBullet = 20f;
    float HP = 1;


    public void TakeDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            sound.PlayOneShot(soundbomb);
            Instantiate(efaf, transform.position, transform.rotation);
            Destroy(gameObject, 0.1f);
        }
    }



    [SerializeField]
    GameObject efaf;

    // private Transform targets;
    float moveSpeed;
    float damage;


    Vector3 target;
    public void SetBullet(float bulletSpeed, float damages, Vector3 PlayerPoint)
    {
        moveSpeed = bulletSpeed;
        damage = damages;
        target = PlayerPoint;
    }
    // Start is called before the first frame update
    void Start()
    {

        sound = GetComponent<AudioSource>();

        // targets = GameObject.FindObjectOfType<ScriptTargetplayer>().PointShoot;

        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 dir = (target - transform.position).normalized * speedBullet;
        rb.velocity = new Vector3(dir.x, dir.y, dir.z);


        Destroy(gameObject, 5f);
    }

    void Update()
    {

        // transform.LookAt(targets, Vector3.up);
        //  transform.position = Vector3.Lerp(transform.position, targets.position, 0.1f);


    }



    private void OnTriggerEnter(Collider other)
    {


        //Instantiate(efaf, transform.position, transform.rotation);


        if (other.gameObject.tag == "Player")
        {
            //   PlayerWalk hit = GetComponent<PlayerWalk>();
            Destroy(gameObject);
            //hit.TakeDamade = damage;
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {

            }
            else
            {
                Instantiate(efaf, transform.position, transform.rotation);
                Destroy(gameObject);
            }


        }
    }
}
