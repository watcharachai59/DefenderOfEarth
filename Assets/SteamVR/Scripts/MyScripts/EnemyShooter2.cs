using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter2 : MonoBehaviour
{
    public AudioClip sounddie;
    AudioSource sound;

    Collider coai;
    public GameObject EfBomb;
    //public static bool checkdie = false;
    Animator ani;
    public float timeEnemy;
    public float HP = 100;
    public float ShootDamage = 20;
    float BulletSpeed = 23f;
    //public int GoStand;
    public void TakeDamage(float damage)
    {
        HP -= damage;
        ani.SetTrigger("Damage");
        if (HP <= 0)
        {
            sound.PlayOneShot(sounddie);
            coai.enabled = false;
            ani.SetTrigger("Death");
            Instantiate(EfBomb, transform.position, transform.rotation);
            Invoke("des", 1f);
            //checkdie = true;



        }
    }

    [SerializeField]
    private LineRenderer laserLineRenderer;


    [SerializeField]
    private float laserMaxLength = 100f;


    [SerializeField]
    private EnemyAnimation EnemyAnim;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        coai = GetComponent<Collider>();

        ani = GetComponent<Animator>();
        laserLineRenderer = laserLineRenderer.GetComponent<LineRenderer>();

        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);

#pragma warning restore CS0618 // Type or member is obsolete
        laserLineRenderer.enabled = false;

        StartAnimShoot();

    }

    Vector3 target;

    // Angular speed in radians per sec.
    float Lookspeed = 2f;

    [SerializeField]
    private Transform Gun;


    Transform targets;

    void StartAnimShoot()
    {
        targets = GameObject.FindObjectOfType<ScriptTargetplayer>().PointShoot;


        // EnemyAnim.StarkATK();
        StartCoroutine(StartShooting());
    }
    void ShowLaser(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);


        if (Physics.Raycast(ray, out raycastHit, length))
        {
            /*
                EnemyShooter target = raycastHit.transform.GetComponent<EnemyShooter>();
                if (target != null)
                {
                    target.TakeDamage(ShootDamage);
                }
          */
            endPosition = raycastHit.point;
            PlayerPoint = raycastHit.point;
        }


        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);


    }
    [SerializeField]
    GameObject bullet;

    Vector3 PlayerPoint;

    IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(0f);

        target = targets.transform.position;

        float duration = 4f;
        float startTime = Time.time;
        var targetRotationset = Quaternion.LookRotation(target - Gun.position);

        Gun.rotation = Quaternion.Slerp(Gun.rotation, targetRotationset, 100);
        //laserLineRenderer.enabled = true;
        while (Time.time - startTime < duration)
        {

            target = targets.transform.position;

            ShowLaser(laserLineRenderer.transform.position, laserLineRenderer.transform.forward, laserMaxLength);

            //    Vector3 targetDir = target - Gun.position;


            float step = Lookspeed * Time.deltaTime;

            //    Vector3 newDir = Vector3.RotateTowards(Gun.forward, targetDir, step, 0.0f);


            //     Gun.rotation = Quaternion.LookRotation(newDir);

            var targetRotation = Quaternion.LookRotation(target - Gun.position);

            // Smoothly rotate towards the target point.
            Gun.rotation = Quaternion.Slerp(Gun.rotation, targetRotation, step);


            yield return null;
        }

        yield return new WaitForSeconds(timeEnemy);

        laserLineRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);

        ani.SetTrigger("Attack");
        GameObject B = Instantiate(bullet, Gun.transform.position, Quaternion.identity);
        B.GetComponent<EnemyBullet>().SetBullet(BulletSpeed, ShootDamage, PlayerPoint);


        yield return new WaitForSeconds(2f);
        StartAnimShoot();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BombKillai")
        {
            coai.enabled = false;
            ani.SetTrigger("Death");
            Invoke("des", 2.3f);
            //  checkdie = true;

        }
    }


    void des()
    {

        Destroy(gameObject);
        ScriptPlayer.checkstate += 1;

    }

}
