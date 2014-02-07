using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BinDecHexOct.Resources;

namespace BinDecHexOct
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Method to handle number button presses
        private void numberButtonHandler(object sender, RoutedEventArgs e)
        {
            if (button0.IsFocused)
                Input.Text += "0";
            else if (button1.IsFocused)
                Input.Text += "1";
            else if (button2.IsFocused)
                Input.Text += "2";
            else if (button3.IsFocused)
                Input.Text += "3";
            else if (button4.IsFocused)
                Input.Text += "4";
            else if (button5.IsFocused)
                Input.Text += "5";
            else if (button6.IsFocused)
                Input.Text += "6";
            else if (button7.IsFocused)
                Input.Text += "7";
            else if (button8.IsFocused)
                Input.Text += "8";
            else if (button9.IsFocused)
                Input.Text += "9";
        }

        // Method to handle letter button presses
        private void letterButtonHandler(object sender, RoutedEventArgs e)
        {
            if (buttonA.IsFocused)
                Input.Text += "A";
            else if (buttonB.IsFocused)
                Input.Text += "B";
            else if (buttonC.IsFocused)
                Input.Text += "C";
            else if (buttonD.IsFocused)
                Input.Text += "D";
            else if (buttonE.IsFocused)
                Input.Text += "E";
            else if (buttonF.IsFocused)
                Input.Text += "F";
        }

        // Method to handle a single of the del/clear button and the radio buttons
        private void delClearSingleTap(object sender, RoutedEventArgs e)
        {
            if (buttonDelClear.IsFocused)
            {
                if (Input.Text.Length <= 1)
                    Input.Text = "";
                else
                {
                    var inputMinusLastChar = Input.Text.Substring(0, Input.Text.Length - 1);
                    Input.Text = inputMinusLastChar;
                }
            }
        }

        // Clear the input and output boxes
        private void delClearDoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Input.Text = "";
            Output.Text = "";
        }


        // Check to see if the given object is a string representation of a binary number
        private Boolean isBin(String input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                if (!(current >= '0' && current <= '1'))
                    return false;
            }
            return true;
        }

        // Is the given string a hexadecimal?
        private Boolean isHex(String input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                if (!input.All(c => "0123456789abcdefABCDEF\n".Contains(c)))
                    return false;
            }
            return true;
        }

        // Is the given string an octal?
        private Boolean isOct(String input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                if ((Char.IsLetter(current)) || (!(current >= '0' && current <= '7')))
                    return false;
            }
            return true;
        }

        // Is the given string a base-2 decimal?
        private Boolean isDec(String input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                if (Char.IsLetter(current))
                    return false;
            }
            return true;
        }

        // Convert the given string to the given base
        private String convertToBase(String input, int fromBase, int toBase)
        {
            var intResult = Convert.ToInt64(input, fromBase);
            String strResult = Convert.ToString(intResult, toBase);
            return strResult;
        }

        // Do calculations for conversions
        private void doCalculations(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var userInput = Input.Text;
            if (userInput == "")
                MessageBox.Show("Invalid input. Either the input field is empty or you have not chosen a desired input or output.");
            else if (binButtonInput.IsChecked == true && binButtonOutput.IsChecked == true)
            {
                if (isBin(userInput) && (userInput.Length <= 63))
                    Output.Text = userInput;
                else
                    MessageBox.Show("Please enter a valid binary number (max number of characters is 63).");
            }
            else if (decButtonInput.IsChecked == true && decButtonOutput.IsChecked == true)
            {
                if (isDec(userInput) && (userInput.Length <= 19))
                    Output.Text = userInput;
                else
                    MessageBox.Show("Please enter a valid decimal number between 0 - 9999999999999999999.");
            }
            else if (hexButtonInput.IsChecked == true && hexButtonOutput.IsChecked == true)
            {
                if (isHex(userInput) && (userInput.Length <= 15))
                    Output.Text = userInput;
                else
                    MessageBox.Show("Please enter a valid hexadecimal number between 0 - FFFFFFFFFFFFFFF.");
            }
            else if (octButtonInput.IsChecked == true && octButtonOutput.IsChecked == true)
            {
                if (isOct(userInput) && (userInput.Length <= 21))
                    Output.Text = userInput;
                else
                    MessageBox.Show("Please enter a valid octal number between 0 - 777777777777777777777.");
            }
            else if (binButtonInput.IsChecked == true && decButtonOutput.IsChecked == true) // Bin -> Dec
            {
                if (!(isBin(userInput)) || (userInput.Length > 63))
                    MessageBox.Show("Please enter a valid binary number (max number of characters is 63).");
                else
                    Output.Text = convertToBase(userInput, 2, 10);
            }
            else if (binButtonInput.IsChecked == true && hexButtonOutput.IsChecked == true) // Bin -> Hex
            {
                if (!(isBin(userInput)) || (userInput.Length > 63))
                    MessageBox.Show("Please enter a valid binary number (max number of characters is 63).");
                else
                    Output.Text = convertToBase(userInput, 2, 16);
            }
            else if (binButtonInput.IsChecked == true && octButtonOutput.IsChecked == true) // Bin -> Oct
            {
                if (!(isBin(userInput)) || (userInput.Length > 63))
                    MessageBox.Show("Please enter a valid binary number (max number of characters is 63).");
                else
                    Output.Text = convertToBase(userInput, 2, 8);
            }
            else if (decButtonInput.IsChecked == true && binButtonOutput.IsChecked == true) // Dec -> Bin
            {
                if (!(isDec(userInput)) || (userInput.Length > 19))
                    MessageBox.Show("Please enter a valid decimal number between 0 - 9999999999999999999.");
                else
                    Output.Text = convertToBase(userInput, 10, 2);
            }
            else if (decButtonInput.IsChecked == true && hexButtonOutput.IsChecked == true) // Dec -> Hex
            {
                if (!(isDec(userInput)) || (userInput.Length > 19))
                    MessageBox.Show("Please enter a valid decimal number between 0 - 9999999999999999999.");
                else
                    Output.Text = convertToBase(userInput, 10, 16);
            }
            else if (decButtonInput.IsChecked == true && octButtonOutput.IsChecked == true) // Dec -> Oct
            {
                if (!(isDec(userInput)) || (userInput.Length > 19))
                    MessageBox.Show("Please enter a valid decimal number between 0 - 9999999999999999999.");
                else
                    Output.Text = convertToBase(userInput, 10, 8);
            }
            else if (hexButtonInput.IsChecked == true && binButtonOutput.IsChecked == true) // Hex -> Bin
            {
                if (!(isHex(userInput)) || (userInput.Length > 15))
                    MessageBox.Show("Please enter a valid hexadecimal number between 0 - FFFFFFFFFFFFFFF.");
                else
                    Output.Text = convertToBase(userInput, 16, 2);
            }
            else if (hexButtonInput.IsChecked == true && decButtonOutput.IsChecked == true) // Hex -> Dec
            {
                if (!(isHex(userInput)) || (userInput.Length > 15))
                    MessageBox.Show("Please enter a valid hexadecimal number between 0 - FFFFFFFFFFFFFFF.");
                else
                    Output.Text = convertToBase(userInput, 16, 10);
            }
            else if (hexButtonInput.IsChecked == true && octButtonOutput.IsChecked == true) // Hex -> Oct
            {
                if (!(isHex(userInput)) || (userInput.Length > 15))
                    MessageBox.Show("Please enter a valid hexadecimal number between 0 - FFFFFFFFFFFFFFF.");
                else
                    Output.Text = convertToBase(userInput, 16, 8);
            }
            else if (octButtonInput.IsChecked == true && binButtonOutput.IsChecked == true) // Oct -> Bin
            {
                if (!(isOct(userInput)) || (userInput.Length > 21))
                    MessageBox.Show("Please enter a valid octal number between 0 - 777777777777777777777.");
                else
                    Output.Text = convertToBase(userInput, 8, 2);
            }
            else if (octButtonInput.IsChecked == true && decButtonOutput.IsChecked == true) // Oct -> Dec
            {
                if (!(isOct(userInput)) || (userInput.Length > 21))
                    MessageBox.Show("Please enter a valid octal number between 0 - 777777777777777777777.");
                else
                    Output.Text = convertToBase(userInput, 8, 10);
            }
            else if (octButtonInput.IsChecked == true && hexButtonOutput.IsChecked == true) // Oct -> Hex
            {
                if (!(isOct(userInput)) || (userInput.Length > 21))
                    MessageBox.Show("Please enter a valid octal number between 0 - 777777777777777777777.");
                else
                    Output.Text = convertToBase(userInput, 8, 16);
            }

        }



        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}