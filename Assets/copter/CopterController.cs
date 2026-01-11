using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CopterController : PersonController
{
    public List<GameObject> Blades;
    public Transform VectorForward;
    public GameObject LightLookObject;
    public float MaxZRotate;
    public float MinZRotate;
    public float MaxXRotate;
    public float MinXRotate;
    public bool IsMagnitWork;
    public Magnit Magnit;
    public GameObject Indicator;
    public float forceTakt;
    [SerializeField] private float forwardVelocity;
    public Transform copterRender;
    public float Y;
    public float Speed;
    

    public float MaxYUp = 11.3f;
    public float ForwardVelocity
    {
        get => forwardVelocity;
        set
        {
            if (value > MaxForwardVelocity)
                forwardVelocity = MaxForwardVelocity;
            else if (value < MinForwardVelocity)
                forwardVelocity = MinForwardVelocity;
            else forwardVelocity = value;
        }
    }
    public float MaxForwardVelocity;
    public float MinForwardVelocity;
    public bool RunState;
    public float sensitivity = 15f;
    public float mouseX;
    public Rigidbody rb;
    void Start()
    {
        Y = transform.position.y;
        //  CameraController.GetComponent<CameraController>().sensitivity = sensitivity;
        Blades.ForEach(obj => obj.transform.LookAt(LightLookObject.transform));
        //Blades.Where(obj => obj != null)
        //       .Select(obj => obj.GetComponent<Anima>())
        //       .Where(renderer => renderer != null)
        //       .ToList()
        //       .ForEach(renderer => renderer.enabled = true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsMagnitWork = !IsMagnitWork;
            if (IsMagnitWork)
            {
                Magnit.On();
                Indicator.SetActive(true);
            }
            else
            {
                Magnit.Off();
                Indicator.SetActive(false);
            }
        }
        //  Blades.ForEach(obj => obj.transform.LookAt(LightLookObject.transform));
        if (RunState)
        {
            // rb.AddForce (transform.up * ForwardVelocity);  realistic, but inconvenient
            //  print($"{transform.rotation.eulerAngles.x}, {MaxXRotate}");

            //if (Mathf.Abs(Mathf.Abs(transform.rotation.eulerAngles.x) - Mathf.Abs(MaxXRotate)) < 1)
            //{
            //    print("Предел");
            //    rb.linearVelocity = VectorForward.forward * Mathf.Abs( ForwardVelocity);
            //    rb.useGravity = false;
            //}
            //else if(Mathf.Abs(360 - (Mathf.Abs(transform.rotation.eulerAngles.x) + Mathf.Abs(MaxXRotate))) < 1)
            //{
            //    print("Предел");
            //    rb.linearVelocity = -VectorForward.forward * Mathf.Abs(ForwardVelocity) ;
            //    rb.useGravity = false;
            //}
            //else
            //{
            //    rb.useGravity = true;
            //    rb.linearVelocity = (transform.up * ForwardVelocity);
            //}

        }
    }
    private void FixedUpdate()
    {
        InputControl();
    }
    void InputControl()
    {
        var z = Input.GetAxis("Horizontal");
        float smoothZRot = Mathf.Lerp(MinZRotate, -z * MaxZRotate, 1f);

        var x = Input.GetAxis("Vertical");
        float smoothXRot = Mathf.Lerp(MinXRotate, x * MaxXRotate, 1f);
        //  float smoothXRot = transform.rotation.x + x * 5;

        if (!Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity;


        }

        //var mouseX += Input.GetAxis("Mouse X") * sensitivity;



        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        copterRender.transform.rotation = Quaternion.Euler(smoothXRot, mouseX, smoothZRot);

        if (Input.GetKey(KeyCode.Space) && transform.localPosition.y <MaxYUp)
        {
            ForwardVelocity += forceTakt * Time.fixedDeltaTime;
            Y = forceTakt;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            ForwardVelocity -= forceTakt * Time.fixedDeltaTime;
            Y = -forceTakt;
        }
   
        else
        {
            Y = 0;
        }

        print(VectorForward.forward);
        var speedUp = Input.GetKeyDown(KeyCode.LeftShift) ? 2 : 1;
        //    transform.position = new Vector3(transform.position.x, Y, transform.position.z) + Vector3.forward * z * Time.fixedDeltaTime * Speed + Vector3.right * x * Time.fixedDeltaTime * Speed;
        var _movementVector = transform.right * z * Speed + transform.forward * x * Speed + Vector3.up * Y * speedUp;

        rb.linearVelocity = (_movementVector);
        //  transform.position = new Vector3(transform.position.x, Y, transform.position.z);
    }


}
