﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BookingProject.View.CustomMessageBoxes
{
    public class CustomNotificationMessageBoxNewlyCreatedTours
    {
        public void ShowCustomNotification(string messageText, Model.User userGuest)
        {
            Window customNotification = new Window
            {
                Title = "Notification",
                FontWeight = FontWeights.Bold,
                Height = 200, // Adjust the height to make it smaller
                Width = 300, // Adjust the width to make it smaller
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.Gray,
                WindowStartupLocation = WindowStartupLocation.CenterScreen, // Display in the middle of the screen
                AllowsTransparency = true,
            };

            TextBlock message = new TextBlock
            {
                Text = messageText,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20, 20, 20, 10) // Adjust the margin to bring it closer to the text
            };

            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0) // Adjust the margin to bring it closer to the text
            };

            Button okButton = new Button
            {
                Content = "OK",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 0, 10, 0),
                FontWeight = FontWeights.Bold,
                Background = Brushes.LightBlue,
                Foreground = Brushes.White,
                Template = GetRoundedButtonTemplate()
            };
            okButton.Click += (o, args) =>
            {
                customNotification.Close();
            };

            buttonPanel.Children.Add(okButton);

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            stackPanel.Children.Add(message);
            stackPanel.Children.Add(buttonPanel);

            customNotification.Content = stackPanel;
            customNotification.ShowDialog();

            ControlTemplate GetRoundedButtonTemplate()
            {
                ControlTemplate template = new ControlTemplate(typeof(Button));
                FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
                borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
                borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
                FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center); // Align content in the center
                contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center); // Align content in the center
                borderFactory.AppendChild(contentPresenterFactory);
                template.VisualTree = borderFactory;

                // Define trigger to change the button color when pressed
                Trigger pressedTrigger = new Trigger()
                {
                    Property = Button.IsPressedProperty,
                    Value = true
                };
                pressedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightGray));
                template.Triggers.Add(pressedTrigger);

                return template;
            }
        }
    }
}