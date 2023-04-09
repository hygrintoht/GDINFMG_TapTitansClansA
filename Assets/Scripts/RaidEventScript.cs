using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] 
public enum BodyPart 
{
    Head, 
    RightShoulder, 
    LeftShoulder, 
    Torso, 
    RightArm, 
    LeftArm, 
    RightLeg, 
    LeftLeg 
};

public class RaidEventScript : MonoBehaviour
{
    void Update()
    {
        

        if (timer > 0 && isAttacking)
        {
            timer -= Time.deltaTime;
        }

        if ((timer <= 0 && isAttacking) || healthDmg <= 0)
        {
            isAttacking = false;
            RaidEventEnd();
        }
    }

    bool isAttacking = false;
    float timer = 0;
  
  // note: health(Max/Dmg) = health sum of all parts (only used for UI health bar and not to be posted)

  //current hp                  max hp                      damage delt in attack
    int health = 0;             int healthMax = 0;          int healthDmg = 0;

    int headHP = 0;             int headHPMax = 0;          int headHPDmg = 0;    
    int rightShoulderHP = 0;    int rightShoulderHPMax = 0; int rightShoulderHPDmg = 0;
    int leftShoulderHP = 0;     int leftShoulderHPMax = 0;  int leftShoulderHPDmg = 0;
    int torsoHP = 0;            int torsoHPMax = 0;         int torsoHPDmg = 0;
    int rightArmHP = 0;         int rightArmHPMax = 0;      int rightArmHPDmg = 0;
    int leftArmHP = 0;          int leftArmHPMax = 0;       int leftArmHPDmg = 0;
    int rightLegHP = 0;         int rightLegHPMax = 0;      int rightLegHPDmg = 0;
    int leftLegHP = 0;          int leftLegHPMax = 0;       int leftLegHPDmg = 0;

    public void RaidEventStart()
    {
        //  GET current health and max health of parts
        
        timer = 30.0f; // time to deal damage
        isAttacking = true;
    }

    public void RaidEventEnd()
    {
        //  POST damage delt of parts
    }


    // for simplicity sake damage will be 2 (if the part still has hp)
    // or 1 (if the part has no hp) (it supposed to be a multiplier based on player level/stage)
    // there must be a better way to do this (i think)
    public void Damage(int bodyPart)
    {
        if (isAttacking)
        {
            switch ((BodyPart)bodyPart)
            {
                case BodyPart.Head:
                    headHPDmg += headHP > headHPDmg ? 2 : 1;
                    break;
                case BodyPart.RightShoulder:
                    rightShoulderHPDmg += rightShoulderHP > rightShoulderHPDmg ? 2 : 1;
                    break;
                case BodyPart.LeftShoulder:
                    leftShoulderHPDmg += leftShoulderHP > leftShoulderHPDmg ? 2 : 1;
                    break;
                case BodyPart.Torso:
                    torsoHPDmg += torsoHP > torsoHPDmg ? 2 : 1;
                    break;
                case BodyPart.RightArm:
                    rightArmHPDmg += rightArmHP > rightArmHPDmg ? 2 : 1;
                    break;
                case BodyPart.LeftArm:
                    leftArmHPDmg += leftArmHP > leftArmHPDmg ? 2 : 1;
                    break;
                case BodyPart.RightLeg:
                    rightLegHPDmg += rightLegHP > rightLegHPDmg ? 2 : 1;
                    break;
                case BodyPart.LeftLeg:
                    leftLegHPDmg += leftLegHP > leftLegHPDmg ? 2 : 1;
                    break;
                default:
                    break;
            }
        }
        HealthDamageUpdate();
    }
    
    void HealthDamageUpdate()
    {
        healthDmg = headHPDmg + rightLegHPDmg + leftShoulderHPDmg + torsoHPDmg + rightArmHPDmg + leftArmHPDmg + rightLegHPDmg + leftLegHPDmg;
    }

}
