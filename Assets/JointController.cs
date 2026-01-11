using NUnit.Framework;
using UnityEngine;

public class JointController : MonoBehaviour
{
    public Rigidbody body;
    public CharacterJoint joint;
    private void OnTriggerEnter(Collider other)
    {
        var t = other.GetComponent<Telega>();
        if (t != null)
        {
            body = t.GetComponent<Rigidbody>();
        }
    }


    public void On()
    {
        if (body != null)
            joint.connectedBody = body;
    }
    public void Off()
    {
        joint.connectedBody = null;
    }
}
