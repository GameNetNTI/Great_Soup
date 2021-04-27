using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private int tact;
    [SerializeField] private bool isWorking;
    [SerializeField] private float DefaultInterval = 20;
    [SerializeField] private float LowInterval = 4;
    [SerializeField] private MeshRenderer Green;
    [SerializeField] private MeshRenderer Yellow;
    [SerializeField] private MeshRenderer Red;
    [SerializeField] private Material GreenMat;
    [SerializeField] private Material YellowMat;
    [SerializeField] private Material RedMat;
    [SerializeField] private Material DisableMat;


    public ETrafficLightState State => _state;
    private ETrafficLightState _state;
    private float _exceedTime;

    private void Update()
    {
        if (isWorking && Time.time > _exceedTime)
            NextTact();
    }

    private void NextTact()
    {
        tact++;
        tact %= 4;
        switch (tact)
        {
            case 0:
                SetState(ETrafficLightState.Run);
                break;
            case 1:
                SetState(ETrafficLightState.Alternating);
                break;
            case 2:
                SetState(ETrafficLightState.Stop);
                break;
            case 3:
                SetState(ETrafficLightState.Alternating);
                break;
        }
    }

    public void SetState(ETrafficLightState state)
    {
        _state = state;
        if (state == ETrafficLightState.Run)
        {
            Green.material = GreenMat;
            Yellow.material = DisableMat;
            Red.material = DisableMat;
            _exceedTime = Time.time + DefaultInterval;
        }
        else if (state == ETrafficLightState.Stop)
        {
            Green.material = DisableMat;
            Yellow.material = DisableMat;
            Red.material = RedMat;
            _exceedTime = Time.time + DefaultInterval;
        }
        else if (state == ETrafficLightState.Alternating)
        {
            Green.material = DisableMat;
            Yellow.material = YellowMat;
            _exceedTime = Time.time + LowInterval;
        }
    }

    public enum ETrafficLightState
    {
        Run,
        Stop,
        Alternating
    }
}