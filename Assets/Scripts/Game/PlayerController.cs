using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float horizontalForce = 4f;
    public float jumpForceLimit = 15f;
    public float jumpForceIncrement = 0.5f;
    public float jumpForceInitial = 5f;

    private string jumpDirection = "Up";
    private float jumpForce;

    public Text txtForce;
    public Text txtDirection;
    public Text txtScore;
    public Text txtHighScore;
    public Text txtWin;

    private int score = 0;
    private int highScore = 0;

    public Canvas pauseMenu;
    
    private bool landed = true;
    private bool landingPlayed = true;
    private bool alowUnland = false;

    private AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
        // log the game mode
        // Debug.Log(GameSettings.gameMode);
        pauseMenu.gameObject.SetActive(false);
        audios = GetComponents<AudioSource>();
        jumpForce = jumpForceInitial;
        txtForce.text = "Jump Force = " + jumpForce.ToString();
        txtDirection.text = "Jump Direction = " + jumpDirection;
        txtWin.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // use y position do determine player score
        score = (int) gameObject.transform.position.y;
        txtScore.text = "Score = " + score.ToString();
        if(score > highScore){
            highScore = score;
            txtHighScore.text = "High Score = " + highScore.ToString();
        }    
        
        buttonControl();

        // if player velocity x >=0, player is moving right                                                             
        if(gameObject.GetComponent<Rigidbody2D>().velocity.x > 0){
            // flip player sprite to face right                                                             
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        // if player velocity x < 0, player is moving left
        else if(gameObject.GetComponent<Rigidbody2D>().velocity.x < 0){
            // flip player sprite to face left                                                             
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    private void buttonControl(){
        if( gameObject.tag != "Player2"){
            if(gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 && Time.timeScale == 1){
                if (Input.GetKey(KeyCode.A))
                {
                    leftKey();
                }
                if (Input.GetKey(KeyCode.D))
                {
                    rightKey();
                }
                if (Input.GetKey(KeyCode.S))
                {
                    downKey();

                }
                if (Input.GetKey(KeyCode.W))
                {
                    upKey();
                }  
                if (Input.GetKeyUp(KeyCode.W))
                {
                    upKeyReleased();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseKey();
            }
        }
        if( gameObject.tag != "Player1"){
            if(gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 && Time.timeScale == 1){
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    leftKey();
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rightKey();
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    downKey();
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    upKey();
                }  
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    upKeyReleased();
                }
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseKey();
            }
        }
    }

    private void leftKey(){
        startMoving();
        // flip player sprite to face left
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if("Left" == jumpDirection) {
            // translate player to left
            gameObject.transform.Translate(Vector2.left * 2.5f * Time.deltaTime);
        }else{
            jumpDirection = "Left";
        }
        txtDirection.text = "Jump Direction = " + jumpDirection;
    }

    private void rightKey(){
        startMoving();
        // flip player sprite to face right
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        if("Right" == jumpDirection) {
            // translate player to right
            gameObject.transform.Translate(Vector2.right * 2.5f * Time.deltaTime);

        }else{
            jumpDirection = "Right";
        }
        txtDirection.text = "Jump Direction = " + jumpDirection;
    }

    private void downKey(){
        startMoving();
        // flip player sprite to face right
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        jumpDirection = "Up";
        txtDirection.text = "Jump Direction = " + jumpDirection;
    }

    private void upKey(){
        startMoving();
        if(jumpForce < jumpForceLimit){
            jumpForce += jumpForceIncrement + Time.deltaTime;
            //write jump force to screen with 1 decimal place
            txtForce.text = "Jump Force = " + jumpForce.ToString("F1");
        }else{
            jumpForce = jumpForceLimit;
            //write jump force to screen with 1 decimal place
            txtForce.text = "Jump Force = " + jumpForce.ToString("F1");
        }
    }

    private void upKeyReleased(){
        startMoving();
        // log jump force, horizontal force, jump force limit, jump force increment like a table
        Debug.Log("Jump Force\t | Horizontal Force\t | Jump Force Limit\t | Jump Force Increment\t | Initial Jump Force\n\t" + 
        jumpForce + "\t | " + horizontalForce + "\t\t\t | " + jumpForceLimit + "\t\t\t | " + jumpForceIncrement + "\t\t\t | " + jumpForceInitial);
        // Debug.Log("Jump Force = " + jumpForce + " Jump Direction = " + jumpDirection );
        // Debug.Log("Horizontal Force = " + horizontalForce);
        // Debug.Log("Jump Force Limit = " + jumpForceLimit);
        // Debug.Log("Jump Force Increment = " + jumpForceIncrement);

        // jump
        if("Up" == jumpDirection) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if("Left" == jumpDirection) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * horizontalForce + (Vector2.up * jumpForce), ForceMode2D.Impulse);
        }
        if("Right" == jumpDirection) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalForce + (Vector2.up * jumpForce), ForceMode2D.Impulse);
        }
        // flip player sprite to face right
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        jumpForce = jumpForceInitial;
        jumpDirection = "Up";
        txtForce.text = "Jump Force = " + jumpForce.ToString("F1");
        txtDirection.text = "Jump Direction = " + jumpDirection;
        // play jump sound
        audios[1].Play();
        // reset landed and landingPlayed
        // startMoving();
    }

    public void stopMoving(Transform platform){
        if(landed == false && gameObject.GetComponent<Rigidbody2D>().velocity.y <= 1.5 &&
            !(gameObject.transform.position.y < platform.position.y + 
            (gameObject.GetComponent<BoxCollider2D>().size.y / 2) - (platform.lossyScale.y / 2) + gameObject.GetComponent<Rigidbody2D>().velocity.y*0.2f)){
            landed = true;
            // Debug.Log("Stop Moving");
            // put player rigidbody to sleep
            gameObject.GetComponent<Rigidbody2D>().Sleep();

            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // set player y position to platform y position + half of player height (using collider height) + half of platform height (global height)
            Vector2 newPlayerPosition = new Vector2(gameObject.transform.position.x, platform.position.y 
                                                + (gameObject.GetComponent<BoxCollider2D>().size.y / 2) 
                                                - (platform.lossyScale.y / 2));
            gameObject.transform.Translate(newPlayerPosition - (Vector2)gameObject.transform.position);
            
            // awake player rigidbody
            gameObject.GetComponent<Rigidbody2D>().WakeUp();

            if(landingPlayed == false){
                audios[2].Play();
                landingPlayed = true;
            }
        }
        // else{
        //     Debug.Log("Already Stopped Moving"); 
        //     print values used on if
        //     Debug.Log("Player Velocity Y = " + gameObject.GetComponent<Rigidbody2D>().velocity.y);
        //     Debug.Log("Player Position Y = " + gameObject.transform.position.y);
        //     Debug.Log("Platform Position Y = " + platform.position.y);
        //     Debug.Log("Player Height = " + gameObject.GetComponent<BoxCollider2D>().size.y +" | " + gameObject.GetComponent<BoxCollider2D>().size.y / 2);
        //     Debug.Log("Platform Height = " + platform.lossyScale.y +" | " + platform.lossyScale.y / 2);
        //     Debug.Log("Player Position Y >= Platform Position Y + (Player Height / 2) - (Platform Height / 2) = " +
        //     gameObject.transform.position.y +" >= " + platform.position.y + " + " +
        //     (gameObject.GetComponent<BoxCollider2D>().size.y / 2) +" - " + (platform.lossyScale.y / 2) + " + " + (gameObject.GetComponent<Rigidbody2D>().velocity.y*0.2) + " = " +
        //     (platform.position.y + (gameObject.GetComponent<BoxCollider2D>().size.y / 2) - (platform.lossyScale.y / 2) + gameObject.GetComponent<Rigidbody2D>().velocity.y * 0.2));
        // }
    }

    public void startMoving(){
        if(landed == true && landingPlayed == true){
            // Debug.Log("Start Moving");
            alowUnland = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void unland(){
        if(alowUnland == true){
            // Debug.Log("Unland");
            landed = false;
            landingPlayed = false;
            alowUnland = false;
        }else{
            Debug.Log("Already Unlanded");
        }
    }

    private void pauseKey(){
        if(Time.timeScale == 1){
            pauseGame();
        }else{
            resumeGame();
        }
    }

    // pause game
    public void pauseGame(){
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    // resume game
    public void resumeGame(){
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }

    public void restartGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void quitGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    // quando o player colide com algo e landed = true então reproduz o som de pouso, caso landed = false então reproduz o som de colisão
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!landed){
            // Debug.Log("Collided: " + collision.gameObject.name);
            audios[0].Play();
        }
        // se colidir com outro player (Player1 ou Player2) aciona o startMoving
        if(collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2")){
            startMoving();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")){
            // Debug.Log("Landed Trigger: " + collision.gameObject.name);
            stopMoving(collision.gameObject.transform);
        }
    }

    // on stay trigger Finish, show txtWin
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish")){
            txtWin.gameObject.SetActive(true);
        }
    }

    // on exit trigger Finish, hide txtWin
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish")){
            txtWin.gameObject.SetActive(false);
        }
    }   
}
