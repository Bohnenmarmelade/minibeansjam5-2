
using UnityEngine;

public class BuildingMovement : MonoBehaviour {
    public Camera camera;

    [SerializeField]
    [Range(1f, 5f)] private float movementMultiplier = 2f;


    private void Update() {
        Vector3 pos = transform.position;
        pos.x = camera.transform.position.x / movementMultiplier;
        transform.position = pos;
    }
}
