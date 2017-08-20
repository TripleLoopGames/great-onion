using UnityEngine;

public class SmoothFollow : MonoBehaviour {

  void FixedUpdate() {
    if (target) {
      var delta = target.position - ownCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, zoom)); //0.5 in X and Y coordinates to center to the viewport
      var destination = transform.position + delta;
      transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
  }

  private void Start() {
    ownCamera = GetComponent<Camera>();
  }

  public float dampTime = 0.05f;
  private Vector3 velocity = Vector3.zero;
  public Transform target;
  private Camera ownCamera;

  [SerializeField]
  private float zoom = 120f;
}