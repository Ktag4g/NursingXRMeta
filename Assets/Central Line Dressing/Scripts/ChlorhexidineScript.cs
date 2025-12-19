using System.Collections;
using UnityEngine;
using Oculus.Interaction;

public class ChlorhexidineScript : MonoBehaviour
{    
    private StepManager stepManager;
    public GrabInteractable grabInteractable;

    public SkinnedMeshRenderer chlorhexadineMesh;

    private bool isActivated;

    void Start()
    {
        //Find and assign Step Manager
        stepManager = GameObject.Find("Step Manager").GetComponent<StepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Makes sure that the grab interactable of the applicator has been assigned 
        if (grabInteractable != null)
        {
            //Checks to see if the chlorhexidine applicator is being held
            if (grabInteractable.State == InteractableState.Select)
            {
                //If the chlorhexidine applicator is currently being held, the trigger can be used to squeeze the tabs
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.0f)
                {
                    //Checks to see if the applicator has already been activated so its only activated once
                    if (isActivated == false)
                    {
                        //Activates the applicator so the medication can be applied
                        isActivated = true;

                        //Triggers the animation of the chlorhexidine being triggered
                        StartCoroutine(TriggerChlorhexidineAnim());

                        //Updates the step manager to mark the step as complete
                        stepManager.UpdateChecklist(2);
                    }

                }
            }
        }
    }

    //Animates the chlorhexidine being triggered using blend shapes
    IEnumerator TriggerChlorhexidineAnim()
    {
        for (int i = 1; i <= 15; i++)
        {
            chlorhexadineMesh.SetBlendShapeWeight(0, 100 * i / 15f);
            yield return new WaitForSeconds(1 / 60f);
        }

        for (int i = 15; i >= 0; i--)
        {
            chlorhexadineMesh.SetBlendShapeWeight(0, 100 * i / 15f);
            yield return new WaitForSeconds(1 / 60f);
        }
    }
}
