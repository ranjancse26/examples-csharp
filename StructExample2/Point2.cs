﻿using Neo.SmartContract.Framework;
using System;
using System.Numerics;

namespace StructExample
{
    public class Point2
    {
        public int X;
        public int Y;

        public Point2()
        {
        }

        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static void Put(byte[] key, Point2 p)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#X")), p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Y")), p.Y);
        }

        public static void Put(string key, Point2 p)
        {
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#X", p.X);
            Neo.SmartContract.Framework.Services.Neo.Storage.Put(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Y", p.Y);
        }

        public static Point2 Get(byte[] key)
        {
            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#X"))).AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, Helper.Concat(key, Helper.AsByteArray("/#Y"))).AsBigInteger();
            Point2 p = new Point2();
            p.X = x;
            p.Y = y;
            return p;
        }

        public static Point2 Get(string key)
        {
            int x = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#X").AsBigInteger();
            int y = (int)Neo.SmartContract.Framework.Services.Neo.Storage.Get(Neo.SmartContract.Framework.Services.Neo.Storage.CurrentContext, key + "/#Y").AsBigInteger();
            Point2 p = new Point2();
            p.X = x;
            p.Y = y;
            return p;
        }
    }
}
