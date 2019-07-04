using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace util
{
    public static class Line{
         public static LineRenderer DrawTempLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
         {
             
             GameObject myLine = new GameObject();
             myLine.transform.position = start;
             LineRenderer lr = myLine.AddComponent<LineRenderer>();
             lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
             lr.SetColors(color, Color.red);
             lr.SetWidth(0.1f, 0.1f);
             lr.SetPosition(0, start);
             lr.SetPosition(1, end);
             GameObject.Destroy(myLine, duration);
             return lr;
         }
    }
    public static class Rectangle
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

       
    }
}