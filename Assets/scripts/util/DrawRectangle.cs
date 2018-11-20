using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace util
{
    public static class DrawRectangle
    {
        public static Texture2D WhiteTexture
        {
            get
            {
                if( _whiteTexture == null )
                {
                    _whiteTexture = new Texture2D( 1, 1 );
                    _whiteTexture.SetPixel( 0, 0, Color.white );
                    _whiteTexture.Apply();
                }
                return _whiteTexture;
            }
        } 
        static Texture2D _whiteTexture;       
        public static void DrawScreenRect( Rect rect, Color color )
        {
            GUI.color = color;
            GUI.DrawTexture( rect, WhiteTexture );
            GUI.color = Color.white;
        }
        public static void DrawScreenRectBorder( Rect rect, float thickness, Color color )
        {
            // Top
            DrawScreenRect( new Rect( rect.xMin, rect.yMin, rect.width, thickness ), color );
            // Left
            DrawScreenRect( new Rect( rect.xMin, rect.yMin, thickness, rect.height ), color );
            // Right
            DrawScreenRect( new Rect( rect.xMax - thickness, rect.yMin, thickness, rect.height ), color);
            // Bottom
            DrawScreenRect( new Rect( rect.xMin, rect.yMax - thickness, rect.width, thickness ), color );
        }

        public static Rect GetScreenRect( Vector3 screenPosition1, Vector3 screenPosition2 )
        {
            // Move origin from bottom left to top left
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;
            // Calculate corners
            var topLeft = Vector3.Min( screenPosition1, screenPosition2 );
            var bottomRight = Vector3.Max( screenPosition1, screenPosition2 );
            // Create Rect
            return Rect.MinMaxRect( topLeft.x, topLeft.y, bottomRight.x, bottomRight.y );
        }
        public static Bounds GetViewportBounds( Camera camera, Vector3 screenPosition1, Vector3 screenPosition2 )
        {
            var v1 = Camera.main.ScreenToViewportPoint( screenPosition1 );
            var v2 = Camera.main.ScreenToViewportPoint( screenPosition2 );
            var min = Vector3.Min( v1, v2 );
            var max = Vector3.Max( v1, v2 );
            min.z = camera.nearClipPlane;
            max.z = camera.farClipPlane;
        
            var bounds = new Bounds();
            bounds.SetMinMax( min, max );
            return bounds;
        }
        public static List<toFind> getObjectsInBox<toFind>(Bounds bounds) where toFind : MonoBehaviour, IRenderable{
                var things = GameManager.instance.selectedStar.gameObject.GetComponentsInChildren<toFind>();
                if (things != null){
                    Debug.Log(typeof(toFind) + " - things.Length: " +things.Length);
                    var bounded = new List<toFind>();
                    foreach (var item in things)
                    {
                        if (bounds.Contains(Camera.main.WorldToViewportPoint(item.renderHelper.transform.position))){
                            bounded.Add(item);
                        }
                    }
                    return bounded;
                }else{
                    return new List<toFind>();
                }

        }
    }
}