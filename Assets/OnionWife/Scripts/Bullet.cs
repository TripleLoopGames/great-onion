using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    public Bullet Initialize() {
        ownRigidbody = GetComponent<Rigidbody>();
        timerComponent = GetComponent<TimerComponent>();       
        return this;
    }

    public Bullet Shoot(Vector3 direction, float magnitude, int lifeTime)
    {
        ownRigidbody.AddForce(direction * magnitude, ForceMode.Impulse);
        timerComponent.StartTimer(lifeTime, () => Destroy(gameObject));
        return this;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Enemy")){
           collision.GetComponentInParent<Bunny>().Damage();
           Destroy(gameObject);
        }
    }

    private Rigidbody ownRigidbody;
    private TimerComponent timerComponent;
}
