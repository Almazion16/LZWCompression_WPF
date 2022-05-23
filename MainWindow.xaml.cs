using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LZWCompression
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToCompress_Button_Click(object sender, RoutedEventArgs e)
        {
            string text = Text_TextBox.Text;
            List<int> compressedTextList = Compression.ToCompress(text);
            CompressedText_Textbox.Text = string.Join(" ", compressedTextList);


            OriginalLength_Textblock.Text = text.Length.ToString();
            CompressedLength_TextBlock.Text = CompressedText_Textbox.Text.Length.ToString();

            StreamWriter sw = new StreamWriter("textForSizeOriginal.txt", false);
            sw.Write(text);
            FileInfo fi = new FileInfo("textForSizeOriginal.txt");
            OriginalSize_Textblock.Text = fi.Length.ToString()+" bytes";

            StreamWriter sw2 = new StreamWriter("textForSizeCompressed.txt", false);
            sw2.Write(CompressedText_Textbox.Text);
            FileInfo fi2 = new FileInfo("textForSizeCompressed.txt");
            CompressedSize_TextBlock.Text = fi2.Length.ToString()+" bytes";
            sw.Close();
            sw2.Close();
            
        }

        private void ToDecompressButton_Click(object sender, RoutedEventArgs e)
        {
            string text = CompressedText_Textbox.Text;
            List<int> compressedTextList = new List<int>(text.Split(' ').Select(int.Parse));
            string originalText = Compression.ToDecompress(compressedTextList);
            DecompressedText_Textbox.Text = originalText;

        }

        private void PullTxt_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы txt | *.txt";

            if (OPF.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(OPF.FileName);

                StreamReader reader = new StreamReader(fileInfo.Open(FileMode.Open, FileAccess.Read), Encoding.GetEncoding(1251));

                Text_TextBox.Text = reader.ReadToEnd();
                reader.Close();

            }
        }
    }
}
