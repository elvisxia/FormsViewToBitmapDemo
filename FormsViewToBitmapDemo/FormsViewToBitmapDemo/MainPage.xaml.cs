using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsViewToBitmapDemo
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Grid grid = new Grid();

            for (int i = 0; i < 30; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(30,GridUnitType.Absolute)
                };
                ColumnDefinition col = new ColumnDefinition
                {
                    Width = new GridLength(30,GridUnitType.Absolute)
                };
                grid.RowDefinitions.Add(row);
                grid.ColumnDefinitions.Add(col);
            }

            for (int row = 0; row < 30; row++)
            {
                for (int col = 0; col < 30; col++)
                {
                    Label label = new Label
                    {
                        Text = "Row:" + row + "; Col:" + col
                    };
                    if (col == 29 || row == 29)
                    {
                        label.TextColor = Color.Red;
                    } else
                    {
                        label.TextColor = Color.Black;
                    }
                    grid.Children.Add(label, col,row);
                }
            }
            root.Children.Add(grid);
            btnClick.Clicked += BtnClick_Clicked;
        }

        private void BtnClick_Clicked(object sender, EventArgs e)
        {
            root.OnDrawBitmap.Invoke(root, new EventArgs());
        }
    }
}
