   M S B A M L   `   `   `  ����  DICOMApp    WpfApp3.TagDisplayWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
DICOM Tags��5   	   $	��600��6   $
��1100��6)   .��5   	   � .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ TagDataGrid��5         AutoGenerateColumns$ False��6+   $��10q�6G   $��Stretch=�5      $��Stretch=�64   "   VerticalScrollBarVisibility$ Visible=�6P      CanUserAddRows$ False��5	         
IsReadOnly$
 True��6-    � 	Resources 5
      )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Triggers	 �� Binding
 �� Value �� Property �� Value��%�   '        �� 5      "  6    5      �� 5      	 6&   �� *HasNestedTags+
 True6H   .a�5      ��  � ��$
 Bold"�6;   5      5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 Tag��6(      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6M   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Tag+6M     5      $ Description��6(   $
 2.5* 6X    6=   �� *Descrp+6X     5      $ VR��6(   $ 1* 6Q    64   �� *
ValueRep+6Q     5      $ VM��6(   $ 1* 6S    64   �� *
ValueMulti+6S     5      $ Length��6(   $ 1* 6X    68   �� *ValueLength+6X     5      $ Value��6(   $
 3.5* 6Q    67   �� *Value+6Q   5      5      5!      