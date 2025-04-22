using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace GJ
{
    /// <summary>
    /// 序列化
    /// </summary>
    public class CSerializable<T>
    {
        private delegate void WriteHanler(string fileName, T sender);
        private delegate void ReadHanler(string fileName, ref T sender);
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sender"></param>
        public static void Write(string fileName, T sender)
        {
            string fileType = Path.GetExtension(fileName).ToLower();
            if (fileType == ".xml")
            {
                WriteHanler OnWrite = new WriteHanler(WriteXml);
                OnWrite(System.AppDomain.CurrentDomain.BaseDirectory + fileName, sender);
            }
            else
            {
                WriteHanler OnWrite = new WriteHanler(WriteBinary);
                OnWrite(fileName, sender);
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sender"></param>
        public static void Read(string fileName, ref T sender)
        {
            string fileType = Path.GetExtension(fileName).ToLower();
            if (fileType == ".xml")
            {
                ReadHanler OnRead = new ReadHanler(ReadXml);
                OnRead(fileName, ref sender);
            }
            else
            {
                ReadHanler OnRead = new ReadHanler(ReadBinary);
                OnRead(fileName, ref sender);
            }
        }
        /// <summary>
        /// 序列化二进制流
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sender"></param>
        protected static void WriteBinary(string fileName, T sender)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, sender);
            stream.Close();
        }
        /// <summary>
        /// 反序列化二进制流
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sender"></param>
        protected static void ReadBinary(string fileName, ref T sender)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            sender = (T)formatter.Deserialize(stream);
            stream.Close();
        }
        /// <summary>
        /// 序列化xml
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sender"></param>
        public static void WriteXml(string fileName, T sender)
        {
            XmlSerializer formatter = new XmlSerializer(sender.GetType());
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, sender);
            stream.Close();
        }
        /// <summary>
        /// 反序列化xml
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sender"></param>
        public static void ReadXml(string fileName, ref T sender)
        {
            XmlSerializer formatter = new XmlSerializer(sender.GetType());
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            sender =(T)formatter.Deserialize(stream);
            stream.Close();
        }
    }
}
