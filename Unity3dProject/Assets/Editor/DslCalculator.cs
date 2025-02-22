﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using GameLibrary;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
#region 解释器
namespace DslExpression
{
    public struct Float2
    {
        public float x;
        public float y;

        public override string ToString()
        {
            return string.Format("{{{0},{1}}}", x, y);
        }
        public static Float2 zero = new Float2();
    }
    public struct Float3
    {
        public float x;
        public float y;
        public float z;

        public override string ToString()
        {
            return string.Format("{{{0},{1},{2}}}", x, y, z);
        }
        public static Float3 zero = new Float3();
    }
    public struct Float4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public override string ToString()
        {
            return string.Format("{{{0},{1},{2},{3}}}}", x, y, z, w);
        }
        public static Float4 zero = new Float4();
    }
    public struct Integer2
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return string.Format("{{{0},{1}}}", x, y);
        }
        public static Integer2 zero = new Integer2();
    }
    public struct Integer3
    {
        public int x;
        public int y;
        public int z;

        public override string ToString()
        {
            return string.Format("{{{0},{1},{2}}}", x, y, z);
        }
        public static Integer3 zero = new Integer3();
    }
    public struct Integer4
    {
        public int x;
        public int y;
        public int z;
        public int w;

        public override string ToString()
        {
            return string.Format("{{{0},{1},{2},{3}}}", x, y, z, w);
        }
        public static Integer4 zero = new Integer4();
    }
    public static class CalculatorValueConverter
    {
        public static CalculatorValue ToCalculatorValue<T>(T v)
        {
            var from = s_FromBoxedValue as FromGenericDelegation<CalculatorValue, T>;
            if (null != from)
                return from(v);
            return CalculatorValue.NullObject;
        }
        public static object ToObject<T>(T v)
        {
            var from = s_FromObject as FromGenericDelegation<object, T>;
            if (null != from)
                return from(v);
            return null;
        }
        public static string ToString<T>(T v)
        {
            var from = s_FromString as FromGenericDelegation<string, T>;
            if (null != from)
                return from(v);
            return null;
        }
        public static bool ToBool<T>(T v)
        {
            var from = s_FromBool as FromGenericDelegation<bool, T>;
            if (null != from)
                return from(v);
            return false;
        }
        public static char ToChar<T>(T v)
        {
            var from = s_FromChar as FromGenericDelegation<char, T>;
            if (null != from)
                return from(v);
            return (char)0;
        }
        public static sbyte ToSByte<T>(T v)
        {
            var from = s_FromSByte as FromGenericDelegation<sbyte, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static short ToShort<T>(T v)
        {
            var from = s_FromShort as FromGenericDelegation<short, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static int ToInt<T>(T v)
        {
            var from = s_FromInt as FromGenericDelegation<int, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static long ToLong<T>(T v)
        {
            var from = s_FromLong as FromGenericDelegation<long, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static byte ToByte<T>(T v)
        {
            var from = s_FromByte as FromGenericDelegation<byte, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static ushort ToUShort<T>(T v)
        {
            var from = s_FromUShort as FromGenericDelegation<ushort, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static uint ToUInt<T>(T v)
        {
            var from = s_FromUInt as FromGenericDelegation<uint, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static ulong ToULong<T>(T v)
        {
            var from = s_FromULong as FromGenericDelegation<ulong, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static float ToFloat<T>(T v)
        {
            var from = s_FromFloat as FromGenericDelegation<float, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static double ToDouble<T>(T v)
        {
            var from = s_FromDouble as FromGenericDelegation<double, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static decimal ToDecimal<T>(T v)
        {
            var from = s_FromDecimal as FromGenericDelegation<decimal, T>;
            if (null != from)
                return from(v);
            return 0;
        }
        public static Float2 ToFloat2<T>(T v)
        {
            var from = s_FromFloat2 as FromGenericDelegation<Float2, T>;
            if (null != from)
                return from(v);
            return Float2.zero;
        }
        public static Float3 ToFloat3<T>(T v)
        {
            var from = s_FromFloat3 as FromGenericDelegation<Float3, T>;
            if (null != from)
                return from(v);
            return Float3.zero;
        }
        public static Float4 ToFloat4<T>(T v)
        {
            var from = s_FromFloat4 as FromGenericDelegation<Float4, T>;
            if (null != from)
                return from(v);
            return Float4.zero;
        }
        public static Integer2 ToInteger2<T>(T v)
        {
            var from = s_FromInteger2 as FromGenericDelegation<Integer2, T>;
            if (null != from)
                return from(v);
            return Integer2.zero;
        }
        public static Integer3 ToInteger3<T>(T v)
        {
            var from = s_FromInteger3 as FromGenericDelegation<Integer3, T>;
            if (null != from)
                return from(v);
            return Integer3.zero;
        }
        public static Integer4 ToInteger4<T>(T v)
        {
            var from = s_FromInteger4 as FromGenericDelegation<Integer4, T>;
            if (null != from)
                return from(v);
            return Integer4.zero;
        }

        public static T From<T>(CalculatorValue v)
        {
            var from = s_FromBoxedValue as FromGenericDelegation<T, CalculatorValue>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(object v)
        {
            var from = s_FromObject as FromGenericDelegation<T, object>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(string v)
        {
            var from = s_FromString as FromGenericDelegation<T, string>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(bool v)
        {
            var from = s_FromBool as FromGenericDelegation<T, bool>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(char v)
        {
            var from = s_FromChar as FromGenericDelegation<T, char>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(sbyte v)
        {
            var from = s_FromSByte as FromGenericDelegation<T, sbyte>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(short v)
        {
            var from = s_FromShort as FromGenericDelegation<T, short>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(int v)
        {
            var from = s_FromInt as FromGenericDelegation<T, int>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(long v)
        {
            var from = s_FromLong as FromGenericDelegation<T, long>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(byte v)
        {
            var from = s_FromByte as FromGenericDelegation<T, byte>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(ushort v)
        {
            var from = s_FromUShort as FromGenericDelegation<T, ushort>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(uint v)
        {
            var from = s_FromUInt as FromGenericDelegation<T, uint>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(ulong v)
        {
            var from = s_FromULong as FromGenericDelegation<T, ulong>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(float v)
        {
            var from = s_FromFloat as FromGenericDelegation<T, float>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(double v)
        {
            var from = s_FromDouble as FromGenericDelegation<T, double>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(decimal v)
        {
            var from = s_FromDecimal as FromGenericDelegation<T, decimal>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(Float2 v)
        {
            var from = s_FromFloat2 as FromGenericDelegation<T, Float2>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(Float3 v)
        {
            var from = s_FromFloat3 as FromGenericDelegation<T, Float3>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(Float4 v)
        {
            var from = s_FromFloat4 as FromGenericDelegation<T, Float4>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(Integer2 v)
        {
            var from = s_FromInteger2 as FromGenericDelegation<T, Integer2>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(Integer3 v)
        {
            var from = s_FromInteger3 as FromGenericDelegation<T, Integer3>;
            if (null != from)
                return from(v);
            return default(T);
        }
        public static T From<T>(Integer4 v)
        {
            var from = s_FromInteger4 as FromGenericDelegation<T, Integer4>;
            if (null != from)
                return from(v);
            return default(T);
        }

        public static CalculatorValue ToBoxedValue(Type t, object o)
        {
            return CastTo<CalculatorValue>(o);
        }
        public static bool ToBool(Type t, object o)
        {
            return CastTo<bool>(o);
        }
        public static char ToChar(Type t, object o)
        {
            return CastTo<char>(o);
        }
        public static sbyte ToSByte(Type t, object o)
        {
            return CastTo<sbyte>(o);
        }
        public static short ToShort(Type t, object o)
        {
            return CastTo<short>(o);
        }
        public static int ToInt(Type t, object o)
        {
            return CastTo<int>(o);
        }
        public static long ToLong(Type t, object o)
        {
            return CastTo<long>(o);
        }
        public static byte ToByte(Type t, object o)
        {
            return CastTo<byte>(o);
        }
        public static ushort ToUShort(Type t, object o)
        {
            return CastTo<ushort>(o);
        }
        public static uint ToUInt(Type t, object o)
        {
            return CastTo<uint>(o);
        }
        public static ulong ToULong(Type t, object o)
        {
            return CastTo<ulong>(o);
        }
        public static float ToFloat(Type t, object o)
        {
            return CastTo<float>(o);
        }
        public static double ToDouble(Type t, object o)
        {
            return CastTo<double>(o);
        }
        public static decimal ToDecimal(Type t, object o)
        {
            return CastTo<decimal>(o);
        }
        public static string ToString(Type t, object o)
        {
            return CastTo<string>(o);
        }
        public static object ToObject(Type t, object o)
        {
            return o;
        }
        public static Float2 ToVector2(Type t, object o)
        {
            return CastTo<Float2>(o);
        }
        public static Float3 ToVector3(Type t, object o)
        {
            return CastTo<Float3>(o);
        }
        public static Float4 ToVector4(Type t, object o)
        {
            return CastTo<Float4>(o);
        }
        public static Integer2 ToQuaternion(Type t, object o)
        {
            return CastTo<Integer2>(o);
        }
        public static Integer3 ToColor(Type t, object o)
        {
            return CastTo<Integer3>(o);
        }
        public static Integer4 ToColor32(Type t, object o)
        {
            return CastTo<Integer4>(o);
        }
        public static object From(Type t, object o)
        {
            return o;
        }

        internal static T CastTo<T>(object obj)
        {
            if (obj is T) {
                return (T)obj;
            }
            else {
                try {
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
                catch {
                    return default(T);
                }
            }
        }

        private delegate R FromGenericDelegation<R, T>(T v);
        private static FromGenericDelegation<CalculatorValue, CalculatorValue> s_FromBoxedValue = FromHelper<CalculatorValue>;
        private static FromGenericDelegation<bool, bool> s_FromBool = FromHelper<bool>;
        private static FromGenericDelegation<char, char> s_FromChar = FromHelper<char>;
        private static FromGenericDelegation<sbyte, sbyte> s_FromSByte = FromHelper<sbyte>;
        private static FromGenericDelegation<short, short> s_FromShort = FromHelper<short>;
        private static FromGenericDelegation<int, int> s_FromInt = FromHelper<int>;
        private static FromGenericDelegation<long, long> s_FromLong = FromHelper<long>;
        private static FromGenericDelegation<byte, byte> s_FromByte = FromHelper<byte>;
        private static FromGenericDelegation<ushort, ushort> s_FromUShort = FromHelper<ushort>;
        private static FromGenericDelegation<uint, uint> s_FromUInt = FromHelper<uint>;
        private static FromGenericDelegation<ulong, ulong> s_FromULong = FromHelper<ulong>;
        private static FromGenericDelegation<float, float> s_FromFloat = FromHelper<float>;
        private static FromGenericDelegation<double, double> s_FromDouble = FromHelper<double>;
        private static FromGenericDelegation<decimal, decimal> s_FromDecimal = FromHelper<decimal>;
        private static FromGenericDelegation<string, string> s_FromString = FromHelper<string>;
        private static FromGenericDelegation<object, object> s_FromObject = FromHelper<object>;
        private static FromGenericDelegation<Float2, Float2> s_FromFloat2 = FromHelper<Float2>;
        private static FromGenericDelegation<Float3, Float3> s_FromFloat3 = FromHelper<Float3>;
        private static FromGenericDelegation<Float4, Float4> s_FromFloat4 = FromHelper<Float4>;
        private static FromGenericDelegation<Integer2, Integer2> s_FromInteger2 = FromHelper<Integer2>;
        private static FromGenericDelegation<Integer3, Integer3> s_FromInteger3 = FromHelper<Integer3>;
        private static FromGenericDelegation<Integer4, Integer4> s_FromInteger4 = FromHelper<Integer4>;
        private static T FromHelper<T>(T v)
        {
            return v;
        }
    }
    public struct CalculatorValue
    {
        public const int c_ObjectType = 0;
        public const int c_StringType = 1;
        public const int c_BoolType = 2;
        public const int c_CharType = 3;
        public const int c_SByteType = 4;
        public const int c_ShortType = 5;
        public const int c_IntType = 6;
        public const int c_LongType = 7;
        public const int c_ByteType = 8;
        public const int c_UShortType = 9;
        public const int c_UIntType = 10;
        public const int c_ULongType = 11;
        public const int c_FloatType = 12;
        public const int c_DoubleType = 13;
        public const int c_DecimalType = 14;
        public const int c_Float2Type = 15;
        public const int c_Float3Type = 16;
        public const int c_Float4Type = 17;
        public const int c_Integer2Type = 18;
        public const int c_Integer3Type = 19;
        public const int c_Integer4Type = 20;

        [StructLayout(LayoutKind.Explicit)]
        internal struct UnionValue
        {
            [FieldOffset(0)]
            public bool BoolVal;
            [FieldOffset(0)]
            public char CharVal;
            [FieldOffset(0)]
            public sbyte SByteVal;
            [FieldOffset(0)]
            public short ShortVal;
            [FieldOffset(0)]
            public int IntVal;
            [FieldOffset(0)]
            public long LongVal;
            [FieldOffset(0)]
            public byte ByteVal;
            [FieldOffset(0)]
            public ushort UShortVal;
            [FieldOffset(0)]
            public uint UIntVal;
            [FieldOffset(0)]
            public ulong ULongVal;
            [FieldOffset(0)]
            public float FloatVal;
            [FieldOffset(0)]
            public double DoubleVal;
            [FieldOffset(0)]
            public decimal DecimalVal;
            [FieldOffset(0)]
            public Float2 Float2Val;
            [FieldOffset(0)]
            public Float3 Float3Val;
            [FieldOffset(0)]
            public Float4 Float4Val;
            [FieldOffset(0)]
            public Integer2 Integer2Val;
            [FieldOffset(0)]
            public Integer3 Integer3Val;
            [FieldOffset(0)]
            public Integer4 Integer4Val;
        }

        public string StringVal
        {
            get { return ObjectVal as string; }
            set { ObjectVal = value; }
        }
        public int Type;
        public object ObjectVal;
        private UnionValue Union;

        public static implicit operator CalculatorValue(string v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator string(CalculatorValue v)
        {
            return v.Get<string>();
        }
        public static implicit operator CalculatorValue(bool v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator bool(CalculatorValue v)
        {
            return v.Get<bool>();
        }
        public static implicit operator CalculatorValue(char v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator char(CalculatorValue v)
        {
            return v.Get<char>();
        }
        public static implicit operator CalculatorValue(sbyte v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator sbyte(CalculatorValue v)
        {
            return v.Get<sbyte>();
        }
        public static implicit operator CalculatorValue(short v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator short(CalculatorValue v)
        {
            return v.Get<short>();
        }
        public static implicit operator CalculatorValue(int v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator int(CalculatorValue v)
        {
            return v.Get<int>();
        }
        public static implicit operator CalculatorValue(long v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator long(CalculatorValue v)
        {
            return v.Get<long>();
        }
        public static implicit operator CalculatorValue(byte v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator byte(CalculatorValue v)
        {
            return v.Get<byte>();
        }
        public static implicit operator CalculatorValue(ushort v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator ushort(CalculatorValue v)
        {
            return v.Get<ushort>();
        }
        public static implicit operator CalculatorValue(uint v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator uint(CalculatorValue v)
        {
            return v.Get<uint>();
        }
        public static implicit operator CalculatorValue(ulong v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator ulong(CalculatorValue v)
        {
            return v.Get<ulong>();
        }
        public static implicit operator CalculatorValue(float v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator float(CalculatorValue v)
        {
            return v.Get<float>();
        }
        public static implicit operator CalculatorValue(double v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator double(CalculatorValue v)
        {
            return v.Get<double>();
        }
        public static implicit operator CalculatorValue(decimal v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator decimal(CalculatorValue v)
        {
            return v.Get<decimal>();
        }
        public static implicit operator CalculatorValue(Float2 v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator Float2(CalculatorValue v)
        {
            return v.Get<Float2>();
        }
        public static implicit operator CalculatorValue(Float3 v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator Float3(CalculatorValue v)
        {
            return v.Get<Float3>();
        }
        public static implicit operator CalculatorValue(Float4 v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator Float4(CalculatorValue v)
        {
            return v.Get<Float4>();
        }
        public static implicit operator CalculatorValue(Integer2 v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator Integer2(CalculatorValue v)
        {
            return v.Get<Integer2>();
        }
        public static implicit operator CalculatorValue(Integer3 v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator Integer3(CalculatorValue v)
        {
            return v.Get<Integer3>();
        }
        public static implicit operator CalculatorValue(Integer4 v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator Integer4(CalculatorValue v)
        {
            return v.Get<Integer4>();
        }

        public static implicit operator CalculatorValue(Type v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator Type(CalculatorValue v)
        {
            return v.ObjectVal as Type;
        }
        public static implicit operator CalculatorValue(ArrayList v)
        {
            return CalculatorValue.From(v);
        }
        public static implicit operator ArrayList(CalculatorValue v)
        {
            return v.ObjectVal as ArrayList;
        }

        public string GetTypeName()
        {
            switch (Type) {
                case c_ObjectType:
                    return "object";
                case c_StringType:
                    return "string";
                case c_BoolType:
                    return "bool";
                case c_CharType:
                    return "char";
                case c_SByteType:
                    return "sbyte";
                case c_ShortType:
                    return "short";
                case c_IntType:
                    return "int";
                case c_LongType:
                    return "long";
                case c_ByteType:
                    return "byte";
                case c_UShortType:
                    return "ushort";
                case c_UIntType:
                    return "uint";
                case c_ULongType:
                    return "ulong";
                case c_FloatType:
                    return "float";
                case c_DoubleType:
                    return "double";
                case c_DecimalType:
                    return "decimal";
                case c_Float2Type:
                    return "Float2";
                case c_Float3Type:
                    return "Float3";
                case c_Float4Type:
                    return "Float4";
                case c_Integer2Type:
                    return "Integer2";
                case c_Integer3Type:
                    return "Integer3";
                case c_Integer4Type:
                    return "Integer4";
                default:
                    return "Unknown";
            }
        }

        public bool IsNullObject
        {
            get { return Type == c_ObjectType && ObjectVal == null; }
        }
        public bool IsNullOrEmptyString
        {
            get { return Type == c_StringType && string.IsNullOrEmpty(StringVal); }
        }
        public bool IsObject
        {
            get {
                return Type == c_ObjectType;
            }
        }
        public bool IsString
        {
            get {
                return Type == c_StringType;
            }
        }
        public bool IsBoolean
        {
            get {
                return Type == c_BoolType;
            }
        }
        public bool IsChar
        {
            get {
                return Type == c_CharType;
            }
        }
        public bool IsInteger
        {
            get {
                switch (Type) {
                    case c_SByteType:
                    case c_ShortType:
                    case c_IntType:
                    case c_LongType:
                    case c_ByteType:
                    case c_UShortType:
                    case c_UIntType:
                    case c_ULongType:
                        return true;
                    default:
                        return false;
                }
            }
        }
        public bool IsFloat
        {
            get {
                return Type == c_FloatType || Type == c_DoubleType || Type == c_DecimalType;
            }
        }
        public string AsString
        {
            get {
                return IsString ? StringVal : (IsObject ? ObjectVal as string : null);
            }
        }
        public T As<T>() where T : class
        {
            return IsObject || IsString ? ObjectVal as T : null;
        }
        public object As(Type t)
        {
            if (null == ObjectVal) {
                return null;
            }
            else if (IsObject || IsString) {
                Type st = ObjectVal.GetType();
                if (t.IsAssignableFrom(st) || st.IsSubclassOf(t))
                    return ObjectVal;
                else
                    return null;
            }
            else {
                return null;
            }
        }

        public void SetNullObject()
        {
            Type = c_ObjectType;
            ObjectVal = null;
        }
        public void SetNullString()
        {
            Type = c_StringType;
            StringVal = null;
        }
        public void SetEmptyString()
        {
            Type = c_StringType;
            StringVal = string.Empty;
        }
        public void Set<T>(T v)
        {
            var t = v != null ? v.GetType() : typeof(T);
            Set<T>(t, v);
        }
        public void Set(Type t, object v)
        {
            Set<object>(t, v);
        }
        public T Get<T>()
        {
            var t = typeof(T);
            return Get<T>(t);
        }
        public object Get(Type t)
        {
            return Get<object>(t);
        }
        //供lua或防止隐式转换出问题时使用
        public void SetBool(bool v)
        {
            Set(v);
        }
        public void SetNumber(double v)
        {
            Set(v);
        }
        public void SetString(string v)
        {
            Set(v);
        }
        public void SetObject(object v)
        {
            Set(v);
        }
        public bool GetBool()
        {
            return Get<bool>();
        }
        public double GetNumber()
        {
            return Get<double>();
        }
        public string GetString()
        {
            return Get<string>();
        }
        public object GetObject()
        {
            return Get<object>();
        }

        public void CopyFrom(CalculatorValue other)
        {
            Type = other.Type;
            switch (Type) {
                case c_ObjectType:
                    ObjectVal = other.ObjectVal;
                    break;
                case c_StringType:
                    StringVal = other.StringVal;
                    break;
                case c_BoolType:
                    Union.BoolVal = other.Union.BoolVal;
                    break;
                case c_CharType:
                    Union.CharVal = other.Union.CharVal;
                    break;
                case c_SByteType:
                    Union.SByteVal = other.Union.SByteVal;
                    break;
                case c_ShortType:
                    Union.ShortVal = other.Union.ShortVal;
                    break;
                case c_IntType:
                    Union.IntVal = other.Union.IntVal;
                    break;
                case c_LongType:
                    Union.LongVal = other.Union.LongVal;
                    break;
                case c_ByteType:
                    Union.ByteVal = other.Union.ByteVal;
                    break;
                case c_UShortType:
                    Union.UShortVal = other.Union.UShortVal;
                    break;
                case c_UIntType:
                    Union.UIntVal = other.Union.UIntVal;
                    break;
                case c_ULongType:
                    Union.ULongVal = other.Union.ULongVal;
                    break;
                case c_FloatType:
                    Union.FloatVal = other.Union.FloatVal;
                    break;
                case c_DoubleType:
                    Union.DoubleVal = other.Union.DoubleVal;
                    break;
                case c_DecimalType:
                    Union.DecimalVal = other.Union.DecimalVal;
                    break;
                case c_Float2Type:
                    Union.Float2Val = other.Union.Float2Val;
                    break;
                case c_Float3Type:
                    Union.Float3Val = other.Union.Float3Val;
                    break;
                case c_Float4Type:
                    Union.Float4Val = other.Union.Float4Val;
                    break;
                case c_Integer2Type:
                    Union.Integer2Val = other.Union.Integer2Val;
                    break;
                case c_Integer3Type:
                    Union.Integer3Val = other.Union.Integer3Val;
                    break;
                case c_Integer4Type:
                    Union.Integer4Val = other.Union.Integer4Val;
                    break;
            }
        }
        public override string ToString()
        {
            switch (Type) {
                case c_ObjectType:
                    return null != ObjectVal ? ObjectVal.ToString() : string.Empty;
                case c_StringType:
                    return null != StringVal ? StringVal : string.Empty;
                case c_BoolType:
                    return Union.BoolVal.ToString();
                case c_CharType:
                    return Union.CharVal.ToString();
                case c_SByteType:
                    return Union.SByteVal.ToString();
                case c_ShortType:
                    return Union.ShortVal.ToString();
                case c_IntType:
                    return Union.IntVal.ToString();
                case c_LongType:
                    return Union.LongVal.ToString();
                case c_ByteType:
                    return Union.ByteVal.ToString();
                case c_UShortType:
                    return Union.UShortVal.ToString();
                case c_UIntType:
                    return Union.UIntVal.ToString();
                case c_ULongType:
                    return Union.ULongVal.ToString();
                case c_FloatType:
                    return Union.FloatVal.ToString();
                case c_DoubleType:
                    return Union.DoubleVal.ToString();
                case c_DecimalType:
                    return Union.DecimalVal.ToString();
                case c_Float2Type:
                    return Union.Float2Val.ToString();
                case c_Float3Type:
                    return Union.Float3Val.ToString();
                case c_Float4Type:
                    return Union.Float4Val.ToString();
                case c_Integer2Type:
                    return Union.Integer2Val.ToString();
                case c_Integer3Type:
                    return Union.Integer3Val.ToString();
                case c_Integer4Type:
                    return Union.Integer4Val.ToString();
            }
            return string.Empty;
        }

        private void Set<T>(Type t, T v)
        {
            if (typeof(T) == typeof(object)) {

                if (t == typeof(CalculatorValue)) {
                    var cv = (CalculatorValue)(object)v;
                    CopyFrom(cv);
                }
                else if (t == typeof(bool)) {
                    var cv = (bool)(object)v;
                    Type = c_BoolType;
                    Union.BoolVal = cv;
                }
                else if (t == typeof(char)) {
                    var cv = (char)(object)v;
                    Type = c_CharType;
                    Union.CharVal = cv;
                }
                else if (t == typeof(sbyte)) {
                    var cv = (sbyte)(object)v;
                    Type = c_SByteType;
                    Union.SByteVal = cv;
                }
                else if (t == typeof(short)) {
                    var cv = (short)(object)v;
                    Type = c_ShortType;
                    Union.ShortVal = cv;
                }
                else if (t == typeof(int)) {
                    var cv = (int)(object)v;
                    Type = c_IntType;
                    Union.IntVal = cv;
                }
                else if (t == typeof(long)) {
                    var cv = (long)(object)v;
                    Type = c_LongType;
                    Union.LongVal = cv;
                }
                else if (t == typeof(byte)) {
                    var cv = (byte)(object)v;
                    Type = c_ByteType;
                    Union.ByteVal = cv;
                }
                else if (t == typeof(ushort)) {
                    var cv = (ushort)(object)v;
                    Type = c_UShortType;
                    Union.UShortVal = cv;
                }
                else if (t == typeof(uint)) {
                    var cv = (uint)(object)v;
                    Type = c_UIntType;
                    Union.UIntVal = cv;
                }
                else if (t == typeof(ulong)) {
                    var cv = (ulong)(object)v;
                    Type = c_ULongType;
                    Union.ULongVal = cv;
                }
                else if (t == typeof(float)) {
                    var cv = (float)(object)v;
                    Type = c_FloatType;
                    Union.FloatVal = cv;
                }
                else if (t == typeof(double)) {
                    var cv = (double)(object)v;
                    Type = c_DoubleType;
                    Union.DoubleVal = cv;
                }
                else if (t == typeof(decimal)) {
                    var cv = (decimal)(object)v;
                    Type = c_DecimalType;
                    Union.DecimalVal = cv;
                }
                else if (t == typeof(Float2)) {
                    var cv = (Float2)(object)v;
                    Type = c_Float2Type;
                    Union.Float2Val = cv;
                }
                else if (t == typeof(Float3)) {
                    var cv = (Float3)(object)v;
                    Type = c_Float3Type;
                    Union.Float3Val = cv;
                }
                else if (t == typeof(Float4)) {
                    var cv = (Float4)(object)v;
                    Type = c_Float4Type;
                    Union.Float4Val = cv;
                }
                else if (t == typeof(Integer2)) {
                    var cv = (Integer2)(object)v;
                    Type = c_Integer2Type;
                    Union.Integer2Val = cv;
                }
                else if (t == typeof(Integer3)) {
                    var cv = (Integer3)(object)v;
                    Type = c_Integer3Type;
                    Union.Integer3Val = cv;
                }
                else if (t == typeof(Integer4)) {
                    var cv = (Integer4)(object)v;
                    Type = c_Integer4Type;
                    Union.Integer4Val = cv;
                }
                else if (t == typeof(string)) {
                    var cv = (string)(object)v;
                    Type = c_StringType;
                    ObjectVal = cv;
                }
                else {
                    object vObj = v;
                    Type = c_ObjectType;
                    ObjectVal = vObj;
                }
            }
            else {
                if (t == typeof(CalculatorValue)) {
                    var cv = CalculatorValueConverter.ToCalculatorValue(v);
                    CopyFrom(cv);
                }
                else if (t == typeof(bool)) {
                    var cv = CalculatorValueConverter.ToBool(v);
                    Type = c_BoolType;
                    Union.BoolVal = cv;
                }
                else if (t == typeof(char)) {
                    var cv = CalculatorValueConverter.ToChar(v);
                    Type = c_CharType;
                    Union.CharVal = cv;
                }
                else if (t == typeof(sbyte)) {
                    var cv = CalculatorValueConverter.ToSByte(v);
                    Type = c_SByteType;
                    Union.SByteVal = cv;
                }
                else if (t == typeof(short)) {
                    var cv = CalculatorValueConverter.ToShort(v);
                    Type = c_ShortType;
                    Union.ShortVal = cv;
                }
                else if (t == typeof(int)) {
                    var cv = CalculatorValueConverter.ToInt(v);
                    Type = c_IntType;
                    Union.IntVal = cv;
                }
                else if (t == typeof(long)) {
                    var cv = CalculatorValueConverter.ToLong(v);
                    Type = c_LongType;
                    Union.LongVal = cv;
                }
                else if (t == typeof(byte)) {
                    var cv = CalculatorValueConverter.ToByte(v);
                    Type = c_ByteType;
                    Union.ByteVal = cv;
                }
                else if (t == typeof(ushort)) {
                    var cv = CalculatorValueConverter.ToUShort(v);
                    Type = c_UShortType;
                    Union.UShortVal = cv;
                }
                else if (t == typeof(uint)) {
                    var cv = CalculatorValueConverter.ToUInt(v);
                    Type = c_UIntType;
                    Union.UIntVal = cv;
                }
                else if (t == typeof(ulong)) {
                    var cv = CalculatorValueConverter.ToULong(v);
                    Type = c_ULongType;
                    Union.ULongVal = cv;
                }
                else if (t == typeof(float)) {
                    var cv = CalculatorValueConverter.ToFloat(v);
                    Type = c_FloatType;
                    Union.FloatVal = cv;
                }
                else if (t == typeof(double)) {
                    var cv = CalculatorValueConverter.ToDouble(v);
                    Type = c_DoubleType;
                    Union.DoubleVal = cv;
                }
                else if (t == typeof(decimal)) {
                    var cv = CalculatorValueConverter.ToDecimal(v);
                    Type = c_DecimalType;
                    Union.DecimalVal = cv;
                }
                else if (t == typeof(Float2)) {
                    var cv = CalculatorValueConverter.ToFloat2(v);
                    Type = c_Float2Type;
                    Union.Float2Val = cv;
                }
                else if (t == typeof(Float3)) {
                    var cv = CalculatorValueConverter.ToFloat3(v);
                    Type = c_Float3Type;
                    Union.Float3Val = cv;
                }
                else if (t == typeof(Float4)) {
                    var cv = CalculatorValueConverter.ToFloat4(v);
                    Type = c_Float4Type;
                    Union.Float4Val = cv;
                }
                else if (t == typeof(Integer2)) {
                    var cv = CalculatorValueConverter.ToInteger2(v);
                    Type = c_Integer2Type;
                    Union.Integer2Val = cv;
                }
                else if (t == typeof(Integer3)) {
                    var cv = CalculatorValueConverter.ToInteger3(v);
                    Type = c_Integer3Type;
                    Union.Integer3Val = cv;
                }
                else if (t == typeof(Integer4)) {
                    var cv = CalculatorValueConverter.ToInteger4(v);
                    Type = c_Integer4Type;
                    Union.Integer4Val = cv;
                }
                else if (t == typeof(string)) {
                    var cv = CalculatorValueConverter.ToString(v);
                    Type = c_StringType;
                    ObjectVal = cv;
                }
                else {
                    object vObj = v;
                    Type = c_ObjectType;
                    ObjectVal = vObj;
                }
            }
        }
        private T Get<T>(Type t)
        {
            if (typeof(T) == typeof(object)) {
                var obj = ToObject();
                return CalculatorValueConverter.CastTo<T>(obj);
            }
            else if (Type == c_StringType) {
                if (t == typeof(CalculatorValue)) {
                    return CalculatorValueConverter.From<T>(this);
                }
                else if (t == typeof(string)) {
                    return CalculatorValueConverter.From<T>(StringVal);
                }
                else if (t == typeof(object)) {
                    return CalculatorValueConverter.From<T>(ObjectVal);
                }
                else {
                    return CalculatorValueConverter.CastTo<T>(ObjectVal);
                }
            }
            else {
                if (t == typeof(CalculatorValue)) {
                    return CalculatorValueConverter.From<T>(this);
                }
                else if (t == typeof(bool) && Type == c_BoolType) {
                    return CalculatorValueConverter.From<T>(Union.BoolVal);
                }
                else if (t == typeof(char) && Type == c_CharType) {
                    return CalculatorValueConverter.From<T>(Union.CharVal);
                }
                else if (t == typeof(sbyte) && Type == c_SByteType) {
                    return CalculatorValueConverter.From<T>(Union.SByteVal);
                }
                else if (t == typeof(short) && Type == c_ShortType) {
                    return CalculatorValueConverter.From<T>(Union.ShortVal);
                }
                else if (t == typeof(int) && Type == c_IntType) {
                    return CalculatorValueConverter.From<T>(Union.IntVal);
                }
                else if (t == typeof(long) && Type == c_LongType) {
                    return CalculatorValueConverter.From<T>(Union.LongVal);
                }
                else if (t == typeof(byte) && Type == c_ByteType) {
                    return CalculatorValueConverter.From<T>(Union.ByteVal);
                }
                else if (t == typeof(ushort) && Type == c_UShortType) {
                    return CalculatorValueConverter.From<T>(Union.UShortVal);
                }
                else if (t == typeof(uint) && Type == c_UIntType) {
                    return CalculatorValueConverter.From<T>(Union.UIntVal);
                }
                else if (t == typeof(ulong) && Type == c_ULongType) {
                    return CalculatorValueConverter.From<T>(Union.ULongVal);
                }
                else if (t == typeof(float) && Type == c_FloatType) {
                    return CalculatorValueConverter.From<T>(Union.FloatVal);
                }
                else if (t == typeof(double) && Type == c_DoubleType) {
                    return CalculatorValueConverter.From<T>(Union.DoubleVal);
                }
                else if (t == typeof(decimal) && Type == c_DecimalType) {
                    return CalculatorValueConverter.From<T>(Union.DecimalVal);
                }
                else if (t == typeof(Float2) && Type == c_Float2Type) {
                    return CalculatorValueConverter.From<T>(Union.Float2Val);
                }
                else if (t == typeof(Float3) && Type == c_Float3Type) {
                    return CalculatorValueConverter.From<T>(Union.Float3Val);
                }
                else if (t == typeof(Float4) && Type == c_Float4Type) {
                    return CalculatorValueConverter.From<T>(Union.Float4Val);
                }
                else if (t == typeof(Integer2) && Type == c_Integer2Type) {
                    return CalculatorValueConverter.From<T>(Union.Integer2Val);
                }
                else if (t == typeof(Integer3) && Type == c_Integer3Type) {
                    return CalculatorValueConverter.From<T>(Union.Integer3Val);
                }
                else if (t == typeof(Integer4) && Type == c_Integer4Type) {
                    return CalculatorValueConverter.From<T>(Union.Integer4Val);
                }
                else if (t == typeof(bool)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>(v != 0);
                }
                else if (t == typeof(char)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((char)v);
                }
                else if (t == typeof(sbyte)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((sbyte)v);
                }
                else if (t == typeof(short)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((short)v);
                }
                else if (t == typeof(int)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((int)v);
                }
                else if (t == typeof(long)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>(v);
                }
                else if (t == typeof(byte)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((byte)v);
                }
                else if (t == typeof(ushort)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((ushort)v);
                }
                else if (t == typeof(uint)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((uint)v);
                }
                else if (t == typeof(ulong)) {
                    long v = ToLong();
                    return CalculatorValueConverter.From<T>((ulong)v);
                }
                else if (t == typeof(float)) {
                    double v = ToDouble();
                    return CalculatorValueConverter.From<T>((float)v);
                }
                else if (t == typeof(double)) {
                    double v = ToDouble();
                    return CalculatorValueConverter.From<T>(v);
                }
                else if (t == typeof(decimal)) {
                    double v = ToDouble();
                    return CalculatorValueConverter.From<T>((decimal)v);
                }
                else if (t == typeof(string) && Type == c_StringType) {
                    return CalculatorValueConverter.From<T>(StringVal);
                }
                else if (t == typeof(object) && Type == c_ObjectType) {
                    return CalculatorValueConverter.From<T>(ObjectVal);
                }
                else if (t == typeof(string)) {
                    var str = ToString();
                    return CalculatorValueConverter.From<T>(str);
                }
                else if (t == typeof(object)) {
                    var obj = ToObject();
                    return CalculatorValueConverter.From<T>(obj);
                }
                else {
                    var obj = ToObject();
                    return CalculatorValueConverter.CastTo<T>(obj);
                }
            }
        }
        private object ToObject()
        {
            switch (Type) {
                case c_ObjectType:
                case c_StringType:
                    return ObjectVal;
                case c_BoolType:
                    return Union.BoolVal;
                case c_CharType:
                    return Union.CharVal;
                case c_SByteType:
                    return Union.SByteVal;
                case c_ShortType:
                    return Union.ShortVal;
                case c_IntType:
                    return Union.IntVal;
                case c_LongType:
                    return Union.LongVal;
                case c_ByteType:
                    return Union.ByteVal;
                case c_UShortType:
                    return Union.UShortVal;
                case c_UIntType:
                    return Union.UIntVal;
                case c_ULongType:
                    return Union.ULongVal;
                case c_FloatType:
                    return Union.FloatVal;
                case c_DoubleType:
                    return Union.DoubleVal;
                case c_DecimalType:
                    return Union.DecimalVal;
                case c_Float2Type:
                    return Union.Float2Val;
                case c_Float3Type:
                    return Union.Float3Val;
                case c_Float4Type:
                    return Union.Float4Val;
                case c_Integer2Type:
                    return Union.Integer2Val;
                case c_Integer3Type:
                    return Union.Integer3Val;
                case c_Integer4Type:
                    return Union.Integer4Val;
            }
            return null;
        }
        private bool ToBool()
        {
            if (Type == c_BoolType)
                return Union.BoolVal;
            else
                return ToLong() != 0;
        }
        private char ToChar()
        {
            if (Type == c_CharType)
                return Union.CharVal;
            else
                return (char)(ulong)ToLong();
        }
        private long ToLong()
        {
            long v = 0;
            switch (Type) {
                case c_BoolType:
                    return Union.BoolVal ? 1 : 0;
                case c_CharType:
                    return Union.CharVal;
                case c_SByteType:
                    return Union.SByteVal;
                case c_ShortType:
                    return Union.ShortVal;
                case c_IntType:
                    return Union.IntVal;
                case c_LongType:
                    return Union.LongVal;
                case c_ByteType:
                    return Union.ByteVal;
                case c_UShortType:
                    return Union.UShortVal;
                case c_UIntType:
                    return Union.UIntVal;
                case c_ULongType:
                    return (long)Union.ULongVal;
                case c_FloatType:
                    return (long)Union.FloatVal;
                case c_DoubleType:
                    return (long)Union.DoubleVal;
                case c_DecimalType:
                    return (long)Union.DecimalVal;
                case c_StringType:
                    if (null != StringVal) {
                        long.TryParse(StringVal, out v);
                    }
                    return v;
                case c_ObjectType:
                    if (null != ObjectVal) {
                        v = CalculatorValueConverter.CastTo<long>(ObjectVal);
                    }
                    return v;
            }
            return v;
        }
        private double ToDouble()
        {
            double v = 0;
            switch (Type) {
                case c_BoolType:
                    return Union.BoolVal ? 1 : 0;
                case c_CharType:
                    return Union.CharVal;
                case c_SByteType:
                    return Union.SByteVal;
                case c_ShortType:
                    return Union.ShortVal;
                case c_IntType:
                    return Union.IntVal;
                case c_LongType:
                    return Union.LongVal;
                case c_ByteType:
                    return Union.ByteVal;
                case c_UShortType:
                    return Union.UShortVal;
                case c_UIntType:
                    return Union.UIntVal;
                case c_ULongType:
                    return Union.ULongVal;
                case c_FloatType:
                    return Union.FloatVal;
                case c_DoubleType:
                    return Union.DoubleVal;
                case c_DecimalType:
                    return (double)Union.DecimalVal;
                case c_StringType:
                    if (null != StringVal) {
                        double.TryParse(StringVal, out v);
                    }
                    return v;
                case c_ObjectType:
                    if (null != ObjectVal) {
                        v = CalculatorValueConverter.CastTo<double>(ObjectVal);
                    }
                    return v;
            }
            return v;
        }

        public static CalculatorValue From<T>(T v)
        {
            CalculatorValue bv = new CalculatorValue();
            bv.Set(v);
            return bv;
        }
        public static CalculatorValue From(Type t, object o)
        {
            CalculatorValue bv = new CalculatorValue();
            bv.Set(o);
            return bv;
        }
        //供lua或防止隐式转换出问题时使用
        public static CalculatorValue FromBool(bool v)
        {
            CalculatorValue bv = new CalculatorValue();
            bv.Set(v);
            return bv;
        }
        public static CalculatorValue FromNumber(double v)
        {
            CalculatorValue bv = new CalculatorValue();
            bv.Set(v);
            return bv;
        }
        public static CalculatorValue FromString(string v)
        {
            CalculatorValue bv = new CalculatorValue();
            bv.Set(v);
            return bv;
        }
        public static CalculatorValue FromObject(object v)
        {
            CalculatorValue bv = new CalculatorValue();
            bv.Set(v);
            return bv;
        }

        public static CalculatorValue NullObject
        {
            get { return s_NullObject; }
        }
        public static CalculatorValue EmptyString
        {
            get { return s_EmptyString; }
        }
        private static CalculatorValue s_NullObject = CalculatorValue.FromObject(null);
        private static CalculatorValue s_EmptyString = CalculatorValue.FromString(string.Empty);
    }
    public class CalculatorValueListPool
    {
        public List<CalculatorValue> Alloc()
        {
            if (m_Pool.Count > 0)
                return m_Pool.Dequeue();
            else
                return new List<CalculatorValue>();
        }
        public void Recycle(List<CalculatorValue> list)
        {
            if (null != list) {
                m_Pool.Enqueue(list);
            }
        }
        public void Clear()
        {
            m_Pool.Clear();
        }
        public CalculatorValueListPool(int initCapacity)
        {
            m_Pool = new Queue<List<CalculatorValue>>(initCapacity);
        }

        private Queue<List<CalculatorValue>> m_Pool = null;
    }
    public interface IExpression
    {
        CalculatorValue Calc();
        bool Load(Dsl.ISyntaxComponent dsl, DslCalculator calculator);
    }
    public interface IExpressionFactory
    {
        IExpression Create();
    }
    public sealed class ExpressionFactoryHelper<T> : IExpressionFactory where T : IExpression, new()
    {
        public IExpression Create()
        {
            return new T();
        }
    }
    public abstract class AbstractExpression : IExpression
    {
        public CalculatorValue Calc()
        {
            CalculatorValue ret = CalculatorValue.NullObject;
            try {
                ret = DoCalc();
            }
            catch (Exception ex) {
                var msg = string.Format("calc:[{0}]", ToString());
                throw new Exception(msg, ex);
            }
            return ret;
        }
        public bool Load(Dsl.ISyntaxComponent dsl, DslCalculator calculator)
        {
            m_Calculator = calculator;
            m_Dsl = dsl;
            Dsl.ValueData valueData = dsl as Dsl.ValueData;
            if (null != valueData) {
                return Load(valueData);
            }
            else {
                Dsl.FunctionData funcData = dsl as Dsl.FunctionData;
                if (null != funcData) {
                    if (funcData.HaveParam()) {
                        var callData = funcData;
                        bool ret = Load(callData);
                        if (!ret) {
                            int num = callData.GetParamNum();
                            List<IExpression> args = new List<IExpression>();
                            for (int ix = 0; ix < num; ++ix) {
                                Dsl.ISyntaxComponent param = callData.GetParam(ix);
                                args.Add(calculator.Load(param));
                            }
                            return Load(args);
                        }
                        return ret;
                    }
                    else {
                        return Load(funcData);
                    }
                }
                else {
                    Dsl.StatementData statementData = dsl as Dsl.StatementData;
                    if (null != statementData) {
                        return Load(statementData);
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return string.Format("{0} line:{1}", base.ToString(), m_Dsl.GetLine());
        }
        protected virtual bool Load(Dsl.ValueData valData) { return false; }
        protected virtual bool Load(IList<IExpression> exps) { return false; }
        protected virtual bool Load(Dsl.FunctionData funcData) { return false; }
        protected virtual bool Load(Dsl.StatementData statementData) { return false; }
        protected abstract CalculatorValue DoCalc();

        protected DslCalculator Calculator
        {
            get { return m_Calculator; }
        }

        private DslCalculator m_Calculator = null;
        private Dsl.ISyntaxComponent m_Dsl = null;

        protected static void CastArgsForCall(Type t, string method, BindingFlags flags, params object[] args)
        {
            var mis = t.GetMember(method, flags);
            foreach (var mi in mis) {
                var info = mi as MethodInfo;
                if (null != info) {
                    var pis = info.GetParameters();
                    if (pis.Length == args.Length) {
                        for (int i = 0; i < pis.Length; ++i) {
                            if (null != args[i] && args[i].GetType() != pis[i].ParameterType && args[i].GetType().Name != "MonoType") {
                                args[i] = CastTo(pis[i].ParameterType, args[i]);
                            }
                        }
                        break;
                    }
                }
            }
        }
        protected static void CastArgsForSet(Type t, string property, BindingFlags flags, params object[] args)
        {
            var p = t.GetProperty(property, flags);
            if (null != p) {
                var info = p.GetSetMethod(true);
                if (null != info) {
                    var pis = info.GetParameters();
                    if (pis.Length == args.Length) {
                        for (int i = 0; i < pis.Length; ++i) {
                            if (null != args[i] && args[i].GetType() != pis[i].ParameterType && args[i].GetType().Name != "MonoType") {
                                args[i] = CastTo(pis[i].ParameterType, args[i]);
                            }
                        }
                    }
                }
            }
            else {
                var f = t.GetField(property, flags);
                if (null != f && args.Length == 1 && null != args[0] && args[0].GetType() != f.FieldType && args[0].GetType().Name != "MonoType") {
                    args[0] = CastTo(f.FieldType, args[0]);
                }
            }
        }
        protected static void CastArgsForGet(Type t, string property, BindingFlags flags, params object[] args)
        {
            var p = t.GetProperty(property, flags);
            if (null != p) {
                var info = p.GetGetMethod(true);
                if (null != info) {
                    var pis = info.GetParameters();
                    if (pis.Length == args.Length) {
                        for (int i = 0; i < pis.Length; ++i) {
                            if (null != args[i] && args[i].GetType() != pis[i].ParameterType && args[i].GetType().Name != "MonoType") {
                                args[i] = CastTo(pis[i].ParameterType, args[i]);
                            }
                        }
                    }
                }
            }
            else {
                var f = t.GetField(property, flags);
                if (null != f && args.Length == 0) {
                }
            }
        }
        protected static T CastTo<T>(object obj)
        {
            if (obj is CalculatorValue) {
                return ((CalculatorValue)obj).Get<T>();
            }
            else if (obj is T) {
                return (T)obj;
            }
            else {
                try {
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
                catch {
                    return default(T);
                }
            }
        }
        protected static object CastTo(Type t, object obj)
        {
            if (null == obj)
                return null;
            Type st = obj.GetType();
            if (obj is CalculatorValue) {
                return ((CalculatorValue)obj).Get(t);
            }
            else if (t.IsAssignableFrom(st) || st.IsSubclassOf(t)) {
                return obj;
            }
            else {
                try {
                    return Convert.ChangeType(obj, t);
                }
                catch {
                    return null;
                }
            }
        }
        protected static Encoding GetEncoding(CalculatorValue v)
        {
            var name = v.AsString;
            if (null != name) {
                return Encoding.GetEncoding(name);
            }
            else if (v.IsInteger) {
                int codePage = v.Get<int>();
                return Encoding.GetEncoding(codePage);
            }
            else {
                return Encoding.UTF8;
            }
        }
    }
    public abstract class SimpleExpressionBase : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var operands = Calculator.NewCalculatorValueList();
            for (int i = 0; i < m_Exps.Count; ++i) {
                var v = m_Exps[i].Calc();
                operands.Add(v);
            }
            var r = OnCalc(operands);
            Calculator.RecycleCalculatorValueList(operands);
            return r;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Exps = exps;
            return true;
        }
        protected abstract CalculatorValue OnCalc(IList<CalculatorValue> operands);

        private IList<IExpression> m_Exps = null;
    }
    internal sealed class ArgsGet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue ret = CalculatorValue.FromObject(Calculator.Arguments);
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            return true;
        }
    }
    internal sealed class ArgGet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            var ix = m_ArgIndex.Calc().Get<int>();
            var args = Calculator.Arguments;
            if (ix >= 0 && ix < args.Count) {
                ret = args[ix];
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            m_ArgIndex = Calculator.Load(callData.GetParam(0));
            return true;
        }

        private IExpression m_ArgIndex;
    }
    internal sealed class ArgNumGet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = Calculator.Arguments.Count;
            return CalculatorValue.From(ret);
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            return true;
        }
    }
    internal sealed class VarSet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var varId = m_VarId.Calc();
            CalculatorValue v = m_Op.Calc();
            if (varId.IsInteger) {
                int id = varId.Get<int>();
                Calculator.SetVariable(id, v);
            }
            else {
                var str = varId.AsString;
                if (null != str) {
                    Calculator.SetVariable(str, v);
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            Dsl.FunctionData param1 = callData.GetParam(0) as Dsl.FunctionData;
            Dsl.ISyntaxComponent param2 = callData.GetParam(1);
            m_VarId = Calculator.Load(param1.GetParam(0));
            m_Op = Calculator.Load(param2);
            return true;
        }

        private IExpression m_VarId;
        private IExpression m_Op;
    }
    internal sealed class VarGet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var varId = m_VarId.Calc();
            CalculatorValue v = CalculatorValue.NullObject;
            if (varId.IsInteger) {
                int id = varId.Get<int>();
                v = Calculator.GetVariable(id);
            }
            else {
                var str = varId.AsString;
                if (null != str) {
                    v = Calculator.GetVariable(str);
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            m_VarId = Calculator.Load(callData.GetParam(0));
            return true;
        }

        private IExpression m_VarId;
    }
    internal sealed class NamedVarSet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = m_Op.Calc();
            if (m_VarId.Length > 0) {
                Calculator.SetVariable(m_VarId, v);
                if (!v.IsNullObject && m_VarId[0] != '@' && m_VarId[0] != '$') {
                    Environment.SetEnvironmentVariable(m_VarId, v.ToString());
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            Dsl.ISyntaxComponent param1 = callData.GetParam(0);
            Dsl.ISyntaxComponent param2 = callData.GetParam(1);
            m_VarId = param1.GetId();
            m_Op = Calculator.Load(param2);
            return true;
        }

        private string m_VarId;
        private IExpression m_Op;
    }
    internal sealed class NamedVarGet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_VarId == "break") {
                Calculator.RunState = RunStateEnum.Break;
            }
            else if (m_VarId == "continue") {
                Calculator.RunState = RunStateEnum.Continue;
            }
            else if (m_VarId.Length > 0) {
                ret = Calculator.GetVariable(m_VarId);
                if (ret.IsNullObject && m_VarId[0] != '@' && m_VarId[0] != '$') {
                    ret = CalculatorValue.FromObject(Environment.GetEnvironmentVariable(m_VarId));
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.ValueData valData)
        {
            m_VarId = valData.GetId();
            return true;
        }

        private string m_VarId;
    }
    internal sealed class ConstGet : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = m_Val;
            return v;
        }
        protected override bool Load(Dsl.ValueData valData)
        {
            string id = valData.GetId();
            int idType = valData.GetIdType();
            if (idType == Dsl.ValueData.NUM_TOKEN) {
                if (id.StartsWith("0x")) {
                    long v = long.Parse(id.Substring(2), System.Globalization.NumberStyles.HexNumber);
                    if (v >= int.MinValue && v <= int.MaxValue) {
                        m_Val = (int)v;
                    }
                    else {
                        m_Val = v;
                    }
                }
                else if (id.IndexOf('.') < 0) {
                    long v = long.Parse(id);
                    if (v >= int.MinValue && v <= int.MaxValue) {
                        m_Val = (int)v;
                    }
                    else {
                        m_Val = v;
                    }
                }
                else {
                    double v = double.Parse(id);
                    if (v >= float.MinValue && v <= float.MaxValue) {
                        m_Val = (float)v;
                    }
                    else {
                        m_Val = v;
                    }
                }
            }
            else {
                if (idType == Dsl.ValueData.ID_TOKEN) {
                    if (id == "true")
                        m_Val = true;
                    else if (id == "false")
                        m_Val = false;
                    else
                        m_Val = id;
                }
                else {
                    m_Val = id;
                }
            }
            return true;
        }

        private CalculatorValue m_Val;
    }
    internal sealed class FunctionCall : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var args = Calculator.NewCalculatorValueList();
            foreach (var arg in m_Args) {
                var o = arg.Calc();
                args.Add(o);
            }
            var r = Calculator.Calc(m_Func, args);
            Calculator.RecycleCalculatorValueList(args);
            return r;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            if (!funcData.IsHighOrder && funcData.HaveParam()) {
                m_Func = funcData.GetId();
                int num = funcData.GetParamNum();
                for (int ix = 0; ix < num; ++ix) {
                    Dsl.ISyntaxComponent param = funcData.GetParam(ix);
                    m_Args.Add(Calculator.Load(param));
                }
                return true;
            }
            return false;
        }
        private string m_Func = string.Empty;
        private List<IExpression> m_Args = new List<IExpression>();
    }
    internal sealed class AddExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v;
            if (v1.IsString || v2.IsString) {
                v = v1.ToString() + v2.ToString();
            }
            else {
                v = v1.Get<double>() + v2.Get<double>();
            }
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class SubExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<double>() - v2.Get<double>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class MulExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<double>() * v2.Get<double>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class DivExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<double>() / v2.Get<double>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class ModExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<double>() % v2.Get<double>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class BitAndExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<long>() & v2.Get<long>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class BitOrExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<long>() | v2.Get<long>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class BitXorExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<long>() ^ v2.Get<long>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class BitNotExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            CalculatorValue v = ~v1.Get<long>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class LShiftExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<long>() << v2.Get<int>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class RShiftExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.Get<long>() >> v2.Get<int>();
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class MaxExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = v1 >= v2 ? v1 : v2;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class MinExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = v1 <= v2 ? v1 : v2;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class AbsExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = v1 >= 0 ? v1 : -v1;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class SinExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Sin((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class CosExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Cos((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class TanExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Tan((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class AsinExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Asin((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class AcosExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Acos((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class AtanExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Atan((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class Atan2Exp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Atan2((float)v1, (float)v2);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class SinhExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = Math.Sinh(v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class CoshExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = Math.Cosh(v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class TanhExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = Math.Tanh(v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class RndIntExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            long v1 = m_Op1.Calc().Get<long>();
            long v2 = m_Op2.Calc().Get<long>();
            CalculatorValue v = (long)UnityEngine.Random.Range((int)v1, (int)v2);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class RndFloatExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = (double)UnityEngine.Random.Range((float)v1, (float)v2);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class PowExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = (double)Math.Pow((float)v1, (float)v2);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class SqrtExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            CalculatorValue v = (double)Math.Sqrt((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class LogExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            CalculatorValue v = (double)Math.Log((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class Log10Exp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            CalculatorValue v = (double)Math.Log10((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class FloorExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            CalculatorValue v = Math.Floor(v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class CeilExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            CalculatorValue v = Math.Ceiling(v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class LerpExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            double v3 = m_Op3.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Lerp((float)v1, (float)v2, (float)v3);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            m_Op3 = exps[2];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
        private IExpression m_Op3;
    }
    internal sealed class LerpUnclampedExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            double v3 = m_Op3.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.LerpUnclamped((float)v1, (float)v2, (float)v3);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            m_Op3 = exps[2];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
        private IExpression m_Op3;
    }
    internal sealed class LerpAngleExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            double v3 = m_Op3.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.LerpAngle((float)v1, (float)v2, (float)v3);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            m_Op3 = exps[2];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
        private IExpression m_Op3;
    }
    internal sealed class Clamp01Exp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op.Calc().Get<double>();
            CalculatorValue v = (double)Mathf.Clamp01((float)v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class ClampExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            double v3 = m_Op3.Calc().Get<double>();
            CalculatorValue v;
            if (v3 < v1)
                v = v1;
            else if (v3 > v2)
                v = v2;
            else
                v = v3;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            m_Op3 = exps[2];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
        private IExpression m_Op3;
    }
    internal sealed class ApproximatelyExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            float v1 = m_Op1.Calc().Get<float>();
            float v2 = m_Op2.Calc().Get<float>();
            CalculatorValue v = Mathf.Approximately(v1, v2) ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class IsPowerOfTwoExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            int v1 = m_Op1.Calc().Get<int>();
            int v = Mathf.IsPowerOfTwo(v1) ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class ClosestPowerOfTwoExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            int v1 = m_Op1.Calc().Get<int>();
            int v = Mathf.ClosestPowerOfTwo(v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class NextPowerOfTwoExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            int v1 = m_Op1.Calc().Get<int>();
            int v = Mathf.NextPowerOfTwo(v1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            return true;
        }

        private IExpression m_Op1;
    }
    internal sealed class DistExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            float x1 = (float)m_Op1.Calc().Get<double>();
            float y1 = (float)m_Op2.Calc().Get<double>();
            float x2 = (float)m_Op3.Calc().Get<double>();
            float y2 = (float)m_Op4.Calc().Get<double>();
            CalculatorValue v = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            m_Op3 = exps[2];
            m_Op4 = exps[3];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
        private IExpression m_Op3;
        private IExpression m_Op4;
    }
    internal sealed class DistSqrExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            float x1 = (float)m_Op1.Calc().Get<double>();
            float y1 = (float)m_Op2.Calc().Get<double>();
            float x2 = (float)m_Op3.Calc().Get<double>();
            float y2 = (float)m_Op4.Calc().Get<double>();
            CalculatorValue v = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            m_Op3 = exps[2];
            m_Op4 = exps[3];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
        private IExpression m_Op3;
        private IExpression m_Op4;
    }
    internal sealed class GreatExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = v1 > v2 ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class GreatEqualExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = v1 >= v2 ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class LessExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = v1 < v2 ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class LessEqualExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            double v1 = m_Op1.Calc().Get<double>();
            double v2 = m_Op2.Calc().Get<double>();
            CalculatorValue v = v1 <= v2 ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class EqualExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.ToString() == v2.ToString() ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class NotEqualExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = m_Op2.Calc();
            CalculatorValue v = v1.ToString() != v2.ToString() ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class AndExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            long v1 = m_Op1.Calc().Get<long>();
            long v2 = 0;
            CalculatorValue v = v1 != 0 && (v2 = m_Op2.Calc().Get<long>()) != 0 ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class OrExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            long v1 = m_Op1.Calc().Get<long>();
            long v2 = 0;
            CalculatorValue v = v1 != 0 || (v2 = m_Op2.Calc().Get<long>()) != 0 ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op1 = exps[0];
            m_Op2 = exps[1];
            return true;
        }

        private IExpression m_Op1;
        private IExpression m_Op2;
    }
    internal sealed class NotExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            long val = m_Op.Calc().Get<long>();
            CalculatorValue v = val == 0 ? 1 : 0;
            return v;
        }
        protected override bool Load(IList<IExpression> exps)
        {
            m_Op = exps[0];
            return true;
        }

        private IExpression m_Op;
    }
    internal sealed class CondExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var v1 = m_Op1.Calc();
            var v2 = CalculatorValue.NullObject;
            CalculatorValue v3 = CalculatorValue.NullObject;
            CalculatorValue v = v1.Get<long>() != 0 ? v2 = m_Op2.Calc() : v3 = m_Op3.Calc();
            return v;
        }
        protected override bool Load(Dsl.StatementData statementData)
        {
            Dsl.FunctionData funcData1 = statementData.First;
            Dsl.FunctionData funcData2 = statementData.Second;
            if (funcData1.IsHighOrder && funcData1.HaveLowerOrderParam() && funcData2.GetId() == ":" && funcData2.HaveParamOrStatement()) {
                Dsl.ISyntaxComponent cond = funcData1.LowerOrderFunction.GetParam(0);
                Dsl.ISyntaxComponent op1 = funcData1.GetParam(0);
                Dsl.ISyntaxComponent op2 = funcData2.GetParam(0);
                m_Op1 = Calculator.Load(cond);
                m_Op2 = Calculator.Load(op1);
                m_Op3 = Calculator.Load(op2);
            }
            else {
                //error
                Calculator.Log("DslCalculator error, {0} line {1}", statementData.ToScriptString(false), statementData.GetLine());
            }
            return true;
        }

        private IExpression m_Op1 = null;
        private IExpression m_Op2 = null;
        private IExpression m_Op3 = null;
    }
    internal sealed class IfExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            for (int ix = 0; ix < m_Clauses.Count; ++ix) {
                var clause = m_Clauses[ix];
                if (null != clause.Condition) {
                    var condVal = clause.Condition.Calc();
                    if (condVal.Get<long>() != 0) {
                        for (int index = 0; index < clause.Expressions.Count; ++index) {
                            v = clause.Expressions[index].Calc();
                            if (Calculator.RunState != RunStateEnum.Normal) {
                                return v;
                            }
                        }
                        break;
                    }
                }
                else if (ix == m_Clauses.Count - 1) {
                    for (int index = 0; index < clause.Expressions.Count; ++index) {
                        v = clause.Expressions[index].Calc();
                        if (Calculator.RunState != RunStateEnum.Normal) {
                            return v;
                        }
                    }
                    break;
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            if (funcData.IsHighOrder) {
                Dsl.ISyntaxComponent cond = funcData.LowerOrderFunction.GetParam(0);
                IfExp.Clause item = new IfExp.Clause();
                item.Condition = Calculator.Load(cond);
                for (int ix = 0; ix < funcData.GetParamNum(); ++ix) {
                    IExpression subExp = Calculator.Load(funcData.GetParam(ix));
                    item.Expressions.Add(subExp);
                }
                m_Clauses.Add(item);
            }
            else {
                //error
                Calculator.Log("DslCalculator error, {0} line {1}", funcData.ToScriptString(false), funcData.GetLine());
            }
            return true;
        }
        protected override bool Load(Dsl.StatementData statementData)
        {
            //简化语法if(exp) func(args);语法的处理
            int funcNum = statementData.GetFunctionNum();
            if (funcNum == 2) {
                var first = statementData.First;
                var second = statementData.Second;
                var firstId = first.GetId();
                var secondId = second.GetId();
                if (firstId == "if" && !first.HaveStatement() && !first.HaveExternScript() &&
                        !string.IsNullOrEmpty(secondId) && !second.HaveStatement() && !second.HaveExternScript()) {
                    IfExp.Clause item = new IfExp.Clause();
                    if (first.GetParamNum() > 0) {
                        Dsl.ISyntaxComponent cond = first.GetParam(0);
                        item.Condition = Calculator.Load(cond);
                    }
                    else {
                        //error
                        Calculator.Log("DslCalculator error, {0} line {1}", first.ToScriptString(false), first.GetLine());
                    }
                    IExpression subExp = Calculator.Load(second);
                    item.Expressions.Add(subExp);
                    m_Clauses.Add(item);
                    return true;
                }
            }
            //标准if语句的处理
            foreach (var fData in statementData.Functions) {
                if (fData.GetId() == "if" || fData.GetId() == "elseif") {
                    IfExp.Clause item = new IfExp.Clause();
                    if (fData.IsHighOrder && fData.LowerOrderFunction.GetParamNum() > 0) {
                        Dsl.ISyntaxComponent cond = fData.LowerOrderFunction.GetParam(0);
                        item.Condition = Calculator.Load(cond);
                    }
                    else {
                        //error
                        Calculator.Log("DslCalculator error, {0} line {1}", fData.ToScriptString(false), fData.GetLine());
                    }
                    for (int ix = 0; ix < fData.GetParamNum(); ++ix) {
                        IExpression subExp = Calculator.Load(fData.GetParam(ix));
                        item.Expressions.Add(subExp);
                    }
                    m_Clauses.Add(item);
                }
                else if (fData.GetId() == "else") {
                    if (fData != statementData.Last) {
                        //error
                        Calculator.Log("DslCalculator error, {0} line {1}", fData.ToScriptString(false), fData.GetLine());
                    }
                    else {
                        IfExp.Clause item = new IfExp.Clause();
                        for (int ix = 0; ix < fData.GetParamNum(); ++ix) {
                            IExpression subExp = Calculator.Load(fData.GetParam(ix));
                            item.Expressions.Add(subExp);
                        }
                        m_Clauses.Add(item);
                    }
                }
                else {
                    //error
                    Calculator.Log("DslCalculator error, {0} line {1}", fData.ToScriptString(false), fData.GetLine());
                }
            }
            return true;
        }

        private sealed class Clause
        {
            internal IExpression Condition;
            internal List<IExpression> Expressions = new List<IExpression>();
        }

        private List<Clause> m_Clauses = new List<Clause>();
    }
    internal sealed class WhileExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            for (; ; ) {
                var condVal = m_Condition.Calc();
                if (condVal.Get<long>() != 0) {
                    for (int index = 0; index < m_Expressions.Count; ++index) {
                        v = m_Expressions[index].Calc();
                        if (Calculator.RunState == RunStateEnum.Continue) {
                            Calculator.RunState = RunStateEnum.Normal;
                            break;
                        }
                        else if (Calculator.RunState != RunStateEnum.Normal) {
                            if (Calculator.RunState == RunStateEnum.Break)
                                Calculator.RunState = RunStateEnum.Normal;
                            return v;
                        }
                    }
                }
                else {
                    break;
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            if (funcData.IsHighOrder) {
                Dsl.ISyntaxComponent cond = funcData.LowerOrderFunction.GetParam(0);
                m_Condition = Calculator.Load(cond);
                for (int ix = 0; ix < funcData.GetParamNum(); ++ix) {
                    IExpression subExp = Calculator.Load(funcData.GetParam(ix));
                    m_Expressions.Add(subExp);
                }
            }
            else {
                //error
                Calculator.Log("DslCalculator error, {0} line {1}", funcData.ToScriptString(false), funcData.GetLine());
            }
            return true;
        }
        protected override bool Load(Dsl.StatementData statementData)
        {
            //简化语法while(exp) func(args);语法的处理
            if (statementData.GetFunctionNum() == 2) {
                var first = statementData.First;
                var second = statementData.Second;
                var firstId = first.GetId();
                var secondId = second.GetId();
                if (firstId == "while" && !first.HaveStatement() && !first.HaveExternScript() &&
                        !string.IsNullOrEmpty(secondId) && !second.HaveStatement() && !second.HaveExternScript()) {
                    if (first.GetParamNum() > 0) {
                        Dsl.ISyntaxComponent cond = first.GetParam(0);
                        m_Condition = Calculator.Load(cond);
                    }
                    else {
                        //error
                        Calculator.Log("DslCalculator error, {0} line {1}", first.ToScriptString(false), first.GetLine());
                    }
                    IExpression subExp = Calculator.Load(second);
                    m_Expressions.Add(subExp);
                    return true;
                }
            }
            return false;
        }

        private IExpression m_Condition;
        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class LoopExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            var count = m_Count.Calc();
            long ct = count.Get<long>();
            for (int i = 0; i < ct; ++i) {
                Calculator.SetVariable("$$", i);
                for (int index = 0; index < m_Expressions.Count; ++index) {
                    v = m_Expressions[index].Calc();
                    if (Calculator.RunState == RunStateEnum.Continue) {
                        Calculator.RunState = RunStateEnum.Normal;
                        break;
                    }
                    else if (Calculator.RunState != RunStateEnum.Normal) {
                        if (Calculator.RunState == RunStateEnum.Break)
                            Calculator.RunState = RunStateEnum.Normal;
                        return v;
                    }
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            if (funcData.IsHighOrder) {
                Dsl.ISyntaxComponent count = funcData.LowerOrderFunction.GetParam(0);
                m_Count = Calculator.Load(count);
                for (int ix = 0; ix < funcData.GetParamNum(); ++ix) {
                    IExpression subExp = Calculator.Load(funcData.GetParam(ix));
                    m_Expressions.Add(subExp);
                }
            }
            else {
                //error
                Calculator.Log("DslCalculator error, {0} line {1}", funcData.ToScriptString(false), funcData.GetLine());
            }
            return true;
        }
        protected override bool Load(Dsl.StatementData statementData)
        {
            //简化语法loop(exp) func(args);语法的处理
            if (statementData.GetFunctionNum() == 2) {
                var first = statementData.First;
                var second = statementData.Second;
                var firstId = first.GetId();
                var secondId = second.GetId();
                if (firstId == "loop" && !first.HaveStatement() && !first.HaveExternScript() &&
                        !string.IsNullOrEmpty(secondId) && !second.HaveStatement() && !second.HaveExternScript()) {
                    if (first.GetParamNum() > 0) {
                        Dsl.ISyntaxComponent exp = first.GetParam(0);
                        m_Count = Calculator.Load(exp);
                    }
                    else {
                        //error
                        Calculator.Log("DslCalculator error, {0} line {1}", first.ToScriptString(false), first.GetLine());
                    }
                    IExpression subExp = Calculator.Load(second);
                    m_Expressions.Add(subExp);
                    return true;
                }
            }
            return false;
        }

        private IExpression m_Count;
        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class LoopListExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            var list = m_List.Calc();
            IEnumerable obj = list.As<IEnumerable>();
            if (null != obj) {
                IEnumerator enumer = obj.GetEnumerator();
                while (enumer.MoveNext()) {
                    var val = CalculatorValue.FromObject(enumer.Current);
                    Calculator.SetVariable("$$", val);
                    for (int index = 0; index < m_Expressions.Count; ++index) {
                        v = m_Expressions[index].Calc();
                        if (Calculator.RunState == RunStateEnum.Continue) {
                            Calculator.RunState = RunStateEnum.Normal;
                            break;
                        }
                        else if (Calculator.RunState != RunStateEnum.Normal) {
                            if (Calculator.RunState == RunStateEnum.Break)
                                Calculator.RunState = RunStateEnum.Normal;
                            return v;
                        }
                    }
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            if (funcData.IsHighOrder) {
                Dsl.ISyntaxComponent list = funcData.LowerOrderFunction.GetParam(0);
                m_List = Calculator.Load(list);
                for (int ix = 0; ix < funcData.GetParamNum(); ++ix) {
                    IExpression subExp = Calculator.Load(funcData.GetParam(ix));
                    m_Expressions.Add(subExp);
                }
            }
            else {
                //error
                Calculator.Log("DslCalculator error, {0} line {1}", funcData.ToScriptString(false), funcData.GetLine());
            }
            return true;
        }
        protected override bool Load(Dsl.StatementData statementData)
        {
            //简化语法looplist(exp) func(args);语法的处理
            if (statementData.GetFunctionNum() == 2) {
                var first = statementData.First;
                var second = statementData.Second;
                var firstId = first.GetId();
                var secondId = second.GetId();
                if (firstId == "looplist" && !first.HaveStatement() && !first.HaveExternScript() &&
                        !string.IsNullOrEmpty(secondId) && !second.HaveStatement() && !second.HaveExternScript()) {
                    if (first.GetParamNum() > 0) {
                        Dsl.ISyntaxComponent exp = first.GetParam(0);
                        m_List = Calculator.Load(exp);
                    }
                    else {
                        //error
                        Calculator.Log("DslCalculator error, {0} line {1}", first.ToScriptString(false), first.GetLine());
                    }
                    IExpression subExp = Calculator.Load(second);
                    m_Expressions.Add(subExp);
                    return true;
                }
            }
            return false;
        }

        private IExpression m_List;
        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class ForeachExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            List<object> list = new List<object>();
            for (int ix = 0; ix < m_Elements.Count; ++ix) {
                object val = m_Elements[ix].Calc().Get<object>();
                list.Add(val);
            }
            IEnumerator enumer = list.GetEnumerator();
            while (enumer.MoveNext()) {
                var val = CalculatorValue.FromObject(enumer.Current);
                Calculator.SetVariable("$$", val);
                for (int index = 0; index < m_Expressions.Count; ++index) {
                    v = m_Expressions[index].Calc();
                    if (Calculator.RunState == RunStateEnum.Continue) {
                        Calculator.RunState = RunStateEnum.Normal;
                        break;
                    }
                    else if (Calculator.RunState != RunStateEnum.Normal) {
                        if (Calculator.RunState == RunStateEnum.Break)
                            Calculator.RunState = RunStateEnum.Normal;
                        return v;
                    }
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            if (funcData.IsHighOrder) {
                Dsl.FunctionData callData = funcData.LowerOrderFunction;
                int num = callData.GetParamNum();
                for (int ix = 0; ix < num; ++ix) {
                    Dsl.ISyntaxComponent exp = callData.GetParam(ix);
                    m_Elements.Add(Calculator.Load(exp));
                }
            }
            if (funcData.HaveStatement()) {
                int fnum = funcData.GetParamNum();
                for (int ix = 0; ix < fnum; ++ix) {
                    IExpression subExp = Calculator.Load(funcData.GetParam(ix));
                    m_Expressions.Add(subExp);
                }
            }
            return true;
        }
        protected override bool Load(Dsl.StatementData statementData)
        {
            //简化语法foreach(exp1,exp2,...) func(args);语法的处理
            if (statementData.GetFunctionNum() == 2) {
                var first = statementData.First;
                var second = statementData.Second;
                var firstId = first.GetId();
                var secondId = second.GetId();
                if (firstId == "foreach" && !first.HaveStatement() && !first.HaveExternScript() &&
                        !string.IsNullOrEmpty(secondId) && !second.HaveStatement() && !second.HaveExternScript()) {
                    int num = first.GetParamNum();
                    if (num > 0) {
                        for (int ix = 0; ix < num; ++ix) {
                            Dsl.ISyntaxComponent exp = first.GetParam(ix);
                            m_Elements.Add(Calculator.Load(exp));
                        }
                    }
                    else {
                        //error
                        Calculator.Log("DslCalculator error, {0} line {1}", first.ToScriptString(false), first.GetLine());
                    }
                    IExpression subExp = Calculator.Load(second);
                    m_Expressions.Add(subExp);
                    return true;
                }
            }
            return false;
        }

        private List<IExpression> m_Elements = new List<IExpression>();
        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class ParenthesisExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            for (int ix = 0; ix < m_Expressions.Count; ++ix) {
                var exp = m_Expressions[ix];
                v = exp.Calc();
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class FormatExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            string fmt = string.Empty;
            ArrayList al = new ArrayList();
            for (int ix = 0; ix < m_Expressions.Count; ++ix) {
                var exp = m_Expressions[ix];
                v = exp.Calc();
                if (ix == 0)
                    fmt = v.AsString;
                else
                    al.Add(v);
            }
            v = string.Format(fmt, al.ToArray());
            return v;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class GetTypeAssemblyNameExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_Expressions.Count >= 1) {
                var obj = m_Expressions[0].Calc();
                try {
                    ret = obj.GetType().AssemblyQualifiedName;
                }
                catch (Exception ex) {
                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class GetTypeFullNameExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_Expressions.Count >= 1) {
                var obj = m_Expressions[0].Calc();
                try {
                    ret = obj.GetType().FullName;
                }
                catch (Exception ex) {
                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class GetTypeNameExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_Expressions.Count >= 1) {
                var obj = m_Expressions[0].Calc();
                try {
                    ret = obj.GetType().Name;
                }
                catch (Exception ex) {
                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class GetTypeExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_Expressions.Count >= 1) {
                string type = m_Expressions[0].Calc().AsString;
                try {
                    var r = Type.GetType("UnityEngine." + type + ", UnityEngine");
                    if (null == r) {
                        r = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
                    }
                    if (null == r) {
                        r = Type.GetType("UnityEditor." + type + ", UnityEditor");
                    }
                    if (null == r) {
                        r = Type.GetType(type + ", Assembly-CSharp");
                    }
                    if (null == r) {
                        r = Type.GetType(type);
                    }
                    if (null == r) {
                        Calculator.Log("null == Type.GetType({0})", type);
                    }
                    else {
                        ret = CalculatorValue.FromObject(r);
                    }
                }
                catch (Exception ex) {
                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class ChangeTypeExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_Expressions.Count >= 2) {
                var obj = m_Expressions[0].Calc();
                string type = m_Expressions[1].Calc().AsString;
                try {
                    string str = obj.AsString;
                    if (obj.IsString) {
                        if (0 == type.CompareTo("sbyte")) {
                            ret = CastTo<sbyte>(str);
                        }
                        else if (0 == type.CompareTo("byte")) {
                            ret = CastTo<byte>(str);
                        }
                        else if (0 == type.CompareTo("short")) {
                            ret = CastTo<short>(str);
                        }
                        else if (0 == type.CompareTo("ushort")) {
                            ret = CastTo<ushort>(str);
                        }
                        else if (0 == type.CompareTo("int")) {
                            ret = CastTo<int>(str);
                        }
                        else if (0 == type.CompareTo("uint")) {
                            ret = CastTo<uint>(str);
                        }
                        else if (0 == type.CompareTo("long")) {
                            ret = CastTo<long>(str);
                        }
                        else if (0 == type.CompareTo("ulong")) {
                            ret = CastTo<ulong>(str);
                        }
                        else if (0 == type.CompareTo("float")) {
                            ret = CastTo<float>(str);
                        }
                        else if (0 == type.CompareTo("double")) {
                            ret = CastTo<double>(str);
                        }
                        else if (0 == type.CompareTo("string")) {
                            ret = str;
                        }
                        else if (0 == type.CompareTo("bool")) {
                            ret = CastTo<bool>(str);
                        }
                        else {                            
	                        Type t = Type.GetType("UnityEngine." + type + ", UnityEngine");
	                        if (null == t) {
	                            t = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
	                        }
	                        if (null == t) {
	                            t = Type.GetType("UnityEditor." + type + ", UnityEditor");
	                        }
	                        if (null == t) {
	                            t = Type.GetType(type + ", Assembly-CSharp");
	                        }
	                        if (null == t) {
	                            t = Type.GetType(type);
	                        }
                            if (null != t) {
                                ret = CalculatorValue.FromObject(CastTo(t, str));
                            }
                            else {
                                Calculator.Log("null == Type.GetType({0})", type);
                            }
                        }
                    }
                    else {
                        if (0 == type.CompareTo("sbyte")) {
                            ret = obj.Get<sbyte>();
                        }
                        else if (0 == type.CompareTo("byte")) {
                            ret = obj.Get<byte>();
                        }
                        else if (0 == type.CompareTo("short")) {
                            ret = obj.Get<short>();
                        }
                        else if (0 == type.CompareTo("ushort")) {
                            ret = obj.Get<ushort>();
                        }
                        else if (0 == type.CompareTo("int")) {
                            ret = obj.Get<int>();
                        }
                        else if (0 == type.CompareTo("uint")) {
                            ret = obj.Get<uint>();
                        }
                        else if (0 == type.CompareTo("long")) {
                            ret = obj.Get<long>();
                        }
                        else if (0 == type.CompareTo("ulong")) {
                            ret = obj.Get<ulong>();
                        }
                        else if (0 == type.CompareTo("float")) {
                            ret = obj.Get<float>();
                        }
                        else if (0 == type.CompareTo("double")) {
                            ret = obj.Get<double>();
                        }
                        else if (0 == type.CompareTo("string")) {
                            ret = obj.Get<string>();
                        }
                        else if (0 == type.CompareTo("bool")) {
                            ret = obj.Get<bool>();
	                    }
	                    else {
	                        Type t = Type.GetType("UnityEngine." + type + ", UnityEngine");
	                        if (null == t) {
	                            t = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
	                        }
	                        if (null == t) {
	                            t = Type.GetType("UnityEditor." + type + ", UnityEditor");
	                        }
	                        if (null == t) {
	                            t = Type.GetType(type + ", Assembly-CSharp");
	                        }
	                        if (null == t) {
	                            t = Type.GetType(type);
	                        }
	                        if (null != t) {
	                            ret = CalculatorValue.FromObject(obj.Get(t));
	                        }
	                        else {
	                            Calculator.Log("null == Type.GetType({0})", type);
	                        }
	                    }
					}
                }
                catch (Exception ex) {
                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class ParseEnumExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_Expressions.Count >= 2) {
                string type = m_Expressions[0].Calc().AsString;
                string val = m_Expressions[1].Calc().AsString;
                try {
                    Type t = Type.GetType("UnityEngine." + type + ", UnityEngine");
                    if (null == t) {
                        t = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
                    }
                    if (null == t) {
                        t = Type.GetType("UnityEditor." + type + ", UnityEditor");
                    }
                    if (null == t) {
                        t = Type.GetType(type + ", Assembly-CSharp");
                    }
                    if (null == t) {
                        t = Type.GetType(type);
                    }
                    if (null != t) {
                        ret = CalculatorValue.FromObject(Enum.Parse(t, val, true));
                    }
                    else {
                        Calculator.Log("null == Type.GetType({0})", type);
                    }
                }
                catch (Exception ex) {
                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class DotnetCallExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            object obj = null;
            object methodObj = null;
            string method = null;
            ArrayList arglist = new ArrayList();
            for (int ix = 0; ix < m_Expressions.Count; ++ix) {
                var exp = m_Expressions[ix];
                var v = exp.Calc();
                if (ix == 0) {
                    obj = v.Get<object>();
                }
                else if (ix == 1) {
                    methodObj = v.Get<object>();
                    method = v.AsString;
                }
                else {
                    arglist.Add(v.Get<object>());
                }
            }
            object[] _args = arglist.ToArray();
            if (null != obj) {
                if (null != method) {
                    IDictionary dict = obj as IDictionary;
                    if (null != dict && dict.Contains(method) && dict[method] is Delegate) {
                        var d = dict[method] as Delegate;
                        if (null != d) {
                            ret = CalculatorValue.FromObject(d.DynamicInvoke());
                        }
                    }
                    else {
                        Type t = obj as Type;
                        if (null != t) {
                            try {
                                BindingFlags flags = BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic;
                                CastArgsForCall(t, method, flags, _args);
                                ret = CalculatorValue.FromObject(t.InvokeMember(method, flags, null, null, _args));
                            }
                            catch (Exception ex) {
                                Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                            }
                        }
                        else {
                            t = obj.GetType();
                            if (null != t) {
                                try {
                                    BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic;
                                    CastArgsForCall(t, method, flags, _args);
                                    ret = CalculatorValue.FromObject(t.InvokeMember(method, flags, null, obj, _args));
                                }
                                catch (Exception ex) {
                                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                                }
                            }
                        }
                    }
                }
                else if (null != methodObj) {
                    IDictionary dict = obj as IDictionary;
                    if (null != dict && dict.Contains(methodObj)) {
                        var d = dict[methodObj] as Delegate;
                        if (null != d) {
                            ret = CalculatorValue.FromObject(d.DynamicInvoke());
                        }
                    }
                    else {
                        IEnumerable enumer = obj as IEnumerable;
                        if (null != enumer && methodObj is int) {
                            int index = (int)methodObj;
                            var e = enumer.GetEnumerator();
                            for (int i = 0; i <= index; ++i) {
                                e.MoveNext();
                            }
                            var d = e.Current as Delegate;
                            if (null != d) {
                                ret = CalculatorValue.FromObject(d.DynamicInvoke());
                            }
                        }
                    }
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class DotnetSetExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            object obj = null;
            object methodObj = null;
            string method = null;
            ArrayList arglist = new ArrayList();
            for (int ix = 0; ix < m_Expressions.Count; ++ix) {
                var exp = m_Expressions[ix];
                var v = exp.Calc();
                if (ix == 0) {
                    obj = v.Get<object>();
                }
                else if (ix == 1) {
                    methodObj = v.Get<object>();
                    method = v.AsString;
                }
                else {
                    arglist.Add(v.Get<object>());
                }
            }
            object[] _args = arglist.ToArray();
            if (null != obj) {
                if (null != method) {
                    IDictionary dict = obj as IDictionary;
                    if (null != dict && null == obj.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic)) {
                        dict[method] = _args[0];
                    }
                    else {
                        Type t = obj as Type;
                        if (null != t) {
                            try {
                                BindingFlags flags = BindingFlags.Static | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic;
                                CastArgsForSet(t, method, flags, _args);
                                ret = CalculatorValue.FromObject(t.InvokeMember(method, flags, null, null, _args));
                            }
                            catch (Exception ex) {
                                Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                            }
                        }
                        else {
                            t = obj.GetType();
                            if (null != t) {
                                try {
                                    BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic;
                                    CastArgsForSet(t, method, flags, _args);
                                    ret = CalculatorValue.FromObject(t.InvokeMember(method, flags, null, obj, _args));
                                }
                                catch (Exception ex) {
                                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                                }
                            }
                        }
                    }
                }
                else if (null != methodObj) {
                    IDictionary dict = obj as IDictionary;
                    if (null != dict && dict.Contains(methodObj)) {
                        dict[methodObj] = _args[0];
                    }
                    else {
                        IList list = obj as IList;
                        if (null != list && methodObj is int) {
                            int index = (int)methodObj;
                            if (index >= 0 && index < list.Count) {
                                list[index] = _args[0];
                            }
                        }
                    }
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class DotnetGetExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            object obj = null;
            object methodObj = null;
            string method = null;
            ArrayList arglist = new ArrayList();
            for (int ix = 0; ix < m_Expressions.Count; ++ix) {
                var exp = m_Expressions[ix];
                var v = exp.Calc();
                if (ix == 0) {
                    obj = v.Get<object>();
                }
                else if (ix == 1) {
                    methodObj = v.Get<object>();
                    method = v.AsString;
                }
                else {
                    arglist.Add(v.Get<object>());
                }
            }
            object[] _args = arglist.ToArray();
            if (null != obj) {
                if (null != method) {
                    IDictionary dict = obj as IDictionary;
                    if (null != dict && dict.Contains(method)) {
                        ret = CalculatorValue.FromObject(dict[method]);
                    }
                    else {
                        Type t = obj as Type;
                        if (null != t) {
                            try {
                                BindingFlags flags = BindingFlags.Static | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic;
                                CastArgsForGet(t, method, flags, _args);
                                ret = CalculatorValue.FromObject(t.InvokeMember(method, flags, null, null, _args));
                            }
                            catch (Exception ex) {
                                Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                            }
                        }
                        else {
                            t = obj.GetType();
                            if (null != t) {
                                try {
                                    BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic;
                                    CastArgsForGet(t, method, flags, _args);
                                    ret = CalculatorValue.FromObject(t.InvokeMember(method, flags, null, obj, _args));
                                }
                                catch (Exception ex) {
                                    Calculator.Log("Exception:{0}\n{1}", ex.Message, ex.StackTrace);
                                }
                            }
                        }
                    }
                }
                else if (null != methodObj) {
                    IDictionary dict = obj as IDictionary;
                    if (null != dict && dict.Contains(methodObj)) {
                        ret = CalculatorValue.FromObject(dict[methodObj]);
                    }
                    else {
                        IEnumerable enumer = obj as IEnumerable;
                        if (null != enumer && methodObj is int) {
                            int index = (int)methodObj;
                            var e = enumer.GetEnumerator();
                            for (int i = 0; i <= index; ++i) {
                                e.MoveNext();
                            }
                            ret = CalculatorValue.FromObject(e.Current);
                        }
                    }
                }
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class LinqExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue v = 0;
            var list = m_List.Calc().Get<object>();
            var method = m_Method.Calc().Get<string>();
            IEnumerable obj = list as IEnumerable;
            if (null != obj && !string.IsNullOrEmpty(method)) {
                if (method == "orderby" || method == "orderbydesc") {
                    bool desc = method == "orderbydesc";
                    List<object> results = new List<object>();
                    IEnumerator enumer = obj.GetEnumerator();
                    while (enumer.MoveNext()) {
                        var val = CalculatorValue.FromObject(enumer.Current);
                        results.Add(val);
                    }
                    results.Sort((object o1, object o2) => {
                        Calculator.SetVariable("$$", CalculatorValue.FromObject(o1));
                        var r1 = CalculatorValue.NullObject;
                        for (int index = 0; index < m_Expressions.Count; ++index) {
                            r1 = m_Expressions[index].Calc();
                        }
                        Calculator.SetVariable("$$", CalculatorValue.FromObject(o2));
                        var r2 = CalculatorValue.NullObject;
                        for (int index = 0; index < m_Expressions.Count; ++index) {
                            r2 = m_Expressions[index].Calc();
                        }
                        int r = 0;
                        if (r1.IsString && r2.IsString) {
                            r = r1.Get<string>().CompareTo(r2.Get<string>());
                        }
                        else {
                            double rd1 = r1.Get<double>();
                            double rd2 = r2.Get<double>();
                            r = rd1.CompareTo(rd2);
                        }
                        if (desc)
                            r = -r;
                        return r;
                    });
                    v = CalculatorValue.FromObject(results);
                }
                else if (method == "where") {
                    List<object> results = new List<object>();
                    IEnumerator enumer = obj.GetEnumerator();
                    while (enumer.MoveNext()) {
                        var val = CalculatorValue.FromObject(enumer.Current);

                        Calculator.SetVariable("$$", val);
                        CalculatorValue r = CalculatorValue.NullObject;
                        for (int index = 0; index < m_Expressions.Count; ++index) {
                            r = m_Expressions[index].Calc();
                        }
                        if (r.Get<long>() != 0) {
                            results.Add(val);
                        }
                    }
                    v = CalculatorValue.FromObject(results);
                }
                else if (method == "top") {
                    CalculatorValue r = CalculatorValue.NullObject;
                    for (int index = 0; index < m_Expressions.Count; ++index) {
                        r = m_Expressions[index].Calc();
                    }
                    long ct = r.Get<long>();
                    List<object> results = new List<object>();
                    IEnumerator enumer = obj.GetEnumerator();
                    while (enumer.MoveNext()) {
                        var val = CalculatorValue.FromObject(enumer.Current);
                        if (ct > 0) {
                            results.Add(val);
                            --ct;
                        }
                    }
                    v = CalculatorValue.FromObject(results);
                }
            }
            return v;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            Dsl.ISyntaxComponent list = callData.GetParam(0);
            m_List = Calculator.Load(list);
            Dsl.ISyntaxComponent method = callData.GetParam(1);
            m_Method = Calculator.Load(method);
            for (int i = 2; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private IExpression m_List;
        private IExpression m_Method;
        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal sealed class IsNullExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var ret = CalculatorValue.NullObject;
            if (m_Expressions.Count >= 1) {
                var obj = m_Expressions[0].Calc();
                UnityEngine.Object uo = obj.As<UnityEngine.Object>();
                if (!System.Object.ReferenceEquals(null, uo))
                    ret = null == uo;
                else
                    ret = obj.IsNullObject;
            }
            return ret;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            for (int i = 0; i < callData.GetParamNum(); ++i) {
                Dsl.ISyntaxComponent param = callData.GetParam(i);
                m_Expressions.Add(Calculator.Load(param));
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal class DotnetLoadExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                string path = operands[0].AsString;
                if (!string.IsNullOrEmpty(path) && File.Exists(path)) {
                    r = CalculatorValue.FromObject(Assembly.LoadFile(path));
                }
            }
            return r;
        }
    }
    internal class DotnetNewExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var assem = operands[0].As<Assembly>();
                string typeName = operands[1].AsString;
                if (null!=assem && !string.IsNullOrEmpty(typeName)) {
                    var al = new ArrayList();
                    for(int i = 2; i < operands.Count; ++i) {
                        al.Add(operands[i].Get<object>());
                    }
                    r = CalculatorValue.FromObject(assem.CreateInstance(typeName, false, BindingFlags.CreateInstance, null, al.ToArray(), System.Globalization.CultureInfo.CurrentCulture, null));
                }
            }
            return r;
        }
    }
    internal class AssetPath2GUIDExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string r = string.Empty;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var assetPath = operands[0].AsString;
                if (null != assetPath) {
                    r = AssetDatabase.AssetPathToGUID(assetPath);
                }
            }
#endif
            return r;
        }
    }
    internal class GUID2AssetPathExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string r = string.Empty;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var guid = operands[0].AsString;
                if (null != guid) {
                    r = AssetDatabase.GUIDToAssetPath(guid);
                }
            }
#endif
            return r;
        }
    }
    internal class GetAssetPathExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string r = null;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                if (null != obj) {
                    var pobj = PrefabUtility.GetCorrespondingObjectFromSource(obj);
                    if (null != pobj)
                        r = AssetDatabase.GetAssetPath(pobj);
                    else
                        r = AssetDatabase.GetAssetPath(obj);
                }
            }
#endif
            return r;
        }
    }
    internal class GetGuidAndLocalFileIdentifierExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                if (null != obj) {
                    var pobj = PrefabUtility.GetCorrespondingObjectFromSource(obj);
                    if (null == pobj)
                        pobj = obj;
                    string guid = string.Empty;
                    long localId = 0;
                    if(AssetDatabase.TryGetGUIDAndLocalFileIdentifier(pobj, out guid, out localId)) {
                        r = CalculatorValue.FromObject(new KeyValuePair<string, long>(guid, localId));
                    }
                }
            }
#endif
            return r;
        }
    }
    internal class GetDependenciesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string[] ret = null;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var list = new List<string>();
                for (int i = 0; i < operands.Count; ++i) {
                    var str = operands[i].AsString;
                    if (null != str) {
                        list.Add(str);
                    }
                    else {
                        var strList = operands[i].As<IList>();
                        if (null != strList) {
                            foreach(var strObj in strList) {
                                var tempStr = strObj as string;
                                if (null != tempStr)
                                    list.Add(tempStr);
                            }
                        }
                    }
                }
                if (list.Count == 1) {
                    ret = AssetDatabase.GetDependencies(list[0]);
                }
                else if (list.Count > 1) {
                    ret = AssetDatabase.GetDependencies(list.ToArray());
                }
            }
#endif
            return CalculatorValue.FromObject(ret);
        }
    }
    internal class GetAssetImporterExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    r = CalculatorValue.FromObject(AssetImporter.GetAtPath(path));
                }
            }
#endif
            return r;
        }
    }
    internal class LoadAssetExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    r = CalculatorValue.FromObject(AssetDatabase.LoadMainAssetAtPath(path));
                }
            }
#endif
            return r;
        }
    }
    internal class UnloadAssetExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                if (null != obj) {
                    Resources.UnloadAsset(obj);
                }
            }
            return CalculatorValue.NullObject;
        }
    }
    internal class GetPrefabTypeExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                if (null != obj) {
                    r = CalculatorValue.FromObject(PrefabUtility.GetPrefabAssetType(obj));
                }
            }
#endif
            return r;
        }
    }
    internal class GetPrefabStatusExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                if (null != obj) {
                    r = CalculatorValue.FromObject(PrefabUtility.GetPrefabInstanceStatus(obj));
                }
            }
#endif
            return r;
        }
    }
    internal class GetPrefabObjectExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                if (null != obj) {
                    r = CalculatorValue.FromObject(PrefabUtility.GetPrefabInstanceHandle(obj));
                }
            }
#endif
            return r;
        }
    }
    internal class GetPrefabParentExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                if (null != obj) {
                    r = CalculatorValue.FromObject(PrefabUtility.GetCorrespondingObjectFromSource(obj));
                }
            }
#endif
            return r;
        }
    }
    internal class DestroyObjectExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var obj = operands[0].As<UnityEngine.Object>();
                bool modifyAsset = false;
                if (operands.Count >= 2) {
                    modifyAsset = operands[1].Get<bool>();
                }
                if (null != obj) {
                    UnityEngine.Object.DestroyImmediate(obj, modifyAsset);
                    r = modifyAsset;
                }
            }
            return r;
        }
    }
    internal class GetComponentExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var obj = operands[0].As<GameObject>();
                var type = operands[1].AsString;
                if (null != obj && null != type) {
                    Type t = Type.GetType("UnityEngine." + type + ", UnityEngine");
                    if (null == t) {
                        t = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
                    }
                    if (null == t) {
                        t = Type.GetType(type + ", Assembly-CSharp");
                    }
                    if (null != t) {
                        r = CalculatorValue.FromObject(obj.GetComponent(t));
                    }
                }
            }
            return r;
        }
    }
    internal class GetComponentsExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var obj = operands[0].As<GameObject>();
                var type = operands[1].AsString;
                if (null != obj && null != type) {
                    Type t = Type.GetType("UnityEngine." + type + ", UnityEngine");
                    if (null == t) {
                        t = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
                    }
                    if (null == t) {
                        t = Type.GetType(type + ", Assembly-CSharp");
                    }
                    if (null != t) {
                        r = CalculatorValue.FromObject(obj.GetComponents(t));
                    }
                }
            }
            return r;
        }
    }
    internal class GetComponentInChildrenExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var obj = operands[0].As<GameObject>();
                var type = operands[1].AsString;
                if (null != obj && null != type) {
                    Type t = Type.GetType("UnityEngine." + type + ", UnityEngine");
                    if (null == t) {
                        t = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
                    }
                    if (null == t) {
                        t = Type.GetType(type + ", Assembly-CSharp");
                    }
                    if (null != t) {
                        r = CalculatorValue.FromObject(obj.GetComponentInChildren(t));
                    }
                }
            }
            return r;
        }
    }
    internal class GetComponentsInChildrenExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var obj = operands[0].As<GameObject>();
                var type = operands[1].AsString;
                if (null != obj && null != type) {
                    Type t = Type.GetType("UnityEngine." + type + ", UnityEngine");
                    if (null == t) {
                        t = Type.GetType("UnityEngine.UI." + type + ", UnityEngine.UI");
                    }
                    if (null == t) {
                        t = Type.GetType(type + ", Assembly-CSharp");
                    }
                    if (null != t) {
                        r = CalculatorValue.FromObject(obj.GetComponentsInChildren(t));
                    }
                }
            }
            return r;
        }
    }
    internal class NewStringBuilderExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 0) {
                r = CalculatorValue.FromObject(new StringBuilder());
            }
            return r;
        }
    }
    internal class AppendFormatExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var sb = operands[0].As<StringBuilder>();
                string fmt = string.Empty;
                var al = new ArrayList();
                for (int i = 1; i < operands.Count; ++i) {
                    if (i == 1)
                        fmt = operands[i].AsString;
                    else
                        al.Add(operands[i].Get<object>());
                }
                if (null != sb && !string.IsNullOrEmpty(fmt)) {
                    sb.AppendFormat(fmt, al.ToArray());
                    r = CalculatorValue.FromObject(sb);
                }
            }
            return r;
        }
    }
    internal class AppendLineFormatExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var sb = operands[0].As<StringBuilder>();
                string fmt = string.Empty;
                var al = new ArrayList();
                for (int i = 1; i < operands.Count; ++i) {
                    if (i == 1)
                        fmt = operands[i].AsString;
                    else
                        al.Add(operands[i].Get<object>());
                }
                if (null != sb) {
                    if (string.IsNullOrEmpty(fmt)) {
                        sb.AppendLine();
                    }
                    else {
                        sb.AppendFormat(fmt, al.ToArray());
                        sb.AppendLine();
                    }
                    r = CalculatorValue.FromObject(sb);
                }
            }
            return r;
        }
    }
    internal class StringBuilderToStringExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var sb = operands[0].As<StringBuilder>();
                if (null != sb) {
                    r = sb.ToString();
                }
            }
            return r;
        }
    }
    internal class StringJoinExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var sep = operands[0].AsString;
                var list = operands[1].As<IList>();
                if (null != sep && null != list) {
                    string[] strs = new string[list.Count];
                    for (int i = 0; i < list.Count; ++i) {
                        strs[i] = list[i].ToString();
                    }
                    r = string.Join(sep, strs);
                }
            }
            return r;
        }
    }
    internal class StringSplitExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var str = operands[0].AsString;
                var seps = operands[1].As<IList>();
                if (!string.IsNullOrEmpty(str) && null != seps) {
                    char[] cs = new char[seps.Count];
                    for (int i = 0; i < seps.Count; ++i) {
                        string sep = seps[i].ToString();
                        if (sep.Length > 0) {
                            cs[i] = sep[0];
                        }
                        else {
                            cs[i] = '\0';
                        }
                    }
                    r = CalculatorValue.FromObject(str.Split(cs));
                }
            }
            return r;
        }
    }
    internal class StringTrimExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                r = str.Trim();
            }
            return r;
        }
    }
    internal class StringTrimStartExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                r = str.TrimStart();
            }
            return r;
        }
    }
    internal class StringTrimEndExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                r = str.TrimEnd();
            }
            return r;
        }
    }
    internal class StringToLowerExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                r = str.ToLower();
            }
            return r;
        }
    }
    internal class StringToUpperExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                r = str.ToUpper();
            }
            return r;
        }
    }
    internal class StringReplaceExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 3) {
                var str = operands[0].AsString;
                var key = operands[1].AsString;
                var val = operands[2].AsString;
                r = str.Replace(key, val);
            }
            return r;
        }
    }
    internal class StringReplaceCharExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 3) {
                var str = operands[0].AsString;
                var key = operands[1].AsString;
                var val = operands[2].AsString;
                if (null != str && !string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(val)) {
                    r = str.Replace(key[0], val[0]);
                }
            }
            return r;
        }
    }
    internal class MakeStringExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            List<char> chars = new List<char>();
            for (int i = 0; i < operands.Count; ++i) {
                var v = operands[i];
                var str = v.AsString;
                if (null != str) {
                    char c = '\0';
                    if (str.Length > 0) {
                        c = str[0];
                    }
                    chars.Add(c);
                }
                else {
                    char c = operands[i].Get<char>();
                    chars.Add(c);
                }
            }
            return new String(chars.ToArray());
        }
    }
    internal class StringContainsExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool r = false;
            if (operands.Count >= 2) {
                string str = operands[0].AsString;
                r = true;
                for(int i = 1; i < operands.Count; ++i) {
                    var list = operands[i].As<IList>();
                    if (null != list) {
                        foreach (var o in list) {
                            var key = o as string;
                            if (!string.IsNullOrEmpty(key) && !str.Contains(key)) {
                                return false;
                            }
                        }
                    } else {
                        var key = operands[i].AsString;
                        if (!string.IsNullOrEmpty(key) && !str.Contains(key)) {
                            return false;
                        }
                    }
                }
            }
            return r;
        }
    }
    internal class StringNotContainsExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool r = false;
            if (operands.Count >= 2) {
                string str = operands[0].AsString;
                r = true;
                for (int i = 1; i < operands.Count; ++i) {
                    var list = operands[i].As<IList>();
                    if (null != list) {
                        foreach (var o in list) {
                            var key = o as string;
                            if (!string.IsNullOrEmpty(key) && str.Contains(key)) {
                                return false;
                            }
                        }
                    }
                    else {
                        var key = operands[i].AsString;
                        if (!string.IsNullOrEmpty(key) && str.Contains(key)) {
                            return false;
                        }
                    }
                }
            }
            return r;
        }
    }
    internal class StringContainsAnyExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool r = false;
            if (operands.Count >= 2) {
                r = true;
                string str = operands[0].AsString;
                for (int i = 1; i < operands.Count; ++i) {
                    var list = operands[i].As<IList>();
                    if (null != list) {
                        foreach (var o in list) {
                            var key = o as string;
                            if (!string.IsNullOrEmpty(key)){
                                if (str.Contains(key)) {
                                    return true;
                                }
                                else {
                                    r = false;
                                }
                            }
                        }
                    }
                    else {
                        var key = operands[i].AsString;
                        if (!string.IsNullOrEmpty(key)) {
                            if (str.Contains(key)) {
                                return true;
                            }
                            else {
                                r = false;
                            }
                        }
                    }
                }
            }
            return r;
        }
    }
    internal class StringNotContainsAnyExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool r = false;
            if (operands.Count >= 2) {
                r = true;
                string str = operands[0].AsString;
                for (int i = 1; i < operands.Count; ++i) {
                    var list = operands[i].As<IList>();
                    if (null != list) {
                        foreach (var o in list) {
                            var key = o as string;
                            if (!string.IsNullOrEmpty(key)) {
                                if (!str.Contains(key)) {
                                    return true;
                                }
                                else {
                                    r = false;
                                }
                            }
                        }
                    }
                    else {
                        var key = operands[i].AsString;
                        if (!string.IsNullOrEmpty(key)) {
                            if (!str.Contains(key)) {
                                return true;
                            }
                            else {
                                r = false;
                            }
                        }
                    }
                }
            }
            return r;
        }
    }
    internal class Str2IntExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                int v;
                if (int.TryParse(str, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Str2UintExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                uint v;
                if (uint.TryParse(str, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Str2LongExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                long v;
                if (long.TryParse(str, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Str2UlongExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                ulong v;
                if (ulong.TryParse(str, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Str2FloatExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                float v;
                if (float.TryParse(str, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Str2DoubleExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                double v;
                if (double.TryParse(str, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Hex2IntExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                int v;
                if (int.TryParse(str, System.Globalization.NumberStyles.AllowHexSpecifier, null, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Hex2UintExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                uint v;
                if (uint.TryParse(str, System.Globalization.NumberStyles.AllowHexSpecifier, null, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Hex2LongExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                long v;
                if (long.TryParse(str, System.Globalization.NumberStyles.AllowHexSpecifier, null, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class Hex2UlongExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                ulong v;
                if (ulong.TryParse(str, System.Globalization.NumberStyles.AllowHexSpecifier, null, out v)) {
                    r = v;
                }
            }
            return r;
        }
    }
    internal class DatetimeStrExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var fmt = operands[0].AsString;
                r = DateTime.Now.ToString(fmt);
            } else {
                r = DateTime.Now.ToString();
            }
            return r;
        }
    }
    internal class LongDateStrExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.FromObject(DateTime.Now.ToLongDateString());
            return r;
        }
    }
    internal class LongTimeStrExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.FromObject(DateTime.Now.ToShortDateString());
            return r;
        }
    }
    internal class ShortDateStrExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.FromObject(DateTime.Now.ToShortDateString());
            return r;
        }
    }
    internal class ShortTimeStrExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.FromObject(DateTime.Now.ToShortTimeString());
            return r;
        }
    }
    internal class IsNullOrEmptyExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var str = operands[0].AsString;
                r = string.IsNullOrEmpty(str);
            }
            return r;
        }
    }
    internal class ArrayExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            object[] r = new object[operands.Count];
            for (int i = 0; i < operands.Count; ++i) {
                r[i] = operands[i].Get<object>();
            }
            return CalculatorValue.FromObject(r);
        }
    }
    internal class ToArrayExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var list = operands[0];
                IEnumerable obj = list.As<IEnumerable>();
                if (null != obj) {
                    ArrayList al = new ArrayList();
                    IEnumerator enumer = obj.GetEnumerator();
                    while (enumer.MoveNext()) {
                        var val = CalculatorValue.FromObject(enumer.Current);
                        al.Add(val);
                    }
                    r = CalculatorValue.FromObject(al.ToArray());
                }
            }
            return r;
        }
    }
    internal class ListSizeExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var list = operands[0].As<IList>();
                if (null != list) {
                    r = list.Count;
                }
            }
            return r;
        }
    }
    internal class ListExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            ArrayList al = new ArrayList();
            for (int i = 0; i < operands.Count; ++i) {
                al.Add(operands[i].Get<object>());
            }
            r = al;
            return r;
        }
    }
    internal class ListGetExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var list = operands[0].As<IList>();
                var index = operands[1].Get<int>();
                var defVal = CalculatorValue.NullObject;
                if (operands.Count >= 3) {
                    defVal = operands[2];
                }
                if (null != list) {
                    if (index >= 0 && index < list.Count) {
                        r = CalculatorValue.FromObject(list[index]);
                    }
                    else {
                        r = defVal;
                    }
                }
            }
            return r;
        }
    }
    internal class ListSetExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 3) {
                var list = operands[0].As<IList>();
                var index = operands[1].Get<int>();
                var val = operands[2];
                if (null != list) {
                    if (index >= 0 && index < list.Count) {
                        list[index] = val.Get<object>();
                    }
                }
            }
            return r;
        }
    }
    internal class ListIndexOfExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var list = operands[0].As<IList>();
                object val = operands[1];
                if (null != list) {
                    r = list.IndexOf(val);
                }
            }
            return r;
        }
    }
    internal class ListAddExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var list = operands[0].As<IList>();
                object val = operands[1];
                if (null != list) {
                    list.Add(val);
                }
            }
            return r;
        }
    }
    internal class ListRemoveExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var list = operands[0].As<IList>();
                object val = operands[1];
                if (null != list) {
                    list.Remove(val);
                }
            }
            return r;
        }
    }
    internal class ListInsertExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 3) {
                var list = operands[0].As<IList>();
                var index = operands[1].Get<int>();
                object val = operands[2].Get<object>();
                if (null != list) {
                    list.Insert(index, val);
                }
            }
            return r;
        }
    }
    internal class ListRemoveAtExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var list = operands[0].As<IList>();
                var index = operands[1].Get<int>();
                if (null != list) {
                    list.RemoveAt(index);
                }
            }
            return r;
        }
    }
    internal class ListClearExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var list = operands[0].As<IList>();
                if (null != list) {
                    list.Clear();
                }
            }
            return r;
        }
    }
    internal class ListSplitExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var enumer = operands[0].As<IEnumerable>();
                var ct = operands[1].Get<int>();
                if (null != enumer) {
                    var e = enumer.GetEnumerator();
                    if (null != e) {
                        ArrayList al = new ArrayList();
                        ArrayList arr = new ArrayList();
                        int ix = 0;
                        while (e.MoveNext()) {
                            if (ix < ct) {
                                arr.Add(e.Current);
                                ++ix;
                            }
                            if (ix >= ct) {
                                al.Add(arr);
                                arr = new ArrayList();
                                ix = 0;
                            }
                        }
                        if (arr.Count > 0) {
                            al.Add(arr);
                        }
                        r = al;
                    }
                }
            }
            return r;
        }
    }
    internal class HashtableSizeExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var dict = operands[0].As<IDictionary>();
                if (null != dict) {
                    r = dict.Count;
                }
            }
            return r;
        }
    }
    internal class HashtableExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            CalculatorValue r = CalculatorValue.NullObject;
            Hashtable dict = new Hashtable();
            for (int i = 0; i < m_Expressions.Count - 1; i += 2) {
                var key = m_Expressions[i].Calc().Get<object>();
                var val = m_Expressions[i + 1].Calc().Get<object>();
                dict.Add(key, val);
            }
            r = CalculatorValue.FromObject(dict);
            return r;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            for (int i = 0; i < funcData.GetParamNum(); ++i) {
                Dsl.FunctionData callData = funcData.GetParam(i) as Dsl.FunctionData;
                if (null != callData && callData.GetParamNum() == 2) {
                    var expKey = Calculator.Load(callData.GetParam(0));
                    m_Expressions.Add(expKey);
                    var expVal = Calculator.Load(callData.GetParam(1));
                    m_Expressions.Add(expVal);
                }
            }
            return true;
        }

        private List<IExpression> m_Expressions = new List<IExpression>();
    }
    internal class HashtableGetExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var dict = operands[0].As<IDictionary>();
                var index = operands[1].Get<object>();
                var defVal = CalculatorValue.NullObject;
                if (operands.Count >= 3) {
                    defVal = operands[2];
                }
                if (null != dict && dict.Contains(index)) {
                    r = CalculatorValue.FromObject(dict[index]);
                }
                else {
                    r = defVal;
                }
            }
            return r;
        }
    }
    internal class HashtableSetExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 3) {
                var dict = operands[0].As<IDictionary>();
                var index = operands[1].Get<object>();
                object val = operands[2].Get<object>();
                if (null != dict) {
                    dict[index] = val;
                }
            }
            return r;
        }
    }
    internal class HashtableAddExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 3) {
                var dict = operands[0].As<IDictionary>();
                object key = operands[1];
                object val = operands[2];
                if (null != dict && null != key) {
                    dict.Add(key, val);
                }
            }
            return r;
        }
    }
    internal class HashtableRemoveExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var dict = operands[0].As<IDictionary>();
                object key = operands[1];
                if (null != dict && null != key) {
                    dict.Remove(key);
                }
            }
            return r;
        }
    }
    internal class HashtableClearExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var dict = operands[0].As<IDictionary>();
                if (null != dict) {
                    dict.Clear();
                }
            }
            return r;
        }
    }
    internal class HashtableKeysExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var dict = operands[0].As<IDictionary>();
                if (null != dict) {
                    var list = new ArrayList();
                    list.AddRange(dict.Keys);
                    r = list;
                }
            }
            return r;
        }
    }
    internal class HashtableValuesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var dict = operands[0].As<IDictionary>();
                if (null != dict) {
                    var list = new ArrayList();
                    list.AddRange(dict.Values);
                    r = list;
                }
            }
            return r;
        }
    }
    internal class ListHashtableExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var dict = operands[0].As<IDictionary>();
                if (null != dict) {
                    var list = new ArrayList();
                    foreach (var pair in dict) {
                        list.Add(pair);
                    }
                    r = list;
                }
            }
            return r;
        }
    }
    internal class HashtableSplitExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var dict = operands[0].As<IDictionary>();
                var ct = operands[1].Get<int>();
                if (null != dict) {
                    var e = dict.GetEnumerator();
                    if (null != e) {
                        ArrayList al = new ArrayList();
                        Hashtable ht = new Hashtable();
                        int ix = 0;
                        while (e.MoveNext()) {
                            if (ix < ct) {
                                ht.Add(e.Key, e.Value);
                                ++ix;
                            }
                            if (ix >= ct) {
                                al.Add(ht);
                                ht = new Hashtable();
                                ix = 0;
                            }
                        }
                        if (ht.Count > 0) {
                            al.Add(ht);
                        }
                        r = al;
                    }
                }
            }
            return r;
        }
    }
    //stack与queue共用peek函数
    internal class PeekExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var stack = operands[0].As<Stack<object>>();
                var queue = operands[0].As<Queue<object>>();
                if (null != stack) {
                    r = CalculatorValue.FromObject(stack.Peek());
                }
                else if (null != queue) {
                    r = CalculatorValue.FromObject(queue.Peek());
                }
            }
            return r;
        }
    }
    internal class StackSizeExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int r = 0;
            if (operands.Count >= 1) {
                var stack = operands[0].As<Stack<object>>();
                if (null != stack) {
                    r = stack.Count;
                }
            }
            return r;
        }
    }
    internal class StackExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            var stack = new Stack<object>();
            for (int i = 0; i < operands.Count; ++i) {
                stack.Push(operands[i].Get<object>());
            }
            r = CalculatorValue.FromObject(stack);
            return r;
        }
    }
    internal class PushExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var stack = operands[0].As<Stack<object>>();
                var val = operands[1];
                if (null != stack) {
                    stack.Push(val);
                }
            }
            return r;
        }
    }
    internal class PopExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var stack = operands[0].As<Stack<object>>();
                if (null != stack) {
                    r = CalculatorValue.FromObject(stack.Pop());
                }
            }
            return r;
        }
    }
    internal class StackClearExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var stack = operands[0].As<Stack<object>>();
                if (null != stack) {
                    stack.Clear();
                }
            }
            return r;
        }
    }
    internal class QueueSizeExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int r = 0;
            if (operands.Count >= 1) {
                var queue = operands[0].As<Queue<object>>();
                if (null != queue) {
                    r = queue.Count;
                }
            }
            return r;
        }
    }
    internal class QueueExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            var queue = new Queue<object>();
            for (int i = 0; i < operands.Count; ++i) {
                queue.Enqueue(operands[i].Get<object>());
            }
            r = CalculatorValue.FromObject(queue);
            return r;
        }
    }
    internal class EnqueueExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var queue = operands[0].As<Queue<object>>();
                var val = operands[1];
                if (null != queue) {
                    queue.Enqueue(val);
                }
            }
            return r;
        }
    }
    internal class DequeueExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var queue = operands[0].As<Queue<object>>();
                if (null != queue) {
                    r = CalculatorValue.FromObject(queue.Dequeue());
                }
            }
            return r;
        }
    }
    internal class QueueClearExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var queue = operands[0].As<Queue<object>>();
                if (null != queue) {
                    queue.Clear();
                }
            }
            return r;
        }
    }
    internal class SetEnvironmentExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var key = operands[0].AsString;
                var val = operands[1].AsString;
                val = Environment.ExpandEnvironmentVariables(val);
                Environment.SetEnvironmentVariable(key, val);
            }
            return ret;
        }
    }
    internal class GetEnvironmentExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string ret = string.Empty;
            if (operands.Count >= 1) {
                var key = operands[0].AsString;
                return Environment.GetEnvironmentVariable(key);
            }
            return ret;
        }
    }
    internal class ExpandEnvironmentsExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string ret = string.Empty;
            if (operands.Count >= 1) {
                var key = operands[0].AsString;
                return Environment.ExpandEnvironmentVariables(key);
            }
            return ret;
        }
    }
    internal class EnvironmentsExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return CalculatorValue.FromObject(Environment.GetEnvironmentVariables());
        }
    }
    internal class SetCurrentDirectoryExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string ret = string.Empty;
            if (operands.Count >= 1) {
                var dir = operands[0].AsString;
                Environment.CurrentDirectory = Environment.ExpandEnvironmentVariables(dir);
                ret = dir;
            }
            return ret;
        }
    }
    internal class GetCurrentDirectoryExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Environment.CurrentDirectory;
        }
    }
    internal class CommandLineExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Environment.CommandLine;
        }
    }
    internal class CommandLineArgsExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            if (operands.Count >= 1) {
                string name = operands[0].AsString;
                if (!string.IsNullOrEmpty(name)) {
                    string[] args = System.Environment.GetCommandLineArgs();
                    int suffixIndex = Array.FindIndex(args, item => item == name);
                    if (suffixIndex != -1 && suffixIndex < args.Length - 1) {
                        return args[suffixIndex + 1];
                    }
                }
                return string.Empty;
            }
            else {
                return CalculatorValue.FromObject(Environment.GetCommandLineArgs());
            }
        }
    }
    internal class OsExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Environment.OSVersion.VersionString;
        }
    }
    internal class OsPlatformExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Environment.OSVersion.Platform.ToString();
        }
    }
    internal class OsVersionExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Environment.OSVersion.Version.ToString();
        }
    }
    internal class GetFullPathExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string ret = string.Empty;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    return Path.GetFullPath(path);
                }
            }
            return ret;
        }
    }
    internal class GetPathRootExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string ret = string.Empty;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    return Path.GetPathRoot(path);
                }
            }
            return ret;
        }
    }
    internal class GetRandomFileNameExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Path.GetRandomFileName();
        }
    }
    internal class GetTempFileNameExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Path.GetTempFileName();
        }
    }
    internal class GetTempPathExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            return Path.GetTempPath();
        }
    }
    internal class HasExtensionExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    return Path.HasExtension(path);
                }
            }
            return ret;
        }
    }
    internal class IsPathRootedExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    return Path.IsPathRooted(path);
                }
            }
            return ret;
        }
    }
    internal class GetFileNameExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    r = Path.GetFileName(path);
                }
            }
            return r;
        }
    }
    internal class GetFileNameWithoutExtensionExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    r = Path.GetFileNameWithoutExtension(path);
                }
            }
            return r;
        }
    }
    internal class GetExtensionExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    r = Path.GetExtension(path);
                }
            }
            return r;
        }
    }
    internal class GetDirectoryNameExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var path = operands[0].AsString;
                if (null != path) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    r = Path.GetDirectoryName(path);
                }
            }
            return r;
        }
    }
    internal class CombinePathExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var path1 = operands[0].AsString;
                var path2 = operands[1].AsString;
                if (null != path1 && null != path2) {
                    path1 = Environment.ExpandEnvironmentVariables(path1);
                    path2 = Environment.ExpandEnvironmentVariables(path2);
                    r = Path.Combine(path1, path2);
                }
            }
            return r;
        }
    }
    internal class ChangeExtensionExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 2) {
                var path = operands[0].AsString;
                var ext = operands[1].AsString;
                if (null != path && null != ext) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    r = Path.ChangeExtension(path, ext);
                }
            }
            return r;
        }
    }
    internal class DebugBreakExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 0) {
                Debug.Break();
            }
            return r;
        }
    }
    internal class DebugLogExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var obj = operands[0];
                if (obj.IsString) {
                    var fmt = obj.StringVal;
                    if (operands.Count > 1 && null != fmt) {
                        ArrayList arrayList = new ArrayList();
                        for (int i = 1; i < operands.Count; ++i) {
                            arrayList.Add(operands[i].Get<object>());
                        }
                		Debug.LogFormat(fmt, arrayList.ToArray());
					}
					else {
						Debug.Log(obj.Get<object>());
					}
				}
				else {
					Debug.Log(obj.Get<object>());
				}
            }
            return r;
        }
    }
    internal class DebugWarningExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var fmt = operands[0].AsString;
                ArrayList al = new ArrayList();
                for (int i = 1; i < operands.Count; ++i) {
                    al.Add(operands[i].Get<object>());
                }
                Debug.LogWarningFormat(fmt, al.ToArray());
            }
            return r;
        }
    }
    internal class DebugErrorExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var fmt = operands[0].AsString;
                ArrayList al = new ArrayList();
                for (int i = 1; i < operands.Count; ++i) {
                    al.Add(operands[i].Get<object>());
                }
                Debug.LogErrorFormat(fmt, al.ToArray());
            }
            return r;
        }
    }
    internal class CallStackExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var r = System.Environment.StackTrace;
            return CalculatorValue.FromObject(r);
        }
    }
    internal class CallExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var proc = operands[0].AsString;
                if (null != proc) {
                    var args = Calculator.NewCalculatorValueList();
                    for (int i = 1; i < operands.Count; ++i) {
                        args.Add(operands[i]);
                    }
                    r = Calculator.Calc(proc, args);
                    Calculator.RecycleCalculatorValueList(args);
                }
            }
            return r;
        }
    }
    internal class ReturnExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            Calculator.RunState = RunStateEnum.Return;
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                r = operands[0];
            }
            return r;
        }
    }
    internal class RedirectExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            Calculator.RunState = RunStateEnum.Redirect;
            if (operands.Count >= 1) {
                List<string> args = new List<string>();
                for (int i = 0; i < operands.Count; ++i) {
                    var arg = operands[i].ToString();
                    args.Add(arg);
                }
                return CalculatorValue.FromObject(args);
            }
            return CalculatorValue.NullObject;
        }
    }
    internal class DirectoryExistExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var dir = operands[0].AsString;
                dir = Environment.ExpandEnvironmentVariables(dir);
                ret = Directory.Exists(dir);
            }
            return ret;
        }
    }
    internal class FileExistExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var file = operands[0].AsString;
                file = Environment.ExpandEnvironmentVariables(file);
                ret = File.Exists(file);
            }
            return ret;
        }
    }
    internal class ListDirectoriesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var baseDir = operands[0].AsString;
                baseDir = Environment.ExpandEnvironmentVariables(baseDir);
                IList<string> filterList = new string[] { "*" };
                if (operands.Count >= 2) {
                    var list = new List<string>();
                    for (int i = 1; i < operands.Count; ++i) {
                        var str = operands[i].AsString;
                        if (null != str) {
                            list.Add(str);
                        }
                        else {
                            var strList = operands[i].As<IList>();
                            if (null != strList) {
                                foreach (var strObj in strList) {
                                    var tempStr = strObj as string;
                                    if (null != tempStr)
                                        list.Add(tempStr);
                                }
                            }
                        }
                    }
                    filterList = list;
                }
                if (null != baseDir && Directory.Exists(baseDir)) {
                    var fullList = new List<string>();
                    foreach (var filter in filterList) {
                        var list = Directory.GetDirectories(baseDir, filter, SearchOption.TopDirectoryOnly);
                        fullList.AddRange(list);
                    }
                    ret = CalculatorValue.FromObject(fullList);
                }
            }
            return ret;
        }
    }
    internal class ListFilesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var baseDir = operands[0].AsString;
                baseDir = Environment.ExpandEnvironmentVariables(baseDir);
                IList<string> filterList = new string[] { "*" };
                if (operands.Count >= 2) {
                    var list = new List<string>();
                    for (int i = 1; i < operands.Count; ++i) {
                        var str = operands[i].AsString;
                        if (null != str) {
                            list.Add(str);
                        }
                        else {
                            var strList = operands[i].As<IList>();
                            if (null != strList) {
                                foreach (var strObj in strList) {
                                    var tempStr = strObj as string;
                                    if (null != tempStr)
                                        list.Add(tempStr);
                                }
                            }
                        }
                    }
                    filterList = list;
                }
                if (null != baseDir && Directory.Exists(baseDir)) {
                    var fullList = new List<string>();
                    foreach (var filter in filterList) {
                        var list = Directory.GetFiles(baseDir, filter, SearchOption.TopDirectoryOnly);
                        fullList.AddRange(list);
                    }
                    ret = CalculatorValue.FromObject(fullList);
                }
            }
            return ret;
        }
    }
    internal class ListAllDirectoriesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var baseDir = operands[0].AsString;
                baseDir = Environment.ExpandEnvironmentVariables(baseDir);
                IList<string> filterList = new string[] { "*" };
                if (operands.Count >= 2) {
                    var list = new List<string>();
                    for (int i = 1; i < operands.Count; ++i) {
                        var str = operands[i].AsString;
                        if (null != str) {
                            list.Add(str);
                        }
                        else {
                            var strList = operands[i].As<IList>();
                            if (null != strList) {
                                foreach (var strObj in strList) {
                                    var tempStr = strObj as string;
                                    if (null != tempStr)
                                        list.Add(tempStr);
                                }
                            }
                        }
                    }
                    filterList = list;
                }
                if (null != baseDir && Directory.Exists(baseDir)) {
                    var fullList = new List<string>();
                    foreach (var filter in filterList) {
                        var list = Directory.GetDirectories(baseDir, filter, SearchOption.AllDirectories);
                        fullList.AddRange(list);
                    }
                    ret = CalculatorValue.FromObject(fullList);
                }
            }
            return ret;
        }
    }
    internal class ListAllFilesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var baseDir = operands[0].AsString;
                baseDir = Environment.ExpandEnvironmentVariables(baseDir);
                IList<string> filterList = new string[] { "*" };
                if (operands.Count >= 2) {
                    var list = new List<string>();
                    for (int i = 1; i < operands.Count; ++i) {
                        var str = operands[i].AsString;
                        if (null != str) {
                            list.Add(str);
                        }
                        else {
                            var strList = operands[i].As<IList>();
                            if (null != strList) {
                                foreach (var strObj in strList) {
                                    var tempStr = strObj as string;
                                    if (null != tempStr)
                                        list.Add(tempStr);
                                }
                            }
                        }
                    }
                    filterList = list;
                }
                if (null != baseDir && Directory.Exists(baseDir)) {
                    var fullList = new List<string>();
                    foreach (var filter in filterList) {
                        var list = Directory.GetFiles(baseDir, filter, SearchOption.AllDirectories);
                        fullList.AddRange(list);
                    }
                    ret = CalculatorValue.FromObject(fullList);
                }
            }
            return ret;
        }
    }
    internal class CreateDirectoryExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 1) {
                var dir = operands[0].AsString;
                dir = Environment.ExpandEnvironmentVariables(dir);
                if (!Directory.Exists(dir)) {
                    Directory.CreateDirectory(dir);
                    ret = true;
                    Debug.LogFormat("create directory {0}", dir);
                }
            }
            return ret;
        }
    }
    internal class CopyDirectoryExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int ct = 0;
            if (operands.Count >= 2) {
                var dir1 = operands[0].AsString;
                var dir2 = operands[1].AsString;
                dir1 = Environment.ExpandEnvironmentVariables(dir1);
                dir2 = Environment.ExpandEnvironmentVariables(dir2);
                List<string> filterAndNewExts = new List<string>();
                for (int i = 2; i < operands.Count; ++i) {
                    var str = operands[i].AsString;
                    if (null != str) {
                        filterAndNewExts.Add(str);
                    }
                    else {
                        var strList = operands[i].As<IList>();
                        if (null != strList) {
                            foreach (var strObj in strList) {
                                var tempStr = strObj as string;
                                if (null != tempStr)
                                    filterAndNewExts.Add(tempStr);
                            }
                        }
                    }
                }
                if (filterAndNewExts.Count <= 0) {
                    filterAndNewExts.Add("*");
                }
                var targetRoot = Path.GetFullPath(dir2);
                if (Directory.Exists(dir1)) {
                    CopyFolder(targetRoot, dir1, dir2, filterAndNewExts, ref ct);
                }
            }
            return ct;
        }
        private static void CopyFolder(string targetRoot, string from, string to, IList<string> filterAndNewExts, ref int ct)
        {
            if (!string.IsNullOrEmpty(to) && !Directory.Exists(to))
                Directory.CreateDirectory(to);
            // 子文件夹
            foreach (string sub in Directory.GetDirectories(from)) {
                var srcPath = Path.GetFullPath(sub);
                if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) {
                    if (srcPath.IndexOf(targetRoot) == 0)
                        continue;
                }
                else {
                    if (srcPath.IndexOf(targetRoot, StringComparison.CurrentCultureIgnoreCase) == 0)
                        continue;
                }
                var sName = Path.GetFileName(sub);
                CopyFolder(targetRoot, sub, Path.Combine(to, sName), filterAndNewExts, ref ct);
            }
            // 文件
            for (int i = 0; i < filterAndNewExts.Count; i += 2) {
                string filter = filterAndNewExts[i];
                string newExt = string.Empty;
                if (i + 1 < filterAndNewExts.Count) {
                    newExt = filterAndNewExts[i + 1];
                }
                foreach (string file in Directory.GetFiles(from, filter, SearchOption.TopDirectoryOnly)) {
                    string targetFile;
                    if (string.IsNullOrEmpty(newExt))
                        targetFile = Path.Combine(to, Path.GetFileName(file));
                    else
                        targetFile = Path.Combine(to, Path.ChangeExtension(Path.GetFileName(file), newExt));
                    File.Copy(file, targetFile, true);
                    ++ct;
                    Debug.LogFormat("copy file {0} => {1}", file, targetFile);
                }
            }
        }
    }
    internal class MoveDirectoryExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 2) {
                var dir1 = operands[0].AsString;
                var dir2 = operands[1].AsString;
                dir1 = Environment.ExpandEnvironmentVariables(dir1);
                dir2 = Environment.ExpandEnvironmentVariables(dir2);
                if (Directory.Exists(dir1)) {
                    if (Directory.Exists(dir2)) {
                        Directory.Delete(dir2);
                    }
                    Directory.Move(dir1, dir2);
                    ret = true;
                    Debug.LogFormat("move directory {0} => {1}", dir1, dir2);
                }
            }
            return ret;
        }
    }
    internal class DeleteDirectoryExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 1) {
                var dir = operands[0].AsString;
                dir = Environment.ExpandEnvironmentVariables(dir);
                if (Directory.Exists(dir)) {
                    Directory.Delete(dir, true);
                    ret = true;
                    Debug.LogFormat("delete directory {0}", dir);
                }
            }
            return ret;
        }
    }
    internal class CopyFileExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 2) {
                var file1 = operands[0].AsString;
                var file2 = operands[1].AsString;
                file1 = Environment.ExpandEnvironmentVariables(file1);
                file2 = Environment.ExpandEnvironmentVariables(file2);
                if (File.Exists(file1)) {
                    var dir = Path.GetDirectoryName(file2);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) {
                        Directory.CreateDirectory(dir);
                    }
                    File.Copy(file1, file2, true);
                    ret = true;
                    Debug.LogFormat("copy file {0} => {1}", file1, file2);
                }
            }
            return ret;
        }
    }
    internal class CopyFilesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int ct = 0;
            if (operands.Count >= 2) {
                var dir1 = operands[0].AsString;
                var dir2 = operands[1].AsString;
                dir1 = Environment.ExpandEnvironmentVariables(dir1);
                dir2 = Environment.ExpandEnvironmentVariables(dir2);
                List<string> filterAndNewExts = new List<string>();
                for (int i = 2; i < operands.Count; ++i) {
                    var str = operands[i].AsString;
                    if (null != str) {
                        filterAndNewExts.Add(str);
                    }
                    else {
                        var strList = operands[i].As<IList>();
                        if (null != strList) {
                            foreach (var strObj in strList) {
                                var tempStr = strObj as string;
                                if (null != tempStr)
                                    filterAndNewExts.Add(tempStr);
                            }
                        }
                    }
                }
                if (filterAndNewExts.Count <= 0) {
                    filterAndNewExts.Add("*");
                }
                if (Directory.Exists(dir1)) {
                    CopyFolder(dir1, dir2, filterAndNewExts, ref ct);
                }
            }
            return ct;
        }
        private static void CopyFolder(string from, string to, IList<string> filterAndNewExts, ref int ct)
        {
            if (!string.IsNullOrEmpty(to) && !Directory.Exists(to))
                Directory.CreateDirectory(to);
            // 文件
            for (int i = 0; i < filterAndNewExts.Count; i += 2) {
                string filter = filterAndNewExts[i];
                string newExt = string.Empty;
                if (i + 1 < filterAndNewExts.Count) {
                    newExt = filterAndNewExts[i + 1];
                }
                foreach (string file in Directory.GetFiles(from, filter, SearchOption.TopDirectoryOnly)) {
                    string targetFile;
                    if (string.IsNullOrEmpty(newExt))
                        targetFile = Path.Combine(to, Path.GetFileName(file));
                    else
                        targetFile = Path.Combine(to, Path.ChangeExtension(Path.GetFileName(file), newExt));
                    File.Copy(file, targetFile, true);
                    ++ct;
                    Debug.LogFormat("copy file {0} => {1}", file, targetFile);
                }
            }
        }
    }
    internal class MoveFileExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 2) {
                var file1 = operands[0].AsString;
                var file2 = operands[1].AsString;
                file1 = Environment.ExpandEnvironmentVariables(file1);
                file2 = Environment.ExpandEnvironmentVariables(file2);
                if (File.Exists(file1)) {
                    var dir = Path.GetDirectoryName(file2);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) {
                        Directory.CreateDirectory(dir);
                    }
                    if (File.Exists(file2)) {
                        File.Delete(file2);
                    }
                    File.Move(file1, file2);
                    ret = true;
                    Debug.LogFormat("move file {0} => {1}", file1, file2);
                }
            }
            return ret;
        }
    }
    internal class DeleteFileExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
            if (operands.Count >= 1) {
                var file = operands[0].AsString;
                file = Environment.ExpandEnvironmentVariables(file);
                if (File.Exists(file)) {
                    File.Delete(file);
                    ret = true;
                    Debug.LogFormat("delete file {0}", file);
                }
            }
            return ret;
        }
    }
    internal class DeleteFilesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int ct = 0;
            if (operands.Count >= 1) {
                var dir = operands[0].AsString;
                List<string> filters = new List<string>();
                for (int i = 1; i < operands.Count; ++i) {
                    var str = operands[i].AsString;
                    if (null != str) {
                        filters.Add(str);
                    }
                    else {
                        var strList = operands[i].As<IList>();
                        if (null != strList) {
                            foreach (var strObj in strList) {
                                var tempStr = strObj as string;
                                if (null != tempStr)
                                    filters.Add(tempStr);
                            }
                        }
                    }
                }
                if (filters.Count <= 0) {
                    filters.Add("*");
                }
                dir = Environment.ExpandEnvironmentVariables(dir);
                if (Directory.Exists(dir)) {
                    foreach (var filter in filters) {
                        foreach (string file in Directory.GetFiles(dir, filter, SearchOption.TopDirectoryOnly)) {
                            File.Delete(file);
                            ++ct;
                            Debug.LogFormat("delete file {0}", file);
                        }
                    }
                }
            }
            return ct;
        }
    }
    internal class DeleteAllFilesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int ct = 0;
            if (operands.Count >= 1) {
                var dir = operands[0].AsString;
                List<string> filters = new List<string>();
                for (int i = 1; i < operands.Count; ++i) {
                    var str = operands[i].AsString;
                    if (null != str) {
                        filters.Add(str);
                    }
                    else {
                        var strList = operands[i].As<IList>();
                        if (null != strList) {
                            foreach (var strObj in strList) {
                                var tempStr = strObj as string;
                                if (null != tempStr)
                                    filters.Add(tempStr);
                            }
                        }
                    }
                }
                if (filters.Count <= 0) {
                    filters.Add("*");
                }
                dir = Environment.ExpandEnvironmentVariables(dir);
                if (Directory.Exists(dir)) {
                    foreach (var filter in filters) {
                        foreach (string file in Directory.GetFiles(dir, filter, SearchOption.AllDirectories)) {
                            File.Delete(file);
                            ++ct;
                            Debug.LogFormat("delete file {0}", file);
                        }
                    }
                }
            }
            return ct;
        }
    }
    internal class GetFileInfoExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var file = operands[0].AsString;
                file = Environment.ExpandEnvironmentVariables(file);
                if (File.Exists(file)) {
                    ret = CalculatorValue.FromObject(new FileInfo(file));
                }
            }
            return ret;
        }
    }
    internal class GetDirectoryInfoExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var file = operands[0].AsString;
                file = Environment.ExpandEnvironmentVariables(file);
                if (Directory.Exists(file)) {
                    ret = CalculatorValue.FromObject(new DirectoryInfo(file));
                }
            }
            return ret;
        }
    }
    internal class GetDriveInfoExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var drive = operands[0].AsString;
                ret = CalculatorValue.FromObject(new DriveInfo(drive));
            }
            return ret;
        }
    }
    internal class GetDrivesInfoExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = DriveInfo.GetDrives();
            return CalculatorValue.FromObject(ret);
        }
    }
    internal class ReadAllLinesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            if (operands.Count >= 1) {
                string path = operands[0].AsString;
                if (!string.IsNullOrEmpty(path)) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    Encoding encoding = Encoding.UTF8;
                    if (operands.Count >= 2) {
                        var v = operands[1];
                        encoding = GetEncoding(v);
                    }
                    return CalculatorValue.FromObject(File.ReadAllLines(path, encoding));
                }
            }
            return CalculatorValue.FromObject(new string[0]);
        }
    }
    internal class WriteAllLinesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            if (operands.Count >= 2) {
                string path = operands[0].AsString;
                var lines = operands[1].As<IList>();
                if (!string.IsNullOrEmpty(path) && null != lines) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    Encoding encoding = Encoding.UTF8;
                    if (operands.Count >= 3) {
                        var v = operands[2];
                        encoding = GetEncoding(v);
                    }
                    var strs = new List<string>();
                    foreach (var line in lines) {
                        strs.Add(line.ToString());
                    }
                    File.WriteAllLines(path, strs, encoding);
                    return true;
                }
            }
            return false;
        }
    }
    internal class ReadAllTextExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            if (operands.Count >= 1) {
                string path = operands[0].AsString;
                if (!string.IsNullOrEmpty(path)) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    Encoding encoding = Encoding.UTF8;
                    if (operands.Count >= 2) {
                        var v = operands[1];
                        encoding = GetEncoding(v);
                    }
                    return File.ReadAllText(path, encoding);
                }
            }
            return CalculatorValue.NullObject;
        }
    }
    internal class WriteAllTextExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            if (operands.Count >= 2) {
                string path = operands[0].AsString;
                var text = operands[1].AsString;
                if (!string.IsNullOrEmpty(path) && null != text) {
                    path = Environment.ExpandEnvironmentVariables(path);
                    Encoding encoding = Encoding.UTF8;
                    if (operands.Count >= 3) {
                        var v = operands[2];
                        encoding = GetEncoding(v);
                    }
                    File.WriteAllText(path, text, encoding);
                    return true;
                }
            }
            return false;
        }
    }
    internal class CommandExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            int exitCode = 0;
            MemoryStream ims = null, oms = null;
            int ct = m_CommandConfigs.Count;
            for (int i = 0; i < ct; ++i) {
                try {
                    if (i > 0) {
                        ims = oms;
                        oms = null;
                    }
                    if (i < ct - 1) {
                        oms = new MemoryStream();
                    }
                    var cfg = m_CommandConfigs[i];
                    if (cfg.m_Commands.Count > 0) {
                        exitCode = ExecCommand(cfg, ims, oms);
                    }
                    else {
                        exitCode = ExecProcess(cfg, ims, oms);
                    }
                }
                finally {
                    if (null != ims) {
                        ims.Close();
                        ims.Dispose();
                        ims = null;
                    }
                }
            }
            return exitCode;
        }
        protected override bool Load(Dsl.FunctionData funcData)
        {
            var id = funcData.GetId();
            if (funcData.IsHighOrder) {
                var callData = funcData.LowerOrderFunction;
                LoadCall(callData);
            }
            else if (funcData.HaveParam()) {
                LoadCall(funcData);
            }
            else {
                var cmd = new CommandConfig();
                m_CommandConfigs.Add(cmd);
            }
            if (funcData.HaveStatement()) {
                var cmd = m_CommandConfigs[m_CommandConfigs.Count - 1];
                for (int i = 0; i < funcData.GetParamNum(); ++i) {
                    var comp = funcData.GetParam(i);
                    var cd = comp as Dsl.FunctionData;
                    if (null != cd) {
                        int num = cd.GetParamNum();
                        if (cd.HaveExternScript()) {
                            string os = cd.GetId();
                            string txt = cd.GetParamId(0);
                            cmd.m_Commands.Add(os, txt);
                        }
                        else if (num >= 1) {
                            string type = cd.GetId();
                            var exp = Calculator.Load(cd.GetParam(0));
                            if (type == "input") {
                                cmd.m_Input = exp;
                            }
                            else if (type == "output") {
                                cmd.m_Output = exp;
                            }
                            else if (type == "error") {
                                cmd.m_Error = exp;
                            }
                            else if (type == "redirecttoconsole") {
                                cmd.m_RedirectToConsole = exp;
                            }
                            else if (type == "nowait") {
                                cmd.m_NoWait = exp;
                            }
                            else if (type == "useshellexecute") {
                                cmd.m_UseShellExecute = exp;
                            }
                            else if (type == "verb") {
                                cmd.m_Verb = exp;
                            }
                            else if (type == "domain") {
                                cmd.m_Domain = exp;
                            }
                            else if (type == "user") {
                                cmd.m_UserName = exp;
                            }
                            else if (type == "password") {
                                cmd.m_Password = exp;
                            }
                            else if (type == "passwordincleartext") {
                                cmd.m_PasswordInClearText = exp;
                            }
                            else if (type == "loadprofile") {
                                cmd.m_LoadUserProfile = exp;
                            }
                            else if (type == "windowstyle") {
                                cmd.m_WindowStyle = exp;
                            }
                            else if (type == "newwindow") {
                                cmd.m_NewWindow = exp;
                            }
                            else if (type == "errordialog") {
                                cmd.m_ErrorDialog = exp;
                            }
                            else if (type == "workingdirectory") {
                                cmd.m_WorkingDirectory = exp;
                            }
                            else if (type == "encoding") {
                                cmd.m_Encoding = exp;
                            }
                            else {
                                Debug.LogWarningFormat("[syntax error] {0} line:{1}", cd.ToScriptString(false), cd.GetLine());
                            }
                        }
                        else {
                            Debug.LogWarningFormat("[syntax error] {0} line:{1}", cd.ToScriptString(false), cd.GetLine());
                        }
                    }
                    else {
                        Debug.LogErrorFormat("[syntax error] {0} line:{1}", comp.ToScriptString(false), comp.GetLine());
                    }
                }
            }
            return true;
        }
        protected override bool Load(Dsl.StatementData statementData)
        {
            for (int i = 0; i < statementData.GetFunctionNum(); ++i) {
                var funcData = statementData.GetFunction(i);
                Load(funcData);
            }
            return true;
        }

        private bool LoadCall(Dsl.FunctionData callData)
        {
            var cmd = new CommandConfig();
            m_CommandConfigs.Add(cmd);

            var id = callData.GetId();
            if (id == "process") {
                int num = callData.GetParamNum();
                if (num > 0) {
                    var param0 = callData.GetParam(0);
                    var exp0 = Calculator.Load(param0);
                    cmd.m_FileName = exp0;

                    if (num > 1) {
                        var param1 = callData.GetParam(1);
                        var exp1 = Calculator.Load(param1);
                        cmd.m_Argments = exp1;
                    }
                }
                else {
                    Debug.LogErrorFormat("[syntax error] {0} line:{1}", callData.ToScriptString(false), callData.GetLine());
                }
            }
            else if (id == "command") {
                int num = callData.GetParamNum();
                if (num > 0) {
                    Debug.LogErrorFormat("[syntax error] {0} line:{1}", callData.ToScriptString(false), callData.GetLine());
                }
            }
            else {
                Debug.LogErrorFormat("[syntax error] {0} line:{1}", callData.ToScriptString(false), callData.GetLine());
            }
            return true;
        }
        private int ExecProcess(CommandConfig cfg, Stream istream, Stream ostream)
        {
            string fileName = string.Empty;
            if (null != cfg.m_FileName) {
                fileName = cfg.m_FileName.Calc().AsString;
            }
            string args = string.Empty;
            if (null != cfg.m_Argments) {
                args = cfg.m_Argments.Calc().AsString;
            }
            bool noWait = false;
            if (null != cfg.m_NoWait) {
                noWait = cfg.m_NoWait.Calc().Get<bool>();
            }
            DslCalculator.ProcessStartOption option = new DslCalculator.ProcessStartOption();
            if (null != cfg.m_UseShellExecute) {
                option.UseShellExecute = cfg.m_UseShellExecute.Calc().Get<bool>();
            }
            if (null != cfg.m_Verb) {
                option.Verb = cfg.m_Verb.Calc().AsString;
            }
            if (null != cfg.m_Domain) {
                option.Domain = cfg.m_Domain.Calc().AsString;
            }
            if (null != cfg.m_UserName) {
                option.UserName = cfg.m_UserName.Calc().AsString;
            }
            if (null != cfg.m_Password) {
                option.Password = cfg.m_Password.Calc().AsString;
            }
            if (null != cfg.m_PasswordInClearText) {
                option.PasswordInClearText = cfg.m_PasswordInClearText.Calc().AsString;
            }
            if (null != cfg.m_LoadUserProfile) {
                option.LoadUserProfile = cfg.m_LoadUserProfile.Calc().Get<bool>();
            }
            if (null != cfg.m_WindowStyle) {
                var str = cfg.m_WindowStyle.Calc().AsString;
                System.Diagnostics.ProcessWindowStyle style;
                if (Enum.TryParse(str, out style)) {
                    option.WindowStyle = style;
                }
            }
            if (null != cfg.m_NewWindow) {
                option.NewWindow = cfg.m_NewWindow.Calc().Get<bool>();
            }
            if (null != cfg.m_ErrorDialog) {
                option.ErrorDialog = cfg.m_ErrorDialog.Calc().Get<bool>();
            }
            if (null != cfg.m_WorkingDirectory) {
                option.WorkingDirectory = cfg.m_WorkingDirectory.Calc().AsString;
            }
            Encoding encoding = null;
            if (null != cfg.m_Encoding) {
                var v = cfg.m_Encoding.Calc();
                var name = v.AsString;
                if (!string.IsNullOrEmpty(name)) {
                    encoding = Encoding.GetEncoding(name);
                }
                else if (v.IsInteger) {
                    int codePage = v.Get<int>();
                    encoding = Encoding.GetEncoding(codePage);
                }
            }
            if (null == encoding) {
                encoding = Encoding.UTF8;
            }

            fileName = Environment.ExpandEnvironmentVariables(fileName);
            args = Environment.ExpandEnvironmentVariables(args);

            IList<string> input = null;
            if (null != cfg.m_Input) {
                var v = cfg.m_Input.Calc();
                try {
                    var str = v.AsString;
                    if (!string.IsNullOrEmpty(str)) {
                        if (str[0] == '@' || str[0] == '$') {
                            var val = Calculator.GetVariable(str);
                            if (!val.IsNullObject) {
                                var slist = new List<string>();
                                var list = val.As<IList>();
                                foreach (var s in list) {
                                    slist.Add(s.ToString());
                                }
                                input = slist;
                            }
                        }
                        else {
                            str = Environment.ExpandEnvironmentVariables(str);
                            input = File.ReadAllLines(str);
                        }
                    }
                    else {
                        int vn = v.Get<int>();
                        object val = Calculator.GetVariable(vn);
                        if (null != val) {
                            var slist = new List<string>();
                            var list = val as IList;
                            foreach (var s in list) {
                                slist.Add(s.ToString());
                            }
                            input = slist;
                        }
                    }
                }
                catch (Exception ex) {
                    Calculator.Log("input {0} failed:{1}", v, ex.Message);
                }
            }
            bool redirectToConsole = false;
            StringBuilder outputBuilder = null;
            StringBuilder errorBuilder = null;
            var output = CalculatorValue.NullObject;
            var error = CalculatorValue.NullObject;
            if (null != cfg.m_Output) {
                var v = cfg.m_Output.Calc();
                var str = v.AsString;
                if (!string.IsNullOrEmpty(str)) {
                    str = Environment.ExpandEnvironmentVariables(str);
                    output = str;
                }
                else {
                    output = v;
                }
                outputBuilder = new StringBuilder();
            }
            if (null != cfg.m_Error) {
                var v = cfg.m_Error.Calc();
                var str = v.AsString;
                if (!string.IsNullOrEmpty(str)) {
                    str = Environment.ExpandEnvironmentVariables(str);
                    error = str;
                }
                else {
                    error = v;
                }
                errorBuilder = new StringBuilder();
            }
            if (null != cfg.m_RedirectToConsole) {
                var v = cfg.m_RedirectToConsole.Calc();
                redirectToConsole = v.Get<bool>();
            }
            int exitCode = DslCalculator.NewProcess(noWait, fileName, args, option, istream, ostream, input, outputBuilder, errorBuilder, redirectToConsole, encoding);
            Debug.LogFormat("new process:{0} {1}, exit code:{2}", fileName, args, exitCode);

            if (null != outputBuilder && !output.IsNullObject) {
                try {
                    var file = output.AsString;
                    if (!string.IsNullOrEmpty(file)) {
                        if (file[0] == '@' || file[0] == '$') {
                            Calculator.SetVariable(file, outputBuilder.ToString());
                        }
                        else {
                            File.WriteAllText(file, outputBuilder.ToString());
                        }
                    }
                    else {
                        int v = output.Get<int>();
                        Calculator.SetVariable(v, outputBuilder.ToString());
                    }
                }
                catch (Exception ex) {
                    Calculator.Log("output {0} failed:{1}", output, ex.Message);
                }
            }
            if (null != errorBuilder && !error.IsNullObject) {
                try {
                    var file = error.AsString;
                    if (!string.IsNullOrEmpty(file)) {
                        if (file[0] == '@' || file[0] == '$') {
                            Calculator.SetVariable(file, errorBuilder.ToString());
                        }
                        else {
                            File.WriteAllText(file, errorBuilder.ToString());
                        }
                    }
                    else {
                        int v = error.Get<int>();
                        Calculator.SetVariable(v, errorBuilder.ToString());
                    }
                }
                catch (Exception ex) {
                    Calculator.Log("error {0} failed:{1}", error, ex.Message);
                }
            }
            return exitCode;
        }
        private int ExecCommand(CommandConfig cfg, Stream istream, Stream ostream)
        {
            int exitCode = 0;
            string os = string.Empty;
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                os = "unix";
            else
                os = "win";
            string cmd;
            if (cfg.m_Commands.TryGetValue(os, out cmd) || cfg.m_Commands.TryGetValue("common", out cmd)) {
                bool noWait = false;
                if (null != cfg.m_NoWait) {
                    noWait = cfg.m_NoWait.Calc().Get<bool>();
                }
                DslCalculator.ProcessStartOption option = new DslCalculator.ProcessStartOption();
                if (null != cfg.m_UseShellExecute) {
                    option.UseShellExecute = cfg.m_UseShellExecute.Calc().Get<bool>();
                }
                if (null != cfg.m_Verb) {
                    option.Verb = cfg.m_Verb.Calc().AsString;
                }
                if (null != cfg.m_Domain) {
                    option.Domain = cfg.m_Domain.Calc().AsString;
                }
                if (null != cfg.m_UserName) {
                    option.UserName = cfg.m_UserName.Calc().AsString;
                }
                if (null != cfg.m_Password) {
                    option.Password = cfg.m_Password.Calc().AsString;
                }
                if (null != cfg.m_PasswordInClearText) {
                    option.PasswordInClearText = cfg.m_PasswordInClearText.Calc().AsString;
                }
                if (null != cfg.m_LoadUserProfile) {
                    option.LoadUserProfile = cfg.m_LoadUserProfile.Calc().Get<bool>();
                }
                if (null != cfg.m_WindowStyle) {
                    var str = cfg.m_WindowStyle.Calc().AsString;
                    System.Diagnostics.ProcessWindowStyle style;
                    if (Enum.TryParse(str, out style)) {
                        option.WindowStyle = style;
                    }
                }
                if (null != cfg.m_NewWindow) {
                    option.NewWindow = cfg.m_NewWindow.Calc().Get<bool>();
                }
                if (null != cfg.m_ErrorDialog) {
                    option.ErrorDialog = cfg.m_ErrorDialog.Calc().Get<bool>();
                }
                if (null != cfg.m_WorkingDirectory) {
                    option.WorkingDirectory = cfg.m_WorkingDirectory.Calc().AsString;
                }
                Encoding encoding = null;
                if (null != cfg.m_Encoding) {
                    var v = cfg.m_Encoding.Calc();
                    var name = v.AsString;
                    if (!string.IsNullOrEmpty(name)) {
                        encoding = Encoding.GetEncoding(name);
                    }
                    else if (v.IsInteger) {
                        int codePage = v.Get<int>();
                        encoding = Encoding.GetEncoding(codePage);
                    }
                }
                if (null == encoding) {
                    encoding = Encoding.UTF8;
                }
                IList<string> input = null;
                if (null != cfg.m_Input) {
                    var v = cfg.m_Input.Calc();
                    try {
                        var str = v.AsString;
                        if (!string.IsNullOrEmpty(str)) {
                            if (str[0] == '@' || str[0] == '$') {
                                object val = Calculator.GetVariable(str);
                                if (null != val) {
                                    var slist = new List<string>();
                                    var list = val as IList;
                                    foreach (var s in list) {
                                        slist.Add(s.ToString());
                                    }
                                    input = slist;
                                }
                            }
                            else {
                                str = Environment.ExpandEnvironmentVariables(str);
                                input = File.ReadAllLines(str);
                            }
                        }
                        else {
                            int vn = v.Get<int>();
                            object val = Calculator.GetVariable(vn);
                            if (null != val) {
                                var slist = new List<string>();
                                var list = val as IList;
                                foreach (var s in list) {
                                    slist.Add(s.ToString());
                                }
                                input = slist;
                            }
                        }
                    }
                    catch (Exception ex) {
                        Calculator.Log("input {0} failed:{1}", v, ex.Message);
                    }
                }
                bool redirectToConsole = false;
                StringBuilder outputBuilder = null;
                StringBuilder errorBuilder = null;
                var output = CalculatorValue.NullObject;
                var error = CalculatorValue.NullObject;
                if (null != cfg.m_Output) {
                    var v = cfg.m_Output.Calc();
                    var str = v.AsString;
                    if (!string.IsNullOrEmpty(str)) {
                        str = Environment.ExpandEnvironmentVariables(str);
                        output = str;
                    }
                    else {
                        output = v;
                    }
                    outputBuilder = new StringBuilder();
                }
                if (null != cfg.m_Error) {
                    var v = cfg.m_Error.Calc();
                    var str = v.AsString;
                    if (!string.IsNullOrEmpty(str)) {
                        str = Environment.ExpandEnvironmentVariables(str);
                        error = str;
                    }
                    else {
                        error = v;
                    }
                    errorBuilder = new StringBuilder();
                }
                if (null != cfg.m_RedirectToConsole) {
                    var v = cfg.m_RedirectToConsole.Calc();
                    redirectToConsole = v.Get<bool>();
                }

                cmd = cmd.Trim();
                var lines = cmd.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string oneCmd = string.Join(" ", lines).Trim();
                if (!string.IsNullOrEmpty(oneCmd)) {
                    int split = oneCmd.IndexOfAny(new char[] { ' ', '\t' });
                    string fileName = oneCmd;
                    string args = string.Empty;
                    if (split > 0) {
                        fileName = oneCmd.Substring(0, split).Trim();
                        args = oneCmd.Substring(split).Trim();
                    }

                    fileName = Environment.ExpandEnvironmentVariables(fileName);
                    args = Environment.ExpandEnvironmentVariables(args);

                    exitCode = DslCalculator.NewProcess(noWait, fileName, args, option, istream, ostream, input, outputBuilder, errorBuilder, redirectToConsole, encoding);
                    Debug.LogFormat("new process:{0} {1}, exit code:{2}", fileName, args, exitCode);

                    if (null != outputBuilder && !output.IsNullObject) {
                        try {
                            var file = output.AsString;
                            if (!string.IsNullOrEmpty(file)) {
                                if (file[0] == '@' || file[0] == '$') {
                                    Calculator.SetVariable(file, outputBuilder.ToString());
                                }
                                else {
                                    File.WriteAllText(file, outputBuilder.ToString());
                                }
                            }
                            else {
                                int v = output.Get<int>();
                                Calculator.SetVariable(v, outputBuilder.ToString());
                            }
                        }
                        catch (Exception ex) {
                            Calculator.Log("output {0} failed:{1}", output, ex.Message);
                        }
                    }
                    if (null != errorBuilder && !error.IsNullObject) {
                        try {
                            var file = error.AsString;
                            if (!string.IsNullOrEmpty(file)) {
                                if (file[0] == '@' || file[0] == '$') {
                                    Calculator.SetVariable(file, errorBuilder.ToString());
                                }
                                else {
                                    File.WriteAllText(file, errorBuilder.ToString());
                                }
                            }
                            else {
                                int v = error.Get<int>();
                                Calculator.SetVariable(v, errorBuilder.ToString());
                            }
                        }
                        catch (Exception ex) {
                            Calculator.Log("error {0} failed:{1}", error, ex.Message);
                        }
                    }
                }
            }
            return exitCode;
        }

        private class CommandConfig
        {
            internal IExpression m_FileName = null;
            internal IExpression m_Argments = null;
            internal Dictionary<string, string> m_Commands = new Dictionary<string, string>();

            internal IExpression m_NoWait = null;
            internal IExpression m_UseShellExecute = null;
            internal IExpression m_Verb = null;
            internal IExpression m_Domain = null;
            internal IExpression m_UserName = null;
            internal IExpression m_Password = null;
            internal IExpression m_PasswordInClearText = null;
            internal IExpression m_LoadUserProfile = null;
            internal IExpression m_WindowStyle = null;
            internal IExpression m_NewWindow = null;
            internal IExpression m_ErrorDialog = null;
            internal IExpression m_WorkingDirectory = null;
            internal IExpression m_Encoding = null;
            internal IExpression m_Input = null;
            internal IExpression m_Output = null;
            internal IExpression m_Error = null;
            internal IExpression m_RedirectToConsole = null;
        }

        private List<CommandConfig> m_CommandConfigs = new List<CommandConfig>();
    }
    internal class KillExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int ret = 0;
            if (operands.Count >= 1) {
                int myselfId = 0;
                var myself = System.Diagnostics.Process.GetCurrentProcess();
                if (null != myself) {
                    myselfId = myself.Id;
                }
                var vObj = operands[0];
                var name = vObj.AsString;
                if (!string.IsNullOrEmpty(name)) {
                    int ct = 0;
                    var ps = System.Diagnostics.Process.GetProcessesByName(name);
                    foreach (var p in ps) {
                        if (p.Id != myselfId) {
                            Debug.LogFormat("kill {0}[pid:{1},session id:{2}]", p.ProcessName, p.Id, p.SessionId);
                            p.Kill();
                            ++ct;
                        }
                    }
                    ret = ct;
                }
                else if (vObj.IsInteger) {
                    int pid = vObj.Get<int>();
                    var p = System.Diagnostics.Process.GetProcessById(pid);
                    if (null != p && p.Id != myselfId) {
                        Debug.LogFormat("kill {0}[pid:{1},session id:{2}]", p.ProcessName, p.Id, p.SessionId);
                        p.Kill();
                        ret = 1;
                    }
                }
                else {

                }
            }
            return CalculatorValue.From(ret);
        }
    }
    internal class KillMeExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int ret = 0;
            var p = System.Diagnostics.Process.GetCurrentProcess();
            if (null != p) {
                ret = p.Id;
                int exitCode = 0;
                if (operands.Count >= 1) {
                    exitCode = operands[0].Get<int>();
                }
                Debug.LogFormat("killme {0}[pid:{1},session id:{2}] exit code:{3}", p.ProcessName, p.Id, p.SessionId, exitCode);
                Environment.Exit(exitCode);
            }
            return ret;
        }
    }
    internal class GetCurrentProcessIdExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            int ret = 0;
            var p = System.Diagnostics.Process.GetCurrentProcess();
            if (null != p) {
                ret = p.Id;
            }
            return ret;
        }
    }
    internal class ListProcessesExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            IList<System.Diagnostics.Process> ret = null;
            var ps = System.Diagnostics.Process.GetProcesses();
            string filter = null;
            if (operands.Count >= 1) {
                filter = operands[0].AsString;
            }
            if (null == filter)
                filter = string.Empty;
            if (!string.IsNullOrEmpty(filter)) {
                var list = new List<System.Diagnostics.Process>();
                foreach (var p in ps) {
                    try {
                        if (!p.HasExited) {
                            if (p.ProcessName.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0) {
                                list.Add(p);
                            }
                        }
                    }
                    catch {
                    }
                }
                ret = list;
            }
            else {
                ret = ps;
            }
            return CalculatorValue.FromObject(ret);

        }
    }
    internal class WaitExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var time = operands[0].Get<int>();
                System.Threading.Thread.Sleep(time);
                ret = time;
            }
            return ret;
        }
    }
    internal class WaitAllExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var tasks = DslCalculator.Tasks;
            int timeout = -1;
            if (operands.Count >= 1) {
                timeout = operands[0].Get<int>();
            }
            List<int> results = new List<int>();
            if (Task.WaitAll(tasks.ToArray(), timeout)) {
                foreach (var task in tasks) {
                    results.Add(task.Result);
                }
            }
            return CalculatorValue.FromObject(results);
        }
    }
    internal class WaitStartIntervalExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            if (operands.Count >= 1) {
                var v = operands[0];
                if (!v.IsNullObject) {
                    DslCalculator.CheckStartInterval = v.Get<int>();
                }
            }
            return DslCalculator.CheckStartInterval;
        }
    }

    internal class DisplayProgressBarExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            if (operands.Count >= 3) {
                var title = operands[0].AsString;
                var text = operands[1].AsString;
                var progress = operands[2].Get<float>();
                if (null != title && null != text) {
                    EditorUtility.DisplayProgressBar(title, text, progress);
                }
            }
