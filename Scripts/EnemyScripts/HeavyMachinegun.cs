using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class HeavyMachinegun : Enemy_Minion
{
    public AimConstraint aimConstraint;

    public GameObject ParentObject;
    public GameObject upperStand;
    public GameObject lowerStand;

    public GameObject ShooterEnd;

    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = ParentObject.GetComponent<Animator>();

        Life = 9999;
        Health = 30f;

        player = FindPlayer();
        if (player == null) return;

        BGM_Script = FindObjectOfType<BGMController>();
        AimToPlayer(player);

        if (transform.position.y > 0)
        {
            upperStand.SetActive(false);
        }
        else
        {
            lowerStand.SetActive(false);
        }
    }

    public void MotionChange(string state)

    {
        //animator.SetInteger("Motion", state);
        animator.SetTrigger(state);
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

    public void ShootSmall()
    {
        //SingleShot(HeavyMachineguns[k], 80, attackPrefab[1], playerCharacter, 200, 0, 0);
        SingleShot(ShooterEnd.transform.position, 80, AttackPrefab[0], player, 200, 0, 0);
        MotionChange("ShootSmall");
        //PlaySFX(4);
    }

    public void ShootBig()
    {
        //SingleShot(HeavyMachineguns[k], 80, attackPrefab[1], playerCharacter, 200, 0, 0);
        BasicAttack(ShooterEnd.transform.position, 8, 20f, 6, player, AttackPrefab[1], 200, 0, 0);
        MotionChange("ShootSmall");
        //PlaySFX(4);
    }
}


