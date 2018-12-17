// using Objects;
// using UnityEngine;
// namespace Objects.Galaxy
// {
//     public static class MasterFactory
//     {
//         public static bool ready = false;
//         public static StarFactory starFactory;
//         public static T create<T>(Transform holder, Vector3 position) where T:StarNode{
//             return starFactory.createStar(holder, position) as T;
//         }
//         public static T create<T>(StarNode holder, Vector3 position) where T:Planet{
//             return starFactory.planetfactory.newPlanet(holder,position) as T;
//         }
//     }
// }