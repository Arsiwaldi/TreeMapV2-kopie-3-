using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace TreeMapV2
{
    /// <summary>
    /// Interaction logic for TreeMapControl.xaml
    /// </summary>
    public partial class TreeMapControl : UserControl
    {
        private static int _innerMargin = 10;
        private static Random rnd;


        public TreeMapControl()
        {
            InitializeComponent();
            Loaded +=OnLoaded;
            var rect = (Rectangle) FindName("parent");
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            rnd = new Random();

            var xmlStruct = new XmlReaderHelper();

            var arrList = new List<Tuple<double[], double[], XmlNode>>();

                 double x = CanvasGrid.ActualWidth;
            double y = CanvasGrid.ActualHeight;
            TreeMap(xmlStruct.XmlRoot, new[] {0.0,0.0}, new [] {CanvasGrid.ActualWidth, CanvasGrid.ActualHeight }, 0, arrList);
            //TreeMap(xmlStruct.XmlRoot, new[] { 0.0, 0.0 }, new[] { CanvasGrid.ActualWidth, CanvasGrid.ActualHeight }, 0);

            /*foreach (var arr in arrList)
            {
                PaintRectangle(arr.Item1,arr.Item2,arr.Item3);
            }*/


        }


        //P  - 0-x || 1-y
        //Q  - 0-x || 1-y
        private void PaintRectangle(double[] P, double[] Q, XmlNode root)
        {

            Color color = new Color
            {
                R = (byte) rnd.Next(0, 256),
                G = (byte) rnd.Next(0, 256),
                B = (byte) rnd.Next(0, 256),
                A = 100
            };

            Color borderColor = new Color()
            {
                R = (byte) rnd.Next(0, 256),
                G = (byte) rnd.Next(0, 256),
                B = (byte) rnd.Next(0, 256),
                A = 100
            };


            var margin = GetDepth(root);
            var topRoot = GetTopRoot(root, margin/10);

            var width = Equals(P[0], Q[0]) ? Q[0] : Q[0] - P[0];
            var height = Equals(P[1], Q[1]) ? Q[1] : Q[1] - P[1];

            var topmargin = Equals(P[1], Q[1]) ? 0 : P[1];
            var leftmargin = Equals(P[0], Q[0]) ? P[0] : P[0];

            var rect = new Rectangle
            {
                //Stroke = new SolidColorBrush(borderColor),
                //StrokeThickness = 5,
                Fill = new SolidColorBrush(color),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Name = root.Name
            };

            #region Test 

            /*
                Pokud je to uzel který má další uzel a zároveň jeho pozice je P[0] = 0, tzn je to ten 
                nejlevejsi. 
                Neboli tzv. first-child
             */

            /*
            if (Equals(P[0], 0.0) && root.NextSibling != null)
            {
                rect.Width = width - margin;
                rect.Height = height - 2*margin;
                rect.Margin = new Thickness(leftmargin + margin, topmargin + margin, 0,0);
            }

            /*
                Pokud je to uzel, ktery je poslednim childem. tzv. last-child a zaroven otec tohoto uzlu
                musi mít více potomku jak jeden. Ponevadz pote nedavá vyznam kontrolovat jestli je to prvni
                nebo posledni uzel. Ponevadz tento uzel by byl jak prvnim tak poslednim.
                
                například tedy 3 uzly

            else if (root.ParentNode?.LastChild == root && root.ParentNode.ChildNodes.Count > 1)
            {
                rect.Width = width - margin - _innerMargin;
                rect.Height = height - 2*margin;
                rect.Margin = new Thickness(leftmargin + _innerMargin,topmargin + margin,margin,0);
            }

            /*
                Pokud je to kořenový uzel v XML struktůře, myšleno ten úplně první.
              
            else if (root.ParentNode?.Name == "#document")
            {
                rect.Width = width - 2* margin;
                rect.Height = height - 2*margin;
                rect.Margin = new Thickness(leftmargin + margin, topmargin + margin, 0, 0);
            }

            /*
                Pokud je to uzel, který má pouze jednoho potomka nebo žadného potomka a jedná se tedy o
                listový uzel a zaroveň se nachazí vlevo

            else if ( (root.ChildNodes.Count == 1 || root.ChildNodes.Count == 0) && Equals(P[0],0.0))
            {
                if (margin >= 30)
                    rect.Width = width - margin - (margin - 20);
                else
                    rect.Width = width - margin;
                rect.Height = height - 2*margin;
                rect.Margin = new Thickness(leftmargin + margin, topmargin + margin, 0,0);
            }

            else if ((root.ChildNodes.Count == 1 || root.ChildNodes.Count == 0) && !Equals(P[0], 0.0) &&
                     root.ParentNode?.ChildNodes.Count < 2 && 
                     (root.ParentNode.FirstChild != root || root.ParentNode.LastChild != root) )
            {
                if (margin >= 30)
                    rect.Width = width - margin - (margin - 20) - _innerMargin;
                else
                    rect.Width = width - margin;
                // rect.Width = width - margin - 2*_innerMargin;
                rect.Height = height - 2*margin;
                rect.Margin = new Thickness(leftmargin + (margin - 20) + _innerMargin, topmargin + margin, 0, 0);
            }

            else
            {
                if (margin >= 30)
                {
                    rect.Width = width - margin - (margin - 20) + _innerMargin;
                    rect.Height = height - 2*margin; 
                    rect.Margin = new Thickness(leftmargin + margin - _innerMargin, topmargin + margin, 0, 0);
                }
                else
                {
                    rect.Width = width - _innerMargin;
                    rect.Height = height - 2 * margin;
                    rect.Margin = new Thickness(leftmargin + _innerMargin, topmargin + margin, 0, 0);
                } 
            }
            */

            #endregion

            if (root.ParentNode?.Name == "#document")
            {
                rect.Width = width;
                rect.Height = height;
                rect.Margin = new Thickness(leftmargin, topmargin + margin , 0, 0);
            }
            
            // otec tohoto kořene má alespoň dva potomky a zároveň není posledním potomkem svého otce
            // cili všechny uzly od prvního po poslední uzel (mimo něj)

            else if (root.ParentNode?.ChildNodes.Count >= 2 && root.ParentNode.LastChild != root)
            {
                if (margin >= 20)
                {
                    rect.Width = width - margin - (margin - 10);
                    rect.Height = height - margin;
                }
                else
                {
                    rect.Width = width - margin;
                    rect.Height = height - 2 * margin;
                }
                rect.Margin = new Thickness(leftmargin + margin, topmargin + margin, 0,0);
            }

            // tento kořen je posledním potomkem svého otce, ale musí platit že otec má alespoň 2 potomky
            else if (root.ParentNode?.LastChild == root && root.ParentNode.ChildNodes.Count >= 2)
            {
                if (margin < 20)
                {
                    rect.Width = width - 2*margin;
                    rect.Height = height - 2*margin;
                }
                else
                {
                    rect.Width = width - margin - (margin - 10);
                    rect.Height = height - 2*margin;
                }
              
                rect.Margin = new Thickness(leftmargin + margin, topmargin + margin, 0, 0);
            }

            // otec má pouze jednoho potomka

            else if (root.ParentNode.NextSibling == null)
            {
               // if (margin <= 20)
                //{
                    rect.Width = width - 2*margin;
                    rect.Height = height - 2*margin;
               /* }
                else
                {
                    rect.Width = width - margin - (margin - 10);
                    rect.Height = height - 2 * margin;
                }*/
                rect.Margin = new Thickness(leftmargin + margin, topmargin + margin, 0, 0);
            }

            else
            {
                rect.Width = width - margin - (margin - _innerMargin);
                rect.Height = height - 2 * margin;
                rect.Margin = new Thickness(leftmargin + margin, topmargin + margin, 0, 0);
            }

          
            rect.MouseDown  += delegate(object sender, MouseButtonEventArgs args)
            {
                /*var realWidth = sender.GetType().GetProperty("Width").GetValue(sender).ToString();
                var realHeight = sender.GetType().GetProperty("Height").GetValue(sender).ToString();
                var realName = sender.GetType().GetProperty("Name").GetValue(sender).ToString();
                // MessageBox.Show(sender.GetType().GetProperty("Margin").GetValue(sender).ToString());
                MessageBox.Show($"Width - {realWidth} Height - {realHeight} Name - {realName}");*/
                var realName = sender.GetType().GetProperty("Name").GetValue(sender).ToString();
                MessageBox.Show(realName);
            };
            TreeMapCanvas.Children.Add(rect);
        }

        private int CheckSize(int size)
        {
            return (size > 0 ? size : 1);
        }

        int GetDepth(XmlNode element)
        {
            int depth = 0;
            while (element != null)
            {
                depth++;
                element = element.ParentNode;
            }
            return ((depth-1)*10) - 10;
        }

        XmlNode GetTopRoot(XmlNode root, int depth)
        {
            XmlNode toproot = root;
            while (depth != 0)
            {
                toproot = toproot.ParentNode;
                depth--;
            }

            return toproot;
        }

        int GetChildsCount(XmlNode root, ref int sum)
        {

            sum += 1;

            var childs = root.ChildNodes;
            foreach (XmlNode child in root)
            {
                GetChildsCount(child,ref sum);
               
            }

            return sum;
        }

        public void TreeMap(XmlNode root, double[] P, double[] Q, int axis, List<Tuple<double[],double[],XmlNode>> list)
        {

            list.Add(new Tuple<double[], double[], XmlNode>(new []{P[0],P[1]}, new [] {Q[0],Q[1]}, root));
            using (var writer = new StreamWriter("C:/Detail.txt", true))
            {
               writer.WriteLine($"P-[{P[0]},{P[1]}] Q-[{Q[0]},{Q[1]}] Name:{root.Name}");
            }


            var width = Math.Abs(Q[axis] - P[axis]);

            foreach (XmlNode child in root.ChildNodes)
            {
                int sum = 0;
                var rootSize = GetChildsCount(root,ref sum) -1;
                sum = 0;
                var childSize = GetChildsCount(child, ref sum) - 1;
                
                Q[axis] = P[axis] + (childSize / (double)rootSize) * width;
                TreeMap(child, new [] {P[0],P[1]}, new [] {Q[0],Q[1]}, 1 - axis, list);
                P[axis] = Q[axis];
            }

        }

    }
}

