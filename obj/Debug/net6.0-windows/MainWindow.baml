   M S B A M L   `   `   `  ����C  ?DICOMApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp3WpfApp3  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	       WpfApp3.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp3     5   	   Dwpf4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   R NOxyPlot.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5   	     � Title$  DICOM Viewer��5
   	   $	��859��6   $
��1334��6+   .��5      �  � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��0.25* �6   .+�5   
   �� $��0��6   
���   �$��1q�65   $��0,0,0,54q�6I   $��2��6[   ._�5      �� -   5         ��Name$ 
ImgDisplay��5   "   $��0��6   $��Uniform=�66   $��9,9,9,9q�6H    J� RenderTransformOrigin$ 0.436,0.504&�6Y   5      �� 5   
   $	��Top=�6   $��0,10,0,0q�6-    �� Orientation$ 
Horizontal=�6?   $��Center=�6X   $��49��6u   $��1��6�   $ 0.526,0.248&�6�   .+�5      �� -   5        $��Previous��5      $��80��6(   $��25��63   $��5q�6?   6J   �� 5      -   5        $
��Next��5      $��80��6$   $��25��6/   $��50,5,5,5q�6;   6M   5      �� 5   
   $��1��6   .+�5       �� -   5        $��Upload��5       $
��Left=�6&   $��25��6A   $��	10,40,0,0q�6M   $	��Top=�6`   $��77��6x   $ 
1.936,0.59&�6�   �� 5!      -   5         ��Name$ FileName��5!      $
��Bold"�6&   $�� ��68   $
��Left=�6C   $��25��6^   $��	10,10,0,0q�6j   $	��Top=�6}   �� 5"      -   5        $��	Show Tags��5"      $
��Left=�6)   $��25��6E   $��77��6Q   $��	10,10,0,0q�6\   6o   5#      �� 5%   
   $��1��6   $	��Top=�6%   $	��238��6=   $	��144��6I   $��Right=�6V   $��0,35,0,0q�6r   .+�5&      ]� $��Center=�6   $��5q�63   .+�5'      �� $
��Left=�6   $
��Bold"�6.   .��6@   R:6D   �� 5(      $��Right=�6q    �� TextAlignment$ Right=�6�   $��40��6�   ��6   ��  �� ElementName$ slColorR��	 �� Path$	 Value�
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 5)      -   5        $��slColorR��5)   p    � Maximum$	 255Q�6    �� TickPlacement$ BottomRight=�6'    �� TickFrequency$ 3Q�6C    �� IsSnapToTickEnabled$
 True��6U   6�   5*      ]� 5,      $��Center=�6   $��5q�63   .+�5-      �� $
��Left=�6   $
��Bold"�6.   .��6@   G:6D   �� 5.      $��Right=�6q   $ Right=�6�   $��40��6�   ��6   �� $ slColorG��$	 Value�$
 PropertyChanged=�6�   �� 5/      -   5        $��slColorG��5/   p   $	 255Q�6   $ BottomRight=�6'   $ 3Q�6C   $
 True��6U   6�   50      ]� 52      $��Center=�6   $��5q�63   .+�53      �� $
��Left=�6   $
��Bold"�6.   .��6@   B:6D   �� 54      $��Right=�6q   $ Right=�6�   $��40��6�   ��6   �� $ slColorB��$	 Value�$
 PropertyChanged=�6�   �� 55      -	   5        $��slColorB��55   p   $	 255Q�6   $ BottomRight=�6'   $ 3Q�6C   $
 True��6U   6�   56      57      �� 59   
   $��1��6   $	��Top=�6%   $��Center=�6=   $��0, 70, 100, 40q�6Z   .+�5:      	� -
   5        $��
Monochrome��5:   G   $��25��6   $	��100��6&   $��
Monochrome��62   $
��Bold"�6Y   $ 0.504,1.596&�6k   6�   	� 5;      -   5        $	��RGB��5;   @   $��25��6   $	��100��6&   $	��RGB��62   $
��Bold"�6K   $ 0.504,1.596&�6^   6�   5<      �� 5>   
   $��1��6   $	��Top=�6%   $	��357��6=   $	��101��6I   $
��Left=�6V   $��
80,244,0,0q�6q   $ 0.507,1.488&�6�   .+�5?      ]� $��Center=�6   $��10q�63   .+�5@      �� $
��Left=�6   $
��Bold"�6.   .��6@   Window Width:6O   �� 5A      $��Right=�6t   $ Right=�6�   $��40��6�   ��6   �� $ windowWidth��$	 Value�$
 PropertyChanged=�6�   �� 5B      -   5        $��windowWidth��5B   }    � Minimum$ 0Q�6   $
 2000Q�6%   $ BottomRight=�64   $ 1Q�6P   $
 True��6b   6�   5C      ]� 5E      $��Center=�6   $��10q�63   .+�5F      �� $
��Left=�6   $
��Bold"�6.   .��6@   Window Center:6P   �� 5G      $��Right=�6u   $ Right=�6�   $��40��6�   ��6   �� $ windowCenter��$	 Value�$
 PropertyChanged=�6�   �� 5H      -   5        $��windowCenter��5H   �   $ -1000Q�6   $
 1000Q�6)   $ BottomRight=�68   $ 1Q�6T   $
 True��6f   6�   5I      5J      � 5L   
   $��
800,0,0,10q�6   $��2��6#   $��2��67   5\      5^      5_      