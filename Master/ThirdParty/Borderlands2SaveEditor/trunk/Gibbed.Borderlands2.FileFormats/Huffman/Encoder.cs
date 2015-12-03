/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

// is this code terrible?
// yup!

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gibbed.Borderlands2.FileFormats.Huffman
{
    public class Encoder
    {
        private Node _Root;
        private readonly Dictionary<byte, BitArray> _Codes = new Dictionary<byte, BitArray>();

        public int TotalBits { get; private set; }

        public void Build(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            this._Root = null;
            var frequencies = new Dictionary<byte, int>();
            this._Codes.Clear();

            foreach (var b in bytes)
            {
                int frequency;
                if (frequencies.TryGetValue(b, out frequency) == false)
                {
                    frequency = 0;
                }
                frequencies[b] = frequency + 1;
            }

            var nodes = frequencies.Select(
                symbol => new Node()
                {
                    Symbol = symbol.Key,
                    Frequency = symbol.Value,
                }).ToList();

            while (nodes.Count > 1)
            {
                var orderedNodes = nodes
                    .OrderBy(n => n.Frequency).ToList();

                if (orderedNodes.Count >= 2)
                {
                    var taken = orderedNodes.Take(2).ToArray();
                    var first = taken[0];
                    var second = taken[1];

                    var parent = new Node()
                    {
                        Symbol = 0,
                        Frequency = first.Frequency + second.Frequency,
                        Left = first,
                        Right = second,
                    };

                    nodes.Remove(first);
                    nodes.Remove(second);
                    nodes.Add(parent);
                }

                this._Root = nodes.FirstOrDefault();
            }

            foreach (var frequency in frequencies)
            {
                var bits = Traverse(this._Root, frequency.Key, new List<bool>());
                if (bits == null)
                {
                    throw new InvalidOperationException(string.Format(
                        "could not traverse '{0}'", frequency.Key));
                }
                this._Codes.Add(frequency.Key, new BitArray(bits.ToArray()));
            }

            this.TotalBits = GetTotalBits(this._Root);
        }

        private static int GetTotalBits(Node root)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(root);

            int totalBits = 0;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.Left == null && node.Right == null)
                {
                    totalBits += 1 + 8; // tree building
                    continue;
                }

                totalBits += 1; // tree building
                totalBits += node.Frequency;

                if (node.Left != null &&
                    node.Left.Left != null &&
                    node.Left.Right != null)
                {
                    queue.Enqueue(node.Left);
                }

                if (node.Right != null &&
                    node.Right.Left != null &&
                    node.Right.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }

            return totalBits + 5000;
        }

        private static List<bool> Traverse(Node node, byte symbol, List<bool> data)
        {
            if (node.Left == null &&
                node.Right == null)
            {
                return symbol == node.Symbol ? data : null;
            }

            if (node.Left != null)
            {
                var path = new List<bool>();
                path.AddRange(data);
                path.Add(false);

                var left = Traverse(node.Left, symbol, path);
                if (left != null)
                {
                    return left;
                }
            }

            if (node.Right != null)
            {
                var path = new List<bool>();
                path.AddRange(data);
                path.Add(true);

                var right = Traverse(node.Right, symbol, path);
                if (right != null)
                {
                    return right;
                }
            }

            return null;
        }

        private static int EncodeByte(BitArray data, byte value, int offset)
        {
            for (int i = 7; i >= 0; i--)
            {
                data[offset++] = (value & 1 << i) != 0;
            }
            return offset;
        }

        private static int EncodeNode(BitArray bits, Node node, int offset)
        {
            if (node.Left == null &&
                node.Right == null)
            {
                bits[offset++] = true;
                offset = EncodeByte(bits, node.Symbol, offset);
            }
            else
            {
                bits[offset++] = false;
                offset = EncodeNode(bits, node.Left, offset);
                offset = EncodeNode(bits, node.Right, offset);
            }
            return offset;
        }

        private int Encode(byte symbol, BitArray bits, int offset)
        {
            var code = this._Codes[symbol];
            for (int i = 0; i < code.Length; i++)
            {
                bits[offset + i] = code[i];
            }
            return code.Length;
        }

        private int Encode(IEnumerable<byte> bytes, BitArray bits, int offset)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            offset = EncodeNode(bits, this._Root, offset);

            var bitCount = 0;
            foreach (var b in bytes)
            {
                if (this._Codes.ContainsKey(b) == false)
                {
                    throw new ArgumentException(string.Format(
                        "could not lookup '{0}'", b),
                                                "bytes");
                }

                bitCount += this.Encode(b, bits, offset + bitCount);
            }

            return bitCount;
        }

        private static byte[] ConvertBitsToBytes(BitArray data)
        {
            var bytes = new byte[1 + (data.Length / 8)];
            for (int i = 0, o = 0; i < data.Length;)
            {
                for (int j = 7; j >= 0 && i < data.Length; j--)
                {
                    if (data[i++] == true)
                    {
                        bytes[o] |= (byte)(1 << j);
                    }
                }
                o++;
            }
            return bytes;
        }

        public byte[] Encode(byte[] bytes)
        {
            var bits = new BitArray(this.TotalBits);
            this.Encode(bytes, bits, 0);
            return ConvertBitsToBytes(bits);
        }

        private class Node
        {
            public byte Symbol;
            public int Frequency;
            public Node Left;
            public Node Right;
        }
    }
}
