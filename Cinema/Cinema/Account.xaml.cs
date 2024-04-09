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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public Account()
        {
            InitializeComponent();
            // Заполнение информации про пользователя
            CustomerInformationSetter();
        }


        // Заполнение Информации про пользователя
        private void CustomerInformationSetter()
        {
            // Перебираем Словрь с пользовательской информацие из класса работы с БД
            Dictionary<string, string> customerInformation = new DatabaseWork().takeCustomerAccountInformation();
            CustomerLogin.Text = customerInformation["Имя"];
            CustomerPassword.Text = customerInformation["Пароль"];
            CustomerEmail.Text = customerInformation["Почта"];
            CustomerAge.Text = customerInformation["Возраст"];
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

        // Выход из аккаунта
        public void reLogin(object sender, RoutedEventArgs e)
        {
            // Очищаем КЕШ от id аккаунта
            new CashWork().setCustomerId("nth");
            // Переходим в окно авторизации
            AuthorizationWindow authorizationWindow = new AuthorizationWindow()
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = Left,
                Top = Top
            };

            this.Visibility = Visibility.Collapsed;
            authorizationWindow.Show();
        }

        // Переход к окну регистрации
        public void goRegistrationWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("goRegistrationWindow");
            RegistrationWindow registrationWindow = new RegistrationWindow()
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = Left,
                Top = Top
            };

            this.Visibility = Visibility.Collapsed;
            registrationWindow.Show();
        }

    }
}
