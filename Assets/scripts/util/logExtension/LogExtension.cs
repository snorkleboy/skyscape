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

        public static string delim = "||\t";
        public static string makeMessage(object caller,string message, params object[] additionals){
            var msg = "";
            msg += caller.GetType() + " :";
            msg += message + delim;
            foreach (var item in additionals)
            {
                msg += item.GetType() + " :" + item.ToString() + delim;
            }
            return msg;
        }

        
    }
}