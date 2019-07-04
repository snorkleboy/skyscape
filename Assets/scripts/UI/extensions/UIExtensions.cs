using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace util
{
    public static class UIExt
    {
        public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2) { 
            // Move origin from bottom left to top left
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;
            // Calculate corners
            var topLeft = Vector3.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
            // Create Rect
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }
        public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
        {
            var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
            var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
            var min = Vector3.Min(v1, v2);
            var max = Vector3.Max(v1, v2);
            min.z = camera.nearClipPlane;
            max.z = camera.farClipPlane;

            var bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }
        private static toFind[] getThings<toFind>() where toFind : MonoBehaviour, IMoveable
        {
            var currentStar = GameManager.instance.selectedStar;
            var things = currentStar.gameObject.GetComponentsInChildren<toFind>();
            if (things != null)
            {
                return things;
            }
            else
            {
                return null;
            }

        }
        public static Vector3 MouseToWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // create a plane at 0,0,0 whose normal points to +Y:
            Plane hPlane = new Plane(Vector3.up, Vector3.zero);
            // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
            float distance = 0;
            // if the ray hits the plane...
            Vector3 pos;
            if (hPlane.Raycast(ray, out distance))
            {
                // get the hit point:
                pos = ray.GetPoint(distance);
            }
            else
            {
                pos = Vector3.negativeInfinity;
            }
            return pos;

        }
        public static List<toFind>getObjectsUnderMouse<toFind>() where toFind : MonoBehaviour, IMoveable
        {
            var things = getThings<toFind>();
            var mousePosition = MouseToWorld();
            var found = new List<toFind>() ;
            foreach(var thing in things)
            {
                var xDiff = thing.positionState.position.x - mousePosition.x;
                var yDiff = thing.positionState.position.z - mousePosition.z;
                if (xDiff < 2 && yDiff<2)
                {
                    Debug.Log("found some " + typeof(toFind));
                    found.Add(thing);
                }
            }
            return found;
        }
        public static List<toFind> getObjectsInBox<toFind>(Bounds bounds) where toFind : MonoBehaviour, IMoveable
        {
            var things = getThings<toFind>();
            Debug.Log(typeof(toFind) + " - things.Length: " + things.Length);
            var bounded = new List<toFind>();
            foreach (var item in things)
            {
                var positionToCheck = Camera.main.WorldToViewportPoint(item.positionState.position);
                if (bounds.Contains(positionToCheck))
                {
                    bounded.Add(item);
                }
            }
            return bounded;

        }
    }
}
