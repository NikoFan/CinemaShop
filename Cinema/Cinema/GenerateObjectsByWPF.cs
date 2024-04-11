using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media.Imaging;


namespace Cinema
{
    // Класс для создания объектов WPF
    internal class GenerateObjectsByWPF
    {
        // Абсолютный путь к фото
        public string computerFoldersPATH = Convert.ToString(String.Join("\\", Environment.CurrentDirectory.ToString().Split('\\').Take(
            Environment.CurrentDirectory.ToString().Split('\\').Length - 2))) + "/Images/";

        // Создается ценник фильма
        public TextBlock generateRatingText(string putText)
        {
            return new TextBlock
            {
                Text = putText,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 20,
                Background = new SolidColorBrush(Colors.GreenYellow),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Margin = new System.Windows.Thickness(0, 0, 20, 0),
            };
        }

        // Создается ценник фильма
        public TextBlock generateText(string putText)
        {
            return new TextBlock
            {
                Text = putText,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 20,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Margin = new System.Windows.Thickness(0, 5, 0, 0),
                Padding = new System.Windows.Thickness(0, 0, 20, 0),
            };
        }
        // Создание текстового поля с названием
        public TextBlock generateTitleFilmName(string filmName)
        {
            return new TextBlock
            {
                Text = filmName,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Padding = new System.Windows.Thickness(0,0,10,0),
            };
        }

        private void cardColorChange_MD(Border sender) => sender.Background = (SolidColorBrush) new BrushConverter().ConvertFromString("#AE89C8");

        private void cardColorChange_ME(Border sender) => sender.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#955FB9");

        private void cardColorChange_ML(Border sender) => sender.Background = new SolidColorBrush(Colors.MediumPurple);

        // Создание карточки товара
        public Border generateFilmCard( string borderName)
        {
            Border card = new Border
            {
                Name = borderName,
                Height = 280,
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
