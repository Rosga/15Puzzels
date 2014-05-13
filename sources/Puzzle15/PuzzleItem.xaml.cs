﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Puzzle15
{
    /// <summary>
    /// Interaction logic for PuzzleItem.xaml
    /// </summary>
    public partial class PuzzleItem : UserControl
    {
        public PuzzleItem()
        {
            
        }

        public PuzzleItem(int n)
        {
            InitializeComponent();

            TbVal.Text = n.ToString();
            TargetPosition = n;
        }


        public readonly int TargetPosition;


        public int X { get; set; }
        public int Y { get; set; }
        public int CurrentPosition { get; set; }

        //private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    TbVal.Text = "I'm Selected";
        //}


    }
}
