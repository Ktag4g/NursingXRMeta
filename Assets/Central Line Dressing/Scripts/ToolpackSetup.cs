using UnityEngine;
using Oculus.Interaction;

public class ToolpackSetup : MonoBehaviour
{
    public GrabInteractable grabInteractable;

    public GameObject[] tools;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Puts all of the tools into the tool package until it is opened
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Makes sure that the grab interactable of the tool package has been assigned
        if (grabInteractable != null)
        {
            //Checks to see if the tool package is being held
            if (grabInteractable.State == InteractableState.Select)
            {
                //If the tool package is being held, pressing the trigger opens the package
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.0f)
                {
                    //If the package is opened, takes all of the tools out and destroys the packaging
                    for (int i = 0; i < tools.Length; i++)
                    {
                        tools[i].transform.parent = null;
                    }

                    Destroy(gameObject);
                }
            }
        }
    }

    
}
