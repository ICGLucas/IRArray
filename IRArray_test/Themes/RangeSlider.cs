using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace IRArray_test
{
    /// <summary>
    /// 依照步驟 1a 或 1b 執行，然後執行步驟 2，以便在 XAML 檔中使用此自訂控制項。
    ///
    /// 步驟 1a) 於存在目前專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:IRArray_test"
    ///
    ///
    /// 步驟 1b) 於存在其他專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:IRArray_test;assembly=IRArray_test"
    ///
    /// 您還必須將 XAML 檔所在專案的專案參考加入
    /// 此專案並重建，以免發生編譯錯誤: 
    ///
    ///     在 [方案總管] 中以滑鼠右鍵按一下目標專案，並按一下
    ///     [加入參考]->[專案]->[瀏覽並選取此專案]
    ///
    ///
    /// 步驟 2)
    /// 開始使用 XAML 檔案中的控制項。
    ///
    ///     <MyNamespace:RangeSlider/>
    ///
    /// </summary>
    public class RangeSlider : Control
    {
        #region Parameter
        //private string Flag = "ProgressRotate";
        private FrameworkElement SliderContainer;
        private Thumb StartThumb;
        private Thumb EndThumb;
        private FrameworkElement StartArea;
        private FrameworkElement EndArea;
        enum SliderThumb
        {
            None,
            Start,
            End
        }
        #endregion
        #region Property
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation",
            typeof(Orientation),
            typeof(RangeSlider),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum",
            typeof(double),
            typeof(RangeSlider),
            new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum",
            typeof(double),
            typeof(RangeSlider),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure)
        );
        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            "Start",
            typeof(double),
            typeof(RangeSlider),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }
        public static readonly DependencyProperty EndProperty = DependencyProperty.Register(
            "End",
            typeof(double),
            typeof(RangeSlider),
            new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public bool IsMoveToPointEnabled
        {
            get { return (bool)GetValue(IsMoveToPointEnabledProperty); }
            set { SetValue(IsMoveToPointEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsMoveToPointEnabledProperty = DependencyProperty.Register(
            "IsMoveToPointEnabled",
            typeof(bool),
            typeof(RangeSlider),
            new FrameworkPropertyMetadata(true)
        );

        public object StartThumbToolTip
        {
            get { return GetValue(StartThumbToolTipProperty); }
            set { SetValue(StartThumbToolTipProperty, value); }
        }
        public static readonly DependencyProperty StartThumbToolTipProperty = DependencyProperty.Register(
            "StartThumbToolTip",
            typeof(object),
            typeof(RangeSlider)
        );

        public object EndThumbToolTip
        {
            get { return GetValue(EndThumbToolTipProperty); }
            set { SetValue(EndThumbToolTipProperty, value); }
        }
        public static readonly DependencyProperty EndThumbToolTipProperty = DependencyProperty.Register(
            "EndThumbToolTip",
            typeof(object),
            typeof(RangeSlider)
        );

        public Style StartThumbStyle
        {
            get { return (Style)GetValue(StartThumbStyleProperty); }
            set { SetValue(StartThumbStyleProperty, value); }
        }
        public static readonly DependencyProperty StartThumbStyleProperty = DependencyProperty.Register(
            "StartThumbStyle",
            typeof(Style),
            typeof(RangeSlider)
        );

        public Style EndThumbStyle
        {
            get { return (Style)GetValue(EndThumbStyleProperty); }
            set { SetValue(EndThumbStyleProperty, value); }
        }
        public static readonly DependencyProperty EndThumbStyleProperty = DependencyProperty.Register(
            "EndThumbStyle",
            typeof(Style),
            typeof(RangeSlider)
        );

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            "IsReadOnly",
            typeof(bool),
            typeof(RangeSlider)
        );
        #endregion
        #region Presentation
        #endregion
        #region Contents
        #endregion
        #region Event
        public event System.EventHandler<EventInfo> EventHandler;
        protected virtual void OnEvent(string Option, params object[] Values)
        {
            EventHandler?.Invoke(this, new EventInfo(Option, Values));
        }

        private static void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            RangeSlider rs = sender as RangeSlider; if (rs == null) { return; }
            rs.OnThumbDragDelta(e);
        }
        public event DragDeltaEventHandler ThumbDragDelta;
        protected virtual void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            DragDeltaEventHandler handler = ThumbDragDelta;
            if (handler != null) { handler.Invoke(this, e); }
            OnEvent("ThumbDragDelta");

            Thumb thumb = e.OriginalSource as Thumb; if (thumb == null) { return; }
            if (!IsReadOnly && SliderContainer != null)
            {
                double change;
                if (Orientation == Orientation.Horizontal) { change = e.HorizontalChange / SliderContainer.ActualWidth * (Maximum - Minimum); }
                else { change = e.VerticalChange / SliderContainer.ActualHeight * (Maximum - Minimum); }

                if (thumb == StartThumb) { Start = Math.Max(Minimum, Math.Min(End, Start + change)); }
                else if (thumb == EndThumb) { End = Math.Min(Maximum, Math.Max(Start, End + change)); }
            }
        }

        private static void OnDragStartedEvent(object sender, DragStartedEventArgs e)
        {
            RangeSlider rs = sender as RangeSlider; if (rs == null) { return; }
            rs.OnDragStartedEvent(e);
        }
        public event DragStartedEventHandler DragStartedEvent;
        protected virtual void OnDragStartedEvent(DragStartedEventArgs e)
        {
            DragStartedEventHandler handler = DragStartedEvent;
            if (handler != null) { handler.Invoke(this, e); }
            OnEvent("DragStarted");
        }

        private static void OnDragCompletedEvent(object sender, DragCompletedEventArgs e)
        {
            RangeSlider rs = sender as RangeSlider; if (rs == null) { return; }
            rs.OnDragCompletedEvent(e);
        }

        public event DragCompletedEventHandler DragCompletedEvent;
        protected virtual void OnDragCompletedEvent(DragCompletedEventArgs e)
        {
            DragCompletedEventHandler handler = DragCompletedEvent;
            if (handler != null) { handler.Invoke(this, e); }
            OnEvent("DragCompleted");
        }
        #endregion
        #region Method
        static RangeSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RangeSlider), new FrameworkPropertyMetadata(typeof(RangeSlider)));

            EventManager.RegisterClassHandler(typeof(RangeSlider), Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStartedEvent));
            EventManager.RegisterClassHandler(typeof(RangeSlider), Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnThumbDragDelta));
            EventManager.RegisterClassHandler(typeof(RangeSlider), Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragCompletedEvent));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SliderContainer = GetTemplateChild("PART_SliderContainer") as FrameworkElement;
            if (SliderContainer != null) { SliderContainer.PreviewMouseDown += ViewBox_PreviewMouseDown; }

            StartArea = GetTemplateChild("PART_StartArea") as FrameworkElement;
            EndArea = GetTemplateChild("PART_EndArea") as FrameworkElement;
            StartThumb = GetTemplateChild("PART_StartThumb") as Thumb;
            EndThumb = GetTemplateChild("PART_EndThumb") as Thumb;
        }
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var arrangeSize = base.ArrangeOverride(arrangeBounds);

            if (Maximum > Minimum && StartArea != null && EndArea != null)
            {
                var start = Math.Max(Minimum, Math.Min(Maximum, Start));
                var end = Math.Max(Minimum, Math.Min(Maximum, End));
                Rect rectStart, rectEnd;
                if (Orientation == Orientation.Horizontal)
                {
                    var viewportSize = SliderContainer != null ? SliderContainer.ActualWidth : arrangeBounds.Width;
                    var startPosition = (start - Minimum) / (Maximum - Minimum) * viewportSize;
                    var endPosition = (end - Minimum) / (Maximum - Minimum) * viewportSize;
                    rectStart = new Rect(0, 0, startPosition, arrangeBounds.Height);
                    rectEnd = new Rect(endPosition, 0, viewportSize - endPosition, arrangeBounds.Height);
                }
                else
                {
                    var viewportSize = SliderContainer != null ? SliderContainer.ActualHeight : arrangeBounds.Height;
                    var startPosition = (start - Minimum) / (Maximum - Minimum) * viewportSize;
                    var endPosition = (end - Minimum) / (Maximum - Minimum) * viewportSize;
                    rectStart = new Rect(0, 0, arrangeBounds.Width, startPosition);
                    rectEnd = new Rect(0, endPosition, arrangeBounds.Width, viewportSize - endPosition);
                }

                if (StartArea != null) StartArea.Arrange(rectStart);
                if (EndArea != null) EndArea.Arrange(rectEnd);
                if (StartThumb != null) StartThumb.Arrange(rectStart);
                if (EndThumb != null) EndThumb.Arrange(rectEnd);
            }

            return arrangeSize;
        }
        private void ViewBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsReadOnly && IsMoveToPointEnabled)
            {
                if ((StartThumb != null && StartThumb.IsMouseOver) || (EndThumb != null && EndThumb.IsMouseOver)) { return; }

                var point = e.GetPosition(SliderContainer);
                if (e.ChangedButton == MouseButton.Left) { MoveBlockTo(point, SliderThumb.Start); }
                else if (e.ChangedButton == MouseButton.Right) { MoveBlockTo(point, SliderThumb.End); }

                e.Handled = true;
            }
        }
        private void MoveBlockTo(Point point, SliderThumb block)
        {
            double position;
            if (Orientation == Orientation.Horizontal) { position = point.X; }
            else { position = point.Y; }

            double viewportSize = (Orientation == Orientation.Horizontal) ? SliderContainer.ActualWidth : SliderContainer.ActualHeight;
            if (!double.IsNaN(viewportSize) && viewportSize > 0)
            {
                var value = Math.Min(Maximum, Minimum + (position / viewportSize) * (Maximum - Minimum));
                if (block == SliderThumb.Start) { Start = Math.Min(End, value); }
                else if (block == SliderThumb.End) { End = Math.Max(Start, value); }
            }
        }
        #endregion
        #region Command
        #endregion
    }
}
