using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Gun : MonoBehaviour
{
    public AudioClip sndFire; //총 발사음
    public AudioClip sndCocking; //실탄 장전음
    public AudioClip sndStage; // 배경음악
    public AudioClip sndOver; //게임오버

        public Transform clay, bird, exp, gunFire, firebullet;

    Transform bulletCase, spPoint; //탄피, 총구위치

    static public int miss;
    int hit;
    int bulletCnt;
    float startTime, overTime;
    bool gameOver = false;

    int width, height;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Cursor.visible = false;

        SetStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (miss >= 10)
        {
            gameOver = true;
            overTime = Time.time;
        }

        if (gameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) EditorApplication.isPlaying = false;

       
        MakeClay();
        RotateGun();

        if (Input.GetMouseButtonDown(0))
        {
            FireGun();
        }

        if (bulletCnt < 5 && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ChargeGun());
        }
    }

    void RotateGun()
    {
        Vector3 pos = Input.mousePosition; //카메로부터 거리 설정

        pos.x = Mathf.Clamp(pos.x, 0, Screen.width); //거리제한
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);

        pos.z = 13.2f;

        Vector3 view = Camera.main.ScreenToWorldPoint(pos); //마우스 위치를 월드좌표로 변환
        transform.LookAt(view);
    }

    void FireGun()
    {
        if (GetComponent<Animation>().isPlaying) return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //카메라 시점에서 커서 위치 계산
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        if(hit.transform.tag == "Gun" && bulletCnt < 5) //
        {
            StartCoroutine(ChargeGun());
            return;
        }

        Instantiate(gunFire, spPoint.position, Quaternion.identity);

        Instantiate(firebullet, spPoint.position, spPoint.rotation);

        GetComponent<Animation>().Play("Fire");
        
        CheckTarget(hit);

        bulletCnt--;

        if (bulletCnt <= 0)
        {
            StartCoroutine(ChargeGun());
        }
    }
    //목표물 적중 여버 판정
    void CheckTarget(RaycastHit hit)
    {
        switch (hit.transform.tag)
        {
            case "Clay":
                this.hit++;
                Instantiate(exp, hit.transform.position, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                break;

            default:
                Instantiate(gunFire, hit.transform.position, Quaternion.identity);
                miss++;
                hit.transform.SendMessage("DeadBird", SendMessageOptions.DontRequireReceiver);
                break;
        }
    }

    IEnumerator ChargeGun()
    {
        while (GetComponent<Animation>().isPlaying)
        {
            yield return 0; 

        }
        GetComponent<Animation>().Play("ChargeGun");
        yield return new WaitForSeconds(0.5f);
        bulletCnt = 5;
    }

    void MakeClay()
    {
        if(Random.Range(0,1000)>970 && !GetComponent<Animation>().isPlaying)
        {
            if (Random.Range(0, 100) < 70)
            {
                Instantiate(clay);
            }
            else
            {
                Instantiate(bird);
            }
        }
    }

    public void SoundClick()
    {
        bulletCase.gameObject.GetComponent<Renderer>().enabled = true;
        AudioSource.PlayClipAtPoint(sndCocking, Vector3.zero);
    }

    public void SoundFire()
    {
        bulletCase.gameObject.GetComponent<Renderer>().enabled = true;
        AudioSource.PlayClipAtPoint(sndFire, Vector3.zero);
    }

    public void HideBullet()
    {
        bulletCase.gameObject.GetComponent<Renderer>().enabled = false;
    }

    void SetStage()
    {
        width = Screen.width;
        height = Screen.height;

        spPoint = GameObject.Find("spPoint").transform;
        bulletCase = transform.Find("Cylinder");
        bulletCase.gameObject.GetComponent<Renderer>().enabled = false;
        startTime = Time.time;
        hit = miss = 0;
        bulletCnt = 5;
        gameOver = false;

        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }

    private void OnGUI()
    {
        float time = Time.time - startTime;
        if (!gameOver)
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 24, height - Input.mousePosition.y - 24, 48, 48), Resources.Load("crossHair") as Texture2D);

        }
        else
        {
            time = overTime - startTime;
        }

        for(int i = 1; i <= bulletCnt; i++)
        {
            GUI.DrawTexture(new Rect(i * 12, height - 20, 8, 16), Resources.Load("bullet") as Texture2D);
        }

        string sHit = "<size='30'>HIT : " + hit + "</size>";
        string sMiss = "<size='30'>MISS : " + miss + "</size>";
        string sTime = "<color='yellow'><size='30'>Time :" + (int)time + "</size></color>";

        GUI.Label(new Rect(30, 20, 120, 40), sHit);
        GUI.Label(new Rect(width/2-40, 20, 160, 40), sTime);
        GUI.Label(new Rect(width-120, 20, 120, 40), sMiss);

        string msg = "Shoot : Left Btn, Charge : Gun Click";
        GUI.Label(new Rect(width - 280, height - 40, 380, 40), msg);

        if (gameOver)
        {
            Cursor.visible = true;
            if (GetComponent<AudioSource>().clip != sndOver)
            {
                GetComponent<AudioSource>().clip = sndOver;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
            }
             if(GUI.Button(new Rect(width/2 - 70, height/2 -50, 140, 60), "Play Game"))
            {
                Application.LoadLevel("MainGame");
            }

            if (GUI.Button(new Rect(width / 2 - 70, height / 2 + 50, 140, 60), "Quit Game"))
            {
                EditorApplication.isPlaying = false;
            }

        }
    }
}
