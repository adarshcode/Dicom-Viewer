using FellowOakDicom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp3
{
    public partial class TagDisplayWindow : Window
    {
        public DicomFile file;
        public List<DicomTagData> DicomItems { get; }
        public List<DicomTagData> DicomItemsSeq { get; }

        public TagDisplayWindow(List<DicomElement> dicomElements, Dictionary<DicomSequence, List<DicomElement>> dicomSequence, string path)
        {
            InitializeComponent();

            file = DicomFile.Open(path);

            DicomItems = dicomElements.Select(element =>
                 {
                     if (element == null)
                     {
                         return new DicomTagData
                         {
                             Tag = "null",
                             ValueRep = "null",
                             ValueLength = 0, // Adjust the default value as needed
                             Descrp = "null",
                             Value = "null",
                             ValueMulti = "null",
                             HasNestedTags = false,
                             // NestedTags = null,
                         };
                     }


                     return new DicomTagData
                     {
                         Tag = element.Tag?.ToString() ?? "null",//Tag.ToString(), 
                         ValueRep = element.ValueRepresentation?.ToString() ?? "null",
                         ValueLength = element.Length,
                         Descrp = element?.ToString()?.Substring(14) ?? "null",
                         Value = file.Dataset.GetString(element.Tag) ?? "null",

                         //following is the previous code used for getting values of tags:-
                         //element.ValueRepresentation != DicomVR.OW ? System.Text.Encoding.ASCII.GetString(element.Buffer.Data) : BitConverter.ToString(element.Buffer.Data)

                         ValueMulti = element.Tag?.DictionaryEntry?.ValueMultiplicity?.ToString() ?? "null",

                         HasNestedTags = false,

                         //NestedTags = element.Value,
                     };


                 }).ToList();

            DicomItemsSeq = dicomSequence.Select(element =>
                new DicomTagData
                {
                    Tag = element.Key.Tag.ToString(),//Tag.ToString(), 
                    ValueRep = element.Key.ValueRepresentation.ToString() ?? "null",
                    ValueLength = (uint)element.Key.Items.Count,
                    Descrp = element.Key.ToString()?.Substring(14) ?? "null",
                    //Value = file.Dataset.GetString(element.Key.Tag) ?? "null",

                    //following is the previous code used for getting values of tags:-
                    //element.ValueRepresentation != DicomVR.OW ? System.Text.Encoding.ASCII.GetString(element.Buffer.Data) : BitConverter.ToString(element.Buffer.Data)

                    ValueMulti = element.Key.Tag.DictionaryEntry.ValueMultiplicity.ToString() ?? "null",

                    HasNestedTags = true,

                    NestedTags = element.Value


                }).ToList();

            DicomItems = DicomItems.Concat(DicomItemsSeq).ToList();

            Random rn = new Random();

            DicomItems = DicomItems.OrderBy(x => rn.Next()).ToList();

            TagDataGrid.ItemsSource = DicomItems;
        }

        public class DicomTagData
        {
            public string Tag { get; set; }
            public string Value { get; set; }
            public string ValueRep { get; set; }
            public string Descrp { get; set; }
            public string ValueMulti { get; set; }
            public uint ValueLength { get; set; }
            public bool HasNestedTags { get; set; }

            public List<DicomElement> NestedTags { get; set; }
        }

        private void TagDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItems.Count == 1)
            {
                var selectedTag = (DicomTagData)dataGrid.SelectedItem;
                if (selectedTag.HasNestedTags)
                {
                    //Open a new window to display nested tags
                    DisplayNestedTagsWindow(selectedTag);
                }
            }
        }

        private void DisplayNestedTagsWindow(DicomTagData selectedTag)
        {
            NestedTagsWindow nestedTagsWindow = new NestedTagsWindow(selectedTag.NestedTags, file);
            nestedTagsWindow.Show();
        }

    }
}