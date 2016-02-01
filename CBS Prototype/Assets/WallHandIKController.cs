using UnityEngine;
using System.Collections;

public class WallHandIKController : MonoBehaviour
{

    protected Animator animator;
    public GameObject m_RightHandTarget;
    public GameObject m_LeftHandTarget;
    //public bool m_HandOnWall;
    public float m_LerpTime = 2.0f;
    public float m_LerpResetDist = 2.0f;

    public float m_ViewDist = 5.0f;

    private float m_TimerRight = 0.0f;
    private float m_TimerLeft = 0.0f;
    private Vector3 m_PreviousRightHandTargetPos;
    private Vector3 m_PreviousLeftHandTargetPos;

    // Use this for initialization
    void Start()
    {
        //m_HandOnWall = false;
        animator = GetComponent<Animator>();
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 1);
        m_PreviousRightHandTargetPos = m_RightHandTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // for (and if) when we will have hand animation
        //animator.SetBool("RightHandWall", m_HandOnWall);
    }


    void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("checking IK");

        ///
        /// RIGHT HAND
        ///
        Vector3 handPos1 = Vector3.zero;
        Vector3 handPos2 = Vector3.zero;
        float weight1 = 0.0f;
        float weight2 = 0.0f;

        RaycastHit hit;
        Ray ray = new Ray(transform.position + transform.up * 3, transform.right * 1.5f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if(Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos1 = hit.point;
                weight1 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        ray = new Ray(transform.position + transform.up * 3, transform.forward * 1.5f + transform.right * 0.4f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos2 = hit.point;
                weight2 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        float finalWeight = Mathf.Max(weight1, weight2);
        Vector3 finalPos = Vector3.zero;
        if (weight1 > weight2)
            finalPos = handPos1;
        else
            finalPos = handPos2;
        if (finalWeight == 0 || m_TimerRight >= m_LerpTime || Vector3.Distance(m_PreviousRightHandTargetPos, finalPos) >= m_LerpResetDist)
        {
            m_TimerRight = 0.0f;
        }

        m_RightHandTarget.transform.position = Vector3.Lerp(m_RightHandTarget.transform.position, finalPos, m_TimerRight / m_LerpTime);
        m_TimerRight += Time.deltaTime;

        m_PreviousRightHandTargetPos = finalPos;



        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, finalWeight);        
        animator.SetIKPosition(AvatarIKGoal.RightHand, m_RightHandTarget.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, finalWeight);
        Vector3 rot = m_RightHandTarget.transform.rotation.eulerAngles - Vector3.right * 90;
        Quaternion newrot = Quaternion.Euler(rot.x, rot.y, rot.z);
        animator.SetIKRotation(AvatarIKGoal.RightHand, newrot);


        ///
        /// LEFT HAND
        ///
        handPos1 = Vector3.zero;
        handPos2 = Vector3.zero;
        weight1 = 0.0f;
        weight2 = 0.0f;

        ray = new Ray(transform.position + transform.up * 3, transform.right * (-1.5f ));
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos1 = hit.point;
                weight1 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        ray = new Ray(transform.position + transform.up * 3, transform.forward * 1.5f - transform.right * 0.4f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos2 = hit.point;
                weight2 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        finalWeight = Mathf.Max(weight1, weight2);
        finalPos = Vector3.zero;
        if (weight1 > weight2)
            finalPos = handPos1;
        else
            finalPos = handPos2;

        if (finalWeight == 0 || m_TimerLeft >= m_LerpTime || Vector3.Distance(m_PreviousLeftHandTargetPos, finalPos) >= m_LerpResetDist)
        {
            m_TimerLeft = 0.0f;
        }

        m_LeftHandTarget.transform.position = Vector3.Lerp(m_LeftHandTarget.transform.position, finalPos, m_TimerLeft / m_LerpTime);
        m_TimerLeft += Time.deltaTime;

        m_PreviousLeftHandTargetPos = finalPos;



        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, finalWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, m_LeftHandTarget.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, finalWeight);
        rot = m_LeftHandTarget.transform.rotation.eulerAngles - Vector3.right * 90;
        newrot = Quaternion.Euler(rot.x, rot.y, rot.z);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, newrot);

    }
}
