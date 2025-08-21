using WeatherAppliction.TablePanalSection;
using WeatherAppliction.WeatherSection;

namespace WeatherAppliction
{
    internal static partial class Program
    {
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            try
            {
                Application.Run(new MainForm(new WeatherApi(), new TablePanalcCass()));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong, please try again.",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
    }
}