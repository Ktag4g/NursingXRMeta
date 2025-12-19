using System.Collections;
using UnityEngine;
using Oculus.Interaction;

public class AlcoholSwabScript : MonoBehaviour
{
    public GrabInteractable grabInteractable;
    public SkinnedMeshRenderer packetMesh;

    //private bool packetOpened = true;
    private Vector3 newColliderCenter = new Vector3(-0.07029252f, 1.024455e-08f, 0);
    private Vector3 newColliderSize = new Vector3(0.1439853f, 0.01800332f, 0.01800332f);

    public GameObject packet, swab;

    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        packetMesh.SetBlendShapeWeight(0, slider.value * 100);

        if (slider.value >= 1)
        {
            slider.gameObject.SetActive(false);
            //packetOpened = false;
            StartCoroutine(OpenPacketAnimation());
        }
    }

    IEnumerator OpenPacketAnimation()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 120; i++)
        {
            //swab.transform.position += (slideDirection.transform.position - reference.transform.position) * .2f / 120;
            yield return new WaitForSeconds(1 / 60f);
        }

        yield return new WaitForSeconds(.25f);
        packet.SetActive(false);
        gameObject.transform.position = swab.transform.position;
        gameObject.GetComponent<BoxCollider>().center = newColliderCenter;
        gameObject.GetComponent<BoxCollider>().size = newColliderSize;
        //swab.GetComponent<BoxCollider>().enabled = !swab.GetComponent<BoxCollider>().enabled;
    }
}
