   M S B A M L   `   `   `  ����I  ELokali_u_gradu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Lokali_u_gradu.ViewsLokali_u_gradu.Views  5      +    %Lokali_u_gradu.Views.tabelaTipoviView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Lokali_u_gradu.Views     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��50 �6   �� 5      �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ txtPretraga��5      $��0��6'   $��3��67   $��
0,10,68,10q�6K   6_   �� 5   
   -   5         ��Name$ lupaIMG��5      $g��aD:\\6. semestar- VEZBANJE\\HCI\\ProjekatPonovo\\Lokali_u_gradu\\Lokali_u_gradu\\Resursi\\lupa.png��6!   $��131,10,20,10q�6�   $��2��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ tableGridTipL��5      $��1��6U   $��0��6b   $��3��6r      
IsReadOnly .    AutoGenerateColumns .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��6*   ��  �� Path$ TipoviLokala�	   Columns	 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$	
 Tip��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ sizeToCells 66      CellTemplate 5!      �� 5"      .R�5#      ��  �� Orientation$ 
Horizontal=�6)   .+�5$   "   �� ��6,   �� *Ime+5%      5&      5)      0  *System.Windows.Controls.DataGridTextColumn  5*      $
 Oznaka��6%   $ SizeToCells 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6I   �� *ID+6b     5+      $

 Opis��6%   $ SizeToCells 63    6G   �� *Opis+6b   5.      �� 50   
   -   5         ��Name$ btnDodajTipL��50      $��Dodaj novi tip��6'   $��20(�6@   ���	#FF1A2F98$��Stretch=�6f   $��2��6�   $��0��6�   $��Bottom=�6�   $��50��6�    J� Opacity$ 0.685Q�6�   $��1q�6�   ���	#FF9C9595$��Verdana)�6  6  �� 51   
   -   5        $ btnIzmeniTipL��51      $��Izmeni tip lokala��6(   $��20(�6D   ���	#FF1A2F98$��Stretch=�6j   $��2��6�   $��1��6�   $��Bottom=�6�   $��50��6�   $ 0.685Q�6�   $��1q�6�   ���	#FF9C9595$��Verdana)�6	  6  �� 52   
   -   5        $ btnObrisiTipL��52      $��Obriši tip lokala��6(   $��20(�6D   ���	#FF1A2F98$��Stretch=�6j   $��2��6�   $��2��6�   $��Bottom=�6�   $��50��6�   $ 0.685Q�6�   $��1q�6�   ���	#FF9C9595$��Verdana)�6	  6  56      57      