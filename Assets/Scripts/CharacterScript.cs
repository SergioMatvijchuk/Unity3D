using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class CharacterScript : MonoBehaviour
{

    //скрипт управление персонажем , в данный момент сфрой , а
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    //скрипт забирает из ИнпутСистем свойств 2
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
			//привязываем движение камеры к поворотам

			Vector3 move = Camera.main.transform.forward; // направление взгляда камеры
			move.y = 0.0f;  //проектируем на горизонтальну плоскость ( убираем у и остается плоскость) 
							//если вектор был вертикальным ( взгляд вниз)) 
			if (move == Vector3.zero)
			{
				move = Camera.main.transform.up;
			}

			move.Normalize(); // вытягиваем вектор поле проектирования  , произведение его до единичного вектора


			//делаем так . чтобы шарик крутился лицом туда . куда смотрит камера
			//для етого сохраняем для поворота персонажа 1.
			Vector3 moveForvard = move;

			//в данном месте move - направление постоянного движение 
			//добавляем к нему управление . которое тоже ориентирвоано по камере
			move += moveValue.x * Camera.main.transform.right; // движение по -лево-право , но делаем коррецкии на камеру
			move.y += moveValue.y;  // жвижение верх - вниз на клавишах относительно мировой оси
									//падение
			move.y -= 300f * Time.deltaTime;

			//движение 
			characterController.Move(move * Time.deltaTime * speedFactor);



			//а вот сам поворот персонажа , где мы вращаем его в направлении движения 
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
