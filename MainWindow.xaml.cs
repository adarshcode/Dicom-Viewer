using FellowOakDicom;
using FellowOakDicom.Imaging;
using FellowOakDicom.IO;
using LiveCharts;
using Microsoft.Win32;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<DicomElement> _dicomElements = new List<DicomElement>();
        private Dictionary<DicomSequence, List<DicomElement>> _dicomSequence = new Dictionary<DicomSequence, List<DicomElement>>();

        private byte _redAdjust;
        private byte _greenAdjust;
        private byte _blueAdjust;

        private double _windowWidth;
        private double _windowCenter;

        private bool _isRGB;

        private string _filePath;

        private byte[] _mytransformedPixelData;

        private int currFileIndex = 0;

        private List<string> selectedDicomFiles;

        //private ExceptionWindow exWin;

        private int[] modalityArray;

        private double[] volumeArray;

        //public ChartValues<int> BeforeHistogram { get; set; }
        public ChartValues<int> AfterHistogram { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //BeforeHistogram = new ChartValues<int>();
            AfterHistogram = new ChartValues<int>();

            DataContext = this;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    //Filter = "DICOM Files|*.dcm",
                    Multiselect = true
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    selectedDicomFiles = openFileDialog.FileNames.ToList();

                    currFileIndex = 0;

                    _filePath = selectedDicomFiles[currFileIndex];

                    if (!DicomFile.HasValidHeader(_filePath))
                        throw new Exception("Not a dicom file");

                }
            }
            catch (Exception ex)
            {
                var warnWindow = new WarningWindow(ex.Message);
                warnWindow.ShowDialog();
                //exWin = new ExceptionWindow(new Exception(ex.Message));
                //exWin.ShowDialog();
            }

        }

        private void processDicomFiles()
        {
            _filePath = selectedDicomFiles[currFileIndex];

            FileName.Content = Path.GetFileName(_filePath);

            DicomFile dicomFile = DicomFile.Open(_filePath);

            DicomDataset dataset = dicomFile.Dataset;

            foreach (var element in dataset)
            {

                if (element?.ValueRepresentation == DicomVR.SQ)
                {
                    // access the dicom sequence
                    DicomSequence sequence = dataset.GetSequence(element.Tag);

                    List<DicomElement> elements = new List<DicomElement>();

                    foreach (DicomDataset items in sequence.Items)
                    {
                        foreach (var item in items)
                        {
                            if (item.ValueRepresentation == DicomVR.SQ)
                            {
                                continue;
                            }
                             

                            elements.Add(item as DicomElement);
                        }

                    }

                    _dicomSequence.Add(sequence, elements);
                    // ProcessSequence(sequence);

                    // process each item in the sequence
                }
                else
                {
                    _dicomElements.Add(element as DicomElement);
                }

            }

            //RGB.IsChecked = false;
            //Monochrome.IsChecked = false;

            //slColorR.Value = 0;
            //slColorG.Value = 0;
            //slColorB.Value = 0;



        }

        private BitmapImage LoadAndConvertToBitmapImage(string filePath)
        {
            DicomFile dicomFile = DicomFile.Open(filePath);

            DicomImage dicomImage = new DicomImage(dicomFile.Dataset);

            PinnedIntArray pixelData = dicomImage.RenderImage().Pixels;

            int width = dicomImage.Width;
            int height = dicomImage.Height;

            byte[] transformedModalPixelData = /*dicomFile.Dataset.GetString(DicomTag.Modality).Equals("") ?*/ ApplyModalityTransformation(pixelData, dicomFile);// : GetPixelData(pixelData);

            modalityArray = new int[transformedModalPixelData.Length];
            volumeArray = new double[transformedModalPixelData.Length];

            for (int i = 0; i < transformedModalPixelData.Length; i++)
            {
                modalityArray[i] = (int)transformedModalPixelData[i];
            }

            byte[] transformedWindowPixelData = ApplyWindowing(transformedModalPixelData, dicomFile, _windowWidth, _windowCenter);

            var nvolumeArray = ApplyWindowing(transformedModalPixelData, dicomFile, 200, 90);

            for (int i = 0; i < transformedModalPixelData.Length; i++)
            {
                volumeArray[i] = (double)nvolumeArray[i];
            }

            _mytransformedPixelData = transformedWindowPixelData;

            byte[] coloredPixelData = ApplyColorAdjustments(transformedWindowPixelData);

            BitmapSource bitmapSource = BitmapSource.Create(
                width, height,
                96, 96,
                _isRGB ? PixelFormats.Pbgra32 : PixelFormats.Gray8, null,
            _isRGB ? coloredPixelData : transformedWindowPixelData, _isRGB ? width * 4 : width);


            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(memoryStream);

                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        private void ShowHistogram()
        {
            int[] afterHist = CalculateHistogram(_mytransformedPixelData);


            AfterHistogram.AddRange(afterHist);


            //AfterHistogramChart.Update(true);
        }

        private void ShowWindowPlot()
        {
            var plotModel = new PlotModel();

            var line = new OxyPlot.Series.LineSeries();

            for (int i = 0; i < modalityArray.Length; i++)
            {
                line.Points.Add(new DataPoint(modalityArray[i], volumeArray[i]));
            }

            plotModel.Series.Add(line);

            //plotView.Model = plotModel;

        }

        private byte[] GetPixelData(PinnedIntArray pixelData)
        {
            byte[] bytePixelData = new byte[pixelData.Count];

            for (int i = 0; i < pixelData.Count; i++)
            {
                bytePixelData[i] = (byte)(pixelData[i] & 0xFF);
            }

            return bytePixelData;
        }

        private byte[] ApplyModalityTransformation(PinnedIntArray pixelData, DicomFile dicomFile)
        {

            double rescaleIntercept = dicomFile.Dataset.TryGetString(DicomTag.RescaleIntercept, out string intercept) ? double.Parse(intercept) : double.Parse("-1024");
            double rescaleSlope = dicomFile.Dataset.TryGetString(DicomTag.RescaleSlope, out string slope) ? double.Parse(slope) : double.Parse("1");

            int minValue = int.MaxValue;
            int maxValue = int.MinValue;
            byte[] transformedPixelData = new byte[pixelData.Count];

            for (int i = 0; i < pixelData.Count; i++)
            {
                double originalValue = pixelData[i];
                int transformedValue = (int)(originalValue * rescaleSlope + rescaleIntercept);

                transformedPixelData[i] = (byte)transformedValue;

                if (transformedValue < minValue)
                    minValue = transformedValue;
                if (transformedValue > maxValue)
                    maxValue = transformedValue;
            }

            return transformedPixelData;
        }

        private byte[] ApplyColorAdjustments(byte[] grayScalePixelData)
        {

            byte[] coloredPixelData = new byte[grayScalePixelData.Length * 4];
            for (int i = 0; i < grayScalePixelData.Length; i++)
            {
                byte pixelValue = grayScalePixelData[i];

                coloredPixelData[i * 4] = (byte)(_blueAdjust * pixelValue / 255);    // Blue
                coloredPixelData[i * 4 + 1] = (byte)(_greenAdjust * pixelValue / 255); // Green
                coloredPixelData[i * 4 + 2] = (byte)(_redAdjust * pixelValue / 255);   // Red
                coloredPixelData[i * 4 + 3] = 255;                                       // Alpha (full opacity)
            }

            return coloredPixelData;
        }


        private byte[] ApplyWindowing(byte[] pixelData, DicomFile dicomFile, double ww, double wc)
        {
            double min = wc - 0.5 * ww;//_windowCenter - 0.5 * _windowWidth;
            double max = wc + 0.5 * ww;//_windowCenter + 0.5 * _windowWidth;

            for (int i = 0; i < pixelData.Length; i++)
            {
                double pixelValue = pixelData[i];

                // Mapping pixel values to the [0, 1] range
                //double displayedValue = (pixelValue - _windowCenter) / _windowWidth;

                //displayedValue = Math.Max(0, Math.Min(1, displayedValue));

                // Mapping values to range 0 to 255
                //byte displayedPixelValue = (byte)(displayedValue * 255);

                //pixelData[i] = displayedPixelValue;

                if (pixelValue < min)
                    pixelValue = 0;
                else if (pixelValue > max)
                    pixelValue = 255;
                else
                {
                    pixelValue = ((pixelValue - min) / (max - min)) * 255;
                }

                //volumeArray[i] = pixelValue;
                pixelData[i] = (byte)pixelValue;

            }

            return pixelData;
        }

        private int[] CalculateHistogram(byte[] pixelData)
        {
            int[] histogram = new int[256]; // 256 bins for grayscale

            foreach (byte pixelValue in pixelData)
            {
                histogram[pixelValue]++;
            }

            return histogram;
        }


        private void ShowTagsButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDicomFiles == null)
            {
                var win = new WarningWindow("No files uploaded!!! Please upload files !!!");
                win.ShowDialog();
                return;
            }

            try
            {

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            TagDisplayWindow tagDisplayWindow = new TagDisplayWindow(_dicomElements, _dicomSequence, _filePath);
            tagDisplayWindow.Show();

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectedDicomFiles == null)
            {
                var win = new WarningWindow("No files uploaded!!! Please upload files !!!");
                win.ShowDialog();
                return;
            }

            _redAdjust = (byte)slColorR.Value;
            _greenAdjust = (byte)slColorG.Value;
            _blueAdjust = (byte)slColorB.Value;

            BitmapImage bitmapImage = LoadAndConvertToBitmapImage(_filePath);

            ImgDisplay.Source = bitmapImage;
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectedDicomFiles == null)
            {
                var win = new WarningWindow("No files uploaded!!! Please upload files !!!");
                win.ShowDialog();
                return;
            }

            _windowWidth = windowWidth.Value;
            _windowCenter = windowCenter.Value;

            BitmapImage bitmapImage = LoadAndConvertToBitmapImage(_filePath);

            //ShowHistogram();
            //ShowWindowPlot();

            ImgDisplay.Source = bitmapImage;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (selectedDicomFiles == null)
            {
                var win = new WarningWindow("No files uploaded!!! Please upload files !!!");
                win.ShowDialog();
                return;
            }

            _isRGB = RGB.IsChecked == true;

            try
            {
                processDicomFiles();
            }
            catch
            {
                var exWin = new WarningWindow("Dicom file not uploaded. Please upload files");
                exWin.ShowDialog();
            }

            BitmapImage bitmapImage = LoadAndConvertToBitmapImage(_filePath);

            ShowHistogram();

            ShowWindowPlot();

            ImgDisplay.Source = bitmapImage;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDicomFiles == null)
            {
                var win = new WarningWindow("No files uploaded!!! Please upload files !!!");
                win.ShowDialog();
                return;
            }

            if (currFileIndex >= 0)
                currFileIndex--;

            if (currFileIndex - 1 >=0)
            {
                _dicomElements.Clear();
                _dicomSequence.Clear();
            }

            try
            {
                processDicomFiles();

                BitmapImage bitmapImage = LoadAndConvertToBitmapImage(_filePath);

                //ShowHistogram();

                //ShowWindowPlot();

                ImgDisplay.Source = bitmapImage;
            }
            catch (Exception ex)
            {
                var exWin = new WarningWindow("No previous Dicom file available.");
                exWin.ShowDialog();
            }


        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDicomFiles == null)
            {
                var win = new WarningWindow("No files uploaded!!! Please upload files !!!");
                win.ShowDialog();
                return;
            }

            if (currFileIndex < selectedDicomFiles.Count)
                currFileIndex++;

            
            if (currFileIndex-1 != selectedDicomFiles.Count - 1)
            {
                _dicomElements.Clear();
                _dicomSequence.Clear();
            }

            try
            {
                processDicomFiles();

                BitmapImage bitmapImage = LoadAndConvertToBitmapImage(_filePath);

                //ShowHistogram();

                //ShowWindowPlot();

                ImgDisplay.Source = bitmapImage;
            }
            catch (Exception ex)
            {
                
                var exWin = new WarningWindow("No next Dicom file available.");
                exWin.ShowDialog();

                

            }
        }

    }

}
