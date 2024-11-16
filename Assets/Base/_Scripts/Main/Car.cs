using PathCreation.Examples;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int carLevel;
    private PathFollower _movement;
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _movement = GetComponent<PathFollower>();
        CheckSameCar();

        Invoke("EnableTrigger", 1);
    }
    public void MovementStatus(bool canMove) => _movement.enabled = canMove;

    private void CheckSameCar()
    {
        for (int i = 0; i < CarManager.Instance.activeCars.Count; i++)
        {
            if (CarManager.Instance.activeCars[i].GetComponent<Car>().carLevel == carLevel)
            {
                MovementStatus(false);
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Finish")) return;

        UIManager.Instance.IncreaseMoney(carLevel + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Car")) return;

        if (other.gameObject.GetComponent<Car>().carLevel != carLevel) return;

        if (!_movement.enabled) return;

        if (carLevel == 19)
        {
            UIManager.Instance.OpenLevelPanel();
            _movement.enabled = false;
        }

        else
            CarManager.Instance.SpawnCar(carLevel + 1, gameObject, other.gameObject);
    }

    private void EnableTrigger() => _collider.enabled = true;
}
