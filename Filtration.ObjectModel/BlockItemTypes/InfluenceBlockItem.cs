using System.Linq;
using System.Windows.Media;
using Filtration.ObjectModel.BlockItemBaseTypes;
using Filtration.ObjectModel.Enums;

namespace Filtration.ObjectModel.BlockItemTypes
{
    public class InfluenceBlockItem : BooleanStringListBlockItem
    {
        public override string PrefixText => "HasInfluence";
        public override int MaximumAllowed => 1;
        public override string DisplayHeading => "Has Influence";

        public override string SummaryText
        {
            get
            {
                var influenceText = "Influences (" + (BooleanValue ? "AND" : "OR") + "): ";
                if (Items.Count > 0 && Items.Count < 3)
                {
                    return influenceText +
                           Items.Aggregate(string.Empty, (current, i) => current + i + ", ").TrimEnd(' ').TrimEnd(',');
                }
                if (Items.Count >= 3)
                {
                    var remaining = Items.Count - 2;
                    return influenceText + Items.Take(2)
                        .Aggregate(string.Empty, (current, i) => current + i + ", ")
                        .TrimEnd(' ')
                        .TrimEnd(',') + " (+" + remaining + " more)";
                }
                return influenceText + "(none)";
            }
        }

        public override Color SummaryBackgroundColor => Colors.DimGray;
        public override Color SummaryTextColor => Colors.White;
        public override BlockItemOrdering SortOrder => BlockItemOrdering.InfluencedItem;
    }
}
