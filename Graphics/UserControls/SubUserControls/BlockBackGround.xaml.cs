using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FESScript2.Graphics.UserControls.SubUserControls
{
    /// <summary>
    /// Interaction logic for BlockBackGround.xaml
    /// </summary>
    public partial class BlockBackground : UserControlPlus
    {
        public BlockBackground()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets color to Type color.
        /// </summary>

        public Type Type 
        {
            set
            {
                try 
                { 
                    Fill = ColorsBrushes.TypeToBrush[value];
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Sets color to Brush.
        /// </summary>

        public Brush Fill
        {
            get
            {
                return border1.Background;
            }
            set
            {
                border1.Background = value;
                border2.Background = value;
            }
        }

        /// <summary>
        /// Sets stroke to brush.
        /// </summary>

        public Brush Stroke
        {
            get
            {
                return border1.BorderBrush;
            }
            set
            {
                border1.BorderBrush = value;
                border2.BorderBrush = value;
            }
        }

        /// <summary>
        /// Sets thickness of border.
        /// </summary>

        public string StrokeThickness
        {
            set
            {
                string[] tempStr = value.Split('|');
                double[] tempDbl = new double[tempStr.Length];
                for (int i = 0; i < tempStr.Length; i++) 
                {
                    tempStr[i] = tempStr[i].Trim();
                    tempDbl[i] = double.Parse(tempStr[i]);
                    //System.Convert.ToDouble(tempStr[i]);
                }
                if (tempDbl.Length == 1)
                {
                    border1.BorderThickness = new Thickness(tempDbl[0]);
                }
                else if (tempDbl.Length == 2)
                {
                    border1.BorderThickness = new Thickness(tempDbl[0], tempDbl[1], tempDbl[0], tempDbl[1]);
                }
                else if (tempDbl.Length == 4)
                {
                    border1.BorderThickness = new Thickness(tempDbl[0], tempDbl[1], tempDbl[2], tempDbl[3]);
                }
                else 
                {
                    border1.BorderThickness = new Thickness();
                }
                
            }
        }
    }
}
