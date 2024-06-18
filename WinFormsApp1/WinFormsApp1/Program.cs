

namespace WinFormsApp1;

public class SinePlotForm : Form
{
    public SinePlotForm()
    {
        // Ustawienie tytułu okienka
        this.Text = "Wykres funkcji sinus";
        
        // Ustawienie rozmiaru okienka
        this.Size = new Size(800, 600);
        
        // Dodanie obsługi zdarzenia Paint, które rysuje wykres
        this.Paint += new PaintEventHandler(OnPaint);
    }

    public sealed override string Text
    {
        get { return base.Text; }
        set { base.Text = value; }
    }

    private void OnPaint(object? sender, PaintEventArgs e)
    {
        // Pobranie obiektu Graphics do rysowania
        Graphics g = e.Graphics;

        // Ustawienie koloru tła na biały
        g.Clear(Color.White);

        // Ustawienie koloru pióra do rysowania osi i wykresu
        Pen axisPen = new Pen(Color.Black, 2);
        Pen sinePen = new Pen(Color.Blue, 2);

        // Środek okienka
        int centerX = this.ClientSize.Width / 2;
        int centerY = this.ClientSize.Height / 2;

        // Rysowanie osi X
        g.DrawLine(axisPen, 0, centerY, this.ClientSize.Width, centerY);

        // Rysowanie osi Y
        g.DrawLine(axisPen, centerX, 0, centerX, this.ClientSize.Height);

        // Rysowanie wykresu funkcji sinus
        for (int i = -360; i < 360; i++)
        {
            // Konwersja stopni na radiany
            double radians = i * Math.PI / 180.0;

            // Obliczenie wartości sinus
            double sinValue = Math.Sin(radians);

            // Skala wykresu
            int x = centerX + i;
            int y = centerY - (int)(sinValue * 100);

            // Rysowanie punktu na wykresie
            g.DrawRectangle(sinePen, x, y, 1, 1);
        }
    }

    [STAThread]
    public static void Main()
    {
        // Uruchomienie aplikacji
        Application.Run(new SinePlotForm());
    }
}