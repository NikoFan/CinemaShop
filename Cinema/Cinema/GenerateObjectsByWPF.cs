using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cinema
{
    // Класс для создания объектов WPF
    internal class GenerateObjectsByWPF
    {
        public TextBlock generateTitleFilmName(string filmName)
        {
            return new TextBlock
            {
                Text = filmName,
                FontSize = 20,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Padding = new System.Windows.Thickness(0,0,10,0),
            };
        }

        private void cardColorChange_MD(Border sender) => sender.Background = new SolidColorBrush(Colors.DarkBlue);


        private void cardColorChange_ME(Border sender) => sender.Background = new SolidColorBrush(Colors.Purple);

        private void cardColorChange_ML(Border sender) => sender.Background = new SolidColorBrush(Colors.MediumPurple);

        public Border generateFilmCard( string borderName)
        {
            Border card = new Border
            {
                Name = borderName,
                Height = 150,
                Background = new SolidColorBrush(Colors.MediumPurple),
                Cursor = Cursors.Hand
            };

            card.MouseDown += (sender, args) =>
            {
                cardColorChange_MD(card);
            };

            card.MouseUp += (sender, args) =>
            {
                cardColorChange_ME(card);
            };

            card.MouseEnter += (sender, args) =>
            {
                cardColorChange_ME(card);
            };

            card.MouseLeave += (sender, args) =>
            {
                cardColorChange_ML(card);
            };

            return card;


        }
    }
}
