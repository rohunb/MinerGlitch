﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this class adds extended functionality to various built in classes like Transform, GameObject, etc.
public static class ExtensionMethods
{
    #region Vector3 Extensions
    public static Vector3 GetVector3(this Vector2 vec2)
    {
        return new Vector3(vec2.x, vec2.y, 0.0f);
    }
    #endregion Vector3 Extensions

    #region Transform Extensions
    /// <summary>
    /// resets a transform to position: zero, quaternion identiy, scale: 1
    /// </summary>
    /// <param name="trans"></param>
    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1f, 1f, 1f);
    }

    /// <summary>
    /// Sets just the x coordinate of a transform's position
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="x"></param>
    public static void SetPositionX(this Transform trans, float x)
    {
        Vector3 newPosition = new Vector3(x, trans.position.y, trans.position.z);
        trans.position = newPosition;
    }
    /// <summary>
    /// Sets just the y coordinate of a transform's position
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="y"></param>
    public static void SetPositionY(this Transform trans, float y)
    {
        Vector3 newPosition = new Vector3(trans.position.x, y, trans.position.z);
        trans.position = newPosition;
    }
    /// <summary>
    /// Sets just the z coordinate of a transform's position
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="z"></param>
    public static void SetPositionZ(this Transform trans, float z)
    {
        Vector3 newPosition = new Vector3(trans.position.x, trans.position.y, z);
        trans.position = newPosition;
    }
    /// <summary>
    /// Rotates the transform around the X axis only
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="angle"></param>
    public static void RotateAroundXAxis(this Transform trans, float angle)
    {
        trans.Rotate(angle, 0f, 0f);
    }
    /// <summary>
    /// Rotates the transform around the Y axis only
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="angle"></param>
    public static void RotateAroundYAxis(this Transform trans, float angle)
    {
        trans.Rotate(0f, angle, 0f);
    }
    /// <summary>
    /// Rotates the transform around the Z axis only
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="angle"></param>
    public static void RotateAroundZAxis(this Transform trans, float angle)
    {
        trans.Rotate(0f, 0f, angle);
    }
    /// <summary>
    /// Sets just the x value of the transform's eulerAngles
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="x"></param>
    public static void SetEulerX(this Transform trans, float x)
    {
        Vector3 newRot = new Vector3(x, trans.eulerAngles.y, trans.eulerAngles.z);
        trans.eulerAngles = newRot;
    }
    /// <summary>
    /// Sets just the y value of the transform's eulerAngles
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="y"></param>
    public static void SetEulerY(this Transform trans, float y)
    {
        Vector3 newRot = new Vector3(trans.eulerAngles.x, y, trans.eulerAngles.z);
        trans.eulerAngles = newRot;
    }
    /// <summary>
    /// Sets just the z value of the transform's eulerAngles
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="z"></param>
    public static void SetEulerZ(this Transform trans, float z)
    {
        Vector3 newRot = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, z);
        trans.eulerAngles = newRot;
    }
    #endregion

    #region GameObject Extensions
    /// <summary>
    /// Defensive GetComponent that generates an error if not found - only works with Monobehaviour scripts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T GetSafeComponent<T>(this GameObject obj) where T : MonoBehaviour
    {
        T component = obj.GetComponent<T>();
        if (!component)
        {
            Debug.LogError("Could not find " + typeof(T) + " component", obj);
        }
        return component;
    }
    #endregion

    #region Rigidbody Extensions
    /// <summary>
    /// resets a rigidbody to zero velocity and angular velocity
    /// </summary>
    /// <param name="_rigidbody"></param>
    public static void ResetMovement(this Rigidbody _rigidbody)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    /// <summary>
    /// Sets just the x velocity of a rigidbody - others remain the same
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <param name="x"></param>
    public static void SetVelocityX(this Rigidbody _rigidbody, float x)
    {
        Vector3 newVelocity = new Vector3(x, _rigidbody.velocity.y, _rigidbody.velocity.z);
        _rigidbody.velocity = newVelocity;
    }
    /// <summary>
    /// Sets just the y velocity of a rigidbody - others remain the same
    /// </summary>
    /// <param name="_rigidbody"></param>
    /// <param name="y"></param>
    public static void SetVelocityY(this Rigidbody _rigidbody, float y)
    {
        Vector3 newVelocity = new Vector3(_rigidbody.velocity.x, y, _rigidbody.velocity.z);
        _rigidbody.velocity = newVelocity;
    }
    /// <summary>
    /// Sets just the z velocity of a rigidbody - others remain the same
    /// </summary>
    /// <param name="_rigidbody"></param>
    /// <param name="z"></param>
    public static void SetVelocityZ(this Rigidbody _rigidbody, float z)
    {
        Vector3 newVelocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, z);
        _rigidbody.velocity = newVelocity;
    }

    #endregion

    #region Material Extensions
    /// <summary>
    /// Returns a Colour with the provided alpha value e.g.: blah.color = someColour.WithAlpha(alpha)
    /// </summary>
    /// <param name="color"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public static Color WithAplha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
    #endregion

    #region List Extensions
    /// <summary>
    /// This method shuffles the contents of the list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while(n-->1)
        {
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #endregion List Extensions

}


