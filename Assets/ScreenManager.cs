using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    [SerializeField]
    Transform hero;
    [SerializeField]
    GameObject cam;
    Bounds cameraBounds;
    float cameraWidth;

    public static Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    void UpdateBounds()
    {
        cameraBounds = OrthographicBounds(cam.GetComponent<Camera>());
    }

    void Start () {
        UpdateBounds();
        cameraWidth = cameraBounds.extents.x;
    }
	
	void Update () {
        if (hero.position.x > cameraBounds.center.x + cameraWidth) {
            cam.transform.position += Vector3.right * cameraWidth * 2;
            UpdateBounds();
        }
        if (hero.position.x < cameraBounds.center.x - cameraWidth)
        {
            cam.transform.position += Vector3.left * cameraWidth * 2;
            UpdateBounds();
        }
    }
}
