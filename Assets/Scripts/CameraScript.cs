using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //ставим скрипт на камеру и следуем за персонажем .

    private GameObject character;

    //расчитываем место камеры
    private Vector3 s;

    //rotation ß наклон камері , 
    private InputAction lookAction;

    //1 накопленный угол поворота камеры , но вокруг своей позииции
    private float angleH , angleH0; // по горизонтали накопительный и стартовый
	private float angleV , angleV0;   //по вертикали накопительынй и стартовый
    void Start()
    {
        character = GameObject.Find("Character");
        lookAction = InputSystem.actions.FindAction("Look");
        s = this.transform.position - character.transform.position;
		angleH0 = angleH = transform.eulerAngles.y;   // крутим по горизонтали - вокруг оси y
		angleV0 = angleV = transform.eulerAngles.x;   // кручение по вертикали - вокруг оси x
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 lookValue = lookAction.ReadValue<Vector2>();

		var tmpValue = 10f * Time.deltaTime;

		angleH += lookValue.x * tmpValue;
		if (0 < angleV - lookValue.y * tmpValue && angleV - lookValue.y * tmpValue < 90)
		{
			angleV -= lookValue.y * tmpValue;
		}
		this.transform.eulerAngles = new Vector3(angleV, angleH, 0f);
		this.transform.position = character.transform.position + Quaternion.Euler(angleV - angleV0, angleH - angleH0, 0f) * s;


	}
}