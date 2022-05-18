using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Wpf_ChangeImage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool Turn = true;
        int count = 0;
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (Turn)
            {
                b.Content = "X";
                b.IsEnabled = false;
                lbl1.Content = "O's turm";
            }
            else
            {
                b.Content = "O";
                b.IsEnabled = false;
                lbl1.Content = "X's turn";
            }
            Turn = !Turn;
            count++;
            CheckWinner();
        }
        void CheckWinner()
        {
            bool there_is_a_winner = false;
            if ((!btnA1.IsEnabled) && btnA1.Content == btnA2.Content && btnA2.Content == btnA3.Content)
            there_is_a_winner = true;
            if ((!btnB1.IsEnabled) && btnB1.Content == btnB2.Content && btnB2.Content == btnB3.Content)
            there_is_a_winner = true;
            if ((!btnC1.IsEnabled) && btnC1.Content == btnC2.Content && btnC2.Content == btnA3.Content)
            there_is_a_winner = true;
            //
            if ((!btnA1.IsEnabled) && btnA1.Content == btnB1.Content && btnB1.Content == btnC1.Content)
            there_is_a_winner = true;
            if ((!btnA2.IsEnabled) && btnA2.Content == btnB2.Content && btnB2.Content == btnC2.Content)
            there_is_a_winner = true;
            if ((!btnA3.IsEnabled) && btnA3.Content == btnB3.Content && btnB3.Content == btnC3.Content)
            there_is_a_winner = true;
            //
            if ((!btnA1.IsEnabled) && btnA1.Content == btnB2.Content && btnB2.Content == btnC3.Content)
            there_is_a_winner = true;
            if ((!btnA3.IsEnabled) && btnA3.Content == btnB2.Content && btnB2.Content == btnC1.Content)
            there_is_a_winner = true;

            if (there_is_a_winner)
            {
                string Winner = "";
                if (Turn)
                    Winner = "O";
                else
                    Winner = "X";
                MessageBox.Show(Winner + " - Победитель");
                DisableButton();
            }
            if (count == 9) MessageBox.Show("Ура");
        }
        void DisableButton()
        {
            btnA1.IsEnabled = false;
            btnA2.IsEnabled = false;
            btnA3.IsEnabled = false;
            btnB1.IsEnabled = false;
            btnB2.IsEnabled = false;
            btnB3.IsEnabled = false;
            btnC1.IsEnabled = false;
            btnC2.IsEnabled = false;
            btnC3.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnA1.Content = "";
            btnA2.Content = "";
            btnA3.Content = "";
            btnB1.Content = "";
            btnB2.Content = "";
            btnB3.Content = "";
            btnC1.Content = "";
            btnC2.Content = "";
            btnC3.Content = "";

            btnA1.IsEnabled = true;
            btnA2.IsEnabled = true;
            btnA3.IsEnabled = true;
            btnB1.IsEnabled = true;
            btnB2.IsEnabled = true;
            btnB3.IsEnabled = true;
            btnC1.IsEnabled = true;
            btnC2.IsEnabled = true;
            btnC3.IsEnabled = true;
        }
    }
}
