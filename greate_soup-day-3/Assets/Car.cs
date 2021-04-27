using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Car : MonoBehaviour
{
    private const float SPEED_LIMIT = 60;
    private const float KMPH2MPS = 3.6f;

    [SerializeField] private float acceleration;
    [SerializeField] private float rotationPerSecond;

    public EMovingType Mode;
    private float _tempSpeedLimit = SPEED_LIMIT;
    private float _targetSpeed;
    private float _targetAngle;
    private float _speed;
    private List<Vector2> _route;
    private Vector2[] _target;
    private int _targetIndex;
    private float _distToTarget;
    private Action _onComplete;
    public Vector2 Position => new Vector2(transform.position.x, transform.position.z);
    public Vector2 MovingVector => Quaternion.Euler(0, CurrentAngle, 0) * Vector3.right * _speed;

    private float CurrentAngle
    {
        get => transform.rotation.eulerAngles.y;
        set => transform.localRotation = Quaternion.Euler(0, value, 0);
    }

    void Start()
    {
    }

    void Update()
    {
        Tick();
    }

    public void ResetTempLimit() => _tempSpeedLimit = SPEED_LIMIT;
    public void SetTargetSpeed(float value) => _targetSpeed = value;

    public void SetDirection(Vector2 dir) =>
        _targetAngle = GetAngle(dir);

    private float GetAngle(Vector2 dir) =>
        (Vector2.Angle(Vector2.right, dir) * -Mathf.Sign(dir.y) + 360) % 360;

    public void SetRotationHardToTarget()
    {
        var delta = _target[_targetIndex] - Position;
        CurrentAngle = GetAngle(delta);
    }

    public void SetDirection(float angle)
    {
        _targetAngle = angle;
        Mode = EMovingType.Manual;
    }

    private void MoveTo(int index)
    {
        var delta = _target[index] - Position;
        SetDirection(delta);
    }

    public void Move(List<Vector2> positions, Action oncomplete = null)
    {
        _distToTarget = 0;
        _target = positions.ToArray();
        _targetIndex = 0;
        MoveTo(_targetIndex);
        Mode = EMovingType.Route;
        _onComplete = oncomplete;
        for (var i = 0; i < positions.Count - 1; i++)
            _distToTarget += Vector2.Distance(positions[i], positions[i + 1]);
    }

    public void Tick()
    {
        var temp = _targetSpeed;
        var needToStop = CarInteractionHelper.NeedToStop(this);
        if (needToStop)
            temp = 0;
        
        _speed = Mathf.Clamp(_speed + Math.Sign(temp - _speed) * acceleration * (needToStop ? 5 : 1) * Time.deltaTime, 0,
            _tempSpeedLimit);
        CurrentAngle = Mathf.LerpAngle(CurrentAngle, _targetAngle,
            Mathf.Min(
                rotationPerSecond / Mathf.Abs(Mathf.DeltaAngle(CurrentAngle, _targetAngle) % 360) * Time.deltaTime, 1));

        var movV = Quaternion.Euler(0, Mathf.LerpAngle(_targetAngle, CurrentAngle, .5f), 0) * Vector3.right *
                   (_speed * Time.deltaTime);
        transform.Translate(movV, Space.World);

        
        if (Mode == EMovingType.Route && _targetIndex >= 0)
        {
            var curDis = _distToTarget + Vector2.Distance(Position, _target[_targetIndex]);
            _targetSpeed = BreakingPath()> curDis ? 0 : _tempSpeedLimit;
            _targetAngle = GetAngle(_target[_targetIndex] - Position);
            if (Vector2.Distance(Position, _target[_targetIndex]) < 1)
            {
                _targetIndex++;
                if (_targetIndex == _target.Length)
                {
                    _targetIndex = -1;
                    _onComplete?.Invoke();
                    _targetSpeed = 0;
                }
                else
                {
                    MoveTo(_targetIndex);
                    _distToTarget-=Vector2.Distance(_target[_targetIndex - 1], _target[_targetIndex]);
                }
            }
        }
    }

    private float BreakingPath()
    {
        var t = _speed / acceleration;
        return _speed * t - acceleration * t * t / 2;
    }

    public enum EMovingType
    {
        Manual,
        Route
    }
}