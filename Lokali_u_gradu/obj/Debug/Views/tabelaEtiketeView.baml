   M S B A M L   `   `   `  ����I  ELokali_u_gradu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Lokali_u_gradu.ViewsLokali_u_gradu.Views  5      ,    &Lokali_u_gradu.Views.tabelaEtiketeView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Lokali_u_gradu.Views     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��50 �6   �� 5      �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ txtPretraga��5      $��0��6'   $��3��67   $��
0,10,68,10q�6K   6_   �� 5   
   -   5         ��Name$ lupaIMG��5      $g��aD:\\6. semestar- VEZBANJE\\HCI\\ProjekatPonovo\\Lokali_u_gradu\\Lokali_u_gradu\\Resursi\\lupa.png��6!   $��131,10,20,10q�6�   $��2��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ tableGridEtikete��5      $��1��6S   $��0��6`   $��3��6p      
IsReadOnly .    AutoGenerateColumns .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��6-   ��  �� Path$ Etikete�	   Columns	 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Oznaka��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ sizeToCells 69      CellTemplate 5!      �� 5"      .R�5$      ��  �� Orientation$ 
Horizontal=�6)   .+�5%   "   �� ��6,   �� *ID+5'      5)      5,        5.      $

 Boja��6)   $ sizeToCells 67    50      �� 51      .R�53      �� $ 
Horizontal=�6)   .+�54   "   �� $��30��6*   ��65   �� *Boja+56      58      5;      0  *System.Windows.Controls.DataGridTextColumn  5>      $

 Opis��6%   $ SizeToCells 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *Opis+6b   5A      �� 5C   
   -   5         ��Name$ btnDodajEtiketu��5C      $��Dodaj novu etiketu��6*   $��18(�6G   ���	#FF1A2F98$��Stretch=�6m   $��2��6�   $��0��6�   $��Bottom=�6�   $��50��6�    J� Opacity$ 0.685Q�6�   $��1q�6�   ���	#FF9C9595$��Verdana)�6  6"  �� 5D   
   -   5        $ btnIzmeniEtiketu��5D      $��Izmeni etiketu��6+   $��18(�6D   ���	#FF1A2F98$��Stretch=�6j   $��2��6�   $��1��6�   $��Bottom=�6�   $��50��6�   $ 0.685Q�6�   $��1q�6�   ���	#FF9C9595$��Verdana)�6
  6  �� 5E   
   -   5        $ btnObrisiEtiketu��5E      $��Obriši etiketu��6+   $��18(�6D   ���	#FF1A2F98$��Stretch=�6j   $��2��6�   $��2��6�   $��Bottom=�6�   $��50��6�   $ 0.685Q�6�   $��1q�6�   ���	#FF9C9595$��Verdana)�6
  6  5H      5I      