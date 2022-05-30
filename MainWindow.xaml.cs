using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        private void ToDecompress_Button_Click(object sender, RoutedEventArgs e)
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

        private void Ru_Button_Click(object sender, RoutedEventArgs e)
        {
            ForLang1.Text = "Исходный файла";
            ForLang2.Text = "Сжатый вид файла";
            ForLang3.Text = "Разжатый вид файла";
            ForLang4.Text = "Размер исходного файла (bytes)";
            ForLang5.Text = "Размер исходного файла (длина текста)";
            ForLang6.Text = "Размер сжатого файла (bytes)";
            ForLang7.Text = "Размер сжатого файла (длина текста)";
            ToCompress_Button.Content = "Сжать";
            ToDecompress_Button.Content = "Разжать";
            PullTxt_Button.Content = "Загрузить текстовый файл";
            En_Button.Background = new SolidColorBrush(Colors.Black);
            En_Button.Foreground = new SolidColorBrush(Colors.White);
            Ru_Button.Background = new SolidColorBrush(Colors.White);
            Ru_Button.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void En_Button_Click(object sender, RoutedEventArgs e)
        {
            ForLang1.Text = "Source file";
            ForLang2.Text = "Compressed file view";
            ForLang3.Text = "Decompressed file view";
            ForLang4.Text = "Source file size (bytes)";
            ForLang5.Text = "Source file size (length)";
            ForLang6.Text = "Compressed file size(bytes)";
            ForLang7.Text = "Compressed file size(length)";
            ToCompress_Button.Content = "To compress";
            ToDecompress_Button.Content = "To decompress";
            PullTxt_Button.Content = "Upload txt file";
            Ru_Button.Background = new SolidColorBrush(Colors.Black);
            Ru_Button.Foreground = new SolidColorBrush(Colors.White);
            En_Button.Background = new SolidColorBrush(Colors.White);
            En_Button.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            byte[] bytes;
            if (OPF.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(OPF.FileName);
                bytes = System.IO.File.ReadAllBytes(fileInfo.FullName);

                OriginalSize_Textblock.Text = fileInfo.Length.ToString();

                string byteInString = Compression.ByteArrayToString(bytes);
                Text_TextBox.Text = byteInString;
                List<int> compr = Compression.ToCompress(byteInString);
                string resultString = string.Join(" ",compr);
                CompressedText_Textbox.Text = resultString;

                /*List <int> compr = new List<int>(resultString.Split(' ').Select(int.Parse));*/
                string decompr = Compression.ToDecompress(compr);
                DecompressedText_Textbox.Text = decompr;

            }
            

        }

        private void Text2_Click(object sender, RoutedEventArgs e)
        {
            byte[] a = { 120, 130, 140 };
            string b = Compression.ByteArrayToString(a);
            CompressedText_Textbox.Text = b;
            List<int> d = new List <int>(b.Split(' ').Select(int.Parse));
            byte[] c = Compression.ListIntToByteArray(d);
            CompressedText_Textbox.Text += "\n" + Compression.ByteArrayToString(c);
        }

        private void PullFile_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            byte[] bytes;
            if (OPF.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(OPF.FileName);
                bytes = System.IO.File.ReadAllBytes(fileInfo.FullName);
                OriginalSize_Textblock.Text = fileInfo.Length.ToString();
                string byteInString = Compression.ByteArrayToString(bytes);
                Text_TextBox.Text = byteInString;
                OriginalLength_Textblock.Text = byteInString.Length.ToString();
            }
        }

        private void ToCompressFile_Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> compressedText = Compression.ToCompress(Text_TextBox.Text);
            string resultString = string.Join(" ", compressedText);
            CompressedText_Textbox.Text = resultString;
            byte[] byteArray = Compression.ListIntToByteArray(compressedText);
            File.WriteAllBytes("resultfile", byteArray);
            FileInfo fileInfo = new FileInfo("resultfile");
            CompressedSize_TextBlock.Text = fileInfo.Length.ToString();
            CompressedLength_TextBlock.Text = resultString.Length.ToString();
        }

        private void ToDecompressFile_Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> compr = new List<int>(CompressedText_Textbox.Text.Split(' ').Select(int.Parse));
            string decompr = Compression.ToDecompress(compr);
            DecompressedText_Textbox.Text = decompr;
        }
    }
}
