using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Util
{
    public static class Util
    {
        public static void destroyRecursive(Transform parent)
        {
            if (parent == null)
            {
                return;
            }
            foreach (Transform trans in parent)
            {
                destroyRecursive(trans);
                GameObject.Destroy(trans.gameObject);
            }
        }
    }

}