#endif
            return true;
        }
    }
    internal class DisplayCancelableProgressBarExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            bool ret = false;
#if UNITY_EDITOR
            if (operands.Count >= 3) {
                var title = operands[0].AsString;
                var text = operands[1].AsString;
                var progress = operands[2].Get<float>();
                if (null != title && null != text) {
                    ret = EditorUtility.DisplayCancelableProgressBar(title, text, progress);
                }
            }
#endif
            return ret;
        }
    }
    internal class ClearProgressBarExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
#endif
            return true;
        }
    }
    internal class OpenWithDefaultAppExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            if (operands.Count >= 1) {
                string file = operands[0].AsString;
                if (!string.IsNullOrEmpty(file)) {
                    EditorUtility.OpenWithDefaultApp(file);
                }
            }
#endif
            return CalculatorValue.NullObject;
        }
    }
    internal class OpenFolderPanelExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            string ret = null;
            if (operands.Count >= 3) {
                string title = operands[0].AsString;
                string dir = operands[1].AsString;
                string def = operands[2].AsString;
                if (null != title && null != dir && null != def) {
                    ret = EditorUtility.OpenFolderPanel(title, dir, def);
                }
            }
            return ret;
#else
            return CalculatorValue.NullObject;
#endif
        }
    }
    internal class OpenFilePanelExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            string ret = null;
            if (operands.Count >= 3) {
                string title = operands[0].AsString;
                string dir = operands[1].AsString;
                string ext = operands[2].AsString;
                if (null != title && null != dir && null != ext) {
                    ret = EditorUtility.OpenFilePanel(title, dir, ext);
                }
            }
            return ret;
