using UnityEngine;

public class JournalInputData : MonoBehaviour
{
    [System.Serializable]
    public class RoundFromTo
    {
        public int round;
        public string[] fromToPaths;
    }

    public RoundFromTo[] roundFromTos;
}
