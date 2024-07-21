using UnityEngine;

public class Ammo : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 lastvalocity;
    int count = 0;
    public static bool Restart;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastvalocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (count < 5 && !collision.gameObject.CompareTag("FIre Ammo"))
        {
            var refleck = Vector2.Reflect(lastvalocity, collision.contacts[0].normal);
            var angle = Mathf.Atan2(refleck.y, refleck.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90;
            rb.velocity = refleck;
            count += 1;
        }
        else
        {
            Destroy(gameObject);
            count = 0;

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("FIre Ammo"); // fire ammo mean which ammo fire by user
            GameObject[] allAmmos = GameObject.FindGameObjectsWithTag("CheckAmmo");
            GameObject[] Zomi = GameObject.FindGameObjectsWithTag("Zomi");

            if (bullets.Length == 1 && allAmmos.Length == 0 && Zomi.Length > 0)
            {
                Restart = true;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var zomi = collision.gameObject;
        var Anim = zomi.GetComponent<Animator>();
        Anim.SetTrigger("ZomiDeath");

        if (collision.gameObject.tag == "Zomi")
        {
            Destroy(collision.gameObject, 1.5f);
        }
    }
}
