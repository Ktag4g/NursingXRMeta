using System.Collections;
using UnityEngine;
using Oculus.Interaction;
using TMPro;

public class NewTegadermScript : MonoBehaviour
{
    private StepManager stepManager;
    public GrabInteractable grabInteractable;

    //temp?
    private bool triggerIsPressed;

    public Slider stickyWrapperSlider, outlineWrapperSlider;

    public SkinnedMeshRenderer stickyWrapperMesh, outlineWrapperMesh;
    public GameObject tegaderm, deformedTegaderm, deformedOutlineWrapper;
    public SkinnedMeshRenderer deformedOutlineWrapperMesh;

    private bool stickyWrapRemoved, outlineWrapRemoved;

    private bool errorMade = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find and assign Step Manager
        stepManager = GameObject.Find("Step Manager").GetComponent<StepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Makes sure that the grab interactable of the tegaderm has been assigned 
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
                            StartCoroutine(PeelStickyWrap());
                            //Destroy(stickyWrapper);

                            //Update the step manager to mark the step of removing the sticky wrapper side as complete
                            stepManager.UpdateChecklist(5);
                        }
                        else if (!outlineWrapRemoved)
                        {
                            //CLEAN-UP: NOTE THIS IS ONLY UNTIL THE SLIDERS ARE ADDED AND THE USER CAN DETERMINE WHICH WRAPPER CAN BE REMOVED FIRST

                            //If the sticky wrapper has already been removed, then the outline wrapper is removed
                            outlineWrapRemoved = true;
                            //Destroy(outlineWrapper);

                            //Checks to see if the tegaderm has been stuck to the catheder area
                            if (stepManager.stepChecklist[6] == false)
                            {
                                //If it hasn't, mark error that the outline wrapper was removed before the tegaderm was put on
                                stepManager.StepOrderError(5);
                                //Plays the animation of the unused tegaderm being removed
                                StartCoroutine(PeelOutlineWrapUnused());
                            }
                            else
                            {
                                //Plays animation of deformed outline wrapper being removed
                                StartCoroutine(PeelOutlineUsed());
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
        */

        if (grabInteractable.enabled == true)
        {
            stickyWrapperSlider.gameObject.SetActive(true);
            outlineWrapperSlider.gameObject.SetActive(true);
        }

        stickyWrapperMesh.SetBlendShapeWeight(0, stickyWrapperSlider.value * 100);
        stickyWrapperMesh.SetBlendShapeWeight(1, stickyWrapperSlider.value * 100);
        stickyWrapperMesh.SetBlendShapeWeight(2, stickyWrapperSlider.value * 100);

        if (stickyWrapperSlider.value >= 1)
        {
            stickyWrapperSlider.gameObject.SetActive(false);

            //Update the step manager to mark the step of removing the sticky wrapper side as complete
            stepManager.UpdateChecklist(5);

            stickyWrapperMesh.enabled = false;
        }

        //Checks to see if the tegaderm has been stuck to the catheder area
        if (stepManager.stepChecklist[6] == false)
        {
            if (errorMade == false)
            {
                //If it hasn't, mark error that the outline wrapper was removed before the tegaderm was put on
                stepManager.StepOrderError(5);
                errorMade = true;
            }

            //Plays the animation of the unused tegaderm being removed
            outlineWrapperMesh.SetBlendShapeWeight(1, outlineWrapperSlider.value * 100);
            outlineWrapperMesh.SetBlendShapeWeight(2, outlineWrapperSlider.value * 100);
            //outlineWrapperMesh.SetBlendShapeWeight(3, outlineWrapperSlider.value * 10);

            if (outlineWrapperSlider.value >= 1)
            {
                outlineWrapperSlider.gameObject.SetActive(false);

                //Disables outline wrapper and hides it
                outlineWrapperMesh.enabled = false;

                //Update the step manager to mark the step of removing outline wrapper as completed
                stepManager.UpdateChecklist(7);
            }
        }
        else
        {
            //Plays animation of deformed outline wrapper being removed
            deformedOutlineWrapperMesh.SetBlendShapeWeight(1, outlineWrapperSlider.value * 100);
            deformedOutlineWrapperMesh.SetBlendShapeWeight(2, outlineWrapperSlider.value * 10);

            if (outlineWrapperSlider.value >= 1)
            {
                outlineWrapperSlider.gameObject.SetActive(false);

                //Disables outline wrapper and hides it
                deformedOutlineWrapperMesh.enabled = false;

                //Update the step manager to mark the step of removing outline wrapper as completed
                stepManager.UpdateChecklist(7);
            }
        }


        //When the new tegaderm is placed on the arm, change the tegaderm from its unused state to form it around the arm
        //Checks to see if the sticky wrapper has been taken off before it sticks onto the arm
        if (stepManager.stepChecklist[6] == true)
        {
            //Hides flat/unsused tegaderm and shows deformed tegaderm
            tegaderm.SetActive(false);
            deformedTegaderm.SetActive(true);

            //deformedTegaderm.transform.position = new Vector3(0.282f, 0.16f, 0.015f);
        }
    }

    IEnumerator PeelStickyWrap()
    {
        for (int i = 0; i < 100; i++)
        {
            stickyWrapperMesh.SetBlendShapeWeight(0, i);
            stickyWrapperMesh.SetBlendShapeWeight(1, i);
            stickyWrapperMesh.SetBlendShapeWeight(2, i);
            yield return new WaitForSeconds(1 / 120f);
        }
        yield return new WaitForSeconds(.25f);
        StartCoroutine(RemoveStickyWrap());
    }
    IEnumerator RemoveStickyWrap()
    {
        for (int i = 0; i < 60; i++)
        {
            stickyWrapperMesh.SetBlendShapeWeight(3, 10f * i / 60);
            yield return new WaitForSeconds(1 / 60f);
        }
        yield return new WaitForSeconds(.25f);
        stickyWrapperMesh.enabled = false;
    }

    IEnumerator PeelOutlineWrapUnused()
    {
        for (int i = 0; i < 30; i++)
        {
            outlineWrapperMesh.SetBlendShapeWeight(1, 100f * i / 30);
            yield return new WaitForSeconds(1 / 60f);
        }
        for (int i = 0; i < 30; i++)
        {
            outlineWrapperMesh.SetBlendShapeWeight(2, 100f * i / 30);
            yield return new WaitForSeconds(1 / 60f);
        }
        for (int i = 0; i < 30; i++)
        {
            outlineWrapperMesh.SetBlendShapeWeight(3, 10f * i / 30);
            yield return new WaitForSeconds(1 / 60f);
        }
        yield return new WaitForSeconds(.25f);
        outlineWrapperMesh.enabled = false;
    }
    IEnumerator PeelOutlineUsed()
    {
        for (int i = 0; i < 60; i++)
        {
            deformedOutlineWrapperMesh.SetBlendShapeWeight(1, 100f * i / 60);
            yield return new WaitForSeconds(1 / 60f);
        }
        for (int i = 0; i < 30; i++)
        {
            deformedOutlineWrapperMesh.SetBlendShapeWeight(2, 10f * i / 30);
            yield return new WaitForSeconds(1 / 60f);
        }
        yield return new WaitForSeconds(.25f);
        deformedOutlineWrapperMesh.enabled = false;
    }
}
