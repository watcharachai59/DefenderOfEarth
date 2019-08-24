using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptHPStam : MonoBehaviour
{
    public GameObject Over;
    public GameObject Win;
    public static float timeStart;
    public Text texttime;

    public Text texttimeBoss;
    public Text texttclass;

    public static bool timerActive = false;



    public static bool checkslow;

    public static bool damaged;
    public Image ImgRed;

    public static int Hp;



    public static int currentHP;
    public Slider Hpslider;

    int Stam = 100;

    public float flashSpeed = 0.1f;
    public Color flashColour = new Color(1f, 0f, 0f, 1f);

    public Color flashColourwhite = new Color(1f, 1f, 1f, 0f);

    public Color FlashGameOver = new Color(0f, 0f, 0f, 1f);

    public static float currentStam;
    public Slider Stamslider;

    public static bool isdead;


    // Start is called before the first frame update
    void Start()
    {
        isdead = false;
        Hp = 100;
        Over.SetActive(false);
        Win.SetActive(false);
        timerActive = true;
        timeStart = Time.time;

        currentHP = Hp;
        currentStam = Stam;

    }

    void loadScenes()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        Hpslider.value = currentHP;

        texttimeBoss.text = texttime.text;
        if (timerActive)
        {
            float t = Time.time - timeStart;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            texttime.text = minutes + ":" + seconds;



            if (minutes == "1")
            {
                texttclass.text = "S";
            }
            else if (minutes == "2")
            {
                texttclass.text = "A";
            }
            else if (minutes == "3")
            {
                texttclass.text = "B";
            }
            else if (minutes == "4")
            {
                texttclass.text = "C";
            }
            else if (minutes == "5")
            {
                texttclass.text = "D";
            }
            else if (minutes != "5")
            {
                texttclass.text = "D";
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            timerActive = !timerActive;

        }




        if (!isdead)
        {
            timerActive = true;

            // Time.timeScale = 1f;
        }
        else
        {
            Over.SetActive(true);
            timerActive = false;
            ImgRed.color = FlashGameOver;
            damaged = false;
            Invoke("loadScenes", 6f);
            isdead = false;


        }


        if (currentHP <= 0)
        {
            isdead = true;

        }

        if (currentHP > 100)
        {
            currentHP = 100;

        }

        if (currentStam <= 0)
        {
            currentStam = 0;
        }
        if (currentStam >= 100)
        {
            currentStam = 100;
        }

        if (checkslow == true)
        {
            currentStam -= 0.3f;
            Stamslider.value = currentStam;
            CancelInvoke("ReStam");
        }
        else
        {
            InvokeRepeating("ReStam", 10f, 0f);

        }

        Stamslider.value = currentStam;



        if (damaged == true)
        {
            ImgRed.color = flashColour;
            damaged = false;
        }
        else
        {
            Invoke("color", 1f);
            //  ImgRed.color = Color.Lerp(ImgRed.color, Color.clear, flashSpeed);
        }

        //damaged = false;
    }

    void ReStam()
    {
        currentStam += 0.5f;
        Stamslider.value = currentStam;
    }


    void color()
    {
        ImgRed.color = flashColourwhite;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            currentHP -= 10;
            Hpslider.value = currentHP;
            damaged = true;
        }
        else
        {
            damaged = false;
        }



    }

    void OnTriggerExit(Collider other)
    {

    }
}
