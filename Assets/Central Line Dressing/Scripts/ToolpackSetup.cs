using UnityEngine;
using Oculus.Interaction;

public class ToolpackSetup : MonoBehaviour
{
    public OVRGrabbable grabInteractable;

    public bool isGrabbed;
    public float triggerPress;

    public GameObject[] tools;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabInteractable = GetComponent<OVRGrabbable>();

        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grabInteractable != null)
        {
            if (grabInteractable.isGrabbed == true)
            {
                Debug.Log(gameObject.name + " is currently grabbed!");

                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.0f)
                {
                    for (int i = 0; i < tools.Length; i++)
                    {
                        tools[i].transform.parent = null;
                    }

                    Destroy(gameObject);

                    Debug.Log("No more " + gameObject.name);
                }
            }
        }

        isGrabbed = grabInteractable.isGrabbed;
        triggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
    }

    
}
