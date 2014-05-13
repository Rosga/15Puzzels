using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _flags = new bool[15];
            for (int i = 0; i < 15; i++)
            {
                _flags[i] = false;
            }

            DrawBoard();
            //btnRestart.Visibility = Visibility.Visible;
        }

        private bool [] _flags;

        private EmptyCell _emptyCell = new EmptyCell();


        /// <summary>
        /// Малює 15 елементів паззду
        /// </summary>
        private void DrawBoard()
        {
            //очистити ігрову панель
            cnv.Children.Clear();

            //оголошення масиву з 15 чисел, що слугуватимуть цільовим значенням комірок
            var arr = new int[15];
            for (int i = 0; i < 15; i++)
            {
                arr[i] = i+1;
            }
            //кількість елементів в масиві, що залишились незадіяними
            //var numbersInArrayLeft = 15;

            //створити Генератор випадкових чисел
            var rnd = new MerseneTwister();
            
            //Цикл
            //Виконати чотири рази, де кожна ітерація відповідає за рядок
            for (int i = 0; i < 4; i++)
            {
                

                //Цикл
                //Виконати чотири рази, де кожна ітерація відповідає стовпець
                for (int j = 0; j < 4; j++)
                {
                    //поточна позиція комірки
                    var pos = i*4 + j + 1;

                    //Вийти з циклу, якщо дістались до останньої комірки
                    if (pos == 16)
                        break;

                    //згенерувати випадкове число, що слугує номером елементу в масиві
                    //var target = rnd.GetRandom(16 - pos);
                    //var target1 = pos;

                    //створити новий елемент пазлу
                    var item = new PuzzleItem(pos)
                    {
                        Width = cnv.Width / 4,
                        Height = cnv.Height / 4,
                        X = i,
                        Y = j,
                        CurrentPosition = pos
                    };
                    Canvas.SetTop(item, i * cnv.Height / 4);
                    Canvas.SetLeft(item, j * cnv.Width / 4);
                    item.MouseLeftButtonUp += new MouseButtonEventHandler(item_MouseHandler);

                    //if (item.CurrentPosition == item.TargetPosition)
                    //{
                    //    flags[item.TargetPosition - 1] = true;
                    //}

                    cnv.Children.Add(item);


                    ////поміняти місьцями задіяний елемент із останнім незадіяним
                    //numbersInArrayLeft--;
                    //var temp = arr[target];
                    //arr[target] = arr[numbersInArrayLeft];
                    //arr[numbersInArrayLeft] = temp;
                }
            }

            _emptyCell.Position = 16;

            for (int i = 0; i < 164; i++)
            {
                Random();
            }
            foreach (var item in cnv.Children.OfType<PuzzleItem>())
            {
                if (item.CurrentPosition == item.TargetPosition)
                {
                    _flags[item.TargetPosition - 1] = true;
                }
            }
        }


        private void item_MouseHandler(object sender, MouseEventArgs e)
        {
            var item = sender as PuzzleItem;

            var f = IsEmptyContact(item.CurrentPosition);
            if (f == true)
            {
                cnv.Children.Remove(item);
                var x = (_emptyCell.Position - 1) / 4;
                var y = (_emptyCell.Position - 1) % 4;
                Canvas.SetTop(item, (int)((_emptyCell.Position-1) / 4) * cnv.Width/4);
                Canvas.SetLeft(item, (int)((_emptyCell.Position -1) % 4) * cnv.Width/4);
                var temp = _emptyCell.Position;
                _emptyCell.Position = item.CurrentPosition;
                item.CurrentPosition = temp;

                if (item.CurrentPosition == item.TargetPosition)
                {
                    _flags[item.TargetPosition - 1] = true;
                }



                if (_emptyCell.Position == 16)
                {
                    if (CheckFlags() == true)
                    {
                        cnv.Children.Clear();
                        btnRestart.Visibility = Visibility.Visible;
                        Congrad.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        cnv.Children.Add(item);
                        
                    }
                }
                else
                {
                    _flags[_emptyCell.Position - 1] = false;
                    cnv.Children.Add(item);
                    
                }
                

            }
        }

        private bool IsEmptyContact(int pos)
        {
            if (pos == _emptyCell.Position -4 || pos == _emptyCell.Position -1 || pos == _emptyCell.Position +1 || pos == _emptyCell.Position  +4)
            {
                return true;
            }
            return false;
        }

        private bool CheckFlags()
        {
            if (_flags.Any(t => t==false))
            {
                return false;
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnRestart.Visibility = Visibility.Hidden;
            Congrad.Visibility = Visibility.Hidden;

            _flags = new bool[15];
            for (int i = 0; i < 15; i++)
            {
                _flags[i] = false;
            }
            DrawBoard();


        }

        private void Random()
        {
            var rnd = new MerseneTwister();
            var num = rnd.GetRandom(4);
            var pos = _emptyCell.GetRandomContactSide(num);
            var item = cnv.Children.OfType<PuzzleItem>().First(i => i.CurrentPosition == pos);
            

            cnv.Children.Remove(item);
            var x = (_emptyCell.Position - 1) / 4;
            var y = (_emptyCell.Position - 1) % 4;
            Canvas.SetTop(item, (int)((_emptyCell.Position - 1) / 4) * cnv.Width / 4);
            Canvas.SetLeft(item, (int)((_emptyCell.Position - 1) % 4) * cnv.Width / 4);
            var temp = _emptyCell.Position;
            _emptyCell.Position = item.CurrentPosition;
            item.CurrentPosition = temp;
            cnv.Children.Add(item);

        }



    }
}
