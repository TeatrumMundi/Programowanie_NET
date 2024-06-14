using System.Diagnostics;
using System.Globalization;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        private double _resultValue;
        private string _operationPerformed = "";
        private bool _isOperationPerformed;

        private const int InitializationTimeThreshold = 1000; // Próg czasu inicjalizacji w milisekundach

        public Form1()
        {
            var stopwatch = Stopwatch.StartNew();
            InitializeComponent();
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > InitializationTimeThreshold)
            {
                LogInitializationTime(stopwatch.ElapsedMilliseconds);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if ((textBox_Result.Text == "0") || (_isOperationPerformed))
                textBox_Result.Clear();

            _isOperationPerformed = false;
            Button button = (Button)sender;
            textBox_Result.Text = textBox_Result.Text + button.Text;
        }

        private void operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            _operationPerformed = button.Text;
            _resultValue = double.Parse(textBox_Result.Text);
            _isOperationPerformed = true;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            switch (_operationPerformed)
            {
                case "+":
                    textBox_Result.Text = (_resultValue + double.Parse(textBox_Result.Text)).ToString(CultureInfo.InvariantCulture);
                    break;
                case "-":
                    textBox_Result.Text = (_resultValue - double.Parse(textBox_Result.Text)).ToString(CultureInfo.InvariantCulture);
                    break;
                case "*":
                    textBox_Result.Text = (_resultValue * double.Parse(textBox_Result.Text)).ToString(CultureInfo.InvariantCulture);
                    break;
                case "/":
                    textBox_Result.Text = (_resultValue / double.Parse(textBox_Result.Text)).ToString(CultureInfo.InvariantCulture);
                    break;
            }
            _isOperationPerformed = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            _resultValue = 0;
        }

        private void LogInitializationTime(long elapsedMilliseconds)
        {
            using EventLog eventLog = new EventLog("Application");
            eventLog.Source = "SimpleCalculator";
            eventLog.WriteEntry($"Initialization time exceeded threshold: {elapsedMilliseconds} ms", EventLogEntryType.Warning);
        }
    }
}
