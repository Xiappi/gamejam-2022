using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int currentAmmo;
    public int maxAmmo;
    public GameObject bullet;
    public Transform firePoint;
    public Transform bulletHolder;
    // Start is called before the first frame update
    void Start()
    {
        //MouseFacing();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject copyBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        copyBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * 1000);
        copyBullet.transform.parent = bulletHolder;

        if (currentAmmo > 0)
        {
            currentAmmo--;
        }
    }
}
