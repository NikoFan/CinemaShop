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
using System.Windows.Shapes;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        // Перенос окна
        public void DragWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DragMove();
                Console.WriteLine(Head.GetType());
            }
            catch (Exception)
            {
                Console.WriteLine("nothing");
            }
        }

        // Вывод сообщений для пользователя
        public bool UsersMessage(string message)
        {
            return (MessageBox.Show(message, "Тех-поддержка", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);

        }
        // Закрытие окна
        public void closeApp(object sender, RoutedEventArgs e)
        {
            if (UsersMessage("Вы хотите закрыть приложение?"))
                Environment.Exit(0);
        }

        // Возврат к главному окну
        public void goBack(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("go back");
            MainWindow mainWindow = new MainWindow()
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = Left,
                Top = Top
            };

            this.Visibility = Visibility.Collapsed;
            mainWindow.Show();
        }

        // Переход на окно аккаунта
        private void accountWindowOpen()
        {
            Account accountWindow = new Account()
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = Left,
                Top = Top
            };

            this.Visibility = Visibility.Collapsed;
            accountWindow.Show();
        }

        // Авторизация пользователя
        public void doCustomerEnter(object sender, RoutedEventArgs e)
        {
            try
            {
                if (new CheckInputInformation().IsValidEmail(customer_email.Text.ToString()) && // Если почта корректна
                new DatabaseWork().checkCustomerAccDataToSighUp(customer_email.Text.ToString(), customer_password.Text.ToString()) && // Проверка почты и пароля что такой акк существует

                customer_password.Text.ToString().Length > 0) // Если пароль не пустой
                {
                    // Пользователь авторизован
                    Console.WriteLine("AUTH");

                    // При удачной регистрации выводим сообщение
                    UsersMessage("ПОЛЬЗОВАТЕЛЬ АВТОРИЗОВАН.");
                    accountWindowOpen();
                    return;
                }

                UsersMessage("Неверные данные для регистрации!\nПроверьте Почту!\nУдостоверьтесь что поля не пустые.");
            }
            catch (Exception) // Ловим ошибки при работе с базой данных
            {
                UsersMessage("Удостоверьтесь что поля не пустые.");
            }
            return;
        }
    }
}
