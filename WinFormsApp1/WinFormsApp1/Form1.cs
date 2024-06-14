using System.Diagnostics;
using System.Globalization;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    private readonly TextBox _txtDividend;
    private readonly TextBox _txtDivisor;
    private readonly TextBox _txtResult;
    private readonly Button _btnDivide;
    public Form1()
    {
        // Initialize components
        _txtDividend = new TextBox { Left = 50, Top = 20, Width = 200 };
        _txtDivisor = new TextBox { Left = 50, Top = 50, Width = 200 };
        _txtResult = new TextBox { Left = 50, Top = 80, Width = 200, ReadOnly = true };
        _btnDivide = new Button { Text = "Divide", Left = 50, Top = 110, Width = 200 };

        _btnDivide.Click += BtnDivide_Click!;

        Controls.Add(_txtDividend);
        Controls.Add(_txtDivisor);
        Controls.Add(_txtResult);
        Controls.Add(_btnDivide);

        Text = "Division App";
        Width = 320;
        Height = 200;
    }
    private void BtnDivide_Click(object sender, EventArgs e)
    {
        try
        {
            double dividend = double.Parse(_txtDividend.Text);
            double divisor = double.Parse(_txtDivisor.Text);

            if (divisor == 0)
            {
                throw new DivideByZeroException("Divisor cannot be zero.");
            }

            double result = dividend / divisor;
            _txtResult.Text = result.ToString(CultureInfo.InvariantCulture);
        }
        catch (FormatException ex)
        {
            LogError("Input format error", ex);
            MessageBox.Show("Please enter valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (DivideByZeroException ex)
        {
            LogError("Division by zero error", ex);
            MessageBox.Show("Divisor cannot be zero.", "Math Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            LogError("General error", ex);
            MessageBox.Show("An error occurred. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void LogError(string message, Exception ex)
    {
        if (!EventLog.SourceExists("DivisionApp"))
        {
            EventLog.CreateEventSource("DivisionApp", "Application");
        }

        EventLog.WriteEntry("DivisionApp", $"{message}: {ex.Message}", EventLogEntryType.Error);
    }
}