#else
            return CalculatorValue.NullObject;
#endif
        }
    }
    internal class OpenFilePanelWithFiltersExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            string ret = null;
            if (operands.Count >= 3) {
                string title = operands[0].AsString;
                string dir = operands[1].AsString;
                List<string> filters = new List<string>();
                for (int i = 2; i < operands.Count; ++i) {
                    string filter = operands[i].AsString;
                    if (!string.IsNullOrEmpty(filter)) {
                        filters.Add(filter);
                    }
                }
                if (null != title && null != dir) {
                    ret = EditorUtility.OpenFilePanelWithFilters(title, dir, filters.ToArray());
                }
            }
            return ret;
#else
            return CalculatorValue.NullObject;
#endif
        }
    }
    internal class SaveFilePanelExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            string ret = null;
            if (operands.Count >= 4) {
                string title = operands[0].AsString;
                string dir = operands[1].AsString;
                string def = operands[2].AsString;
                string ext = operands[3].AsString;
                if (null != title && null != dir && null != def && null != ext) {
                    ret = EditorUtility.SaveFilePanel(title, dir, def, ext);
                }
            }
            return ret;
#else
            return CalculatorValue.NullObject;
#endif
        }
    }
    internal class SaveFilePanelInProjectExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            string ret = null;
            if (operands.Count >= 4) {
                string title = operands[0].AsString;
                string def = operands[1].AsString;
                string ext = operands[2].AsString;
                string msg = operands[3].AsString;
                string path = string.Empty;
                if (operands.Count >= 5) {
                    path = operands[4].AsString;
                }
                if (null != title && null != def && null != ext && null != msg) {
                    if (!string.IsNullOrEmpty(path)) {
                        ret = EditorUtility.SaveFilePanelInProject(title, def, ext, msg, path);
                    }
                    else {
                        ret = EditorUtility.SaveFilePanelInProject(title, def, ext, msg);
                    }
                }
            }
            return ret;
