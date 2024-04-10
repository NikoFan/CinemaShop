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
using Cinema.Properties;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            completeFilmCards(null);
        }



        private void completeFilmCards(string searchingFilmName)
        {
            // Очищаем старую информацию из области перед заполнением
            FilmShower.Children.Clear();
            
            
            
            // Если это подборка фильмов без фильтраg, 
            if (searchingFilmName == null )
            {
                // Формат : НазваниеФильма - {Фото:Фото.png; Цена:Цена руб;} 
                // Словарь куда будет приходить информация про фильмы
                Dictionary<string, Dictionary<string, string>> filmsInf = new DatabaseWork().getFilmsInfWithOutFilterAndSearch();
                GenerateObjectsByWPF wpfGenerate = new GenerateObjectsByWPF();
                foreach(var filmID in  filmsInf)
                {
                    Border filmCard = wpfGenerate.generateFilmCard("_" + filmID.Key);
                    Grid grid = new Grid();
                    foreach(var filmInformation in filmID.Value)
                    {
                        // Если ключ внутреннего словаря
                        switch(filmInformation.Key)
                        {
                            case ("Название"):
                                // Добавляем объект в grid
                                grid.Children.Add(wpfGenerate.generateTitleFilmName(filmInformation.Value));
                                break;
                            case ("Цена"):
                                break;
                            case ("Фото"):
                                break;
                        }
                    }
                    // добавляем grid на карточку фильма
                    filmCard.Child = grid;
                    // Добавляем карточку в общий список
                    FilmShower.Children.Add(filmCard);
                }
            }
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

        // Скрытие подсказки
        public void hideHintText(object sender, RoutedEventArgs e)
        {
            TextBlock hintText = (sender as TextBlock);
            hintText.Visibility = Visibility.Hidden;
        }

        public void makeTextBoxActive(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focusable = true;
        }

        // Показ подсказки
        public void hintTextVisible(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.ToString().Length == 0)
            {
                SearchHintText.Visibility = Visibility.Visible;
                (sender as TextBox).Focusable = false;
            }
                
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
            if (Settings.Default["CustomerID"].ToString() == "nth")
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

        // Найти фильм в строке
        public void searchNewFilm(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ПОИСК");
            Console.WriteLine(SearchTextBox.Text.ToString());
        }


        // Указать фильтр
        public void openFilter(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ФИЛЬТР");
            FilterWindow filterWindow = new FilterWindow()
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = Left,
                Top = Top
            };

            this.Visibility = Visibility.Collapsed;
            filterWindow.Show();
        }
    }
}
