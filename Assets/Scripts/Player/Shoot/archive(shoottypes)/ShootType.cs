//using System.Collections;
//using System;
//using System.Reflection;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "ShootType", menuName = "Archive/CreateShootType", order = 110)]
//[System.Serializable]
//public class ShootType : ScriptableObject
//{
//    public int level;
//    public GameObject projectile;
//    public bool isOverriding;
//    public string scriptName;
//    public void Shoot(object[] parameters)
//    {
//        //Vector2 dir, float held, int indicator, Vector3 firepoint, PlayerControl pc, float speedMultiplier, float indicatorMultiplier, float indiAmount
//        if (isOverriding)
//        {
            
//            Type type = Type.GetType(scriptName);
//            MethodInfo method = type.GetMethod("Shoot");
//            object[] newParameters = new object[parameters.Length + 2]; 
//            for (int i = 0; i < parameters.Length; i++)
//            {
//                newParameters[i] = parameters[i];
//            }
//            newParameters[parameters.Length] = level;
//            newParameters[parameters.Length + 1] = projectile;
//            method.Invoke(null, newParameters);
//        }
//        else
//        {
//            Vector2 dir = (Vector2)parameters[0];
//            float held = (float)parameters[1];
//            int indicator = (int)parameters[2];
//            Vector3 firepoint = (Vector3)parameters[3];
//            PlayerControl pc = (PlayerControl)parameters[4];
//            float speedMultiplier = (float)parameters[5];
//            float indicatorMultiplier = (float)parameters[6];
//            float indiAmount = (float)parameters[7];
//            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//            GameObject arrow = Instantiate(projectile, firepoint, Quaternion.AngleAxis(angle, Vector3.forward));
//            var arrowrigid = arrow.GetComponent<Rigidbody2D>();
//            var arrowProj = arrow.GetComponent<Projectile>();
//            foreach (DamageType damage in pc.damageTypes)
//            {
//                if (damage.value > 0)
//                {
//                    DamageType tempDamage = new DamageType
//                    {
//                        damageElement = damage.damageElement,
//                        value = damage.value
//                    };
//                    arrowProj.damages.Add(tempDamage);
//                }
//            }
//            arrowrigid.velocity = dir.normalized * (held / indiAmount + (indicator * indicatorMultiplier)) * speedMultiplier;
//        }
//    }
//    public void Projectory(object[] parameters)
//    {
//        //Vector2 dir, float held, float indiAmount, float indicatorMultiplier, float speedMultiplier
//        if (isOverriding)
//        {
//            Type type = Type.GetType(scriptName);
//            MethodInfo method = type.GetMethod("Projectory");
//            method.Invoke(type, parameters);
//        }
//        else
//        {
//            float angle = (float)parameters[0];
//            float held = (float)parameters[1];
//            float indicator = (int)parameters[2];
//            Vector3 iniPos = (Vector3)parameters[3];
//            PlayerControl pc = (PlayerControl)parameters[4];
//            float speedMultiplier = (float)parameters[5];
//            float indicatorMultiplier = (float)parameters[6];
//            float indiAmount = (float)parameters[7];
//            CurveRenderer curve = (CurveRenderer)parameters[8];

//            float speed = (held / indiAmount + (indicator * indicatorMultiplier)) * speedMultiplier;
//            float hSpeed = speed * Mathf.Cos(angle);
//            float vSpeed = speed * Mathf.Sin(angle);
//            float xDist = 2 * hSpeed * vSpeed / 9.8f;
//            float yDist = vSpeed * vSpeed * 1.75f / (2 * 9.8f);
//            curve.point3.transform.position = new Vector2(curve.point1.transform.position.x + xDist, curve.point1.transform.position.y);
//            curve.point2.transform.position = new Vector2((curve.point1.transform.position.x + curve.point3.transform.position.x) / 2, curve.point1.transform.position.y + yDist);
//        }
//    }
//    public void ProjectoryDown(object[] parameters)
//    {
//        //Vector2 dir, float held, float indiAmount, float indicatorMultiplier, float speedMultiplier
//        if (isOverriding)
//        {
//            Debug.Log("do it later haha fuck");
//        }
//        else
//        {
//            Debug.Log("do it later haha fuck");
//        }
//    }
//}
