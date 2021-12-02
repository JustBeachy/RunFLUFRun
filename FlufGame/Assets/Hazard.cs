using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hazard : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10 + (LoadFluf.score / 150);
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadFluf.start)
            transform.position = new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y);

        if (LoadFluf.clearHazards)
            Destroy(gameObject);

        if (transform.position.x < -15)
            Destroy(gameObject);
    }
}
