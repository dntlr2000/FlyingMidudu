using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechahandParent : MonoBehaviour
{
    public MechaHand hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public MechaHand returnChild() { return hand; }
}
