   M S B A M L   `   `   `  ���� A  =Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ParserParser      Parser.ParserSetup   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     &localclr-namespace:Parser     -     N� Title$  ParserSetup��$	��530��.��� .+�� $��10,10,10,424.6q�.+��� -    ��Name$ ProductTypeT��$��Тип продукта��$
��Left=�$	��Top=�$��25(�$��55��$	��211��$��10,0,0,0q�'  !MaterialDesignThemes.Wpf.PopupBox     PlacementMode$ LeftAndAlignMiddles=�   UnfurlOrientation$ 
Horizontal=� � ToolTip$ Manage items��$��Right=�$	��Top=�$��55��   PopupContent. ��  �� Orientation$ 
Horizontal=�����  �� Color$ White�� �� Opacity$ 0Q�.+��� -   	 ��Name$	 AddItem�� &  "MaterialDesignFloatingActionButton#��[  $% Добавить продукт������ *
AddCommand+.��F� |������!@]� �@]� @]� �@]� ��!��ا ��!��ا @]� �@KL @]� �@KL �ا ��ا �ا ��ا @KL �@]� @KL �@]� �ا ���!�ا ���!@]� 	
 �� Stretch$
 Uniform=�$��15��$��15������ *
Foreground+ �� RelativeSource ��  �� AncestorType" ���� -   $	 DeletionItem��#��[  $# Удалить продукт������ *RemoveSelectedItemCommand+.��F� �������! 	= ����  	= Ѡ@� ��- �`�� ��- � ��  	= �@KL  	= �@KL ��[ ���!��[ ���[ ��!� z @o@ �� �� � $� @o@׀���! �� �� р���j р�[ ��j р�[ ��!	$
 Uniform=�$��15��$��15������ *
Foreground+ �� " ���� -    ��Name$ ProductComboBox��   MaterialDesignFilledComboBox#��[ )  #MaterialDesignThemes.Wpf.HintAssist   Hint$% Выбирите продукт��$��
409,0,39,0q�$��55��$	��Top=�{��� *	IsChecked+ �� ElementName$# FilledComboBoxEnabledCheckBox��� -    �Name$ ParamG��$��10,65,10,114.6q�.+��� -   $ NumberLinesL��$5��/Укажите количество строк:��$
��Left=�$	��Top=�$��25(�$��	10,10,0,0q�$	��363���� -    ��Name$ NumberLinesT��$��42��$��418,10,10,0q� �� TextWrapping$
 Wrap=�$��20��$	��Top=�$��25(��� -	   $ MainColumnsL��$;��5Укажите количество столбцов:��$
��Left=�$��	10,57,0,0q�$	��Top=�$��25(�$	��363���� -
   $ MainColumnsT��$��42��$��419,58,10,0q�$
 Wrap=�$��2��$	��Top=�$��25(��� -   $ PathRezL��$&�� Путь к результату��$
��Left=�$��
10,104,0,0q�$	��Top=�$��25(�$	��363���� -   $ PathRezT��$��42��$��419,105,73,0q�$
 Wrap=�$,��&C:\Users\Andrey\Desktop\work\MyRez.ini��$	��Top=�$��25(��� -   #��[  $( "MaterialDesignFloatingActionButton��$��
0,103,10,0q�$��58��$��43��$��Right=�$	��Top=�.��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 
FolderOpen=�$��43��$��44���� -   $ FormattingL��$#��Укажите отступы��$
��Left=�$��
10,198,0,0q�$	��Top=�$��25(�$	��363���� -   $ FormattingT��$��42��$��419,198,10,0q�$
 Wrap=�$��1;3;1;3��$	��Top=�$��25(��� -   $ TitleL��$%��Заголовки блоков��$
��Left=�$��
10,151,0,0q�$	��Top=�$��25(�$	��363���� -   $ TitleT��$��42��$��419,151,10,0q�$
 Wrap=�$��--- RAPORT ---  ��$	��Top=�$��25(��� -   $ PackL��$#��Кол-во в коробке��$
��Left=�$��
10,245,0,0q�$	��Top=�$��25(�$	��363���� -   $ PackT��$��42��$��418,245,11,0q�$
 Wrap=�$��30��$	��Top=�$��25(��� -   $��PresenceHeaders��$)��#Наличие заголовков��$��418,292,7,0q�$	��Top=�$��52��$��30(��� -   $		 run�� " MaterialDesignFlatDarkBgButton#��[ $@ :Выполняет форматирование файла��$��Выполнить��$��10,415,10,0q�$��52��$	��Top=�� $��10,474,10,0q�$��54��$	��Top=�.+�� -    �Name$ 	ProgressB��$��48��$	��Top=��� -   $ 
BarLineAll��$�� ��$
��Left=�$��	597,0,0,0q�$	��Top=�$��48��$	��182��$��25(�$��Right=��� -   $ 
BarLineNau��$�� ��$
��Left=�$	��Top=�$��48��$	��177��$��25(