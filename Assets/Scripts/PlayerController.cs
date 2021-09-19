using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 500f;
    public int health = 5;
    private int score = 0;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseImage;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(xMove, 0, zMove) * speed * Time.deltaTime;
    }

    void Update()
    {
        if (health == 0)
        {
            winLoseImage.gameObject.SetActive(true);
            winLoseText.text = "Game Over!";
            speed = 0f;
            StartCoroutine(GameOver(3));
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Pickup")
        {
            score++;
            speed += 50f;
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.GetComponent<Collider>().tag == "Trap")
        {
            health--;
            SetHealthText();
        }
        if (other.GetComponent<Collider>().tag == "Goal")
        {
            if (score > 20)
            {
                winLoseImage.gameObject.SetActive(true);
                winLoseImage.color = Color.green;
                winLoseText.color = Color.black;
                winLoseText.text = "You Win!";
                speed = 0f;
                StartCoroutine(GameOver(3));
            }
            else
            {
                StartCoroutine(ChekNumCoins(1.5f));
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    private IEnumerator ChekNumCoins(float delay)
    {
        winLoseImage.gameObject.SetActive(true);
        winLoseText.text = "Still missing coins!";
        yield return new WaitForSeconds(delay);
        winLoseImage.gameObject.SetActive(false);

    }

    private IEnumerator GameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
