using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Run : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private bool moving = true;

    [SerializeField] private GameObject[] EnemiesAfter;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private string sceneName;
    [SerializeField] private AudioSource mRe;
    [SerializeField] private AudioSource mBG;
    [SerializeField] private AudioSource mClap;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        foreach (GameObject b in EnemiesAfter)
        {
            b.SetActive(false);
        }
        title.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.localScale = new Vector3(1, 1);
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) //Trigger for Collectibles
    {
        if (collision.tag == "Stop")
        {
            rb.velocity = Vector2.zero;
            mClap.Play();
            moving = false;
            anim.SetTrigger("Idle");
            title.enabled = true;
            foreach (GameObject b in EnemiesAfter)
            {
                b.SetActive(true);
            }
        }
    }

    public void Pressedbtn()
    {
            mBG.Stop();
            mRe.Play();
            StartCoroutine(Changes());
    }

    private IEnumerator Changes()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

}
