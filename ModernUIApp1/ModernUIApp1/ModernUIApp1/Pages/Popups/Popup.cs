using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ModernUIApp1.Pages.Popups
{
    /* Class popup for ModernDialog which returns a boolean result with show() method, useful class, could be use outside this class */
    class Popup
    {
        ModernDialog pop;
        public String result { get; protected set; } // Text return in the textbox
        bool textbox;
        TextBox text; 

        /* Title, Label, textbox = true if you want a textbox */
        public Popup(String title, String content, bool textbox)
        {
            this.textbox = textbox;

            pop = new ModernDialog();
            pop.Title = title;

            if (!textbox)
            {
                pop.Content = new Label().Content = content;
            }
            else
            {
                Grid grid = new Grid();
                grid.Height = 75;
                grid.Width = 250;

                RowDefinition row1 = new RowDefinition();
                row1.Height = GridLength.Auto;
                RowDefinition row2 = new RowDefinition();
                row2.Height = new GridLength(1, GridUnitType.Star);

                grid.RowDefinitions.Add(row1);
                grid.RowDefinitions.Add(row2);

                Label lab = new Label();
                lab.Content = content;
                Grid.SetRow(lab, 0);

                grid.Children.Add(lab);

                text = new TextBox();
                Grid.SetRow(text, 1);

                grid.Children.Add(text);

                pop.Content = grid;
            }

            pop.FontSize = 24;
            pop.OkButton.Click += OkButton_Click;
            pop.Buttons = new Button[] { pop.OkButton, pop.CancelButton };
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            pop.DialogResult = true;

            if (textbox && text != null)
            {
                result = text.Text;
            }

            pop.Close();
        }

        public bool show()
        {
            return (bool)pop.ShowDialog();
        }
    }
}
