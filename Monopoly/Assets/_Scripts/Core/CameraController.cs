using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : Singleton<CameraController>
{
    [HideInInspector] public Vector3 defaultPosition;
    [HideInInspector] public Quaternion defaultRotation;
    [SerializeField] private CameraShake cameraShake;

    [SerializeField] private Transform allLevelViewPoint;
    [SerializeField] private Transform playerViewPoint;

    public bool isAlwaysMoving = true;
    public float smooth = 1f;
    public float reachDistance = 0.05f;
    [NaughtyAttributes.Required]
    public Transform defaultViewTr;

    public bool isMove;

    [Header("Current Target Transform")]
    public Transform currentTarget;

    [Header("ToPainting")]
    public Transform ExamplePosT;

    private Transform cameraTr;

    private float defaultSmooth;

    protected override void Initialize()
    {
        cameraTr = transform;
        currentTarget = defaultViewTr;
        defaultSmooth = smooth;
        SetAsDefaultPosition(transform); // Current pos as default

        GlobalEvents.MoveCameraToExamplePos.AddListener(() => { SetAndMoveToTarget(ExamplePosT); });
    }

    internal void ShootShake()
    {
        if (!isMove)
            cameraShake.Shake();
    }

    public void ChangeDefaultSpeed(float speed)
    {
        defaultSmooth = speed;
    }

    public void SetAndMoveToTarget(Transform _targetT, bool isInstantMove = false, float speed = -1f)
    {
        if (speed != -1f)
            smooth = speed;
        else
            smooth = defaultSmooth;

        currentTarget = _targetT;
        isMove = true;

        if (isInstantMove)
        {
            cameraTr.position = currentTarget.position;
            cameraTr.rotation = currentTarget.rotation;
        }
    }

    public void SetAsDefaultPosition(Transform view)
    {
        transform.position = view.position;
        defaultPosition = view.position;
    }

    public void MoveToAllLevelView()
    {
        SetAndMoveToTarget(allLevelViewPoint, true);
    }

    public void MoveToPlayerView()
    {
        SetAndMoveToTarget(playerViewPoint);
    }

    public void MoveAtTarget()
    {
        isMove = true;
    }

    private void LateUpdate()
    {
        if (isMove)
        {
            if (currentTarget == null)
                isMove = false;
            else
            {
                if (!IsTargetReached())
                {
                    cameraTr.position = Vector3.Lerp(cameraTr.position, currentTarget.position, Time.deltaTime * smooth);
                    cameraTr.rotation = Quaternion.Lerp(cameraTr.rotation, currentTarget.rotation, Time.deltaTime * smooth);
                }
                else
                {
                    isMove = false;
                }
            }
        }
    }

    private bool IsTargetReached()
    {
        if (isAlwaysMoving)
            return false;
        else
            return (cameraTr.position - currentTarget.position).sqrMagnitude < reachDistance;
    }
}
