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


        // Получение полной информации про пользователя для заполнения окна аккаунта
        public Dictionary<string, string> takeCustomerAccountInformation()
        {
            Dictionary<string, string> returnCustomerInformation = new Dictionary<string, string>()
            {
                {"Имя", ""}, {"Пароль", ""}, {"Почта", ""}, {"Возраст", ""}
            };

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
