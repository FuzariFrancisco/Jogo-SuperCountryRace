using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool play;
    public float m_DampTime = 0.1f;
    public float m_ScreenEdgeBuffer = 8f;
    public float m_MinSize = 6.5f;

    [SerializeField] private Vector3 positionCamera;
    [SerializeField] private Vector3 rotationCamera;

    [HideInInspector] private GameObject[] m_Targets;

    private Camera m_Camera;
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

    private void Awake(){
        play = false;
        m_Camera = GetComponentInChildren<Camera>();
        m_Targets = GameObject.FindGameObjectsWithTag("Player");
    }

    private void FixedUpdate(){
        if (play)
        {
            Move();
            Zoom();
        } 
    }

    public void Play()
    {
        m_Camera.transform.position = positionCamera;
        m_Camera.transform.rotation = Quaternion.Euler(rotationCamera.x, rotationCamera.y, rotationCamera.z);
        play = true;
    }

    private void Move(){
        FindAveragePosition();
        this.transform.position = Vector3.SmoothDamp(this.transform.position, m_DesiredPosition, 
            ref m_MoveVelocity, m_DampTime);
    }

    private void FindAveragePosition(){
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < m_Targets.Length; i++){
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            averagePos += m_Targets[i].transform.position;
            numTargets++;
        }

        averagePos.y = transform.position.y;
        m_DesiredPosition = averagePos;
    }

    private void Zoom(){
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, 
            ref m_ZoomSpeed, m_DampTime);
    }

    private float FindRequiredSize(){
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);
        float size = 0f;

        for (int i = 0; i < m_Targets.Length; i++){
            if (!m_Targets[i].gameObject.activeSelf)
                continue;
            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].transform.position);
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        size += m_ScreenEdgeBuffer;
        size = Mathf.Max(size, m_MinSize);
        return size;
    }

    public void SetStartPositionAndSize(){
        FindAveragePosition();
        transform.position = m_DesiredPosition;
        m_Camera.orthographicSize = FindRequiredSize();
    }
    

}
