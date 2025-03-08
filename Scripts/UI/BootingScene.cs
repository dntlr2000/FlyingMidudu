using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootingScene : MenuParent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        StageLoader("MainMenu");
    }

    // Update is called once per frame

}