#else
            return CalculatorValue.NullObject;
#endif
        }
    }
    internal class SaveFolderPanelExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            string ret = null;
            if (operands.Count >= 3) {
                string title = operands[0].AsString;
                string dir = operands[1].AsString;
                string def = operands[2].AsString;
                if (null != title && null != dir && null != def) {
                    ret = EditorUtility.SaveFolderPanel(title, dir, def);
                }
            }
            return ret;
#else
            return CalculatorValue.NullObject;
#endif
        }
    }
    internal class DisplayDialogExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
#if UNITY_EDITOR
            int ret = -1;
            if (operands.Count >= 3) {
                string title = operands[0].AsString;
                string msg = operands[1].AsString;
                string ok = operands[2].AsString;
                if (null != title && null != msg && null != ok) {
                    if (operands.Count >= 4) {
                        string cancel = operands[3].AsString;
                        if (operands.Count >= 5) {
                            string alt = operands[4].AsString;
                            ret = EditorUtility.DisplayDialogComplex(title, msg, ok, cancel, alt);
                        }
                        else {
                            ret = EditorUtility.DisplayDialog(title, msg, ok, cancel) ? 1 : 0;
                        }
                    }
                    else {
                        ret = EditorUtility.DisplayDialog(title, msg, ok) ? 1 : 0;
                    }
                }
            }
            return ret;
