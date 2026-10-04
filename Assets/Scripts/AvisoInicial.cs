using UnityEngine;

public class Avisoinicial : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
