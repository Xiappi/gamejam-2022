using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] float XRebound = 10f;
    [SerializeField] float YRebound = 10f;

    public void KnockbackEntity(Rigidbody2D entity, Rigidbody2D entityToKnockback)
    {
        var direction = Mathf.Abs(entityToKnockback.position.x) - Mathf.Abs(entity.position.x);
        var vector = new Vector2((XRebound + Mathf.Abs(entity.velocity.x)) * Mathf.Sign(direction), YRebound);
        entityToKnockback.velocity = vector;
    }

}