#else
            return -1;
#endif
        }
    }
    internal class CalcMd5Exp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            CalculatorValue r = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                var file = operands[0].AsString;
                if (null != file) {
                    r = CalcMD5(file);
                }
            }
            return r;
        }
        public string CalcMD5(string file)
        {
            byte[] array = null;
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                MD5 md5 = MD5.Create();
                array = md5.ComputeHash(stream);
                stream.Close();
            }
            if (null != array) {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++) {
                    stringBuilder.Append(array[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
            else {
                return string.Empty;
            }
        }
    }
#if QINSHI
    internal class DebugValueExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            string ret = string.Empty;
            if (operands.Count >= 1) {
                string name = operands[0].AsString;
                if (operands.Count >= 2) {
                    var val = operands[1];
                    if (!string.IsNullOrEmpty(name)) {
                        string valStr = val.AsString;
                        GameFramework.GmCommands.GlobalDebugValues.SetValue(name, valStr);
                        ret = valStr;
                    }
                } else {
                    ret = GameFramework.GmCommands.GlobalDebugValues.GetStringValue(name);
                }
            }
            return ret;
        }
    }
    internal class StoryVarExp : SimpleExpressionBase
    {
        protected override CalculatorValue OnCalc(IList<CalculatorValue> operands)
        {
            var ret = CalculatorValue.NullObject;
            if (operands.Count >= 1) {
                string name = operands[0].AsString;
                if (operands.Count >= 2) {
                    var val = operands[1];
                    if (!string.IsNullOrEmpty(name)) {
                        var instance = CsLibrary.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
                        if (null == instance) {
                            string txt = "script(main){onmessage(\"start\"){};};";
                            CsLibrary.GmCommands.ClientGmStorySystem.Instance.LoadStoryText(Encoding.UTF8.GetBytes(txt));
                            instance = CsLibrary.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
                        }
                        instance.SetVariable(name, BoxedValue.FromObject(val.GetObject()));
                        ret = val;
                    }
                }
                else {
                    var instance = CsLibrary.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
                    if (null == instance) {
                        string txt = "script(main){onmessage(\"start\"){};};";
                        CsLibrary.GmCommands.ClientGmStorySystem.Instance.LoadStoryText(Encoding.UTF8.GetBytes(txt));
                        instance = CsLibrary.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
                    }
                    BoxedValue bv;
                    instance.TryGetVariable(name, out bv);
                    ret = CalculatorValue.FromObject(bv.GetObject());
                }
            }
            return ret;
        }
    }
    internal class StoryValueExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var instance = CsLibrary.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
            if (null == instance) {
                string txt = "script(main){onmessage(\"start\"){};};";
                GameFramework.GmCommands.ClientGmStorySystem.Instance.LoadStoryText(Encoding.UTF8.GetBytes(txt));
                GameFramework.GmCommands.ClientGmStorySystem.Instance.StartStory("main");
                instance = GameFramework.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
            }
            var handler = instance.GetMessageHandler("start");
            object ret = null;
            foreach (var exp in m_Values) {
                exp.Evaluate(instance, handler, BoxedValue.NullObject, null);
                if (exp.HaveValue) {
                    ret = exp.Value.GetObject();
                }
            }
            return CalculatorValue.FromObject(ret);
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            int num = callData.GetParamNum();
            for (int ix = 0; ix < num; ++ix) {
                Dsl.ISyntaxComponent param = callData.GetParam(ix);
                var exp = StorySystem.StoryValueManager.Instance.CalcValue(param);
                m_Values.Add(exp);
            }
            return true;
        }

        private List<StorySystem.IStoryValue> m_Values = new List<StorySystem.IStoryValue>();
    }
    internal class StoryCommandExp : AbstractExpression
    {
        protected override CalculatorValue DoCalc()
        {
            var instance = GameFramework.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
            if (null == instance) {
                string txt = "script(main){onmessage(\"start\"){};};";
                GameFramework.GmCommands.ClientGmStorySystem.Instance.LoadStoryText(Encoding.UTF8.GetBytes(txt));
                instance = GameFramework.GmCommands.ClientGmStorySystem.Instance.GetStory("main");
            }
            var handler = instance.GetMessageHandler("start");
            foreach (var cmd in m_Commands) {
                cmd.Reset();
            }
            foreach (var cmd in m_Commands) {
                cmd.Execute(instance, handler, 0, BoxedValue.NullObject, null);
            }
            return CalculatorValue.NullObject;
        }
        protected override bool Load(Dsl.FunctionData callData)
        {
            int num = callData.GetParamNum();
            for (int ix = 0; ix < num; ++ix) {
                Dsl.ISyntaxComponent param = callData.GetParam(ix);
                var cmd = StorySystem.StoryCommandManager.Instance.CreateCommand(param);
                m_Commands.Add(cmd);
            }
            return true;
        }

        private List<StorySystem.IStoryCommand> m_Commands = new List<StorySystem.IStoryCommand>();
    }
