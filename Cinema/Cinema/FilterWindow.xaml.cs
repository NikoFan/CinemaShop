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
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        /*Border empty = new Border()
        {
            Name = "EMPTY"
        };*/
        // Список для хранения выбранных пунктов фильтра
        public List<Border> choosenAgeBorders = new List<Border>();
        public List<Border> choosenTypeBorders = new List<Border>();
        public List<Border> choosenCountryBorders = new List<Border>();
        public FilterWindow()
        {
            InitializeComponent();
            /*choosenAgeBorders.Add(empty);
            choosenCountryBorders.Add(empty);
            choosenTypeBorders.Add(empty);*/
        }


        // Очистка фильтра Стран
        public void ClearCountry(object sender , RoutedEventArgs e)
        {
            foreach(var point in choosenCountryBorders)
            {
                point.Background = new SolidColorBrush(Colors.White);
            }
            // Очищаем список
            choosenCountryBorders = new List<Border>();
            // choosenCountryBorders.Add(empty);
        }

        // Очистка фильтра Возраст
        public void ClearAge(object sender, RoutedEventArgs e)
        {
            foreach (var point in choosenAgeBorders)
            {
                point.Background = new SolidColorBrush(Colors.White);
            }
            // Очищаем список
            choosenAgeBorders = new List<Border>();
            // choosenAgeBorders.Add(empty);
        }

        // Очистка фильтра Жанр
        public void ClearType(object sender, RoutedEventArgs e)
        {
            foreach (var point in choosenTypeBorders)
            {
                point.Background = new SolidColorBrush(Colors.White);
            }
            // Очищаем список
            choosenTypeBorders = new List<Border>();
            // choosenTypeBorders.Add(empty);
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

        // Выбор возраста в фильтре
        public void ChooseAge(object sender, RoutedEventArgs e)
        {
            Border choosen = (sender as Border);
            // Меняем цвет
            if (choosenAgeBorders.Contains(choosen))
            {
                // Если список уже выбраных пунктов пуст (ничего не выбрано)
                choosen.Background = new SolidColorBrush(Colors.White);

                choosenAgeBorders.Remove(choosen);
                return;
            }
            // Меняем цвет нового на зеленый
            choosen.Background = new SolidColorBrush(Colors.LightGreen);
            // Добавляем
            choosenAgeBorders.Add(choosen);
            return;


        }

        // Выбор жанра в фильтре
        public void ChooseType(object sender, RoutedEventArgs e)
        {
            Border choosen = (sender as Border);
            // Меняем цвет
            if (choosenTypeBorders.Contains(choosen))
            {
                // Если список уже выбраных пунктов пуст (ничего не выбрано)
                choosen.Background = new SolidColorBrush(Colors.White);

                choosenTypeBorders.Remove(choosen);
                return;
            }
            // Меняем цвет нового на зеленый
            choosen.Background = new SolidColorBrush(Colors.LightGreen);
            // Добавляем
            choosenTypeBorders.Add(choosen);
            return;


        }

        // Выбор страны в фильтре
        public void ChooseCountry(object sender, RoutedEventArgs e)
        {
            Border choosen = (sender as Border);
            // Меняем цвет
            if (choosenCountryBorders.Contains(choosen))
            {
                // Если список уже выбраных пунктов пуст (ничего не выбрано)
                choosen.Background = new SolidColorBrush(Colors.White);

                choosenCountryBorders.Remove(choosen);
                return;
            }

            // Меняем цвет нового на зеленый
            choosen.Background = new SolidColorBrush(Colors.LightGreen);
            // Добавляем
            choosenCountryBorders.Add(choosen);
            return;


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
        

        // Обработка фильтра
        public void AcceptFilter(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Accept Filter");
            try
            {
                Console.WriteLine(choosenCountryBorders[0].Name.ToString());
                Console.WriteLine(choosenTypeBorders[0].Name.ToString());
                Console.WriteLine(choosenAgeBorders[0].Name.ToString());
                

                


            }
            catch (Exception)
            {
                Console.WriteLine("nth");
            }
            if (choosenCountryBorders.Count > 0 ||
                choosenAgeBorders.Count > 0 ||
                choosenTypeBorders.Count > 0)
            {
                Settings.Default["filter"] = "yes";
            }
            else
            {
                Console.WriteLine("ПУСТОООООЙ");
                Settings.Default["filter"] = "";
            }
            while (choosenCountryBorders.Count != 4)
                choosenCountryBorders.Add(null);
            while (choosenAgeBorders.Count != 4)
                choosenAgeBorders.Add(null);
            while (choosenTypeBorders.Count != 4)
                choosenTypeBorders.Add(null);
            new CashWork().countryAdder(choosenCountryBorders);
            new CashWork().ageAdder(choosenAgeBorders);
            new CashWork().styleAdder(choosenTypeBorders);

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

    }
}
