﻿using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace Transactions1
{
    public static class NeoExtensionMethods
    {
        private const int BUFSIZE = 200;
        static byte[] dobBytes = new byte[BUFSIZE];

        public static byte[] AsDisplayOrderBytes(this byte[] bytes)
        {
            //byte[] dobBytes = new byte[bytes.Length];
            //byte[] dobBytes = (byte[])bytes.Clone();
            //byte[] dobBytes = new byte[BUFSIZE];

            int len = bytes.Length;
            //Console.WriteLine("len " + len.ToString());
            Runtime.Notify("len ", len);
            if (len > BUFSIZE) len = BUFSIZE;
            //Console.WriteLine("len " + len.ToString());
            Runtime.Notify("len ", len);
            int dobOffset = len - 4;
            for (int bytesOffset = 0; bytesOffset < len; bytesOffset += 4)
            {
                //Console.WriteLine("bytesOffset " + bytesOffset.ToString());
                //Console.WriteLine("dobOffset " + dobOffset.ToString());
                Runtime.Notify("bytesOffset ", bytesOffset);
                Runtime.Notify("dobOffset ", dobOffset);
                dobBytes[dobOffset + 3] = bytes[bytesOffset + 0];
                dobBytes[dobOffset + 2] = bytes[bytesOffset + 1];
                dobBytes[dobOffset + 1] = bytes[bytesOffset + 2];
                dobBytes[dobOffset + 0] = bytes[bytesOffset + 3];
                dobOffset -= 4;
            }
            return dobBytes;
        }
    }

    public class Transactions1 : SmartContract
    {
        public static readonly byte[] hexStringToBytes = Neo.SmartContract.Framework.Helper.HexToBytes("0x0123456789abcdef");

        public static readonly byte[] byteArray8a = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
        public static readonly byte[] byteArray16a = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        public static readonly byte[] byteArray8b = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
        public static readonly byte[] byteArray16b = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };

        public static void Main()
        {
            Runtime.Log("Main:Runtime.Log() byte order tests...");;
            Runtime.Notify("Main: 0x0123456789abcdef", 0x0123456789abcdef);
            //Runtime.Notify("Main: 0x0123456789abcdef", 0x0123456789abcdef.ToString());
            Runtime.Notify("Main: (BigInteger)0x0123456789abcdef", (BigInteger)0x0123456789abcdef);
            //Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).ToString()", ((BigInteger)0x0123456789abcdef).ToString());
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).ToByteArray()", ((BigInteger)0x0123456789abcdef).ToByteArray());
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).AsByteArray()", ((BigInteger)0x0123456789abcdef).AsByteArray());
            //Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).ToByteArray().ToString()", ((BigInteger)0x0123456789abcdef).ToByteArray().ToString());
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).AsByteArray().AsString()", ((BigInteger)0x0123456789abcdef).AsByteArray().AsString());
            Runtime.Notify("Main: ((BigInteger)0x0123456789abcdef).AsByteArray().AsDisplayOrderBytes()", ((BigInteger)0x0123456789abcdef).AsByteArray().AsDisplayOrderBytes());
            Runtime.Notify("Main: Helper.HexToBytes(\"0x0123456789abcdef\")", hexStringToBytes);
            Runtime.Notify("Main: Helper.AsString(byteArray8a)", Neo.SmartContract.Framework.Helper.AsString(byteArray8a));
            Runtime.Notify("Main: byteArray8a", byteArray8a);
            Runtime.Notify("Main: byteArray16a", byteArray16a);
            Runtime.Notify("Main: byteArray8b", byteArray8b);
            Runtime.Notify("Main: byteArray16b", byteArray16b);
            Runtime.Notify("Main: Helper.AsString(byteArray8a)", Neo.SmartContract.Framework.Helper.AsString(byteArray8a));
            Runtime.Notify("Main: Helper.AsString(byteArray16a)", Neo.SmartContract.Framework.Helper.AsString(byteArray16a));
            Runtime.Notify("Main: Helper.AsString(byteArray8b)", Neo.SmartContract.Framework.Helper.AsString(byteArray8b));
            Runtime.Notify("Main: Helper.AsString(byteArray16b)", Neo.SmartContract.Framework.Helper.AsString(byteArray16b));

            //Runtime.Notify("Main: 0x0123456789abcdef.ToString()", 0x0123456789abcdef.ToString());
            //Runtime.Notify("Main: Helper.AsByteArray(0x0123456789abcdef.ToString())", Neo.SmartContract.Framework.Helper.AsByteArray(0x0123456789abcdef.ToString()));

            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;

            //Runtime.Log("tx.GetAttributes...");
            //TransactionAttribute[] attrs = tx.GetAttributes();
            //foreach (TransactionAttribute a in attrs)
            //{
            //    Runtime.Notify("GetAttributes: Data,Usage", a.Data, a.Usage);
            //}

            Runtime.Log("tx.GetInputs...");
            TransactionInput[] inputs = tx.GetInputs();
            Runtime.Notify("GetInputs:" , inputs.Length);
            foreach (TransactionInput i in inputs)
            {
                Runtime.Notify("GetInputs: PrevHash,PrevIndex", i.PrevHash, i.PrevIndex);
            }

            Runtime.Log("tx.GetOutputs...");
            TransactionOutput[] outputs = tx.GetOutputs();
            Runtime.Notify("GetOutputs:", outputs.Length);
            foreach (TransactionOutput o in outputs)
            {
                Runtime.Notify("GetOutputs: AssetId,ScriptHash", o.AssetId, o.ScriptHash);
                Runtime.Notify("GetOutputs: Value$NEO", o.Value);
            }

            Runtime.Log("tx.GetReferences...");
            TransactionOutput[] refs = tx.GetReferences();
            Runtime.Notify("GetReferences:", refs.Length);
            foreach (TransactionOutput r in refs)
            {
                Runtime.Notify("GetReferences: AssetId,ScriptHash", r.AssetId, r.ScriptHash);
                Runtime.Notify("GetReferences: Value$NEO", r.Value);
            }

            //Runtime.Log("tx.GetUnspentCoins...");
            //TransactionOutput[] unspents = tx.GetUnspentCoins();
            //Runtime.Notify("GetUnspentCoins:", unspents.Length);
            //foreach (TransactionOutput c in unspents)
            //{
            //    Runtime.Notify("GetUnspentCoins: AssetId,ScriptHash,Value:", c.AssetId, c.ScriptHash, c.Value);
            //}

            Runtime.Log("GetSenderScriptHash...");
            byte[] senderSH = GetSenderScriptHash();
            if (senderSH.Length == 0)
            {
                Runtime.Notify("GetSenderScriptHash: senderSH", 0xbadbad);
            }
            else
            {
                Runtime.Notify("GetSenderScriptHash: senderSH", senderSH);
            }
        }

        private static readonly byte[] NEO_ASSET_ID = { 155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229, 254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };
        private static byte[] GetSenderScriptHash()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransactionOutput[] refs = tx.GetReferences();
            Runtime.Notify("GetSenderScriptHash:", refs.Length);
            Runtime.Notify("GetSenderScriptHash: NEO_ASSET_ID", NEO_ASSET_ID);
            foreach (TransactionOutput r in refs)
            {
                Runtime.Notify("GetSenderScriptHash: AssetId", r.AssetId);
                if (r.AssetId == NEO_ASSET_ID)
                {
                    Runtime.Notify("GetSenderScriptHash: AssetId,ScriptHash", r.AssetId, r.ScriptHash);
                    return r.ScriptHash;
                }
            }
            return new byte[] { };
        }
    }
}
