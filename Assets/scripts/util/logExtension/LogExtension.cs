using UnityEngine;
using System.Linq.Expressions;
using System;
namespace util
{
    public static class Log
    {
        public static void errorLog( object caller,string message, params object[] additionals){
            Debug.LogError(makeMessage(caller,message,additionals));
        }
        public static void debugLog(object caller,string message, params object[] additionals){
            Debug.Log(makeMessage(caller,message,additionals));
        }
        public static void warnLog(object caller,string message, params object[] additionals){
            Debug.LogWarning(makeMessage(caller,message,additionals));
        }
        public static void message(object caller,string message, params object[] additionals){
            Debug.Log(makeMessage(caller,message,additionals));
        }
        public static string delim = "- - \t";
        private static string makeMessage(object caller,string message, params object[] additionals){
            var msg = "";
            msg += caller.GetType() + " :";
            msg += "message: " +message + delim;
            if(additionals != null){
                foreach (var item in additionals)
                {

                    msg += (item != null?item.ToString() : "null") + delim;
                }
            }
  
            return msg;
        }

        
    }
}