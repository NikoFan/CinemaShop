using Cinema.Properties;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
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

        // Переход к Аккаунта
        public void goAuthorizationWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Settings.Default["CustomerID"].ToString());
            if (Settings.Default["CustomerID"].ToString() == "nth" || Settings.Default["CustomerID"].ToString() == "")
            {
                AuthorizationWindow authorizationWindow = new AuthorizationWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Left = Left,
                    Top = Top
                };

                this.Visibility = Visibility.Collapsed;
                authorizationWindow.Show();
                return;
            }
            // если пользователь авторизован
            accountWindowOpen();

        }

        // Регистрация пользователя
        public void doCustomerRegistration(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Регистрация пользователя");
            Dictionary<string, string> getInf = new Dictionary<string, string>()
            {
                {"логин", ""}, {"пароль", ""}, {"почта", ""}, {"возраст", ""}
            };
            try
            {
                if (customer_login.Text.ToString().Length > 0 && // Если имя не пустое
                Convert.ToInt32(customer_age.Text.ToString()) > 0 && Convert.ToInt32(customer_age.Text.ToString()) < 110 && // если возраст не фальшивый
                new CheckInputInformation().IsValidEmail(customer_email.Text.ToString()) && // Если почта корректна
                new DatabaseWork().checkEmailTwice(customer_email.Text.ToString()) && // Проверка почты на повторение
                customer_password.Text.ToString().Length > 0) // Если пароль не пустой
                {
                    // Пользователь регистрируется
                    Console.WriteLine("REG");
                    // Заполняем словарь и отправляем все данные на регистрацию
                    getInf["логин"] = customer_login.Text.ToString();
                    getInf["пароль"] = customer_password.Text.ToString();
                    getInf["почта"] = customer_email.Text.ToString();
                    getInf["возраст"] = customer_age.Text.ToString();
                    // Отправляем данные на регистрацию
                    new DatabaseWork().customerRegistration(getInf);
                    // При удачной регистрации выводим сообщение
                    UsersMessage("ПОЛЬЗОВАТЕЛЬ ЗАРЕГИСТРИРОВАН.");
                    accountWindowOpen();
                    return;
                }

                UsersMessage("Неверные данные для регистрации!\nПроверьте Почту или Возраст!\nУдостоверьтесь что поля не пустые, а в графе 'Возраст' отсутствуют буквы.");
            }
            catch (Exception) // Ловим ошибки при работе с базой данных
            {
                UsersMessage("Удостоверьтесь что поля не пустые, а в графе 'Возраст' отсутствуют буквы.");
            }
            return;
            
        }
    }
}
