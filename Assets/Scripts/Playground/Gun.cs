using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public float bulletSpeedModifier = 10f;
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject specialBullet;

    private float cooldown = 0f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        //RotateGun();
    }

    //void RotateGun()
    //{
    //    // Distance from camera to object.  We need this to get the proper calculation.
    //    float camDis = Camera.main.transform.position.y - gameObject.transform.position.y;

    //    // Get the mouse position in world space. Using camDis for the Z axis.
    //    Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));
    //    //gameObject.transform.LookAt(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

    //    Vector3 target = new Vector3(mouse.x, mouse.y, 0);
    //    transform.right = target - transform.position;
    //    //gameObject.transform.LookAt(new Vector3(0, 0, Input.mousePosition.x));

    //    //float AngleRad = Mathf.Atan2(mouse.y - gameObject.transform.position.y, mouse.x - gameObject.transform.position.x);
    //    //float angle = (180 / Mathf.PI) * AngleRad;

    //    //Debug.Log(AngleRad - transform.rotation.z);

    //    //gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    //gameObject.transform.RotateAround(new Vector3(-4, 0, 0), Vector3.forward, AngleRad);
    //}

    public void FireBullet(Vector2 origin, Vector2 target, bool isSpecial)
    {
        if (cooldown < 0)
        {
            animator.SetTrigger("fireGun");
            Vector2 direction = target - origin;
            direction.Normalize();
            GameObject projectile = Instantiate(isSpecial ? specialBullet : bullet, origin, Quaternion.identity);
            projectile.SetActive(true); // needed for for when enemy has learnt
            projectile.GetComponent<Bullet>().InitProps(gameObject, bulletSpeedModifier, direction * bulletSpeedModifier, isSpecial);
            cooldown = animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}
