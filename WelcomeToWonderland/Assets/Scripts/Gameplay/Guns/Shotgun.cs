using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : NewtonVR.Example.NVRExampleGun {
    bool cocked = false;
    [SerializeField] GameObject cock; //;)
    Vector2 cockBlock;
    public override void UseButtonDown( ) {
        base.UseButtonDown();

        if ( cocked ) {
            GameObject bullet = GameObject.Instantiate(BulletPrefab);
            bullet.transform.position = FirePoint.position;
            bullet.transform.forward = FirePoint.forward;

            bullet.GetComponent<Rigidbody>().AddRelativeForce(BulletForce);

            AttachedHand.TriggerHapticPulse(500);
        }
    }

    private void Start( ) {
        var temp = cock;
        if(temp == null ) {
            Debug.LogError("Cock is missing from:" + name);
        }
        cockBlock = new Vector2(transform.localPosition.z, transform.localPosition.y);
    }

    private void Update( ) {
        Vector3 temp = new Vector3();
        if ( cock.transform.localPosition.y != cockBlock.y ) temp.y = cockBlock.y;
        if ( cock.transform.localPosition.z != cockBlock.x ) temp.z = cockBlock.x;
        temp.z = cock.transform.position.x;
        cock.transform.position = temp;
    }
}
