using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace SailAway
{

    class XmlTool
    {
        protected 
        protected XmlTool()
        {

        }
        protected void LoadXmlStuff(XmlDocument XmlFile)
        {
            XmlNode LevelNode = XmlFile.FirstChild.NextSibling;

            foreach (XmlNode lBasicPlatform in LevelNode.SelectSingleNode("BasicPlatforms").ChildNodes)
            {
                XmlNode ColorNode = lBasicPlatform.SelectSingleNode("ColorRGBA");//I think it's more effecient to store this location and not write it out four times in the line below but idk.
                Color mBasicPlatformColor = new Color(new Vector4(ChildNodeIntFromParent(ColorNode, "Red"), ChildNodeIntFromParent(ColorNode, "Green"), ChildNodeIntFromParent(ColorNode, "Blue"), ChildNodeIntFromParent(ColorNode, "Alpha")));

                XmlNode CoordinatesNode = lBasicPlatform.SelectSingleNode("Coordinates");//I think it's more effecient to store this location and not write it out four times in the line below but idk.
                Platform platform = new Platform();
                Platform basicBasicPlatform = new Platform( Pixel, Math.Abs(ChildNodeIntFromParent(CoordinatesNode, "x1") - ChildNodeIntFromParent(CoordinatesNode, "x2")), Math.Abs(ChildNodeIntFromParent(CoordinatesNode, "y1") - ChildNodeIntFromParent(CoordinatesNode, "y2")), ChildNodeIntFromParent(CoordinatesNode, "x1"), ChildNodeIntFromParent(CoordinatesNode, "x2"), mBasicPlatformColor, ChildNodeStringFromParent(lBasicPlatform, "Name"));
                Platform basicBasicPlatform2 = new Platform( Pixel, 300, 300, 1, 1, Color.HotPink, "cock");
            }
        }



        public static int ChildNodeIntFromParent(XmlNode pParentNode, string pNodeName)
        {
            return NodeInt(pParentNode.SelectSingleNode(pNodeName));
        }
        public static string ChildNodeStringFromParent(XmlNode pParentNode, string pNodeName)
        {
            return NodeString(pParentNode.SelectSingleNode(pNodeName));
        }
        public static string NodeString(XmlNode pNode)
        {
            return pNode.FirstChild.Value;
        }
        public static int NodeInt(XmlNode pNode)
        {
            return Int32.Parse(pNode.FirstChild.Value);
        }
    }
}
