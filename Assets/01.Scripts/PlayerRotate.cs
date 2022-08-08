using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerRotate : MonoBehaviourPun, IPunObservable
{
    //회전속도 
    public float rotSpeed = 800;
    //Player의 회전값을 저장 하고 있는 변수 
    Vector3 rotPlayer;
    //Camera의 회전값을 저장 하고 있는 변수
    Vector3 rotCam;

    public Transform trCam;
    Quaternion camRot;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            trCam.GetComponent<Camera>().enabled = true;
            trCam.GetComponent<AudioListener>().enabled = true;
            //초기 회전값 세팅
            rotPlayer = transform.localEulerAngles;
            rotCam = trCam.localEulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

            if (!Cursor.visible)
            {
                // 1. 마우스 상하 좌우 입력 받아서
                //float x = Input.GetAxis("Mouse X");
                //float y = Input.GetAxis("Mouse Y");

                float x = Input.GetAxis("Horizontal");
                //float y = Input.GetAxis("Vertical");


                //2. 그 값으로 각도를 갱신해주고
                rotPlayer.y += x * rotSpeed * Time.deltaTime;
                //rotCam.x -= y * rotSpeed * Time.deltaTime;


                // -80 ~ 80의 값으로 세팅
                //rotCam.x = Mathf.Clamp(rotCam.x, -80, 80);


                //3. 그 값을 Player, Camera세팅
                transform.localEulerAngles = rotPlayer;
                trCam.localEulerAngles = rotCam;
            }

        }
        else
        {
            trCam.rotation = Quaternion.Lerp(trCam.rotation, camRot, 0.2f);
        }

        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(trCam.rotation);
        }
        if(stream.IsReading)
        {
            camRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
