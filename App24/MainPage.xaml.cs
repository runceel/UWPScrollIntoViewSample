using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace App24
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel ViewModel { get; } = new MainPageViewModel();

        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel.Items.CollectionChanged += this.Items_CollectionChanged;
            this.ViewModel.ItemTextChanged += this.ViewModel_ItemTextChanged;
        }

        private async void ViewModel_ItemTextChanged(object sender, EventArgs e)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.ScrollToBottom();
            });
        }

        private async void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.ScrollToBottom();
            });
        }

        private void ScrollToBottom()
        {
            var target = this.ViewModel.Items.Last();
            var viewItems = VisualTreeHelper.FindElementsInHostCoordinates(
                new Rect(0, 0, 1, this.ListBox.ActualHeight), this.ListBox, true).OfType<ListBoxItem>();
            if (!viewItems.Any(x => x.DataContext == target))
            {
                Debug.WriteLine("表示されてない");
                this.ListBox.ScrollIntoView(target);
            }
            else
            {
                Debug.WriteLine("表示されてる");
            }
        }
    }
}
