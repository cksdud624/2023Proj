using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    Animator animator;

    public GameObject LookTarget;
    public GameObject RightHand;
    public bool IsIKOn = false;
    public float weight = 0f;
    public float PointX = 0.7f;
    [Range(2.5f, 3f)] public float PointY = 2.5f;
    [Range(-3f, 3f)] public float TestY = 0f;
    [Range(-3f, 3f)] public float TestX = 0f;
    [Range(-3f, 3f)] public float TestZ = 0f;

    int mode = 0;
    bool exchange = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(IsIKOn)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                mode = 1;
            }

            else if(Input.GetKey(KeyCode.W))
            {
                if (weight < 1f)
                {
                    weight += Time.deltaTime * 5;
                }
                else
                {
                    if (exchange == false)
                    {
                        PointY -= Time.deltaTime / 2;
                        if (PointY <= 2.5f)
                        {
                            exchange = true;
                        }
                    }
                    else
                    {
                        PointY += Time.deltaTime / 2;
                        if(PointY >= 3.0f)
                        {
                            exchange = false;
                        }
                    }
                }
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(mode == 1)
        {
            Vector3 pointXVec = new Vector3(PointX, 0, 0);
            Vector3 pointYRightVec = new Vector3(0, PointY, 0);
            Vector3 pointYLeftVec = new Vector3(0, 5.5f - PointY, 0);
            Vector3 RightVec = this.transform.position + pointXVec + pointYRightVec;
            Vector3 LeftVec = this.transform.position - pointXVec + pointYLeftVec;

            Vector3 pointYLegLeftVec = new Vector3(0, TestY, 0);
            Vector3 pointXLegLeftVec = new Vector3(TestX, 0, 0);
            Vector3 pointZLegLeftVec = new Vector3(0, 0, TestZ);
            Vector3 RightLegVec = this.transform.position;
            Vector3 LeftLegVec = this.transform.position - pointXLegLeftVec + pointYLegLeftVec + pointZLegLeftVec; 




            Quaternion rightHandRotation = Quaternion.LookRotation(pointXVec + pointYRightVec);
            Quaternion leftHandRotation = Quaternion.LookRotation(-pointXVec + pointYLeftVec);
            rightHandRotation *= Quaternion.Euler(new Vector3(0, 0, -90));
            leftHandRotation *= Quaternion.Euler(new Vector3(0, 0, 90));

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, RightVec);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandRotation);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftVec);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRotation);

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, weight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, RightLegVec);
            //animator.SetIKRotation(AvatarIKGoal.RightFoot, leftHandRotation);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, weight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftLegVec);
            //animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftHandRotation);
        }
    }
}
