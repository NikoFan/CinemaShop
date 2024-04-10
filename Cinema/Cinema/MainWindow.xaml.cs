using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            
            if (Settings.Default["filter"].ToString() != "")
            {
                Console.WriteLine("--" + Settings.Default["filter"].ToString());
                
                completeFilmCardsFilter();
            }
                
            else
            {
                Console.WriteLine("-+" + Settings.Default["filter"].ToString());
                completeFilmCards(null);
            }
                
        }

        // Заполнение с фильтром
        private void completeFilmCardsFilter()
        {
            Console.WriteLine("-+" + Settings.Default["filter"].ToString());
            // Очищаем старую информацию из области перед заполнением
            FilmShower.Children.Clear();
            // Формат : НазваниеФильма - {Фото:Фото.png; Цена:Цена руб;} 
            // Словарь куда будет приходить информация про фильмы
            Dictionary<string, Dictionary<string, string>> filmsInf = new Dictionary<string, Dictionary<string, string>>();


            filmsInf = new DatabaseWork().getFilterFilmsInf();




            GenerateObjectsByWPF wpfGenerate = new GenerateObjectsByWPF();
            foreach (var filmID in filmsInf)
            {
                Border filmCard = wpfGenerate.generateFilmCard("_" + filmID.Key);
                // Создание панели для размещения текста
                Grid grid = new Grid();
                StackPanel InfPanel = new StackPanel();
                InfPanel.Width = 600;
                InfPanel.HorizontalAlignment = HorizontalAlignment.Right;
                foreach (var filmInformation in filmID.Value)
                {
                    // Если ключ внутреннего словаря
                    switch (filmInformation.Key)
                    {
                        case ("Название"):
                            // Добавляем объект в grid
                            InfPanel.Children.Add(wpfGenerate.generateTitleFilmName(filmInformation.Value));
                            break;
                        case ("Цена"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Цена: " + filmInformation.Value + "р."));
                            break;
                        case ("Страна"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Страна: " + filmInformation.Value));
                            break;
                        case ("Время"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Длительность: " + filmInformation.Value));
                            break;
                        case ("Жанр"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Жанр: " + filmInformation.Value));
                            break;
                        case ("Категория"):
                            InfPanel.Children.Add(wpfGenerate.generateRatingText("Рейтинг: " + filmInformation.Value));
                            break;
                        case ("Возраст"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Возраст: " + filmInformation.Value + "+"));
                            break;
                        case ("Рейтинг"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Оценки: " + filmInformation.Value + "/10"));
                            break;
                        case ("Фото"):
                            // Путь к фото
                            string computerFoldersPATH = Convert.ToString(String.Join("\\", Environment.CurrentDirectory.ToString().Split('\\').Take(
                                                        Environment.CurrentDirectory.ToString().Split('\\').Length - 2))) + "/Images/";
                            BitmapImage img
                            = new BitmapImage(
                                            new Uri($@"{computerFoldersPATH}{filmInformation.Value}"));
                            Image image = new Image()
                            {
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(10, 0, 0, 0),
                                Height = 250,
                                Width = 150
                            };
                            image.Source = img;
                            grid.Children.Add(image);
                            break;
                    }
                }
                // добавляем grid на карточку фильма
                grid.Children.Add(InfPanel);

                filmCard.Child = grid;
                // Добавляем карточку в общий список
                FilmShower.Children.Add(filmCard);
            }
        }




        // Заполнение без фильтра
        private void completeFilmCards(string searchingFilmName)
        {
            // Очищаем старую информацию из области перед заполнением
            FilmShower.Children.Clear();
            // Формат : НазваниеФильма - {Фото:Фото.png; Цена:Цена руб;} 
            // Словарь куда будет приходить информация про фильмы
            Dictionary<string, Dictionary<string, string>> filmsInf = new Dictionary<string, Dictionary<string, string>>();


            // Если это подборка фильмов без фильтра или не по поиску 
            if (searchingFilmName == null )
                filmsInf = new DatabaseWork().getFilmsInfWithOutFilterAndSearch();
                
            
            else if (searchingFilmName != null)
                filmsInf = new DatabaseWork().getSearchFilmsInf(searchingFilmName);
            



            GenerateObjectsByWPF wpfGenerate = new GenerateObjectsByWPF();
            foreach (var filmID in filmsInf)
            {
                Border filmCard = wpfGenerate.generateFilmCard("_" + filmID.Key);
                // Создание панели для размещения текста
                Grid grid = new Grid();
                StackPanel InfPanel = new StackPanel();
                InfPanel.Width = 600;
                InfPanel.HorizontalAlignment = HorizontalAlignment.Right;
                foreach (var filmInformation in filmID.Value)
                {
                    // Если ключ внутреннего словаря
                    switch (filmInformation.Key)
                    {
                        case ("Название"):
                            // Добавляем объект в grid
                            InfPanel.Children.Add(wpfGenerate.generateTitleFilmName(filmInformation.Value));
                            break;
                        case ("Цена"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Цена: " + filmInformation.Value + "р."));
                            break;
                        case ("Страна"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Страна: " + filmInformation.Value));
                            break;
                        case ("Время"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Длительность: " + filmInformation.Value));
                            break;
                        case ("Жанр"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Жанр: " + filmInformation.Value));
                            break;
                        case ("Категория"):
                            InfPanel.Children.Add(wpfGenerate.generateRatingText("Рейтинг: " + filmInformation.Value));
                            break;
                        case ("Возраст"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Возраст: " + filmInformation.Value + "+"));
                            break;
                        case ("Рейтинг"):
                            InfPanel.Children.Add(wpfGenerate.generateText("Оценки: " + filmInformation.Value + "/10"));
                            break;
                        case ("Фото"):
                            // Путь к фото
                            string computerFoldersPATH = Convert.ToString(String.Join("\\", Environment.CurrentDirectory.ToString().Split('\\').Take(
                                                        Environment.CurrentDirectory.ToString().Split('\\').Length - 2))) + "/Images/";
                            BitmapImage img
                            = new BitmapImage(
                                            new Uri($@"{computerFoldersPATH}{filmInformation.Value}"));
                            Image image = new Image()
                            {
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(10, 0, 0, 0),
                                Height = 250,
                                Width = 150
                            };
                            image.Source = img;
                            grid.Children.Add(image);
                            break;
                    }
                }
                // добавляем grid на карточку фильма
                grid.Children.Add(InfPanel);

                filmCard.Child = grid;
                // Добавляем карточку в общий список
                FilmShower.Children.Add(filmCard);
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

        // Найти фильм в строке
        public void searchNewFilm(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ПОИСК");
            if (SearchTextBox.Text.ToString().Trim().Length != 0)
                completeFilmCards(SearchTextBox.Text.ToString());
            else
                completeFilmCards(null);
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
