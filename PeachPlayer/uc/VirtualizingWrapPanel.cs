using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PeachPlayer.uc
{
    public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo
    {
        private TranslateTransform trans = new TranslateTransform();

        public VirtualizingWrapPanel()
        {
            this.RenderTransform = trans;
        }

        #region DependencyProperties
        public static readonly DependencyProperty ChildWidthProperty = DependencyProperty.RegisterAttached("ChildWidth", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty ChildHeightProperty = DependencyProperty.RegisterAttached("ChildHeight", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        //鼠标每一次滚动 UI上的偏移
        public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.RegisterAttached("ScrollOffset", typeof(int), typeof(VirtualizingWrapPanel), new PropertyMetadata(10));

        public int ScrollOffset
        {
            get { return Convert.ToInt32(GetValue(ScrollOffsetProperty)); }
            set { SetValue(ScrollOffsetProperty, value); }
        }
        public double ChildWidth
        {
            get => Convert.ToDouble(GetValue(ChildWidthProperty));
            set => SetValue(ChildWidthProperty, value);
        }
        public double ChildHeight
        {
            get => Convert.ToDouble(GetValue(ChildHeightProperty));
            set => SetValue(ChildHeightProperty, value);
        }
        #endregion

        int GetItemCount(DependencyObject element)
        {
            var itemsControl = ItemsControl.GetItemsOwner(element);
            return itemsControl.HasItems ? itemsControl.Items.Count : 0;
        }
        int CalculateChildrenPerRow(Size availableSize)
        {
            int childPerRow = 0;
            if (availableSize.Width == double.PositiveInfinity)
                childPerRow = this.Children.Count;
            else
                childPerRow = Math.Max(1, Convert.ToInt32(Math.Floor(availableSize.Width / this.ChildWidth)));
            return childPerRow;
        }
        /// <summary>
        /// width不超过availableSize的情况下，自身实际需要的Size(高度可能会超出availableSize)
        /// </summary>
        /// <param name="availableSize"></param>
        /// <param name="itemsCount"></param>
        /// <returns></returns>
        Size CalculateExtent(Size availableSize, int itemsCount)
        {
            int childPerRow = CalculateChildrenPerRow(availableSize);//现有宽度下 一行可以最多容纳多少个
            return new Size(childPerRow * this.ChildWidth, this.ChildHeight * Math.Ceiling(Convert.ToDouble(itemsCount) / childPerRow));
        }
        /// <summary>
        /// 更新滚动条
        /// </summary>
        /// <param name="availableSize"></param>
        void UpdateScrollInfo(Size availableSize)
        {
            var extent = CalculateExtent(availableSize, GetItemCount(this));//extent 自己实际需要
            if (extent != this.extent)
            {
                this.extent = extent;
                this.ScrollOwner.InvalidateScrollInfo();
            }
            if (availableSize != this.viewPort)
            {
                this.viewPort = availableSize;
                this.ScrollOwner.InvalidateScrollInfo();
            }
        }
        /// <summary>
        /// 获取所有item，在可视区域内第一个item和最后一个item的索引
        /// </summary>
        /// <param name="firstIndex"></param>
        /// <param name="lastIndex"></param>
        void GetVisiableRange(ref int firstIndex, ref int lastIndex)
        {
            int childPerRow = CalculateChildrenPerRow(this.extent);
            firstIndex = Convert.ToInt32(Math.Floor(this.offset.Y / this.ChildHeight)) * childPerRow;
            lastIndex = Convert.ToInt32(Math.Ceiling((this.offset.Y + this.viewPort.Height) / this.ChildHeight)) * childPerRow - 1;
            int itemsCount = GetItemCount(this);
            if (lastIndex >= itemsCount)
                lastIndex = itemsCount - 1;

        }
        /// <summary>
        /// 将不在可视区域内的item 移除
        /// </summary>
        /// <param name="startIndex">可视区域开始索引</param>
        /// <param name="endIndex">可视区域结束索引</param>
        void CleanUpItems(int startIndex, int endIndex)
        {
            var children = this.InternalChildren;
            var generator = this.ItemContainerGenerator;
            for (int i = children.Count - 1; i >= 0; i--)
            {
                var childGeneratorPosi = new GeneratorPosition(i, 0);
                int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPosi);

                if (itemIndex < startIndex || itemIndex > endIndex)
                {

                    generator.Remove(childGeneratorPosi, 1);
                    RemoveInternalChildRange(i, 1);
                }
            }
        }
        /// <summary>
        /// scroll/availableSize/添加删除元素 改变都会触发  edit元素不会改变
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            this.UpdateScrollInfo(availableSize);//availableSize更新后，更新滚动条
            int firstVisiableIndex = 0, lastVisiableIndex = 0;
            GetVisiableRange(ref firstVisiableIndex, ref lastVisiableIndex);//availableSize更新后，获取当前viewport内可放置的item的开始和结束索引  firstIdnex-lastIndex之间的item可能部分在viewport中也可能都不在viewport中。

            UIElementCollection children = this.InternalChildren;//因为配置了虚拟化，所以children的个数一直是viewport区域内的个数,如果没有虚拟化则是ItemSource的整个的个数
            IItemContainerGenerator generator = this.ItemContainerGenerator;
            //获得第一个可被显示的item的位置
            GeneratorPosition startPosi = generator.GeneratorPositionFromIndex(firstVisiableIndex);
            int childIndex = (startPosi.Offset == 0) ? startPosi.Index : startPosi.Index + 1;//startPosi在chilren中的索引
            using (generator.StartAt(startPosi, GeneratorDirection.Forward, true))
            {
                int itemIndex = firstVisiableIndex;
                while (itemIndex <= lastVisiableIndex)//生成lastVisiableIndex-firstVisiableIndex个item
                {
                    bool newlyRealized = false;
                    var child = generator.GenerateNext(out newlyRealized) as UIElement;
                    if (newlyRealized)
                    {
                        if (childIndex >= children.Count)
                            base.AddInternalChild(child);
                        else
                        {
                            base.InsertInternalChild(childIndex, child);
                        }
                        generator.PrepareItemContainer(child);
                    }
                    else
                    {
                        //处理 正在显示的child被移除了这种情况
                        if (!child.Equals(children[childIndex]))
                        {
                            base.RemoveInternalChildRange(childIndex, 1);
                        }
                    }
                    child.Measure(new Size(this.ChildWidth, this.ChildHeight));
                    //child.DesiredSize//child想要的size
                    itemIndex++;
                    childIndex++;
                }
            }
            CleanUpItems(firstVisiableIndex, lastVisiableIndex);
            return new Size(double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width, double.IsInfinity(availableSize.Height) ? 0 : availableSize.Height);//自身想要的size
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine("----ArrangeOverride");
            var generator = this.ItemContainerGenerator;
            UpdateScrollInfo(finalSize);
            int childPerRow = CalculateChildrenPerRow(finalSize);
            double availableItemWidth = finalSize.Width / childPerRow;
            for (int i = 0; i <= this.Children.Count - 1; i++)
            {
                var child = this.Children[i];
                int itemIndex = generator.IndexFromGeneratorPosition(new GeneratorPosition(i, 0));
                int row = itemIndex / childPerRow;//current row
                int column = itemIndex % childPerRow;
                double xCorrdForItem = 0;

                xCorrdForItem = column * availableItemWidth + (availableItemWidth - this.ChildWidth) / 2;

                Rect rec = new Rect(xCorrdForItem, row * this.ChildHeight, this.ChildWidth, this.ChildHeight);
                child.Arrange(rec);
            }
            return finalSize;
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.SetVerticalOffset(this.VerticalOffset);
        }
        protected override void OnClearChildren()
        {
            base.OnClearChildren();
            this.SetVerticalOffset(0);
        }
        protected override void BringIndexIntoView(int index)
        {
            if (index < 0 || index >= Children.Count)
                throw new ArgumentOutOfRangeException();
            int row = index / CalculateChildrenPerRow(RenderSize);
            SetVerticalOffset(row * this.ChildHeight);
        }
        #region IScrollInfo Interface
        public bool CanVerticallyScroll { get; set; }
        public bool CanHorizontallyScroll { get; set; }

        private Size extent = new Size(0, 0);
        public double ExtentWidth => this.extent.Width;

        public double ExtentHeight => this.extent.Height;

        private Size viewPort = new Size(0, 0);
        public double ViewportWidth => this.viewPort.Width;

        public double ViewportHeight => this.viewPort.Height;

        private Point offset;
        public double HorizontalOffset => this.offset.X;

        public double VerticalOffset => this.offset.Y;

        public ScrollViewer ScrollOwner { get; set; }

        public void LineDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
        }

        public void LineLeft()
        {
            throw new NotImplementedException();
        }

        public void LineRight()
        {
            throw new NotImplementedException();
        }

        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            return new Rect();
        }

        public void MouseWheelDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
        }

        public void MouseWheelLeft()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelRight()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
        }

        public void PageDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.viewPort.Height);
        }

        public void PageLeft()
        {
            throw new NotImplementedException();
        }

        public void PageRight()
        {
            throw new NotImplementedException();
        }

        public void PageUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.viewPort.Height);
        }

        public void SetHorizontalOffset(double offset)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalOffset(double offset)
        {
            if (offset < 0 || this.viewPort.Height >= this.extent.Height)
                offset = 0;
            else
                if (offset + this.viewPort.Height >= this.extent.Height)
                offset = this.extent.Height - this.viewPort.Height;

            this.offset.Y = offset;
            this.ScrollOwner?.InvalidateScrollInfo();
            this.trans.Y = -offset;
            this.InvalidateMeasure();
            //接下来会触发MeasureOverride()
        }
        #endregion
    }
}
