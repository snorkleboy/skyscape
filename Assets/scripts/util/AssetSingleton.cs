using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Loaders{

	public static  class AssetSingleton {
		public static class bundleNames{
			public static string sprites ="sprites";
			public static string prefabs = "prefabs";
		}

		public static Dictionary<string,AssetBundle> bundles = new Dictionary<string, AssetBundle>();
		public static AssetBundle sprites{get{return bundles[bundleNames.sprites];}}

		public static AssetBundle prefabs{get{return bundles[bundleNames.prefabs];}}
		public static Object[] getBundledDirectory<T> (string bundle, string directory)where T: UnityEngine.Object{
			List<T> objectList = new List<T>();
			foreach(var path in bundles[bundle].GetAllAssetNames()){
				if (path.Contains(bundle+"/"+directory)){
					objectList.Add(
						bundles[bundle].LoadAsset<T>(path)
					);
				}
			}
			return objectList.ToArray();
		}
		public static Dictionary<string,AssetBundle> addBundle(string name, AssetBundle bundle){
			bundles[name] = bundle;
			return bundles;
		}
		public static AssetBundle getBundle(string name){
			return bundles[name];
		}
		public static obj getAsset<obj>(string bundle, string name)where obj : class{
			var assetBundle = getBundle(bundle);
			return assetBundle.LoadAsset(name) as obj;
		}
	}

}
