using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DissassemblyModel : MonoBehaviour
{
    private Grabbable[] grabbables;
    // Start is called before the first frame update
    void Start()
    {
        grabbables = GetComponentsInChildren<Grabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            ResetModelToInit();
        }
    }

    private void ResetModelToInit()
    {
        foreach (var grabbale in grabbables)
        {
            grabbale.transform.localPosition = Vector3.zero;
            grabbale.transform.localScale = Vector3.zero;
            grabbale.transform.localScale = Vector3.one;
        }
    }
}