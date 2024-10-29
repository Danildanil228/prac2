using System;
using System.Threading;
using System.Windows;

namespace prac2
{
    public partial class App : Application
    {
        private const int MaxInstances = 3;
        private static Semaphore _semaphore;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            _semaphore = new Semaphore(MaxInstances, MaxInstances, "prac2_LimitedInstances", out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Достигнуто максимальное количество запущенных копий приложения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
            else
            {
                base.OnStartup(e);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _semaphore?.Release();
            base.OnExit(e);
        }
    }
}