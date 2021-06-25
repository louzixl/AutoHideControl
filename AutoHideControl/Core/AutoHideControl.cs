using System;
using System.Windows;
using System.Windows.Controls;

namespace AutoHideControl.Core
{
    public class MyAutoHideControl : UserControl
    {
        public MyAutoHideControl()
            : base()
        {
            Focusable = true;
            _lastTimeCollapsed = DateTime.Now.Ticks / 10000;

            IsVisibleChanged += AutoHideControl_IsVisibleChanged;
            GotFocus += AutoHideControl_GotFocus;
            LostFocus += AutoHideControl_LostFocus;
        }

        private void AutoHideControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Visibility != Visibility.Visible)
                Visibility = Visibility.Visible;
        }

        private void AutoHideControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
                Visibility = Visibility.Collapsed;
        }

        private void AutoHideControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == (bool)e.OldValue)
                return;

            if ((bool)e.NewValue)
            {
                long interval = DateTime.Now.Ticks / 10000 - _lastTimeCollapsed;
                if (interval > MinInterval && interval < MaxInterval)
                {
                    if (Visibility == Visibility.Visible)
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Visibility = Visibility.Collapsed;
                        }));
                    }
                }
                else
                    Focus();
            }
            else
                _lastTimeCollapsed = DateTime.Now.Ticks / 10000;
        }

        private long _lastTimeCollapsed;

        // 需处理从隐藏状态切换为显示状态的情况
        // 需处理的时间间隔
        private const long MinInterval = 50;
        private const long MaxInterval = 200;
    }
}
