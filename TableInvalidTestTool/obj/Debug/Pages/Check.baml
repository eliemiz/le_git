   M S B A M L   `   `   `  ����  TableInvalidTestTool&     TableInvalidTestTool.Pages.Check   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Check��5      .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��list_view_rows��5      $��0��6K   ��6)   �� *	CheckRows+ �� View 5      �� 5      .P�5      �� $��Index��6%    �� Width$
 Auto��64    �� DisplayMemberBinding 6A   �� *Index+6j   �� 5      $��TagName��6%   $
 Auto��66    6C   �� *	TagName+6n   �� 5      $��	MasterKey��6%   $
 Auto��68    6E   �� *	MasterKey+6r   �� 5      $��SlaveKey��6%   $
 Auto��67    6D   �� *
SlaveKey+6p   �� 5      $��Check��6%   $
 Auto��64    �� CellTemplate 5      �� 5      .R�5   "   � .+�5   &   �� -   5        $��Check��5   B   6T   5   #   5      5       �� 5!      $��Result��6%   $
 Auto��65    6B   �� *Result+6l   5"      5$      �� 5%   
   -   5        $��
result_box��5%   @   $��1��6   $��Stretch=�6"   $
��Auto=�6R   .��6�   5&      5'      5(      