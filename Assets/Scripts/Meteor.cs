using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public delegate void MeteorDelegate();
    public static MeteorDelegate MeteorDown;

    // Update is called once per frame
    protected void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2f);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            PlayerHit(whatIHit);
        } else if (whatIHit.tag == "Laser")
        {
            LaserHit(whatIHit);
        }
    }

    protected void LaserHit(Collider2D hit)
    {
        Destroy(hit.gameObject);
        Destroy(this.gameObject);
    }
    protected void PlayerHit(Collider2D hit)
    {
        Destroy(hit.gameObject);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        if(MeteorDown != null) MeteorDown();
    }
}
