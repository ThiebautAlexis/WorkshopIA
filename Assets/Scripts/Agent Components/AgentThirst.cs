using UnityEngine;

public class AgentThirst : MonoBehaviour
{
    [SerializeField] private float maxThirst = 100.0f;
    [SerializeField] private float currentThirst = 100.0f;
    [SerializeField] private float minThirst = 15.0f; 
    public void ResetThirst() => currentThirst = maxThirst;

    public bool CheckThirst(float _minThirstValue) => currentThirst < _minThirstValue;
    public bool CheckThirst() => currentThirst < minThirst;

    private void Update() => currentThirst -= Time.deltaTime; 
}
