using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Filtration.ObjectModel.BlockItemBaseTypes
{
    public abstract class BooleanStringListBlockItem : BlockItemBase
    {
        private bool _booleanValue;

        protected BooleanStringListBlockItem()
        {
            Items = new ObservableCollection<string>();
            Items.CollectionChanged += OnItemsCollectionChanged;
        }

        public bool BooleanValue
        {
            get { return _booleanValue; }
            set
            {
                _booleanValue = value;
                IsDirty = true;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SummaryText));
            }
        }

        public ObservableCollection<string> Items { get; protected set; }

        public override string OutputText
        {
            get
            {
                var enumerable = Items as IList<string> ?? Items.ToList();
                if (enumerable.Count > 0)
                {
                    return PrefixText + (_booleanValue ? " == " : " ") + string.Join(" ", enumerable);
                }

                return string.Empty;
            }
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(SummaryText));
        }
    }
}
