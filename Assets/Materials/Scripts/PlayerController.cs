using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour
{

	public float rotationSpeed;
	public float walkSpeed;
	public float runSpeed;
    public float startWater;
	public static float initialWater;
	//current water:
	public static float water;
	//private float water;



	public GameObject winImage;
	public Animator	animatorController;
	public Scrollbar waterBar;
	public GameObject waterBarImage;

    //private Rigidbody rb;
	private CharacterController characterController;
	private Quaternion targetRotation;

    //booleans for objectives
    public bool obj1 = false;
    public bool obj2 = false;
    public bool obj3 = false;




    void Start()
    {
		initialWater = startWater;
		water = initialWater;

		//waterBarImage.position = new Vector3(0,0,0);

		//rb = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
		winImage.GetComponent<RawImage>().enabled = false;
		//waterBarImage.GetComponent<Image>().transform.position = new Vector3(332,31,0);
		animatorController = GetComponent<Animator> ();
		//waterBar = GetComponent<Scrollbar> ();
		waterBar.size = 1;
    }

    void FixedUpdate()
    {
		Vector3 input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));


		if (input != Vector3.zero) {
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

			animatorController.SetBool ("isWalking", true);


			if (Input.GetButton ("Run") && water > 0) {

				water -= 1;
				waterBar.size = (float)water / initialWater;
				print (water.ToString ());

				animatorController.SetBool ("isRunning", true);


			}
			else 
			{
				animatorController.SetBool ("isRunning", false);
			}

			if (Input.GetButton ("Crawl")) {
				animatorController.SetBool ("isCrawling", true);
			} else 
			{
				animatorController.SetBool ("isCrawling", false);
			}
		

		
		} else 
		{
			animatorController.SetBool ("isWalking", false);
			animatorController.SetBool ("isRunning", false);
			animatorController.SetBool ("isCrawling", false);
		}



		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = input;

//		movement = Camera.main.WorldToScreenPoint (movement);

//		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
//			movement = transform.forward;
//		}
//		else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
//			movement = transform.right;
//		}

		movement *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.z) == 1) ? .7f : 1;
		if (Input.GetButton ("Run") && water > 0) {
			movement *= runSpeed;
		} else 
		{
			movement *= walkSpeed;
		}
		//movement *= () ? runSpeed : walkSpeed;

//		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow)) {
			movement += Vector3.up * -8 ;
//		}
//		else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.LeftArrow)) {
//			movement -= Vector3.up * 8 * Time.deltaTime;
//		}

		characterController.Move (movement* Time.deltaTime);

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(movement * walkSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obj1")){
            obj1 = true;
        }
        if (other.gameObject.CompareTag("obj2")){
            obj2 = true;
        }
        if (other.gameObject.CompareTag("obj3")){
            obj3 = true;
        }
        if (other.gameObject.CompareTag("Door") && obj1 && obj2 && obj3)
		{	
			print ("YOU ESCAPED!!"); 
			winImage.GetComponent<RawImage>().enabled = true;
            //SceneManager.LoadScene("levelname");
        }
    }

    void OnTriggerStay (Collider other)
    {

    }
}