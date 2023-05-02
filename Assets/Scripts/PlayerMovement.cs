using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10;
    [SerializeField] float jumpForce = 30;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject head,body,leg1,leg2;
    [SerializeField] private LevelGenerator levelGenerator;

    [SerializeField] private GameObject player;
    private bool isWalking;
    private bool isGorundTouch;
    private float startPostionofTrigger;
    private GameObject trigger;
    public float startTime2;
    public float startTime;
    public float elapsedTime;
    [SerializeField] public float lifeTime = 60;

    private void Awake(){
        startTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPostionofTrigger = transform.position.y;
        animator = gameObject.GetComponent<Animator>();
        trigger = GameObject.FindGameObjectWithTag("isFall");
        levelGenerator = GameObject.FindGameObjectWithTag("levelGenerator").GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    // 
    void Update()
    {
        Vector2 inputVector = new Vector2(0,0);
        bool isJump = (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGorundTouch;

        if(Input.GetKey(KeyCode.A)){
           inputVector.x = -1;
           head.GetComponent<SpriteRenderer>().flipX = true;
           body.GetComponent<SpriteRenderer>().flipX = true;
           leg1.GetComponent<SpriteRenderer>().flipX = true;
           leg2.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(Input.GetKey(KeyCode.D)){
           inputVector.x = 1;
           head.GetComponent<SpriteRenderer>().flipX = false;
           body.GetComponent<SpriteRenderer>().flipX = false;
           leg1.GetComponent<SpriteRenderer>().flipX = false;
           leg2.GetComponent<SpriteRenderer>().flipX = false;
           
        }
        if(isJump){
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.Escape)){
            
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0, 0);

        transform.position += moveDir * speed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;
        AnimationController(isJump);
        FollowCharacterUnder(trigger);

    }

    public string countDownTimer(){

        elapsedTime = Time.time - startTime;

        int minutes = (int)((lifeTime - elapsedTime) / 60) % 60;
        int seconds = (int)((lifeTime - elapsedTime) % 60);
        string gameTimerString = string.Format("00:{0:00}:{1:00}", minutes, seconds);
        if (elapsedTime >= lifeTime)
        {
            //gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            //animator.SetBool("died",true);
            //Instantiate(player,new Vector3(gameObject.transform.position.x-4, gameObject.transform.position.y,gameObject.transform.position.z),Quaternion.identity);
            //gameObject.GetComponent<PlayerMovement>().enabled = false;
            //gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            startTime2 = Time.time;

        }
        return gameTimerString;
    }

    public string countUp(){
            
            float elapsedTime2 = Time.time - startTime2;
            int minutes2 = (int)((elapsedTime2) / 60) % 60;
            int seconds2 = (int)((elapsedTime2) % 60);
            string gameTimerString2 = string.Format("-00:{0:00}:{1:00}", minutes2, seconds2);
            return gameTimerString2;
    }

    void FollowCharacterUnder(GameObject go){
        go.transform.position = new Vector3(transform.position.x,startPostionofTrigger-7.78f,1);
        
    }

    void AnimationController(bool jump){
        if(isWalking){
            animator.SetBool("isWalking", true);
        }else{
            animator.SetBool("isWalking", false);
        }
        if(jump){
            animator.SetBool("isJump", true);
        }else{
            animator.SetBool("isJump", false);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isGorundTouch = true;
        startPostionofTrigger = transform.position.y;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "isFall"){
            startPostionofTrigger = transform.position.y;
            trigger.transform.position = new Vector3(gameObject.transform.position.x, startPostionofTrigger - 7.78f, 0);
            levelGenerator.lastEndPosition = new Vector3(transform.position.x-3,transform.position.y-3,0);
            levelGenerator.SpawnLevelPart();
            
        }
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground"){
            isGorundTouch = false;
        }
    }


}
