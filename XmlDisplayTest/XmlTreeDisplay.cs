using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlDisplayTest
{
    public class XmlTreeDisplay : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TreeView treeXml = new TreeView();

        public XmlTreeDisplay()
        {
            treeXml.Nodes.Clear();
            this.Controls.Add(treeXml);
            //Load the XML Document
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml("<books><A property='a'><B>text</B><C>texttg</C><D>99999</D></A></books>");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }

            ConvertXmlNodeToTreeNode(doc, treeXml.Nodes);
            treeXml.Nodes[0].ExpandAll();
        }

        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode, TreeNodeCollection treeNodes)
        {
            TreeNode newTreeNode = treeNodes.Add(xmlNode.Name);

            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    newTreeNode.Text = "<?" + xmlNode.Name + " " +
                        xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    newTreeNode.Text = "<" + xmlNode.Name + ">";
                    break;
                case XmlNodeType.Attribute:
                    newTreeNode.Text = "ATTTRIBUTE: " + xmlNode.Name;
                    break;
                case XmlNodeType.Text:
                case XmlNodeType.CDATA:
                    newTreeNode.Text = xmlNode.Value;
                    break;
                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
            }
        }

        public static void Main()
        {
            Application.Run(new XmlTreeDisplay());
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XmlTreeDisplay
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "XmlTreeDisplay";
            this.Load += new System.EventHandler(this.XmlTreeDisplay_Load);
            this.ResumeLayout(false);

        }

        private void XmlTreeDisplay_Load(object sender, EventArgs e)
        {

        }
    }
}