#endif
    public enum RunStateEnum
    {
        Normal = 0,
        Break,
        Continue,
        Return,
        Redirect,
    }
    public sealed class DslCalculator
    {
        public Dsl.DslLogDelegation OnLog;
        public IDictionary<string, CalculatorValue> NamedGlobalVariables
        {
            get { return m_NamedGlobalVariables; }
        }
        public void Init()
        {
            Register("args", new ExpressionFactoryHelper<ArgsGet>());
            Register("arg", new ExpressionFactoryHelper<ArgGet>());
            Register("argnum", new ExpressionFactoryHelper<ArgNumGet>());
            Register("var", new ExpressionFactoryHelper<VarGet>());
            Register("+", new ExpressionFactoryHelper<AddExp>());
            Register("-", new ExpressionFactoryHelper<SubExp>());
            Register("*", new ExpressionFactoryHelper<MulExp>());
            Register("/", new ExpressionFactoryHelper<DivExp>());
            Register("%", new ExpressionFactoryHelper<ModExp>());
            Register("&", new ExpressionFactoryHelper<BitAndExp>());
            Register("|", new ExpressionFactoryHelper<BitOrExp>());
            Register("^", new ExpressionFactoryHelper<BitXorExp>());
            Register("~", new ExpressionFactoryHelper<BitNotExp>());
            Register("<<", new ExpressionFactoryHelper<LShiftExp>());
            Register(">>", new ExpressionFactoryHelper<RShiftExp>());
            Register("max", new ExpressionFactoryHelper<MaxExp>());
            Register("min", new ExpressionFactoryHelper<MinExp>());
            Register("abs", new ExpressionFactoryHelper<AbsExp>());
            Register("sin", new ExpressionFactoryHelper<SinExp>());
            Register("cos", new ExpressionFactoryHelper<CosExp>());
            Register("tan", new ExpressionFactoryHelper<TanExp>());
            Register("asin", new ExpressionFactoryHelper<AsinExp>());
            Register("acos", new ExpressionFactoryHelper<AcosExp>());
            Register("atan", new ExpressionFactoryHelper<AtanExp>());
            Register("atan2", new ExpressionFactoryHelper<Atan2Exp>());
            Register("sinh", new ExpressionFactoryHelper<SinhExp>());
            Register("cosh", new ExpressionFactoryHelper<CoshExp>());
            Register("tanh", new ExpressionFactoryHelper<TanhExp>());
            Register("rndint", new ExpressionFactoryHelper<RndIntExp>());
            Register("rndfloat", new ExpressionFactoryHelper<RndFloatExp>());
            Register("pow", new ExpressionFactoryHelper<PowExp>());
            Register("sqrt", new ExpressionFactoryHelper<SqrtExp>());
            Register("log", new ExpressionFactoryHelper<LogExp>());
            Register("log10", new ExpressionFactoryHelper<Log10Exp>());
            Register("floor", new ExpressionFactoryHelper<FloorExp>());
            Register("ceil", new ExpressionFactoryHelper<CeilExp>());
            Register("lerp", new ExpressionFactoryHelper<LerpExp>());
            Register("lerpunclamped", new ExpressionFactoryHelper<LerpUnclampedExp>());
            Register("lerpangle", new ExpressionFactoryHelper<LerpAngleExp>());
            Register("clamp01", new ExpressionFactoryHelper<Clamp01Exp>());
            Register("clamp", new ExpressionFactoryHelper<ClampExp>());
            Register("approximately", new ExpressionFactoryHelper<ApproximatelyExp>());
            Register("ispoweroftwo", new ExpressionFactoryHelper<IsPowerOfTwoExp>());
            Register("closestpoweroftwo", new ExpressionFactoryHelper<ClosestPowerOfTwoExp>());
            Register("nextpoweroftwo", new ExpressionFactoryHelper<NextPowerOfTwoExp>());
            Register("dist", new ExpressionFactoryHelper<DistExp>());
            Register("distsqr", new ExpressionFactoryHelper<DistSqrExp>());
            Register(">", new ExpressionFactoryHelper<GreatExp>());
            Register(">=", new ExpressionFactoryHelper<GreatEqualExp>());
            Register("<", new ExpressionFactoryHelper<LessExp>());
            Register("<=", new ExpressionFactoryHelper<LessEqualExp>());
            Register("==", new ExpressionFactoryHelper<EqualExp>());
            Register("!=", new ExpressionFactoryHelper<NotEqualExp>());
            Register("&&", new ExpressionFactoryHelper<AndExp>());
            Register("||", new ExpressionFactoryHelper<OrExp>());
            Register("!", new ExpressionFactoryHelper<NotExp>());
            Register("?", new ExpressionFactoryHelper<CondExp>());
            Register("if", new ExpressionFactoryHelper<IfExp>());
            Register("while", new ExpressionFactoryHelper<WhileExp>());
            Register("loop", new ExpressionFactoryHelper<LoopExp>());
            Register("looplist", new ExpressionFactoryHelper<LoopListExp>());
            Register("foreach", new ExpressionFactoryHelper<ForeachExp>());
            Register("format", new ExpressionFactoryHelper<FormatExp>());
            Register("gettypeassemblyname", new ExpressionFactoryHelper<GetTypeAssemblyNameExp>());
            Register("gettypefullname", new ExpressionFactoryHelper<GetTypeFullNameExp>());
            Register("gettypename", new ExpressionFactoryHelper<GetTypeNameExp>());
            Register("gettype", new ExpressionFactoryHelper<GetTypeExp>());
            Register("changetype", new ExpressionFactoryHelper<ChangeTypeExp>());
            Register("parseenum", new ExpressionFactoryHelper<ParseEnumExp>());
            Register("dotnetcall", new ExpressionFactoryHelper<DotnetCallExp>());
            Register("dotnetset", new ExpressionFactoryHelper<DotnetSetExp>());
            Register("dotnetget", new ExpressionFactoryHelper<DotnetGetExp>());
            Register("linq", new ExpressionFactoryHelper<LinqExp>());
            Register("isnull", new ExpressionFactoryHelper<IsNullExp>());
            Register("dotnetload", new ExpressionFactoryHelper<DotnetLoadExp>());
            Register("dotnetnew", new ExpressionFactoryHelper<DotnetNewExp>());
            Register("assetpath2guid", new ExpressionFactoryHelper<AssetPath2GUIDExp>());
            Register("guid2assetpath", new ExpressionFactoryHelper<GUID2AssetPathExp>());
            Register("getassetpath", new ExpressionFactoryHelper<GetAssetPathExp>());
            Register("getguidandfileid", new ExpressionFactoryHelper<GetGuidAndLocalFileIdentifierExp>());
            Register("getdependencies", new ExpressionFactoryHelper<GetDependenciesExp>());
            Register("getassetimporter", new ExpressionFactoryHelper<GetAssetImporterExp>());
            Register("loadasset", new ExpressionFactoryHelper<LoadAssetExp>());
            Register("unloadasset", new ExpressionFactoryHelper<UnloadAssetExp>());
            Register("getprefabtype", new ExpressionFactoryHelper<GetPrefabTypeExp>());
            Register("getprefabstatus", new ExpressionFactoryHelper<GetPrefabStatusExp>());
            Register("getprefabobject", new ExpressionFactoryHelper<GetPrefabObjectExp>());
            Register("getprefabparent", new ExpressionFactoryHelper<GetPrefabParentExp>());
            Register("destroyobject", new ExpressionFactoryHelper<DestroyObjectExp>());
            Register("getcomponent", new ExpressionFactoryHelper<GetComponentExp>());
            Register("getcomponents", new ExpressionFactoryHelper<GetComponentsExp>());
            Register("getcomponentinchildren", new ExpressionFactoryHelper<GetComponentInChildrenExp>());
            Register("getcomponentsinchildren", new ExpressionFactoryHelper<GetComponentsInChildrenExp>());
            Register("newstringbuilder", new ExpressionFactoryHelper<NewStringBuilderExp>());
            Register("appendformat", new ExpressionFactoryHelper<AppendFormatExp>());
            Register("appendlineformat", new ExpressionFactoryHelper<AppendLineFormatExp>());
            Register("stringbuildertostring", new ExpressionFactoryHelper<StringBuilderToStringExp>());
            Register("stringjoin", new ExpressionFactoryHelper<StringJoinExp>());
            Register("stringsplit", new ExpressionFactoryHelper<StringSplitExp>());
            Register("stringtrim", new ExpressionFactoryHelper<StringTrimExp>());
            Register("stringtrimstart", new ExpressionFactoryHelper<StringTrimStartExp>());
            Register("stringtrimend", new ExpressionFactoryHelper<StringTrimEndExp>());
            Register("stringtolower", new ExpressionFactoryHelper<StringToLowerExp>());
            Register("stringtoupper", new ExpressionFactoryHelper<StringToUpperExp>());
            Register("stringreplace", new ExpressionFactoryHelper<StringReplaceExp>());
            Register("stringreplacechar", new ExpressionFactoryHelper<StringReplaceCharExp>());
            Register("makestring", new ExpressionFactoryHelper<MakeStringExp>());
            Register("stringcontains", new ExpressionFactoryHelper<StringContainsExp>());
            Register("stringnotcontains", new ExpressionFactoryHelper<StringNotContainsExp>());
            Register("stringcontainsany", new ExpressionFactoryHelper<StringContainsAnyExp>());
            Register("stringnotcontainsany", new ExpressionFactoryHelper<StringNotContainsAnyExp>());
            Register("str2int", new ExpressionFactoryHelper<Str2IntExp>());
            Register("str2uint", new ExpressionFactoryHelper<Str2UintExp>());
            Register("str2long", new ExpressionFactoryHelper<Str2LongExp>());
            Register("str2ulong", new ExpressionFactoryHelper<Str2UlongExp>());
            Register("str2float", new ExpressionFactoryHelper<Str2FloatExp>());
            Register("str2double", new ExpressionFactoryHelper<Str2DoubleExp>());
            Register("hex2int", new ExpressionFactoryHelper<Hex2IntExp>());
            Register("hex2uint", new ExpressionFactoryHelper<Hex2UintExp>());
            Register("hex2long", new ExpressionFactoryHelper<Hex2LongExp>());
            Register("hex2ulong", new ExpressionFactoryHelper<Hex2UlongExp>());
            Register("datetimestr", new ExpressionFactoryHelper<DatetimeStrExp>());
            Register("longdatestr", new ExpressionFactoryHelper<LongDateStrExp>());
            Register("longtimestr", new ExpressionFactoryHelper<LongTimeStrExp>());
            Register("shortdatestr", new ExpressionFactoryHelper<ShortDateStrExp>());
            Register("shorttimestr", new ExpressionFactoryHelper<ShortTimeStrExp>());
            Register("isnullorempty", new ExpressionFactoryHelper<IsNullOrEmptyExp>());
            Register("array", new ExpressionFactoryHelper<ArrayExp>());
            Register("toarray", new ExpressionFactoryHelper<ToArrayExp>());
            Register("listsize", new ExpressionFactoryHelper<ListSizeExp>());
            Register("list", new ExpressionFactoryHelper<ListExp>());
            Register("listget", new ExpressionFactoryHelper<ListGetExp>());
            Register("listset", new ExpressionFactoryHelper<ListSetExp>());
            Register("listindexof", new ExpressionFactoryHelper<ListIndexOfExp>());
            Register("listadd", new ExpressionFactoryHelper<ListAddExp>());
            Register("listremove", new ExpressionFactoryHelper<ListRemoveExp>());
            Register("listinsert", new ExpressionFactoryHelper<ListInsertExp>());
            Register("listremoveat", new ExpressionFactoryHelper<ListRemoveAtExp>());
            Register("listclear", new ExpressionFactoryHelper<ListClearExp>());
            Register("listsplit", new ExpressionFactoryHelper<ListSplitExp>());
            Register("hashtablesize", new ExpressionFactoryHelper<HashtableSizeExp>());
            Register("hashtable", new ExpressionFactoryHelper<HashtableExp>());
            Register("hashtableget", new ExpressionFactoryHelper<HashtableGetExp>());
            Register("hashtableset", new ExpressionFactoryHelper<HashtableSetExp>());
            Register("hashtableadd", new ExpressionFactoryHelper<HashtableAddExp>());
            Register("hashtableremove", new ExpressionFactoryHelper<HashtableRemoveExp>());
            Register("hashtableclear", new ExpressionFactoryHelper<HashtableClearExp>());
            Register("hashtablekeys", new ExpressionFactoryHelper<HashtableKeysExp>());
            Register("hashtablevalues", new ExpressionFactoryHelper<HashtableValuesExp>());
            Register("listhashtable", new ExpressionFactoryHelper<ListHashtableExp>());
            Register("hashtablesplit", new ExpressionFactoryHelper<HashtableSplitExp>());
            Register("peek", new ExpressionFactoryHelper<PeekExp>());
            Register("stacksize", new ExpressionFactoryHelper<StackSizeExp>());
            Register("stack", new ExpressionFactoryHelper<StackExp>());
            Register("push", new ExpressionFactoryHelper<PushExp>());
            Register("pop", new ExpressionFactoryHelper<PopExp>());
            Register("stackclear", new ExpressionFactoryHelper<StackClearExp>());
            Register("queuesize", new ExpressionFactoryHelper<QueueSizeExp>());
            Register("queue", new ExpressionFactoryHelper<QueueExp>());
            Register("enqueue", new ExpressionFactoryHelper<EnqueueExp>());
            Register("dequeue", new ExpressionFactoryHelper<DequeueExp>());
            Register("queueclear", new ExpressionFactoryHelper<QueueClearExp>());
            Register("setenv", new ExpressionFactoryHelper<SetEnvironmentExp>());
            Register("getenv", new ExpressionFactoryHelper<GetEnvironmentExp>());
            Register("expand", new ExpressionFactoryHelper<ExpandEnvironmentsExp>());
            Register("envs", new ExpressionFactoryHelper<EnvironmentsExp>());
            Register("cd", new ExpressionFactoryHelper<SetCurrentDirectoryExp>());
            Register("pwd", new ExpressionFactoryHelper<GetCurrentDirectoryExp>());
            Register("cmdline", new ExpressionFactoryHelper<CommandLineExp>());
            Register("cmdlineargs", new ExpressionFactoryHelper<CommandLineArgsExp>());
            Register("os", new ExpressionFactoryHelper<OsExp>());
            Register("osplatform", new ExpressionFactoryHelper<OsPlatformExp>());
            Register("osversion", new ExpressionFactoryHelper<OsVersionExp>());
            Register("getfullpath", new ExpressionFactoryHelper<GetFullPathExp>());
            Register("getpathroot", new ExpressionFactoryHelper<GetPathRootExp>());
            Register("getrandomfilename", new ExpressionFactoryHelper<GetRandomFileNameExp>());
            Register("gettempfilename", new ExpressionFactoryHelper<GetTempFileNameExp>());
            Register("gettemppath", new ExpressionFactoryHelper<GetTempPathExp>());
            Register("hasextension", new ExpressionFactoryHelper<HasExtensionExp>());
            Register("ispathrooted", new ExpressionFactoryHelper<IsPathRootedExp>());
            Register("getfilename", new ExpressionFactoryHelper<GetFileNameExp>());
            Register("getfilenamewithoutextension", new ExpressionFactoryHelper<GetFileNameWithoutExtensionExp>());
            Register("getextension", new ExpressionFactoryHelper<GetExtensionExp>());
            Register("getdirectoryname", new ExpressionFactoryHelper<GetDirectoryNameExp>());
            Register("combinepath", new ExpressionFactoryHelper<CombinePathExp>());
            Register("changeextension", new ExpressionFactoryHelper<ChangeExtensionExp>());
            Register("debugbreak", new ExpressionFactoryHelper<DebugBreakExp>());
            Register("debuglog", new ExpressionFactoryHelper<DebugLogExp>());
            Register("debugwarning", new ExpressionFactoryHelper<DebugWarningExp>());
            Register("debugerror", new ExpressionFactoryHelper<DebugErrorExp>());
            Register("callstack", new ExpressionFactoryHelper<CallStackExp>());
            Register("call", new ExpressionFactoryHelper<CallExp>());
            Register("return", new ExpressionFactoryHelper<ReturnExp>());
            Register("redirect", new ExpressionFactoryHelper<RedirectExp>());

            Register("direxist", new ExpressionFactoryHelper<DirectoryExistExp>());
            Register("fileexist", new ExpressionFactoryHelper<FileExistExp>());
            Register("listdirs", new ExpressionFactoryHelper<ListDirectoriesExp>());
            Register("listfiles", new ExpressionFactoryHelper<ListFilesExp>());
            Register("listalldirs", new ExpressionFactoryHelper<ListAllDirectoriesExp>());
            Register("listallfiles", new ExpressionFactoryHelper<ListAllFilesExp>());
            Register("createdir", new ExpressionFactoryHelper<CreateDirectoryExp>());
            Register("copydir", new ExpressionFactoryHelper<CopyDirectoryExp>());
            Register("movedir", new ExpressionFactoryHelper<MoveDirectoryExp>());
            Register("deletedir", new ExpressionFactoryHelper<DeleteDirectoryExp>());
            Register("copyfile", new ExpressionFactoryHelper<CopyFileExp>());
            Register("copyfiles", new ExpressionFactoryHelper<CopyFilesExp>());
            Register("movefile", new ExpressionFactoryHelper<MoveFileExp>());
            Register("deletefile", new ExpressionFactoryHelper<DeleteFileExp>());
            Register("deletefiles", new ExpressionFactoryHelper<DeleteFilesExp>());
            Register("deleteallfiles", new ExpressionFactoryHelper<DeleteAllFilesExp>());
            Register("getfileinfo", new ExpressionFactoryHelper<GetFileInfoExp>());
            Register("getdirinfo", new ExpressionFactoryHelper<GetDirectoryInfoExp>());
            Register("getdriveinfo", new ExpressionFactoryHelper<GetDriveInfoExp>());
            Register("getdrivesinfo", new ExpressionFactoryHelper<GetDrivesInfoExp>());
            Register("readalllines", new ExpressionFactoryHelper<ReadAllLinesExp>());
            Register("writealllines", new ExpressionFactoryHelper<WriteAllLinesExp>());
            Register("readalltext", new ExpressionFactoryHelper<ReadAllTextExp>());
            Register("writealltext", new ExpressionFactoryHelper<WriteAllTextExp>());
            Register("process", new ExpressionFactoryHelper<CommandExp>());
            Register("command", new ExpressionFactoryHelper<CommandExp>());
            Register("kill", new ExpressionFactoryHelper<KillExp>());
            Register("killme", new ExpressionFactoryHelper<KillMeExp>());
            Register("pid", new ExpressionFactoryHelper<GetCurrentProcessIdExp>());
            Register("plist", new ExpressionFactoryHelper<ListProcessesExp>());
            Register("wait", new ExpressionFactoryHelper<WaitExp>());
            Register("waitall", new ExpressionFactoryHelper<WaitAllExp>());
            Register("waitstartinterval", new ExpressionFactoryHelper<WaitStartIntervalExp>());

            Register("displayprogressbar", new ExpressionFactoryHelper<DisplayProgressBarExp>());
            Register("displaycancelableprogressbar", new ExpressionFactoryHelper<DisplayCancelableProgressBarExp>());
            Register("clearprogressbar", new ExpressionFactoryHelper<ClearProgressBarExp>());
            Register("openwithdefaultapp", new ExpressionFactoryHelper<OpenWithDefaultAppExp>());
            Register("openfilepanel", new ExpressionFactoryHelper<OpenFilePanelExp>());
            Register("openfilepanelwithfilters", new ExpressionFactoryHelper<OpenFilePanelWithFiltersExp>());
            Register("openfolderpanel", new ExpressionFactoryHelper<OpenFolderPanelExp>());
            Register("savefilepanel", new ExpressionFactoryHelper<SaveFilePanelExp>());
            Register("savefilepanelinproject", new ExpressionFactoryHelper<SaveFilePanelInProjectExp>());
            Register("savefolderpanel", new ExpressionFactoryHelper<SaveFolderPanelExp>());
            Register("displaydialog", new ExpressionFactoryHelper<DisplayDialogExp>());
            Register("calcmd5", new ExpressionFactoryHelper<CalcMd5Exp>());
#if QINSHI
            Register("debugvalue", new ExpressionFactoryHelper<DebugValueExp>());
            Register("storyvar", new ExpressionFactoryHelper<StoryVarExp>());
            Register("storyvalue", new ExpressionFactoryHelper<StoryValueExp>());
            Register("storycommand", new ExpressionFactoryHelper<StoryCommandExp>());
#endif
        }
        public void Register(string name, IExpressionFactory factory)
        {
            if (!m_ExpressionFactories.ContainsKey(name)) {
                m_ExpressionFactories.Add(name, factory);
            }
            else {
                m_ExpressionFactories[name] = factory;
            }
        }
        public void Cleanup()
        {
            m_Procs.Clear();
            m_Stack.Clear();
            m_NamedGlobalVariables.Clear();
        }
        public void ClearGlobalVariables()
        {
            m_NamedGlobalVariables.Clear();
        }
        public bool TryGetGlobalVariable(string v, out CalculatorValue result)
        {
            return m_NamedGlobalVariables.TryGetValue(v, out result);
        }
        public CalculatorValue GetGlobalVariable(string v)
        {
            CalculatorValue result;
            m_NamedGlobalVariables.TryGetValue(v, out result);
            return result;
        }
        public void SetGlobalVariable(string v, CalculatorValue val)
        {
            var vars = m_NamedGlobalVariables;
            vars[v] = val;
        }
        public bool RemoveGlobalVariable(string v)
        {
            return m_NamedGlobalVariables.Remove(v);
        }
        public void LoadDsl(string dslFile)
        {
            Dsl.DslFile file = new Dsl.DslFile();
            string path = dslFile;
            if (file.Load(path, OnLog)) {
                foreach (Dsl.ISyntaxComponent info in file.DslInfos) {
                    LoadDsl(info);
                }
            }
        }
        public void LoadDsl(Dsl.ISyntaxComponent info)
        {
            if (info.GetId() != "script")
                return;
            var func = info as Dsl.FunctionData;
            string id;
            if (null != func) {
                if (func.IsHighOrder)
                    id = func.LowerOrderFunction.GetParamId(0);
                else
                    return;
            }
            else {
                var statement = info as Dsl.StatementData;
                if (null != statement && statement.GetFunctionNum() == 2) {
                    id = statement.First.GetParamId(0);
                    func = statement.Second;
                    if (func.GetId() == "args" && func.IsHighOrder) {
                        if (func.LowerOrderFunction.GetParamNum() > 0) {
                            List<string> names;
                            if (!m_ProcArgNames.TryGetValue(id, out names)) {
                                names = new List<string>();
                                m_ProcArgNames.Add(id, names);
                            }
                            else {
                                names.Clear();
                            }
                            foreach (var p in func.LowerOrderFunction.Params) {
                                names.Add(p.GetId());
                            }
                        }
                    }
	                else {
	                    return;
	                }
                }
                else {
                    return;
                }
            }
            List<IExpression> list;
            if (!m_Procs.TryGetValue(id, out list)) {
                list = new List<IExpression>();
                m_Procs.Add(id, list);
            }
            else {
                list.Clear();
            }
            foreach (Dsl.ISyntaxComponent comp in func.Params) {
                var exp = Load(comp);
                if (null != exp) {
                    list.Add(exp);
                }
            }
        }
        public void LoadDsl(string proc, Dsl.FunctionData func)
        {
            LoadDsl(proc, null, func);
        }
        public void LoadDsl(string proc, IList<string> argNames, Dsl.FunctionData func)
        {
            if (null != argNames && argNames.Count > 0) {
                List<string> names;
                if (!m_ProcArgNames.TryGetValue(proc, out names)) {
                    names = new List<string>(argNames);
                    m_ProcArgNames.Add(proc, names);
                }
                else {
                    names.Clear();
                    names.AddRange(argNames);
                }
            }
            List<IExpression> list;
            if (!m_Procs.TryGetValue(proc, out list)) {
                list = new List<IExpression>();
                m_Procs.Add(proc, list);
            }
            else {
                list.Clear();
            }
            foreach (Dsl.ISyntaxComponent comp in func.Params) {
                var exp = Load(comp);
                if (null != exp) {
                    list.Add(exp);
                }
            }
        }
        public List<CalculatorValue> NewCalculatorValueList()
        {
            return m_Pool.Alloc();
        }
        public void RecycleCalculatorValueList(List<CalculatorValue> list)
        {
            list.Clear();
            m_Pool.Recycle(list);
        }
        public CalculatorValue Calc(string proc)
        {
            var args = NewCalculatorValueList();
            var r = Calc(proc, args);
            RecycleCalculatorValueList(args);
            return r;
        }
        public CalculatorValue Calc(string proc, CalculatorValue arg1)
        {
            var args = NewCalculatorValueList();
            args.Add(arg1);
            var r = Calc(proc, args);
            RecycleCalculatorValueList(args);
            return r;
        }
        public CalculatorValue Calc(string proc, CalculatorValue arg1, CalculatorValue arg2)
        {
            var args = NewCalculatorValueList();
            args.Add(arg1);
            args.Add(arg2);
            var r = Calc(proc, args);
            RecycleCalculatorValueList(args);
            return r;
        }
        public CalculatorValue Calc(string proc, CalculatorValue arg1, CalculatorValue arg2, CalculatorValue arg3)
        {
            var args = NewCalculatorValueList();
            args.Add(arg1);
            args.Add(arg2);
            args.Add(arg3);
            var r = Calc(proc, args);
            RecycleCalculatorValueList(args);
            return r;
        }
        public CalculatorValue Calc(string proc, List<CalculatorValue> args)
        {
            CalculatorValue ret = 0;
            List<IExpression> exps;
            if (m_Procs.TryGetValue(proc, out exps)) {
                var si = new StackInfo();
                si.Args.AddRange(args);
                m_Stack.Push(si);
                try {
                    List<string> names;
                    if (m_ProcArgNames.TryGetValue(proc, out names)) {
                        for (int i = 0; i < names.Count; ++i) {
                            if (i < args.Count)
                                SetVariable(names[i], args[i]);
                            else
                                SetVariable(names[i], CalculatorValue.NullObject);
                        }
                    }
                    for (int i = 0; i < exps.Count; ++i) {
                        var exp = exps[i];
                        try {
                            ret = exp.Calc();
                            if (m_RunState == RunStateEnum.Return) {
                                m_RunState = RunStateEnum.Normal;
                                break;
                            }
                            else if (m_RunState == RunStateEnum.Redirect) {
                                break;
                            }
                        }
                        catch (DirectoryNotFoundException ex5) {
                            Log("calc:[{0}] exception:{1}\n{2}", exp.ToString(), ex5.Message, ex5.StackTrace);
                            OutputInnerException(ex5);
                        }
                        catch (FileNotFoundException ex4) {
                            Log("calc:[{0}] exception:{1}\n{2}", exp.ToString(), ex4.Message, ex4.StackTrace);
                            OutputInnerException(ex4);
                        }
                        catch (IOException ex3) {
                            Log("calc:[{0}] exception:{1}\n{2}", exp.ToString(), ex3.Message, ex3.StackTrace);
                            OutputInnerException(ex3);
                            ret = -1;
                        }
                        catch (UnauthorizedAccessException ex2) {
                            Log("calc:[{0}] exception:{1}\n{2}", exp.ToString(), ex2.Message, ex2.StackTrace);
                            OutputInnerException(ex2);
                            ret = -1;
                        }
                        catch (NotSupportedException ex1) {
                            Log("calc:[{0}] exception:{1}\n{2}", exp.ToString(), ex1.Message, ex1.StackTrace);
                            OutputInnerException(ex1);
                            ret = -1;
                        }
                        catch (Exception ex) {
                            Log("calc:[{0}] exception:{1}\n{2}", exp.ToString(), ex.Message, ex.StackTrace);
                            OutputInnerException(ex);
                            ret = -1;
                            break;
                        }
                    }
                }
                finally {
                    m_Stack.Pop();
                }
            }
            return ret;
        }
        public RunStateEnum RunState
        {
            get { return m_RunState; }
            internal set { m_RunState = value; }
        }
        public void Log(string fmt, params object[] args)
        {
            if (null != OnLog) {
                OnLog(string.Format(fmt, args));
            }
        }
        public void Log(object arg)
        {
            if (null != OnLog) {
                OnLog(string.Format("{0}", arg));
            }
        }
        public IList<CalculatorValue> Arguments
        {
            get {
                var stackInfo = m_Stack.Peek();
                return stackInfo.Args;
            }
        }
        public bool TryGetVariable(int v, out CalculatorValue result)
        {
            return Variables.TryGetValue(v, out result);
        }
        public CalculatorValue GetVariable(int v)
        {
            CalculatorValue result;
            Variables.TryGetValue(v, out result);
            return result;
        }
        public void SetVariable(int v, CalculatorValue val)
        {
            Variables[v] = val;
        }
        public bool RemoveVariable(int v)
        {
            return Variables.Remove(v);
        }
        public bool TryGetVariable(string v, out CalculatorValue result)
        {
            bool ret = false;
            if (v.Length > 0) {
                if (v[0] == '@') {
                    ret = TryGetGlobalVariable(v, out result);
                }
                else if (v[0] == '$') {
                    ret = NamedVariables.TryGetValue(v, out result);
                }
                else {
                    ret = TryGetGlobalVariable(v, out result);
                }
            }
            else {
                result = CalculatorValue.NullObject;
            }
            return ret;
        }
        public CalculatorValue GetVariable(string v)
        {
            CalculatorValue result = CalculatorValue.NullObject;
            if (v.Length > 0) {
                if (v[0] == '@') {
                    result = GetGlobalVariable(v);
                }
                else if (v[0] == '$') {
                    NamedVariables.TryGetValue(v, out result);
                }
                else {
                    result = GetGlobalVariable(v);
                }
            }
            return result;
        }
        public void SetVariable(string v, CalculatorValue val)
        {
            if (v.Length > 0) {
                if (v[0] == '@') {
                    SetGlobalVariable(v, val);
                }
                else if (v[0] == '$') {
                    NamedVariables[v] = val;
                }
                else {
                    SetGlobalVariable(v, val);
                }
            }
        }
        public bool RemoveVariable(string v)
        {
            bool ret = false;
            if (v.Length > 0) {
                if (v[0] == '@') {
                    ret = RemoveGlobalVariable(v);
                }
                else if (v[0] == '$') {
                    ret = NamedVariables.Remove(v);
                }
                else {
                    ret = RemoveGlobalVariable(v);
                }
            }
            return ret;
        }
        public IExpression Load(Dsl.ISyntaxComponent comp)
        {
            Dsl.ValueData valueData = comp as Dsl.ValueData;
            Dsl.FunctionData funcData = null;
            if (null != valueData) {
                int idType = valueData.GetIdType();
                if (idType == Dsl.ValueData.ID_TOKEN) {
                    string id = valueData.GetId();
                    var p = Create(id);
                    if (null != p) {
                        //将无参数名字转换为无参函数调用
                        Dsl.FunctionData fd = new Dsl.FunctionData();
                        fd.Name.CopyFrom(valueData);
                        fd.SetParamClass((int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS);
                        if (!p.Load(fd, this)) {
                            //error
                            Log("DslCalculator error, {0} line {1}", comp.ToScriptString(false), comp.GetLine());
                        }
                        return p;
                    }
                    else if(id == "true" || id == "false") {
                        ConstGet constExp = new ConstGet();
                        constExp.Load(comp, this);
                        return constExp;
                    }
                    else {
                        NamedVarGet varExp = new NamedVarGet();
                        varExp.Load(comp, this);
                        return varExp;
                    }
                }
                else {
                    ConstGet constExp = new ConstGet();
                    constExp.Load(comp, this);
                    return constExp;
                }
            }
            else {
                funcData = comp as Dsl.FunctionData;
                if (null != funcData) {
                    if (funcData.HaveParam()) {
                        var callData = funcData;
                        if (!callData.HaveId() && !callData.IsHighOrder && (callData.GetParamClass() == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS || callData.GetParamClass() == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET)) {
                            switch (callData.GetParamClass()) {
                                case (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS:
                                    int num = callData.GetParamNum();
                                    if (num == 1) {
                                        Dsl.ISyntaxComponent param = callData.GetParam(0);
                                        return Load(param);
                                    }
                                    else {
                                        ParenthesisExp exp = new ParenthesisExp();
                                        exp.Load(comp, this);
                                        return exp;
                                    }
                                case (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET: {
                                        ArrayExp exp = new ArrayExp();
                                        exp.Load(comp, this);
                                        return exp;
                                    }
                                default:
                                    return null;
                            }
                        }
                        else if (!callData.HaveParam()) {
                            //退化
                            valueData = callData.Name;
                            return Load(valueData);
                        }
                        else {
                            int paramClass = callData.GetParamClass();
                            string op = callData.GetId();
                            if (op == "=") {//赋值
                                Dsl.FunctionData innerCall = callData.GetParam(0) as Dsl.FunctionData;
                                if (null != innerCall) {
                                    //obj.property = val -> dotnetset(obj, property, val)
                                    int innerParamClass = innerCall.GetParamClass();
                                    if (innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD ||
                                      innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET ||
                                      innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACE ||
                                      innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACKET ||
                                      innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_PARENTHESIS) {
                                        Dsl.FunctionData newCall = new Dsl.FunctionData();
                                        newCall.Name = new Dsl.ValueData("dotnetset", Dsl.ValueData.ID_TOKEN);
                                        newCall.SetParamClass((int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS);
                                        if (innerCall.IsHighOrder) {
                                            newCall.Params.Add(innerCall.LowerOrderFunction);
                                            newCall.Params.Add(ConvertMember(innerCall.GetParam(0), innerCall.GetParamClass()));
                                            newCall.Params.Add(callData.GetParam(1));
                                        }
                                        else {
                                            newCall.Params.Add(innerCall.Name);
                                            newCall.Params.Add(ConvertMember(innerCall.GetParam(0), innerCall.GetParamClass()));
                                            newCall.Params.Add(callData.GetParam(1));
                                        }

                                        var setExp = new DotnetSetExp();
                                        setExp.Load(newCall, this);
                                        return setExp;
                                    }
                                }
                                IExpression exp = null;
                                string name = callData.GetParamId(0);
                                if (name == "var") {
                                    exp = new VarSet();
                                }
                                else {
                                    exp = new NamedVarSet();
                                }
                                if (null != exp) {
                                    exp.Load(comp, this);
                                }
                                else {
                                    //error
                                    Log("DslCalculator error, {0} line {1}", callData.ToScriptString(false), callData.GetLine());
                                }
                                return exp;
                            }
                            else {
                                if (callData.IsHighOrder) {
                                    Dsl.FunctionData innerCall = callData.LowerOrderFunction;
                                    int innerParamClass = innerCall.GetParamClass();
                                    if (paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS && (
                                        innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD ||
                                        innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET ||
                                        innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACE ||
                                        innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACKET ||
                                        innerParamClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_PARENTHESIS)) {
                                        //obj.member(a,b,...) or obj[member](a,b,...) or obj.(member)(a,b,...) or obj.[member](a,b,...) or obj.{member}(a,b,...) -> dotnetcall(obj,member,a,b,...)
                                        string apiName;
                                        string member = innerCall.GetParamId(0);
                                        if (member == "orderby" || member == "orderbydesc" || member == "where" || member == "top") {
                                            apiName = "linq";
                                        }
                                        else {
                                            apiName = "dotnetcall";
                                        }
                                        Dsl.FunctionData newCall = new Dsl.FunctionData();
                                        newCall.Name = new Dsl.ValueData(apiName, Dsl.ValueData.ID_TOKEN);
                                        newCall.SetParamClass((int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS);
                                        if (innerCall.IsHighOrder) {
                                            newCall.Params.Add(innerCall.LowerOrderFunction);
                                            newCall.Params.Add(ConvertMember(innerCall.GetParam(0), innerCall.GetParamClass()));
                                            for (int i = 0; i < callData.GetParamNum(); ++i) {
                                                Dsl.ISyntaxComponent p = callData.Params[i];
                                                newCall.Params.Add(p);
                                            }
                                        }
                                        else {
                                            newCall.Params.Add(innerCall.Name);
                                            newCall.Params.Add(ConvertMember(innerCall.GetParam(0), innerCall.GetParamClass()));
                                            for (int i = 0; i < callData.GetParamNum(); ++i) {
                                                Dsl.ISyntaxComponent p = callData.Params[i];
                                                newCall.Params.Add(p);
                                            }
                                        }

                                        if (apiName == "dotnetcall") {
                                            var callExp = new DotnetCallExp();
                                            callExp.Load(newCall, this);
                                            return callExp;
                                        }
                                        else {
                                            var callExp = new LinqExp();
                                            callExp.Load(newCall, this);
                                            return callExp;
                                        }
                                    }
                                }
                                if (paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD ||
                                  paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET ||
                                  paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACE ||
                                  paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACKET ||
                                  paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_PARENTHESIS) {
                                    //obj.property or obj[property] or obj.(property) or obj.[property] or obj.{property} -> dotnetget(obj,property)
                                    Dsl.FunctionData newCall = new Dsl.FunctionData();
                                    newCall.Name = new Dsl.ValueData("dotnetget", Dsl.ValueData.ID_TOKEN);
                                    newCall.SetParamClass((int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS);
                                    if (callData.IsHighOrder) {
                                        newCall.Params.Add(callData.LowerOrderFunction);
                                        newCall.Params.Add(ConvertMember(callData.GetParam(0), callData.GetParamClass()));
                                    }
                                    else {
                                        newCall.Params.Add(callData.Name);
                                        newCall.Params.Add(ConvertMember(callData.GetParam(0), callData.GetParamClass()));
                                    }

                                    var getExp = new DotnetGetExp();
                                    getExp.Load(newCall, this);
                                    return getExp;
                                }
                            }
                        }
                    }
                    else {
                        if (funcData.HaveStatement()) {
                            if (!funcData.HaveId() && !funcData.IsHighOrder) {
                                HashtableExp exp = new HashtableExp();
                                exp.Load(comp, this);
                                return exp;
                            }
                        }
                        else if (!funcData.HaveExternScript()) {
                            //退化
                            valueData = funcData.Name;
                            return Load(valueData);
                        }
                    }
                }
            }
            IExpression ret = null;
            string expId = comp.GetId();
            if (null != funcData && !funcData.IsHighOrder && m_Procs.ContainsKey(expId)) {
                ret = new FunctionCall();
            }
            else {
                ret = Create(comp.GetId());
            }
            if (null != ret) {
                Dsl.StatementData stData = comp as Dsl.StatementData;
                if (null != stData) {
                    Dsl.FunctionData first = stData.First;
                    if(first.HaveId() && !first.HaveParamOrStatement()) {
                        //将命令行语法转换为函数调用语法
                        Dsl.FunctionData fd = new Dsl.FunctionData();
                        fd.CopyFrom(first);
                        for(int argi = 1; argi < stData.GetFunctionNum(); ++argi) {
                            var pfd = stData.GetFunction(argi);
                            if (pfd.HaveId() && !pfd.HaveParamOrStatement()) {
                                fd.AddParam(pfd.Name);
                            }
                            else {
                                fd.AddParam(pfd);
                            }
                        }
                        if (!ret.Load(fd, this)) {
                            //error
                            Log("DslCalculator error, {0} line {1}", comp.ToScriptString(false), comp.GetLine());
                        }
                        return ret;
                    }
                }
                if (!ret.Load(comp, this)) {
                    //error
                    Log("DslCalculator error, {0} line {1}", comp.ToScriptString(false), comp.GetLine());
                }
            }
            else {
                //error
                Log("DslCalculator error, {0} line {1}", comp.ToScriptString(false), comp.GetLine());
            }
            return ret;
        }
        private Dsl.ISyntaxComponent ConvertMember(Dsl.ISyntaxComponent p, int paramClass)
        {
            var pvd = p as Dsl.ValueData;
            if (null != pvd && pvd.IsId() && (paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD
                || paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_POINTER
                || paramClass == (int)Dsl.FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD)) {
                pvd.SetType(Dsl.ValueData.STRING_TOKEN);
                return pvd;
            }
            else {
                return p;
            }
        }

        private IExpression Create(string name)
        {
            IExpression ret = null;
            IExpressionFactory factory;
            if (m_ExpressionFactories.TryGetValue(name, out factory)) {
                ret = factory.Create();
            }
            return ret;
        }
        private void OutputInnerException(Exception ex)
        {
            while (null != ex.InnerException) {
                ex = ex.InnerException;
                Log("\t=> exception:{0} stack:{1}", ex.Message, ex.StackTrace);
            }
        }

        private Dictionary<int, CalculatorValue> Variables
        {
            get {
                var stackInfo = m_Stack.Peek();
                return stackInfo.Vars;
            }
        }
        private Dictionary<string, CalculatorValue> NamedVariables
        {
            get {
                var stackInfo = m_Stack.Peek();
                return stackInfo.NamedVars;
            }
        }

        private class StackInfo
        {
            internal List<CalculatorValue> Args = new List<CalculatorValue>();
            internal Dictionary<int, CalculatorValue> Vars = new Dictionary<int, CalculatorValue>();
            internal Dictionary<string, CalculatorValue> NamedVars = new Dictionary<string, CalculatorValue>();
        }

        private RunStateEnum m_RunState = RunStateEnum.Normal;
        private Dictionary<string, List<string>> m_ProcArgNames = new Dictionary<string, List<string>>();
        private Dictionary<string, List<IExpression>> m_Procs = new Dictionary<string, List<IExpression>>();
        private Stack<StackInfo> m_Stack = new Stack<StackInfo>();
        private Dictionary<string, CalculatorValue> m_NamedGlobalVariables = new Dictionary<string, CalculatorValue>();
        private Dictionary<string, IExpressionFactory> m_ExpressionFactories = new Dictionary<string, IExpressionFactory>();
        private CalculatorValueListPool m_Pool = new CalculatorValueListPool(16);
        
		internal static int CheckStartInterval
        {
            get { return s_CheckStartInterval; }
            set { s_CheckStartInterval = value; }
        }
        internal static List<Task<int>> Tasks
        {
            get { return s_Tasks; }
        }
        internal static int NewProcess(bool noWait, string fileName, string args, ProcessStartOption option, Stream istream, Stream ostream, IList<string> input, StringBuilder output, StringBuilder error, bool redirectToConsole, Encoding encoding)
        {
            if (noWait) {
                var task = Task.Run<int>(() => NewProcessTask(fileName, args, option, istream, ostream, input, output, error, redirectToConsole, encoding));
                s_Tasks.Add(task);
                while (task.Status == TaskStatus.Created || task.Status == TaskStatus.WaitingForActivation || task.Status == TaskStatus.WaitingToRun) {
                    Debug.LogFormat("wait {0}[{1}] start", Path.GetFileName(fileName), task.Status);
                    task.Wait(s_CheckStartInterval);
                }
                return 0;
            }
            else {
                return NewProcessTask(fileName, args, option, istream, ostream, input, output, error, redirectToConsole, encoding);
            }
        }
        private static int NewProcessTask(string fileName, string args, ProcessStartOption option, Stream istream, Stream ostream, IList<string> input, StringBuilder output, StringBuilder error, bool redirectToConsole, Encoding encoding)
        {
            //考虑到跨平台兼容性，不使用特定进程环境变量
            try {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                var psi = p.StartInfo;
                psi.FileName = fileName;
                psi.Arguments = args;
                psi.UseShellExecute = option.UseShellExecute;
                if (null != option.Verb) {
                    psi.Verb = option.Verb;
                }
                if (null != option.Domain) {
                    psi.Domain = option.Domain;
                }
                if (null != option.UserName) {
                    psi.UserName = option.UserName;
                }
                if (null != option.Password) {
                    unsafe {
                        fixed (char* pchar = option.Password.ToCharArray()) {
                            psi.Password = new System.Security.SecureString(pchar, option.Password.Length);
                        }
                    }
                }
                if (null != option.PasswordInClearText) {
                    psi.PasswordInClearText = option.PasswordInClearText;
                }
                psi.LoadUserProfile = option.LoadUserProfile;
                psi.WindowStyle = option.WindowStyle;
                psi.CreateNoWindow = !option.NewWindow;
                psi.ErrorDialog = option.ErrorDialog;
                psi.WorkingDirectory = option.WorkingDirectory;

                if (null != istream || null != input) {
                    psi.RedirectStandardInput = true;
                }
                if (null != ostream || null != output || redirectToConsole) {
                    psi.RedirectStandardOutput = true;
                    psi.StandardOutputEncoding = encoding;
                    var tempStringBuilder = new StringBuilder();
                    p.OutputDataReceived += (sender, e) => OnOutputDataReceived(sender, e, ostream, output, redirectToConsole, encoding, tempStringBuilder);
                }
                if (null != error || redirectToConsole) {
                    psi.RedirectStandardError = true;
                    psi.StandardErrorEncoding = encoding;
                    var tempStringBuilder = new StringBuilder();
                    p.ErrorDataReceived += (sender, e) => OnErrorDataReceived(sender, e, ostream, error, redirectToConsole, encoding, tempStringBuilder);
                }
                if (p.Start()) {
                    if (psi.RedirectStandardInput) {
                        if (null != istream) {
                            istream.Seek(0, SeekOrigin.Begin);
                            using (var sr = new StreamReader(istream, encoding, true, 1024, true)) {
                                string line;
                                while ((line = sr.ReadLine()) != null) {
                                    p.StandardInput.WriteLine(line);
                                    p.StandardInput.Flush();
                                }
                            }
                            p.StandardInput.Close();
                        }
                        else if (null != input) {
                            foreach (var line in input) {
                                p.StandardInput.WriteLine(line);
                                p.StandardInput.Flush();
                            }
                            p.StandardInput.Close();
                        }
                    }
                    if (null != ostream) {
                        ostream.Seek(0, SeekOrigin.Begin);
                        ostream.SetLength(0);
                    }
                    if (psi.RedirectStandardOutput)
                        p.BeginOutputReadLine();
                    if (psi.RedirectStandardError)
                        p.BeginErrorReadLine();
                    p.WaitForExit();
                    if (psi.RedirectStandardOutput) {
                        p.CancelOutputRead();
                    }
                    if (psi.RedirectStandardError) {
                        p.CancelErrorRead();
                    }
                    int r = p.ExitCode;
                    p.Close();
                    return r;
                }
                else {
                    Debug.LogFormat("process({0} {1}) failed.", fileName, args);
                    return -1;
                }
            }
            catch (Exception ex) {
                Debug.LogErrorFormat("process({0} {1}) exception:{2} stack:{3}", fileName, args, ex.Message, ex.StackTrace);
                while (null != ex.InnerException) {
                    ex = ex.InnerException;
                    Debug.LogErrorFormat("\t=> exception:{0} stack:{1}", ex.Message, ex.StackTrace);
                }
                return -1;
            }
        }

        private static void OnOutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e, Stream ostream, StringBuilder output, bool redirectToConsole, Encoding encoding, StringBuilder temp)
        {
            var p = sender as System.Diagnostics.Process;
            if (p.StartInfo.RedirectStandardOutput) {
                temp.Length = 0;
                temp.AppendLine(e.Data);
                var txt = temp.ToString();
                if (null != ostream) {
                    var bytes = encoding.GetBytes(txt);
                    ostream.Write(bytes, 0, bytes.Length);
                }
                if (null != output) {
                    output.Append(txt);
                }
                if (redirectToConsole) {
                    Debug.LogFormat("{0}", txt);
                }
            }
        }

        private static void OnErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e, Stream ostream, StringBuilder error, bool redirectToConsole, Encoding encoding, StringBuilder temp)
        {
            var p = sender as System.Diagnostics.Process;
            if (p.StartInfo.RedirectStandardError) {
                temp.Length = 0;
                temp.AppendLine(e.Data);
                var txt = temp.ToString();
                if (null != ostream) {
                    var bytes = encoding.GetBytes(txt);
                    ostream.Write(bytes, 0, bytes.Length);
                }
                if (null != error) {
                    error.Append(txt);
                }
                if (redirectToConsole) {
                    Debug.LogFormat("{0}", txt);
                }
            }
        }

        internal class ProcessStartOption
        {
            internal bool UseShellExecute = false;
            internal string Verb = null;
            internal string Domain = null;
            internal string UserName = null;
            internal string Password = null;
            internal string PasswordInClearText = null;
            internal bool LoadUserProfile = false;
            internal System.Diagnostics.ProcessWindowStyle WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            internal bool NewWindow = false;
            internal bool ErrorDialog = false;
            internal string WorkingDirectory = Environment.CurrentDirectory;
        }
        private static List<Task<int>> s_Tasks = new List<Task<int>>();
        private static int s_CheckStartInterval = 500;
    }
}
#endregion
#endif