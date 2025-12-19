using System.Collections;
using UnityEngine;

public class PatientStatusCheck : MonoBehaviour
{
    private StepManager stepManager;
    public GameObject chlorMark;

    public bool isDrying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Connect Step Manager
        stepManager = GameObject.Find("Step Manager").GetComponent<StepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Checks if the Chlorhexadine Applicator is used on the area
        if (other.name == "Chlorhexadine Applicator")
        {
            //Checks to see if the user has taken off the old tegaderm
            if (stepManager.stepChecklist[0] == true)
            {
                //Checks to see if the user activated the applicator by squeezing the tabs
                if (stepManager.stepChecklist[2] == true)
                {
                    //If the applicator was activated, the solution is applied to the area and that step is completed
                    stepManager.UpdateChecklist(3);

                    //Once the chlorhexadine is applied, start a timer that would give it the time to dry
                    isDrying = true;
                    StartCoroutine(DryChlorhexadine());
                }
                else
                {
                    //If the applicator has not been activated, report error in the step order
                    stepManager.StepOrderError(2);
                }
            }
            else
            {
                //If the old tegaderm has not been taken off, report the error in step order
                stepManager.StepOrderError(0);
            }
        }

        //Checks to see if the catheter area has chlorhexidine that is actively drying (to make sure the drying is not disturbed)
        if (isDrying)
        {
            chlorMark.SetActive(true);

            if (other.name == "Gauze_Large" || other.name == "Gauze_Small")
            {
                //If the user dries the chlorhexadine with the gauze instead of air drying, mark error
                stepManager.StepOtherError(0);
                isDrying = false;
            }

            if (other.name == "Alcohol Swab Package")
            {
                //If the user dries the chlorhexadine with the gauze instead of air drying, mark error
                stepManager.StepOtherError(1);
                isDrying = false;
            }

            if (other.name == "Tegaderm")
            {
                //If the user puts the new tegaderm on before the chlorhexidine finishes drying, mark error
                stepManager.StepOtherError(2);
                isDrying = false;
            }
        }

        if (other.name == "Tegaderm")
        {
            //Checks to see if the sticky wrapper has been removed from the new tegaderm
            if (stepManager.stepChecklist[5] == true)
            {
                //If it has, put the new tegaderm on the arm
                //FIGURE OUT HOW TO PUT VISUAL ON VISUALLY
                
                //Mark step of sticking tegaderm on arm as completed
                stepManager.UpdateChecklist(6);

                //Check for all other order errors related to putting on tegaderm
                if (stepManager.stepChecklist[0] == false)
                {
                    //If the new tegaderm was put on before the old tegaderm was taken off, mark error
                    stepManager.StepOrderError(1);
                }
                if (stepManager.stepChecklist[3] == false)
                {
                    //If the new tegaderm was put on before the chlorhexidine was applied, mark error
                    stepManager.StepOrderError(3);
                }
            }
            else
            {
                //If it hasn't, mark an error that the user tried to stick the tegaderm on without exposing the sticky side
                stepManager.StepOrderError(4);
            }
        }
    }

    IEnumerator DryChlorhexadine()
    {
        //Gives time for chlorhexadine to dry
        yield return new WaitForSeconds(2);
        //Checks to see if the drying process has been interupted by the user
        if (isDrying)
        {
            //If left to dry untouched, the step is marked as completed
            stepManager.UpdateChecklist(4);
            isDrying = false;
            chlorMark.SetActive(false);
        }
    }
}
