using RBXStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RBXStudio.ViewModels
{
    public delegate void NotifyCallback();
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        public class Item
        {
            RBXItem rbxitem;
            public RBXClass Class => rbxitem.rbclass;
            public string Name => rbxitem.Name;
            bool expanded = false;

            NotifyCallback notify;
            public List<Item> Items = null;
            int level = 0;

            public int Margin => level * 25;
            public Item(RBXItem _rbxitem, int l, NotifyCallback n)
            {
                rbxitem = _rbxitem;
                notify = n;
                level = l;
            }

            public bool Expanded
            {
                get => expanded;
                set
                {
                    expanded = value;
                    DoExpand();
                    notify();
                }
            }

            void DoExpand()
            {
                if (expanded)
                {
                    Items = rbxitem.Items?.Select(rbxi => new Item(rbxi, level+1, notify)).ToList();
                }
                else
                    Items = null;
            }

            public void AddExpandedItems(List<Item> expandedItems)
            {
                expandedItems.Add(this);
                if (expanded && Items != null)
                {
                    foreach (var subitem in Items)
                    {
                        subitem.AddExpandedItems(expandedItems);
                    }
                }
            }
        }

        RBXDoc doc;

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<Item> ExpandedItems { get; set; } = new List<Item>();
        List<Item> topLevelItems;

        void NotifyRefresh()
        {
            RefreshExpandedItems();
        }
        public MainWindowViewModel()
        {
            doc = new RBXDoc();
            doc.Open("BreakInSmall.rbxl");
            topLevelItems = doc.Items.Select(rbxi => new Item(rbxi, 0, NotifyRefresh)).ToList();
            RefreshExpandedItems();
        }

        void RefreshExpandedItems()
        {
            ExpandedItems = new List<Item>();
            foreach (var item in topLevelItems)
            {
                item.AddExpandedItems(ExpandedItems);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpandedItems)));
        }
    }
}
