using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//앞으로 계속 나간다
//충돌하면 없어지게
//없어지면서 터지는 이펙트 보여주자
//터지는 소리도 내자 

public class BulletUp : MonoBehaviour
{
    //속력
    public float speed = 80;
    //이펙트 프리팹 
    public GameObject exploFactory;
    //Audio Source
    AudioSource audioSource;

    private float gravityMultiplier = 15f;
    Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        //앞으로 계속 a간다
        //transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PlayerMove pm = collision.transform.GetComponent<PlayerMove>();
            if (pm)
            {
                pm.OnDamaged(30);
            }
        }
        //터지는 이펙트
        GameObject explo = Instantiate(exploFactory);
        explo.transform.position = transform.position;
        Destroy(explo, 2);
        //터지는 소리
        audioSource.Play();
        //충돌하면 없어지고
        Destroy(gameObject,0.15f);
    }
}
