using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;  

namespace GJ
{
   public class CXml
   {

      /// <summary>
     /// 保存TreeView为Xml
     /// </summary>
     /// <param name="treeViewControl"></param>
     /// <param name="xmlFile"></param>
      public static void SaveTreeViewToXml(TreeView treeViewControl, string xmlFile)
      {
         XmlDocument doc = new XmlDocument(); 
         doc.LoadXml("<冠佳电子></冠佳电子>");
         XmlNode root = doc.DocumentElement;
         //doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);
         TreeNodeToXml(treeViewControl.Nodes, root);
         doc.Save(xmlFile);
      }
      /// <summary>
      /// 加载xml到TreeView
      /// </summary>
      /// <param name="xmlFile"></param>
      /// <param name="treeViewControl"></param>
      public static void LoadXmlToTreeView(string xmlFile, TreeView treeViewControl)
      {
         XmlDocument xmlDocument = new XmlDocument();
         xmlDocument.Load(xmlFile);
         TreeToXml(xmlDocument.ChildNodes, treeViewControl.Nodes);
      }
      private static void TreeNodeToXml(TreeNodeCollection treeNodes, XmlNode xmlNode)
      {
         XmlDocument doc = xmlNode.OwnerDocument;
         foreach (TreeNode treeNode in treeNodes)
         {
            //创建一个xml元素（element）
            XmlNode element = doc.CreateNode("element", treeNode.Text, "");
            //创建一个属性Name
            XmlAttribute attr = doc.CreateAttribute("Name");
            //为属性赋值
            attr.Value = treeNode.Text;
            //为该元素添加属性
            element.Attributes.Append(attr);
            //添加元素
            xmlNode.AppendChild(element);
            if (treeNode.Nodes.Count > 0)
            {
               TreeNodeToXml(treeNode.Nodes, element);
            }
         }
      }
      private static void TreeToXml(XmlNodeList xmlNode, TreeNodeCollection nodes)
      {
         foreach (XmlNode node in xmlNode)
         {
            string strNode;
            if (node.Name == "冠佳电子")            
               TreeToXml(node.ChildNodes,nodes);            
            if (node != null)
            {
               if (node.Attributes != null)
                  if (node.Attributes.Count > 0)
                     strNode = node.Attributes[0].Value;
                  else
                     strNode = node.Name;
               else
                  strNode = node.Value;
            }
            else
               strNode = node.Name;
            if (strNode == "冠佳电子")
               return;
            TreeNode new_Child = new TreeNode(strNode);
            nodes.Add(new_Child);
            TreeToXml(node.ChildNodes, new_Child.Nodes);
         }

      }

      public static Dictionary<string, int> debugDev = new Dictionary<string, int>(); 
      /// <summary>
      /// 加载xml
      /// </summary>
      /// <param name="xlmFile"></param>
      public static bool load(string xlmFile="DevLog\\device.xml")
      {
          try
          {
              if(!File.Exists(xlmFile))
                 return false;
              XmlDocument doc = new XmlDocument();
              doc.Load(xlmFile);
              XmlNode node = doc.DocumentElement;
              XmlNodeList nodeList=node.ChildNodes; 
              for (int i = 0; i < nodeList.Count; i++)
			 {
			    switch (nodeList[i].NodeType)
	            {
                    case XmlNodeType.Element:
                        debugDev.Add(nodeList[i].Name,System.Convert.ToInt32(nodeList[i].InnerText)); 
                        break;
                  case XmlNodeType.Text:
                        debugDev.Add(nodeList[i].Name, System.Convert.ToInt32(nodeList[i].Value)); 
                        break;
		           default:
                        break;
	             } 
			 }
             return true;
          }
          catch (Exception)
          {
              return false;
          }
      }

   }
}
