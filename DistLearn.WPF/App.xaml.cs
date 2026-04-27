using System.Windows;
using DistLearn.WPF.Data;

namespace DistLearn.WPF
{
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            AppStorage.Save();
            base.OnExit(e);
        }
    }
}