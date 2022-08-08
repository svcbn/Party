using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//character controller이용해서 움직이자
//ex1) 만약에 w키를 누르면 camera가 보는 방향으로 가는거고
//ex2) 만약에 w + a 키를 누르면 camera가 보는 방향 대각선으로 가는거고
//스페이스바 누르면 점프



public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    //속력
    public float moveSpeed = 5;
    CharacterController cc;

    //점프파워
    //public float jumpPower = 5;

    //y관련 속력
    float yVelocity;

    //중력
    //float gravity = -20f;

    Vector3 playerPos;
    Quaternion playerRot;

    MeshRenderer mr;
    public Material[] mats;

    public Text nickName;

    //hp bar
    public Slider sliderHp;
    float maxHp = 100;
    float currHp;

    //피격 이펙트
    Image damageEft;

    [SerializeField] float blinkSpeed = 0.1f; //speed of Blinking
    [SerializeField] int blinkCount = 24;  // Count of Blinking(2 times per on/off)
    int currentBlinkCount = 0; // stop blinking when finished
    bool isBlink = false;  //check whether player is blinking(Invincible mode)
    [SerializeField] MeshRenderer playerMesh = null; //player's mesh renderer on/off


    // Start is called before the first frame update
    void Start()
    {
        //Hp세팅
        currHp = maxHp;

        //닉네임 세팅
        nickName.text = photonView.Owner.NickName;

        //MeshRenderer mr = GetComponent<MeshRenderer>();
        mr = GetComponent<MeshRenderer>();

        damageEft = GameObject.Find("Canvas/DamagedEffect").GetComponent<Image>();
        
        if (photonView.IsMine)
        {
            cc = GetComponent<CharacterController>();
            mr.material = mats[0];


        }
        else
        {
            mr.material = mats[1];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Cursor.visible = false;
            }
        }

        if (photonView.IsMine)
        {
            if (!Cursor.visible)
            {
                //키보드 입력 (w, a, s, d)
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                //h,v에 따라서 방향을 구한다
                Vector3 dir = new Vector3(h, 0, v);
                dir.Normalize();

                //카메라가 보는 방향으로 dir 수정
                dir = Camera.main.transform.TransformDirection(dir);

                //점프
                //dir.y = Jump(dir.y);

                //이동하자
                cc.Move(dir * moveSpeed * Time.deltaTime);
            }
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, 0.2f);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerRot, 0.2f);
        }
       

    }

    //float Jump(float dirY)
    //{
    //    if(cc.isGrounded)
    //    {
    //        yVelocity = 0;
    //    }

    //    if(Input.GetButtonDown("Jump"))
    //    {
    //        yVelocity = jumpPower;
    //    }
    //    dirY = yVelocity;
    //    yVelocity += gravity * Time.deltaTime;

    //    return dirY;
    //}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            //위치랑 각도
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        if(stream.IsReading)
        {
            //위치
            playerPos = (Vector3)stream.ReceiveNext();
            //각도
            playerRot = (Quaternion)stream.ReceiveNext();

        }
    }

    public void OnDamaged(float damage)
    {
        photonView.RPC("RPCOnDamaged", RpcTarget.All, damage);
    }


    [PunRPC]
    void RPCOnDamaged(float damage)
    {
        if (!isBlink)
        {
            currHp -= damage;
            if (currHp < 0)
            {
                currHp = 0;
            }
            sliderHp.value = currHp / maxHp;
        }

        //맞은 놈이 나일때만 피격 이펙트를 보여주자 
        if (photonView.IsMine)
        {
            damageEft.enabled = false;
            StopAllCoroutines();
            StartCoroutine(ShowDamagedEft());
        }

        StartCoroutine(BlinkCoroutine());


    }

    //Blinking when player is in Invincible mode 
    IEnumerator BlinkCoroutine()
    {
        isBlink = true;
        while (currentBlinkCount <= blinkCount) //iterate unitl approach
        {
            playerMesh.enabled = !playerMesh.enabled; //mesh On/Off
            yield return new WaitForSeconds(blinkSpeed); // per 1.5 sec
            currentBlinkCount++;
        }

        playerMesh.enabled = true; //mesh On
        currentBlinkCount = 0;
        isBlink = false;
    }


    IEnumerator ShowDamagedEft()
    {
        damageEft.enabled = true;
        yield return new WaitForSeconds(0.5f);
        damageEft.enabled = false;
    }
}
