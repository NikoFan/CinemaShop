USE CinemaShop


-- �������� ������� ������������
CREATE TABLE ������������(
-- id ������������
customer_id INT PRIMARY KEY NOT NULL,
-- ����� ������������
customer_login NCHAR(50) NOT NULL,
-- ������
customer_password NCHAR(20) NOT NULL,
-- ��. �����
customer_email NCHAR(50) NOT NULL,
-- �������
customer_age INT NOT NULL
)

-- ������� ��������
CREATE TABLE �������(
-- ���. �����
film_id INT PRIMARY KEY NOT NULL,
-- ��������
film_name NCHAR(100) NOT NULL,
-- ����������������� ������ ������ : 1 ��� 20 ����� 30 ������
film_duration NCHAR(40) NOT NULL,
-- ������ ��� �������� ������ ������ : �������.png
film_picture NCHAR(50) NOT NULL,
-- ���� ������ ������
film_style NCHAR(20) NOT NULL,
-- �������� ������ ������
film_category NCHAR(20) NOT NULL,
-- ������� ������
film_rating INT NOT NULL,
-- ���������� ����������� ������ ������ : 18 >> 18 ���
film_age_control INT NOT NULL,
-- ���� �� ����� ������ ������ : 1400 >> 1400 ���
film_cost INT NOT NULL,
-- ������ ������������� ������
film_country NCHAR(30) NOT NULL,
-- ���� � ������� (����� �� ���� ���) ������ ������ : �������.mp4...
film_file NCHAR(50)
)

-- �������� ������� ������
CREATE TABLE ������(
payment_id INT PRIMARY KEY NOT NULL,
-- id ������������
customer_payments_id INT NOT NULL,
-- ���. �����
film_payments_id INT NOT NULL,

-- ��������� ����� ��� ������ ������������ � ��������
FOREIGN KEY (customer_payments_id) REFERENCES ������������ (customer_id),
FOREIGN KEY (film_payments_id) REFERENCES ������� (film_id),
)