using FellowOakDicom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static WpfApp3.TagDisplayWindow;

namespace WpfApp3
{
    public partial class NestedTagsWindow : Window
    {
        public List<DicomTagData> NestedTagItems { get; set; }

        public NestedTagsWindow(List<DicomElement> nestedTags, DicomFile file)
        {
            InitializeComponent();

            //NestedTags = nestedTags;

            NestedTagItems = nestedTags.Select(element =>
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
                        
                    };
                }

                return new DicomTagData
                {
                    Tag = element.Tag.ToString() ?? "null",
                    ValueRep = element.ValueRepresentation?.ToString() ?? "null",
                    ValueLength = element.Length,
                    Descrp = element?.ToString()?.Substring(14) ?? "null",
                    Value = System.Text.Encoding.ASCII.GetString(element.Buffer.Data) ?? "null",

                    //following is the previous code used for getting values of tags:-
                    //element.ValueRepresentation != DicomVR.OW ?  : BitConverter.ToString(element.Buffer.Data)

                    ValueMulti = element.Tag?.DictionaryEntry?.ValueMultiplicity?.ToString() ?? "null",

                    
                };


            }).ToList();

            NestedTagGrid.ItemsSource = NestedTagItems;
        }
    }

}
