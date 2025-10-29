using UnityEngine;
using Oculus.Interaction;

public class NewTegadermScript : MonoBehaviour
{
    private StepManager stepManager;
    public GrabInteractable grabInteractable;

    //temp?
    public bool triggerIsPressed;

    public GameObject stickyWrapper;
    public GameObject outlineWrapper;

    private bool stickyWrapRemoved;
    private bool outlineWrapRemoved;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find and assign Step Manager
        stepManager = GameObject.Find("Step Manager").GetComponent<StepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Makes sure that the grab interactable of the tegaderm has been assigned 
        if (grabInteractable != null)
        {
            //Checks to see if the tegaderm is being held
            if (grabInteractable.State == InteractableState.Select)
            {
                //If the tegaderm is currently being held, the trigger can be used to perform task
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.0f)
                {
                    //After doing the action once, considers the trigger pressed so that the code is only performed once per press
                    //CLEAN UP: SEE IF THERE IS A WAY TO DO ON ENTER OR DIFFERENT CLEANER WAY TO ONLY PERFORM CODE ONCE
                    if (!triggerIsPressed)
                    {
                        //Checks to see if the sticky wrapper has been removed
                        if (!stickyWrapRemoved)
                        {
                            //If it hasn't, remove the sticky side as the first interaction
                            stickyWrapRemoved = true;
                            Destroy(stickyWrapper);

                            //Update the step manager to mark the step of removing the sticky wrapper side as complete
                            stepManager.UpdateChecklist(5);
                        }
                        else if (!outlineWrapRemoved)
                        {
                            //CLEAN-UP: NOTE THIS IS ONLY UNTIL THE SLIDERS ARE ADDED AND THE USER CAN DETERMINE WHICH WRAPPER CAN BE REMOVED FIRST

                            //If the sticky wrapper has already been removed, then the outline wrapper is removed
                            outlineWrapRemoved = true;
                            Destroy(outlineWrapper);

                            //Checks to see if the tegaderm has been stuck to the catheder area
                            if (stepManager.stepChecklist[6] == false)
                            {
                                //If it hasn't, mark error that the outline wrapper was removed before the tegaderm was put on
                                stepManager.StepOrderError(5);
                            }

                            //Update the step manager to mark the step of removing outline wrapper as completed
                            stepManager.UpdateChecklist(7);
                        }

                        triggerIsPressed = true;
                    }
                }
                else
                {
                    //Resets trigger press variable once the trigger is no longer being pressed 
                    triggerIsPressed = false;
                }
            }
        }
    }
}
