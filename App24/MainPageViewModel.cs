using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;

namespace App24
{
    public class MainPageViewModel
    {
        public event EventHandler ItemTextChanged;
        private Random Random { get; } = new Random();
        public MainPageViewModel()
        {
            var timer = new DispatcherTimer();
            timer.Tick += this.Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            if (this.Random.Next() % 2 == 0)
            {
                var item = new Item
                {
                    Text = string.Join("", Enumerable.Range(1, this.Random.Next(140)).Select(_ => "a").ToArray())
                };
                item.PropertyChanged += (_, args) =>
                {
                    if (args.PropertyName == nameof(Item.Text))
                    {
                        this.ItemTextChanged?.Invoke(this, EventArgs.Empty);
                    }
                };
                this.Items.Add(item);
            }
            else
            {
                if (this.Items.Count == 0) { return; }
                var item = this.Items[this.Random.Next(this.Items.Count)];
                item.Text = string.Join("", Enumerable.Range(1, this.Random.Next(140)).Select(_ => "a").ToArray());
            }
        }
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

    }

    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly PropertyChangedEventArgs TextPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Text));

        private string text;

        public string Text
        {
            get { return this.text; }
            set
            {
                if (this.text == value) { return; }
                this.text = value;
                this.PropertyChanged?.Invoke(this, TextPropertyChangedEventArgs);
            }
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

    }
}