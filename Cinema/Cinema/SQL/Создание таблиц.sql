USE CinemaShop


-- Создание таблицы пользователя
CREATE TABLE Пользователь(
-- id пользователя
customer_id INT PRIMARY KEY NOT NULL,
-- логин пользователя
customer_login NCHAR(50) NOT NULL,
-- пароль
customer_password NCHAR(20) NOT NULL,
-- эл. почта
customer_email NCHAR(50) NOT NULL,
-- возраст
customer_age INT NOT NULL
)

-- Таблица магазина
CREATE TABLE Магазин(
-- Инд. номер
film_id INT PRIMARY KEY NOT NULL,
-- название
film_name NCHAR(100) NOT NULL,
-- продолжительность формат записи : 1 час 20 минут 30 секунд
film_duration NCHAR(40) NOT NULL,
-- постер для карточки формат записи : Алладин.png
film_picture NCHAR(50) NOT NULL,
-- Жанр фильма КАПСОМ
film_style NCHAR(20) NOT NULL,
-- Категоря фильма КАПСОМ
film_category NCHAR(20) NOT NULL,
-- Рейтинг фильма
film_rating INT NOT NULL,
-- Возрастное ограничение формат записи : 18 >> 18 лет
film_age_control INT NOT NULL,
-- цена на фильм формат записи : 1400 >> 1400 руб
film_cost INT NOT NULL,
-- Страна производитель КАПСОМ
film_country NCHAR(30) NOT NULL,
-- файл с фильмом (видео на пару сек) формат записи : Алладин.mp4...
film_file NCHAR(50)
)

-- создание таблицы оплаты
CREATE TABLE Оплата(
payment_id INT PRIMARY KEY NOT NULL,
-- id пользователя
customer_payments_id INT NOT NULL,
-- Инд. номер
film_payments_id INT NOT NULL,

-- Вторичные ключи для таблиц Пользователя и Магазина
FOREIGN KEY (customer_payments_id) REFERENCES Пользователь (customer_id),
FOREIGN KEY (film_payments_id) REFERENCES Магазин (film_id),
)