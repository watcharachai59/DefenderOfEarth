using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSitShoot : MonoBehaviour
{
    //public GameObject ColiderHeadobj;
    public Collider ColiderHead;

    public AudioClip SoundShoot;
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


    private static ScriptSitShoot instance;

    public static ScriptSitShoot MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScriptSitShoot>();
            }

            return instance;
        }
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        ani.SetTrigger("Damage");
        if (HP <= 0)
        {
           // Destroy(ColiderHeadobj);
            Destroy(ColiderHead);
            Onoff();
            coai.enabled = false;
            ani.SetTrigger("Death");
            Instantiate(EfBomb, transform.position, transform.rotation);
            Invoke("des", 2.3f);
            
            //checkdie = true;



        }

    }

    [SerializeField]
    private LineRenderer laserLineRenderer;


    [SerializeField]
    private float laserMaxLength = 100f;



    void Start()
    {


        //ColiderHead = GetComponentInChildren<Collider>();
        //ColiderHead.enabled = false;
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
    float Lookspeed = 20f;

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

        float duration = 1f;
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


        yield return null;


        ani.SetTrigger("Attack");
         ColiderHead.enabled = true;
        GameObject B = Instantiate(bullet, Gun.transform.position, Quaternion.identity);
        B.GetComponent<EnemyBullet>().SetBullet(BulletSpeed, ShootDamage, PlayerPoint);
        sound.PlayOneShot(SoundShoot, 30f);






        yield return new WaitForSeconds(2f);
        StartAnimShoot();
    }

     public void Onoff()
     {
        
         ColiderHead.enabled = false;
         print("1234");
         Debug.Log(ColiderHead);
     }





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BombKillai")
        {

           // Destroy(ColiderHeadobj);
            Destroy(ColiderHead);
            Onoff();
            coai.enabled = false;
            ani.SetTrigger("Death");
            Instantiate(EfBomb, transform.position, transform.rotation);
            Invoke("des", 2.3f);
            ColiderHead.enabled = false;

        }
    }


    void des()
    {
        
        Destroy(gameObject);
        ScriptPlayer.checkstate += 1;

    }

}
