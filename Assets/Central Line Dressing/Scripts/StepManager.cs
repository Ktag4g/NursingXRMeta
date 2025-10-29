using UnityEngine;
using TMPro;

public class StepManager : MonoBehaviour
{
    /* Steps:
     * 1. Open supplies package (ensure things stay sterile)
     * 2. Peel off old tegaderm. Make sure catheter stays secure in doing so.
     * 3. Grab chlorhexadine applicator, squeeze "wing" tabs until they release the chlorhexadine into the sponge.
     * 4. Apply chlorhexadine using applicator to area around catheter. DO NOT BLOW ON OR WIPE CLEAN, LET IT AIR DRY!
     * 5. Grab new tegaderm, peel of wrapping on the sticky side.
     * 6. Stick tegaderm over the catheter area.
     * 7. Peel off outline wrapping from tegaderm.
     * 
     * 
     * X[0] Peel off old tegaderm.
     * X[1] Keeping catheter secure while peeling off old tegaderm.
     * X[2] Squeezing "wing" tabs of chlorhexadine applicator until they release the chlorhexadine into the sponge.
     * X[3] Applying chlorhexadine to the area around the catheter.
     * X[4] Letting the chlorhexadine air dry.
     * X[5] Peeling off wrapping covering sticky side of new tegaderm.
     * X[6] Sticking tegaderm to catheter area.
     * X[7] Peeling off outline wrapping from tegaderm.
     */

    public bool[] stepChecklist = { false, false, false, false, false, false, false, false };

    private readonly string[] stepsText =
    {
        "peel off old tegaderm.",
        "keep catheter secure while peeling off old tegaderm.",
        "squeeze 'wing' tabs of chlorhexadine applicator until they release the chlorhexadine into the sponge.",
        "apply chlorhexadine to the area around the catheter.",
        "let the chlorhexadine air dry.",
        "peel off wrapping covering sticky side of new tegaderm.",
        "stick tegaderm to catheter area.",
        "peel off outline wrapping from tegaderm."
    };
    
    private readonly string[] orderErrorText = 
    {
        "Order Error: Applied chlorhexadine before removing old tegaderm",
        "Order Error: Put on new tegaderm before removing old tegaderm",
        "Order Error: Attempted to apply chlorhexadine without activating the applicator",
        "Order Error: Put on the new tegaderm without applying chlorhexadine",
        "Order Error: Attempted to put on new tegaderm without exposing sticky side first",
        "Order Error: Peeled off outline wrapper of new tegaderm without putting it on first"
    };

    public static readonly string[] miscErrorText = 
    {
        "Misc Error: You did not allow the chlorhexadine to air dry (don't dry with the gauze).",
        "Misc Error: You used the alcohol swab to disinfect the catheter area instead of the chlorhexadine applicator.",
        "Misc Error: You did not remove the old tegaderm before disinfecting the site."
    };

    public TextMeshPro stepMenuText;

    public void UpdateChecklist(int step)
    {
        stepChecklist[step] = true;

        stepMenuText.text += stepsText[step] + "\n";
    }

    public void StepOrderError(int error)
    {
        //Keeps track of the order in which stepChecks were triggered.
        /* Order Checks
         * [0] in FAILED_ORDER_TEXT: Doing [3] before [0]
         * [1] in FAILED_ORDER_TEXT: Doing [6] before [0]
         * [2] in FAILED_ORDER_TEXT: Doing [3] before [2]
         * [3] in FAILED_ORDER_TEXT: Doing [6] before [3]
         * [4] in FAILED_ORDER_TEXT: Doing [6] before [5]
         * [5] in FAILED_ORDER_TEXT: Doing [7] before [6]
         */

        switch(error)
        {
            //Applied chlorhexadine before removing old tegaderm
            case 0:
                Debug.Log("Order Error: Applied chlorhexadine before removing old tegaderm");
                //Mark Error
                stepMenuText.text += orderErrorText[0] + " \n";
                break;
            
            //Put on new tegaderm before removing old tegaderm
            case 1:
                Debug.Log("Order Error: Put on new tegaderm before removing old tegaderm");
                //Mark Error
                stepMenuText.text += orderErrorText[1] + " \n";
                break;
            
            //Attempted to apply chlorhexadine without activating the applicator
            case 2:
                Debug.Log("Order Error: Attempted to apply chlorhexadine without activating the applicator");
                //Mark Error
                stepMenuText.text += orderErrorText[2] + " \n";
                break;
            
            //Put on the new tegaderm without applying chlorhexadine
            case 3:
                Debug.Log("Order Error: Put on the new tegaderm without applying chlorhexadine");
                //Mark Error
                stepMenuText.text += orderErrorText[3] + " \n";
                break;

            //Attempted to put on new tegaderm without exposing sticky side first
            case 4:
                Debug.Log("Order Error: Attempted to put on new tegaderm without exposing sticky side first");
                //Mark Error
                stepMenuText.text += orderErrorText[4] + " \n";
                break;

            //Peeled off outline wrapper of new tegaderm without putting it on first
            case 5:
                Debug.Log("Order Error: Peeled off outline wrapper of new tegaderm without putting it on first");
                //Mark Error
                stepMenuText.text += orderErrorText[5] + " \n";
                break;
        }
    }

    public void StepOtherError(int error)
    {
        //Mistakes from Other Prop Use
        /* [0] Using the gauze to dry off the catheter area.
         * [1] Using the alcohol swab the disinfect the area instead of the chlorahexadine applicator.
         * [2] Put new tegaderm on before chlorhexadine finished drying 
         */

        switch (error)
        {
            case 0:
                //Used gauze to dry chlorhexadine
                Debug.Log("Misc Error: Used gauze to dry chlorhexadine");
                stepMenuText.text += miscErrorText[0] + " \n";
                break;
            case 1:
                //Used alcohol swab to disinfect catheter area
                Debug.Log("Misc Error: Used alcohol swab to disinfect catheter area");
                stepMenuText.text += miscErrorText[1] + " \n";
                break;
            case 2:
                //Put new tegaderm on before chlorhexadine finished drying
                Debug.Log("Misc Error: Put new tegaderm on before chlorhexadine finished drying");
                stepMenuText.text += miscErrorText[2] + " \n";
                break;
        }
    }

    void DisplayChecklist()
    {
        for (int i = 0; i < stepChecklist.Length; i++)
        {
            if (stepChecklist[i] == true)
            {
                stepMenuText.text += "You did " + stepsText[i];
            }
            else
            {
                stepMenuText.text += "You did not " + stepsText[i];
            }
        }
    }
}
