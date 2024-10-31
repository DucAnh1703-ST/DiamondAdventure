using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    Animator animator;
    bool broken = true;
    public static int remainRobot;


    AudioSource audioSource, stopClip;
    public AudioClip fixClip, winClip;

    private int robot = 0;
    private int totalRobot;
    [SerializeField] private TextMeshProUGUI robotText;
    private int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stopClip = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        remainRobot = GameObject.FindGameObjectsWithTag("Bot").Length;

        currentLevel = SceneManager.GetActiveScene().buildIndex;
        totalRobot = GameObject.FindGameObjectsWithTag("Bot").Length;
        robotText.text = "Level " + currentLevel + " - Robot fixed: " + robot + "/" + totalRobot;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (!broken)
        {
            return;
        }
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        if (!broken)
        {
            return;
        }
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction; ;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + Time.deltaTime * speed * direction; ;
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
        

    }
    public void Fix()
    {
        stopClip.Stop();
        audioSource.PlayOneShot(fixClip);
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        remainRobot -= 1;
        if (remainRobot <= 0)
        {
            audioSource.PlayOneShot(winClip);
        }
        robot = totalRobot - remainRobot;
        robotText.text = "Level " + currentLevel + " - Robot fixed: " + robot + "/" +  totalRobot;
    }

}