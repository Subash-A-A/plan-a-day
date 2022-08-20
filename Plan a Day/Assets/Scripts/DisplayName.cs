using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    [SerializeField] LayerMask buildingMask;
    [SerializeField] GameObject buildingName;

    private Text nameText;

    private void Awake()
    {
        nameText = buildingName.GetComponent<Text>();
    }
    private void Start()
    {
        buildingName.SetActive(false);
    }
    private void Update()
    {
        ShowName();
        FaceCamera();
    }
    void ShowName()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, 999f, buildingMask))
        {
            Vector3 namePosition = hitInfo.transform.position;

            if (hitInfo.transform.gameObject.TryGetComponent<NameDisplayHeight>(out var height))
            {
                namePosition.y = hitInfo.transform.position.y + height.displayAtHeight;
            }
            nameText.text = hitInfo.transform.name;
            buildingName.SetActive(true);
            
            
            buildingName.transform.position = namePosition;
        }
        else
        {
            buildingName.SetActive(false);
        }
    }
    void FaceCamera()
    {
        buildingName.transform.rotation = Camera.main.transform.rotation;
    }
}
