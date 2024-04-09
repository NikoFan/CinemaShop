using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
