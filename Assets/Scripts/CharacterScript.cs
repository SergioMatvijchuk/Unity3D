using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class CharacterScript : MonoBehaviour
{

    //������ ���������� ���������� , � ������ ������ ����� , �
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    //������ �������� �� ����������� ������� 2
    private InputAction moveAction;
    private CharacterController characterController;

    private float speedFactor = 10f;

    private Animator animator;
    bool isMoving = false;



	void Start()
    {
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;
        }
        if (isMoving)
        {
			Vector2 moveValue = moveAction.ReadValue<Vector2>();
			//����������� �������� ������ � ���������

			Vector3 move = Camera.main.transform.forward; // ����������� ������� ������
			move.y = 0.0f;  //����������� �� ������������� ��������� ( ������� � � �������� ���������) 
							//���� ������ ��� ������������ ( ������ ����)) 
			if (move == Vector3.zero)
			{
				move = Camera.main.transform.up;
			}

			move.Normalize(); // ���������� ������ ���� ��������������  , ������������ ��� �� ���������� �������


			//������ ��� . ����� ����� �������� ����� ���� . ���� ������� ������
			//��� ����� ��������� ��� �������� ��������� 1.
			Vector3 moveForvard = move;

			//� ������ ����� move - ����������� ����������� �������� 
			//��������� � ���� ���������� . ������� ���� ������������� �� ������
			move += moveValue.x * Camera.main.transform.right; // �������� �� -����-����� , �� ������ ��������� �� ������
			move.y += moveValue.y;  // �������� ���� - ���� �� �������� ������������ ������� ���
									//�������
			move.y -= 300f * Time.deltaTime;

			//�������� 
			characterController.Move(move * Time.deltaTime * speedFactor);



			//� ��� ��� ������� ��������� , ��� �� ������� ��� � ����������� �������� 
			this.transform.forward = moveForvard;  // 
			if (this.transform.position.y -
						   Terrain.activeTerrain.SampleHeight(this.transform.position) > 1.5f)
			{
				animator.SetInteger("MoveState", 2);

			}
			else
			{
				animator.SetInteger("MoveState", 1);
			}

		}
        else
        {
            animator.SetInteger("MoveState", 0);
        } 


		
	}
}
