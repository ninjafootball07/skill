﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Skill.IO
{
    public delegate T CreateISavable<T>() where T : ISavable;

    public class XmlLoadStream
    {

        public int ReadInt(XmlElement e)
        {
            string v = e.GetAttribute("Value");
            if (v != null)
            {
                int i;
                if (int.TryParse(v, out i))
                    return i;
            }
            return 0;
        }

        public float ReadFloat(XmlElement e)
        {
            string v = e.GetAttribute("Value");
            if (v != null)
            {
                float f;
                if (float.TryParse(v, out f))
                    return f;
            }
            return 0.0f;
        }

        public bool ReadBoolean(XmlElement e)
        {
            string v = e.GetAttribute("Value");
            if (v != null)
            {
                bool b;
                if (bool.TryParse(v, out b))
                    return b;
            }
            return false;
        }

        public string ReadString(XmlElement e)
        {
            string v = e.GetAttribute("Value");
            if (v == null)
                v = "";
            return v;
        }

        public Bounds ReadBounds(XmlElement e)
        {
            Vector3 center = new Vector3();
            Vector3 size = new Vector3();

            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "Center":
                        center = ReadVector3(element);
                        break;
                    case "Size":
                        size = ReadVector3(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }

            return new Bounds(center, size);
        }
        public Color ReadColor(XmlElement e)
        {
            float a = 0, g = 0, b = 0, r = 0;

            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "A":
                        a = ReadFloat(element);
                        break;
                    case "B":
                        b = ReadFloat(element);
                        break;
                    case "G":
                        g = ReadFloat(element);
                        break;
                    case "R":
                        r = ReadFloat(element);
                        break;

                }
                element = element.NextSibling as XmlElement;
            }

            return new Color(r, g, b, a);
        }
        public Matrix4x4 ReadMatrix4x4(XmlElement e)
        {
            Matrix4x4 matrix = new Matrix4x4();

            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "M00":
                        matrix.m00 = ReadFloat(element);
                        break;
                    case "M01":
                        matrix.m01 = ReadFloat(element);
                        break;
                    case "M02":
                        matrix.m02 = ReadFloat(element);
                        break;
                    case "M03":
                        matrix.m03 = ReadFloat(element);
                        break;
                    case "M10":
                        matrix.m10 = ReadFloat(element);
                        break;
                    case "M11":
                        matrix.m11 = ReadFloat(element);
                        break;
                    case "M12":
                        matrix.m12 = ReadFloat(element);
                        break;
                    case "M13":
                        matrix.m13 = ReadFloat(element);
                        break;
                    case "M20":
                        matrix.m20 = ReadFloat(element);
                        break;
                    case "M21":
                        matrix.m21 = ReadFloat(element);
                        break;
                    case "M22":
                        matrix.m22 = ReadFloat(element);
                        break;
                    case "M23":
                        matrix.m23 = ReadFloat(element);
                        break;
                    case "M30":
                        matrix.m30 = ReadFloat(element);
                        break;
                    case "M31":
                        matrix.m31 = ReadFloat(element);
                        break;
                    case "M32":
                        matrix.m32 = ReadFloat(element);
                        break;
                    case "M33":
                        matrix.m33 = ReadFloat(element);
                        break;

                }
                element = element.NextSibling as XmlElement;
            }
            return matrix;
        }

        public Plane ReadPlane(XmlElement e)
        {

            Vector3 normal = new Vector3();
            float distance = 0;

            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "Normal":
                        normal = ReadVector3(element);
                        break;
                    case "Distance":
                        distance = ReadFloat(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }

            return new Plane(normal, distance);
        }

        public Quaternion ReadQuaternion(XmlElement e)
        {
            Quaternion quaternion = new Quaternion();

            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "W":
                        quaternion.w = ReadFloat(element);
                        break;
                    case "X":
                        quaternion.x = ReadFloat(element);
                        break;
                    case "Y":
                        quaternion.y = ReadFloat(element);
                        break;
                    case "Z":
                        quaternion.z = ReadFloat(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }
            return quaternion;
        }
        public Ray ReadRay(XmlElement e)
        {
            Vector3 origin = new Vector3();
            Vector3 direction = Vector3.forward;

            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "Origin":
                        origin = ReadVector3(element);
                        break;
                    case "Direction":
                        direction = ReadVector3(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }
            return new Ray(origin, direction);
        }

        public Rect ReadRect(XmlElement e)
        {
            Rect rect = new Rect();

            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "X":
                        rect.x = ReadFloat(element);
                        break;
                    case "Y":
                        rect.y = ReadFloat(element);
                        break;
                    case "Width":
                        rect.width = ReadFloat(element);
                        break;
                    case "Height":
                        rect.height = ReadFloat(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }
            return rect;
        }
        public Vector2 ReadVector2(XmlElement e)
        {
            Vector2 vector = new Vector2();
            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "X":
                        vector.x = ReadFloat(element);
                        break;
                    case "Y":
                        vector.y = ReadFloat(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }
            return vector;
        }
        public Vector3 ReadVector3(XmlElement e)
        {
            Vector3 vector = new Vector3();
            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "X":
                        vector.x = ReadFloat(element);
                        break;
                    case "Y":
                        vector.y = ReadFloat(element);
                        break;
                    case "Z":
                        vector.z = ReadFloat(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }
            return vector;
        }
        public Vector4 ReadVector4(XmlElement e)
        {
            Vector4 vector = new Vector4();
            XmlElement element = e.FirstChild as XmlElement;
            while (element != null)
            {
                switch (element.Name)
                {
                    case "W":
                        vector.w = ReadFloat(element);
                        break;
                    case "X":
                        vector.x = ReadFloat(element);
                        break;
                    case "Y":
                        vector.y = ReadFloat(element);
                        break;
                    case "Z":
                        vector.z = ReadFloat(element);
                        break;
                }
                element = element.NextSibling as XmlElement;
            }
            return vector;
        }

        public T ReadSavable<T>(XmlElement e, CreateISavable<T> creator) where T : ISavable
        {
            T newItem = creator();
            newItem.Load(e, this);
            return newItem;
        }

        public T[] ReadSavableArray<T>(XmlElement e, CreateISavable<T> creator) where T : ISavable
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            T[] array = new T[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                if (element.Name != XmlSaveStream.NoData)
                {
                    T newItem = creator();
                    newItem.Load(element, this);
                    array[index] = newItem;
                }
                element = element.NextSibling as XmlElement;
                index++;
            }
            return array;
        }


        public int[] ReadIntArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            int[] array = new int[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadInt(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public float[] ReadFloatArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            float[] array = new float[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadFloat(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public bool[] ReadBooleanArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            bool[] array = new bool[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadBoolean(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public string[] ReadStringArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            string[] array = new string[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadString(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Bounds[] ReadBoundsArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Bounds[] array = new Bounds[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadBounds(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Color[] ReadColorArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Color[] array = new Color[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadColor(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Matrix4x4[] ReadMatrix4x4Array(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Matrix4x4[] array = new Matrix4x4[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadMatrix4x4(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Plane[] ReadPlaneArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Plane[] array = new Plane[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadPlane(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Quaternion[] ReadQuaternionArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Quaternion[] array = new Quaternion[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadQuaternion(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Ray[] ReadRayArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Ray[] array = new Ray[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadRay(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Rect[] ReadRectArray(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Rect[] array = new Rect[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadRect(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Vector2[] ReadVector2Array(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Vector2[] array = new Vector2[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadVector2(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Vector3[] ReadVector3Array(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Vector3[] array = new Vector3[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadVector3(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
        public Vector4[] ReadVector4Array(XmlElement e)
        {
            string lenghtStr = e.GetAttribute("Lenght");
            int length = 0;
            if (!int.TryParse(lenghtStr, out length))
                length = 0;
            Vector4[] array = new Vector4[length];

            XmlElement element = e.FirstChild as XmlElement;
            int index = 0;
            while (element != null)
            {
                array[index++] = ReadVector4(element);
                element = element.NextSibling as XmlElement;
            }
            return array;
        }
    }
}
