using System;
using System.Windows.Forms;

using Microsoft.Practices.ServiceLocation;

using Opgenorth.RangeLog.Core.Commands;
using Opgenorth.RangeLog.Core.Container;
using Opgenorth.RangeLog.Core.Data.db4o;


namespace Opgenorth.RangeLog.WinForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeApplication();

            Application.Run(new RangeTripsView());
        }

        private static void InitializeApplication()
        {
            var initCmd = new InitializeCommonServiceLocatorCommand();
            initCmd.Execute();

            var initAutoMapper = new ConfigureAutoMapperCommand();
            initAutoMapper.Execute();

            var createDb = ServiceLocator.Current.GetInstance<ICreateDb4oDatabase>();
            createDb.Execute();
        }
    }
}