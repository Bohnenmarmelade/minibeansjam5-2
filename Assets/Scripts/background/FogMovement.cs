using UnityEngine;

public class FogMovement : MonoBehaviour {
    public Camera camera;

    [SerializeField]
    [Range(1f, 5f)] private float movementMultiplier = 3f;


    private void Update() {
        Vector3 pos = transform.position;
        pos.x = camera.transform.position.x / movementMultiplier;
        transform.position = pos;
    }
}