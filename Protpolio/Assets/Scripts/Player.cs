using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 3.0f;
    public float jumpHeight = 4.0f;

    Vector3 inputvec = Vector3.zero;

    bool ground = false;
    public LayerMask layer;
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputvec.x = Input.GetAxis("Horizontal");
        inputvec.z = Input.GetAxis("Vertical");
        inputvec.Normalize();

        Checkground();

        if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigid.AddForce(jumpPower, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        //Vector3 nextVec = inputvec.normalized * moveSpeed * Time.fixedDeltaTime;
        //rigid.MovePosition((Vector3)rigid.position + inputvec);
        if (inputvec != Vector3.zero)
        {
            // 지금 바라보는 방향의 부호 != 나아갈 방향 부호 (정반대 방향 입력들어왔을때 비정상적인 움직임때문에 작성)
            // Sign: 들어온값이 0인지 음수인지 양수인지 판단
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(inputvec.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(inputvec.z))
            {
                transform.Rotate(0, 1, 0);
            }
            // Lerp(현재값, 바라볼방향, )  : 원하는 방향까지 서서히 변화
            transform.forward = Vector3.Lerp(transform.forward, inputvec, rotSpeed * Time.deltaTime);
        }
        //MovePosition(이동할목적지(현재위치+나아갈방향벡터*속도))
        rigid.MovePosition(this.gameObject.transform.position + inputvec * moveSpeed * Time.deltaTime);

    }
    void Checkground()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }
}
