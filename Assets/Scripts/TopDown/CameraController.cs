using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap tilemap;

    private float halfHeight;
    private float halfWidth;

    private Vector3 bottomeLeftLimit;
    private Vector3 topRightLimit;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomeLeftLimit = tilemap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = tilemap.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);

        PlayerController.instance.SetBounds(tilemap.localBounds.min, tilemap.localBounds.max);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, bottomeLeftLimit.x, topRightLimit.x), Mathf.Clamp(target.position.y, bottomeLeftLimit.y, topRightLimit.y), transform.position.z); 
    }
}
