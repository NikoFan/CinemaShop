using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Cinema.Properties; // Вызываем инструмент работы с КЭШем программы

namespace Cinema
{
    // Класс для работы с КЕШем программы
    internal class CashWork
    {
        public void setCustomerId(string customerIdToSet)
        {
            // Устанавливаем значение в КЕШ
            Settings.Default["CustomerID"] = customerIdToSet;
            // Сохраняем значение
            Settings.Default.Save();
        }

        public void countryAdder(List<Border> country)
        {
            bool check = false;
            foreach (var item in country)
            {
                if (item != null)
                    check = true; break;
            }
            if (!check)
            {
                Settings.Default["Country1"] = "Россия"; Settings.Default.Save();
                Settings.Default["Country2"] = "США"; Settings.Default.Save();
                Settings.Default["Country3"] = "Франция"; Settings.Default.Save();
                Settings.Default["Country4"] = "Турция";
                Settings.Default.Save();
                return;
            }
            Settings.Default["Country1"] = country[0] != null ? country[0].Name.ToString() : "-"; Settings.Default.Save();
            Settings.Default["Country2"] = country[1] != null ? country[1].Name.ToString() : "-"; Settings.Default.Save();
            Settings.Default["Country3"] = country[2] != null ? country[2].Name.ToString() : "-"; Settings.Default.Save();
            Settings.Default["Country4"] = country[3] != null ? country[3].Name.ToString() : "-";
            Settings.Default.Save();
        }

        public void ageAdder(List<Border> age)
        {
            bool check = false;
            foreach (var item in age)
            {
                if (item != null)
                    check = true; break;
            }
            if (!check)
            {
                Settings.Default["Age1"] = "6"; Settings.Default.Save();
                Settings.Default["Age2"] = "10"; Settings.Default.Save();
                Settings.Default["Age3"] = "16"; Settings.Default.Save();
                Settings.Default["Age4"] = "18";
                Settings.Default.Save();
                return;
            }
            Settings.Default["Age1"] = age[0] != null ? age[0].Name.ToString().Split('_')[1] : "1"; Settings.Default.Save();
            Settings.Default["Age2"] = age[1] != null ? age[1].Name.ToString().Split('_')[1] : "1"; Settings.Default.Save();
            Settings.Default["Age3"] = age[2] != null ? age[2].Name.ToString().Split('_')[1] : "1"; Settings.Default.Save();
            Settings.Default["Age4"] = age[3] != null ? age[3].Name.ToString().Split('_')[1] : "1";
            Settings.Default.Save();
        }

        public void styleAdder(List<Border> style)
        {
            bool check = false;
            foreach (var item in style)
            {
                if (item != null)
                    check = true; break;
            }
            if (!check)
            {
                Settings.Default["Style1"] = "Комедия"; Settings.Default.Save();
                Settings.Default["Style2"] = "Хоррор"; Settings.Default.Save();
                Settings.Default["Style3"] = "Любовь"; Settings.Default.Save();
                Settings.Default["Style4"] = "Экшен";
                Settings.Default.Save();
                return;
            }
            Settings.Default["Style1"] = style[0] != null ? style[0].Name.ToString() : "-"; Settings.Default.Save();
            Settings.Default["Style2"] = style[1] != null ? style[1].Name.ToString() : "-"; Settings.Default.Save();
            Settings.Default["Style3"] = style[2] != null ? style[2].Name.ToString() : "-"; Settings.Default.Save();
            Settings.Default["Style4"] = style[3] != null ? style[3].Name.ToString() : "-";
            Settings.Default.Save();
        }
    }
}
