using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Games
{
    public class SourceCfgFile
    {
        protected Stream localStream;
        protected StreamReader reader;
        protected StreamWriter writer;
        protected string rawData;
        protected string backupData;


        public string RawData
        {
            get { return rawData; }
        }

        public SourceCfgFile(Stream stream)
        {
            localStream = stream;
            reader = new StreamReader(stream);
            rawData = reader.ReadToEnd();
            backupData = string.Copy(rawData);
        }
        public void Reset()
        {
            rawData = string.Copy(backupData);
        }

        public void ChangeProperty(string propertyName, string value)
        {
            int start;
            int end;
            GetPosition(rawData, propertyName, out start, out end);

            rawData = rawData.Remove(start, end - start);
            rawData = rawData.Insert(start, value);
        }

        public void Write(Stream stream)
        {
            writer = new StreamWriter(stream);
            writer.Write(rawData);
            writer.Flush();
            stream.Flush();
        }

        private void GetPosition(string text, string word, out int start, out int end)
        {
            start = -1;
            end = 0;

            int def = text.IndexOf(word);
            int aspas = 0;
            bool firstNumber = true;
            for (int i = def; i < text.Length; i++)
            {
                char c = text[i];
                if (start == -1)
                {
                    if (c == '"')
                    {
                        aspas++;
                        if (aspas == 2)
                        {
                            start = i;
                        }
                    }
                }
                else
                {
                    if (StringUtil.IsNumber(c) && firstNumber)
                    {
                        firstNumber = false;
                        start = i;
                    }
                    if (!StringUtil.IsNumber(c))
                    {
                        end = i;
                        break;
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
