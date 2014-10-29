using UnityEngine;
using System.Collections;

//Adapted from http://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055
public class WrapAround : MonoBehaviour
{

    private Renderer[] renderers;
    private bool isVisible;
    
    bool isWrappingX = false;
    bool isWrappingY = false;

    // Use this for initialization
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    bool CheckRenderers()
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        ScreenWrap();
    }

    void ScreenWrap()
    {
        isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        Vector3 nextPosition = transform.position;
        Vector3 viewPortPos = Camera.main.WorldToViewportPoint(nextPosition);

        if (!isWrappingX && (viewPortPos.x > 1 || viewPortPos.x < 0))
        {
            nextPosition.x = -nextPosition.x;
            isWrappingX = true;
        }
        if (!isWrappingY && (viewPortPos.y > 1 || viewPortPos.y < 0))
        {
            nextPosition.y = -nextPosition.y;
            isWrappingY = true;
        }
        transform.position = nextPosition;
    }
}
