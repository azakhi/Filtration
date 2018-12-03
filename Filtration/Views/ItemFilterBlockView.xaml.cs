﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Filtration.UserControls;
using Filtration.Utility;

namespace Filtration.Views
{
    public partial class ItemFilterBlockView
    {
        public ItemFilterBlockView()
        {
            InitializeComponent();
        }

        private void BlockExpander_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var originalSource = e.OriginalSource as Visual;
            if (originalSource != null && originalSource.IsDescendantOf(BlockItemsGrid))
            {
                return;
            }
            
            BlockExpander.IsExpanded = !BlockExpander.IsExpanded;
        }

        private void AutoCompleteBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AutoCompleteBox box = sender as AutoCompleteBox;
                dynamic viewModel = box.DataContext;
                viewModel.AddBlockGroupCommand.Execute(null);
            }
        }
        
        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Prevents the ScrollViewer from handling mouse wheel events, and passes the events
            // to the parent control instead. This is necessary because the ItemsControl that displays
            // ItemFilterBlocks is in a ScrollViewer but we want to use the mouse wheel for scrolling
            // up and down in ItemFilterScriptView rather than within the block.
            if (sender is ScrollViewer viewer && !e.Handled)
            {
                // Don't handle events if they originated from a control within an EditableListBoxControl
                // since we still want to allow scrolling within those with the mouse wheel
                if (e.OriginalSource is DependencyObject dependencyObject && VisualTreeUtility.FindParent<EditableListBoxControl>(dependencyObject) != null)
                {
                    e.Handled = false;
                    return;
                }

                e.Handled = true;

                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) {RoutedEvent = MouseWheelEvent, Source = viewer};
                if (viewer.Parent is UIElement parent)
                {
                    parent.RaiseEvent(eventArg);
                }
            }
        }
    }
}
