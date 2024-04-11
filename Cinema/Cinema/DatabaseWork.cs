using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Cinema.Properties;

namespace Cinema
{
    // Класс работы с базой данных
    internal class DatabaseWork
    {
        // Строка подключения к базы данных
        public const string connectionURL = "data source=6.tcp.eu.ngrok.io, 16136;" + // Имя Сервера
            "Database=CinemaShop;" + // Название используемой базы данных
            "User Id=ArseniyKleym;" + // Логин пользователя базы данных
            "Password=arseniy_008;" + // Пароль пользователя базы данных
            "TrustServerCertificate=True;"; // Уровень доверия к серверу
        public int activeID = 1; // Используется для заполнения базы данных и хранит в себе последний id
        public int activePayID = 1;

        // Узнаем последний ID в Пользовательской таблице
        private void takeCurrentID()
        {
            // Используем строку подключения для соединения с БД на сервере
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = @"
                    Use CinemaShop
                    SELECT customer_id
                    FROM Пользователь;
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Перебираем каждый кортеж
                    // Формат обращения к клонке
                    // "" + dr["Имя Колонки"] | Пример ниже
                    while (dr.Read())
                    {
                        // Получаем id
                        string id = "" + dr["customer_id"];
                        // Последнее используемое id + 1 = новое id сразу после последнего
                        // Пример: Последнее = 5, добавляемая строка требует след по счету id
                        // 5 + 1 = 6 - это будет id для новой строки
                        activeID = Convert.ToInt32(id) + 1;
                    }
                    dr.Close();
                }
                conn.Close();
            }

        }

        private void takeCurrentPayID()
        {
            // Используем строку подключения для соединения с БД на сервере
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = @"
                    Use CinemaShop
                    SELECT payment_id
                    FROM Оплата;
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Перебираем каждый кортеж
                    // Формат обращения к клонке
                    // "" + dr["Имя Колонки"] | Пример ниже
                    while (dr.Read())
                    {
                        // Получаем id
                        string id = "" + dr["payment_id"];
                        // Последнее используемое id + 1 = новое id сразу после последнего
                        // Пример: Последнее = 5, добавляемая строка требует след по счету id
                        // 5 + 1 = 6 - это будет id для новой строки
                        activePayID = Convert.ToInt32(id) + 1;
                    }
                    dr.Close();
                }
                conn.Close();
            }

        }

        // Получение информации для заполнении оплаты
        public List<string> payInf(string film_id)
        {
            List<string> returnInf = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = $@"
                    Use CinemaShop
                    SELECT film_name, film_cost
                    FROM Магазин
                    where film_id = {film_id};
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnInf.Add(("" + dr["film_name"]).Trim());
                        returnInf.Add(("" + dr["film_cost"]).Trim());
                    }
                    dr.Close();
                }
                conn.Close();

            }
            return returnInf;
        }


        // Оплата
        public void payInDB(string film_id)
        {
            takeCurrentPayID();
            Console.WriteLine($"{activePayID}, {Settings.Default["CustomerID"]},{film_id}") ;
            using (SqlConnection connection = new SqlConnection(connectionURL))
            {

                connection.Open();
                // Заполняем таблицу оплата
                SqlCommand commandToAddInformationFromTable = new SqlCommand($@"
                Use CinemaShop
                INSERT INTO Оплата
                VALUES({activePayID}, {Settings.Default["CustomerID"]},{film_id})");
                commandToAddInformationFromTable.Connection = connection;
                commandToAddInformationFromTable.ExecuteNonQuery();
            }
        }

        // Проверка что нет почт, совпадающих с той что пытаются зарегистрировать
        // Авторизация будет производиться по почте и паролю - почта не должна повторяться в бд
        public bool checkEmailTwice(string emailToCheck)
        {
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = @"
                    Use CinemaShop
                    SELECT customer_email
                    FROM Пользователь;
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // Получаем почту
                        // Используем TRIM() чтобы убрать лишние пробелы после текста (ОНИ ТАМ ЕСТЬ)
                        string customerEmail = ("" + dr["customer_email"]).Trim();

                        // если почты совпадают
                        if (customerEmail == emailToCheck)
                            return false;
                    }
                    dr.Close();
                }
                conn.Close();
            }
            return true;
        }

        // Получение информации про фильмы по поиску
        public Dictionary<string, Dictionary<string, string>> getSearchFilmsInf(string userSearchMessage)
        {
            Dictionary<string, Dictionary<string, string>> returnFilmsInf = new Dictionary<string, Dictionary<string, string>>();

            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = $@"
                    Use CinemaShop
                    SELECT * 
                    FROM Магазин
                    Where film_name = N'{userSearchMessage}';
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Dictionary<string, string> oneFilmInformation = new Dictionary<string, string>()
                        {
                            {"Название", ""}, {"Цена", ""}, {"Фото", ""}, {"Страна", ""}, {"Время", ""}, {"Жанр", ""}, {"Категория", ""},
                            {"Возраст", ""}, {"Рейтинг", ""}
                        };
                        // сначала в обычный словарь
                        oneFilmInformation["Цена"] = ("" + dr["film_cost"]).Trim();
                        oneFilmInformation["Фото"] = ("" + dr["film_picture"]).Trim();
                        oneFilmInformation["Название"] = ("" + dr["film_name"]).Trim();
                        oneFilmInformation["Страна"] = ("" + dr["film_country"]).Trim();
                        oneFilmInformation["Время"] = ("" + dr["film_duration"]).Trim();
                        oneFilmInformation["Жанр"] = ("" + dr["film_style"]).Trim();
                        oneFilmInformation["Категория"] = ("" + dr["film_category"]).Trim();
                        oneFilmInformation["Возраст"] = ("" + dr["film_age_control"]).Trim();
                        oneFilmInformation["Рейтинг"] = ("" + dr["film_rating"]).Trim();
                        // теперь в основной
                        returnFilmsInf[("" + dr["film_id"]).Trim()] = oneFilmInformation;

                    }
                    dr.Close();
                }
                conn.Close();
            }
            return returnFilmsInf;
        }


        // Получение информации про фильмы
        public Dictionary<string, Dictionary<string, string>> getFilmsInfWithOutFilterAndSearch()
        {
            Dictionary<string, Dictionary<string, string>> returnFilmsInf = new Dictionary<string, Dictionary<string, string>>();
            
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = @"
                    Use CinemaShop
                    SELECT * 
                    FROM Магазин;
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Dictionary<string, string> oneFilmInformation = new Dictionary<string, string>()
                        {
                            {"Название", ""}, {"Цена", ""}, {"Фото", ""}, {"Страна", ""}, {"Время", ""}, {"Жанр", ""}, {"Категория", ""},
                            {"Возраст", ""}, {"Рейтинг", ""}
                        };
                        // сначала в обычный словарь
                        oneFilmInformation["Цена"] = ("" + dr["film_cost"]).Trim();
                        oneFilmInformation["Фото"] = ("" + dr["film_picture"]).Trim();
                        oneFilmInformation["Название"] = ("" + dr["film_name"]).Trim();
                        oneFilmInformation["Страна"] = ("" + dr["film_country"]).Trim();
                        oneFilmInformation["Время"] = ("" + dr["film_duration"]).Trim();
                        oneFilmInformation["Жанр"] = ("" + dr["film_style"]).Trim();
                        oneFilmInformation["Категория"] = ("" + dr["film_category"]).Trim();
                        oneFilmInformation["Возраст"] = ("" + dr["film_age_control"]).Trim();
                        oneFilmInformation["Рейтинг"] = ("" + dr["film_rating"]).Trim();
                        // теперь в основной
                        returnFilmsInf[("" + dr["film_id"]).Trim()] = oneFilmInformation;

                    }
                    dr.Close();
                }
                conn.Close();
            }
            return returnFilmsInf;
        }

        // Проверка почты и пароля при авторизации пользователя
        public bool checkCustomerAccDataToSighUp(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = $@"
                    Use CinemaShop
                    SELECT *
                    FROM Пользователь
                    WHERE customer_email = N'{email}'
                        AND
                        customer_password = N'{password}';
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // если найдена хоть 1 запись - пользователь существует и его можно авторизовать
                        // Записываем его ID в КЭШ
                        new CashWork().setCustomerId(("" + dr["customer_id"]).Trim());
                        return true;
                    }
                    dr.Close();
                }
                conn.Close();
            }
            return false;
        }

        // Поиск по фильтру
        public Dictionary<string, Dictionary<string, string>> getFilterFilmsInf()
        {
            Settings.Default["filter"] = null;
            Settings.Default.Save();
            
            Dictionary<string, Dictionary<string, string>> returnFilmsInf = new Dictionary<string, Dictionary<string, string>>();
            Console.WriteLine();
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = $@"
                    Use CinemaShop
                    SELECT * 
                    FROM Магазин
WHERE 
    film_country in (
    
        select film_country
        from Магазин
        where film_country = N'{Settings.Default["Country1"].ToString().Trim()}'
        or
        film_country = N'{Settings.Default["Country2"].ToString().Trim()}'
        or
        film_country = N'{Settings.Default["Country3"].ToString().Trim()}'
        or
        film_country = N'{Settings.Default["Country4"].ToString().Trim()}')

and film_age_control in (
    
        select film_age_control
	    from Магазин
        where film_age_control = {Settings.Default["Age1"].ToString().Trim()}
        or
        film_age_control = {Settings.Default["Age2"].ToString().Trim()}
        or
        film_age_control = {Settings.Default["Age3"].ToString().Trim()}
        or
        film_age_control = {Settings.Default["Age4"].ToString().Trim()})

and film_style in (
    
        select film_style
	    from Магазин
        where film_style = N'{Settings.Default["Style1"].ToString().Trim()}'
        or
        film_style = N'{Settings.Default["Style2"].ToString().Trim()}'
        or
        film_style = N'{Settings.Default["Style3"].ToString().Trim()}'
        or
        film_style = N'{Settings.Default["Style4"].ToString().Trim()}')
                    ";

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Console.WriteLine(123);
                        Dictionary<string, string> oneFilmInformation = new Dictionary<string, string>()
                        {
                            {"Название", ""}, {"Цена", ""}, {"Фото", ""}, {"Страна", ""}, {"Время", ""}, {"Жанр", ""}, {"Категория", ""},
                            {"Возраст", ""}, {"Рейтинг", ""}
                        };
                        // сначала в обычный словарь
                        oneFilmInformation["Цена"] = ("" + dr["film_cost"]).Trim();
                        oneFilmInformation["Фото"] = ("" + dr["film_picture"]).Trim();
                        oneFilmInformation["Название"] = ("" + dr["film_name"]).Trim();
                        oneFilmInformation["Страна"] = ("" + dr["film_country"]).Trim();
                        oneFilmInformation["Время"] = ("" + dr["film_duration"]).Trim();
                        oneFilmInformation["Жанр"] = ("" + dr["film_style"]).Trim();
                        oneFilmInformation["Категория"] = ("" + dr["film_category"]).Trim();
                        oneFilmInformation["Возраст"] = ("" + dr["film_age_control"]).Trim();
                        oneFilmInformation["Рейтинг"] = ("" + dr["film_rating"]).Trim();
                        // теперь в основной
                        returnFilmsInf[("" + dr["film_id"]).Trim()] = oneFilmInformation;

                    }
                    dr.Close();
                }
                conn.Close();
            }
            return returnFilmsInf;
        }


        // Получение полной информации про пользователя для заполнения окна аккаунта
        public Dictionary<string, string> takeCustomerAccountInformation()
        {
            Dictionary<string, string> returnCustomerInformation = new Dictionary<string, string>()
            {
                {"Имя", ""}, {"Пароль", ""}, {"Почта", ""}, {"Возраст", ""}
            };
            Console.WriteLine(Settings.Default["CustomerID"].ToString());
            using (SqlConnection conn = new SqlConnection(connectionURL))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // Создаем команду как в T-SQL
                    cmd.CommandText = $@"
                    Use CinemaShop
                    SELECT *
                    FROM Пользователь
                    WHERE customer_id = {Settings.Default["CustomerID"].ToString()};
                    "; // Используем КЕШ для получения ID у активного пользователя

                    // Получаем ответ на запрос и разбираем его по кортежам и колонкам
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // Получаем почту
                        // Используем TRIM() чтобы убрать лишние пробелы после текста (ОНИ ТАМ ЕСТЬ)
                        returnCustomerInformation["Имя"] = ("" + dr["customer_login"]).Trim();
                        returnCustomerInformation["Пароль"] = ("" + dr["customer_password"]).Trim();
                        returnCustomerInformation["Почта"] = ("" + dr["customer_email"]).Trim();
                        returnCustomerInformation["Возраст"] = ("" + dr["customer_age"]).Trim();
                    }
                    dr.Close();
                }
                conn.Close();
            }

            return returnCustomerInformation;
        }

        // Регистрация пользователя в базе данных
        public void customerRegistration(Dictionary<string, string> setInf)
        {
            // обновляем значение ID для новой записи
            takeCurrentID();
            using (SqlConnection connection = new SqlConnection(connectionURL))
            {

                connection.Open();
                // Заполняем таблицу пользователь
                // Передаем данные из полученного словаря, выбирая их по ключу в [""]
                SqlCommand commandToAddInformationFromTable = new SqlCommand($@"
                Use CinemaShop
                INSERT INTO Пользователь
                VALUES({activeID}, N'{setInf["логин"]}',
                        N'{setInf["пароль"]}', N'{setInf["почта"]}',
                        {setInf["возраст"]})");
                commandToAddInformationFromTable.Connection = connection;
                commandToAddInformationFromTable.ExecuteNonQuery();
            }

            // После регистрации определяем ID авторизованного пользователя в КЕШ программы
            // Передаем на запись ID пользователя
            new CashWork().setCustomerId(Convert.ToString(activeID));
        }
    }
}
