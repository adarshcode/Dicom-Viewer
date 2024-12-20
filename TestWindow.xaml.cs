using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using FellowOakDicom;
using static WpfApp3.TagDisplayWindow;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public List<DicomTagData> DicomItems { get; }
        public TestWindow(List<DicomElement> dicomElements)
        {
            InitializeComponent();

            DicomItems = dicomElements.Select(element =>
                new DicomTagData
                {
                    Tag = element.Tag.ToString(),
                    DictEntry = element.Tag.DictionaryEntry.ToString(),
                    VM = element.Tag.DictionaryEntry.ValueMultiplicity.ToString(),
                    Keyword = element.Tag.DictionaryEntry.Keyword.ToString(),
                    Name = element.Tag.DictionaryEntry.Name.ToString(),
                    Element = element.Tag.Element.ToString(),
                    Group = element.Tag.Group.ToString(),
                    PrivCreator = "null" ?? element.Tag.PrivateCreator.ToString(),
                    Ispriv = element.Tag.IsPrivate.ToString(),
                    MaskTag = "null" ?? element.Tag.DictionaryEntry.MaskTag.ToString(),
                    IsRetired = element.Tag.DictionaryEntry.IsRetired.ToString(),
                    Code = element.ValueRepresentation.Code.ToString(),
                    VRName = element.ValueRepresentation.Name.ToString(),

                }).ToList();

            TestGrid.ItemsSource = DicomItems;
        }

        public class DicomTagData
        {
            public string Tag { get; set; }
            public string DictEntry { get; set; }
            public string VM { get; set; }
            public string Keyword { get; set; }
            public string Name { get; set; }
            public string Element { get; set; }
            public string Group { get; set; }
            public string PrivCreator { get; set; }
            public string Ispriv { get; set;}
            public string MaskTag { get; set; }
            public string IsRetired { get; set;}
            public string Code { get; set;}
            public string VRName { get; set; }

        }
    }

    
}
