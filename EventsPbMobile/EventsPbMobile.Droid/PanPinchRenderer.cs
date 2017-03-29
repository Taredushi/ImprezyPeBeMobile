using System;
using Android.Views;
using EventsPbMobile.Classes;
using EventsPbMobile.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(PinchToZoomContainer), typeof(PanPinchRenderer))]

namespace EventsPbMobile.Droid
{
    internal class PanPinchRenderer : ViewRenderer
    {
        private double _originDistance;
        private bool _panStarted;
        private bool _pinchStarted;


        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            _panStarted = false;
            _pinchStarted = false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var totalX = e.GetX();
            var totalY = e.GetY();
            var element = Element as PinchToZoomContainer;

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _pinchStarted = false;
                    _panStarted = true;

                    element.OnPanUpdated(this,
                        new PanUpdatedEventArgs(GestureStatus.Started, e.ActionIndex, totalX, totalY));

                    break;

                case MotionEventActions.Move:
                    if (e.PointerCount > 1)
                    {
                        var x1 = e.GetX(1);
                        var y1 = e.GetY(1);

                        if (_panStarted)
                            EndPan(element, totalX, totalY);

                        if (!_pinchStarted)
                        {
                            _pinchStarted = true;

                            element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Started));
                            _originDistance = GetDistance(totalX, totalY, x1, y1);
                        }
                        else
                        {
                            var distance = GetDistance(totalX, totalY, x1, y1);
                            var scale = distance / _originDistance;
                            _originDistance = distance;

                            var centre = new Point(Math.Min(totalX, x1) + Math.Abs(x1 - totalX) / 2,
                                Math.Min(totalY, y1) + Math.Abs(y1 - totalY) / 2);
                            var origin = new Point(centre.X / Width, centre.Y / Height);

                            element.OnPinchUpdated(this,
                                new PinchGestureUpdatedEventArgs(GestureStatus.Running, scale, origin));
                        }
                    }
                    else if (_panStarted)
                    {
                        element.OnPanUpdated(this,
                            new PanUpdatedEventArgs(GestureStatus.Running, e.ActionIndex, totalX, totalY));
                    }

                    break;

                case MotionEventActions.Up:
                    if (_panStarted)
                    {
                        EndPan(element, totalX, totalY);
                    }
                    else if (_pinchStarted)
                    {
                        element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Completed));
                        _pinchStarted = false;
                    }
                    break;

                default:
                    return base.OnTouchEvent(e);
            }

            return true;
        }

        private double GetDistance(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private void EndPan(PinchToZoomContainer element, float x, float y)
        {
            element.OnPanUpdated(this, new PanUpdatedEventArgs(GestureStatus.Completed, 0, x, y));
            _panStarted = false;
        }
    }
}