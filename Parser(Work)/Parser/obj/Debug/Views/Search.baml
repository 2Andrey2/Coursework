   M S B A M L   `   `   `  ����A  =Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ParserParser  5   	       Parser.Search   5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Parser     5   	   -   5          � Title$  Search��5	   	   $	��606��6   $��790.8��6%   .��5
      � .+�5   
   �� -   5         ��Name$ PathSearchL��5      $��Путь к поиску��6&   $
��Left=�6>   $��	10,76,0,0q�6Y   $	��Top=�6l   $��25(�6�   $	��174��6�   �� 5   
   -   5         ��Name$ PathSearchT��5      $��42��6(   $��
189,78,0,0q�64    �� TextWrapping$
 Wrap=�6H   $,��&C:\Users\Andrey\Desktop\work\MyRez.ini��6\   $	��Top=�6�   $��25(�6�   $
��Left=�6�   $	��464��6�   �� 5   
   -   5         &  "MaterialDesignFloatingActionButton#��[  5       � ToolTip$( "MaterialDesignFloatingActionButton��6M   $��
658,78,0,0q�6z   $��58��6�   $��43��6�   $
��Left=�6�   $	��Top=�6�   .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 
FolderOpen=�6&   $��43��68   $��44��6D   5      �� 5   
   -   5        $
 BoxT��5      $��42��6!   $��222,123,0,0q�6-   $
 Wrap=�6B   $��Коробка��6V   $	��Top=�6e   $��25(�6}   $
��Left=�6�   $	��106��6�   �� 5   
   -   5        $ NumberT��5      $��40��6$   $��333,125,0,0q�60   $
 Wrap=�6E   $��Рапорт��6Y   $	��Top=�6g   $��25(�6   $
��Left=�6�   $��87��6�   �� 5   
   -   5        $ 	DormRoomT��5      $��39��6&   $��222,167,0,0q�62   $
 Wrap=�6G   $��Общий номер��6[   $	��Top=�6n   $��25(�6�   $
��Left=�6�   $	��198��6�   �� 5   
   -   5        $ ProductSerialT��5      $��39��6+   $��222,211,0,0q�67   $
 Wrap=�6L   $��
Серия��6`   $	��Top=�6m   $��25(�6�   $
��Left=�6�   $	��106��6�   �� 5   
   -	   5        $ ProductNumberT��5      $��39��6+   $��333,211,0,0q�67   $
 Wrap=�6L   $��
Номер��6`   $	��Top=�6m   $��25(�6�   $
��Left=�6�   $��87��6�   �� 5   
   -
   5         ��Name$ CheckBoxBox��5       d� IsThreeState .   d� 	IsChecked$ falseY�5      $&�� Поиск по коробкам��5      $��
10,133,0,0q�6)   $��20(�6=   $��32��6K   $	��Top=�6W   $
��Left=�6o   $	��207��6�   �� 5   
   -   5        $ CheckBoxAllline��5       .  $ falseY�5      $"��Поиск по номеру��5      $��
10,174,0,0q�6'   $��20(�6;   $��32��6I   $	��Top=�6U   $
��Left=�6m   $	��207��6�   �� 5   
   -   5        $ CheckBoxline��5        .  $ falseY�5"      $$��Поиск по строкам��5#      $��
10,218,0,0q�6(   $��20(�6<   $��32��6J   $	��Top=�6V   $
��Left=�6n   $	��207��6�   �� 5$   
   -   5        	 ��Name$	 SearchB��5$       " MaterialDesignFlatDarkBgButton#��[ 5%      $@ :Выполняет форматирование файла��5&      $��
Поиск��5'      $��	0,255,0,0q�6   $��40��60   $	��Top=�6<   $
��Left=�6T   $	��777��6o   6{   �� 5(   
   -   5        $ ProductTypeT��5(      $��Тип продукта��6&   $
��Left=�6=   $	��Top=�6X   $��25(�6p   $��61��6~   $	��174��6�   $��	10,10,0,0q�6�   $��Center=�6�   $��Center=�6�   �� 5)   
   -   5        
 ��Name$
 ProductComboBox��5*         MaterialDesignFilledComboBox#��[ 5+      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$% Выбирите продукт��5-      $��
189,10,0,0q�6?   $��61��6S   $	��Top=�6_   $
��Left=�6w   $	��332��6�   {�5,      �� *	IsChecked+ �� ElementName$# FilledComboBoxEnabledCheckBox��5-   �   �� 5.   
   -   5        $��SavingResult��5.      $$��Сохранить в файл��6%   $
��Left=�6@   $��595,218,0,0q�6[   $	��Top=�6p   $	��182��6�   6�   d� 5/   
   -   5        $��GraphicsModeC��5/       * &MaterialDesignSwitchAccentToggleButton#��[ 50      $, &MaterialDesignSwitchAccentToggleButton��51      $ falseY�52      $��731,182,10,363.8q�6   �� 53   
   -   5        $��GraphicsModeL��53      $'��!Графический режим��6%   $
��Left=�6A   $��647,153,0,0q�6\   $	��Top=�6q   $	��127��6�   �� 54   
   -   5        $��SearchResultsL��54      $
��Left=�6)   $	��267��6D   $��
10,300,0,0q�6Q   $	��Top=�6e   $	��767��6}    �� View 55      �� 56      .P�57      �� 58      5:      5;      5<      