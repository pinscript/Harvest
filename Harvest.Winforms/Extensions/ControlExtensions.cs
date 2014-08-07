using System;
using System.Reflection;
using System.Windows.Forms;

namespace Harvest.Winforms.Extensions
{
    public static class ControlExtensions
    {
         public static void InvokeIfRequired(this Control instance, Action action)
         {
             if (instance.InvokeRequired)
             {
                 instance.BeginInvoke(new MethodInvoker(() => action()));
             }
             else
             {
                 action();
             }
         }

         public static void DoubleBuffering(this Control control, bool enable)
         {
             var method = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
             method.Invoke(control, new object[] { ControlStyles.OptimizedDoubleBuffer, enable });
         }
    }
}