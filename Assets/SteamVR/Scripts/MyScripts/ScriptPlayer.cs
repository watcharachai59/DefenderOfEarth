using UnityEngine;
using Valve.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScriptPlayer : MonoBehaviour
{
    public SteamVR_Action_Vibration vibra;

    public GameObject showscoregame;

    /*public GameObject stai2;
    public GameObject stai3;
    public GameObject stai4;*/





    public Text textammoR;
    public Text textammoL;

    public AudioClip soundShoot;
    public AudioClip soundReload;
    public AudioClip soundReHp;
    AudioSource Sound;


    float aowailop;
    float aowailop2;
    float ti = 0.1f;
    private static ScriptPlayer instance;

    public static ScriptPlayer MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScriptPlayer>();
            }

            return instance;
        }
    }

    private Animator ani;


    public static int checkstate;
    public ParticleSystem particleR;
    public ParticleSystem particleL;

    [SerializeField]
    private GameObject particleGun;
    [SerializeField]
    private GameObject blood;
    public SteamVR_Input_Sources handTypeR, handTypeL;

    public SteamVR_Action_Boolean grabAction1L, grabAction1R;

    public SteamVR_Action_Boolean SlowAction1L, SlowAction1R;

    public SteamVR_Action_Boolean TouchdownR, TouchdownL;


    [SerializeField]
    private LineRenderer laserLineRendererL;

    [SerializeField]
    private LineRenderer laserLineRendererR;
    [SerializeField]
    private Rigidbody GunmaxL, GunmaxR;

    [SerializeField]
    Collider GunmaxLShow, GunmaxRShow;
    [SerializeField]
    private float laserMaxLength = 5f;

    public float ShootDamage = 100;
    public float PlayerHP = 100;
    public static int AmmoR, AmmoL = 25;
    public Transform PointShoot;
    void Start()
    {
        ScriptHeadShot.checkwin = false;

        ScriptHPStam.isdead = false;
        ScriptHPStam.currentHP = 100;
        showscoregame.SetActive(false);


        Sound = GetComponent<AudioSource>();


        AmmoR = 25;
        AmmoL = 25;

        textammoL.text = AmmoL.ToString();
        textammoR.text = AmmoR.ToString();



        GunmaxLShow.GetComponent<Collider>().enabled = false;
        GunmaxRShow.GetComponent<Collider>().enabled = false;
        ani = GetComponent<Animator>();
        checkstate = 0;
        Time.timeScale = 1;
        laserLineRendererL = laserLineRendererL.GetComponent<LineRenderer>();
        laserLineRendererR = laserLineRendererR.GetComponent<LineRenderer>();
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRendererL.SetPositions(initLaserPositions);

#pragma warning restore CS0618 // Type or member is obsolete
        laserLineRendererL.enabled = true;
        laserLineRendererR.SetPositions(initLaserPositions);
        laserLineRendererL.SetPositions(initLaserPositions);

#pragma warning restore CS0618 // Type or member is obsolete
        laserLineRendererR.enabled = true;
    }



    void showbtn()
    {
        showscoregame.SetActive(true);
    }
    void Update()
    {
        

        textammoL.text = AmmoL.ToString();
        textammoR.text = AmmoR.ToString();




        if (checkstate == 3)//(ScriptSpawnState.gos2 == true)
        {
            ani.SetBool("GOS2", true);


        }
        if (checkstate == 12)//(ScriptSpawnState.gos3 == true)
        {
            ani.SetBool("GOS3", true);

        }
        if (checkstate == 19)//(ScriptSpawnState.gos4 == true)
        {
            ani.SetBool("GOS4", true);

        }
        if (checkstate == 34)//(ScriptSpawnState.gos4 == true)
        {
            ani.SetBool("GOBOSS", true);
        }
        if (checkstate == 41 && ScriptHeadShot.checkwin == true)
        {
            ani.SetBool("win", true);
            Invoke("showbtn", 8f);
            ScriptHPStam.timerActive = false;
        }







        ShootLaserFromTargetPositionL(laserLineRendererL.transform.position, laserLineRendererL.transform.forward, laserMaxLength);



        ShootLaserFromTargetPositionR(laserLineRendererR.transform.position, laserLineRendererR.transform.forward, laserMaxLength);

        if (TouchdownR.GetStateDown(handTypeR))
        {
            if (GunmaxR != null)
            {
                GunmaxRShow.gameObject.GetComponent<CheckOnAmmo>().OnGun = false;
                Sound.PlayOneShot(soundReload);
                GunmaxR.GetComponent<GunMax>().HasUse = true;
                GunmaxR.transform.SetParent(null);
                GunmaxR.isKinematic = false;
                GunmaxR.GetComponent<Rigidbody>().AddForce(GunmaxRShow.transform.forward * 200);
                GunmaxRShow.enabled = true;
                AmmoR = 0;


                Destroy(GunmaxR.gameObject, 10);

                GunmaxR = null;


                print("outs");
            }
            print("RRR");

        }
        if (TouchdownL.GetStateDown(handTypeL))
        {
            if (GunmaxL != null)
            {
                Sound.PlayOneShot(soundReload);
                GunmaxL.GetComponent<GunMax>().HasUse = true;
                GunmaxL.transform.SetParent(null);
                GunmaxL.isKinematic = false;
                GunmaxL.GetComponent<Rigidbody>().AddForce(GunmaxLShow.transform.forward * 200);
                GunmaxLShow.enabled = true;
                GunmaxLShow.GetComponent<CheckOnAmmo>().OnGun = false;

                AmmoL = 0;
                Destroy(GunmaxL.gameObject, 10);

                GunmaxL = null;

                print("outs");

            }

            print("LLL");

        }

        if (SlowAction1R.GetState(handTypeR))
        {
            if (ScriptHPStam.currentStam > 0)
            {
                ScriptHPStam.checkslow = true;
                Time.timeScale = 0.2f;
            }
            else if (ScriptHPStam.currentStam <= 0)
            {
                ScriptHPStam.checkslow = false;
                Time.timeScale = 1f;
            }


            print("outs");
        }
        if (SlowAction1R.GetStateUp(handTypeR))
        {
            ScriptHPStam.checkslow = false;
            Time.timeScale = 1f;

            print("outs");
        }
        if (SlowAction1L.GetStateDown(handTypeL))
        {
            if (ScriptHPStam.currentStam > 0)
            {
                ScriptHPStam.checkslow = true;
                Time.timeScale = 0.2f;
            }
            else if (ScriptHPStam.currentStam <= 0)
            {
                ScriptHPStam.checkslow = false;
                Time.timeScale = 1f;
            }

            print("outs");
        }
        if (SlowAction1L.GetStateUp(handTypeL))
        {
            ScriptHPStam.checkslow = false;
            Time.timeScale = 1f;

            print("outs");
        }

    }



    void st2()
    {
        ScriptSpawnst2 spawnai2 = GameObject.Find("SpawnPontSt2").GetComponent<ScriptSpawnst2>();
        spawnai2.spawns2();
    }
    void st3()
    {
        ScriptSpawnst2 spawnai2 = GameObject.Find("SpawnPontSt3").GetComponent<ScriptSpawnst2>();
        spawnai2.spawns2();
    }
    void st4()
    {
        ScriptSpawnst2 spawnai2 = GameObject.Find("SpawnPontSt4").GetComponent<ScriptSpawnst2>();
        spawnai2.spawns2();
    }

    void stend()
    {
        ScriptSpawnst2 spawnai2 = GameObject.Find("SpawnPontStEnd").GetComponent<ScriptSpawnst2>();
        spawnai2.spawns2();
    }





    void ShootLaserFromTargetPositionR(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);

        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);




        if (grabAction1R.GetStateUp(handTypeR))
        {
            laserLineRendererR.enabled = false;
        }
        if (grabAction1L.GetStateUp(handTypeL))
        {
            laserLineRendererL.enabled = false;
        }



        if (grabAction1R.GetStateDown(handTypeR))
        {
            Sound.PlayOneShot(soundReload);
            if (Time.time - aowailop >= ti)
            {
                if (AmmoR > 0)
                {

                    float seconds = (float)900 / 9000f;
                    vibra.Execute(0, seconds, 20f / seconds, 20, handTypeR);


                    laserLineRendererR.enabled = true;
                    Sound.PlayOneShot(soundShoot);
                    particleR.Emit(1);

                    if (Physics.Raycast(ray, out raycastHit, length))
                    {

                        if (raycastHit.transform.tag == "Enemy")
                        {
                            if (raycastHit.transform.name == "head")
                            {


                                EnemyShooter target = raycastHit.transform.GetComponentInChildren<EnemyShooter>();
                                if (target != null)
                                {

                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);

                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(100);
                                    print("001");

                                }
                            }
                            else
                            {
                                EnemyShooter target = raycastHit.transform.GetComponent<EnemyShooter>();
                                if (target != null)
                                {


                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(ShootDamage);
                                    

                                }
                            }

                        }



                        if (raycastHit.transform.name == "Militarytarget")
                        {
                            raycastHit.rigidbody.AddForce(-raycastHit.normal * 50f);
                        }




                            if (raycastHit.transform.tag == "headAi")
                        {
                            if (raycastHit.transform.name == "head")
                            {


                                ScriptSitShoot target = raycastHit.transform.GetComponentInChildren<ScriptSitShoot>();
                                if (target != null)
                                {

                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);

                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(100);
                                    print("001");

                                }
                            }
                            else
                            {
                                ScriptSitShoot target = raycastHit.transform.GetComponent<ScriptSitShoot>();
                                if (target != null)
                                {


                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(ShootDamage);


                                }
                            }

                        }









                        else
                        {
                            if (raycastHit.transform.tag == "Bomb")
                            {

                                ScriptBome target = raycastHit.transform.GetComponent<ScriptBome>();
                                if (target != null)
                                {
                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(ShootDamage);
                                }


                            }
                            else
                            {
                                if (raycastHit.transform.tag == "Bossfinal")
                                {

                                    ScriptHeadShot target = raycastHit.transform.GetComponent<ScriptHeadShot>();
                                    if (target != null)
                                    {
                                        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                        GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                        Destroy(particleGun2, 2f);
                                        target.TakeDamage(20f);
                                    }


                                }
                                else
                                {
                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(particleGun, raycastHit.point, rot);

                                    Destroy(particleGun2, 2f);
                                }

                                if (raycastHit.transform.tag == "StartGame")
                                {

                                    OpenDoor target = raycastHit.transform.GetComponent<OpenDoor>();
                                    if (target != null)
                                    {
                                        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                        GameObject particleGun2 = Instantiate(particleGun, raycastHit.point, rot);


                                        Destroy(particleGun2, 2f);
                                        target.TakeDamage(ShootDamage);
                                    }


                                }

                                if (raycastHit.transform.tag == "lobot")
                                {

                                    EnemyShooter2 target = raycastHit.transform.GetComponent<EnemyShooter2>();
                                    if (target != null)
                                    {
                                        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                        GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                        Destroy(particleGun2, 2f);
                                        target.TakeDamage(ShootDamage);
                                    }



                                }
                                if (raycastHit.transform.tag == "newgame")
                                {
                                    SceneManager.LoadScene(0);

                                }

                            }

                            if (raycastHit.transform.tag == "hp")
                            {

                                reHp target = raycastHit.transform.GetComponent<reHp>();
                                if (target != null)
                                {
                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(particleGun, raycastHit.point, rot);

                                    Sound.PlayOneShot(soundReHp, 30f);
                                    Destroy(particleGun2, 2f);
                                    ScriptHPStam.currentHP += 100;
                                    target.TakeDamage(500);
                                }



                            }

                            if (raycastHit.transform.tag == "Bullet")
                            {

                                EnemyBullet target = raycastHit.transform.GetComponent<EnemyBullet>();
                                if (target != null)
                                {
                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(particleGun, raycastHit.point, rot);


                                    Destroy(particleGun2, 2f);

                                    target.TakeDamage(ShootDamage);
                                }



                            }

                            if (raycastHit.transform.tag == "StartGame")
                            {

                                Sound.PlayOneShot(soundReHp, 30f);
                                StartCoroutine(stargame());

                            }

                        }

                        AmmoR--;


                    }

                    else
                    {
                        print("Empty");
                    }
                    endPosition = raycastHit.point;

                }
                aowailop = Time.time;



            }

            laserLineRendererR.SetPosition(0, targetPosition);
            laserLineRendererR.SetPosition(1, endPosition);
        }
    }
    IEnumerator stargame()
    {
        
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    void ShootLaserFromTargetPositionL(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);

        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (grabAction1L.GetStateDown(handTypeL))
        {
            Sound.PlayOneShot(soundReload);
            if (Time.time - aowailop2 >= ti)
            {
                if (AmmoL > 0)
                {
                    float seconds = (float)900 / 9000f;
                    vibra.Execute(0, seconds, 20f / seconds, 20, handTypeL);


                    laserLineRendererL.enabled = true;
                    Sound.PlayOneShot(soundShoot);
                    particleL.Emit(1);

                    if (Physics.Raycast(ray, out raycastHit, length))
                    {


                        

                        if (raycastHit.transform.tag == "Enemy")
                        {
                            if (raycastHit.transform.name == "head")
                            {


                                EnemyShooter target = raycastHit.transform.GetComponentInChildren<EnemyShooter>();
                                if (target != null)
                                {

                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);

                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(100);
                                    print("001");

                                }
                            }
                            else
                            {
                                EnemyShooter target = raycastHit.transform.GetComponent<EnemyShooter>();
                                if (target != null)
                                {


                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(ShootDamage);


                                }
                            }

                        }






                        if (raycastHit.transform.name == "Militarytarget")
                        {
                            raycastHit.rigidbody.AddForce(-raycastHit.normal * 50f);
                        }








                        if (raycastHit.transform.tag == "headAi")
                        {
                            if (raycastHit.transform.name == "head")
                            {


                                ScriptSitShoot target = raycastHit.transform.GetComponentInChildren<ScriptSitShoot>();
                                if (target != null)
                                {

                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);

                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(100);
                                    print("001");

                                }
                            }
                            else
                            {
                                ScriptSitShoot target = raycastHit.transform.GetComponent<ScriptSitShoot>();
                                if (target != null)
                                {


                                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                    GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                    Destroy(particleGun2, 2f);
                                    target.TakeDamage(ShootDamage);


                                }
                            }

                        }
















                        if (raycastHit.transform.tag == "Bomb")
                        {

                            ScriptBome target = raycastHit.transform.GetComponent<ScriptBome>();
                            if (target != null)
                            {
                                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                Destroy(particleGun2, 2f);
                                target.TakeDamage(ShootDamage);
                            }


                        }

                        if (raycastHit.transform.tag == "Bossfinal")
                        {

                            ScriptHeadShot target = raycastHit.transform.GetComponent<ScriptHeadShot>();
                            if (target != null)
                            {
                                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                Destroy(particleGun2, 2f);
                                target.TakeDamage(10);
                            }


                        }

                        if (raycastHit.transform.tag == "StartGame")
                        {

                            OpenDoor target = raycastHit.transform.GetComponent<OpenDoor>();
                            if (target != null)
                            {
                                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                GameObject particleGun2 = Instantiate(particleGun, raycastHit.point, rot);


                                Destroy(particleGun2, 2f);
                                target.TakeDamage(ShootDamage);
                            }


                        }

                        if (raycastHit.transform.tag == "lobot")
                        {

                            EnemyShooter2 target = raycastHit.transform.GetComponent<EnemyShooter2>();
                            if (target != null)
                            {
                                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                GameObject particleGun2 = Instantiate(blood, raycastHit.point, rot);


                                Destroy(particleGun2, 2f);
                                target.TakeDamage(ShootDamage);
                            }



                        }

                        if (raycastHit.transform.tag == "newgame")
                        {
                            SceneManager.LoadScene(0);

                        }

                        if (raycastHit.transform.tag == "hp")
                        {

                            reHp target = raycastHit.transform.GetComponent<reHp>();
                            if (target != null)
                            {
                                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                GameObject particleGun2 = Instantiate(particleGun, raycastHit.point, rot);

                                Sound.PlayOneShot(soundReHp, 30f);
                                Destroy(particleGun2, 2f);
                                ScriptHPStam.currentHP += 100;
                                target.TakeDamage(500);
                            }



                        }

                        if (raycastHit.transform.tag == "Bullet")
                        {

                            EnemyBullet target = raycastHit.transform.GetComponent<EnemyBullet>();
                            if (target != null)
                            {
                                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);

                                GameObject particleGun2 = Instantiate(particleGun, raycastHit.point, rot);


                                Destroy(particleGun2, 2f);

                                target.TakeDamage(ShootDamage);
                            }



                        }

                        if (raycastHit.transform.tag == "StartGame")
                        {

                            Sound.PlayOneShot(soundReHp, 30f);
                            StartCoroutine(stargame());

                        }




                        AmmoL--;
                    }
                    else
                    {
                        print("Empty");
                    }
                    endPosition = raycastHit.point;


                }

                aowailop2 = Time.time;

            }

            laserLineRendererL.SetPosition(0, targetPosition);
            laserLineRendererL.SetPosition(1, endPosition);
        }


    }
    public void GetMax(Rigidbody maxRigi, bool isRight)
    {
        if (isRight)
        {
            GunmaxR = maxRigi;
            //   GunmaxRShow.enabled = false;
            Sound.PlayOneShot(soundReload);
            AmmoR = 25;
        }
        else
        {
            GunmaxL = maxRigi;
            //   GunmaxLShow.enabled = false;
            Sound.PlayOneShot(soundReload);
            AmmoL = 25;
        }
    }
    public float TakeDamade
    {
        set
        {
            PlayerHP -= value;
            if (PlayerHP <= 0)
            {
                print("Die");
            }
        }
    }




}
