using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class HeavyMachinegun : Enemy_Minion
{
    public AimConstraint aimConstraint;
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 9999;
        Health = 30f;

        player = FindPlayer();
        if (player == null) return;

        BGM_Script = FindObjectOfType<BGMController>();
        AimToPlayer(player);
    }

    public void MotionChange(int state)
    {
        animator.SetInteger("Motion", state);
    }

    private void AimToPlayer(GameObject obj, bool face = true)
    {
        if (aimConstraint == null)
        {
            return;
        }
        else
        {
            aimConstraint.constraintActive = false;
            if (aimConstraint.sourceCount > 0)
            {
                aimConstraint.RemoveSource(0);
            }
        }

        ConstraintSource source = new ConstraintSource
        {
            sourceTransform = obj.transform,
            weight = 1.0f
        };
        aimConstraint.AddSource(source);

        if (face == true) aimConstraint.constraintActive = true;
        else aimConstraint.constraintActive = false;

        //aimConstraint.locked = true;
        aimConstraint.rotationAtRest = Vector3.zero;
        aimConstraint.worldUpType = AimConstraint.WorldUpType.SceneUp;
    }
}


