using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public Vector2 turn;

    public float sensitivity = 0.5f;

    public Vector3 deltaMove;

    public float horizontalMove;

    public float verticalMove;

    public float moveSpeed = 2;

    public GameObject player;

    private Animator PLayerAnimator;

    void Start()
    {
        //��������� ������� ����� �� ����� ���� "����"

        Cursor.lockState = CursorLockMode.Locked;

        PLayerAnimator = player.GetComponent<Animator>();
    }
    void Update()
    {
        //�������� ���������� �����

        turn.x += Input.GetAxis("Mouse X") * sensitivity;

        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        //������������ ������� ������ �� ��� Y

        turn.y = Mathf.Clamp(turn.y, -20, 20);

        //��������� ������� ��� ������

        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        //��������� ������� ��� ������

        player.transform.localRotation = Quaternion.Euler(0, turn.x, 0);

       //��������� �������� ��� ������
        
        horizontalMove = Input.GetAxis("Horizontal");

        verticalMove = Input.GetAxis("Vertical");

        deltaMove = new Vector3 (horizontalMove, 0, verticalMove) * moveSpeed * Time.deltaTime;

        player.transform.Translate (deltaMove);

        PlayerControllerAnim();
    }

     void PlayerControllerAnim()
    {
        if (verticalMove > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                PLayerAnimator.SetInteger("Move", 2);
                moveSpeed = 4;
            }
            else
            {
                PLayerAnimator.SetInteger("Move", 1);
                moveSpeed = 2;
            }
        }
        else if (verticalMove < 0)
        {
            PLayerAnimator.SetInteger("Move", -1);
            moveSpeed = 2;
        }
        else
        {
            PLayerAnimator.SetInteger("Move", 0);
        }
    }
}
