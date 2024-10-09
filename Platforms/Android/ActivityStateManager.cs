using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Platforms.Android
{
    public static class ActivityStateManager
    {
        private static Activity _currentActivity;

        public static Activity CurrentActivity
        {
            get => _currentActivity;
            set => _currentActivity = value;
        }
    }
}
