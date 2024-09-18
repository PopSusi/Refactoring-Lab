using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : Meteor
{
    private int hitCount = 0;

    new void LaserHit(Collider2D hit)
    {
        hitCount++;
        if (hitCount >= 5)
        {
            Destroy(gameObject);
        }
    }
    new void PlayerHit(Collider2D hit)
    {
        Destroy(hit.gameObject);
    }
}
