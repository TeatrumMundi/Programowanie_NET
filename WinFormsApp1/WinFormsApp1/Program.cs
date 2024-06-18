using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1;

public class EncryptionApp : Form
{
    private readonly ComboBox _algorithmComboBox;
    private readonly Button _generateKeyButton, _encryptButton, _decryptButton, _getEncryptTimeButton, _getDecryptTimeButton;
    private readonly TextBox _keyTextBox, _ivTextBox, _plainTextAsciiTextBox, _plainTextHexTextBox, _cipherTextAsciiTextBox, _cipherTextHexTextBox;
    private readonly Label _encryptTimeLabel, _decryptTimeLabel;
    private SymmetricAlgorithm _algorithm;

    public EncryptionApp()
    {
        // Initialize components
        _algorithmComboBox = new ComboBox { Left = 10, Top = 10, Width = 150 };
        _algorithmComboBox.Items.AddRange(new string[] { "DES", "TripleDES", "AES" });
        _algorithmComboBox.SelectedIndexChanged += AlgorithmComboBox_SelectedIndexChanged;

        _generateKeyButton = new Button { Text = "Generate Key and IV", Left = 170, Top = 10, Width = 150 };
        _generateKeyButton.Click += GenerateKeyButton_Click;

        _keyTextBox = new TextBox { Left = 10, Top = 70, Width = 200, PlaceholderText = "Key" };
        _ivTextBox = new TextBox { Left = 10, Top = 100, Width = 200, PlaceholderText = "IV" };

        _plainTextAsciiTextBox = new TextBox { Left = 10, Top = 130, Width = 200, PlaceholderText = "PlainText ASCII" };
        _plainTextHexTextBox = new TextBox { Left = 10, Top = 160, Width = 200, PlaceholderText = "PlainText HEX" };

        _encryptButton = new Button { Text = "Encrypt", Left = 10, Top = 190 };
        _encryptButton.Click += EncryptButton_Click;

        _cipherTextAsciiTextBox = new TextBox { Left = 10, Top = 220, Width = 200, PlaceholderText = "CipherText ASCII" };
        _cipherTextHexTextBox = new TextBox { Left = 10, Top = 250, Width = 200, PlaceholderText = "CipherText HEX" };

        _decryptButton = new Button { Text = "Decrypt", Left = 10, Top = 280 };
        _decryptButton.Click += DecryptButton_Click;

        _getEncryptTimeButton = new Button { Text = "Get Encrypt Time", Left = 10, Top = 310 };
        _getEncryptTimeButton.Click += GetEncryptTimeButton_Click;

        _getDecryptTimeButton = new Button { Text = "Get Decrypt Time", Left = 10, Top = 340 };
        _getDecryptTimeButton.Click += GetDecryptTimeButton_Click;

        _encryptTimeLabel = new Label { Text = "Time/message at encryption:", Left = 10, Top = 370, Width = 200 };
        _decryptTimeLabel = new Label { Text = "Time/message at decryption:", Left = 10, Top = 400, Width = 200 };

        // Add components to form
        this.Controls.Add(_algorithmComboBox);
        this.Controls.Add(_generateKeyButton);
        this.Controls.Add(_keyTextBox);
        this.Controls.Add(_ivTextBox);
        this.Controls.Add(_plainTextAsciiTextBox);
        this.Controls.Add(_plainTextHexTextBox);
        this.Controls.Add(_encryptButton);
        this.Controls.Add(_cipherTextAsciiTextBox);
        this.Controls.Add(_cipherTextHexTextBox);
        this.Controls.Add(_decryptButton);
        this.Controls.Add(_getEncryptTimeButton);
        this.Controls.Add(_getDecryptTimeButton);
        this.Controls.Add(_encryptTimeLabel);
        this.Controls.Add(_decryptTimeLabel);
        this.Text = "Symmetric Encryption Test";
    }

    private void AlgorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (_algorithmComboBox.SelectedItem.ToString())
        {
            case "DES":
                _algorithm = DES.Create();
                break;
            case "TripleDES":
                _algorithm = TripleDES.Create();
                break;
            case "AES":
                _algorithm = Aes.Create();
                break;
        }
    }

    private void GenerateKeyButton_Click(object sender, EventArgs e)
    {
        _algorithm.GenerateKey();
        _algorithm.GenerateIV();
        _keyTextBox.Text = BitConverter.ToString(_algorithm.Key).Replace("-", "");
        _ivTextBox.Text = BitConverter.ToString(_algorithm.IV).Replace("-", "");
    }

    private void EncryptButton_Click(object sender, EventArgs e)
    {
        string plainText = _plainTextAsciiTextBox.Text;
        byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
        ICryptoTransform encryptor = _algorithm.CreateEncryptor(_algorithm.Key, _algorithm.IV);

        byte[] cipherBytes;
        using (var ms = new System.IO.MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();
                cipherBytes = ms.ToArray();
            }
        }

        _cipherTextAsciiTextBox.Text = Encoding.ASCII.GetString(cipherBytes);
        _cipherTextHexTextBox.Text = BitConverter.ToString(cipherBytes).Replace("-", "");
    }

    private void DecryptButton_Click(object sender, EventArgs e)
    {
        byte[] cipherBytes = Encoding.ASCII.GetBytes(_cipherTextAsciiTextBox.Text);
        ICryptoTransform decryptor = _algorithm.CreateDecryptor(_algorithm.Key, _algorithm.IV);

        byte[] plainBytes;
        using (var ms = new System.IO.MemoryStream(cipherBytes))
        {
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                plainBytes = new byte[cipherBytes.Length];
                int decryptedByteCount = cs.Read(plainBytes, 0, plainBytes.Length);
            }
        }

        _plainTextAsciiTextBox.Text = Encoding.ASCII.GetString(plainBytes);
        _plainTextHexTextBox.Text = BitConverter.ToString(plainBytes).Replace("-", "");
    }

    private void GetEncryptTimeButton_Click(object sender, EventArgs e)
    {
        string plainText = _plainTextAsciiTextBox.Text;
        byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
        ICryptoTransform encryptor = _algorithm.CreateEncryptor(_algorithm.Key, _algorithm.IV);

        var watch = System.Diagnostics.Stopwatch.StartNew();

        using (var ms = new System.IO.MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();
            }
        }

        watch.Stop();
        _encryptTimeLabel.Text = $"Time/message at encryption: {watch.ElapsedMilliseconds} ms";
    }

    private void GetDecryptTimeButton_Click(object sender, EventArgs e)
    {
        byte[] cipherBytes = Encoding.ASCII.GetBytes(_cipherTextAsciiTextBox.Text);
        ICryptoTransform decryptor = _algorithm.CreateDecryptor(_algorithm.Key, _algorithm.IV);

        var watch = System.Diagnostics.Stopwatch.StartNew();

        using (var ms = new System.IO.MemoryStream(cipherBytes))
        {
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                byte[] plainBytes = new byte[cipherBytes.Length];
                cs.Read(plainBytes, 0, plainBytes.Length);
            }
        }

        watch.Stop();
        _decryptTimeLabel.Text = $"Time/message at decryption: {watch.ElapsedMilliseconds} ms";
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new EncryptionApp());
    }
}
