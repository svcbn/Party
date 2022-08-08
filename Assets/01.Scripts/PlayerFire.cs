using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

//Fire1 버튼을 누르면
//bullet 총구앞엑서 나가게
//총쏠때 소리나게 
public class PlayerFire : MonoBehaviourPun
{
    //총구
    public Transform firePos;
    public Transform firePos2;
    //bullet prefab
    public GameObject bulletFactory;
    public GameObject bulletFactory2;
    //Audio Source
    AudioSource audioSource;
    //총알파편 prefab
    public GameObject bulletPiecesFactory;

    float currentTime = 1.5f;
    public float coolTime = 1.5f;

    //public Image turnIcon;
    //bool isMyTurn = false;

    public GameObject GunPivot;
    //check whether gun rotating has finished
    bool isGunMoving = false;
    //check whether fireUp event has finished
    bool fireComplete = false;


    // Start is called before the first frame update
    void Start()
    {
        //자기 포톤 뷰를 게임매니저에 알려주자 
        GameManager.instance.AddPlayer(photonView);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        if(!photonView.IsMine)
        {
            return;
        }
        if (Cursor.visible)
        {
            return;
        }

        //내 순서가 아니면 return
        //if (!isMyTurn) return;

        //Fire1 버튼을 누르면
        if (Input.GetButtonDown("Fire1") && currentTime > coolTime) 
        {
            //총구 올렸다 내려야 한다
            //부드럽게 총구를 올렸다가 쏘고 다시 내리고 싶다

            //Gun.transform.RotateAround(GunTargetPivot.transform.position, Vector3.left, 100 * Time.deltaTime);

            if(!isGunMoving)
            {
                isGunMoving = true;
                StartCoroutine(delay());
                //photonView.RPC("FireBullet", RpcTarget.All, firePos.position, firePos.forward);
                currentTime = 0;
            }
            //currentTime = 0;

        }

        //Fire2 버튼 누르면 레이를 쏴서 부딪힌 곳에 파편효과 보여주기
        if (Input.GetButtonDown("Fire2") && currentTime > coolTime)
        {
            if (!isGunMoving)
            {
                photonView.RPC("FireBullet2", RpcTarget.All, firePos2.position, firePos2.forward);
                currentTime = 0;
            }

        }
    }



    [PunRPC]
    void FireBullet(Vector3 pos, Vector3 dir)
    {
        //총쏠때 소리나게
        audioSource.Play();
        //bullet 총구앞엑서 나가게
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = pos;
        bullet.transform.forward = dir;
        fireComplete = true;

        //GameManager.instance.SetPlayerTurn();
    }

    [PunRPC]
    void FireBullet2(Vector3 pos, Vector3 dir)
    {
        //총쏠때 소리나게
        audioSource.Play();
        //bullet 총구앞엑서 나가게
        GameObject bullet = Instantiate(bulletFactory2);
        bullet.transform.position = pos;
        bullet.transform.forward = dir;

        //GameManager.instance.SetPlayerTurn();
    }

    //coroutine for implementing couroutines "Sequentially"
    IEnumerator delay()
    {
        yield return StartCoroutine(RotateFunction(GunPivot, 45));
        yield return StartCoroutine(fireUpCoroutine());
        yield return StartCoroutine(RotateBackFunction(GunPivot, 45));
    }

    

    //Gun automatically rotate Up
    IEnumerator RotateFunction(GameObject pivotPos, float degree)
    {
        print("nice1");
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime * 0.5f;
            pivotPos.transform.Rotate(new Vector3(-1, 0, 0), degree * Time.deltaTime);
            //poll.transform.RotateAround(pivotPos.transform.position, new Vector3(-1, 0, 0), degree * Time.deltaTime);

            // If the object has arrived, stop the coroutine
            if (timeSinceStarted >= 0.45f)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    //gun Fire
    IEnumerator fireUpCoroutine()
    {
        photonView.RPC("FireBullet", RpcTarget.All, firePos.position, firePos.forward);
        if (fireComplete)
        {
            fireComplete = false;
            yield break;
        }

        yield return null;

    }

    //Gun automatically rotate Down
    IEnumerator RotateBackFunction(GameObject pivotPos, float degree)
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime * 0.5f;
            pivotPos.transform.Rotate(new Vector3(1, 0, 0),degree * Time.deltaTime);
            //poll.transform.RotateAround(pivotPos.transform.position, new Vector3(1, 0, 0), degree * Time.deltaTime);

            // If the object has arrived, stop the coroutine
            if (timeSinceStarted >= 0.45f)
            {
                isGunMoving = false;
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }



    //[PunRPC]
    //void ShowBulletPieces(Vector3 pos, Vector3 dir)
    //{
    //    GameObject bulletPieces = Instantiate(bulletPiecesFactory);
    //    bulletPieces.transform.position = pos;
    //    bulletPieces.transform.forward = dir;
    //    Destroy(bulletPieces, 2);

    //    audioSource.Play();

    //    //GameManager.instance.SetPlayerTurn();
    //}

    //[PunRPC]
    //void SetPlayerTurn(bool isTurn)
    //{
    //    isMyTurn = isTurn;
    //    turnIcon.enabled = isTurn;
    //}
}
