using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] Transform FlagTransform;
    [SerializeField] Transform HomeTransform;
    
    private Vector3 flagTransformPosition;
    private Quaternion flagTransformRotation;

    private void Start()
    {
        flagTransformPosition = HomeTransform.GetChild(0).position;
        FlagTransform.position = HomeTransform.GetChild(0).position;
    }

    private void Update()
    {
        FlagTransformLerper();
    }

    public Transform GetFlagTransform()
    {
        return FlagTransform;
    }

    public void SetFlagTransform(Vector3 position, Quaternion rotation)
    {
        flagTransformPosition = position;
        flagTransformRotation = rotation;
    }

    private void FlagTransformLerper()
    {
        FlagTransform.SetPositionAndRotation(Vector3.Lerp(FlagTransform.position, flagTransformPosition, 10 * Time.deltaTime), Quaternion.Slerp(FlagTransform.rotation, flagTransformRotation, 10 * Time.deltaTime));
    }
}
