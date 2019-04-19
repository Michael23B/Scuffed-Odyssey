using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeDummy : Unit
{
    [SerializeField] GameObject targetObject;
    [SerializeField] float shootCooldown = 2f;

    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > shootCooldown)
        {
            time = 0f;
            FireBullet(new Vector2(transform.position.x, transform.position.y), targetObject.transform.position, false);
        }
    }
}
