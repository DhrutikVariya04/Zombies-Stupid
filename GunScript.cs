using UnityEngine;
using UnityEngine.SceneManagement;

public class GunScript : MonoBehaviour
{
    public GameObject Aim, ammo, Restart, Next;
    // Restart veriable for Page Active And Disactive on unity
    // Next veriable for Page Active And Disactive on unity

    [System.Obsolete]
    void Update()
    {

        if (Ammo.Restart) //  static veriable from ammo script for Restart Page
        {
            Restart.SetActive(true);
        }

        GameObject[] Zomi = GameObject.FindGameObjectsWithTag("Zomi");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("FIre Ammo"); // fire ammo mean which ammo fire by user

        if (Zomi.Length == 0 && bullets.Length == 0)
        {
            Next.SetActive(true);
        }

        if (Ammo.Restart || Next.gameObject.active) return; // For Stop Program :-

        var Spot = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Aim.transform.position = new Vector3(
            Spot.x,
            Spot.y,
            transform.position.z
            );

        var offset = Spot - transform.position;
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] CheckAmmo = GameObject.FindGameObjectsWithTag("CheckAmmo");
            int enemyCount = CheckAmmo.Length; // enemyCount And CheckAmmo is same 
            if (enemyCount > 0)
            {

                var bullet = Instantiate(ammo, transform.position, Quaternion.Euler(0, 0, angle - 90));
                var rd = bullet.GetComponent<Rigidbody2D>();

                var Dis = Camera.main.WorldToScreenPoint(transform.position);
                var myoffset = (Input.mousePosition - Dis).normalized;

                rd.AddForce(myoffset * 900);

                Destroy(GameObject.FindGameObjectWithTag("CheckAmmo")); // For Check Display Ammo Ex :- 5 In Corner

            }
        }
    }

    public void Reset()
    {
        Ammo.Restart = false;
        Restart.SetActive(false);
        int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        SceneManager.LoadScene("Level " + CurrentLevel);
    }

    public void next()
    {
        Next.SetActive(false);
        int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        CurrentLevel += 1;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        SceneManager.LoadScene("Level " + CurrentLevel);
    }
}


