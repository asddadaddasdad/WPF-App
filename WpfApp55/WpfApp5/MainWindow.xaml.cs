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
using Microsoft.Win32;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Interop;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Media.Animation;
using System.IO;
using System.Data.SqlClient;
using WpfApp5;




namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string DS = "DESKTOP-FLSSCRR\\SQLEXPRESS", IC = "Test_Cargo1";
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 750;
            btnAnimation.Duration = TimeSpan.FromSeconds(2);
            Window.BeginAnimation(Button.WidthProperty, btnAnimation);

            AniHeight(CheckPassword, 0, 35, 1);
            AniHeight(btEnter, 0, 35, 1);
            AniHeight(btExit, 0, 35, 1);
            AniHeight(Bb1, 0, 35, 1);
        }

        public Random RandomCode = new Random();
        public bool CanIEnter = true;
        public static string MyPassword;

        private void AniHeight(Button RandomButton, int From, int To, int Seconds)
        {
            DoubleAnimation ButtonAnimation = new DoubleAnimation();
            ButtonAnimation.From = From;
            ButtonAnimation.To = To;
            ButtonAnimation.Duration = TimeSpan.FromSeconds(Seconds);
            RandomButton.BeginAnimation(Button.HeightProperty, ButtonAnimation);
        }

        private void FileCreator()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\SaveLogs"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\SaveLogs");
            }
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\SaveLogs\\Log.txt"))
            {
                using (FileStream fileStreamer = new FileStream(Directory.GetCurrentDirectory() + "\\SaveLogs\\Log.txt", FileMode.OpenOrCreate)) { }
            }
        }

        private void LogWriter(string bag)
        {
            using (StreamWriter write = new StreamWriter(Directory.GetCurrentDirectory() + "\\SaveLogs\\Log.txt", true))
            {
                write.WriteLine($"[{DateTime.Now}] Возникла ошибка: {bag}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private bool IsInstalledWord()
        {
            try
            {
                RegistryKey KeyWord = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office\Word");
                if (KeyWord != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsInstalledExcel()
        {
            try
            {
                RegistryKey KeyExel = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office\Excel");
                if (KeyExel != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsInstalledSQL()
        {
            try
            {
                RegistryKey KeyExel = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                if (KeyExel != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private void Reget()
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("DBSetAPPConfig");
            try
            {
                DataSetClass.DS = key.GetValue("DS").ToString();
                DataSetClass.IC = key.GetValue("IC").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DataSetClass.DS = "DESKTOP-FLSSCRR\\SQLEXPRESS";
                DataSetClass.IC = "Test_Cargo1";

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileCreator();
            Reget();
            //Вызов метода получения из реестра ОС информации о строке подключения  
            DataSetClass dataSetClass = new DataSetClass();
            //Организация переключателя для проверки правильного подключения к БД
            switch (dataSetClass.connection_Checking())
            {
                //Если подключение открыто успешно
                case true:
                    if (IsInstalledWord())
                    {
                        if (IsInstalledSQL())
                        {
                            if (IsInstalledExcel())
                            {
                            }
                            else
                            {
                                //Вывод сообщения об ошибке, что запись не выбрана в элемента управления
                                MessageBox.Show("Ошибка, нет требуемой установленной Microsoft Office Excel", "Составление расписания");
                                Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка, нет требуемой установленной Microsoft SQL Server", "Составление расписания");
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, нет требуемой установленной Microsoft Office Word", "Составление расписания");
                        Close();
                    }
                    break;
                case false:
                    //Объявление экземпляра класса окна конфигурирования строки подключения к источнику данных
                    ConfigurationWindow configurationWindow = new ConfigurationWindow();
                    //Вызов экземпляра класса окна в режиме диалогового окна 
                    configurationWindow.ShowDialog();
                    break;
            }
        }

        private void TakeCode_Click(object sender, RoutedEventArgs e)
        {
            MyPassword = RandomCode.Next(10000, 99999).ToString();
            MailAddress FromAdress = new MailAddress("ataniyazov84@inbox.com", "Код подтверждения");
            MailAddress ToAdress = new MailAddress($"{TakeMyEmail.Text}", "Пользователь получатель");
            MailMessage mailMessage = new MailMessage(ToAdress, FromAdress);
            mailMessage.Body = $"Ваш код: {MyPassword}";
            mailMessage.Subject = "Ограничение по входу";
            SmtpClient SmptClient = new SmtpClient();
            SmptClient.Host = "smtp.gmail.com";
            SmptClient.Port = 587;
            SmptClient.EnableSsl = true;
            SmptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmptClient.UseDefaultCredentials = false;
            SmptClient.Credentials = new NetworkCredential(FromAdress.Address, "Alikhan2005a");
            SmptClient.Send(mailMessage);
            ThereCode.IsEnabled = true;
            CheckPassword.IsEnabled = true;
            Bb1.IsEnabled = true;
        }

        private void CheckPassword_Click(object sender, RoutedEventArgs e)
        {
            if (ThereCode.Password == MyPassword && ThereCode.Password != null)
            {
                Bb1.IsEnabled = true;
                MessageBox.Show("Код верный !");
            }
            else
            {
                MessageBox.Show("Код не верный !");
                LogWriter("Указанный пароль неверен!");
            }
        }

        private void Bb1_Click(object sender, RoutedEventArgs e)
        {
            Admin ifrm = new Admin();
            ifrm.ShowDialog();
            Close();
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEnter_Click(object sender, RoutedEventArgs e)
        {
            Admin administratorWindow = new Admin();
            administratorWindow.Show();
        }
    }
}
