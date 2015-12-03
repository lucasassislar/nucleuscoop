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

using System.Collections;

namespace Gibbed.Borderlands2.FileFormats.Huffman
{
    public static class Decoder
    {
        private static byte DecodeByte(BitArray data, ref int offset)
        {
            byte value = 0;
            for (int i = 7; i >= 0; i--)
            {
                value |= (byte)((data[offset++] ? 1 : 0) << i);
            }
            return value;
        }

        private static int DecodeNode(BitArray data, ref int offset, Node[] tree, ref int index)
        {
            var current = index;
            index++;

            var isLeaf = data[offset++];

            if (isLeaf == true)
            {
                var value = DecodeByte(data, ref offset);
                tree[current].Left = -1;
                tree[current].Right = -1;
                tree[current].IsLeaf = true;
                tree[current].Symbol = value;
            }
            else
            {
                tree[current].Left = DecodeNode(data, ref offset, tree, ref index);
                tree[current].Right = DecodeNode(data, ref offset, tree, ref index);
                tree[current].IsLeaf = false;
            }

            return current;
        }

        private static BitArray ConvertBytesToBits(byte[] data)
        {
            var bitArray = new BitArray(data.Length * 8);
            for (int i = 0, o = 0; i < data.Length; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    bitArray[o++] = (data[i] & (1 << j)) != 0;
                }
            }
            return bitArray;
        }

        public static byte[] Decode(byte[] input, int maxLength)
        {
            var bitArray = ConvertBytesToBits(input);

            var tree = new Node[511];
            var index = 0;
            var offset = 0;
            DecodeNode(bitArray, ref offset, tree, ref index);
            return Decode(tree, bitArray, offset, maxLength);
        }

        private static byte[] Decode(Node[] tree, BitArray input, int offset, int maxLength)
        {
            var output = new byte[maxLength];
            var left = maxLength;

            var o = 0;
            while (left > 0)
            {
                var branch = tree[0];
                while (branch.IsLeaf == false)
                {
                    var index = input[offset++] == false ? branch.Left : branch.Right;
                    branch = tree[index];
                }

                output[o++] = branch.Symbol;
                left--;
            }

            return output;
        }

        private struct Node
        {
            public byte Symbol;
            public bool IsLeaf;
            public int Left;
            public int Right;
        }
    }
}
