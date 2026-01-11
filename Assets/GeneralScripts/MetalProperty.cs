using UnityEngine;
using UnityEngine.UI;

public class MetalProperty : MonoBehaviour
{
    public Transform Magnet;
    public float Force = 10;
    public Vector3 offsetCamera = new Vector3(0, 0.015f, -0.04f);
    GameObject cameraObj;
    Camera camera;
    void Start()
    {
        cameraObj = Instantiate(new GameObject(), transform);
        cameraObj.transform.localPosition = offsetCamera;
        camera = cameraObj.AddComponent<Camera>();


        try
        {
            camera.targetTexture = (RenderTexture)GameObject.FindWithTag("ItemRender").GetComponent<RawImage>().texture;
        }

        catch
        {
        }
        camera.enabled = false;


        cameraObj.SetActive(false);
        var collider = GetComponent<Collider>();
        var rb = GetComponent<Rigidbody>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
        if (rb == null)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().useGravity = false;

        }
        else
        if (rb.useGravity == true)
        {
            rb.useGravity = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Magnet == null)
        {
            cameraObj.SetActive(false);
        }
        else
        {

            if (camera.targetTexture != null)
            {




                cameraObj.SetActive(true);
            }
                transform.position = Vector3.MoveTowards(transform.position, Magnet.position, Force / Vector3.Distance(transform.position, Magnet.position) * Time.deltaTime);

            
        }

    }
}
